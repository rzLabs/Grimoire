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
            this.list = new System.Windows.Forms.ListBox();
            this.yesBtn = new System.Windows.Forms.Button();
            this.noBtn = new System.Windows.Forms.Button();
            this.msg_grpBx = new System.Windows.Forms.GroupBox();
            this.msg = new System.Windows.Forms.TextBox();
            this.msg_grpBx.SuspendLayout();
            this.SuspendLayout();
            // 
            // list
            // 
            this.list.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.list.FormattingEnabled = true;
            this.list.Location = new System.Drawing.Point(9, 88);
            this.list.Margin = new System.Windows.Forms.Padding(2);
            this.list.Name = "list";
            this.list.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.list.Size = new System.Drawing.Size(368, 199);
            this.list.TabIndex = 0;
            // 
            // yesBtn
            // 
            this.yesBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.yesBtn.Location = new System.Drawing.Point(286, 299);
            this.yesBtn.Margin = new System.Windows.Forms.Padding(2);
            this.yesBtn.Name = "yesBtn";
            this.yesBtn.Size = new System.Drawing.Size(43, 25);
            this.yesBtn.TabIndex = 1;
            this.yesBtn.Text = "Yes";
            this.yesBtn.UseVisualStyleBackColor = true;
            this.yesBtn.Click += new System.EventHandler(this.yes_btn_Click);
            // 
            // noBtn
            // 
            this.noBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.noBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.noBtn.Location = new System.Drawing.Point(333, 299);
            this.noBtn.Margin = new System.Windows.Forms.Padding(2);
            this.noBtn.Name = "noBtn";
            this.noBtn.Size = new System.Drawing.Size(43, 25);
            this.noBtn.TabIndex = 2;
            this.noBtn.Text = "No";
            this.noBtn.UseVisualStyleBackColor = true;
            this.noBtn.Click += new System.EventHandler(this.no_btn_Click);
            // 
            // msg_grpBx
            // 
            this.msg_grpBx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msg_grpBx.Controls.Add(this.msg);
            this.msg_grpBx.Location = new System.Drawing.Point(9, 3);
            this.msg_grpBx.Margin = new System.Windows.Forms.Padding(2);
            this.msg_grpBx.Name = "msg_grpBx";
            this.msg_grpBx.Padding = new System.Windows.Forms.Padding(2);
            this.msg_grpBx.Size = new System.Drawing.Size(367, 81);
            this.msg_grpBx.TabIndex = 3;
            this.msg_grpBx.TabStop = false;
            this.msg_grpBx.Text = "Message";
            // 
            // msg
            // 
            this.msg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.msg.Location = new System.Drawing.Point(4, 17);
            this.msg.Margin = new System.Windows.Forms.Padding(2);
            this.msg.Multiline = true;
            this.msg.Name = "msg";
            this.msg.ReadOnly = true;
            this.msg.Size = new System.Drawing.Size(358, 51);
            this.msg.TabIndex = 0;
            this.msg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // MessageListBox
            // 
            this.AcceptButton = this.yesBtn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.noBtn;
            this.ClientSize = new System.Drawing.Size(386, 335);
            this.Controls.Add(this.msg_grpBx);
            this.Controls.Add(this.noBtn);
            this.Controls.Add(this.yesBtn);
            this.Controls.Add(this.list);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MessageListBox";
            this.msg_grpBx.ResumeLayout(false);
            this.msg_grpBx.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox list;
        private System.Windows.Forms.Button yesBtn;
        private System.Windows.Forms.Button noBtn;
        private System.Windows.Forms.GroupBox msg_grpBx;
        private System.Windows.Forms.TextBox msg;
    }
}