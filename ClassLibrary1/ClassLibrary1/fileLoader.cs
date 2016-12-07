using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;

namespace SpIceControllerLib
{

    public enum SettingStace { globalSpace, listSpace };


    public static class fileLoader
    {
        /// <interface>

        public static volatile Int32 isValidFile = 0;
        public static bool m_isBufferFull = false;
        public static JobCommand[] m_listJob = new JobCommand[BUFFER_SIZE];
        internal static Mutex m_mut = new Mutex();
        internal static styles m_globalStyle;
        /// internal static bool openJobfile(string path)
        /// internal static void incrementStart()
        /// internal static bool isAviableNExt()
        /// internal  static long getStartPos()
        ///  public static string getStateString()
        /// </summary>


        static Int64 m_startPosition = 0;
        static Int64 startPosition
        {
            get { return Interlocked.Read(ref m_startPosition); }        // for 32bits systems
            set { Interlocked.Exchange(ref m_startPosition, value); }
        }

        static Int64 m_endPosition = 0;
        static Int64 endPosition
        {
            get { return Interlocked.Read(ref m_endPosition); }
            set { Interlocked.Exchange(ref m_endPosition, value); }
        }


        static volatile Int32 runPermission = 0;
        
        static SettingStace m_settingStace = SettingStace.globalSpace;
        volatile static internal bool m_isPreambuleFinish = false;

        const long BUFFER_SIZE = 1048576;//1001000;

        private static StreamReader m_scriptFile;
        static string m_fileName;
        static Int16[] actualArgs = new Int16[4];

        static bool m_resetFile = false;
        static public cardSetting m_cs;
        static StreamWriter m_dbgOutFile;
        
        static bool dbg = false;
        static Thread m_loaThread;

        static Regex regexOperand = new Regex(@"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?");
        static Regex comand = new Regex(@"^\w+\.?(\w+)?");

        static fileLoader()
        {
            runPermission = 1;
            isValidFile = 0;
            m_settingStace = SettingStace.globalSpace;
            initGloballStettingStyle();
            m_loaThread = new Thread(fillJobList);
            m_loaThread.Start();
        }

        public static void stopfillJobList()
        {
            runPermission = 0;
            // myThread.Join();
            m_scriptFile.Close();

        }

        internal static bool openJobfile(string path)
        {
            bool result = false;

            try
            {
                if (m_scriptFile != null) m_scriptFile.Close();
                m_scriptFile = new StreamReader(path);

                m_settingStace = SettingStace.globalSpace;
                initGloballStettingStyle();
                m_globalStyle = m_cs.style1;
                m_isPreambuleFinish = false;
                startPosition = 0;
                endPosition = 0;
                addCommandAtEnd(Command.Nop, 0, 0, 0, 0);
                Array.Clear(actualArgs, 0, 3);
                isValidFile = 1;
                m_fileName = path;
                result = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: Could not read file from disk. Error: " + ex.Message + "  " + path);
            }
            return result;

        }


