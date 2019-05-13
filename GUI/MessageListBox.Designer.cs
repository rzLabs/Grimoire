namespace Grimoire.GUI
{
    partial class MessageListBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MessageListBox));
            this.list = new System.Windows.Forms.ListBox();
            this.yes_btn = new System.Windows.Forms.Button();
            this.no_btn = new System.Windows.Forms.Button();
            this.msgGrp = new System.Windows.Forms.GroupBox();
            this.msg = new System.Windows.Forms.TextBox();
            this.msgGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list.FormattingEnabled = true;
            this.list.ItemHeight = 16;
            this.list.Location = new System.Drawing.Point(12, 108);
            this.list.Name = "list";
            this.list.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.list.Size = new System.Drawing.Size(489, 244);
            this.list.TabIndex = 0;
            // 
            // yes_btn
            // 
            this.yes_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yes_btn.Location = new System.Drawing.Point(381, 368);
            this.yes_btn.Name = "yes_btn";
            this.yes_btn.Size = new System.Drawing.Size(57, 31);
            this.yes_btn.TabIndex = 1;
            this.yes_btn.Text = "Yes";
            this.yes_btn.UseVisualStyleBackColor = true;
            this.yes_btn.Click += new System.EventHandler(this.yes_btn_Click);
            // 
            // no_btn
            // 
            this.no_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.no_btn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.no_btn.Location = new System.Drawing.Point(444, 368);
            this.no_btn.Name = "no_btn";
            this.no_btn.Size = new System.Drawing.Size(57, 31);
            this.no_btn.TabIndex = 2;
            this.no_btn.Text = "No";
            this.no_btn.UseVisualStyleBackColor = true;
            this.no_btn.Click += new System.EventHandler(this.no_btn_Click);
            // 
            // msgGrp
            // 
            this.msgGrp.Controls.Add(this.msg);
            this.msgGrp.Location = new System.Drawing.Point(12, 2);
            this.msgGrp.Name = "msgGrp";
            this.msgGrp.Size = new System.Drawing.Size(489, 100);
            this.msgGrp.TabIndex = 3;
            this.msgGrp.TabStop = false;
            this.msgGrp.Text = "Message";
            // 
            // msg
            // 
            this.msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msg.Location = new System.Drawing.Point(6, 21);
            this.msg.Multiline = true;
            this.msg.Name = "msg";
            this.msg.ReadOnly = true;
            this.msg.Size = new System.Drawing.Size(477, 63);
            this.msg.TabIndex = 0;
            this.msg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MessageListBox
            // 
            this.AcceptButton = this.yes_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.no_btn;
            this.ClientSize = new System.Drawing.Size(514, 412);
            this.Controls.Add(this.msgGrp);
            this.Controls.Add(this.no_btn);
            this.Controls.Add(this.yes_btn);
            this.Controls.Add(this.list);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MessageListBox";
            this.msgGrp.ResumeLayout(false);
            this.msgGrp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.Button yes_btn;
        private System.Windows.Forms.Button no_btn;
        private System.Windows.Forms.GroupBox msgGrp;
        private System.Windows.Forms.TextBox msg;
    }
}