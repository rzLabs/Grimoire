namespace Grimoire.GUI
{
    partial class Setup
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
            this.info_grp_box = new System.Windows.Forms.GroupBox();
            this.info = new System.Windows.Forms.RichTextBox();
            this.next_btn = new System.Windows.Forms.Button();
            this.back_btn = new System.Windows.Forms.Button();
            this.paths_pnl = new System.Windows.Forms.Panel();
            this.db_pnl = new System.Windows.Forms.Panel();
            this.eh = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dbPort = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.dbUser = new System.Windows.Forms.TextBox();
            this.dbIP = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dbPass = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.trusted_chkBx = new System.Windows.Forms.CheckBox();
            this.conPreview = new System.Windows.Forms.RichTextBox();
            this.testCon_btn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.info_grp_box.SuspendLayout();
            this.paths_pnl.SuspendLayout();
            this.db_pnl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            this.SuspendLayout();
            // 
            // info_grp_box
            // 
            this.info_grp_box.Controls.Add(this.info);
            this.info_grp_box.Location = new System.Drawing.Point(10, 12);
            this.info_grp_box.Name = "info_grp_box";
            this.info_grp_box.Size = new System.Drawing.Size(580, 160);
            this.info_grp_box.TabIndex = 1;
            this.info_grp_box.TabStop = false;
            this.info_grp_box.Text = "Info";
            // 
            // info
            // 
            this.info.Dock = System.Windows.Forms.DockStyle.Fill;
            this.info.Location = new System.Drawing.Point(3, 18);
            this.info.Name = "info";
            this.info.ReadOnly = true;
            this.info.Size = new System.Drawing.Size(574, 139);
            this.info.TabIndex = 0;
            this.info.Text = "";
            // 
            // next_btn
            // 
            this.next_btn.Location = new System.Drawing.Point(515, 409);
            this.next_btn.Name = "next_btn";
            this.next_btn.Size = new System.Drawing.Size(75, 23);
            this.next_btn.TabIndex = 2;
            this.next_btn.Text = "Next";
            this.next_btn.UseVisualStyleBackColor = true;
            this.next_btn.Click += new System.EventHandler(this.next_btn_Click);
            // 
            // back_btn
            // 
            this.back_btn.Location = new System.Drawing.Point(434, 409);
            this.back_btn.Name = "back_btn";
            this.back_btn.Size = new System.Drawing.Size(75, 23);
            this.back_btn.TabIndex = 3;
            this.back_btn.Text = "Back";
            this.back_btn.UseVisualStyleBackColor = true;
            this.back_btn.Click += new System.EventHandler(this.back_btn_Click);
            // 
            // paths_pnl
            // 
            this.paths_pnl.Controls.Add(this.db_pnl);
            this.paths_pnl.Controls.Add(this.button4);
            this.paths_pnl.Controls.Add(this.button3);
            this.paths_pnl.Controls.Add(this.button2);
            this.paths_pnl.Controls.Add(this.button1);
            this.paths_pnl.Controls.Add(this.textBox4);
            this.paths_pnl.Controls.Add(this.label4);
            this.paths_pnl.Controls.Add(this.textBox3);
            this.paths_pnl.Controls.Add(this.label3);
            this.paths_pnl.Controls.Add(this.textBox2);
            this.paths_pnl.Controls.Add(this.label2);
            this.paths_pnl.Controls.Add(this.textBox1);
            this.paths_pnl.Controls.Add(this.label1);
            this.paths_pnl.Location = new System.Drawing.Point(12, 178);
            this.paths_pnl.Name = "paths_pnl";
            this.paths_pnl.Size = new System.Drawing.Size(578, 225);
            this.paths_pnl.TabIndex = 4;
            // 
            // db_pnl
            // 
            this.db_pnl.Controls.Add(this.checkBox2);
            this.db_pnl.Controls.Add(this.checkBox1);
            this.db_pnl.Controls.Add(this.label9);
            this.db_pnl.Controls.Add(this.testCon_btn);
            this.db_pnl.Controls.Add(this.conPreview);
            this.db_pnl.Controls.Add(this.trusted_chkBx);
            this.db_pnl.Controls.Add(this.numericUpDown2);
            this.db_pnl.Controls.Add(this.label8);
            this.db_pnl.Controls.Add(this.dbPass);
            this.db_pnl.Controls.Add(this.label7);
            this.db_pnl.Controls.Add(this.dbIP);
            this.db_pnl.Controls.Add(this.dbUser);
            this.db_pnl.Controls.Add(this.label6);
            this.db_pnl.Controls.Add(this.dbPort);
            this.db_pnl.Controls.Add(this.label5);
            this.db_pnl.Controls.Add(this.eh);
            this.db_pnl.Location = new System.Drawing.Point(0, 0);
            this.db_pnl.Name = "db_pnl";
            this.db_pnl.Size = new System.Drawing.Size(578, 225);
            this.db_pnl.TabIndex = 12;
            this.db_pnl.Visible = false;
            // 
            // eh
            // 
            this.eh.AutoSize = true;
            this.eh.Location = new System.Drawing.Point(13, 19);
            this.eh.Name = "eh";
            this.eh.Size = new System.Drawing.Size(24, 17);
            this.eh.TabIndex = 0;
            this.eh.Text = "IP:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(522, 122);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(35, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(522, 88);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(35, 23);
            this.button3.TabIndex = 10;
            this.button3.Text = "...";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(522, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(522, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(129, 122);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(376, 22);
            this.textBox4.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 125);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Flags Path:";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(129, 88);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(376, 22);
            this.textBox3.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Build Directory:";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(129, 54);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(376, 22);
            this.textBox2.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Data Directory:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(129, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(376, 22);
            this.textBox1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "RDB Structures:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(258, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(38, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "Port:";
            // 
            // dbPort
            // 
            this.dbPort.Location = new System.Drawing.Point(302, 17);
            this.dbPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.dbPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.dbPort.Name = "dbPort";
            this.dbPort.Size = new System.Drawing.Size(90, 22);
            this.dbPort.TabIndex = 3;
            this.dbPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dbPort.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 17);
            this.label6.TabIndex = 4;
            this.label6.Text = "User:";
            // 
            // dbUser
            // 
            this.dbUser.Location = new System.Drawing.Point(62, 57);
            this.dbUser.Name = "dbUser";
            this.dbUser.Size = new System.Drawing.Size(180, 22);
            this.dbUser.TabIndex = 5;
            // 
            // dbIP
            // 
            this.dbIP.FormattingEnabled = true;
            this.dbIP.Items.AddRange(new object[] {
            "127.0.0.1",
            "(local)"});
            this.dbIP.Location = new System.Drawing.Point(62, 16);
            this.dbIP.Name = "dbIP";
            this.dbIP.Size = new System.Drawing.Size(180, 24);
            this.dbIP.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 99);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(43, 17);
            this.label7.TabIndex = 7;
            this.label7.Text = "Pass:";
            // 
            // dbPass
            // 
            this.dbPass.Location = new System.Drawing.Point(63, 96);
            this.dbPass.Name = "dbPass";
            this.dbPass.Size = new System.Drawing.Size(180, 22);
            this.dbPass.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(410, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 17);
            this.label8.TabIndex = 9;
            this.label8.Text = "Timeout:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(479, 17);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(78, 22);
            this.numericUpDown2.TabIndex = 10;
            this.numericUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // trusted_chkBx
            // 
            this.trusted_chkBx.AutoSize = true;
            this.trusted_chkBx.Location = new System.Drawing.Point(16, 145);
            this.trusted_chkBx.Name = "trusted_chkBx";
            this.trusted_chkBx.Size = new System.Drawing.Size(154, 21);
            this.trusted_chkBx.TabIndex = 11;
            this.trusted_chkBx.Text = "Trusted Connection";
            this.trusted_chkBx.UseVisualStyleBackColor = true;
            // 
            // conPreview
            // 
            this.conPreview.Location = new System.Drawing.Point(261, 77);
            this.conPreview.Name = "conPreview";
            this.conPreview.ReadOnly = true;
            this.conPreview.Size = new System.Drawing.Size(296, 81);
            this.conPreview.TabIndex = 12;
            this.conPreview.Text = "";
            // 
            // testCon_btn
            // 
            this.testCon_btn.Location = new System.Drawing.Point(482, 180);
            this.testCon_btn.Name = "testCon_btn";
            this.testCon_btn.Size = new System.Drawing.Size(75, 23);
            this.testCon_btn.TabIndex = 13;
            this.testCon_btn.Text = "Test";
            this.testCon_btn.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(258, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(124, 17);
            this.label9.TabIndex = 14;
            this.label9.Text = "Connection String:";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(17, 181);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(199, 21);
            this.checkBox1.TabIndex = 15;
            this.checkBox1.Text = "Drop and Create (on save)";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(222, 182);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(124, 21);
            this.checkBox2.TabIndex = 16;
            this.checkBox2.Text = "Backup Tables";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // Setup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(604, 443);
            this.Controls.Add(this.paths_pnl);
            this.Controls.Add(this.back_btn);
            this.Controls.Add(this.next_btn);
            this.Controls.Add(this.info_grp_box);
            this.Name = "Setup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setup";
            this.Load += new System.EventHandler(this.Setup_Load);
            this.Shown += new System.EventHandler(this.Setup_Shown);
            this.info_grp_box.ResumeLayout(false);
            this.paths_pnl.ResumeLayout(false);
            this.paths_pnl.PerformLayout();
            this.db_pnl.ResumeLayout(false);
            this.db_pnl.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox info_grp_box;
        private System.Windows.Forms.RichTextBox info;
        private System.Windows.Forms.Button next_btn;
        private System.Windows.Forms.Button back_btn;
        private System.Windows.Forms.Panel paths_pnl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel db_pnl;
        private System.Windows.Forms.Label eh;
        private System.Windows.Forms.Button testCon_btn;
        private System.Windows.Forms.RichTextBox conPreview;
        private System.Windows.Forms.CheckBox trusted_chkBx;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox dbPass;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox dbIP;
        private System.Windows.Forms.TextBox dbUser;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown dbPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
    }
}