using System;
using System.Windows.Forms;
// DLL support
using ClassLibrary1;
using System.Text;
using System.IO;
//using RAYLASE.SPICE1;
using System.Threading;

namespace testWarp
{
    public partial class Form1 : Form
    {
        StreamWriter file;
        bool crThread = true;
        //      Controller Class1;
        Thread myThread;
        private static System.Timers.Timer aTimer;

        public Form1()
        {


            InitializeComponent();
            solveMode();
            this.Closing += Form1_Closing;
            file = new StreamWriter("write.txt", true);
            Class1.initialize();
            crThread = true;
            myThread = new Thread(OnTimedEvent);
            myThread.IsBackground = true;
            myThread.Start();
            addLog("_______START PROGRAMM______ ", true, false);

            //aTimer = new System.Timers.Timer(200);
            //aTimer.Elapsed += OnTimedEvent;
            //aTimer.AutoReset = true;
            //aTimer.Enabled = true;
            //aTimer.Start();
            //GC.KeepAlive(aTimer);
        }

        private void Form1_Closing(object sender, EventArgs e)
        {
            crThread = false;
            //myThread.Join();
            UInt16 ndev = UInt16.Parse(tb_devn.Text);
            file.Close();
            Class1.deinitialize();
            Class1.Remove_Scan_Card_Ex(ndev);


        }

        private void b_init_Click(object sender, EventArgs e)
        {
            string logStr = "";
            UInt16 usmode = solveMode();
            string corrFile = tb_corrFile.Text;
            UInt16 ndev = UInt16.Parse(tb_devn.Text);
            //Class1.Init(ndev, usmode, corrFile, ref logStr);
            //listBox1.Items.Insert(0,">" + logStr);

            bt_InitCard_Click(sender, e);
            bt_LoadCorrFile_Click(sender, e);

            bt_setActive_Click(sender, e);

            btSetMode_Click(sender, e);
            addLog("Init seqence -> ", true, false);

            Class1.m_laserPower = UInt16.Parse(tb_laser_power.Text);
            fileLoader.gateMmToField = Double.Parse(tb_multiplier.Text);
            bt_osclOn_Click(sender, e);
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private UInt16 solveMode()
        {
            UInt16 mode = 0;

            if (cb_mode_b0.Checked) mode += 1;
            if (cb_mode_b2.Checked) mode += 4;
            mode += (UInt16)(cb_mode_b3.Checked ? Math.Pow(2, 3) : 0);
            mode += (UInt16)(cb_mode_b7.Checked ? Math.Pow(2, 7) : 0);
            mode += (UInt16)(cb_mode_b8.Checked ? Math.Pow(2, 8) : 0);
            mode += (UInt16)(cb_mode_b10.Checked ? Math.Pow(2, 10) : 0);
            mode += (UInt16)(cb_mode_b11.Checked ? Math.Pow(2, 11) : 0);
            mode += (UInt16)(cb_mode_b13.Checked ? Math.Pow(2, 13) : 0);

            mode += (UInt16)(cb_mode_co.Checked ? 0 : 0);
            mode += (UInt16)(cb_mode_yag2.Checked ? 32 : 0);
            mode += (UInt16)(cb_mode_yag1.Checked ? 16 : 0);
            mode += (UInt16)(cb_mode_dlaser.Checked ? 48 : 0);
            tb_initMode.Text = mode.ToString("X5");

            return mode;

        }
        private void cb_mode_b0_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_b2_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_b3_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_b7_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_b11_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_b13_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_co_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_yag2_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_yag1_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_dlaser_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_b8_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_b10_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            UInt16 ndev = UInt16.Parse(tb_devn.Text);
            Int16 isOk = Class1.Remove_Scan_Card_Ex(ndev);
            addLog("RemoveCard", true);
        }

