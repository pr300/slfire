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
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Reflection;
using System.Diagnostics;


namespace SpIceControllerLib
{

    public enum prm
    {
        lStep = 0,
                lJampSize =1,
        lMarkSize = 2,
        lPower = 3,
        lLaserOn = 4,
        lLaserOff = 5,
        lPolygon = 6,
        lMarkDelay = 7,
        lJampDelay = 8,
        lFps = 9,
        lQt1 = 10,
        lQt2 = 11,
        lScript = 12,
        lCorrect = 13,
        lWorkSpace = 14

    };



    public partial class Form1 : Form
    {
       
        public delegate void CardEventHandler(object sender, CardEventArgs e);
        public event CardEventHandler cardSetting;

        private Timer timer = new Timer();

        public Form1()
        {
            this.Closing += Form1_Closing;




            InitializeComponent();
            //tb_corrFile.Text = Properties.Settings.Default.correctionFile;
           // tb_script.Text = Properties.Settings.Default.scriptFile;
            cb_printDebug.Checked = Properties.Settings.Default.printDebug;

            
            //dg.Columns.Add("name", "Name");
            //dg.Columns.Add("style1", "Style 1");
            //dg.Columns.Add("style2", "Style 2");
            //dg.Columns.Add("style3", "Style 3");
            dg.Rows.Add("Step mks");
            dg.Rows.Add("Jump speed mm/s");
            dg.Rows.Add("Mark speed mm/s");
            dg.Rows.Add("Power %");
            dg.Rows.Add("LaserOn Delay mks");
            dg.Rows.Add("LaserOff Delay mks");
            dg.Rows.Add("Polygon Delay mks");
            dg.Rows.Add("Mark Delay mks");
            dg.Rows.Add("Jump Delay mks");
            dg.Rows.Add("FPS");
            dg.Rows.Add("Q-Switch t1");
            dg.Rows.Add("Q-Switch t2");
            dg.Rows.Add("Script");
            dg.Rows.Add("Correction");
            dg.Rows.Add("WorkSpace");



            dg.AutoGenerateColumns = false;

            dg.AllowUserToAddRows = false;

            Values.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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

            dg.Rows[(int)prm.lWorkSpace].Cells[1].Value = Properties.Settings.Default.workSpace;

            dg.Rows[(int)prm.lScript].Cells[1].Value = Properties.Settings.Default.scriptFile;
            dg.Rows[(int)prm.lCorrect].Cells[1].Value = Properties.Settings.Default.correctionFile;

            readCorrectionTextFile(dg.Rows[(int)prm.lCorrect].Cells[1].Value.ToString());

            dg.EditingControlShowing +=
new DataGridViewEditingControlShowingEventHandler(
dg_EditingControlShowing);
        }


        private void Form1_Closing(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            solveMode();

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

        void dg_EditingControlShowing(object sender,
            DataGridViewEditingControlShowingEventArgs e)
        {
            //restrict the input in the cell[1,1]
         //   if (this.dg.CurrentCell.ColumnIndex == 1)
        //            lStep = 0,
        //        lJampSize =1,
        //lMarkSize = 2,
        //lPower = 3,
        //lLaserOn = 4,
        //lLaserOff = 5,
        //lPolygon = 6,
        //lMarkDelay = 7,
        //lJampDelay = 8,
        //lFps = 9,
        //lQt1 = 10,
        //lQt2 = 11,
        //lScript = 12,
        //lCorrect = 13
            {

                TextBox tx = e.Control as TextBox;

                if (this.dg.CurrentCell.RowIndex == (int)prm.lJampSize || this.dg.CurrentCell.RowIndex == (int)prm.lMarkSize)
                {

                    tx.KeyPress += new KeyPressEventHandler(tx_KeyPress);
                    tx.KeyPress -= new KeyPressEventHandler(tx_KeyPress_Int);
                }
                else if (this.dg.CurrentCell.RowIndex == (int)prm.lStep ||
                    this.dg.CurrentCell.RowIndex == (int)prm.lLaserOn ||
                    this.dg.CurrentCell.RowIndex == (int)prm.lLaserOff ||
                    this.dg.CurrentCell.RowIndex == (int)prm.lPower 
                    )
                {
                    tx.KeyPress += new KeyPressEventHandler(tx_KeyPress_Int);
                }
                else
                {

                    tx.KeyPress -= new KeyPressEventHandler(tx_KeyPress);
                    tx.KeyPress -= new KeyPressEventHandler(tx_KeyPress_Int);

                }
            }
        }

        void tx_KeyPress_Int(object sender, KeyPressEventArgs e)
        {
            TextBox tx = sender as TextBox;
            long tst;
            if (!(Int64.TryParse(tx.Text + e.KeyChar,out tst) || e.KeyChar == '\b'))
            {
                e.Handled = true;
            }
            return;
        }

        void tx_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tx = sender as TextBox;
            float tst;
            if (!(float.TryParse(tx.Text + e.KeyChar, out tst) || e.KeyChar == '\b' ))
            {
                e.Handled = true;
            }
            return;
        }

