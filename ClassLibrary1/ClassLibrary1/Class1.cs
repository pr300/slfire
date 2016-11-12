using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support
using System.Windows.Forms;
using System.Security;
using System.IO;
using System.Threading;
using System.Reflection;


namespace ClassLibrary1
{
    public enum IntState { Wait = 0x01, Stop =0x02, Work = 0x04, WaitListReay = 0x08 };
    public enum IntSignals { Empty = 0x0, Run = 0x01, Stop = 0x02, Reset = 0x4, Pause = 0x8 };

    public struct styles
	{
		public long lStep;
        public long lLaserOn;
        public long lLaserOff;
        public long lPolygon;
        public long lMarkDelay;
        public long lJampDelay;
        public long lFps;
        public long lQt1;
        public long lQt2;
        public long lJampSize;
        public long lMarkSize;
        public long lPower;
	}

    public struct cardSetting
    {
        public UInt16 mode;
        public UInt16 power;
        public string corrFilePatch;
        public float scale;
        public UInt16 t1;
        public UInt16 t2;
        public UInt16 t3;
        public Int16 num;
        public string scriptPath;
        public bool doInit;
        public styles style1;
        public styles style2;
        public styles style3;
        public bool debug;

    };

    public struct cardStatus
    {
        public bool l1load;
        public bool l2load;
        public bool l1redy;
        public bool l2redy;
        public bool l1busy;
        public bool l2busy;
        public bool busy;
        public bool laserOn;
        public bool scanComlete;
        public bool b9;
        public bool b10;

    }


    public static class Class1
    {

        static Class1()
        {
            file = new StreamWriter("write_layer.txt", false);
            m_procesThreadAllowed = true;
            myThread = new Thread(threadProcessSignals);
            myThread.Start();

        }



        public static IntSignals m_inputSignals = IntSignals.Empty;
        public static cardStatus m_cardStatus;

        static StreamWriter file;
        const long LIST_SISE = 1000000;
        public static bool test;
        static public IntState m_state = IntState.Wait;
        static public UInt32 m_layerNumber = 0;
        static public UInt16 m_laserPower = 0;

        static public bool m_layersFinishid = false;
        static private bool m_procesThreadAllowed = false;
        static Thread myThread;
        static private cardSetting m_cardSetting;
        static bool m_isIntiialize = false;
        static ListNumber m_runningLIst = ListNumber.Undefine;
        internal static Mutex m_mut = new Mutex();

        // public static fileLoader fL = new fileLoader();
        static private bool m_dirtyRunSignal = false;
        static private bool m_dirtyResetSignal = false;
        [DllImport("SP-ICE.dll")]
        public static extern UInt16 Init_Scan_Card_Ex(UInt16 N);
        [DllImport("SP-ICE.dll")]
        public static extern UInt16 Get_Version();
        [DllImport("SP-ICE.dll")]
        public static extern UInt16 Get_DLL_Version();
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_Mode(UInt16 usMode);
        //[DllImport("SP-ICE.dll")]
        //[return: MarshalAs(UnmanagedType.LPStr)]
        //public static extern string Corr_File_Name();

        //[DllImport("P-ICE.dll", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl), SuppressUnmanagedCodeSecurity]
        //public static extern bool Load_Corr_N(string lpString, Int16 N);

        [DllImport("SP-ICE.dll", CharSet = CharSet.Ansi)]
        public static extern bool Load_Corr_N([MarshalAs(UnmanagedType.LPStr)] string lpString, Int16 N);


