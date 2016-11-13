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

namespace ClassLibrary1
{
    public enum Command { StarLayer = 0x1, EndLayer = 0x2, PolA_Abs = 0x4, PolB_Abs = 0x8, PolC_Abs = 0x10, Jamp = 0x20, Mark = 0x40, Nop = 0x80, EndF = 0x100, Style = 0x200 };
    public enum StyleState {stUndefine = 0x0,  stStyle1 = 0x1, stStyle2 = 0x2, stStyle3 = 0x3 };

    public struct JobCommand
    {
        public Command cmd;
        public Int16 x;
        public Int16 y;

    };


    public static class fileLoader
    {
        public static long isInstance = 0; //0 - false, 1 true
        public static long isValidFile = 0;
        public static long startPosition = 0;
        public static long endPosition = 0;
        public static long runPermission = 0;
        public static bool m_isBufferFull = false;
        internal static Mutex m_mut = new Mutex();

        const long BUFFER_SIZE = 1001000;
        static public double gateMmToField = 100;
        static public JobCommand[] m_listJob = new JobCommand[BUFFER_SIZE];
        private static StreamReader f;
        static string m_fileName;
        static Int16[] actualArgs = new Int16[4];
        private static StyleState m_style;
        static bool m_resetFile = false;
        static public cardSetting m_cs;
        static StreamWriter file;//= new StreamWriter("F:\\write_code.txt", false);
        // private Object thisLock = new Object();


        //static Thread myThread;
        //static Regex regexOperand = new Regex(@"-?\d+(\.\d+)?");
        static Regex regexOperand = new Regex(@"[-+]?[0-9]*\.?[0-9]+([eE][-+]?[0-9]+)?");

        static fileLoader()
        {
            runPermission = 1;
            isValidFile = 0;
            file = new StreamWriter("write_code.txt", false);
            Interlocked.Exchange(ref isInstance, 1);
            Thread myThread = new Thread(fillJobList);
            myThread.Start();
        }

        public static void stopfillJobList()
        {
            runPermission = 0;
            // myThread.Join();
            f.Close();

        }

        public static bool openJobfile(string path)
        {
            bool result = false;

            try
            {
                if (f != null) f.Close();
                f = new StreamReader(path);
                //GC.SuppressFinalize(f);
                //string str = f.ReadLine();
                startPosition = 0;
                endPosition = 0;
                addCommandAtEnd(Command.Nop, 0, 0, 0, 0);
                Array.Clear(actualArgs, 0, 3);
                Interlocked.Exchange(ref isValidFile, 1);
                m_fileName = path;
                result = true;
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: Could not read file from disk. Error: " + ex.Message + "  " + path);
            }
            return result;

        }



        public static void startFillJobList()
        {
            if (Interlocked.Read(ref isInstance) == 0)
            {
                //MessageBox.Show("startFillJobList", "str", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
                //  runPermission = 1;
                //  file = new StreamWriter("write_code.txt", false);
                //  Interlocked.Exchange(ref isInstance, 1);
                //Thread myThread  = new Thread(fillJobList);
                //  myThread.Start();

                // ThreadStart threadDelegate = new ThreadStart(fillJobList);
                // Thread newThread = new Thread(threadDelegate);
                //  newThread.Start();

            }

        }