        private void bt_list1_fill_Click(object s, EventArgs e)
        {
            bt_list_1_Click(s, e);
            bb_set_delay_Click(s, e);
            bt_set_laser_power_Click(s, e);
            button2_Click(s, e); //jamp
            for (int i = 0; i < 100; i++)
            {
                bt_pol_abc_Click(s, e);
            }
            bt_pol_abc_Click(s, e);
            bt_end_list1_Click(s, e);
            bt_osclOn_Click(s, e);
            bt_exeList1_Click(s, e);
            //bt_laserOff_Click(s, e);
            addLog("Fill list 1 -> ", true, false);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void bt_stop_Click(object sender, EventArgs e)
        {
            bool isOk = Class1.Stop_Execution();
            addLog("Stop_Execution", true);
        }

        private void bt_getState_Click(object sender, EventArgs e)
        {
            UInt16 st = Class1.Read_Status();
            string logStr = " val = 0x" + st.ToString("X5");


            if ((st & (0x1 << 0)) != 0) listBox1.Items.Insert(0, "____Load 1");
            if ((st & (0x1 << 1)) != 0) listBox1.Items.Insert(0, "____Load 2");
            if ((st & (0x1 << 2)) != 0) listBox1.Items.Insert(0, "____Ready 1");
            if ((st & (0x1 << 3)) != 0) listBox1.Items.Insert(0, "____Ready 2");
            if ((st & (0x1 << 4)) != 0) listBox1.Items.Insert(0, "____Busy 1");
            if ((st & (0x1 << 5)) != 0) listBox1.Items.Insert(0, "____Busy 2");
            if ((st & (0x1 << 6)) != 0) listBox1.Items.Insert(0, "____*Busy*");
            if ((st & (0x1 << 7)) != 0) listBox1.Items.Insert(0, "____laser ON");
            if ((st & (0x1 << 8)) != 0) listBox1.Items.Insert(0, "____Scan complite");
            if ((st & (0x1 << 9)) != 0) listBox1.Items.Insert(0, "____Manual op ON");
            if ((st & (0x1 << 10)) != 0) listBox1.Items.Insert(0, "____Control  scanned");
            if ((st & (0x1 << 11)) != 0) listBox1.Items.Insert(0, "____Marking Busy");
            if ((st & (0x1 << 15)) != 0) listBox1.Items.Insert(0, "____Stop MArking");

            addLog("Read_Status -> " + logStr, true);




        }

        private void addLog(string str, bool final = false, bool lastErr = true)
        {
            string s1 = "";
            string s2 = "";


            if (lastErr)
            {
                s1 = str + " > " + Class1.getLastError() + "\n";
                file.WriteLine(s1);
                listBox1.Items.Insert(0, s1);
            }

            if (final)
            {
                s2 = str + "======================================\n";
                file.WriteLine(s2);
                listBox1.Items.Insert(0, s2);

            }

            file.Flush();



        }

        private void bt_LoadCorrFile_Click(object sender, EventArgs e)
        {
            Class1.Load_Corr_N(tb_corrFile.Text, Int16.Parse(tb_devn.Text));
            addLog("Load_Corr_N -> " + tb_corrFile.Text);

            //string s = Class1.Corr_File_Name();
            //addLog("Corr_File_Name -> " + s);

        }

        private void btSetMode_Click(object sender, EventArgs e)
        {
            UInt16 usmode = solveMode();
            Class1.Set_Mode(usmode);
            addLog("Set_Mode -> " + usmode.ToString("X5"));
        }

        private void bt_InitCard_Click(object sender, EventArgs e)
        {
            UInt16 ndev = UInt16.Parse(tb_devn.Text);
            UInt16 res = Class1.Init_Scan_Card_Ex(ndev);
            addLog("Init_Scan_Card_Ex -> #" + ndev.ToString());
        }

        private void bt_list_1_Click(object sender, EventArgs e)
        {
            Class1.Set_Start_List_1();
            addLog("Set_Start_List_1");
        }

        private void bb_set_delay_Click(object sender, EventArgs e)
        {
            Class1.Set_Delays(60, 100,
                               100, 100,
                               100, 100,
                               1000, 800, 0);
            addLog("Set_Delays");
        }

        private void bt_set_laser_power_Click(object sender, EventArgs e)
        {
            UInt16 val = UInt16.Parse(tb_laser_power.Text);

            //Class1.Write_Port_List(0xA, val);
            Class1.Write_DA_List(val);
            //addLog("Write_Port_List, 0xA, val = " + val.ToString());
            addLog("Write_DA, val = " + val.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Int16 X0 = Int16.Parse(tb_x0.Text);
            Int16 Y0 = Int16.Parse(tb_y0.Text);
            Class1.Jump_Abs(X0, Y0);
            addLog("Jump_Abs, X0 = " + X0.ToString() + ", Y0 = " + Y0.ToString());
        }

        private void bt_pol_abc_Click(object sender, EventArgs e)
        {
            Int16 X0 = Int16.Parse(tb_x0.Text);
            Int16 Y0 = Int16.Parse(tb_y0.Text);
            Int16 D = Int16.Parse(tb_step.Text);


            Class1.Jump_Abs(X0, Y0);
            // addLog("Jump_Abs, X0 = " + X0.ToString() + ", Y0 = " + Y0.ToString());


            Int16 X = X0;
            Int16 Y = (Int16)(Y0 + D);
            Class1.PolA_Abs(X0, (Int16)(Y0 + D));
            //addLog("PolA_Abs, X = " + X.ToString() + ", Y = " + Y.ToString());

            X = (Int16)(X0 + D);
            Y = (Int16)(Y0 + D);
            Class1.PolB_Abs((Int16)(X), (Int16)(Y));
            // addLog("PolB_Abs, X = " + X.ToString() + ", Y = " + Y.ToString());

            X = (Int16)(X0 + D);
            Y = (Int16)(Y0);
            Class1.PolB_Abs((Int16)(X), (Int16)(Y));
            //addLog("PolB_Abs, X = " + X.ToString() + ", Y = " + Y.ToString());

            X = (Int16)(X0);
            Y = (Int16)(Y0);
            Class1.PolC_Abs((Int16)(X), (Int16)(Y));
            // addLog("PolC_Abs, X = " + X.ToString() + ", Y = " + Y.ToString());
        }

        private void bt_osclOn_Click(object sender, EventArgs e)
        {
            Class1.Write_Port_List(0xC, 0x010);
            addLog("Write_Port_List, 0xC, 0x10 ");
        }

        private void bt_exeList1_Click(object sender, EventArgs e)
        {
            Class1.Execute_List_1();
            addLog("Execute_List_1");
        }

        private void bt_end_list1_Click(object sender, EventArgs e)
        {
            Class1.Set_End_Of_List();
            addLog("Set_End_Of_List");
        }

        private void bt_laserOff_Click(object sender, EventArgs e)
        {
            Class1.Write_Port_List(0xA, 0x0);
            addLog("Write_Port_List, 0xA, val = 0 ");
        }

        private void bt_laserOn_Click(object sender, EventArgs e)
        {
            Class1.Enable_Laser();
            addLog("Enable_Laser");
        }

        private void bt_LaseOff_Click(object sender, EventArgs e)
        {
            Class1.Disable_Laser();
            addLog("Disable_Laser");
        }

        private void bt_setActive_Click(object sender, EventArgs e)
        {
            Class1.Set_Active_Card(UInt16.Parse(tb_devn.Text));
            addLog("Set_Active_Card");

            UInt16 dev = Class1.Get_Active_Card();
            addLog("Get_Active_Card n = " + dev.ToString());
        }

        private void bwrite_port_list_Click(object sender, EventArgs e)
        {
            UInt16 port = Convert.ToUInt16(tb_write_port.Text, 16);
            UInt16 val = Convert.ToUInt16(tb_write_val.Text, 10);
            Class1.Write_Port_List(port, val);
            addLog("Write_Port_List, port = " + port.ToString("X5") + ", val = " + val.ToString());
        }

        private void bt_del_t1t2_Click(object sender, EventArgs e)
        {
            UInt16 t1 = Convert.ToUInt16(tb_del_t1.Text, 10);
            UInt16 t2 = Convert.ToUInt16(tb_del_t2.Text, 10);
            Class1.Set_Delays_7_8(t1, t2);
            addLog("Set_Delays_7_8, t1 = " + t1.ToString() + ", t2 = " + t2.ToString());
        }

        private void bt_set_delt_3t4_Click(object sender, EventArgs e)
        {
            UInt16 t3 = Convert.ToUInt16(tb_del_t3.Text, 10);
            UInt16 t4 = Convert.ToUInt16(tb_del_t4.Text, 10);
            Class1.Set_Delays_9_10(t3, t4);
            addLog("Set_Delays_9_10, t3 = " + t3.ToString() + ", t4 = " + t4.ToString());
        }

        private void bt_LoadJobFile_Click(object sender, EventArgs e)
        {
            Class1.loadJobFile("sdsd");
        }

        private void OnTimedEvent()
        {

            //:w
            //Class1.fL;
            // Class1.
            //fileLoader.endPosition;
            while (crThread)
            {

                long start = Interlocked.Read(ref fileLoader.startPosition);
                long end = Interlocked.Read(ref fileLoader.endPosition);
                if (IsHandleCreated)
                {
                    Invoke(new Action(() =>
                      {
                          cr_startPos.Text = start.ToString();
                          cr_endPos.Text = end.ToString();
                          cr_isInstance.Checked = Interlocked.Read(ref fileLoader.isInstance) == 1;
                          cr_validFileName.Checked = Interlocked.Read(ref fileLoader.isValidFile) == 1;
                          cb_layerFinished.Checked = Class1.m_layersFinishid;
                          cb_isBufferFull.Checked = fileLoader.m_isBufferFull;

                          //aTimer.AutoReset = true;
                          //aTimer.Enabled = true;
                          //aTimer.Start();
                          cr_state.Text = Class1.m_state.ToString();


                      }));
                }
                Thread.Sleep(10);
            }

        }

        private void bt_sendSignals_Click(object sender, EventArgs e)
        {
            IntSignals s = 0;
            if (cb_run.Checked) s |= IntSignals.Run;
            if (cb_stop.Checked) s |= IntSignals.Stop;
            if (cb_reset.Checked) s |= IntSignals.Reset;
            Class1.m_inputSignals = s;
            Class1.processSignals();
        }

        private void cb_reset_CheckedChanged(object sender, EventArgs e)
        {
            IntSignals s = 0;
            if (cb_run.Checked) s |= IntSignals.Run;
            if (cb_stop.Checked) s |= IntSignals.Stop;
            if (cb_reset.Checked) s |= IntSignals.Reset;
            Class1.m_inputSignals = s;
        }

        private void cb_run_CheckedChanged(object sender, EventArgs e)
        {
            IntSignals s = 0;
            if (cb_run.Checked) s |= IntSignals.Run;
            if (cb_stop.Checked) s |= IntSignals.Stop;
            if (cb_reset.Checked) s |= IntSignals.Reset;
            Class1.m_inputSignals = s;
        }

        private void cb_stop_CheckedChanged(object sender, EventArgs e)
        {
            IntSignals s = 0;
            if (cb_run.Checked) s |= IntSignals.Run;
            if (cb_stop.Checked) s |= IntSignals.Stop;
            if (cb_reset.Checked) s |= IntSignals.Reset;
            Class1.m_inputSignals = s;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            crThread = false;
        }


    }
}
