using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;     // DLL support
using System.Windows.Forms;
using System.Security;

namespace ClassLibrary1
{
    public class Class1
    {
        static fileLoader fL;

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

        public static void initialize()
        {
            fL = new fileLoader();
            fL.startFillJobList();
        }

        public static void deinitialize()
        {
            try
            {
                fL.stopfillJobList();
            }
            catch
            {
            }

        
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
                            fL.openJobfile(openFileDialog1.FileName);
                        }

                }
            }
        }
    }
}
