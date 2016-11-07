using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Globalization;
namespace ClassLibrary1
{

    public enum prm
    {
        lStep = 0,
        lLaserOn = 1,
        lLaserOff = 2,
        lPolygon = 3,
        lMarkDelay = 4,
        lJampDelay = 5,
        lFps = 6,
        lQt1 = 7,
        lQt2 = 8,
        lJampSize =9,
        lMarkSize = 10,
        lPower = 11
    };


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

            readCorrectionTextFile(tb_corrFile.Text);
            dg.Columns.Add("name", "Name");
            dg.Columns.Add("style1", "Style 1");
            dg.Columns.Add("style2", "Style 2");
            dg.Columns.Add("style3", "Style 3");
            dg.Rows.Add("Step");
            dg.Rows.Add("LaserOn Delay");
            dg.Rows.Add("LaserOff Delay");
            dg.Rows.Add("Polygon Delay");
            dg.Rows.Add("Mark Dela");
            dg.Rows.Add("Jump Delay");
            dg.Rows.Add("FPS");
            dg.Rows.Add("Q-Switch t1");
            dg.Rows.Add("Q-Switch t2");
            dg.Rows.Add("Jump size in period");
            dg.Rows.Add("Mark size in period");
            dg.Rows.Add("Power");

            dg.AllowUserToAddRows = false;


            dg.Rows[(int)prm.lStep].Cells[1].Value = Properties.Settings.Default.s1Step;
            dg.Rows[(int)prm.lLaserOn].Cells[1].Value = Properties.Settings.Default.s1LaserOn;
            dg.Rows[(int)prm.lLaserOff].Cells[1].Value = Properties.Settings.Default.s1LaserOff;
            dg.Rows[(int)prm.lPolygon].Cells[1].Value = Properties.Settings.Default.s1PolygonDelay;
            dg.Rows[(int)prm.lMarkDelay].Cells[1].Value = Properties.Settings.Default.s1MarkDelay;
            dg.Rows[(int)prm.lJampDelay].Cells[1].Value = Properties.Settings.Default.s1JampDelay;
            dg.Rows[(int)prm.lFps].Cells[1].Value = Properties.Settings.Default.s1Fps;
            dg.Rows[(int)prm.lQt1].Cells[1].Value = Properties.Settings.Default.s1Q1;
            dg.Rows[(int)prm.lQt2].Cells[1].Value = Properties.Settings.Default.s1Q2;
            dg.Rows[(int)prm.lJampSize].Cells[1].Value = Properties.Settings.Default.s1JampSize;
            dg.Rows[(int)prm.lMarkSize].Cells[1].Value = Properties.Settings.Default.s1MarkSize;
            dg.Rows[(int)prm.lPower].Cells[1].Value = Properties.Settings.Default.s1Power;

            dg.Rows[(int)prm.lStep].Cells[2].Value = Properties.Settings.Default.s2Step;
            dg.Rows[(int)prm.lLaserOn].Cells[2].Value = Properties.Settings.Default.s2LaserOn;
            dg.Rows[(int)prm.lLaserOff].Cells[2].Value = Properties.Settings.Default.s2LaserOff;
            dg.Rows[(int)prm.lPolygon].Cells[2].Value = Properties.Settings.Default.s2PolygonDelay;
            dg.Rows[(int)prm.lMarkDelay].Cells[2].Value = Properties.Settings.Default.s2MarkDelay;
            dg.Rows[(int)prm.lJampDelay].Cells[2].Value = Properties.Settings.Default.s2JampDelay;
            dg.Rows[(int)prm.lFps].Cells[2].Value = Properties.Settings.Default.s2Fps;
            dg.Rows[(int)prm.lQt1].Cells[2].Value = Properties.Settings.Default.s2Q1;
            dg.Rows[(int)prm.lQt2].Cells[2].Value = Properties.Settings.Default.s2Q2;
            dg.Rows[(int)prm.lJampSize].Cells[2].Value = Properties.Settings.Default.s2JampSize;
            dg.Rows[(int)prm.lMarkSize].Cells[2].Value = Properties.Settings.Default.s2MarkSize;
            dg.Rows[(int)prm.lPower].Cells[2].Value = Properties.Settings.Default.s2Power;

