namespace ClassLibrary1
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cb_printDebug = new System.Windows.Forms.CheckBox();
            this.tb_form_state = new System.Windows.Forms.TextBox();
            this.tb_buffLOad_state = new System.Windows.Forms.TextBox();
            this.tb_buff_state = new System.Windows.Forms.TextBox();
            this.tb_cl1_state = new System.Windows.Forms.TextBox();
            this.tb_l1_state = new System.Windows.Forms.TextBox();
            this.tb_l2_state = new System.Windows.Forms.TextBox();
            this.bt_default = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_devn = new System.Windows.Forms.TextBox();
            this.bt_initialise = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.bt_LoadCorrFile = new System.Windows.Forms.Button();
            this.tb_initMode = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.cb_mode_b10 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b8 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b13 = new System.Windows.Forms.CheckBox();
            this.cb_mode_dlaser = new System.Windows.Forms.RadioButton();
            this.cb_mode_yag1 = new System.Windows.Forms.RadioButton();
            this.cb_mode_yag2 = new System.Windows.Forms.RadioButton();
            this.cb_mode_co = new System.Windows.Forms.RadioButton();
            this.cb_mode_b0 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b7 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b2 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b11 = new System.Windows.Forms.CheckBox();
            this.cb_mode_b3 = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tb_scale = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.cb_ignoreListSetting = new System.Windows.Forms.CheckBox();
            this.bt_loadSetting = new System.Windows.Forms.Button();
            this.bt_storeSetting = new System.Windows.Forms.Button();
            this.dg = new System.Windows.Forms.DataGridView();
            this.Column1 = new SpannedDataGridView.DataGridViewTextBoxColumnEx();
            this.Column2 = new SpannedDataGridView.DataGridViewTextBoxColumnEx();
            this.Column3 = new SpannedDataGridView.DataGridViewTextBoxColumnEx();
            this.Column4 = new SpannedDataGridView.DataGridViewTextBoxColumnEx();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bt_storeSetting);
            this.groupBox1.Controls.Add(this.bt_loadSetting);
            this.groupBox1.Controls.Add(this.cb_ignoreListSetting);
            this.groupBox1.Controls.Add(this.cb_printDebug);
            this.groupBox1.Controls.Add(this.tb_form_state);
            this.groupBox1.Controls.Add(this.tb_buffLOad_state);
            this.groupBox1.Controls.Add(this.tb_buff_state);
            this.groupBox1.Controls.Add(this.tb_cl1_state);
            this.groupBox1.Controls.Add(this.tb_l1_state);
            this.groupBox1.Controls.Add(this.tb_l2_state);
            this.groupBox1.Controls.Add(this.bt_default);
            this.groupBox1.Controls.Add(this.dg);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.tb_devn);
            this.groupBox1.Controls.Add(this.bt_initialise);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.bt_LoadCorrFile);
            this.groupBox1.Controls.Add(this.tb_initMode);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.cb_mode_b10);
            this.groupBox1.Controls.Add(this.cb_mode_b8);
            this.groupBox1.Controls.Add(this.cb_mode_b13);
            this.groupBox1.Controls.Add(this.cb_mode_dlaser);
            this.groupBox1.Controls.Add(this.cb_mode_yag1);
            this.groupBox1.Controls.Add(this.cb_mode_yag2);
            this.groupBox1.Controls.Add(this.cb_mode_co);
            this.groupBox1.Controls.Add(this.cb_mode_b0);
            this.groupBox1.Controls.Add(this.cb_mode_b7);
            this.groupBox1.Controls.Add(this.cb_mode_b2);
            this.groupBox1.Controls.Add(this.cb_mode_b11);
            this.groupBox1.Controls.Add(this.cb_mode_b3);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.tb_scale);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(885, 487);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Параметры инициализации";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cb_printDebug
            // 
            this.cb_printDebug.AutoSize = true;
            this.cb_printDebug.Location = new System.Drawing.Point(779, 281);
            this.cb_printDebug.Name = "cb_printDebug";
            this.cb_printDebug.Size = new System.Drawing.Size(81, 17);
            this.cb_printDebug.TabIndex = 70;
            this.cb_printDebug.Text = "Debug print";
            this.cb_printDebug.UseVisualStyleBackColor = true;
            // 
            // tb_form_state
            // 
            this.tb_form_state.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_form_state.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_form_state.Location = new System.Drawing.Point(12, 463);
            this.tb_form_state.Name = "tb_form_state";
            this.tb_form_state.ReadOnly = true;
            this.tb_form_state.Size = new System.Drawing.Size(867, 15);
            this.tb_form_state.TabIndex = 69;
            // 
            // tb_buffLOad_state
            // 
            this.tb_buffLOad_state.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_buffLOad_state.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_buffLOad_state.Location = new System.Drawing.Point(12, 353);
            this.tb_buffLOad_state.Name = "tb_buffLOad_state";
            this.tb_buffLOad_state.ReadOnly = true;
            this.tb_buffLOad_state.Size = new System.Drawing.Size(867, 15);
            this.tb_buffLOad_state.TabIndex = 68;
            // 
            // tb_buff_state
            // 
            this.tb_buff_state.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_buff_state.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_buff_state.Location = new System.Drawing.Point(12, 375);
            this.tb_buff_state.Name = "tb_buff_state";
            this.tb_buff_state.ReadOnly = true;
            this.tb_buff_state.Size = new System.Drawing.Size(867, 15);
            this.tb_buff_state.TabIndex = 67;
            // 
            // tb_cl1_state
            // 
            this.tb_cl1_state.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_cl1_state.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_cl1_state.Location = new System.Drawing.Point(12, 397);
            this.tb_cl1_state.Name = "tb_cl1_state";
            this.tb_cl1_state.ReadOnly = true;
            this.tb_cl1_state.Size = new System.Drawing.Size(867, 15);
            this.tb_cl1_state.TabIndex = 63;
            // 
            // tb_l1_state
            // 
            this.tb_l1_state.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_l1_state.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_l1_state.Location = new System.Drawing.Point(12, 419);
            this.tb_l1_state.Name = "tb_l1_state";
            this.tb_l1_state.ReadOnly = true;
            this.tb_l1_state.Size = new System.Drawing.Size(867, 15);
            this.tb_l1_state.TabIndex = 62;
            // 
            // tb_l2_state
            // 
            this.tb_l2_state.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_l2_state.Font = new System.Drawing.Font("Consolas", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tb_l2_state.Location = new System.Drawing.Point(12, 441);
            this.tb_l2_state.Name = "tb_l2_state";
            this.tb_l2_state.ReadOnly = true;
            this.tb_l2_state.Size = new System.Drawing.Size(867, 15);
            this.tb_l2_state.TabIndex = 61;
            // 
            // bt_default
            // 
            this.bt_default.Location = new System.Drawing.Point(779, 304);
            this.bt_default.Name = "bt_default";
            this.bt_default.Size = new System.Drawing.Size(100, 32);
            this.bt_default.TabIndex = 59;
            this.bt_default.Text = "Default";
            this.bt_default.UseVisualStyleBackColor = true;
            this.bt_default.Click += new System.EventHandler(this.bt_default_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(773, 48);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 29);
            this.button1.TabIndex = 40;
            this.button1.Text = "Script";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_devn
            // 
            this.tb_devn.Location = new System.Drawing.Point(827, 254);
            this.tb_devn.Name = "tb_devn";
            this.tb_devn.Size = new System.Drawing.Size(44, 20);
            this.tb_devn.TabIndex = 15;
            this.tb_devn.Text = "1";
            // 
            // bt_initialise
            // 
            this.bt_initialise.Location = new System.Drawing.Point(773, 14);
            this.bt_initialise.Name = "bt_initialise";
            this.bt_initialise.Size = new System.Drawing.Size(100, 29);
            this.bt_initialise.TabIndex = 39;
            this.bt_initialise.Text = "Initialise";
            this.bt_initialise.UseVisualStyleBackColor = true;
            this.bt_initialise.Click += new System.EventHandler(this.bt_initialise_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(773, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Dev";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // bt_LoadCorrFile
            // 
            this.bt_LoadCorrFile.Location = new System.Drawing.Point(774, 82);
            this.bt_LoadCorrFile.Name = "bt_LoadCorrFile";
            this.bt_LoadCorrFile.Size = new System.Drawing.Size(100, 29);
            this.bt_LoadCorrFile.TabIndex = 38;
            this.bt_LoadCorrFile.Text = "CorrFile";
            this.bt_LoadCorrFile.UseVisualStyleBackColor = true;
            this.bt_LoadCorrFile.Click += new System.EventHandler(this.bt_LoadCorrFile_Click);
            // 
            // tb_initMode
            // 
            this.tb_initMode.Location = new System.Drawing.Point(49, 298);
            this.tb_initMode.Name = "tb_initMode";
            this.tb_initMode.Size = new System.Drawing.Size(60, 20);
            this.tb_initMode.TabIndex = 35;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 301);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 34;
            this.label9.Text = "Mode";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // cb_mode_b10
            // 
            this.cb_mode_b10.AutoSize = true;
            this.cb_mode_b10.Checked = true;
            this.cb_mode_b10.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_mode_b10.Location = new System.Drawing.Point(12, 138);
            this.cb_mode_b10.Name = "cb_mode_b10";
            this.cb_mode_b10.Size = new System.Drawing.Size(71, 17);
            this.cb_mode_b10.TabIndex = 33;
            this.cb_mode_b10.Text = "LM signal";
            this.cb_mode_b10.UseVisualStyleBackColor = true;
            this.cb_mode_b10.CheckedChanged += new System.EventHandler(this.cb_mode_b10_CheckedChanged);
            // 
            // cb_mode_b8
            // 
            this.cb_mode_b8.AutoSize = true;
            this.cb_mode_b8.Location = new System.Drawing.Point(12, 115);
            this.cb_mode_b8.Name = "cb_mode_b8";
            this.cb_mode_b8.Size = new System.Drawing.Size(97, 17);
            this.cb_mode_b8.TabIndex = 32;
            this.cb_mode_b8.Text = "Disable 3d corr";
            this.cb_mode_b8.UseVisualStyleBackColor = true;
            this.cb_mode_b8.CheckedChanged += new System.EventHandler(this.cb_mode_b8_CheckedChanged);
            // 
            // cb_mode_b13
            // 
            this.cb_mode_b13.AutoSize = true;
            this.cb_mode_b13.Location = new System.Drawing.Point(12, 183);
            this.cb_mode_b13.Name = "cb_mode_b13";
            this.cb_mode_b13.Size = new System.Drawing.Size(84, 17);
            this.cb_mode_b13.TabIndex = 31;
            this.cb_mode_b13.Text = "3d set mode";
            this.cb_mode_b13.UseVisualStyleBackColor = true;
            this.cb_mode_b13.CheckedChanged += new System.EventHandler(this.cb_mode_b13_CheckedChanged);
            // 
            // cb_mode_dlaser
            // 
            this.cb_mode_dlaser.AutoSize = true;
            this.cb_mode_dlaser.Location = new System.Drawing.Point(12, 271);
            this.cb_mode_dlaser.Name = "cb_mode_dlaser";
            this.cb_mode_dlaser.Size = new System.Drawing.Size(105, 17);
            this.cb_mode_dlaser.TabIndex = 30;
            this.cb_mode_dlaser.Text = "Diod Laser mode";
            this.cb_mode_dlaser.UseVisualStyleBackColor = true;
            this.cb_mode_dlaser.CheckedChanged += new System.EventHandler(this.cb_mode_dlaser_CheckedChanged);
            // 
            // cb_mode_yag1
            // 
            this.cb_mode_yag1.AutoSize = true;
            this.cb_mode_yag1.Checked = true;
            this.cb_mode_yag1.Location = new System.Drawing.Point(12, 248);
            this.cb_mode_yag1.Name = "cb_mode_yag1";
            this.cb_mode_yag1.Size = new System.Drawing.Size(85, 17);
            this.cb_mode_yag1.TabIndex = 29;
            this.cb_mode_yag1.TabStop = true;
            this.cb_mode_yag1.Text = "YAG mode 1";
            this.cb_mode_yag1.UseVisualStyleBackColor = true;
            this.cb_mode_yag1.CheckedChanged += new System.EventHandler(this.cb_mode_yag1_CheckedChanged);
            // 
            // cb_mode_yag2
            // 
            this.cb_mode_yag2.AutoSize = true;
            this.cb_mode_yag2.Location = new System.Drawing.Point(12, 225);
            this.cb_mode_yag2.Name = "cb_mode_yag2";
            this.cb_mode_yag2.Size = new System.Drawing.Size(85, 17);
            this.cb_mode_yag2.TabIndex = 28;
            this.cb_mode_yag2.Text = "YAG mode 2";
            this.cb_mode_yag2.UseVisualStyleBackColor = true;
            this.cb_mode_yag2.CheckedChanged += new System.EventHandler(this.cb_mode_yag2_CheckedChanged);
            // 
            // cb_mode_co
            // 
            this.cb_mode_co.AutoSize = true;
            this.cb_mode_co.Location = new System.Drawing.Point(12, 202);
            this.cb_mode_co.Name = "cb_mode_co";
            this.cb_mode_co.Size = new System.Drawing.Size(75, 17);
            this.cb_mode_co.TabIndex = 27;
            this.cb_mode_co.Text = "CO2 mode";
            this.cb_mode_co.UseVisualStyleBackColor = true;
            this.cb_mode_co.CheckedChanged += new System.EventHandler(this.cb_mode_co_CheckedChanged);
            // 
            // cb_mode_b0
            // 
            this.cb_mode_b0.AutoSize = true;
            this.cb_mode_b0.Location = new System.Drawing.Point(12, 26);
            this.cb_mode_b0.Name = "cb_mode_b0";
            this.cb_mode_b0.Size = new System.Drawing.Size(70, 17);
            this.cb_mode_b0.TabIndex = 22;
            this.cb_mode_b0.Text = "Swap XY";
            this.cb_mode_b0.UseVisualStyleBackColor = true;
            this.cb_mode_b0.CheckedChanged += new System.EventHandler(this.cb_mode_b0_CheckedChanged);
            // 
            // cb_mode_b7
            // 
            this.cb_mode_b7.AutoSize = true;
            this.cb_mode_b7.Location = new System.Drawing.Point(12, 95);
            this.cb_mode_b7.Name = "cb_mode_b7";
            this.cb_mode_b7.Size = new System.Drawing.Size(98, 17);
            this.cb_mode_b7.TabIndex = 26;
            this.cb_mode_b7.Text = "Skip Correction";
            this.cb_mode_b7.UseVisualStyleBackColor = true;
            this.cb_mode_b7.CheckedChanged += new System.EventHandler(this.cb_mode_b7_CheckedChanged);
            // 
            // cb_mode_b2
            // 
            this.cb_mode_b2.AutoSize = true;
            this.cb_mode_b2.Location = new System.Drawing.Point(12, 49);
            this.cb_mode_b2.Name = "cb_mode_b2";
            this.cb_mode_b2.Size = new System.Drawing.Size(63, 17);
            this.cb_mode_b2.TabIndex = 23;
            this.cb_mode_b2.Text = "Invert X";
            this.cb_mode_b2.UseVisualStyleBackColor = true;
            this.cb_mode_b2.CheckedChanged += new System.EventHandler(this.cb_mode_b2_CheckedChanged);
            // 
            // cb_mode_b11
            // 
            this.cb_mode_b11.AutoSize = true;
            this.cb_mode_b11.Checked = true;
            this.cb_mode_b11.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_mode_b11.Location = new System.Drawing.Point(12, 160);
            this.cb_mode_b11.Name = "cb_mode_b11";
            this.cb_mode_b11.Size = new System.Drawing.Size(67, 17);
            this.cb_mode_b11.TabIndex = 25;
            this.cb_mode_b11.Text = "LM Gate";
            this.cb_mode_b11.UseVisualStyleBackColor = true;
            this.cb_mode_b11.CheckedChanged += new System.EventHandler(this.cb_mode_b11_CheckedChanged);
            // 
            // cb_mode_b3
            // 
            this.cb_mode_b3.AutoSize = true;
            this.cb_mode_b3.Location = new System.Drawing.Point(12, 72);
            this.cb_mode_b3.Name = "cb_mode_b3";
            this.cb_mode_b3.Size = new System.Drawing.Size(60, 17);
            this.cb_mode_b3.TabIndex = 24;
            this.cb_mode_b3.Text = "Inver Y";
            this.cb_mode_b3.UseVisualStyleBackColor = true;
            this.cb_mode_b3.CheckedChanged += new System.EventHandler(this.cb_mode_b3_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(773, 222);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(34, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Scale";
            this.label8.Click += new System.EventHandler(this.label8_Click);
            // 
            // tb_scale
            // 
            this.tb_scale.Location = new System.Drawing.Point(826, 222);
            this.tb_scale.Name = "tb_scale";
            this.tb_scale.Size = new System.Drawing.Size(45, 20);
            this.tb_scale.TabIndex = 20;
            this.tb_scale.Text = "50";
            // 
            // cb_ignoreListSetting
            // 
            this.cb_ignoreListSetting.AutoSize = true;
            this.cb_ignoreListSetting.Location = new System.Drawing.Point(771, 116);
            this.cb_ignoreListSetting.Name = "cb_ignoreListSetting";
            this.cb_ignoreListSetting.Size = new System.Drawing.Size(105, 17);
            this.cb_ignoreListSetting.TabIndex = 71;
            this.cb_ignoreListSetting.Text = "Ignore list setting";
            this.cb_ignoreListSetting.UseVisualStyleBackColor = true;
            // 
            // bt_loadSetting
            // 
            this.bt_loadSetting.Location = new System.Drawing.Point(774, 138);
            this.bt_loadSetting.Name = "bt_loadSetting";
            this.bt_loadSetting.Size = new System.Drawing.Size(100, 29);
            this.bt_loadSetting.TabIndex = 72;
            this.bt_loadSetting.Text = "Load setting";
            this.bt_loadSetting.UseVisualStyleBackColor = true;
            this.bt_loadSetting.Click += new System.EventHandler(this.bt_loadSetting_Click);
            // 
            // bt_storeSetting
            // 
            this.bt_storeSetting.Location = new System.Drawing.Point(774, 172);
            this.bt_storeSetting.Name = "bt_storeSetting";
            this.bt_storeSetting.Size = new System.Drawing.Size(100, 29);
            this.bt_storeSetting.TabIndex = 73;
            this.bt_storeSetting.Text = "Store setting";
            this.bt_storeSetting.UseVisualStyleBackColor = true;
            this.bt_storeSetting.Click += new System.EventHandler(this.bt_storeSetting_Click);
            // 
            // dg
            // 
            this.dg.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dg.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dg.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
            this.dg.Location = new System.Drawing.Point(150, 12);
            this.dg.Name = "dg";
            this.dg.Size = new System.Drawing.Size(617, 324);
            this.dg.TabIndex = 58;
            this.dg.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dg_CellContentClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Name";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Style 1 Border";
            this.Column2.Name = "Column2";
            // 
            // Column3
            // 
            this.Column3.HeaderText = "Style 2 Outer";
            this.Column3.Name = "Column3";
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Style 3 Inner";
            this.Column4.Name = "Column4";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(885, 487);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Инициализация SPI-PRO-1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tb_scale;
        private System.Windows.Forms.CheckBox cb_mode_b10;
        private System.Windows.Forms.CheckBox cb_mode_b8;
        private System.Windows.Forms.CheckBox cb_mode_b13;
        private System.Windows.Forms.RadioButton cb_mode_dlaser;
        private System.Windows.Forms.RadioButton cb_mode_yag1;
        private System.Windows.Forms.RadioButton cb_mode_yag2;
        private System.Windows.Forms.RadioButton cb_mode_co;
        private System.Windows.Forms.CheckBox cb_mode_b0;
        private System.Windows.Forms.CheckBox cb_mode_b7;
        private System.Windows.Forms.CheckBox cb_mode_b2;
        private System.Windows.Forms.CheckBox cb_mode_b11;
        private System.Windows.Forms.CheckBox cb_mode_b3;
        private System.Windows.Forms.TextBox tb_initMode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button bt_LoadCorrFile;
        private System.Windows.Forms.Button bt_initialise;
        private System.Windows.Forms.TextBox tb_devn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridView dg;
        private System.Windows.Forms.Button bt_default;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox tb_l1_state;
        private System.Windows.Forms.TextBox tb_l2_state;
        private System.Windows.Forms.TextBox tb_cl1_state;
        private System.Windows.Forms.TextBox tb_buff_state;
        private System.Windows.Forms.TextBox tb_buffLOad_state;
        private System.Windows.Forms.TextBox tb_form_state;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.CheckBox cb_printDebug;
        private SpannedDataGridView.DataGridViewTextBoxColumnEx Column1;
        private SpannedDataGridView.DataGridViewTextBoxColumnEx Column2;
        private SpannedDataGridView.DataGridViewTextBoxColumnEx Column3;
        private SpannedDataGridView.DataGridViewTextBoxColumnEx Column4;
        private System.Windows.Forms.CheckBox cb_ignoreListSetting;
        private System.Windows.Forms.Button bt_storeSetting;
        private System.Windows.Forms.Button bt_loadSetting;


    }
}