        [DllImport("SP-ICE.dll")]
        public static extern Int16 Remove_Scan_Card_Ex(UInt16 N);
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_Gain(double dGainX, double dGainY, UInt16 ssOffsetX, UInt16 ssOffsetY);
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_Start_List_1();
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_Start_List_2();
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_Delays(UInt16 usStepPeriod, UInt16 usJumpDelay, UInt16 usMarkDelay, UInt16 usPolyDelay,
    UInt16 usLaserOffDelay, UInt16 usLaserOnDelay, UInt16 usT1, UInt16 usT2, UInt16 usT3);
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_Speed(double dJumpSpeed, double dMarkSpeed);
        [DllImport("SP-ICE.dll")]
        public static extern bool Jump_Abs(Int16 ssXVal, Int16 ssYVal);
        [DllImport("SP-ICE.dll")]
        public static extern bool PolA_Abs(Int16 ssXVal, Int16 ssYVal);
        [DllImport("SP-ICE.dll")]
        public static extern bool PolB_Abs(Int16 ssXVal, Int16 ssYVal);
        [DllImport("SP-ICE.dll")]
        public static extern bool PolC_Abs(Int16 ssXVal, Int16 ssYVal);
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_End_Of_List();
        [DllImport("SP-ICE.dll")]
        public static extern bool Execute_List_1();
        [DllImport("SP-ICE.dll")]
        public static extern bool Execute_List_2();
        [DllImport("SP-ICE.dll")]
        public static extern bool Stop_Execution();
        [DllImport("SP-ICE.dll")]
        public static extern UInt16 Read_Status();
        [DllImport("SP-ICE.dll")]
        public static extern IntPtr Get_Error_Message(Int16 ErrorCode);
        [DllImport("SP-ICE.dll")]
        public static extern IntPtr Get_Last_Error_Message();
        [DllImport("SP-ICE.dll")]
        public static extern Int16 Get_Last_Error_Code();
        [DllImport("SP-ICE.dll")]
        public static extern bool Write_Port_List(UInt16 usAddressOffset, UInt16 usValue);
        [DllImport("SP-ICE.dll")]
        public static extern bool Enable_Laser();
        [DllImport("SP-ICE.dll")]
        public static extern bool Disable_Laser();
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_Active_Card(UInt16 iCard);
        [DllImport("SP-ICE.dll")]
        public static extern UInt16 Get_Active_Card();
        [DllImport("SP-ICE.dll")]
        public static extern bool Write_DA(UInt16 usValue);
        [DllImport("SP-ICE.dll")]
        public static extern bool Write_DA_List(UInt16 usValue);
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_Delays_7_8(UInt16 usT1, UInt16 usT2);
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_Delays_9_10(UInt16 usT3, UInt16 usT4);
        [DllImport("SP-ICE.dll")]
        public static extern bool Long_Delay(UInt16 usDelay);
        [DllImport("SP-ICE.dll")]
        public static extern bool Mark_Abs(Int16 ssXVal, Int16 ssYVal);
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_Mark_Parameters_List(UInt16  usStepPeriod, UInt16  usStepSize);
        [DllImport("SP-ICE.dll")]
        public static extern bool Set_Jump_Parameters_List(UInt16 usStepPeriod, UInt16 usJumpSize);

        private static bool initFromForm(cardSetting cs)
        {
            fileLoader.m_mut.WaitOne();
            m_mut.WaitOne();

            m_cardSetting = cs;
            fileLoader.gateMmToField = cs.scale;
            m_laserPower = cs.power;
            UInt16 rInit = Init_Scan_Card_Ex((UInt16)cs.num);
            bool rLoad = Load_Corr_N(cs.corrFilePatch, cs.num);
            bool rSetAct = Set_Active_Card((UInt16)cs.num);
            bool rSetMode = Set_Mode(cs.mode);
            bool rOsc = Write_Port_List(0xC, 0x010);

            fileLoader.m_cs = cs;

            bool openScript = fileLoader.openJobfile(cs.scriptPath);
            PrefetchList.resetList();

            m_isIntiialize = (rInit == 0) && rSetAct && rSetMode && rOsc && openScript;

            MessageBox.Show(string.Format(" {0, -25} -- {1, -10} \n {2,-25} -- {4, -10}   ({3}) \n {5,-25} -- {6, -10} \n {7, -25} -- {8, -10} \n {9,-25} -- {10, -10}  \n {11,-25} -- {12, -10} ({13})",
                 "Init", (rInit == 0).ToString(),
                 "Load correction", cs.corrFilePatch, rLoad.ToString(),
                 "Set mode", rSetMode.ToString(),
                 "Set active card", rSetAct.ToString(),
                 "Oscillator on", rOsc.ToString() ,
                 "Open script", openScript.ToString(), cs.scriptPath),
                 "Initialize is " + (m_isIntiialize ? "Success" : "Fail"), 
                 MessageBoxButtons.OK,
                 MessageBoxIcon.None, 
                 MessageBoxDefaultButton.Button1, 
                 (MessageBoxOptions)0x40000);
//             MessageBox.Show(new Form() { TopMost = true }, "I'm on top!");


            m_layersFinishid = false;
          

            fileLoader.m_mut.ReleaseMutex();
            m_mut.ReleaseMutex();

            return m_isIntiialize;

        }

