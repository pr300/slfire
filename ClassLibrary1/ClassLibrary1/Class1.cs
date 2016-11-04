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
    public enum IntState { Wait = 0x01, Stop =0x02, Work = 0x04, FillList = 0x08 };
    public enum IntSignals { Empty = 0x0, Run = 0x01, Stop = 0x02, Reset = 0x4, Pause = 0x8 };

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
            m_procesThreadAllowed = true;
            myThread = new Thread(threadProcessSignals);
            myThread.Start();
        }

        public static IntSignals m_inputSignals = IntSignals.Empty;
        public static cardStatus m_cardStatus;

        static StreamWriter file;
        const long LIST_SISE = 140000;
        public static bool test;
        static public IntState m_state = IntState.Wait;
        static public UInt32 m_layerNumber = 0;
        static public UInt16 m_laserPower = 0;

        static public bool m_layersFinishid = false;
        static private bool m_procesThreadAllowed = false;
        static Thread myThread;
        static private cardSetting m_cardSetting;

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

        private static bool initFromForm(cardSetting cs)
        {
            m_cardSetting = cs;
            fileLoader.gateMmToField = cs.scale;
            m_laserPower = cs.power;
            UInt16 rInit = Init_Scan_Card_Ex((UInt16)cs.num);
            bool rLoad = Load_Corr_N(cs.corrFilePatch, cs.num);
            bool rSetAct = Set_Active_Card((UInt16)cs.num);
            bool rSetMode = Set_Mode(cs.mode);
            bool rOsc = Write_Port_List(0xC, 0x010);



            bool openScript = fileLoader.openJobfile(cs.scriptPath);

            bool initOk = (rInit == 0) && rSetAct && rSetMode && rOsc && openScript;

            MessageBox.Show(string.Format(" {0, -25} -- {1, -10} \n {2,-25} -- {4, -10}   ({3}) \n {5,-25} -- {6, -10} \n {7, -25} -- {8, -10} \n {9,-25} -- {10, -10}  \n {11,-25} -- {12, -10} ({13})",
                 "Init", (rInit == 0).ToString(),
                 "Load correction", cs.corrFilePatch, rLoad.ToString(),
                 "Set mode", rSetMode.ToString(),
                 "Set active card", rSetAct.ToString(),
                 "Oscillator on", rOsc.ToString() ,
                 "Open script", openScript.ToString(), cs.scriptPath),
                 "Initialize is " + (initOk ? "Success" : "Fail"), MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
//             MessageBox.Show(new Form() { TopMost = true }, "I'm on top!");


            m_layersFinishid = false;

            return initOk;

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
                processSignals();
                Thread.Sleep(10);
            }
        }


        public static void processSignals()
        {
            if ((m_state & (IntState.Work | IntState.FillList)) !=0)
            {
                readStatus();
            }

            IntSignals s = m_inputSignals;
            switch (m_state)
            {
                case IntState.Wait:
                    WaitState();
                    if ((s & IntSignals.Run) != 0)
                    {
                        m_inputSignals &= ~IntSignals.Run;
                        m_state = IntState.FillList;
                    }


                    break;

                case IntState.FillList:

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
            if (fileLoader.m_isBufferFull) //wait until buffre fill
                fillList1();

        }

        private static void WorkState()
        {
            if (m_cardStatus.scanComlete) //wait until escan comlete
            {
                m_state = IntState.Wait;
            }
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

            file = new StreamWriter("write_layer.txt", false);

            Stop_Execution();
            printDebug("Stop_Execution");
            Set_Start_List_1();
            printDebug("Set_Start_List_1()");
            Set_Delays(60, 100, 100, 100, 100, 100, 1000, 500, 0);
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

            file.Close();
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
        private static void printDebug(long iterator, Command cmd, Int16 x, Int16 y)
        {
            file.WriteLine(string.Format("{0, 10}:  {1, -12}  {2, -10} {3, -10}   => {4}", iterator.ToString(), cmd.ToString(), x.ToString(), y.ToString(), getLastError()));
            file.Flush();
        }


        public static void deinitialize()
        {
            try
            {
                m_procesThreadAllowed = false;
                myThread.Join();

                fileLoader.stopfillJobList();
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
            if (Get_Last_Error_Code() == 0)
                return "Ok";
            IntPtr ptr = Get_Last_Error_Message();
            Int16 code = Get_Last_Error_Code();
            return ("Fail; code = " + code.ToString() + "; " + PtrToStringUtf8(ptr));
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
            return (m_state & (IntState.FillList | IntState.Work)) != 0;
        }

        public static bool isWait()
        {
            return (m_state == IntState.Wait);
        }

        public static bool isFinish()
        {
            return m_layersFinishid;
        }
    }
}
