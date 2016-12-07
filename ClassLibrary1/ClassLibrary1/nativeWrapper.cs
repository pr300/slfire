using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.IO;

namespace SpIceControllerLib
{
    internal static class NativeMethods
    {
        static bool dbg = false;
        static StreamWriter m_dbgOutFile;
  

        [DllImport("SP-ICE.dll")]
        private static extern UInt16 Init_Scan_Card_Ex(UInt16 N);
        [DllImport("SP-ICE.dll")]
        private static extern UInt16 Get_Version();
        [DllImport("SP-ICE.dll")]
        private static extern UInt16 Get_DLL_Version();
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_Mode(UInt16 usMode);
        [DllImport("SP-ICE.dll")] //, CharSet = CharSet.Ansi
        private static extern bool Load_Corr_N([MarshalAs(UnmanagedType.LPStr)] string lpString, Int16 N);
        [DllImport("SP-ICE.dll")]
        private static extern Int16 Remove_Scan_Card_Ex(UInt16 N);
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_Gain(double dGainX, double dGainY, UInt16 ssOffsetX, UInt16 ssOffsetY);
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_Start_List_1();
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_Start_List_2();
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_Delays(UInt16 usStepPeriod, UInt16 usJumpDelay, UInt16 usMarkDelay, UInt16 usPolyDelay,
    UInt16 usLaserOffDelay, UInt16 usLaserOnDelay, UInt16 usT1, UInt16 usT2, UInt16 usT3);
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_Speed(double dJumpSpeed, double dMarkSpeed);
        [DllImport("SP-ICE.dll")]
        private static extern bool Jump_Abs(Int16 ssXVal, Int16 ssYVal);
        [DllImport("SP-ICE.dll")]
        private static extern bool PolA_Abs(Int16 ssXVal, Int16 ssYVal);
        [DllImport("SP-ICE.dll")]
        private static extern bool PolB_Abs(Int16 ssXVal, Int16 ssYVal);
        [DllImport("SP-ICE.dll")]
        private static extern bool PolC_Abs(Int16 ssXVal, Int16 ssYVal);
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_End_Of_List();
        [DllImport("SP-ICE.dll")]
        private static extern bool Execute_List_1();
        [DllImport("SP-ICE.dll")]
        private static extern bool Execute_List_2();
        [DllImport("SP-ICE.dll")]
        private static extern bool Stop_Execution();
        [DllImport("SP-ICE.dll")]
        private static extern UInt16 Read_Status();
        [DllImport("SP-ICE.dll")]
        private static extern IntPtr Get_Error_Message(Int16 ErrorCode);
        [DllImport("SP-ICE.dll")]
        private static extern IntPtr Get_Last_Error_Message();
        [DllImport("SP-ICE.dll")]
        private static extern Int16 Get_Last_Error_Code();
        [DllImport("SP-ICE.dll")]
        private static extern bool Write_Port(UInt16 usAddressOffset, UInt16 usValue);
        [DllImport("SP-ICE.dll")]
        private static extern bool Write_Port_List(UInt16 usAddressOffset, UInt16 usValue);
        [DllImport("SP-ICE.dll")]
        private static extern bool Enable_Laser();
        [DllImport("SP-ICE.dll")]
        private static extern bool Disable_Laser();
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_Active_Card(UInt16 iCard);
        [DllImport("SP-ICE.dll")]
        private static extern UInt16 Get_Active_Card();
        [DllImport("SP-ICE.dll")]
        private static extern bool Write_DA(UInt16 usValue);
        [DllImport("SP-ICE.dll")]
        private static extern bool Write_DA_List(UInt16 usValue);
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_Delays_7_8(UInt16 usT1, UInt16 usT2);
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_Delays_9_10(UInt16 usT3, UInt16 usT4);
        [DllImport("SP-ICE.dll")]
        private static extern bool Long_Delay(UInt16 usDelay);
        [DllImport("SP-ICE.dll")]
        private static extern bool Mark_Abs(Int16 ssXVal, Int16 ssYVal);
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_Mark_Parameters_List(UInt16 usStepPeriod, UInt16 usStepSize);
        [DllImport("SP-ICE.dll")]
        private static extern bool Set_Jump_Parameters_List(UInt16 usStepPeriod, UInt16 usJumpSize);