        public static void ThreadProc()
        {
            var frm = new Form1();

            frm.initCmd += initFromForm;

            frm.ShowDialog();
        }


        public static void initForm()
        {
            //  Form1 form = new Form1();
            //  form.initCmd +=initFromForm;

            Thread ATM2 = new Thread(new ThreadStart(ThreadProc));
            ATM2.SetApartmentState(ApartmentState.STA);
            ATM2.Start();

            //Form1.CallBack += this.OnCallBack;

            //form.Show();
        }

        public static void threadProcessSignals()
        {
            while (m_procesThreadAllowed)
            {
                m_mut.WaitOne();
                processSignals();
                m_mut.ReleaseMutex();
                Thread.Sleep(10);
            }
        }


        public static void processSignals()
        {
            

                readStatus();


            if (m_isIntiialize)
            PrefetchList.stepExecution();

            IntSignals s = m_inputSignals;
            switch (m_state)
            {
                case IntState.Wait:
                    WaitState();
                    if ((s & IntSignals.Run) != 0)
                    {
                        m_inputSignals &= ~IntSignals.Run;
                        m_state = IntState.WaitListReay;
                    }


                    break;

                case IntState.WaitListReay:

                    fillList();

                    if ((s & (IntSignals.Reset)) != 0)
                    {
                        Stop_Execution();
                        fileLoader.resetFile();
                        m_inputSignals &= ~(IntSignals.Reset);

                        m_state = IntState.Wait;


                    }

                    break;


                case IntState.Work:

                    WorkState();

                    if ((s & (IntSignals.Reset)) != 0)
                    {
                        Stop_Execution();
                        fileLoader.resetFile();
                        m_inputSignals &= ~(IntSignals.Reset);

                        m_state = IntState.Wait;


                    }
                    break;

            }
            
            
        }

        private static void fillList()
        {
            //wait list ready
            m_runningLIst = PrefetchList.getNextReadyList();
            if (m_runningLIst == ListNumber.Undefine)
            {
                m_layersFinishid = PrefetchList.m_lastListReay;
                m_state = IntState.Wait;
                return;
            }

            m_layersFinishid = false;
 
            if (m_runningLIst == ListNumber.list1)
                Execute_List_1();
            else
                Execute_List_2();

            m_state = IntState.Work;

            //if (fileLoader.m_isBufferFull) //wait until buffre fill
            //    fillList1();

        }

        private static void WorkState()
        {
            if (m_cardStatus.scanComlete) //wait until escan comlete
            {
                bool finish = PrefetchList.isOneListOnLayer(m_runningLIst);
                PrefetchList.setFree(m_runningLIst);
                m_state = finish ? IntState.Wait : IntState.WaitListReay;
            }


            if (PrefetchList.getNextReadyList() == ListNumber.Undefine)
            {
                m_layersFinishid = PrefetchList.m_lastListReay;
            }

            //if (m_cardStatus.scanComlete) //wait until escan comlete
            //{
            //    m_state = IntState.Wait;
            //}
        }

        private static void StopState()
        {

        }

        private static void WaitState()
        {

        }

