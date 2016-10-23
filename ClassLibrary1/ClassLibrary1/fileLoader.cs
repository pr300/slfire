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
    public enum Command { StarLayer = 0x1, EndLayer = 0x2, PolA_Abs = 0x4, PolB_Abs = 0x8, PolC_Abs = 0x10, Jamp = 0x20, Mark = 0x40, Nop = 0x80 };
    public struct JobCommand
    {
        public Command cmd;
        public Int16 x;
        public Int16 y;

    };


    public class fileLoader
    {
        public static long isInstance = 0; //0 - false, 1 true
        public static long isValidFile = 0;
        public static long startPosition = 0;
        public static long endPosition = 0;
        public static long runPermission = 0;
        public static bool m_isBufferFull = false;

        const long BUFFER_SIZE = 100000;
        static public double gateMmToField = 100;
        static public JobCommand[] m_listJob = new JobCommand[BUFFER_SIZE];
        private static StreamReader f;
        static string m_fileName;
        static Int16[] actualArgs = new Int16[4];

        static bool m_resetFile = false;

        static StreamWriter file;
        // private Object thisLock = new Object();


        static Thread myThread;
        static Regex regexOperand = new Regex(@"-?\d+(\.\d+)?");



        public static void stopfillJobList()
        {
            runPermission = 0;
            myThread.Join();
            f.Close();

        }

        public static void openJobfile(string path)
        {
            try
            {
                f = new StreamReader(path);
                //GC.SuppressFinalize(f);
                //string str = f.ReadLine();
                startPosition = 0;
                endPosition = 0;
                addCommandAtEnd(Command.Nop, 0, 0, 0, 0);
                Array.Clear(actualArgs, 0, 3);
                Interlocked.Exchange(ref isValidFile, 1);
                m_fileName = path;
                // MessageBox.Show("Ok");
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error: Could not read file from disk. Error: " + ex.Message);
            }

        }



        public static void startFillJobList()
        {
            if (Interlocked.Read(ref isInstance) == 0)
            {
                runPermission = 1;
                file = new StreamWriter("write_code.txt", false);
                Interlocked.Exchange(ref isInstance, 1);
                myThread = new Thread(fillJobList);
                myThread.Start();

            }

        }


        private static void fillJobList()
        {
            while (runPermission == 1)
            {

                if (m_resetFile)
                {
                    m_resetFile = false;

                    try { f.Close(); }
                    catch { }

                    openJobfile(m_fileName);


                }
                else if ((Interlocked.CompareExchange(ref isValidFile, 1, 1) == 1) && isNextFree())
                {
                    try
                    {
                        m_isBufferFull = false;

                        string str = f.ReadLine();
                        int nop = 0;
                        if (str != null)
                        {
                            string command = "";
                            Regex comand = new Regex(@"^\w+\.?(\w+)?");
                            Match matchC = comand.Match(str);
                            if (matchC.Success) command = matchC.Value;
                            switch (command)
                            {
                                case "Image.Line":
                                    //Match match = regexOperand.Match(str);
                                    MatchCollection matches = regexOperand.Matches(str);

                                    if (matches.Count == 4)
                                    {
                                        //string[] arg = new string[4];
                                        // matches.CopyTo(arg, 0);
                                        for (int i = 0; i < 4; i++)
                                        {
                                            string str1 = matches[i].Value;
                                            actualArgs[i] = (Int16)(double.Parse(matches[i].Value, CultureInfo.InvariantCulture) * gateMmToField);
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
                                    break;
                                case "F_Out":
                                    correctLastPol(Int16.MaxValue, Int16.MaxValue);
                                    addCommandAtEnd(Command.EndLayer, 0, 0, 0, 0);
                                    break;

                            }
                        }
                        else
                        {
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

                Thread.Sleep(0);
            }

        }



        public static bool isAviableNExt()
        {
            return (endPosition - startPosition) > 0;
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

            return (endPosition - startPosition) < BUFFER_SIZE - 1;
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

            string deb = string.Format("{0, 10}:  {1, -12}  {2, -10} {3, -10}   => {4}", pos.ToString(), cmd.ToString(), a1.ToString(), a2.ToString() , debug);
            file.WriteLine(deb);
            file.Flush();

        }

        static bool isPolA(Int16 x, Int16 y)
        {
            long pos = (endPosition - 1) % BUFFER_SIZE;
            if ((m_listJob[pos].cmd & (Command.StarLayer | Command.EndLayer | Command.Jamp | Command.PolC_Abs | Command.Mark | Command.Nop)) != 0) return true;
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
                    file.WriteLine(string.Format("<{0, 9}:  <----correct to Mark", pos.ToString()));
                    file.Flush();
                }
            }
            else if (m_listJob[pos].cmd == Command.PolB_Abs)
            {
                if (isEnd || m_listJob[pos].x != x || m_listJob[pos].y != y)
                {
                    m_listJob[pos].cmd = Command.PolC_Abs;
                    file.WriteLine(string.Format("<{0, 9}:  <----correct to PolC_Abs", pos.ToString()));
                    file.Flush();
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

    }
}
