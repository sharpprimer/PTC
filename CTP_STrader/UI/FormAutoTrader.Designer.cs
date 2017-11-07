using CTP_STrader.UI;
namespace CTP_STrader
{
    partial class FormAutoTrader
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        protected void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.nUD_HedgeVol = new System.Windows.Forms.NumericUpDown();
            this.label15 = new System.Windows.Forms.Label();
            this.cB_IF1 = new System.Windows.Forms.ComboBox();
            this.label16 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.cB_IF2PriceLevel = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.cB_IF1PriceLevel = new System.Windows.Forms.ComboBox();
            this.label25 = new System.Windows.Forms.Label();
            this.nUD_InitPosition = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.nUD_ReorderTimes = new System.Windows.Forms.NumericUpDown();
            this.cB_IF2 = new System.Windows.Forms.ComboBox();
            this.tB_OpenSpread = new System.Windows.Forms.TextBox();
            this.nUD_MaxPosition = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.tB_CloseSpread = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nUD_ReorderAppendPt = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.nUD_IF1 = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nUD_IF2 = new System.Windows.Forms.NumericUpDown();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btn_Stop = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.listBox_Log = new System.Windows.Forms.ListBox();
            this.btn_Start = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tB_MaxWarnTimes = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.tB_EPrice = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.tB_MaxOpenN = new System.Windows.Forms.TextBox();
            this.tB_QueryWaitTime = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tB_ESpread = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.tB_MaxTN = new System.Windows.Forms.TextBox();
            this.dGV_Detail = new System.Windows.Forms.DataGridView();
            this.dGV_Summary = new System.Windows.Forms.DataGridView();
            this.lb_CloseSpread = new System.Windows.Forms.Label();
            this.lb_OpenSpread = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lb_Diff1 = new System.Windows.Forms.Label();
            this.lb_Time1 = new System.Windows.Forms.Label();
            this.lb_IF2Price = new System.Windows.Forms.Label();
            this.lb_IF1Price = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.lb_IF1 = new System.Windows.Forms.Label();
            this.lb_IF2 = new System.Windows.Forms.Label();
            this.lb_Time2 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.rB_S = new System.Windows.Forms.RadioButton();
            this.rB_A = new System.Windows.Forms.RadioButton();
            this.rB_H = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_HedgeVol)).BeginInit();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_InitPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ReorderTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_MaxPosition)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ReorderAppendPt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_IF1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_IF2)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Detail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Summary)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // nUD_HedgeVol
            // 
            this.nUD_HedgeVol.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nUD_HedgeVol.Location = new System.Drawing.Point(107, 82);
            this.nUD_HedgeVol.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nUD_HedgeVol.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nUD_HedgeVol.Name = "nUD_HedgeVol";
            this.nUD_HedgeVol.Size = new System.Drawing.Size(70, 26);
            this.nUD_HedgeVol.TabIndex = 23;
            this.nUD_HedgeVol.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_HedgeVol.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(283, 18);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 14);
            this.label15.TabIndex = 29;
            this.label15.Text = "IF1";
            // 
            // cB_IF1
            // 
            this.cB_IF1.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cB_IF1.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cB_IF1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_IF1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.cB_IF1.FormattingEnabled = true;
            this.cB_IF1.Location = new System.Drawing.Point(320, 15);
            this.cB_IF1.Name = "cB_IF1";
            this.cB_IF1.Size = new System.Drawing.Size(91, 24);
            this.cB_IF1.TabIndex = 30;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(509, 17);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(31, 14);
            this.label16.TabIndex = 31;
            this.label16.Text = "IF2";
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.Color.LightSeaGreen;
            this.groupBox3.Controls.Add(this.cB_IF2PriceLevel);
            this.groupBox3.Controls.Add(this.label26);
            this.groupBox3.Controls.Add(this.cB_IF1PriceLevel);
            this.groupBox3.Controls.Add(this.label25);
            this.groupBox3.Controls.Add(this.nUD_InitPosition);
            this.groupBox3.Controls.Add(this.label24);
            this.groupBox3.Controls.Add(this.nUD_ReorderTimes);
            this.groupBox3.Controls.Add(this.cB_IF2);
            this.groupBox3.Controls.Add(this.tB_OpenSpread);
            this.groupBox3.Controls.Add(this.nUD_MaxPosition);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.tB_CloseSpread);
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.nUD_ReorderAppendPt);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.nUD_IF1);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label16);
            this.groupBox3.Controls.Add(this.cB_IF1);
            this.groupBox3.Controls.Add(this.label15);
            this.groupBox3.Controls.Add(this.nUD_HedgeVol);
            this.groupBox3.Controls.Add(this.nUD_IF2);
            this.groupBox3.Controls.Add(this.label18);
            this.groupBox3.Controls.Add(this.label17);
            this.groupBox3.Location = new System.Drawing.Point(4, 163);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(706, 200);
            this.groupBox3.TabIndex = 33;
            this.groupBox3.TabStop = false;
            // 
            // cB_IF2PriceLevel
            // 
            this.cB_IF2PriceLevel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cB_IF2PriceLevel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cB_IF2PriceLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_IF2PriceLevel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.cB_IF2PriceLevel.FormattingEnabled = true;
            this.cB_IF2PriceLevel.Location = new System.Drawing.Point(549, 47);
            this.cB_IF2PriceLevel.Name = "cB_IF2PriceLevel";
            this.cB_IF2PriceLevel.Size = new System.Drawing.Size(91, 24);
            this.cB_IF2PriceLevel.TabIndex = 92;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label26.Location = new System.Drawing.Point(453, 51);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(91, 14);
            this.label26.TabIndex = 91;
            this.label26.Text = "IF2下单档位";
            // 
            // cB_IF1PriceLevel
            // 
            this.cB_IF1PriceLevel.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cB_IF1PriceLevel.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cB_IF1PriceLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_IF1PriceLevel.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.cB_IF1PriceLevel.FormattingEnabled = true;
            this.cB_IF1PriceLevel.Location = new System.Drawing.Point(320, 48);
            this.cB_IF1PriceLevel.Name = "cB_IF1PriceLevel";
            this.cB_IF1PriceLevel.Size = new System.Drawing.Size(91, 24);
            this.cB_IF1PriceLevel.TabIndex = 90;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label25.Location = new System.Drawing.Point(224, 52);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(91, 14);
            this.label25.TabIndex = 89;
            this.label25.Text = "IF1下单档位";
            // 
            // nUD_InitPosition
            // 
            this.nUD_InitPosition.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nUD_InitPosition.Location = new System.Drawing.Point(107, 13);
            this.nUD_InitPosition.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nUD_InitPosition.Name = "nUD_InitPosition";
            this.nUD_InitPosition.Size = new System.Drawing.Size(70, 26);
            this.nUD_InitPosition.TabIndex = 88;
            this.nUD_InitPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_InitPosition.ThousandsSeparator = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label24.Location = new System.Drawing.Point(34, 18);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(67, 14);
            this.label24.TabIndex = 87;
            this.label24.Text = "初始仓位";
            // 
            // nUD_ReorderTimes
            // 
            this.nUD_ReorderTimes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nUD_ReorderTimes.Location = new System.Drawing.Point(320, 117);
            this.nUD_ReorderTimes.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.nUD_ReorderTimes.Name = "nUD_ReorderTimes";
            this.nUD_ReorderTimes.Size = new System.Drawing.Size(70, 26);
            this.nUD_ReorderTimes.TabIndex = 84;
            this.nUD_ReorderTimes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_ReorderTimes.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // cB_IF2
            // 
            this.cB_IF2.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cB_IF2.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cB_IF2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_IF2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.cB_IF2.FormattingEnabled = true;
            this.cB_IF2.Location = new System.Drawing.Point(549, 15);
            this.cB_IF2.Name = "cB_IF2";
            this.cB_IF2.Size = new System.Drawing.Size(91, 24);
            this.cB_IF2.TabIndex = 83;
            // 
            // tB_OpenSpread
            // 
            this.tB_OpenSpread.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tB_OpenSpread.Location = new System.Drawing.Point(107, 164);
            this.tB_OpenSpread.Name = "tB_OpenSpread";
            this.tB_OpenSpread.Size = new System.Drawing.Size(70, 26);
            this.tB_OpenSpread.TabIndex = 82;
            this.tB_OpenSpread.Text = "-7.6";
            // 
            // nUD_MaxPosition
            // 
            this.nUD_MaxPosition.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nUD_MaxPosition.Location = new System.Drawing.Point(107, 47);
            this.nUD_MaxPosition.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nUD_MaxPosition.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.nUD_MaxPosition.Name = "nUD_MaxPosition";
            this.nUD_MaxPosition.Size = new System.Drawing.Size(70, 26);
            this.nUD_MaxPosition.TabIndex = 81;
            this.nUD_MaxPosition.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_MaxPosition.ThousandsSeparator = true;
            this.nUD_MaxPosition.Value = new decimal(new int[] {
            66,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(247, 170);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(67, 14);
            this.label7.TabIndex = 73;
            this.label7.Text = "平仓价差";
            // 
            // tB_CloseSpread
            // 
            this.tB_CloseSpread.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tB_CloseSpread.Location = new System.Drawing.Point(320, 164);
            this.tB_CloseSpread.Name = "tB_CloseSpread";
            this.tB_CloseSpread.Size = new System.Drawing.Size(70, 26);
            this.tB_CloseSpread.TabIndex = 72;
            this.tB_CloseSpread.Text = "-6";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(219, 122);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(97, 14);
            this.label11.TabIndex = 79;
            this.label11.Text = "补单尝试次数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(7, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 14);
            this.label8.TabIndex = 75;
            this.label8.Text = "仓位上限(对)";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(24, 170);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(67, 14);
            this.label6.TabIndex = 71;
            this.label6.Text = "开仓价差";
            // 
            // nUD_ReorderAppendPt
            // 
            this.nUD_ReorderAppendPt.DecimalPlaces = 1;
            this.nUD_ReorderAppendPt.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nUD_ReorderAppendPt.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nUD_ReorderAppendPt.Location = new System.Drawing.Point(107, 117);
            this.nUD_ReorderAppendPt.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nUD_ReorderAppendPt.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nUD_ReorderAppendPt.Name = "nUD_ReorderAppendPt";
            this.nUD_ReorderAppendPt.Size = new System.Drawing.Size(72, 26);
            this.nUD_ReorderAppendPt.TabIndex = 74;
            this.nUD_ReorderAppendPt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.nUD_ReorderAppendPt.Value = new decimal(new int[] {
            8,
            0,
            0,
            65536});
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(4, 122);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(97, 14);
            this.label10.TabIndex = 78;
            this.label10.Text = "补单附加点数";
            // 
            // nUD_IF1
            // 
            this.nUD_IF1.DecimalPlaces = 1;
            this.nUD_IF1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nUD_IF1.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nUD_IF1.Location = new System.Drawing.Point(320, 81);
            this.nUD_IF1.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nUD_IF1.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nUD_IF1.Name = "nUD_IF1";
            this.nUD_IF1.Size = new System.Drawing.Size(70, 26);
            this.nUD_IF1.TabIndex = 39;
            this.nUD_IF1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(4, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(97, 14);
            this.label4.TabIndex = 61;
            this.label4.Text = "每次下单数量";
            // 
            // nUD_IF2
            // 
            this.nUD_IF2.DecimalPlaces = 1;
            this.nUD_IF2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nUD_IF2.Increment = new decimal(new int[] {
            2,
            0,
            0,
            65536});
            this.nUD_IF2.Location = new System.Drawing.Point(549, 80);
            this.nUD_IF2.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.nUD_IF2.Minimum = new decimal(new int[] {
            99999,
            0,
            0,
            -2147483648});
            this.nUD_IF2.Name = "nUD_IF2";
            this.nUD_IF2.Size = new System.Drawing.Size(70, 26);
            this.nUD_IF2.TabIndex = 36;
            this.nUD_IF2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(455, 85);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(91, 14);
            this.label18.TabIndex = 35;
            this.label18.Text = "IF2附加点数";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(224, 85);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(91, 14);
            this.label17.TabIndex = 34;
            this.label17.Text = "IF1附加点数";
            // 
            // btn_Stop
            // 
            this.btn_Stop.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Stop.Location = new System.Drawing.Point(604, 385);
            this.btn_Stop.Name = "btn_Stop";
            this.btn_Stop.Size = new System.Drawing.Size(100, 46);
            this.btn_Stop.TabIndex = 34;
            this.btn_Stop.Text = "停止";
            this.btn_Stop.UseVisualStyleBackColor = true;
            this.btn_Stop.Click += new System.EventHandler(this.btn_Stop_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 9F);
            this.label19.Location = new System.Drawing.Point(4, 729);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(53, 12);
            this.label19.TabIndex = 36;
            this.label19.Text = "交易日志";
            // 
            // listBox_Log
            // 
            this.listBox_Log.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listBox_Log.FormattingEnabled = true;
            this.listBox_Log.HorizontalScrollbar = true;
            this.listBox_Log.Location = new System.Drawing.Point(3, 657);
            this.listBox_Log.Name = "listBox_Log";
            this.listBox_Log.ScrollAlwaysVisible = true;
            this.listBox_Log.Size = new System.Drawing.Size(701, 160);
            this.listBox_Log.TabIndex = 37;
            // 
            // btn_Start
            // 
            this.btn_Start.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Start.Location = new System.Drawing.Point(493, 385);
            this.btn_Start.Name = "btn_Start";
            this.btn_Start.Size = new System.Drawing.Size(102, 46);
            this.btn_Start.TabIndex = 38;
            this.btn_Start.Text = "开始";
            this.btn_Start.UseVisualStyleBackColor = true;
            this.btn_Start.Click += new System.EventHandler(this.btn_Start_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.LightSeaGreen;
            this.groupBox1.Controls.Add(this.tB_MaxWarnTimes);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.tB_EPrice);
            this.groupBox1.Controls.Add(this.label23);
            this.groupBox1.Controls.Add(this.tB_MaxOpenN);
            this.groupBox1.Controls.Add(this.tB_QueryWaitTime);
            this.groupBox1.Controls.Add(this.label22);
            this.groupBox1.Controls.Add(this.label12);
            this.groupBox1.Controls.Add(this.tB_ESpread);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.tB_MaxTN);
            this.groupBox1.Location = new System.Drawing.Point(5, 59);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(705, 96);
            this.groupBox1.TabIndex = 39;
            this.groupBox1.TabStop = false;
            // 
            // tB_MaxWarnTimes
            // 
            this.tB_MaxWarnTimes.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tB_MaxWarnTimes.Location = new System.Drawing.Point(548, 60);
            this.tB_MaxWarnTimes.Name = "tB_MaxWarnTimes";
            this.tB_MaxWarnTimes.Size = new System.Drawing.Size(70, 26);
            this.tB_MaxWarnTimes.TabIndex = 90;
            this.tB_MaxWarnTimes.Text = "3";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(415, 64);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(127, 14);
            this.label20.TabIndex = 89;
            this.label20.Text = "错误提醒次数上限";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(18, 64);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(82, 14);
            this.label14.TabIndex = 88;
            this.label14.Text = "异常最新价";
            // 
            // tB_EPrice
            // 
            this.tB_EPrice.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tB_EPrice.Location = new System.Drawing.Point(106, 58);
            this.tB_EPrice.Name = "tB_EPrice";
            this.tB_EPrice.Size = new System.Drawing.Size(70, 26);
            this.tB_EPrice.TabIndex = 87;
            this.tB_EPrice.Text = "6";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label23.Location = new System.Drawing.Point(187, 64);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(127, 14);
            this.label23.TabIndex = 86;
            this.label23.Text = "累计开仓数量上限";
            // 
            // tB_MaxOpenN
            // 
            this.tB_MaxOpenN.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tB_MaxOpenN.Location = new System.Drawing.Point(319, 60);
            this.tB_MaxOpenN.Name = "tB_MaxOpenN";
            this.tB_MaxOpenN.Size = new System.Drawing.Size(70, 26);
            this.tB_MaxOpenN.TabIndex = 85;
            this.tB_MaxOpenN.Text = "888";
            // 
            // tB_QueryWaitTime
            // 
            this.tB_QueryWaitTime.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tB_QueryWaitTime.Location = new System.Drawing.Point(548, 22);
            this.tB_QueryWaitTime.Name = "tB_QueryWaitTime";
            this.tB_QueryWaitTime.Size = new System.Drawing.Size(70, 26);
            this.tB_QueryWaitTime.TabIndex = 84;
            this.tB_QueryWaitTime.Text = "508";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(429, 26);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(113, 14);
            this.label22.TabIndex = 83;
            this.label22.Text = "下单等待(毫秒)";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(7, 26);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(97, 14);
            this.label12.TabIndex = 82;
            this.label12.Text = "异常行情价差";
            // 
            // tB_ESpread
            // 
            this.tB_ESpread.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tB_ESpread.Location = new System.Drawing.Point(106, 22);
            this.tB_ESpread.Name = "tB_ESpread";
            this.tB_ESpread.Size = new System.Drawing.Size(70, 26);
            this.tB_ESpread.TabIndex = 81;
            this.tB_ESpread.Text = "6";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(187, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(127, 14);
            this.label9.TabIndex = 77;
            this.label9.Text = "累计成交数量上限";
            // 
            // tB_MaxTN
            // 
            this.tB_MaxTN.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tB_MaxTN.Location = new System.Drawing.Point(319, 22);
            this.tB_MaxTN.Name = "tB_MaxTN";
            this.tB_MaxTN.Size = new System.Drawing.Size(70, 26);
            this.tB_MaxTN.TabIndex = 76;
            this.tB_MaxTN.Text = "888";
            // 
            // dGV_Detail
            // 
            this.dGV_Detail.AllowUserToAddRows = false;
            this.dGV_Detail.AllowUserToDeleteRows = false;
            this.dGV_Detail.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGV_Detail.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dGV_Detail.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGV_Detail.DefaultCellStyle = dataGridViewCellStyle5;
            this.dGV_Detail.Location = new System.Drawing.Point(6, 447);
            this.dGV_Detail.Name = "dGV_Detail";
            this.dGV_Detail.ReadOnly = true;
            this.dGV_Detail.RowTemplate.Height = 23;
            this.dGV_Detail.Size = new System.Drawing.Size(698, 121);
            this.dGV_Detail.TabIndex = 40;
            this.dGV_Detail.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dGV_Detail_CellFormatting);
            // 
            // dGV_Summary
            // 
            this.dGV_Summary.AllowUserToAddRows = false;
            this.dGV_Summary.AllowUserToDeleteRows = false;
            this.dGV_Summary.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dGV_Summary.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dGV_Summary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dGV_Summary.DefaultCellStyle = dataGridViewCellStyle6;
            this.dGV_Summary.Location = new System.Drawing.Point(6, 571);
            this.dGV_Summary.Name = "dGV_Summary";
            this.dGV_Summary.ReadOnly = true;
            this.dGV_Summary.RowTemplate.Height = 23;
            this.dGV_Summary.Size = new System.Drawing.Size(698, 64);
            this.dGV_Summary.TabIndex = 41;
            this.dGV_Summary.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dGV_Summary_CellFormatting);
            // 
            // lb_CloseSpread
            // 
            this.lb_CloseSpread.AutoSize = true;
            this.lb_CloseSpread.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_CloseSpread.Location = new System.Drawing.Point(399, 419);
            this.lb_CloseSpread.Name = "lb_CloseSpread";
            this.lb_CloseSpread.Size = new System.Drawing.Size(35, 16);
            this.lb_CloseSpread.TabIndex = 95;
            this.lb_CloseSpread.Text = "N/A";
            // 
            // lb_OpenSpread
            // 
            this.lb_OpenSpread.AutoSize = true;
            this.lb_OpenSpread.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_OpenSpread.Location = new System.Drawing.Point(399, 377);
            this.lb_OpenSpread.Name = "lb_OpenSpread";
            this.lb_OpenSpread.Size = new System.Drawing.Size(35, 16);
            this.lb_OpenSpread.TabIndex = 94;
            this.lb_OpenSpread.Text = "N/A";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label5.Location = new System.Drawing.Point(339, 398);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(45, 14);
            this.label5.TabIndex = 93;
            this.label5.Text = "价差:";
            // 
            // lb_Diff1
            // 
            this.lb_Diff1.AutoSize = true;
            this.lb_Diff1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Diff1.Location = new System.Drawing.Point(399, 398);
            this.lb_Diff1.Name = "lb_Diff1";
            this.lb_Diff1.Size = new System.Drawing.Size(35, 16);
            this.lb_Diff1.TabIndex = 92;
            this.lb_Diff1.Text = "N/A";
            // 
            // lb_Time1
            // 
            this.lb_Time1.AutoSize = true;
            this.lb_Time1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Time1.Location = new System.Drawing.Point(97, 394);
            this.lb_Time1.Name = "lb_Time1";
            this.lb_Time1.Size = new System.Drawing.Size(35, 16);
            this.lb_Time1.TabIndex = 91;
            this.lb_Time1.Text = "N/A";
            // 
            // lb_IF2Price
            // 
            this.lb_IF2Price.AutoSize = true;
            this.lb_IF2Price.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_IF2Price.Location = new System.Drawing.Point(236, 418);
            this.lb_IF2Price.Name = "lb_IF2Price";
            this.lb_IF2Price.Size = new System.Drawing.Size(35, 16);
            this.lb_IF2Price.TabIndex = 89;
            this.lb_IF2Price.Text = "N/A";
            // 
            // lb_IF1Price
            // 
            this.lb_IF1Price.AutoSize = true;
            this.lb_IF1Price.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_IF1Price.Location = new System.Drawing.Point(236, 393);
            this.lb_IF1Price.Name = "lb_IF1Price";
            this.lb_IF1Price.Size = new System.Drawing.Size(35, 16);
            this.lb_IF1Price.TabIndex = 87;
            this.lb_IF1Price.Text = "N/A";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 9F);
            this.label13.Location = new System.Drawing.Point(13, 642);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(53, 12);
            this.label13.TabIndex = 96;
            this.label13.Text = "交易日志";
            // 
            // lb_IF1
            // 
            this.lb_IF1.AutoSize = true;
            this.lb_IF1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_IF1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lb_IF1.Location = new System.Drawing.Point(23, 395);
            this.lb_IF1.Name = "lb_IF1";
            this.lb_IF1.Size = new System.Drawing.Size(39, 14);
            this.lb_IF1.TabIndex = 97;
            this.lb_IF1.Text = "IF1:";
            // 
            // lb_IF2
            // 
            this.lb_IF2.AutoSize = true;
            this.lb_IF2.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_IF2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lb_IF2.Location = new System.Drawing.Point(23, 418);
            this.lb_IF2.Name = "lb_IF2";
            this.lb_IF2.Size = new System.Drawing.Size(39, 14);
            this.lb_IF2.TabIndex = 98;
            this.lb_IF2.Text = "IF2:";
            // 
            // lb_Time2
            // 
            this.lb_Time2.AutoSize = true;
            this.lb_Time2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lb_Time2.Location = new System.Drawing.Point(97, 418);
            this.lb_Time2.Name = "lb_Time2";
            this.lb_Time2.Size = new System.Drawing.Size(35, 16);
            this.lb_Time2.TabIndex = 100;
            this.lb_Time2.Text = "N/A";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label21.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label21.Location = new System.Drawing.Point(107, 366);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(37, 14);
            this.label21.TabIndex = 101;
            this.label21.Text = "时间";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label28.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label28.Location = new System.Drawing.Point(236, 366);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(52, 14);
            this.label28.TabIndex = 102;
            this.label28.Text = "最新价";
            // 
            // rB_S
            // 
            this.rB_S.AutoSize = true;
            this.rB_S.Checked = true;
            this.rB_S.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rB_S.Location = new System.Drawing.Point(107, 20);
            this.rB_S.Name = "rB_S";
            this.rB_S.Size = new System.Drawing.Size(55, 18);
            this.rB_S.TabIndex = 103;
            this.rB_S.TabStop = true;
            this.rB_S.Text = "投机";
            this.rB_S.UseVisualStyleBackColor = true;
            // 
            // rB_A
            // 
            this.rB_A.AutoSize = true;
            this.rB_A.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rB_A.Location = new System.Drawing.Point(191, 20);
            this.rB_A.Name = "rB_A";
            this.rB_A.Size = new System.Drawing.Size(55, 18);
            this.rB_A.TabIndex = 104;
            this.rB_A.TabStop = true;
            this.rB_A.Text = "套利";
            this.rB_A.UseVisualStyleBackColor = true;
            // 
            // rB_H
            // 
            this.rB_H.AutoSize = true;
            this.rB_H.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rB_H.Location = new System.Drawing.Point(271, 20);
            this.rB_H.Name = "rB_H";
            this.rB_H.Size = new System.Drawing.Size(55, 18);
            this.rB_H.TabIndex = 105;
            this.rB_H.TabStop = true;
            this.rB_H.Text = "套保";
            this.rB_H.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.Color.LightSeaGreen;
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.rB_H);
            this.groupBox2.Controls.Add(this.rB_A);
            this.groupBox2.Controls.Add(this.rB_S);
            this.groupBox2.Location = new System.Drawing.Point(4, -5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(706, 58);
            this.groupBox2.TabIndex = 91;
            this.groupBox2.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(19, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 14);
            this.label1.TabIndex = 91;
            this.label1.Text = "下单类型";
            // 
            // FormAutoTrader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 821);
            this.Controls.Add(this.label28);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.lb_Time2);
            this.Controls.Add(this.lb_IF2);
            this.Controls.Add(this.lb_IF1);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.lb_CloseSpread);
            this.Controls.Add(this.lb_OpenSpread);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lb_Diff1);
            this.Controls.Add(this.lb_Time1);
            this.Controls.Add(this.lb_IF2Price);
            this.Controls.Add(this.lb_IF1Price);
            this.Controls.Add(this.dGV_Summary);
            this.Controls.Add(this.dGV_Detail);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_Start);
            this.Controls.Add(this.listBox_Log);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.btn_Stop);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FormAutoTrader";
            this.Text = "半自动化股指期货跨期下单";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormIFAutoTrader_FormClosing);
            this.Load += new System.EventHandler(this.FormIFAutoTrader_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nUD_HedgeVol)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_InitPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ReorderTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_MaxPosition)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_ReorderAppendPt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_IF1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nUD_IF2)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Detail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dGV_Summary)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        protected System.Windows.Forms.NumericUpDown nUD_HedgeVol;
        protected System.Windows.Forms.Label label15;
        protected System.Windows.Forms.ComboBox cB_IF1;
        protected System.Windows.Forms.Label label16;
        protected System.Windows.Forms.GroupBox groupBox3;
        protected System.Windows.Forms.Label label17;
        protected System.Windows.Forms.Label label18;
        protected System.Windows.Forms.NumericUpDown nUD_IF2;
        protected System.Windows.Forms.Button btn_Stop;
        protected System.Windows.Forms.Label label19;
        protected System.Windows.Forms.ListBox listBox_Log;
        protected System.Windows.Forms.Button btn_Start;
        protected System.Windows.Forms.Label label4;
        protected System.Windows.Forms.NumericUpDown nUD_IF1;
        protected System.Windows.Forms.Label label6;
        protected System.Windows.Forms.Label label7;
        protected System.Windows.Forms.TextBox tB_CloseSpread;
        protected System.Windows.Forms.GroupBox groupBox1;
        protected System.Windows.Forms.Label label8;
        protected System.Windows.Forms.Label label9;
        protected System.Windows.Forms.TextBox tB_MaxTN;
        protected System.Windows.Forms.Label label10;
        protected System.Windows.Forms.Label label11;
        protected System.Windows.Forms.NumericUpDown nUD_ReorderAppendPt;
        protected System.Windows.Forms.Label label12;
        protected System.Windows.Forms.TextBox tB_ESpread;
        protected System.Windows.Forms.DataGridView dGV_Detail;
        protected System.Windows.Forms.DataGridView dGV_Summary;
        protected System.Windows.Forms.NumericUpDown nUD_MaxPosition;
        protected System.Windows.Forms.ComboBox cB_IF2;
        protected System.Windows.Forms.TextBox tB_OpenSpread;
        protected System.Windows.Forms.Label lb_CloseSpread;
        protected System.Windows.Forms.Label lb_OpenSpread;
        protected System.Windows.Forms.Label label5;
        protected System.Windows.Forms.Label lb_Diff1;
        protected System.Windows.Forms.Label lb_Time1;
        protected System.Windows.Forms.Label lb_IF2Price;
        protected System.Windows.Forms.Label lb_IF1Price;
        protected System.Windows.Forms.Label label13;
        protected System.Windows.Forms.NumericUpDown nUD_ReorderTimes;
        protected System.Windows.Forms.Label lb_IF1;
        protected System.Windows.Forms.Label lb_IF2;
        protected System.Windows.Forms.Label label22;
        protected System.Windows.Forms.TextBox tB_QueryWaitTime;
        protected System.Windows.Forms.Label label23;
        protected System.Windows.Forms.TextBox tB_MaxOpenN;
        protected System.Windows.Forms.Label label24;
        protected System.Windows.Forms.NumericUpDown nUD_InitPosition;
        protected System.Windows.Forms.Label label25;
        protected System.Windows.Forms.ComboBox cB_IF2PriceLevel;
        protected System.Windows.Forms.Label label26;
        protected System.Windows.Forms.ComboBox cB_IF1PriceLevel;
        protected System.Windows.Forms.Label label21;
        protected System.Windows.Forms.Label label28;
        protected System.Windows.Forms.Label lb_Time2;
        protected System.Windows.Forms.Label label14;
        protected System.Windows.Forms.TextBox tB_EPrice;
        protected System.Windows.Forms.TextBox tB_MaxWarnTimes;
        protected System.Windows.Forms.Label label20;
        private System.Windows.Forms.RadioButton rB_S;
        private System.Windows.Forms.RadioButton rB_A;
        private System.Windows.Forms.RadioButton rB_H;
        protected System.Windows.Forms.GroupBox groupBox2;
        protected System.Windows.Forms.Label label1;
    }
}

