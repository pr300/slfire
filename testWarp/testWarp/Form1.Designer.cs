namespace testWarp
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.b_init = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_mode_b0 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b2 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b3 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b11 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b7 = new System.Windows.Forms.CheckBox();
            this.cb_mode_co = new System.Windows.Forms.RadioButton();
            this.cb_mode_yag2 = new System.Windows.Forms.RadioButton();
            this.cb_mode_yag1 = new System.Windows.Forms.RadioButton();
            this.cb_mode_dlaser = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.tb_multiplier = new System.Windows.Forms.TextBox();
            this.bt_setActive = new System.Windows.Forms.Button();
            this.bt_InitCard = new System.Windows.Forms.Button();
            this.btSetMode = new System.Windows.Forms.Button();
            this.bt_LoadCorrFile = new System.Windows.Forms.Button();
            this.bt_close = new System.Windows.Forms.Button();
            this.tb_devn = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_corrFile = new System.Windows.Forms.TextBox();
            this.cb_mode_b10 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b8 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b13 = new System.Windows.Forms.CheckBox();
            this.tb_initMode = new System.Windows.Forms.TextBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.bt_laserOff = new System.Windows.Forms.Button();
            this.bt_end_list1 = new System.Windows.Forms.Button();
            this.bt_exeList1 = new System.Windows.Forms.Button();
            this.bt_osclOn = new System.Windows.Forms.Button();
            this.bt_pol_abc = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.tb_laser_power = new System.Windows.Forms.TextBox();
            this.bt_set_laser_power = new System.Windows.Forms.Button();
            this.bb_set_delay = new System.Windows.Forms.Button();
            this.bt_list_1 = new System.Windows.Forms.Button();
            this.bt_stop = new System.Windows.Forms.Button();
            this.tb_step = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.X0 = new System.Windows.Forms.Label();
            this.tb_y0 = new System.Windows.Forms.TextBox();
            this.tb_x0 = new System.Windows.Forms.TextBox();
            this.bt_list1_fill = new System.Windows.Forms.Button();
            this.bt_LaseOff = new System.Windows.Forms.Button();
            this.bt_laserOn = new System.Windows.Forms.Button();
            this.bt_getState = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tb_del_t4 = new System.Windows.Forms.TextBox();
            this.tb_del_t3 = new System.Windows.Forms.TextBox();
            this.bt_set_delt_3t4 = new System.Windows.Forms.Button();
            this.tb_del_t2 = new System.Windows.Forms.TextBox();
            this.tb_del_t1 = new System.Windows.Forms.TextBox();
            this.bt_del_t1t2 = new System.Windows.Forms.Button();
            this.tb_write_val = new System.Windows.Forms.TextBox();
            this.tb_write_port = new System.Windows.Forms.TextBox();
            this.bwrite_port_list = new System.Windows.Forms.Button();
            this.cr_endPos = new System.Windows.Forms.TextBox();
            this.cr_startPos = new System.Windows.Forms.TextBox();
            this.cr_isInstance = new System.Windows.Forms.CheckBox();
            this.cr_validFileName = new System.Windows.Forms.CheckBox();
            this.bt_LoadJobFile = new System.Windows.Forms.Button();
            this.cr_state = new System.Windows.Forms.TextBox();
            this.cb_run = new System.Windows.Forms.CheckBox();
            this.cb_stop = new System.Windows.Forms.CheckBox();
            this.bt_sendSignals = new System.Windows.Forms.Button();
            this.cb_reset = new System.Windows.Forms.CheckBox();
            this.cb_layerFinished = new System.Windows.Forms.CheckBox();
            this.cb_isBufferFull = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_init
            // 
            this.b_init.Location = new System.Drawing.Point(6, 19);
            this.b_init.Name = "b_init";
            this.b_init.Size = new System.Drawing.Size(204, 28);
            this.b_init.TabIndex = 0;
            this.b_init.Text = "Init";
            this.b_init.UseVisualStyleBackColor = true;
            this.b_init.Click += new System.EventHandler(this.b_init_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 87);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mode";
            // 
            // cb_mode_b0
            // 
            this.cb_mode_b0.AutoSize = true;
            this.cb_mode_b0.Location = new System.Drawing.Point(6, 135);
            this.cb_mode_b0.Name = "cb_mode_b0";
            this.cb_mode_b0.Size = new System.Drawing.Size(70, 17);
            this.cb_mode_b0.TabIndex = 3;
            this.cb_mode_b0.Text = "Swap XY";
            this.cb_mode_b0.UseVisualStyleBackColor = true;
            this.cb_mode_b0.CheckedChanged += new System.EventHandler(this.cb_mode_b0_CheckedChanged);
            // 
            // cb_mode_b2
            // 
            this.cb_mode_b2.AutoSize = true;
            this.cb_mode_b2.Location = new System.Drawing.Point(6, 158);
            this.cb_mode_b2.Name = "cb_mode_b2";
            this.cb_mode_b2.Size = new System.Drawing.Size(63, 17);
            this.cb_mode_b2.TabIndex = 4;
            this.cb_mode_b2.Text = "Invert X";
            this.cb_mode_b2.UseVisualStyleBackColor = true;
            this.cb_mode_b2.CheckedChanged += new System.EventHandler(this.cb_mode_b2_CheckedChanged);
            // 
            // cb_mode_b3
            // 
            this.cb_mode_b3.AutoSize = true;
            this.cb_mode_b3.Location = new System.Drawing.Point(6, 181);
            this.cb_mode_b3.Name = "cb_mode_b3";
            this.cb_mode_b3.Size = new System.Drawing.Size(60, 17);
            this.cb_mode_b3.TabIndex = 5;
            this.cb_mode_b3.Text = "Inver Y";
            this.cb_mode_b3.UseVisualStyleBackColor = true;
            this.cb_mode_b3.CheckedChanged += new System.EventHandler(this.cb_mode_b3_CheckedChanged);
            // 
            // cb_mode_b11
            // 
            this.cb_mode_b11.AutoSize = true;
            this.cb_mode_b11.Checked = true;
            this.cb_mode_b11.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_mode_b11.Location = new System.Drawing.Point(6, 269);
            this.cb_mode_b11.Name = "cb_mode_b11";
            this.cb_mode_b11.Size = new System.Drawing.Size(67, 17);
            this.cb_mode_b11.TabIndex = 6;
            this.cb_mode_b11.Text = "LM Gate";
            this.cb_mode_b11.UseVisualStyleBackColor = true;
            this.cb_mode_b11.CheckedChanged += new System.EventHandler(this.cb_mode_b11_CheckedChanged);
            // 
            // cb_mode_b7
            // 
            this.cb_mode_b7.AutoSize = true;
            this.cb_mode_b7.Location = new System.Drawing.Point(6, 204);
            this.cb_mode_b7.Name = "cb_mode_b7";
            this.cb_mode_b7.Size = new System.Drawing.Size(98, 17);
            this.cb_mode_b7.TabIndex = 7;
            this.cb_mode_b7.Text = "Skip Correction";
            this.cb_mode_b7.UseVisualStyleBackColor = true;
            this.cb_mode_b7.CheckedChanged += new System.EventHandler(this.cb_mode_b7_CheckedChanged);
            // 
            // cb_mode_co
            // 
            this.cb_mode_co.AutoSize = true;
            this.cb_mode_co.Location = new System.Drawing.Point(121, 135);
            this.cb_mode_co.Name = "cb_mode_co";
            this.cb_mode_co.Size = new System.Drawing.Size(75, 17);
            this.cb_mode_co.TabIndex = 8;
            this.cb_mode_co.Text = "CO2 mode";
            this.cb_mode_co.UseVisualStyleBackColor = true;
            this.cb_mode_co.CheckedChanged += new System.EventHandler(this.cb_mode_co_CheckedChanged);
            // 
            // cb_mode_yag2
            // 
            this.cb_mode_yag2.AutoSize = true;
            this.cb_mode_yag2.Location = new System.Drawing.Point(121, 158);
            this.cb_mode_yag2.Name = "cb_mode_yag2";
            this.cb_mode_yag2.Size = new System.Drawing.Size(85, 17);
            this.cb_mode_yag2.TabIndex = 9;
            this.cb_mode_yag2.Text = "YAG mode 2";
            this.cb_mode_yag2.UseVisualStyleBackColor = true;
            this.cb_mode_yag2.CheckedChanged += new System.EventHandler(this.cb_mode_yag2_CheckedChanged);
            // 
            // cb_mode_yag1
            // 
            this.cb_mode_yag1.AutoSize = true;
            this.cb_mode_yag1.Checked = true;
            this.cb_mode_yag1.Location = new System.Drawing.Point(121, 181);
            this.cb_mode_yag1.Name = "cb_mode_yag1";
            this.cb_mode_yag1.Size = new System.Drawing.Size(85, 17);
            this.cb_mode_yag1.TabIndex = 10;
            this.cb_mode_yag1.TabStop = true;
            this.cb_mode_yag1.Text = "YAG mode 1";
            this.cb_mode_yag1.UseVisualStyleBackColor = true;
            this.cb_mode_yag1.CheckedChanged += new System.EventHandler(this.cb_mode_yag1_CheckedChanged);
            // 
            // cb_mode_dlaser
            // 
            this.cb_mode_dlaser.AutoSize = true;
            this.cb_mode_dlaser.Location = new System.Drawing.Point(121, 204);
            this.cb_mode_dlaser.Name = "cb_mode_dlaser";
            this.cb_mode_dlaser.Size = new System.Drawing.Size(105, 17);
            this.cb_mode_dlaser.TabIndex = 11;
            this.cb_mode_dlaser.Text = "Diod Laser mode";
            this.cb_mode_dlaser.UseVisualStyleBackColor = true;
            this.cb_mode_dlaser.CheckedChanged += new System.EventHandler(this.cb_mode_dlaser_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.tb_multiplier);
            this.groupBox1.Controls.Add(this.bt_setActive);
            this.groupBox1.Controls.Add(this.bt_InitCard);
            this.groupBox1.Controls.Add(this.btSetMode);
            this.groupBox1.Controls.Add(this.bt_LoadCorrFile);
            this.groupBox1.Controls.Add(this.bt_close);
            this.groupBox1.Controls.Add(this.tb_devn);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.tb_corrFile);
            this.groupBox1.Controls.Add(this.cb_mode_b10);
            this.groupBox1.Controls.Add(this.cb_mode_b8);
            this.groupBox1.Controls.Add(this.cb_mode_b13);
            this.groupBox1.Controls.Add(this.tb_initMode);
            this.groupBox1.Controls.Add(this.cb_mode_dlaser);
            this.groupBox1.Controls.Add(this.b_init);
            this.groupBox1.Controls.Add(this.cb_mode_yag1);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cb_mode_yag2);
            this.groupBox1.Controls.Add(this.cb_mode_co);
            this.groupBox1.Controls.Add(this.cb_mode_b0);
            this.groupBox1.Controls.Add(this.cb_mode_b7);
            this.groupBox1.Controls.Add(this.cb_mode_b2);
            this.groupBox1.Controls.Add(this.cb_mode_b11);
            this.groupBox1.Controls.Add(this.cb_mode_b3);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(231, 337);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Initialisation";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(131, 248);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(47, 13);
            this.label7.TabIndex = 45;
            this.label7.Text = "multiplier";
            // 
            // tb_multiplier
            // 
            this.tb_multiplier.Location = new System.Drawing.Point(177, 244);
            this.tb_multiplier.Name = "tb_multiplier";
            this.tb_multiplier.Size = new System.Drawing.Size(48, 20);
            this.tb_multiplier.TabIndex = 44;
            this.tb_multiplier.Text = "100,00";
            // 
            // bt_setActive
            // 
            this.bt_setActive.Location = new System.Drawing.Point(150, 55);
            this.bt_setActive.Name = "bt_setActive";
            this.bt_setActive.Size = new System.Drawing.Size(60, 23);
            this.bt_setActive.TabIndex = 18;
            this.bt_setActive.Text = "setActive";
            this.bt_setActive.UseVisualStyleBackColor = true;
            this.bt_setActive.Click += new System.EventHandler(this.bt_setActive_Click);
            // 
            // bt_InitCard
            // 
            this.bt_InitCard.Location = new System.Drawing.Point(79, 56);
            this.bt_InitCard.Name = "bt_InitCard";
            this.bt_InitCard.Size = new System.Drawing.Size(63, 23);
            this.bt_InitCard.TabIndex = 17;
            this.bt_InitCard.Text = "InitCard";
            this.bt_InitCard.UseVisualStyleBackColor = true;
            this.bt_InitCard.Click += new System.EventHandler(this.bt_InitCard_Click);
            // 
            // btSetMode
            // 
            this.btSetMode.Location = new System.Drawing.Point(150, 81);
            this.btSetMode.Name = "btSetMode";
            this.btSetMode.Size = new System.Drawing.Size(60, 23);
            this.btSetMode.TabIndex = 16;
            this.btSetMode.Text = "Set mode";
            this.btSetMode.UseVisualStyleBackColor = true;
            this.btSetMode.Click += new System.EventHandler(this.btSetMode_Click);
            // 
            // bt_LoadCorrFile
            // 
            this.bt_LoadCorrFile.Location = new System.Drawing.Point(149, 110);
            this.bt_LoadCorrFile.Name = "bt_LoadCorrFile";
            this.bt_LoadCorrFile.Size = new System.Drawing.Size(61, 23);
            this.bt_LoadCorrFile.TabIndex = 15;
            this.bt_LoadCorrFile.Text = "CorrFile";
            this.bt_LoadCorrFile.UseVisualStyleBackColor = true;
            this.bt_LoadCorrFile.Click += new System.EventHandler(this.bt_LoadCorrFile_Click);
            // 
            // bt_close
            // 
            this.bt_close.Location = new System.Drawing.Point(131, 292);
            this.bt_close.Name = "bt_close";
            this.bt_close.Size = new System.Drawing.Size(75, 23);
            this.bt_close.TabIndex = 14;
            this.bt_close.Text = "Close";
            this.bt_close.UseVisualStyleBackColor = true;
            this.bt_close.Click += new System.EventHandler(this.bt_close_Click);
            // 
            // tb_devn
            // 
            this.tb_devn.Location = new System.Drawing.Point(43, 58);
            this.tb_devn.Name = "tb_devn";
            this.tb_devn.Size = new System.Drawing.Size(30, 20);
            this.tb_devn.TabIndex = 14;
            this.tb_devn.Text = "1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "dev %";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 13);
            this.label2.TabIndex = 14;
            this.label2.Text = "CorrFile";
            // 
            // tb_corrFile
            // 
            this.tb_corrFile.Location = new System.Drawing.Point(43, 110);
            this.tb_corrFile.Name = "tb_corrFile";
            this.tb_corrFile.Size = new System.Drawing.Size(100, 20);
            this.tb_corrFile.TabIndex = 14;
            this.tb_corrFile.Text = "C200_15.gcd";
            // 
            // cb_mode_b10
            // 
            this.cb_mode_b10.AutoSize = true;
            this.cb_mode_b10.Checked = true;
            this.cb_mode_b10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_mode_b10.Location = new System.Drawing.Point(6, 247);
            this.cb_mode_b10.Name = "cb_mode_b10";
            this.cb_mode_b10.Size = new System.Drawing.Size(71, 17);
            this.cb_mode_b10.TabIndex = 14;
            this.cb_mode_b10.Text = "LM signal";
            this.cb_mode_b10.UseVisualStyleBackColor = true;
            this.cb_mode_b10.CheckedChanged += new System.EventHandler(this.cb_mode_b10_CheckedChanged);
            // 
            // cb_mode_b8
            // 
            this.cb_mode_b8.AutoSize = true;
            this.cb_mode_b8.Location = new System.Drawing.Point(6, 224);
            this.cb_mode_b8.Name = "cb_mode_b8";
            this.cb_mode_b8.Size = new System.Drawing.Size(97, 17);
            this.cb_mode_b8.TabIndex = 13;
            this.cb_mode_b8.Text = "Disable 3d corr";
            this.cb_mode_b8.UseVisualStyleBackColor = true;
            this.cb_mode_b8.CheckedChanged += new System.EventHandler(this.cb_mode_b8_CheckedChanged);
            // 
            // cb_mode_b13
            // 
            this.cb_mode_b13.AutoSize = true;
            this.cb_mode_b13.Location = new System.Drawing.Point(6, 292);
            this.cb_mode_b13.Name = "cb_mode_b13";
            this.cb_mode_b13.Size = new System.Drawing.Size(84, 17);
            this.cb_mode_b13.TabIndex = 12;
            this.cb_mode_b13.Text = "3d set mode";
            this.cb_mode_b13.UseVisualStyleBackColor = true;
            this.cb_mode_b13.CheckedChanged += new System.EventHandler(this.cb_mode_b13_CheckedChanged);
            // 
            // tb_initMode
            // 
            this.tb_initMode.Location = new System.Drawing.Point(43, 84);
            this.tb_initMode.Name = "tb_initMode";
            this.tb_initMode.Size = new System.Drawing.Size(100, 20);
            this.tb_initMode.TabIndex = 3;
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBox1.FormattingEnabled = true;
            this.listBox1.HorizontalScrollbar = true;
            this.listBox1.ItemHeight = 16;
            this.listBox1.Location = new System.Drawing.Point(489, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.listBox1.Size = new System.Drawing.Size(675, 740);
            this.listBox1.TabIndex = 13;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.bt_laserOff);
            this.groupBox2.Controls.Add(this.bt_end_list1);
            this.groupBox2.Controls.Add(this.bt_exeList1);
            this.groupBox2.Controls.Add(this.bt_osclOn);
            this.groupBox2.Controls.Add(this.bt_pol_abc);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.tb_laser_power);
            this.groupBox2.Controls.Add(this.bt_set_laser_power);
            this.groupBox2.Controls.Add(this.bb_set_delay);
            this.groupBox2.Controls.Add(this.bt_list_1);
            this.groupBox2.Controls.Add(this.bt_stop);
            this.groupBox2.Controls.Add(this.tb_step);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.X0);
            this.groupBox2.Controls.Add(this.tb_y0);
            this.groupBox2.Controls.Add(this.tb_x0);
            this.groupBox2.Controls.Add(this.bt_list1_fill);
            this.groupBox2.Location = new System.Drawing.Point(249, 21);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(234, 328);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Execute list 1";
            // 
            // bt_laserOff
            // 
            this.bt_laserOff.Location = new System.Drawing.Point(6, 297);
            this.bt_laserOff.Name = "bt_laserOff";
            this.bt_laserOff.Size = new System.Drawing.Size(75, 23);
            this.bt_laserOff.TabIndex = 19;
            this.bt_laserOff.Text = "Laser off";
            this.bt_laserOff.UseVisualStyleBackColor = true;
            this.bt_laserOff.Click += new System.EventHandler(this.bt_laserOff_Click);
            // 
            // bt_end_list1
            // 
            this.bt_end_list1.Location = new System.Drawing.Point(8, 205);
            this.bt_end_list1.Name = "bt_end_list1";
            this.bt_end_list1.Size = new System.Drawing.Size(75, 23);
            this.bt_end_list1.TabIndex = 18;
            this.bt_end_list1.Text = "End List 1";
            this.bt_end_list1.UseVisualStyleBackColor = true;
            this.bt_end_list1.Click += new System.EventHandler(this.bt_end_list1_Click);
            // 
            // bt_exeList1
            // 
            this.bt_exeList1.Location = new System.Drawing.Point(6, 268);
            this.bt_exeList1.Name = "bt_exeList1";
            this.bt_exeList1.Size = new System.Drawing.Size(75, 23);
            this.bt_exeList1.TabIndex = 17;
            this.bt_exeList1.Text = "Excec list1";
            this.bt_exeList1.UseVisualStyleBackColor = true;
            this.bt_exeList1.Click += new System.EventHandler(this.bt_exeList1_Click);
            // 
            // bt_osclOn
            // 
            this.bt_osclOn.Location = new System.Drawing.Point(6, 239);
            this.bt_osclOn.Name = "bt_osclOn";
            this.bt_osclOn.Size = new System.Drawing.Size(75, 23);
            this.bt_osclOn.TabIndex = 16;
            this.bt_osclOn.Text = "Oscl On";
            this.bt_osclOn.UseVisualStyleBackColor = true;
            this.bt_osclOn.Click += new System.EventHandler(this.bt_osclOn_Click);
            // 
            // bt_pol_abc
            // 
            this.bt_pol_abc.Location = new System.Drawing.Point(8, 175);
            this.bt_pol_abc.Name = "bt_pol_abc";
            this.bt_pol_abc.Size = new System.Drawing.Size(75, 23);
            this.bt_pol_abc.TabIndex = 15;
            this.bt_pol_abc.Text = "Pol_ABC";
            this.bt_pol_abc.UseVisualStyleBackColor = true;
            this.bt_pol_abc.Click += new System.EventHandler(this.bt_pol_abc_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(99, 124);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "val";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(8, 145);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 13;
            this.button2.Text = "Jamp to 0";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // tb_laser_power
            // 
            this.tb_laser_power.Location = new System.Drawing.Point(123, 118);
            this.tb_laser_power.Name = "tb_laser_power";
            this.tb_laser_power.Size = new System.Drawing.Size(34, 20);
            this.tb_laser_power.TabIndex = 12;
            this.tb_laser_power.Text = "150";
            // 
            // bt_set_laser_power
            // 
            this.bt_set_laser_power.Location = new System.Drawing.Point(8, 115);
            this.bt_set_laser_power.Name = "bt_set_laser_power";
            this.bt_set_laser_power.Size = new System.Drawing.Size(75, 23);
            this.bt_set_laser_power.TabIndex = 11;
            this.bt_set_laser_power.Text = "Set power";
            this.bt_set_laser_power.UseVisualStyleBackColor = true;
            this.bt_set_laser_power.Click += new System.EventHandler(this.bt_set_laser_power_Click);
            // 
            // bb_set_delay
            // 
            this.bb_set_delay.Location = new System.Drawing.Point(8, 85);
            this.bb_set_delay.Name = "bb_set_delay";
            this.bb_set_delay.Size = new System.Drawing.Size(75, 23);
            this.bb_set_delay.TabIndex = 10;
            this.bb_set_delay.Text = "set delay";
            this.bb_set_delay.UseVisualStyleBackColor = true;
            this.bb_set_delay.Click += new System.EventHandler(this.bb_set_delay_Click);
            // 
            // bt_list_1
            // 
            this.bt_list_1.Location = new System.Drawing.Point(8, 55);
            this.bt_list_1.Name = "bt_list_1";
            this.bt_list_1.Size = new System.Drawing.Size(75, 23);
            this.bt_list_1.TabIndex = 9;
            this.bt_list_1.Text = "Set list 1";
            this.bt_list_1.UseVisualStyleBackColor = true;
            this.bt_list_1.Click += new System.EventHandler(this.bt_list_1_Click);
            // 
            // bt_stop
            // 
            this.bt_stop.Location = new System.Drawing.Point(123, 297);
            this.bt_stop.Name = "bt_stop";
            this.bt_stop.Size = new System.Drawing.Size(89, 23);
            this.bt_stop.TabIndex = 8;
            this.bt_stop.Text = "Stop";
            this.bt_stop.UseVisualStyleBackColor = true;
            this.bt_stop.Click += new System.EventHandler(this.bt_stop_Click);
            // 
            // tb_step
            // 
            this.tb_step.Location = new System.Drawing.Point(137, 177);
            this.tb_step.Name = "tb_step";
            this.tb_step.Size = new System.Drawing.Size(36, 20);
            this.tb_step.TabIndex = 6;
            this.tb_step.Text = "10000";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(104, 180);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(27, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "step";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(163, 150);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Y0";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // X0
            // 
            this.X0.AutoSize = true;
            this.X0.Location = new System.Drawing.Point(100, 150);
            this.X0.Name = "X0";
            this.X0.Size = new System.Drawing.Size(20, 13);
            this.X0.TabIndex = 3;
            this.X0.Text = "X0";
            // 
            // tb_y0
            // 
            this.tb_y0.Location = new System.Drawing.Point(190, 146);
            this.tb_y0.Name = "tb_y0";
            this.tb_y0.Size = new System.Drawing.Size(36, 20);
            this.tb_y0.TabIndex = 2;
            this.tb_y0.Text = "0";
            // 
            // tb_x0
            // 
            this.tb_x0.Location = new System.Drawing.Point(121, 147);
            this.tb_x0.Name = "tb_x0";
            this.tb_x0.Size = new System.Drawing.Size(36, 20);
            this.tb_x0.TabIndex = 1;
            this.tb_x0.Text = "0";
            // 
            // bt_list1_fill
            // 
            this.bt_list1_fill.Location = new System.Drawing.Point(6, 25);
            this.bt_list1_fill.Name = "bt_list1_fill";
            this.bt_list1_fill.Size = new System.Drawing.Size(220, 23);
            this.bt_list1_fill.TabIndex = 0;
            this.bt_list1_fill.Text = "Fill list 1";
            this.bt_list1_fill.UseVisualStyleBackColor = true;
            this.bt_list1_fill.Click += new System.EventHandler(this.bt_list1_fill_Click);
            // 
            // bt_LaseOff
            // 
            this.bt_LaseOff.Location = new System.Drawing.Point(19, 63);
            this.bt_LaseOff.Name = "bt_LaseOff";
            this.bt_LaseOff.Size = new System.Drawing.Size(75, 23);
            this.bt_LaseOff.TabIndex = 20;
            this.bt_LaseOff.Text = "Disable_Laser";
            this.bt_LaseOff.UseVisualStyleBackColor = true;
            this.bt_LaseOff.Click += new System.EventHandler(this.bt_LaseOff_Click);
            // 
            // bt_laserOn
            // 
            this.bt_laserOn.Location = new System.Drawing.Point(19, 34);
            this.bt_laserOn.Name = "bt_laserOn";
            this.bt_laserOn.Size = new System.Drawing.Size(75, 23);
            this.bt_laserOn.TabIndex = 15;
            this.bt_laserOn.Text = "Enable_Laser";
            this.bt_laserOn.UseVisualStyleBackColor = true;
            this.bt_laserOn.Click += new System.EventHandler(this.bt_laserOn_Click);
            // 
            // bt_getState
            // 
            this.bt_getState.Location = new System.Drawing.Point(125, 34);
            this.bt_getState.Name = "bt_getState";
            this.bt_getState.Size = new System.Drawing.Size(75, 23);
            this.bt_getState.TabIndex = 7;
            this.bt_getState.Text = "getState";
            this.bt_getState.UseVisualStyleBackColor = true;
            this.bt_getState.Click += new System.EventHandler(this.bt_getState_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tb_del_t4);
            this.groupBox3.Controls.Add(this.tb_del_t3);
            this.groupBox3.Controls.Add(this.bt_set_delt_3t4);
            this.groupBox3.Controls.Add(this.tb_del_t2);
            this.groupBox3.Controls.Add(this.tb_del_t1);
            this.groupBox3.Controls.Add(this.bt_del_t1t2);
            this.groupBox3.Controls.Add(this.tb_write_val);
            this.groupBox3.Controls.Add(this.tb_write_port);
            this.groupBox3.Controls.Add(this.bwrite_port_list);
            this.groupBox3.Controls.Add(this.bt_LaseOff);
            this.groupBox3.Controls.Add(this.bt_laserOn);
            this.groupBox3.Controls.Add(this.bt_getState);
            this.groupBox3.Location = new System.Drawing.Point(8, 355);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(467, 184);
            this.groupBox3.TabIndex = 15;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "groupBox3";
            // 
            // tb_del_t4
            // 
            this.tb_del_t4.Location = new System.Drawing.Point(195, 152);
            this.tb_del_t4.Name = "tb_del_t4";
            this.tb_del_t4.Size = new System.Drawing.Size(48, 20);
            this.tb_del_t4.TabIndex = 29;
            this.tb_del_t4.Text = "0";
            // 
            // tb_del_t3
            // 
            this.tb_del_t3.Location = new System.Drawing.Point(139, 153);
            this.tb_del_t3.Name = "tb_del_t3";
            this.tb_del_t3.Size = new System.Drawing.Size(50, 20);
            this.tb_del_t3.TabIndex = 28;
            this.tb_del_t3.Text = "100";
            // 
            // bt_set_delt_3t4
            // 
            this.bt_set_delt_3t4.Location = new System.Drawing.Point(19, 150);
            this.bt_set_delt_3t4.Name = "bt_set_delt_3t4";
            this.bt_set_delt_3t4.Size = new System.Drawing.Size(112, 23);
            this.bt_set_delt_3t4.TabIndex = 27;
            this.bt_set_delt_3t4.Text = "Set del t3, t4";
            this.bt_set_delt_3t4.UseVisualStyleBackColor = true;
            this.bt_set_delt_3t4.Click += new System.EventHandler(this.bt_set_delt_3t4_Click);
            // 
            // tb_del_t2
            // 
            this.tb_del_t2.Location = new System.Drawing.Point(195, 123);
            this.tb_del_t2.Name = "tb_del_t2";
            this.tb_del_t2.Size = new System.Drawing.Size(48, 20);
            this.tb_del_t2.TabIndex = 26;
            this.tb_del_t2.Text = "100";
            // 
            // tb_del_t1
            // 
            this.tb_del_t1.Location = new System.Drawing.Point(139, 124);
            this.tb_del_t1.Name = "tb_del_t1";
            this.tb_del_t1.Size = new System.Drawing.Size(50, 20);
            this.tb_del_t1.TabIndex = 25;
            this.tb_del_t1.Text = "100";
            // 
            // bt_del_t1t2
            // 
            this.bt_del_t1t2.Location = new System.Drawing.Point(19, 121);
            this.bt_del_t1t2.Name = "bt_del_t1t2";
            this.bt_del_t1t2.Size = new System.Drawing.Size(112, 23);
            this.bt_del_t1t2.TabIndex = 24;
            this.bt_del_t1t2.Text = "Set del t1, t2";
            this.bt_del_t1t2.UseVisualStyleBackColor = true;
            this.bt_del_t1t2.Click += new System.EventHandler(this.bt_del_t1t2_Click);
            // 
            // tb_write_val
            // 
            this.tb_write_val.Location = new System.Drawing.Point(195, 94);
            this.tb_write_val.Name = "tb_write_val";
            this.tb_write_val.Size = new System.Drawing.Size(48, 20);
            this.tb_write_val.TabIndex = 23;
            this.tb_write_val.Text = "100";
            // 
            // tb_write_port
            // 
            this.tb_write_port.Location = new System.Drawing.Point(139, 95);
            this.tb_write_port.Name = "tb_write_port";
            this.tb_write_port.Size = new System.Drawing.Size(50, 20);
            this.tb_write_port.TabIndex = 22;
            this.tb_write_port.Text = "A";
            // 
            // bwrite_port_list
            // 
            this.bwrite_port_list.Location = new System.Drawing.Point(19, 92);
            this.bwrite_port_list.Name = "bwrite_port_list";
            this.bwrite_port_list.Size = new System.Drawing.Size(112, 23);
            this.bwrite_port_list.TabIndex = 21;
            this.bwrite_port_list.Text = "Write_port_list";
            this.bwrite_port_list.UseVisualStyleBackColor = true;
            this.bwrite_port_list.Click += new System.EventHandler(this.bwrite_port_list_Click);
            // 
            // cr_endPos
            // 
            this.cr_endPos.Location = new System.Drawing.Point(150, 144);
            this.cr_endPos.Name = "cr_endPos";
            this.cr_endPos.Size = new System.Drawing.Size(48, 20);
            this.cr_endPos.TabIndex = 34;
            this.cr_endPos.Text = "0";
            // 
            // cr_startPos
            // 
            this.cr_startPos.Location = new System.Drawing.Point(150, 118);
            this.cr_startPos.Name = "cr_startPos";
            this.cr_startPos.Size = new System.Drawing.Size(48, 20);
            this.cr_startPos.TabIndex = 33;
            this.cr_startPos.Text = "0";
            // 
            // cr_isInstance
            // 
            this.cr_isInstance.AutoSize = true;
            this.cr_isInstance.Location = new System.Drawing.Point(150, 72);
            this.cr_isInstance.Name = "cr_isInstance";
            this.cr_isInstance.Size = new System.Drawing.Size(74, 17);
            this.cr_isInstance.TabIndex = 32;
            this.cr_isInstance.Text = "isInstance";
            this.cr_isInstance.UseVisualStyleBackColor = true;
            // 
            // cr_validFileName
            // 
            this.cr_validFileName.AutoSize = true;
            this.cr_validFileName.Location = new System.Drawing.Point(150, 95);
            this.cr_validFileName.Name = "cr_validFileName";
            this.cr_validFileName.Size = new System.Drawing.Size(92, 17);
            this.cr_validFileName.TabIndex = 31;
            this.cr_validFileName.Text = "validFileName";
            this.cr_validFileName.UseVisualStyleBackColor = true;
            // 
            // bt_LoadJobFile
            // 
            this.bt_LoadJobFile.Location = new System.Drawing.Point(14, 19);
            this.bt_LoadJobFile.Name = "bt_LoadJobFile";
            this.bt_LoadJobFile.Size = new System.Drawing.Size(112, 23);
            this.bt_LoadJobFile.TabIndex = 30;
            this.bt_LoadJobFile.Text = "LoadJobFile";
            this.bt_LoadJobFile.UseVisualStyleBackColor = true;
            this.bt_LoadJobFile.Click += new System.EventHandler(this.bt_LoadJobFile_Click);
            // 
            // cr_state
            // 
            this.cr_state.Location = new System.Drawing.Point(146, 170);
            this.cr_state.Name = "cr_state";
            this.cr_state.Size = new System.Drawing.Size(85, 20);
            this.cr_state.TabIndex = 36;
            this.cr_state.Text = "0";
            // 
            // cb_run
            // 
            this.cb_run.AutoSize = true;
            this.cb_run.Location = new System.Drawing.Point(15, 89);
            this.cb_run.Name = "cb_run";
            this.cb_run.Size = new System.Drawing.Size(46, 17);
            this.cb_run.TabIndex = 38;
            this.cb_run.Text = "Run";
            this.cb_run.UseVisualStyleBackColor = true;
            this.cb_run.CheckedChanged += new System.EventHandler(this.cb_run_CheckedChanged);
            // 
            // cb_stop
            // 
            this.cb_stop.AutoSize = true;
            this.cb_stop.Location = new System.Drawing.Point(14, 112);
            this.cb_stop.Name = "cb_stop";
            this.cb_stop.Size = new System.Drawing.Size(48, 17);
            this.cb_stop.TabIndex = 39;
            this.cb_stop.Text = "Stop";
            this.cb_stop.UseVisualStyleBackColor = true;
            this.cb_stop.CheckedChanged += new System.EventHandler(this.cb_stop_CheckedChanged);
            // 
            // bt_sendSignals
            // 
            this.bt_sendSignals.Location = new System.Drawing.Point(6, 161);
            this.bt_sendSignals.Name = "bt_sendSignals";
            this.bt_sendSignals.Size = new System.Drawing.Size(75, 23);
            this.bt_sendSignals.TabIndex = 40;
            this.bt_sendSignals.Text = "sendSignals";
            this.bt_sendSignals.UseVisualStyleBackColor = true;
            this.bt_sendSignals.Click += new System.EventHandler(this.bt_sendSignals_Click);
            // 
            // cb_reset
            // 
            this.cb_reset.AutoSize = true;
            this.cb_reset.Location = new System.Drawing.Point(15, 66);
            this.cb_reset.Name = "cb_reset";
            this.cb_reset.Size = new System.Drawing.Size(54, 17);
            this.cb_reset.TabIndex = 41;
            this.cb_reset.Text = "Reset";
            this.cb_reset.UseVisualStyleBackColor = true;
            this.cb_reset.CheckedChanged += new System.EventHandler(this.cb_reset_CheckedChanged);
            // 
            // cb_layerFinished
            // 
            this.cb_layerFinished.AutoSize = true;
            this.cb_layerFinished.Location = new System.Drawing.Point(150, 49);
            this.cb_layerFinished.Name = "cb_layerFinished";
            this.cb_layerFinished.Size = new System.Drawing.Size(88, 17);
            this.cb_layerFinished.TabIndex = 42;
            this.cb_layerFinished.Text = "layerFiniched";
            this.cb_layerFinished.UseVisualStyleBackColor = true;
            // 
            // cb_isBufferFull
            // 
            this.cb_isBufferFull.AutoSize = true;
            this.cb_isBufferFull.Location = new System.Drawing.Point(150, 26);
            this.cb_isBufferFull.Name = "cb_isBufferFull";
            this.cb_isBufferFull.Size = new System.Drawing.Size(77, 17);
            this.cb_isBufferFull.TabIndex = 43;
            this.cb_isBufferFull.Text = "isBufferFull";
            this.cb_isBufferFull.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(360, 167);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 23);
            this.button1.TabIndex = 44;
            this.button1.Text = "Stop thread:)";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.cr_validFileName);
            this.groupBox4.Controls.Add(this.cb_isBufferFull);
            this.groupBox4.Controls.Add(this.bt_LoadJobFile);
            this.groupBox4.Controls.Add(this.cb_layerFinished);
            this.groupBox4.Controls.Add(this.cr_isInstance);
            this.groupBox4.Controls.Add(this.cb_reset);
            this.groupBox4.Controls.Add(this.cr_startPos);
            this.groupBox4.Controls.Add(this.bt_sendSignals);
            this.groupBox4.Controls.Add(this.cr_endPos);
            this.groupBox4.Controls.Add(this.cb_stop);
            this.groupBox4.Controls.Add(this.cb_run);
            this.groupBox4.Controls.Add(this.cr_state);
            this.groupBox4.Location = new System.Drawing.Point(12, 549);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(471, 206);
            this.groupBox4.TabIndex = 16;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Interface";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1176, 767);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Proto";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button b_init;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cb_mode_b0;
        private System.Windows.Forms.CheckBox cb_mode_b2;
        private System.Windows.Forms.CheckBox cb_mode_b3;
        private System.Windows.Forms.CheckBox cb_mode_b11;
        private System.Windows.Forms.CheckBox cb_mode_b7;
        private System.Windows.Forms.RadioButton cb_mode_co;
        private System.Windows.Forms.RadioButton cb_mode_yag2;
        private System.Windows.Forms.RadioButton cb_mode_yag1;
        private System.Windows.Forms.RadioButton cb_mode_dlaser;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cb_mode_b13;
        private System.Windows.Forms.TextBox tb_initMode;
        private System.Windows.Forms.CheckBox cb_mode_b8;
        private System.Windows.Forms.CheckBox cb_mode_b10;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_corrFile;
        private System.Windows.Forms.TextBox tb_devn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bt_close;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button bt_list1_fill;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label X0;
        private System.Windows.Forms.TextBox tb_y0;
        private System.Windows.Forms.TextBox tb_x0;
        private System.Windows.Forms.TextBox tb_step;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button bt_getState;
        private System.Windows.Forms.Button bt_stop;
        private System.Windows.Forms.Button bt_LoadCorrFile;
        private System.Windows.Forms.Button btSetMode;
        private System.Windows.Forms.Button bt_InitCard;
        private System.Windows.Forms.Button bt_list_1;
        private System.Windows.Forms.Button bb_set_delay;
        private System.Windows.Forms.TextBox tb_laser_power;
        private System.Windows.Forms.Button bt_set_laser_power;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button bt_pol_abc;
        private System.Windows.Forms.Button bt_osclOn;
        private System.Windows.Forms.Button bt_exeList1;
        private System.Windows.Forms.Button bt_end_list1;
        private System.Windows.Forms.Button bt_laserOff;
        private System.Windows.Forms.Button bt_laserOn;
        private System.Windows.Forms.Button bt_LaseOff;
        private System.Windows.Forms.Button bt_setActive;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox tb_write_val;
        private System.Windows.Forms.TextBox tb_write_port;
        private System.Windows.Forms.Button bwrite_port_list;
        private System.Windows.Forms.TextBox tb_del_t4;
        private System.Windows.Forms.TextBox tb_del_t3;
        private System.Windows.Forms.Button bt_set_delt_3t4;
        private System.Windows.Forms.TextBox tb_del_t2;
        private System.Windows.Forms.TextBox tb_del_t1;
        private System.Windows.Forms.Button bt_del_t1t2;
        private System.Windows.Forms.Button bt_LoadJobFile;
        private System.Windows.Forms.CheckBox cr_isInstance;
        private System.Windows.Forms.CheckBox cr_validFileName;
        private System.Windows.Forms.TextBox cr_endPos;
        private System.Windows.Forms.TextBox cr_startPos;
        private System.Windows.Forms.TextBox cr_state;
        private System.Windows.Forms.Button bt_sendSignals;
        private System.Windows.Forms.CheckBox cb_stop;
        private System.Windows.Forms.CheckBox cb_run;
        private System.Windows.Forms.CheckBox cb_reset;
        private System.Windows.Forms.CheckBox cb_layerFinished;
        private System.Windows.Forms.CheckBox cb_isBufferFull;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox tb_multiplier;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox4;
    }
}