        static void fillJobList()
        {
            int multilp = 0;
            while (runPermission == 1)
            {
                m_mut.WaitOne();
                if (m_resetFile)
                {
                    m_resetFile = false;

                    try { m_scriptFile.Close(); }
                    catch { }

                    openJobfile(m_fileName);

                }
                else if (isValidFile == 1 && isNextFree())
                {
                    multilp += 1;
                    try
                    {
                        m_isBufferFull = false;

                        string str = m_scriptFile.ReadLine();
                        if (str != null)
                        {
                            string command = "";

                            Match matchC = comand.Match(str);
                            if (matchC.Success) command = matchC.Value;
                            switch (command)
                            {
                                case "Image.Line":
                                    MatchCollection matches = regexOperand.Matches(str);

                                    if (matches.Count == 4)
                                    {
                                        //string[] arg = new string[4];
                                        // matches.CopyTo(arg, 0);
                                        for (int i = 0; i < 4; i++)
                                        {
                                            string str1 = matches[i].Value;
                                            float f1 = float.Parse(matches[i].Value, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture) * m_cs.scale;
                                            actualArgs[i] = (Int16)(f1);
                                        }
                                        Int16 x0 = actualArgs[0];
                                        Int16 y0 = actualArgs[1];
                                        Int16 x1 = actualArgs[2];
                                        Int16 y1 = actualArgs[3];
                                        correctLastPol(x0, y0);
                                        if (isPolA(x0, y0))
                                        {
                                            addCommandAtEnd(Command.Jamp, x0, y0, 0, 0);
                                            addCommandAtEnd(Command.PolA_Abs, x1, y1, 0, 0, str);
                                        }
                                        else if (isPolB(x0, y0))
                                        {
                                            addCommandAtEnd(Command.PolB_Abs, x1, y1, 0, 0, str);
                                        }

                                        else
                                        {
                                            isValidFile = 0;
                                            MessageBox.Show("Can't determinate command type, str = " + str);
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid command:  str = " + str + ", requre 4 arg for Image.Line");
                                        isValidFile = 0;
                                    }
                                    break;
                                case "F_In":
                                    addCommandAtEnd(Command.StarLayer, 0, 0, 0, 0);
                                    m_settingStace = SettingStace.listSpace;
                                    m_isPreambuleFinish = true;
                                    break;
                                case "F_Out":
                                    correctLastPol(Int16.MaxValue, Int16.MaxValue, true);
                                    addCommandAtEnd(Command.EndLayer, 0, 0, 0, 0);
                                    m_settingStace = SettingStace.globalSpace;
                                    break;
                                case "Laser.Power":
                                    if (m_cs.ignoreLocalSetting) break;
                                    correctLastPol(Int16.MaxValue, Int16.MaxValue, true);
                                    UInt32 power = 0;
                                    MatchCollection matchesPower = regexOperand.Matches(str);
                                    if (matchesPower.Count == 1)
                                    {
                                        string strPower = matchesPower[0].Value;
                                        power = UInt32.Parse(strPower);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid command:  str = " + str + ", requre 1 arg for Laser.Power");
                                        isValidFile = 0;
                                    }

                                    //power = (power >> 24);
                                    if (m_settingStace == SettingStace.globalSpace)
                                        m_globalStyle.lPower = power;
                                    else
                                        addCommandAtEnd(Command.Power, (Int16)power, 0, 0, 0);
                                    break;
                                case "Laser.MarkSpeed":
                                    if (m_cs.ignoreLocalSetting) break;
                                    correctLastPol(Int16.MaxValue, Int16.MaxValue, true);

                                    UInt32 markSpeed = 0;
                                    MatchCollection speed = regexOperand.Matches(str);
                                    if (speed.Count == 1)
                                    {
                                        string strPower = speed[0].Value;
                                        markSpeed = UInt32.Parse(strPower);
                                    }
                                    else
                                    {
                                        MessageBox.Show("Invalid command:  str = " + str + ", requre 1 arg for Laser.MarkSpeed");
                                        isValidFile = 0;
                                    }

                                    UInt32 markSize = (UInt32)helpers.speedToJampPeriod((Int64)m_cs.style1.lStep, (float)markSpeed, m_cs.scale);
                                    if (m_settingStace == SettingStace.globalSpace)
                                        m_globalStyle.lMarkSize = markSize;
                                    else
                                        addCommandAtEnd(Command.MarkSize, (Int16)m_cs.style1.lStep, (Int16)markSize, 0, 0);

                                    break;
                                case "Image.Polyline3D":

                                    MatchCollection pol3D = regexOperand.Matches(str.Substring(16));

                                    correctLastPol(0, 0, true);
                                    int endPol = (int)(pol3D.Count / 3);

                                    for (int i = 0; i < endPol; i++)
                                    {
                                        int itX = i * 3;
                                        int itY = i * 3 + 1;
                                        Int16 x = (Int16)(float.Parse(pol3D[itX].Value, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture) * m_cs.scale);
                                        Int16 y = (Int16)(float.Parse(pol3D[itY].Value, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture) * m_cs.scale);

                                        if (i == 0)
                                        {
                                            addCommandAtEnd(Command.Jamp, x, y, 0, 0, string.Format("Image.Polyline3D #{0, 5}  ( {1, 5}, {2, 5} )", i, pol3D[itX].Value, pol3D[itY].Value));
                                        }
                                        else if (i == 1)
                                        {
                                            addCommandAtEnd(Command.PolA_Abs, x, y, 0, 0, string.Format("Image.Polyline3D #{0, 5}  ( {1, 5}, {2, 5} )", i, pol3D[itX].Value, pol3D[itY].Value));
                                        }
                                        else if (i == endPol - 1)
                                        {
                                            addCommandAtEnd(Command.PolC_Abs, x, y, 0, 0, string.Format("Image.Polyline3D #{0, 5}  ( {1, 5}, {2, 5} )", i, pol3D[itX].Value, pol3D[itY].Value));
                                        }
                                        else
                                        {
                                            addCommandAtEnd(Command.PolB_Abs, x, y, 0, 0, string.Format("Image.Polyline3D #{0, 5}  ( {1, 5}, {2, 5} )", i, pol3D[itX].Value, pol3D[itY].Value));
                                        }
                                    }

                                    break;


                            }
                        }
                        else
                        {
                            correctLastPol(0, 0, true);
                            addCommandAtEnd(Command.Nop, 0, 0, 0, 0);
                            addCommandAtEnd(Command.EndF, 0, 0, 0, 0);
                            isValidFile = 0;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Error: " + ex.Message);
                        isValidFile = 0;
                    }

                }
                else
                {
                    m_isBufferFull = true;
                }

                m_mut.ReleaseMutex();
                if (m_isBufferFull)
                    Thread.Sleep(10);
                else if (multilp > 100)
                {
                    Thread.Sleep(1);
                    multilp = 0;
                }
                else
                    Thread.Sleep(0);
            }

        }



        internal static bool isAviableNExt()
        {
            return (endPosition - startPosition) > 1;
        }

        internal static long getStartPos()
        {
            return startPosition % BUFFER_SIZE;
        }

        internal static void incrementStart()
        {

            if (isAviableNExt()) startPosition++;  //change
        }

        static bool isNextFree()
        {

            return (endPosition - startPosition) < BUFFER_SIZE - 1000;
        }


        private static void addCommandAtEnd(Command cmd, Int16 a1, Int16 a2, Int16 a3, Int16 a4, string debug = "")
        {
            long pos = endPosition % BUFFER_SIZE;
            m_listJob[pos].cmd = cmd;
            m_listJob[pos].x = a1;
            m_listJob[pos].y = a2;
            endPosition++;
            if (m_cs.debug)
                printDebug(string.Format("{0, 10}:  {1, -12}  {2, -10} {3, -10}   => {4}", pos.ToString(), cmd.ToString(), a1.ToString(), a2.ToString(), debug));

        }

        static bool isPolA(Int16 x, Int16 y)
        {
            long pos = (endPosition - 1) % BUFFER_SIZE;
            if ((m_listJob[pos].cmd & (Command.StarLayer | Command.EndLayer | Command.Jamp | Command.PolC_Abs | Command.Mark | Command.Nop | Command.Style | Command.Power | Command.MarkSize)) != 0) return true;
            return false;
        }

        static bool isPolB(Int16 x, Int16 y)
        {
            if (endPosition == 0) return false;
            long pos = (endPosition - 1) % BUFFER_SIZE;
            if ((m_listJob[pos].cmd & (Command.PolA_Abs | Command.PolB_Abs)) != 0)
            {
                if (m_listJob[pos].x == x && m_listJob[pos].y == y)
                    return true;
            }
            return false;
        }

        static void correctLastPol(Int16 x, Int16 y, bool isEnd = false)
        {
            long pos = (endPosition - 1) % BUFFER_SIZE;
            if (m_listJob[pos].cmd == Command.PolA_Abs)
            {
                if (isEnd || m_listJob[pos].x != x || m_listJob[pos].y != y)
                {
                    m_listJob[pos].cmd = Command.Mark;
                    if (dbg)
                        printDebug(string.Format("<{0, 9}:  <----correct to Mark", pos.ToString()));

                }
            }
            else if (m_listJob[pos].cmd == Command.PolB_Abs)
            {
                if (isEnd || m_listJob[pos].x != x || m_listJob[pos].y != y)
                {
                    m_listJob[pos].cmd = Command.PolC_Abs;

                    if (dbg)
                        printDebug(string.Format("<{0, 9}:  <----correct to PolC_Abs", pos.ToString()));

                }
            }
        }



        static bool isContinued(Int16 x, Int16 y)
        {
            long pos = (endPosition - 1) % BUFFER_SIZE;
            if (m_listJob[pos].cmd != Command.PolB_Abs) return false;
            if (m_listJob[pos].x != x || m_listJob[pos].y != y) return false;

            return true;
        }

        static public void resetFile()
        {
            m_resetFile = true;
        }

        static private void initGloballStettingStyle()
        {
            m_globalStyle.lPower = 25;
            m_globalStyle.lMarkSize = 50;
        }

        public static void debugInit()
        {
            dbg = helpers.debugInit(ref m_dbgOutFile, "scriptParsing.txt", fileLoader.m_cs.workSpacePath);
        }

        private static void printDebug(string str)
        {
            if (dbg)
            {
                m_dbgOutFile.WriteLine(string.Format("{0}", str));
                m_dbgOutFile.Flush();
            }
        }

        public static string getStateString()
        {
            return string.Format("Load :  isValidFile: [{0}] Processed: {1, -5} Parsed: {2, -5} isFull [{3}]",
                 isValidFile.toX(), startPosition, endPosition, m_isBufferFull.toX());
        }

        public static string getStateStringDebug()
        {
            return string.Format(", Thread: {0, -15}, gPower {1, 3}, gMark {2, 4}",
                m_loaThread.ThreadState,   m_globalStyle.lPower, m_globalStyle.lMarkSize);
        }
    }
}