        public static void fillJobList()
        {
            // endPosition++;
            // MessageBox.Show("fillJobList p1", "str", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, (MessageBoxOptions)0x40000);
            int multilp = 0;
            while (runPermission == 1)
            {
                m_mut.WaitOne();
                if (m_resetFile)
                {
                    m_resetFile = false;

                    try { f.Close(); }
                    catch { }

                    openJobfile(m_fileName);


                }
                else if ((Interlocked.CompareExchange(ref isValidFile, 1, 1) == 1) && isNextFree())
                {
                    multilp += 1;
                    try
                    {
                        m_isBufferFull = false;
                        // file.WriteLine(m_isBufferFull.ToString());
                        //  file.Flush();

                        string str = f.ReadLine();
                        if (str != null)
                        {
                            string command = "";
                            Regex comand = new Regex(@"^\w+\.?(\w+)?");
                            Match matchC = comand.Match(str);
                            if (matchC.Success) command = matchC.Value;
                            switch (command)
                            {
                                case "Image.Line":
                                    if (m_style != StyleState.stStyle2)
                                    {
                                        correctLastPol(0, 0, true);
                                        addCommandAtEnd(Command.Style, 2, 0, 0, 0, "");
                                        m_style = StyleState.stStyle2;
                                    }
                                    //Match match = regexOperand.Match(str);
                                    MatchCollection matches = regexOperand.Matches(str);

                                    if (matches.Count == 4)
                                    {
                                        //string[] arg = new string[4];
                                        // matches.CopyTo(arg, 0);
                                        for (int i = 0; i < 4; i++)
                                        {
                                            string str1 = matches[i].Value;
                                            float f1 = float.Parse(matches[i].Value, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture) * (float)(gateMmToField);
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
                                    m_style = StyleState.stUndefine;
                                    break;
                                case "F_Out":
                                    correctLastPol(Int16.MaxValue, Int16.MaxValue, true);
                                    addCommandAtEnd(Command.EndLayer, 0, 0, 0, 0);
                                    m_style = StyleState.stUndefine;
                                    break;
                                case "Image.Polyline3D":
                                    if (m_style != StyleState.stStyle1)
                                    {
                                        correctLastPol(0, 0, true);
                                        addCommandAtEnd(Command.Style, 1, 0, 0, 0,"");
                                        m_style = StyleState.stStyle1;
                                    }
                                    MatchCollection pol3D = regexOperand.Matches(str.Substring(16));

                                    correctLastPol(0, 0, true);
                                    int endPol = (int)(pol3D.Count / 3);

                                    for (int i = 0; i < endPol; i++)
                                    {
                                        int itX = i * 3;
                                        int itY = i * 3 + 1;
                                        Int16 x = (Int16)(float.Parse(pol3D[itX].Value, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture) * (float)(gateMmToField));
                                        Int16 y = (Int16)(float.Parse(pol3D[itY].Value, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture) * (float)(gateMmToField));

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
                            Interlocked.Exchange(ref isValidFile, 0);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: Could not read file from disk. Error: " + ex.Message);
                        Interlocked.Exchange(ref isValidFile, 0);
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



        public static bool isAviableNExt()
        {
            return (endPosition - startPosition) > 1;
        }

        public static long getStartPos()
        {
            return startPosition % BUFFER_SIZE;
        }

        public static void incrementStart()
        {

            if (isAviableNExt()) startPosition++;
        }

        private static bool isNextFree()
        {

            return (endPosition - startPosition) < BUFFER_SIZE - 1000;
        }

        private static long freeSpase()
        {

            return BUFFER_SIZE - (endPosition - startPosition);
        }

        private static void addCommandAtEnd(Command cmd, Int16 a1, Int16 a2, Int16 a3, Int16 a4, string debug = "")
        {
            long pos = endPosition % BUFFER_SIZE;
            m_listJob[pos].cmd = cmd;
            m_listJob[pos].x = a1;
            m_listJob[pos].y = a2;
            endPosition++;
            if (m_cs.debug)
            {
                string deb = string.Format("{0, 10}:  {1, -12}  {2, -10} {3, -10}   => {4}", pos.ToString(), cmd.ToString(), a1.ToString(), a2.ToString(), debug);
                file.WriteLine(deb);
                file.Flush();
            }

        }

        static bool isPolA(Int16 x, Int16 y)
        {
            long pos = (endPosition - 1) % BUFFER_SIZE;
            if ((m_listJob[pos].cmd & (Command.StarLayer | Command.EndLayer | Command.Jamp | Command.PolC_Abs | Command.Mark | Command.Nop| Command.Style)) != 0) return true;
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
                    if (m_cs.debug)
                    {
                        file.WriteLine(string.Format("<{0, 9}:  <----correct to Mark", pos.ToString()));
                        file.Flush();
                    }
                }
            }
            else if (m_listJob[pos].cmd == Command.PolB_Abs)
            {
                if (isEnd || m_listJob[pos].x != x || m_listJob[pos].y != y)
                {
                    m_listJob[pos].cmd = Command.PolC_Abs;
                    if (m_cs.debug)
                    {
                        file.WriteLine(string.Format("<{0, 9}:  <----correct to PolC_Abs", pos.ToString()));
                        file.Flush();
                    }
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

        public static string getStateString()
        {
            return string.Format("FLoad : runPermission: [{0}] isValidFile: [{1}] startPosition: {2, -5} endPosition: {3, -5} bufferFull [{4}]",
                runPermission.toX(), isValidFile.toX(), startPosition, endPosition, m_isBufferFull.toX());
        }

    }
}
