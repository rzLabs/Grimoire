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
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.db_pnl = new System.Windows.Forms.Panel();
            this.eh = new System.Windows.Forms.Label();
            this.info_grp_box.SuspendLayout();
            this.paths_pnl.SuspendLayout();
            this.db_pnl.SuspendLayout();
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "RDB Structures:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(129, 22);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(376, 22);
            this.textBox1.TabIndex = 1;
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
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(129, 54);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(376, 22);
            this.textBox2.TabIndex = 3;
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
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(129, 88);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(376, 22);
            this.textBox3.TabIndex = 5;
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
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(129, 122);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(376, 22);
            this.textBox4.TabIndex = 7;
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
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(522, 56);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(35, 23);
            this.button2.TabIndex = 9;
            this.button2.Text = "...";
            this.button2.UseVisualStyleBackColor = true;
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
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(522, 122);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(35, 23);
            this.button4.TabIndex = 11;
            this.button4.Text = "...";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // db_pnl
            // 
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
            this.eh.Location = new System.Drawing.Point(193, 95);
            this.eh.Name = "eh";
            this.eh.Size = new System.Drawing.Size(27, 17);
            this.eh.TabIndex = 0;
            this.eh.Text = "EH";
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
    }
}