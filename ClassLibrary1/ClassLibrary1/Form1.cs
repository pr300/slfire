using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace ClassLibrary1
{



    public partial class Form1 : Form
    {

        public delegate bool initialise(cardSetting cs);
        public event initialise initCmd;

        public Form1()
        {
            this.Closing += Form1_Closing;

            InitializeComponent();
            tb_corrFile.Text = Properties.Settings.Default.correctionFile;
            tb_script.Text = Properties.Settings.Default.scriptFile;
        }

        private void Form1_Closing(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            solveMode();

            Timer timer = new Timer();
            timer.Interval = ( 300);
            timer.Tick += new EventHandler(updateSignals);
            timer.Start();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
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

        private void cb_mode_b8_CheckedChanged(object sender, EventArgs e)
        {
            solveMode();
        }

        private void cb_mode_b10_CheckedChanged(object sender, EventArgs e)
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bt_initialise_Click(object sender, System.EventArgs e)
        {
            solveMode();
            UInt16 usmode = solveMode();
            cardSetting cs = new cardSetting();
            cs.mode = usmode;
            cs.corrFilePatch = tb_corrFile.Text;
            cs.power = UInt16.Parse(tb_Power.Text);
            cs.t1 = UInt16.Parse(tb_t1.Text);
            cs.t2 = UInt16.Parse(tb_t2.Text);
            cs.t3 = UInt16.Parse(tb_t3.Text);
            cs.scale =  UInt16.Parse(tb_scale.Text);
            cs.num = Int16.Parse(tb_devn.Text);
            cs.scriptPath = tb_script.Text;
            
            bool result = initCmd(cs);
            if (result)
            {
                Properties.Settings.Default.correctionFile = tb_corrFile.Text;
                Properties.Settings.Default.scriptFile = tb_script.Text;
                Properties.Settings.Default.Save();
            }
        }

        private void updateSignals(object sender, EventArgs e)
        {
            tb_bufferCount.Text = fileLoader.endPosition.ToString();
            tb_startPosition.Text = fileLoader.startPosition.ToString();
            tb_state.Text = Class1.m_state.ToString();

            cr_isInstance.Checked = fileLoader.isInstance == 1;
            cr_validFileName.Checked = fileLoader.isValidFile == 1;
            cb_layerFinished.Checked = Class1.m_layersFinishid;
            cb_isBufferFull.Checked = fileLoader.m_isBufferFull;

            cb_l1load.Checked = Class1.m_cardStatus.l1load;
            cb_l1redy.Checked = Class1.m_cardStatus.l1redy;
            cb_l1busy.Checked = Class1.m_cardStatus.l1busy;

            cb_busy.Checked = Class1.m_cardStatus.busy;
            cb_LaserOn.Checked = Class1.m_cardStatus.laserOn;
            cb_scanComplete.Checked = Class1.m_cardStatus.scanComlete;
        }

        private void bt_LoadCorrFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            //openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "gcd (*.gcd)|*.gcd|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {

                //  if (( openFileDialog1.FileName) != null)
                {
                    tb_corrFile.Text = openFileDialog1.FileName;
                    //fileLoader.openJobfile(openFileDialog1.FileName);
                    // m_layersFinishid = false;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //if (!System.IO.File.Exists(path))
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
                        tb_script.Text = openFileDialog1.FileName;
                        //fileLoader.openJobfile(openFileDialog1.FileName);
                        // m_layersFinishid = false;
                    }

                }
            }
        }
    }
}