        private static void fillList1()
        {
            if (!fileLoader.isAviableNExt())
            {
                m_state = IntState.Wait;
                if (fileLoader.isValidFile == 0)
                    m_layersFinishid = true;
                return;
            }

           

            Stop_Execution();
            printDebug("Stop_Execution");
            Set_Start_List_1();
            printDebug("Set_Start_List_1()");
            //Set_Delays(60, 100, 100, 100, 100, 100, 1000, 500, 0);
            styles st1 = fileLoader.m_cs.style1;
            Set_Delays((UInt16)st1.lStep, (UInt16)st1.lJampDelay, (UInt16)st1.lMarkDelay, (UInt16)st1.lPolygon, (UInt16)st1.lLaserOff, (UInt16)st1.lLaserOn, (UInt16)st1.lQt1, (UInt16)st1.lQt2, 0);
            printDebug("Set_Delays");
            Long_Delay(10); //??
            printDebug("Long_Delay");
            Write_DA_List(m_laserPower);
            printDebug("Write_DA_List " + m_laserPower.ToString());
            Write_Port_List(0xC, 0x010);
            Long_Delay(1000); //?
            long commandCount = 0;
            long iterator = 0;
            bool isEnd = false;
            while (commandCount < LIST_SISE && fileLoader.isAviableNExt() && !isEnd)
            {
                commandCount++;
                iterator = fileLoader.getStartPos();
                switch (fileLoader.m_listJob[iterator].cmd)
                {
                    case Command.EndF:
                        printDebug(iterator, fileLoader.m_listJob[iterator].cmd, fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        break;
                    case Command.StarLayer:
                        printDebug(iterator, fileLoader.m_listJob[iterator].cmd, fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        break;
                    case Command.EndLayer:
                        printDebug(iterator, fileLoader.m_listJob[iterator].cmd, fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        isEnd = true;
                        break;
                    case Command.Jamp:
                        Jump_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        printDebug(iterator, fileLoader.m_listJob[iterator].cmd, fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        break;
                    case Command.Mark:
                        Mark_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        printDebug(iterator, fileLoader.m_listJob[iterator].cmd, fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        break;
                    case Command.PolA_Abs:
                        PolA_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        printDebug(iterator, fileLoader.m_listJob[iterator].cmd, fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        break;
                    case Command.PolB_Abs:
                        PolB_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        printDebug(iterator, fileLoader.m_listJob[iterator].cmd, fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        break;
                    case Command.PolC_Abs:
                        PolC_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        printDebug(iterator, fileLoader.m_listJob[iterator].cmd, fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        break;
                    case Command.Style:
                        styles st = fileLoader.m_listJob[iterator].x == 1 ? fileLoader.m_cs.style1 : fileLoader.m_listJob[iterator].x == 2 ? fileLoader.m_cs.style2 : fileLoader.m_cs.style3;
                         Write_DA_List((UInt16)st.lPower);
                         printDebug(iterator, "Write_DA_List", (Int16)st.lPower, 0);
                         Set_Mark_Parameters_List((UInt16)st.lStep, (UInt16)st.lMarkSize);
                         printDebug(iterator, "Set_Mark_Parameters_List", (Int16)st.lStep, (Int16)st.lMarkSize);
                         Set_Jump_Parameters_List((UInt16)st.lStep, (UInt16)st.lJampSize);
                         printDebug(iterator, "Set_Jump_Parameters_List", (Int16)st.lStep, (Int16)st.lJampSize);
                        // Set_Delays((UInt16)st.lStep, (UInt16)st.lJampDelay, (UInt16)st.lMarkDelay, (UInt16)st.lPolygon, (UInt16)st.lLaserOff, (UInt16)st.lLaserOn, (UInt16)st.lQt1, (UInt16)st.lQt2, 0);
                         printDebug("Set_Delays");
                        break;

                }

                fileLoader.incrementStart();

            }


            Write_Port_List(0xC, 0x000);
            Set_End_Of_List();
            printDebug("et_End_Of_List() ");
            //Write_Port_List(0xC, 0x010); //???
            //printDebug("Write_Port_List(0xC, 0x010) ");
            Long_Delay(10); //??
            printDebug("Long_Delay ");
            Execute_List_1();
            printDebug("Execute_List_1();");

           // file.Close();
            if (isEnd) m_state = IntState.Work;
        }


        public static void readStatus()
        {
            UInt16 status = Read_Status();
            m_cardStatus.l1load = (status & 0x01) != 0;
            m_cardStatus.l2load = (status & 0x02) != 0;
            m_cardStatus.l1redy = (status & 0x04) != 0;
            m_cardStatus.l2redy = (status & 0x08) != 0;
            m_cardStatus.l1busy = (status & 0x010) != 0;
            m_cardStatus.l2busy = (status & 0x020) != 0;
            m_cardStatus.busy = (status & 0x040) != 0;
            m_cardStatus.laserOn = (status & 0x080) != 0;
            m_cardStatus.scanComlete = (status & 0x0100) != 0;
        }

        private static void printDebug(string str)
        {
            file.WriteLine(str);
            file.Flush();
        }

        private static void printDebugWithError(string str)
        {
            if (fileLoader.m_cs.debug)
            {
                file.WriteLine(string.Format("{0, -10}:  {1}", getLastError(), str));
                file.Flush();
            }
        }
        private static void printDebug(long iterator, Command cmd, Int16 x, Int16 y)
        {
            if (fileLoader.m_cs.debug)
            {
                file.WriteLine(string.Format("{0, 10}:  {1, -12}  {2, -10} {3, -10}   => {4}", iterator.ToString(), cmd.ToString(), x.ToString(), y.ToString(), getLastError()));
                file.Flush();
            }
        }

        private static void printDebug(long iterator, String cmd, Int16 x, Int16 y)
        {
            if (fileLoader.m_cs.debug)
            {
                file.WriteLine(string.Format("{0, 10}:  {1, -12}  {2, -10} {3, -10}   => {4}", iterator.ToString(), cmd, x.ToString(), y.ToString(), getLastError()));
                file.Flush();
            }
        }

        public static void deinitialize()
        {
            try
            {
                m_procesThreadAllowed = false;
                myThread.Join();

                fileLoader.stopfillJobList();
                PrefetchList.terminate();
            }
            catch
            {
            }


        }

        public static void getFileBuffPos1(ref long a, ref long b)
        {
            //fL.isInstance = 0;
            //fL.


        }

        public static void Init(UInt16 cardNumber, UInt16 mode, string corrFile, ref string str)
        {
            UInt16 result = 0;
            result = Init_Scan_Card_Ex(cardNumber);
            // string formattedString = string.Format("Init ...{1}, code = {2}", result == 0 ? "Ок" : "Fail", result);
            // MessageBox.Show(formattedString);
            //    MessageBox.Show("Init... code = " +  result.ToString(), "Init");

            UInt16 Ver_RTB = Get_Version();
            UInt16 Ver_DLL = Get_DLL_Version();

            //char buf[100];
            //sprintf(buf, "DLL Ver = %d, RTB Ver = %d", Ver_DLL, Ver_RTB);
            //::MessageBox(0, buf, "Version Info", MB_OK);
            string res = result == 0 ? "Ok" : "Fail";
            str = "Init(" + cardNumber.ToString() + ") - " + res + "; code " + result.ToString() + "; DLL " + Ver_DLL.ToString();
            //  MessageBox.Show("Init... " + res + "; code = " + result.ToString() + "; RTB_ver " + Ver_RTB.ToString() + "; DLL_ver " + Ver_DLL.ToString(), "Init", MessageBoxButtons.OK, MessageBoxIcon.None,
            // MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            bool isOk = Set_Mode(mode);
            str += "\n; Set_Mode " + mode.ToString("X5") + " " + (isOk ? "Ok" : "Fail");

            // isOk = Load_Cor(corrFile);

            str += "\n; Load_Cor " + corrFile + " - " + (isOk ? "Ok" : "Fail");

            isOk = Set_Gain(1, 0, 0, 0);
            str += "\n; Gain -" + (isOk ? "Ok" : "Fail");

        }


        public static Int16 RemoveCard(UInt16 N)
        {
            return Remove_Scan_Card_Ex(N);

        }

        public static bool SetList1()
        {
            return Set_Start_List_1();
        }

        public static string getLastError()
        {
            //if (Get_Last_Error_Code() == 0)
           //     return "Ok";
            IntPtr ptr = Get_Last_Error_Message();
            Int16 code = Get_Last_Error_Code();
            string descr = PtrToStringUtf8(ptr);
            if (descr == "No error")  return "Ok";
            return ("Error (" + code.ToString() + ") =  " + descr);
        }

        private static string PtrToStringUtf8(IntPtr ptr) // aPtr is nul-terminated
        {
            if (ptr == IntPtr.Zero)
                return "";
            int len = 0;
            while (System.Runtime.InteropServices.Marshal.ReadByte(ptr, len) != 0)
                len++;
            if (len == 0)
                return "";
            byte[] array = new byte[len];
            System.Runtime.InteropServices.Marshal.Copy(ptr, array, 0, len);
            return System.Text.Encoding.UTF8.GetString(array);
        }

        //public static string getCorrFileName()
        //{
        //    IntPtr ptr = Corr_File_Name();
        //    return PtrToStringUtf8(ptr);

        //}

        public static void loadJobFile(string path = "")
        {
            if (!System.IO.File.Exists(path))
            {
                //Stream myStream = null;
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "script (*.script)|*.script|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {

                    //  if (( openFileDialog1.FileName) != null)
                    {
                        fileLoader.openJobfile(openFileDialog1.FileName);
                        m_layersFinishid = false;
                    }

                }
            }
        }

        public static void StartLayer(bool val)
        {
            if (m_state == IntState.Wait)
            {


                if (val && !m_dirtyRunSignal)
                {
                    m_inputSignals |= IntSignals.Run;
                }

            }
            m_dirtyRunSignal = val;
        }

        public static void ResetSignal(bool val)
        {
            if (m_state != IntState.Wait)
            {


                if (val && !m_dirtyResetSignal)
                {
                    m_inputSignals |= IntSignals.Reset;
                }

            }
            m_dirtyResetSignal = val;
        }


        public static bool isBusy()
        {
            return (m_state & (IntState.WaitListReay | IntState.Work)) != 0;
        }

        public static bool isWait()
        {
            return (m_state == IntState.Wait);
        }

        public static bool isFinish()
        {
            return m_layersFinishid;
        }

        internal static bool  PCI_Stop_Execution()
        {
            Stop_Execution();
            printDebugWithError("Stop_Execution");
            return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Start_List_1()
        {
            Set_Start_List_1();
            printDebugWithError("Set_Start_List_1");
            return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Start_List_2()
        {
            Set_Start_List_2();
            printDebugWithError("Set_Start_List_2");
            return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Delays(UInt16 step, UInt16 jampDelay, UInt16 markDelay, UInt16 polygon, UInt16 laserOff, UInt16 laserOn, UInt16 qt1, UInt16 qt2, UInt16 fps = 0) 
        {
            Set_Delays(step, jampDelay, markDelay, polygon, laserOff, laserOn, qt1, qt2, fps);
            printDebugWithError(string.Format("{0, -12}:  {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}", "Set_Delays",step, jampDelay, markDelay, polygon, laserOff, laserOn, qt1, qt2, fps ));
            return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Long_Delay(UInt16  val)
    {
        Long_Delay(val);
        printDebugWithError("Long_Delay  " + val.ToString());
        return Get_Last_Error_Code() == 0;
    }
    
        internal static bool PCI_Write_DA_List(UInt16  val)
        {
        Write_DA_List(val);
        printDebugWithError("Write_DA_List  " + val.ToString());
        return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Write_Port_List(UInt16 usAddressOffset, UInt16 usValue)
        {
        Write_Port_List(usAddressOffset, usValue);
        printDebugWithError("Write_Port_List  " + usAddressOffset.ToString() + ", " + usValue.ToString());
        return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_End_Of_List()
        {
            Set_End_Of_List();
            printDebugWithError("Set_End_Of_List");
            return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Jump_Abs(Int16 ssXVal, Int16 ssYVal) 
        {
            Jump_Abs(ssXVal, ssYVal);
            printDebugWithError("Jump_Abs  " + ssXVal.ToString() + ", " + ssYVal.ToString());
            return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Mark_Abs(Int16 ssXVal, Int16 ssYVal) 
        {
            Mark_Abs(ssXVal, ssYVal);
            printDebugWithError("Mark_Abs  " + ssXVal.ToString() + ", " + ssYVal.ToString());
            return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_PolA_Abs(Int16 ssXVal, Int16 ssYVal) 
        {
            PolA_Abs(ssXVal, ssYVal);
            printDebugWithError("PolA_Abs  " + ssXVal.ToString() + ", " + ssYVal.ToString());
            return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_PolB_Abs(Int16 ssXVal, Int16 ssYVal)
        {
            PolB_Abs(ssXVal, ssYVal);
            printDebugWithError("PolB_Abs  " + ssXVal.ToString() + ", " + ssYVal.ToString());
            return Get_Last_Error_Code() == 0;
        }


        internal static bool PCI_PolC_Abs(Int16 ssXVal, Int16 ssYVal)
        {
            PolC_Abs(ssXVal, ssYVal);
            printDebugWithError("PolC_Abs  " + ssXVal.ToString() + ", " + ssYVal.ToString());
            return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Mark_Parameters_List(UInt16 usStepPeriod, UInt16 usStepSize)
        { 
        Set_Mark_Parameters_List(usStepPeriod, usStepSize);
        printDebugWithError("Set_Mark_Parameters_List  " + usStepPeriod.ToString() + ", " + usStepSize.ToString());
        return Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Jump_Parameters_List(UInt16 usStepPeriod, UInt16 usJumpSize)
        {
            Set_Jump_Parameters_List(usStepPeriod, usJumpSize);
            printDebugWithError("Set_Jump_Parameters_List  " + usStepPeriod.ToString() + ", " + usJumpSize.ToString());
            return Get_Last_Error_Code() == 0;
        }

        public static string getStateString()
        {
            return string.Format("Class1 state: {0, -20} list: {1, -10}", m_state.ToString(), m_runningLIst.ToString());
        }
    }
}
