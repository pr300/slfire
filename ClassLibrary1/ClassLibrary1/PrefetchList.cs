using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ClassLibrary1
{
    public enum ListStateFill{ prolog = 0x01, epilog = 0x02, bobyStart = 0x04, bobyContinue = 0x08, free = 0x10, ready = 0x20 };
    public enum ListNumber {list1 = 0, list2 = 1, Undefine };

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
        static public  bool m_lastListReay = false;

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

            writeLog("start prefetchList");
        }

        static internal void terminate() //(object sender, EventArgs e)
        {
            m_logFile.Close();
            writeLog("close prefetchList");
        }

        static public void resetList()
        {
            m_l[0].reset();
            m_l[1].reset();
            m_layerNumber = 0;
            m_currentList = ListNumber.Undefine;
            m_lastListReay = false;
        }

        public static void stepExecution()
        {
            m_lastListReay = !fileLoader.isAviableNExt() && (fileLoader.isValidFile == 0);

            if (m_lastListReay)
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
                case ListStateFill.bobyStart:
                    bobyStartList();
                    break;
                case ListStateFill.bobyContinue:
                    break;
                case ListStateFill.epilog:
                    fillEpilog();
                    break;
                case ListStateFill.ready:
                    m_currentList = ListNumber.Undefine;
                    break;
                default:
                    break;
            }
        
        }

        private static void openList()
        {


            if (m_currentList == ListNumber.list1)
                Class1.PCI_Set_Start_List_1();
            else
                Class1.PCI_Set_Start_List_2();

            styles st1 = fileLoader.m_cs.style1;
            Class1.PCI_Set_Delays((UInt16)st1.lStep, (UInt16)st1.lJampDelay, (UInt16)st1.lMarkDelay, (UInt16)st1.lPolygon, (UInt16)st1.lLaserOff, (UInt16)st1.lLaserOn, (UInt16)st1.lQt1, (UInt16)st1.lQt2, 0);
            Class1.PCI_Long_Delay(10);
            Class1.PCI_Write_DA_List((UInt16)st1.lPower); ///???
            Class1.PCI_Write_Port_List(0xC, 0x010);

            m_l[(Int32)m_currentList].indexLayer = m_layerNumber;
            writeLog("open list");
            m_l[(Int32)m_currentList].filling = ListStateFill.prolog;
            m_layerNumber++;
            m_l[(Int32)m_currentList].size = 0;
            m_l[(Int32)m_currentList].finishid = false;
        }

        private static void fillProlog()
        {
            writeLog("prolog fill");
            m_l[(Int32)m_currentList].filling = ListStateFill.bobyStart;
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
                writeLog("body start list filling finish");
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
                    writeLog("command: End of File");
                    result = true;
                    break;
                case Command.StarLayer:
                    writeLog("command: Start layer");
                    break;
                case Command.EndLayer:
                    writeLog("command: End layer");
                    m_l[(Int32)m_currentList].finishid = true;
                    result = true;
                    break;
                case Command.Jamp:
                    Class1.PCI_Jump_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                    m_l[(Int32)m_currentList].size++;
                    skipIncrement = finishOnNearest;
                    result = finishOnNearest;
                    writeLog("command: Jamp");
                    break;
                case Command.Mark:
                    Class1.PCI_Mark_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                    m_l[(Int32)m_currentList].size++;
                    writeLog("command: Mark_Abs");
                    break;
                case Command.PolA_Abs:
                    Class1.PCI_PolA_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                    m_l[(Int32)m_currentList].size++;
                    writeLog("command: PolA_Abs");
                    break;
                case Command.PolB_Abs:
                    Class1.PCI_PolB_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                    m_l[(Int32)m_currentList].size++;
                    writeLog("command: PolB_Abs");
                    break;
                case Command.PolC_Abs:
                    Class1.PCI_PolC_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                    m_l[(Int32)m_currentList].size++;
                    writeLog("command: PolC_Abs");
                    result = finishOnNearest;
                    break;
                case Command.Style:
                    writeLog("command: Style change");
                    styles st = fileLoader.m_listJob[iterator].x == 1 ? fileLoader.m_cs.style1 : fileLoader.m_listJob[iterator].x == 2 ? fileLoader.m_cs.style2 : fileLoader.m_cs.style3;
                    Class1.PCI_Write_DA_List((UInt16)st.lPower);
                    m_l[(Int32)m_currentList].size++;
                    //printDebug(iterator, "Write_DA_List", (Int16)st.lPower, 0);
                    Class1.PCI_Set_Mark_Parameters_List((UInt16)st.lStep, (UInt16)st.lMarkSize);
                    m_l[(Int32)m_currentList].size++;
                    //printDebug(iterator, "Set_Mark_Parameters_List", (Int16)st.lStep, (Int16)st.lMarkSize);
                    Class1.PCI_Set_Jump_Parameters_List((UInt16)st.lStep, (UInt16)st.lJampSize);
                    m_l[(Int32)m_currentList].size++;
                    //printDebug(iterator, "Set_Jump_Parameters_List", (Int16)st.lStep, (Int16)st.lJampSize);
                    //Class1.PCI_Set_Delays((UInt16)st.lStep, (UInt16)st.lJampDelay, (UInt16)st.lMarkDelay, (UInt16)st.lPolygon, (UInt16)st.lLaserOff, (UInt16)st.lLaserOn, (UInt16)st.lQt1, (UInt16)st.lQt2, 0);
                   // m_l[(Int32)m_currentList].size++;
                    break;

            }

            if (!skipIncrement)
            fileLoader.incrementStart();

            return result;
        
        }

        private static void fillEpilog()
        {
            Class1.PCI_Write_Port_List(0xC, 0x000);
            Class1.PCI_Set_End_Of_List();
            writeLog("fill epilog");
            if(m_l[(Int32)m_currentList].size>0)
            m_l[(Int32)m_currentList].filling = ListStateFill.ready;
            else
                m_l[(Int32)m_currentList].filling = ListStateFill.free;
        }

        private static ListNumber getNextFreeList()
        {
            if (m_l[0].filling == ListStateFill.free) return ListNumber.list1;
            if (m_l[1].filling == ListStateFill.free) return ListNumber.list2;

            return ListNumber.Undefine;
        }

        public static ListNumber getNextReadyList()
        {
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

        private static void writeLog(string str)
        {
            if (fileLoader.m_cs.debug)
            {
                m_logFile.WriteLine(string.Format("{0, -8}L:{1, -5} T:{2,-1} I:{3, -5} {4} ", DateTime.Now.ToString("h:mm:ss tt"), m_layerNumber.ToString("X5"), (Int32)m_currentList, getCurrentSize().ToString("X5"), str));
                m_logFile.Flush();
            }
        }


        public static string getListState(ListNumber list)
        {
            if (list == ListNumber.Undefine) return "Undefine list..";
            string res = "";

            res = string.Format("{0, 6} state: {1, -20} layer: {2, -6} size: {3, 6} fin: {4, 5}", list.ToString(), m_l[(Int32)list].filling.ToString(), m_l[(Int32)list].indexLayer.ToString("X6"), m_l[(Int32)list].size.ToString("D6"), m_l[(Int32)list].finishid);//
            return res;
        }
    }
}