        private long procentToPower(long val)
        { 
        float f = (float)val;
            return (long)(255.0*f/100.0);
        
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
            cs.corrFilePatch = dg.Rows[(int)prm.lCorrect].Cells[1].Value.ToString();
            cs.power = (UInt16)procentToPower(long.Parse(dg.Rows[(int)prm.lPower].Cells[1].Value.ToString()));
            cs.scale = float.Parse(tb_scale.Text, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture);//UInt16.Parse(tb_scale.Text);
            cs.num = Int16.Parse(tb_devn.Text);
            cs.scriptPath = dg.Rows[(int)prm.lScript].Cells[1].Value.ToString();//tb_script.Text;
            cs.debug = cb_printDebug.Checked;
            cs.ignoreLocalSetting = cb_ignoreListSetting.Checked;
            cs.startLayer = UInt64.Parse(tb_startLayer.Text);

            dg.Rows[(int)prm.lJampSize].Cells[1].Value = dg.Rows[(int)prm.lJampSize].Cells[1].Value.ToString().Replace('.', ',');
            dg.Rows[(int)prm.lMarkSize].Cells[1].Value = dg.Rows[(int)prm.lMarkSize].Cells[1].Value.ToString().Replace('.', ',');


            int i = 1;
            cs.style1.lStep = long.Parse(dg.Rows[(int)prm.lStep].Cells[i].Value.ToString());
            cs.style1.lLaserOn = long.Parse(dg.Rows[(int)prm.lLaserOn].Cells[i].Value.ToString());
            cs.style1.lLaserOff = long.Parse(dg.Rows[(int)prm.lLaserOff].Cells[i].Value.ToString());
            cs.style1.lPolygon = long.Parse(dg.Rows[(int)prm.lPolygon].Cells[i].Value.ToString());
            cs.style1.lMarkDelay = long.Parse(dg.Rows[(int)prm.lMarkDelay].Cells[i].Value.ToString());
            cs.style1.lJampDelay = long.Parse(dg.Rows[(int)prm.lJampDelay].Cells[i].Value.ToString());
            cs.style1.lFps = long.Parse(dg.Rows[(int)prm.lFps].Cells[i].Value.ToString());
            cs.style1.lQt1 = long.Parse(dg.Rows[(int)prm.lQt1].Cells[i].Value.ToString());
            cs.style1.lQt2 = long.Parse(dg.Rows[(int)prm.lQt2].Cells[i].Value.ToString());
            cs.style1.lJampSize = helpers.speedToJampPeriod(cs.style1.lStep, float.Parse(dg.Rows[(int)prm.lJampSize].Cells[i].Value.ToString(), System.Globalization.NumberStyles.Float), cs.scale);
            cs.style1.lMarkSize = helpers.speedToJampPeriod(cs.style1.lStep, float.Parse(dg.Rows[(int)prm.lMarkSize].Cells[i].Value.ToString(), System.Globalization.NumberStyles.Float), cs.scale);
            cs.style1.lPower = procentToPower(long.Parse(dg.Rows[(int)prm.lPower].Cells[i].Value.ToString()));

            cs.workSpacePath = dg.Rows[(int)prm.lWorkSpace].Cells[1].Value.ToString();

            CardEventArgs arg = new CardEventArgs();
            arg.cs= cs;

            cardSetting(this, arg);
            if (SpIceController.isIntiialize)
            {
                Properties.Settings.Default.correctionFile = dg.Rows[(int)prm.lCorrect].Cells[1].Value.ToString();
                Properties.Settings.Default.scriptFile = dg.Rows[(int)prm.lScript].Cells[1].Value.ToString(); ;//tb_script.Text;
                Properties.Settings.Default.printDebug = cb_printDebug.Checked;

                 i = 1;
                Properties.Settings.Default.s1Step = long.Parse(dg.Rows[(int)prm.lStep].Cells[i].Value.ToString());
                Properties.Settings.Default.s1LaserOn = long.Parse(dg.Rows[(int)prm.lLaserOn].Cells[i].Value.ToString());
                Properties.Settings.Default.s1LaserOff = long.Parse(dg.Rows[(int)prm.lLaserOff].Cells[i].Value.ToString());
                Properties.Settings.Default.s1PolygonDelay = long.Parse(dg.Rows[(int)prm.lPolygon].Cells[i].Value.ToString());
                Properties.Settings.Default.s1MarkDelay = long.Parse(dg.Rows[(int)prm.lMarkDelay].Cells[i].Value.ToString());
                Properties.Settings.Default.s1JampDelay = long.Parse(dg.Rows[(int)prm.lJampDelay].Cells[i].Value.ToString());
                Properties.Settings.Default.s1Fps = long.Parse(dg.Rows[(int)prm.lFps].Cells[i].Value.ToString());
                Properties.Settings.Default.s1Q1 = long.Parse(dg.Rows[(int)prm.lQt1].Cells[i].Value.ToString());
                Properties.Settings.Default.s1Q2 = long.Parse(dg.Rows[(int)prm.lQt2].Cells[i].Value.ToString());
                Properties.Settings.Default.s1JampSize = float.Parse(dg.Rows[(int)prm.lJampSize].Cells[i].Value.ToString(), System.Globalization.NumberStyles.Float);
                Properties.Settings.Default.s1MarkSize =float.Parse(dg.Rows[(int)prm.lMarkSize].Cells[i].Value.ToString(), System.Globalization.NumberStyles.Float);
                Properties.Settings.Default.s1Power = long.Parse(dg.Rows[(int)prm.lPower].Cells[i].Value.ToString());

                Properties.Settings.Default.Save();
            }
        }

