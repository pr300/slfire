using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpIceControllerLib
{
    public enum ListStateFill{ prolog = 0x01, epilog = 0x02, body = 0x04, free = 0x10, ready = 0x20 };

    public struct listState
    {
        public ListStateFill filling;
        public readonly ListNumber number;
        public Int64 indexLayer;
        public Int64 size;
        public bool finishid;

        public listState(ListNumber num)
        {
            filling = ListStateFill.free;
            number = num;
            indexLayer = 0;
            size = 0;
            finishid = false;
        }

        public void reset()
        {
            filling = ListStateFill.free;
            indexLayer = 0;
            size = 0;
            finishid = false;
        }
    }

    internal static class PrefetchList
    {
        const long LIST_SIZE = 1000000;
        //const long LIST_SIZE = 10000;
        static public listState[] m_l;
        static StreamWriter m_logFile;
        static ListNumber m_currentList;
        static Int64 m_layerNumber;
        static public  bool m_lastListReady = false;
        static Queue<ListNumber> m_listQueue = new Queue<ListNumber>();

       static styles  m_lastedStyle;
       static bool m_isNeedRestoreStyleOnProlog = false;
        

        static PrefetchList()
        {
            m_l = new listState[2]
            {
                new listState(ListNumber.list1),
                new listState(ListNumber.list2)
            };



            m_currentList = ListNumber.Undefine;
            m_logFile = new StreamWriter("prefetchLog.txt", false);

            //if (AppDomain.CurrentDomain.IsDefaultAppDomain())
            //    AppDomain.CurrentDomain.ProcessExit += terminate;
            //else
            //    AppDomain.CurrentDomain.DomainUnload += terminate;

            m_lastedStyle.lPower = long.MaxValue;
            m_lastedStyle.lMarkSize = long.MaxValue;

        }

        static internal void terminate() //(object sender, EventArgs e)
        {
            m_logFile.Close();
        }

        static public void resetList()
        {
            m_l[0].reset();
            m_l[1].reset();
            m_layerNumber = 0;
            m_currentList = ListNumber.Undefine;
            m_lastListReady = false;
            m_lastedStyle.lPower = long.MaxValue;
            m_lastedStyle.lMarkSize = long.MaxValue;
        }

        public static void stepExecution()
        {
            m_lastListReady = !fileLoader.isAviableNExt() && (fileLoader.isValidFile == 0);

            if (m_lastListReady)
            {
                m_l[(Int32)m_currentList].filling = ListStateFill.free;
                return;
            }

            if (m_currentList == ListNumber.Undefine)
            {
                m_currentList = getNextFreeList();

                if (m_currentList == ListNumber.Undefine) return;
            }


            switch (m_l[(Int32)m_currentList].filling)
            {
                case ListStateFill.free:
                    openList();
                    break;
                case ListStateFill.prolog:
                    fillProlog();
                    break;
                case ListStateFill.body:
                    bobyStartList();
                    break;
                case ListStateFill.epilog:
                    fillEpilog();
                    break;
                case ListStateFill.ready:
                   // m_listQueue.Enqueue(m_currentList);
                    m_currentList = ListNumber.Undefine;
                    break;
                default:
                    break;
            }
        
        }

        private static void openList()
        {
            if (!fileLoader.m_isPreambuleFinish) return;

            if (m_currentList == ListNumber.list1)
                NativeMethods.PCI_Set_Start_List_1();
            else
                NativeMethods.PCI_Set_Start_List_2();

            styles st1 = fileLoader.m_cs.style1;

            st1.lMarkSize = fileLoader.m_globalStyle.lMarkSize;
            st1.lPower = fileLoader.m_globalStyle.lPower;

            //if (m_isNeedRestoreStyleOnProlog)
            {
                if (m_lastedStyle.lPower != long.MaxValue) st1.lPower = m_lastedStyle.lPower;
                if (m_lastedStyle.lMarkSize != long.MaxValue) st1.lMarkSize = m_lastedStyle.lMarkSize; 
            }

            NativeMethods.PCI_Set_Delays((UInt16)st1.lStep, (UInt16)st1.lJampDelay, (UInt16)st1.lMarkDelay, (UInt16)st1.lPolygon, (UInt16)st1.lLaserOff, (UInt16)st1.lLaserOn, (UInt16)st1.lQt1, (UInt16)st1.lQt2, 0);
            NativeMethods.PCI_Set_Mark_Parameters_List((UInt16)st1.lStep, (UInt16)st1.lMarkSize);
            NativeMethods.PCI_Long_Delay(10);
            NativeMethods.PCI_Write_DA_List((UInt16)st1.lPower);
            NativeMethods.PCI_Write_Port_List(0xC, 0x010);

            m_l[(Int32)m_currentList].indexLayer = m_layerNumber;
            m_l[(Int32)m_currentList].filling = ListStateFill.prolog;
            m_layerNumber++;
            m_l[(Int32)m_currentList].size = 0;
            m_l[(Int32)m_currentList].finishid = false;
        }

        private static void fillProlog()
        {
            m_l[(Int32)m_currentList].filling = ListStateFill.body;
        }

        private static void bobyStartList()
        {
           // writeLog("body start list");
            bool isEnd = false;
            while( fileLoader.isAviableNExt()){

                isEnd = decodeCommand(m_l[(Int32)m_currentList].size > LIST_SIZE);
                if (isEnd) break;
                
            }

            if (isEnd)
            {
                m_l[(Int32)m_currentList].filling = ListStateFill.epilog;

            }
        }


        private static bool decodeCommand(bool finishOnNearest = false) //return false if its last command in list
        {
            bool result = false;
            bool skipIncrement = false;
            Int64 iterator = fileLoader.getStartPos();
            switch (fileLoader.m_listJob[iterator].cmd)
            {
                case Command.EndF:
                    result = true;
                    break;
                case Command.StarLayer:
                    break;
                case Command.EndLayer:
                    m_l[(Int32)m_currentList].finishid = true;
                    result = true;
                    break;
                case Command.Jamp:
                    NativeMethods.PCI_Jump_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                    m_l[(Int32)m_currentList].size++;
                    skipIncrement = finishOnNearest;
                    result = finishOnNearest;
                    break;
                case Command.Mark:
                    NativeMethods.PCI_Mark_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                    m_l[(Int32)m_currentList].size++;
                    break;
                case Command.PolA_Abs:
                    NativeMethods.PCI_PolA_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                    m_l[(Int32)m_currentList].size++;
                    break;
                case Command.PolB_Abs:
                    NativeMethods.PCI_PolB_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                    m_l[(Int32)m_currentList].size++;
                    break;
                case Command.PolC_Abs:
                    NativeMethods.PCI_PolC_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                    m_l[(Int32)m_currentList].size++;
                    result = finishOnNearest;
                    break;
                case Command.Power:
                    NativeMethods.PCI_Write_DA_List((UInt16)fileLoader.m_listJob[iterator].x);
                    m_l[(Int32)m_currentList].size++;
                    m_lastedStyle.lPower = (UInt16)fileLoader.m_listJob[iterator].x;
                    break;
                case Command.MarkSize:
                    NativeMethods.PCI_Set_Mark_Parameters_List((UInt16)fileLoader.m_listJob[iterator].x, (UInt16)fileLoader.m_listJob[iterator].y);
                    m_l[(Int32)m_currentList].size++;
                    m_lastedStyle.lStep= (UInt16)fileLoader.m_listJob[iterator].x;
                    m_lastedStyle.lMarkSize = (UInt16)fileLoader.m_listJob[iterator].y;
                    break;

            }

            if (!skipIncrement)
            fileLoader.incrementStart();

            return result;
        
        }

        private static void fillEpilog()
        {
            NativeMethods.PCI_Write_Port_List(0xC, 0x000);
            NativeMethods.PCI_Set_End_Of_List();
            if(m_l[(Int32)m_currentList].size>0)
            m_l[(Int32)m_currentList].filling = ListStateFill.ready;
            else
                m_l[(Int32)m_currentList].filling = ListStateFill.free;

            m_isNeedRestoreStyleOnProlog = !m_l[(Int32)m_currentList].finishid;
        }

        private static ListNumber getNextFreeList()
        {
            if (m_l[0].filling == ListStateFill.free) return ListNumber.list1;
            if (m_l[1].filling == ListStateFill.free) return ListNumber.list2;

            return ListNumber.Undefine;
        }

        public static ListNumber getNextReadyList()
        {
           // return m_listQueue.Count > 0 ? m_listQueue.First() : ListNumber.Undefine;

            Int64 l1 = m_l[0].filling == ListStateFill.ready ? m_l[0].indexLayer : -1;
            Int64 l2 = m_l[1].filling == ListStateFill.ready ? m_l[1].indexLayer : -1;

            if(l1 != -1  || l2 != -1)
            {
                if (l1 == -1) return ListNumber.list2;
                if (l2 == -1) return ListNumber.list1;
                return l1 < l2 ? ListNumber.list1 : ListNumber.list2; 
            }

            return ListNumber.Undefine;
        }

        public static void setFree(ListNumber list)
        {
            if (list == ListNumber.Undefine) return;
            m_l[(Int32)list].filling = ListStateFill.free;
        }

        internal static bool isOneListOnLayer(ListNumber list)
        {
            if (list == ListNumber.Undefine) return true;
            return m_l[(Int32)list].finishid;
        }

        private static Int64 getCurrentSize()
        {
            if (m_currentList == ListNumber.Undefine) return -1;
            return m_l[(Int32)m_currentList].size;
        }


        public static string getListState(ListNumber list)
        {
            if (list == ListNumber.Undefine) return "Undefine list..";
            string res = "";

            res = string.Format("{0, 6}: {1, -20} layer: {2, -6} size: {3, 6} fin: {4, 5}", list.ToString(), m_l[(Int32)list].filling.ToString(), m_l[(Int32)list].indexLayer.ToString("X6"), m_l[(Int32)list].size.ToString("D6"), m_l[(Int32)list].finishid);//
            return res;
        }


    }



}