        internal static bool PCI_Execute_List_2()
        {
            NativeMethods.Execute_List_2();
            if (dbg)
                printDebugWithError("Execute_List_2");
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Execute_List_1()
        {
            NativeMethods.Execute_List_1();
            if (dbg)
                printDebugWithError("Execute_List_1" );
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Active_Card(UInt16 iCard)
        {
            NativeMethods.Set_Active_Card(iCard);
            if (dbg)
                printDebugWithError("Set_Active_Card  " + iCard.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Remove_Scan_Card_Ex(UInt16 N)
        {
            NativeMethods.Remove_Scan_Card_Ex( N);
            if (dbg)
                printDebugWithError("Remove_Scan_Card_Ex  " + N.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Load_Corr_N( string lpString, Int16 N)
        {
            NativeMethods.Load_Corr_N(lpString, N);
            if (dbg)
                printDebugWithError("Load_Corr_N  " + lpString.ToString() + "  " + N.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Mode(UInt16 usMode)
        {
            NativeMethods.Set_Mode(usMode);
            if (dbg)
                printDebugWithError("Set_Mode  " + usMode.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Init_Scan_Card_Ex(UInt16 N)
        {
            NativeMethods.Init_Scan_Card_Ex(N);
            if(dbg)
                printDebugWithError("Init_Scan_Card_Ex  " + N.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Stop_Execution()
        {
            NativeMethods.Stop_Execution();
            if (dbg)
                printDebugWithError("Stop_Execution");
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Start_List_1()
        {
            NativeMethods.Set_Start_List_1();
            if (dbg)
                printDebugWithError("Set_Start_List_1");
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Start_List_2()
        {
            NativeMethods.Set_Start_List_2();
            if (dbg)
                printDebugWithError("Set_Start_List_2");
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Delays(UInt16 step, UInt16 jampDelay, UInt16 markDelay, UInt16 polygon, UInt16 laserOff, UInt16 laserOn, UInt16 qt1, UInt16 qt2, UInt16 fps = 0)
        {
            NativeMethods.Set_Delays(step, jampDelay, markDelay, polygon, laserOff, laserOn, qt1, qt2, fps);
            if (dbg)
                printDebugWithError(string.Format("{0, -12}:  {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}", "Set_Delays", step, jampDelay, markDelay, polygon, laserOff, laserOn, qt1, qt2, fps));
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Long_Delay(UInt16 val)
        {
            NativeMethods.Long_Delay(val);
            if (dbg)
                printDebugWithError("Long_Delay  " + val.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Write_DA_List(UInt16 val)
        {
            val = val > (UInt16)255 ? (UInt16)255 : val;
            NativeMethods.Write_DA_List(val);
            if (dbg)
                printDebugWithError("Write_DA_List  " + val.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Write_Port_List(UInt16 usAddressOffset, UInt16 usValue)
        {
            NativeMethods.Write_Port_List(usAddressOffset, usValue);
            if (dbg)
                printDebugWithError("Write_Port_List  " + usAddressOffset.ToString() + ", " + usValue.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Write_Port(UInt16 usAddressOffset, UInt16 usValue)
        {
            NativeMethods.Write_Port(usAddressOffset, usValue);
            if (dbg)
                printDebugWithError("Write_Port  " + usAddressOffset.ToString() + ", " + usValue.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_End_Of_List()
        {
            NativeMethods.Set_End_Of_List();
            if (dbg)
                printDebugWithError("Set_End_Of_List");
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Jump_Abs(Int16 ssXVal, Int16 ssYVal)
        {
            NativeMethods.Jump_Abs(ssXVal, ssYVal);
            if (dbg)
                printDebugWithError("Jump_Abs  " + ssXVal.ToString() + ", " + ssYVal.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Mark_Abs(Int16 ssXVal, Int16 ssYVal)
        {
            NativeMethods.Mark_Abs(ssXVal, ssYVal);
            if (dbg)
                printDebugWithError("Mark_Abs  " + ssXVal.ToString() + ", " + ssYVal.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_PolA_Abs(Int16 ssXVal, Int16 ssYVal)
        {
            NativeMethods.PolA_Abs(ssXVal, ssYVal);
            if (dbg)
                printDebugWithError("PolA_Abs  " + ssXVal.ToString() + ", " + ssYVal.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_PolB_Abs(Int16 ssXVal, Int16 ssYVal)
        {
            NativeMethods.PolB_Abs(ssXVal, ssYVal);
            if (dbg)
                printDebugWithError("PolB_Abs  " + ssXVal.ToString() + ", " + ssYVal.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }


        internal static bool PCI_PolC_Abs(Int16 ssXVal, Int16 ssYVal)
        {
            NativeMethods.PolC_Abs(ssXVal, ssYVal);
            if (dbg)
                printDebugWithError("PolC_Abs  " + ssXVal.ToString() + ", " + ssYVal.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Mark_Parameters_List(UInt16 usStepPeriod, UInt16 usStepSize)
        {
            NativeMethods.Set_Mark_Parameters_List(usStepPeriod, usStepSize);
            if (dbg)
                printDebugWithError("Set_Mark_Parameters_List  " + usStepPeriod.ToString() + ", " + usStepSize.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        internal static bool PCI_Set_Jump_Parameters_List(UInt16 usStepPeriod, UInt16 usJumpSize)
        {
            NativeMethods.Set_Jump_Parameters_List(usStepPeriod, usJumpSize);
            if (dbg)
                printDebugWithError("Set_Jump_Parameters_List  " + usStepPeriod.ToString() + ", " + usJumpSize.ToString());
            return NativeMethods.Get_Last_Error_Code() == 0;
        }

        public static string getLastError()
        {
            IntPtr ptr = NativeMethods.Get_Last_Error_Message();
            Int16 code = NativeMethods.Get_Last_Error_Code();
            string descr = PtrToStringUtf8(ptr);
            if (descr == "No error") return "Ok";
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

        public static void readStatus(ref cardStatus m_cardStatus)
        {
            UInt16 status = NativeMethods.Read_Status();
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

        private static void printDebugWithError(string str)
        {
            if (dbg)
            {
                m_dbgOutFile.WriteLine(string.Format("{0, -10}:  {1}", getLastError(), str));
                m_dbgOutFile.Flush();
            }
        }

        public static void debugInit()
        {
            dbg = helpers.debugInit(ref m_dbgOutFile, "nativeCall.txt", fileLoader.m_cs.workSpacePath);
        }

    }


}
