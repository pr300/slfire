using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SpIceControllerLib
{

    public enum Command { StarLayer = 0x1, EndLayer = 0x2, PolA_Abs = 0x4, PolB_Abs = 0x8, PolC_Abs = 0x10, Jamp = 0x20, Mark = 0x40, Nop = 0x80, EndF = 0x100, Style = 0x200, Power = 0x400, MarkSize = 800 };
    public enum ListNumber { list1 = 0, list2 = 1, Undefine };

    public enum IntState { Wait = 0x01, Stop = 0x02, Work = 0x04, WaitListReady = 0x08 };
    public enum IntSignals { Empty = 0x0, Run = 0x01, Stop = 0x02, Reset = 0x4, Pause = 0x8 };

    public class CardEventArgs : EventArgs { public cardSetting cs; }

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
        public bool ignoreLocalSetting;

        public styles style1;
        public string workSpacePath;
        // public styles style2;
        // public styles style3;
        public bool debug;

        public string toString()
        {
            //    return string.Format(" Card: s1.JS {0} s1.MS {1} s2.JS {2} s2.MS {3} s3.JS {4} s3.MS {5}",
            //style1.lJampSize, style1.lMarkSize, style2.lJampSize, style2.lMarkSize, style3.lJampSize, style3.lMarkSize);
            return string.Format(" Card: JampSize {0} MarkSize {1} ", style1.lJampSize, style1.lMarkSize);
        }
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

        public string toString()
        {
            return string.Format(" Card : L1 load [{0}] ready[{1}] busy[{2}] L2 load[{3}] ready[{4}] busy[{5}] | busy[{6}] laserOn[{7}] complete[{8}]",
                l1load.toX(), l1redy.toX(), l1busy.toX(), l2load.toX(), l2redy.toX(), l2busy.toX(), busy.toX(), laserOn.toX(), scanComlete.toX());
        }

    }


    public struct JobCommand
    {
        public Command cmd;
        public Int16 x;
        public Int16 y;

    };

   public static class helpers
   {

       internal static bool debugInit(ref StreamWriter m_dbgOutFile, string fileName, string worksSpace)
       {
           
           bool dbg = false;

           try
           {
               if (m_dbgOutFile != null) m_dbgOutFile.Close();

               if (!fileLoader.m_cs.debug)
                   return false;

               bool exists = System.IO.Directory.Exists(worksSpace);
               if (!exists)
                   System.IO.Directory.CreateDirectory(worksSpace);

               Directory.SetCurrentDirectory(worksSpace);

               m_dbgOutFile = new StreamWriter(fileName, false);
               if (m_dbgOutFile != null)
                   dbg = true;
           }
           catch
           {
               //TODO:
           }

           return dbg;
       }

       internal static Int64 speedToJampPeriod(Int64 step, float speed, float K)
       {
           float st = (float)step;
           float sp = speed;
           Int64 res = (Int64)(sp * st * K / 1000 / 1000);
           return res < 1 ? 1 : res;
       }

   }

    public static class stringExtension
    {
        public static string toX(this bool val)
        {
            return val ? "X" : " ";
        }

        public static string toX(this long val)
        {
            return val != 0 ? "X" : " ";
        }
        public static string toX(this Int32 val)
        {
            return val != 0 ? "X" : " ";
        }
    }

    
}
