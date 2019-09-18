namespace Grimoire.GUI
{
    partial class BitFlag
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
            this.flagFiles = new System.Windows.Forms.ComboBox();
            this.flagList = new System.Windows.Forms.ListBox();
            this.flagIO = new System.Windows.Forms.TextBox();
            this.clear_on_change_chkBx = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // flagFiles
            // 
            this.flagFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flagFiles.FormattingEnabled = true;
            this.flagFiles.Location = new System.Drawing.Point(12, 10);
            this.flagFiles.Name = "flagFiles";
            this.flagFiles.Size = new System.Drawing.Size(121, 21);
            this.flagFiles.TabIndex = 8;
            this.flagFiles.SelectedIndexChanged += new System.EventHandler(this.flagFiles_SelectedIndexChanged);
            // 
            // flagList
            // 
            this.flagList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagList.FormattingEnabled = true;
            this.flagList.ItemHeight = 15;
            this.flagList.Location = new System.Drawing.Point(12, 37);
            this.flagList.Name = "flagList";
            this.flagList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.flagList.Size = new System.Drawing.Size(652, 349);
            this.flagList.TabIndex = 7;
            this.flagList.SelectedValueChanged += new System.EventHandler(this.flagList_SelectedValueChanged);
            // 
            // flagIO
            // 
            this.flagIO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagIO.Location = new System.Drawing.Point(149, 405);
            this.flagIO.Name = "flagIO";
            this.flagIO.Size = new System.Drawing.Size(363, 20);
            this.flagIO.TabIndex = 6;
            this.flagIO.Text = "0";
            this.flagIO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.flagIO.TextChanged += new System.EventHandler(this.flagIO_TextChanged);
            // 
            // clear_on_change_chkBx
            // 
            this.clear_on_change_chkBx.AutoSize = true;
            this.clear_on_change_chkBx.Location = new System.Drawing.Point(545, 14);
            this.clear_on_change_chkBx.Name = "clear_on_change_chkBx";
            this.clear_on_change_chkBx.Size = new System.Drawing.Size(119, 17);
            this.clear_on_change_chkBx.TabIndex = 9;
            this.clear_on_change_chkBx.Text = "Clear on list change";
            this.clear_on_change_chkBx.UseVisualStyleBackColor = true;
            this.clear_on_change_chkBx.CheckedChanged += new System.EventHandler(this.clear_on_list_change_CheckedChanged);
            // 
            // BitFlag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(676, 450);
            this.Controls.Add(this.clear_on_change_chkBx);
            this.Controls.Add(this.flagFiles);
            this.Controls.Add(this.flagList);
            this.Controls.Add(this.flagIO);
            this.Name = "BitFlag";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "BitFlag Editor";
            this.Load += new System.EventHandler(this.BitFlag_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox flagFiles;
        private System.Windows.Forms.ListBox flagList;
        private System.Windows.Forms.TextBox flagIO;
        private System.Windows.Forms.CheckBox clear_on_change_chkBx;
    }
}