            dg.Rows[(int)prm.lStep].Cells[3].Value = Properties.Settings.Default.s3Step;
            dg.Rows[(int)prm.lLaserOn].Cells[3].Value = Properties.Settings.Default.s3LaserOn;
            dg.Rows[(int)prm.lLaserOff].Cells[3].Value = Properties.Settings.Default.s3LaserOff;
            dg.Rows[(int)prm.lPolygon].Cells[3].Value = Properties.Settings.Default.s3PolygonDelay;
            dg.Rows[(int)prm.lMarkDelay].Cells[3].Value = Properties.Settings.Default.s3MarkDelay;
            dg.Rows[(int)prm.lJampDelay].Cells[3].Value = Properties.Settings.Default.s3JampDelay;
            dg.Rows[(int)prm.lFps].Cells[3].Value = Properties.Settings.Default.s3Fps;
            dg.Rows[(int)prm.lQt1].Cells[3].Value = Properties.Settings.Default.s3Q1;
            dg.Rows[(int)prm.lQt2].Cells[3].Value = Properties.Settings.Default.s3Q2;
            dg.Rows[(int)prm.lJampSize].Cells[3].Value = Properties.Settings.Default.s3JampSize;
            dg.Rows[(int)prm.lMarkSize].Cells[3].Value = Properties.Settings.Default.s3MarkSize;
            dg.Rows[(int)prm.lPower].Cells[3].Value = Properties.Settings.Default.s3Power;

        }

        private void Form1_Closing(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            solveMode();

            Timer timer = new Timer();
            timer.Interval = (300);
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
            cs.scale = float.Parse(tb_scale.Text, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture);//UInt16.Parse(tb_scale.Text);
            cs.num = Int16.Parse(tb_devn.Text);
            cs.scriptPath = tb_script.Text;

            bool result = initCmd(cs);
            if (result)
            {
                Properties.Settings.Default.correctionFile = tb_corrFile.Text;
                Properties.Settings.Default.scriptFile = tb_script.Text;

                int i = 1;
                Properties.Settings.Default.s1Step = long.Parse(dg.Rows[(int)prm.lStep].Cells[i].Value.ToString());
                Properties.Settings.Default.s1LaserOn = long.Parse(dg.Rows[(int)prm.lLaserOn].Cells[i].Value.ToString());
                Properties.Settings.Default.s1LaserOff = long.Parse(dg.Rows[(int)prm.lLaserOff].Cells[i].Value.ToString());
                Properties.Settings.Default.s1PolygonDelay = long.Parse(dg.Rows[(int)prm.lPolygon].Cells[i].Value.ToString());
                Properties.Settings.Default.s1MarkDelay = long.Parse(dg.Rows[(int)prm.lMarkDelay].Cells[i].Value.ToString());
                Properties.Settings.Default.s1JampDelay = long.Parse(dg.Rows[(int)prm.lJampDelay].Cells[i].Value.ToString());
                Properties.Settings.Default.s1Fps = long.Parse(dg.Rows[(int)prm.lFps].Cells[i].Value.ToString());
                Properties.Settings.Default.s1Q1 = long.Parse(dg.Rows[(int)prm.lQt1].Cells[i].Value.ToString());
                Properties.Settings.Default.s1Q2 = long.Parse(dg.Rows[(int)prm.lQt2].Cells[i].Value.ToString());
                Properties.Settings.Default.s1JampSize = long.Parse(dg.Rows[(int)prm.lJampSize].Cells[i].Value.ToString());
                Properties.Settings.Default.s1MarkSize = long.Parse(dg.Rows[(int)prm.lMarkSize].Cells[i].Value.ToString());
                Properties.Settings.Default.s1Power = long.Parse(dg.Rows[(int)prm.lPower].Cells[i].Value.ToString());

                i = 2;
                Properties.Settings.Default.s2Step = long.Parse(dg.Rows[(int)prm.lStep].Cells[i].Value.ToString());
                Properties.Settings.Default.s2LaserOn = long.Parse(dg.Rows[(int)prm.lLaserOn].Cells[i].Value.ToString());
                Properties.Settings.Default.s2LaserOff = long.Parse(dg.Rows[(int)prm.lLaserOff].Cells[i].Value.ToString());
                Properties.Settings.Default.s2PolygonDelay = long.Parse(dg.Rows[(int)prm.lPolygon].Cells[i].Value.ToString());
                Properties.Settings.Default.s2MarkDelay = long.Parse(dg.Rows[(int)prm.lMarkDelay].Cells[i].Value.ToString());
                Properties.Settings.Default.s2JampDelay = long.Parse(dg.Rows[(int)prm.lJampDelay].Cells[i].Value.ToString());
                Properties.Settings.Default.s2Fps = long.Parse(dg.Rows[(int)prm.lFps].Cells[i].Value.ToString());
                Properties.Settings.Default.s2Q1 = long.Parse(dg.Rows[(int)prm.lQt1].Cells[i].Value.ToString());
                Properties.Settings.Default.s2Q2 = long.Parse(dg.Rows[(int)prm.lQt2].Cells[i].Value.ToString());
                Properties.Settings.Default.s2JampSize = long.Parse(dg.Rows[(int)prm.lJampSize].Cells[i].Value.ToString());
                Properties.Settings.Default.s2MarkSize = long.Parse(dg.Rows[(int)prm.lMarkSize].Cells[i].Value.ToString());
                Properties.Settings.Default.s2Power = long.Parse(dg.Rows[(int)prm.lPower].Cells[i].Value.ToString());

                i = 3;
                Properties.Settings.Default.s3Step = long.Parse(dg.Rows[(int)prm.lStep].Cells[i].Value.ToString());
                Properties.Settings.Default.s3LaserOn = long.Parse(dg.Rows[(int)prm.lLaserOn].Cells[i].Value.ToString());
                Properties.Settings.Default.s3LaserOff = long.Parse(dg.Rows[(int)prm.lLaserOff].Cells[i].Value.ToString());
                Properties.Settings.Default.s3PolygonDelay = long.Parse(dg.Rows[(int)prm.lPolygon].Cells[i].Value.ToString());
                Properties.Settings.Default.s3MarkDelay = long.Parse(dg.Rows[(int)prm.lMarkDelay].Cells[i].Value.ToString());
                Properties.Settings.Default.s3JampDelay = long.Parse(dg.Rows[(int)prm.lJampDelay].Cells[i].Value.ToString());
                Properties.Settings.Default.s3Fps = long.Parse(dg.Rows[(int)prm.lFps].Cells[i].Value.ToString());
                Properties.Settings.Default.s3Q1 = long.Parse(dg.Rows[(int)prm.lQt1].Cells[i].Value.ToString());
                Properties.Settings.Default.s3Q2 = long.Parse(dg.Rows[(int)prm.lQt2].Cells[i].Value.ToString());
                Properties.Settings.Default.s3JampSize = long.Parse(dg.Rows[(int)prm.lJampSize].Cells[i].Value.ToString());
                Properties.Settings.Default.s3MarkSize = long.Parse(dg.Rows[(int)prm.lMarkSize].Cells[i].Value.ToString());
                Properties.Settings.Default.s3Power = long.Parse(dg.Rows[(int)prm.lPower].Cells[i].Value.ToString());

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
                    // getCorrespondingCorrectionFilePath(openFileDialog1.FileName);
                    readCorrectionTextFile(openFileDialog1.FileName);
                    //fileLoader.openJobfile(openFileDialog1.FileName);
                    // m_layersFinishid = false;
                }

            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "script (*.script)|*.script|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                      if (( openFileDialog1.FileName) != null)
                    {
                        tb_script.Text = openFileDialog1.FileName;

                    }

                }
        }

        private void readCorrectionTextFile(string path)
        {
            try
            {
                float f;
                path = Path.ChangeExtension(path, "txt");

                var value = File.ReadAllLines(path).
                    Where(str => str.Contains('=')).
                    ToArray().
                    Select(l => l.Split(new[] { '=' })).
                    ToDictionary(s => s[0].Trim(), s => s[1].Trim())
                    ["calfactor"];

                if (!float.TryParse(value, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture, out f))
                    throw new Exception("calfactor is incorrect number: " + value);
                tb_scale.Text = value;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                    System.Reflection.MethodBase.GetCurrentMethod().Name ,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1, 
                    (MessageBoxOptions)0x40000);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bt_default_Click(object sender, EventArgs e)
        {
            for (int i = 1; i < 4; i++)
            {


                dg.Rows[(int)prm.lStep].Cells[i].Value = 60;
                dg.Rows[(int)prm.lLaserOn].Cells[i].Value = 200;
                dg.Rows[(int)prm.lLaserOff].Cells[i].Value = 100;
                dg.Rows[(int)prm.lPolygon].Cells[i].Value = 50;
                dg.Rows[(int)prm.lMarkDelay].Cells[i].Value = 100;
                dg.Rows[(int)prm.lJampDelay].Cells[i].Value = 200;
                dg.Rows[(int)prm.lFps].Cells[i].Value = 0;
                dg.Rows[(int)prm.lQt1].Cells[i].Value = 1000;
                dg.Rows[(int)prm.lQt2].Cells[i].Value = 500;
                dg.Rows[(int)prm.lJampSize].Cells[i].Value = 50;
                dg.Rows[(int)prm.lMarkSize].Cells[i].Value = 50;
                dg.Rows[(int)prm.lPower].Cells[i].Value = 100;
            }
        }
    }
}
