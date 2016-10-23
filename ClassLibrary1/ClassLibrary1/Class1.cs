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

namespace ClassLibrary1
{
    public enum IntState { Wait, Stop, Work};
    public enum IntSignals {Empty = 0x0, Run = 0x01, Stop = 0x02, Reset = 0x4, Pause = 0x8 };
    
   
   
    public class Class1
    {

        public static IntSignals m_inputSignals = IntSignals.Empty;
        static StreamWriter file;
        const long LIST_SISE = 100000;
        public static bool test;
        static public IntState m_state = IntState.Wait;
        static public UInt32 m_layerNumber = 0;
        static public UInt16 m_laserPower = 0;

        static public bool m_layersFinishid = false;
        static private bool m_isInstance = false;
        static private bool m_procesThreadAllowed = false;
        static Thread myThread;
            // public static fileLoader fL = new fileLoader();

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
            IntSignals s = m_inputSignals;
            switch (m_state)
            { 
                case IntState.Wait:
                    WaitState();
                    if ((s & (IntSignals.Stop)) != 0 && (s & (IntSignals.Run)) == 0)
                    {
                        m_state = IntState.Stop;
                    }

                    if ((s & (IntSignals.Reset)) != 0)
                    {
                        Stop_Execution();
                        m_layersFinishid = false;

                        fileLoader.resetFile();
                        m_inputSignals &= ~IntSignals.Reset;
                    }
                    

                    break;
                case IntState.Stop:
                    StopState();
                    if ((s & (IntSignals.Stop | IntSignals.Reset)) == 0 && (s & (IntSignals.Run)) != 0)
                    {
                        m_state = IntState.Work;
                        WorkState();
                    }

                    if ((s & (IntSignals.Reset)) != 0)
                    {
                        Stop_Execution();
                        m_layersFinishid = false;

                        fileLoader.resetFile();
                        m_inputSignals &= ~IntSignals.Reset;
                    }
                    break;

                case IntState.Work:

                    WorkState();

                    if ((s & (IntSignals.Reset) )!= 0)
                    {
                        Stop_Execution();
                        fileLoader.resetFile();
                        m_inputSignals &= ~(IntSignals.Reset);

                        m_state = IntState.Wait;
                    }
                    break;
            
            }
        }

        private static void WorkState()
        {
            if(fileLoader.m_isBufferFull)
            fillList();
        }

        private static void StopState()
        { 
        
        }

        private static void WaitState()
        { 
        
        }

        private static void fillList()
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
            printDebug("Write_DA_List "  + m_laserPower.ToString());

            long commandCount = 0;
            long iterator = 0;
            bool isEnd = false;
            while (commandCount < LIST_SISE && fileLoader.isAviableNExt() && !isEnd)
            {
                commandCount++;
                iterator = fileLoader.getStartPos();
                switch (fileLoader.m_listJob[iterator].cmd)
                { 
                    case Command.StarLayer:
                        printDebug(iterator.ToString() + ";  " + "---Start command detected");
                        break;
                    case  Command.EndLayer:
                        isEnd = true;
                        printDebug(iterator.ToString() + ";  " + "---end command detected");
                        break;
                    case Command.Jamp:
                        Jump_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);

                        printDebug(iterator.ToString() + ";  " + "Jamp_abs " + fileLoader.m_listJob[iterator].x.ToString() + ", " + fileLoader.m_listJob[iterator].y.ToString());
                        break;
                    case Command.Mark:
                        Mark_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        printDebug(iterator.ToString() + ";  " + "Mark_abs " + fileLoader.m_listJob[iterator].x.ToString() + ", " + fileLoader.m_listJob[iterator].y.ToString());
                        break;
                    case Command.PolA_Abs:
                        PolA_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        printDebug(iterator.ToString() + ";  " + "PosA_Abs " + fileLoader.m_listJob[iterator].x.ToString() + ", " + fileLoader.m_listJob[iterator].y.ToString());
                        break;
                    case Command.PolB_Abs:
                        PolB_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        printDebug(iterator.ToString() + ";  " + "PosB_Bbs " + fileLoader.m_listJob[iterator].x.ToString() + ", " + fileLoader.m_listJob[iterator].y.ToString());
                        break;
                    case Command.PolC_Abs:
                        PolC_Abs(fileLoader.m_listJob[iterator].x, fileLoader.m_listJob[iterator].y);
                        printDebug(iterator.ToString() + ";  " + "PosC_Abs " + fileLoader.m_listJob[iterator].x.ToString() + ", " + fileLoader.m_listJob[iterator].y.ToString());
                        break;
                
                }
                fileLoader.incrementStart();

            }



            Set_End_Of_List();
            printDebug("et_End_Of_List() " );
            Write_Port_List(0xC, 0x010); //???
            printDebug("Write_Port_List(0xC, 0x010) " );
            Long_Delay(10); //??
            printDebug("Long_Delay ");
            Execute_List_1();
            printDebug("Execute_List_1();");

            file.Close();
            if (isEnd) m_state = IntState.Wait;
        }


        private static  void printDebug(string deb)
        {
            file.WriteLine(deb + "  =  " + getLastError());
            file.Flush();
        }

        public  static void initialize()
        {
            if (!m_isInstance)
            {
                m_isInstance = true;
                //fL = new fileLoader();
                fileLoader.startFillJobList();

                m_procesThreadAllowed = true;
                myThread = new Thread(threadProcessSignals);
                myThread.Start();

            }
            else
            {
                MessageBox.Show("Error: SPI - lib already started. once one instance  allow");
            }
           
        }

        public  static void deinitialize()
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

        public static void Init(UInt16 cardNumber, UInt16 mode, string corrFile, ref string  str) {
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
            string res = result ==0 ? "Ok": "Fail";
            str = "Init(" + cardNumber.ToString() + ") - " + res + "; code " + result.ToString() + "; DLL " + Ver_DLL.ToString();
          //  MessageBox.Show("Init... " + res + "; code = " + result.ToString() + "; RTB_ver " + Ver_RTB.ToString() + "; DLL_ver " + Ver_DLL.ToString(), "Init", MessageBoxButtons.OK, MessageBoxIcon.None,
        // MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
 bool isOk = Set_Mode(mode);
 str += "\n; Set_Mode " + mode.ToString("X5") +" " + (isOk ? "Ok" : "Fail");

// isOk = Load_Cor(corrFile);

 str += "\n; Load_Cor " + corrFile + " - " + (isOk ? "Ok" : "Fail");

            isOk = Set_Gain(1, 0, 0, 0);
            str += "\n; Gain -" + (isOk ? "Ok" : "Fail");

        }


        public static Int16 RemoveCard(UInt16 N)
        {
           return  Remove_Scan_Card_Ex(N);
 
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
            return  ("Fail; code = " + code.ToString()  + "; "+ PtrToStringUtf8(ptr));
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
    }
}
