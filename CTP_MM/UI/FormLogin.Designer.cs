namespace CTP_MM.UI
{
    partial class FormLogin
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
            this.label1 = new System.Windows.Forms.Label();
            this.tB_User = new System.Windows.Forms.TextBox();
            this.tB_Pass = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btn_Login = new System.Windows.Forms.Button();
            this.btn_Logout = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.cB_ServerConfig = new System.Windows.Forms.ComboBox();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(54, 94);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户代码";
            // 
            // tB_User
            // 
            this.tB_User.BackColor = System.Drawing.SystemColors.Info;
            this.tB_User.Location = new System.Drawing.Point(134, 91);
            this.tB_User.Name = "tB_User";
            this.tB_User.Size = new System.Drawing.Size(184, 21);
            this.tB_User.TabIndex = 1;
            // 
            // tB_Pass
            // 
            this.tB_Pass.BackColor = System.Drawing.SystemColors.Info;
            this.tB_Pass.Location = new System.Drawing.Point(134, 136);
            this.tB_Pass.Name = "tB_Pass";
            this.tB_Pass.Size = new System.Drawing.Size(184, 21);
            this.tB_Pass.TabIndex = 3;
            this.tB_Pass.UseSystemPasswordChar = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "交易密码";
            // 
            // btn_Login
            // 
            this.btn_Login.Location = new System.Drawing.Point(107, 187);
            this.btn_Login.Name = "btn_Login";
            this.btn_Login.Size = new System.Drawing.Size(75, 23);
            this.btn_Login.TabIndex = 4;
            this.btn_Login.Text = "登录";
            this.btn_Login.UseVisualStyleBackColor = true;
            this.btn_Login.Click += new System.EventHandler(this.btn_Login_Click);
            // 
            // btn_Logout
            // 
            this.btn_Logout.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Logout.Location = new System.Drawing.Point(243, 187);
            this.btn_Logout.Name = "btn_Logout";
            this.btn_Logout.Size = new System.Drawing.Size(75, 23);
            this.btn_Logout.TabIndex = 5;
            this.btn_Logout.Text = "退出";
            this.btn_Logout.UseVisualStyleBackColor = true;
            this.btn_Logout.Click += new System.EventHandler(this.btn_Logout_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 42);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "选择服务器";
            // 
            // cB_ServerConfig
            // 
            this.cB_ServerConfig.BackColor = System.Drawing.SystemColors.Info;
            this.cB_ServerConfig.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_ServerConfig.FormattingEnabled = true;
            this.cB_ServerConfig.Location = new System.Drawing.Point(134, 39);
            this.cB_ServerConfig.Name = "cB_ServerConfig";
            this.cB_ServerConfig.Size = new System.Drawing.Size(184, 20);
            this.cB_ServerConfig.TabIndex = 8;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip.Location = new System.Drawing.Point(0, 251);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(402, 22);
            this.statusStrip.TabIndex = 9;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel1.Text = "未登录";
            // 
            // FormLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(402, 273);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.cB_ServerConfig);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_Logout);
            this.Controls.Add(this.btn_Login);
            this.Controls.Add(this.tB_Pass);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tB_User);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "FormLogin";
            this.Text = "登录";
            this.Load += new System.EventHandler(this.FormLogin_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tB_User;
        private System.Windows.Forms.TextBox tB_Pass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btn_Login;
        private System.Windows.Forms.Button btn_Logout;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cB_ServerConfig;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
    }
}