        private void updateSignals(object sender, EventArgs e)
        {
            tb_buff_state.Text = SpIceController.cardStatus.toString();
            tb_l1_state.Text = PrefetchList.getListState(ListNumber.list1) +  (cb_printDebug.Checked ? PrefetchList.getListStateDebug(ListNumber.list1) : "");
            tb_l2_state.Text = PrefetchList.getListState(ListNumber.list2) + (cb_printDebug.Checked ? PrefetchList.getListStateDebug(ListNumber.list2) : "");
            tb_cl1_state.Text = SpIceController.getStateString() + (cb_printDebug.Checked ? ", " + SpIceController.getStateStringDebug() : "");
            tb_buffLOad_state.Text = fileLoader.getStateString() +  (cb_printDebug.Checked ? fileLoader.getStateStringDebug() : "" );
            tb_form_state.Text = cb_printDebug.Checked ? fileLoader.m_cs.toString() : "";
   
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
                    dg.Rows[(int)prm.lCorrect].Cells[1].Value = openFileDialog1.FileName;
                    readCorrectionTextFile(openFileDialog1.FileName);
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
                        //tb_script.Text = openFileDialog1.FileName;
                        dg.Rows[(int)prm.lScript].Cells[1].Value = openFileDialog1.FileName;
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
           // for (int i = 1; i < 4; i++)
            int i = 1;
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
                dg.Rows[(int)prm.lJampSize].Cells[i].Value = "8000,34";
                dg.Rows[(int)prm.lMarkSize].Cells[i].Value = "8000,34";
                dg.Rows[(int)prm.lPower].Cells[i].Value = 20;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cb_printDebug_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void dg_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void bt_storeSetting_Click(object sender, EventArgs e)
        {

            cardSetting cs = new cardSetting();
 
            cs.corrFilePatch = dg.Rows[(int)prm.lCorrect].Cells[1].Value.ToString();
            cs.power = UInt16.Parse(dg.Rows[(int)prm.lPower].Cells[1].Value.ToString());
            cs.scale = float.Parse(tb_scale.Text, System.Globalization.NumberStyles.Float, CultureInfo.InvariantCulture);//UInt16.Parse(tb_scale.Text);
            cs.num = Int16.Parse(tb_devn.Text);
            cs.scriptPath = dg.Rows[(int)prm.lScript].Cells[1].Value.ToString();//tb_script.Text;
            cs.debug = cb_printDebug.Checked;
            cs.ignoreLocalSetting = cb_ignoreListSetting.Checked;

            dg.Rows[(int)prm.lJampSize].Cells[1].Value = dg.Rows[(int)prm.lJampSize].Cells[1].Value.ToString().Replace('.', ',');
            dg.Rows[(int)prm.lJampSize].Cells[2].Value = dg.Rows[(int)prm.lJampSize].Cells[2].Value.ToString().Replace('.', ',');
            dg.Rows[(int)prm.lJampSize].Cells[3].Value = dg.Rows[(int)prm.lJampSize].Cells[3].Value.ToString().Replace('.', ',');
            dg.Rows[(int)prm.lMarkSize].Cells[1].Value = dg.Rows[(int)prm.lMarkSize].Cells[1].Value.ToString().Replace('.', ',');
            dg.Rows[(int)prm.lMarkSize].Cells[2].Value = dg.Rows[(int)prm.lMarkSize].Cells[2].Value.ToString().Replace('.', ',');
            dg.Rows[(int)prm.lMarkSize].Cells[3].Value = dg.Rows[(int)prm.lMarkSize].Cells[3].Value.ToString().Replace('.', ',');

            int i = 1;
            cs.style1.lStep = long.Parse(dg.Rows[(int)prm.lStep].Cells[i].Value.ToString());
            cs.style1.lLaserOn = long.Parse(dg.Rows[(int)prm.lLaserOn].Cells[i].Value.ToString());
            cs.style1.lLaserOff = long.Parse(dg.Rows[(int)prm.lLaserOff].Cells[i].Value.ToString());
            cs.style1.lPolygon = long.Parse(dg.Rows[(int)prm.lPolygon].Cells[i].Value.ToString());
            cs.style1.lMarkDelay = long.Parse(dg.Rows[(int)prm.lMarkDelay].Cells[i].Value.ToString());
            cs.style1.lJampDelay = long.Parse(dg.Rows[(int)prm.lJampDelay].Cells[i].Value.ToString());
            cs.style1.lFps = long.Parse(dg.Rows[(int)prm.lFps].Cells[i].Value.ToString());
            cs.style1.lQt1 = long.Parse(dg.Rows[(int)prm.lQt1].Cells[i].Value.ToString());
            cs.style1.lQt2 = long.Parse(dg.Rows[(int)prm.lQt2].Cells[i].Value.ToString());
            cs.style1.lJampSize =(UInt16) float.Parse(dg.Rows[(int)prm.lJampSize].Cells[i].Value.ToString(), System.Globalization.NumberStyles.Float);
            cs.style1.lMarkSize = (UInt16) float.Parse(dg.Rows[(int)prm.lMarkSize].Cells[i].Value.ToString(), System.Globalization.NumberStyles.Float);
            cs.style1.lPower = procentToPower(long.Parse(dg.Rows[(int)prm.lPower].Cells[i].Value.ToString()));

            SaveFileDialog openFileDialog1 = new SaveFileDialog();

            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "setting (*.ini)|*.ini|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if ((openFileDialog1.FileName) != null)
                {
                    //tb_script.Text = openFileDialog1.FileName;
                    using (Stream writer = new FileStream(openFileDialog1.FileName, FileMode.Create))
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(cardSetting));
                        serializer.Serialize(writer, cs);
                    }
                }

            }
        }

        private void bt_loadSetting_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();

                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "setting (*.ini)|*.ini|All files (*.*)|*.*";
                openFileDialog1.FilterIndex = 1;
                openFileDialog1.RestoreDirectory = true;

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    if ((openFileDialog1.FileName) != null)
                    {
                        //tb_script.Text = openFileDialog1.FileName;
                        using (Stream stream = new FileStream(openFileDialog1.FileName, FileMode.Open))
                        {
                            XmlSerializer serializer = new XmlSerializer(typeof(cardSetting));
                            cardSetting cs= (cardSetting)serializer.Deserialize(stream);

                            dg.Rows[(int)prm.lStep].Cells[1].Value = cs.style1.lStep.ToString();
                            dg.Rows[(int)prm.lLaserOn].Cells[1].Value = cs.style1.lLaserOn.ToString();
                            dg.Rows[(int)prm.lLaserOff].Cells[1].Value = cs.style1.lLaserOff.ToString();
                            dg.Rows[(int)prm.lPolygon].Cells[1].Value = cs.style1.lPolygon.ToString();
                            dg.Rows[(int)prm.lMarkDelay].Cells[1].Value = cs.style1.lMarkDelay.ToString();
                            dg.Rows[(int)prm.lJampDelay].Cells[1].Value = cs.style1.lJampDelay.ToString();
                            dg.Rows[(int)prm.lFps].Cells[1].Value = cs.style1.lFps.ToString();
                            dg.Rows[(int)prm.lQt1].Cells[1].Value = cs.style1.lQt1.ToString();
                            dg.Rows[(int)prm.lQt2].Cells[1].Value = cs.style1.lQt2.ToString();
                            dg.Rows[(int)prm.lJampSize].Cells[1].Value =cs.style1.lJampSize.ToString();
                            dg.Rows[(int)prm.lMarkSize].Cells[1].Value = cs.style1.lMarkSize.ToString();
                            dg.Rows[(int)prm.lPower].Cells[1].Value = cs.style1.lPower.ToString();


                        }
                    }

                }

            }
            catch
            {


            }

        }

        private void bt_reset_Click(object sender, EventArgs e)
        {
            SpIceController.ResetSignal(true);
            SpIceController.ResetSignal(false);
        }

        private void tb_buff_state_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
