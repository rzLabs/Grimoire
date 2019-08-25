namespace Grimoire.Tabs.Styles
{
    partial class Flag
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.calculate = new System.Windows.Forms.Button();
            this.reverse = new System.Windows.Forms.Button();
            this.flagIO = new System.Windows.Forms.TextBox();
            this.flagList = new System.Windows.Forms.ListBox();
            this.flagFiles = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // calculate
            // 
            this.calculate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.calculate.Location = new System.Drawing.Point(14, 409);
            this.calculate.Name = "calculate";
            this.calculate.Size = new System.Drawing.Size(75, 23);
            this.calculate.TabIndex = 1;
            this.calculate.Text = "Calculate";
            this.calculate.UseVisualStyleBackColor = true;
            this.calculate.Click += new System.EventHandler(this.calculate_Click);
            // 
            // reverse
            // 
            this.reverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.reverse.Location = new System.Drawing.Point(659, 409);
            this.reverse.Name = "reverse";
            this.reverse.Size = new System.Drawing.Size(75, 23);
            this.reverse.TabIndex = 2;
            this.reverse.Text = "Reverse";
            this.reverse.UseVisualStyleBackColor = true;
            this.reverse.Click += new System.EventHandler(this.reverse_Click);
            // 
            // flagIO
            // 
            this.flagIO.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagIO.Location = new System.Drawing.Point(151, 411);
            this.flagIO.Name = "flagIO";
            this.flagIO.Size = new System.Drawing.Size(446, 20);
            this.flagIO.TabIndex = 3;
            this.flagIO.Text = "0";
            this.flagIO.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.flagIO.TextChanged += new System.EventHandler(this.flagIO_TextChanged);
            // 
            // flagList
            // 
            this.flagList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagList.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flagList.FormattingEnabled = true;
            this.flagList.ItemHeight = 15;
            this.flagList.Location = new System.Drawing.Point(14, 43);
            this.flagList.Name = "flagList";
            this.flagList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.flagList.Size = new System.Drawing.Size(720, 349);
            this.flagList.TabIndex = 4;
            // 
            // flagFiles
            // 
            this.flagFiles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flagFiles.FormattingEnabled = true;
            this.flagFiles.Location = new System.Drawing.Point(66, 16);
            this.flagFiles.Name = "flagFiles";
            this.flagFiles.Size = new System.Drawing.Size(121, 21);
            this.flagFiles.TabIndex = 5;
            this.flagFiles.SelectedIndexChanged += new System.EventHandler(this.flagFiles_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Flag List:";
            // 
            // Flag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.flagFiles);
            this.Controls.Add(this.flagList);
            this.Controls.Add(this.flagIO);
            this.Controls.Add(this.reverse);
            this.Controls.Add(this.calculate);
            this.Name = "Flag";
            this.Size = new System.Drawing.Size(753, 448);
            this.Load += new System.EventHandler(this.UseFlag_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button calculate;
        private System.Windows.Forms.Button reverse;
        private System.Windows.Forms.TextBox flagIO;
        private System.Windows.Forms.ListBox flagList;
        private System.Windows.Forms.ComboBox flagFiles;
        private System.Windows.Forms.Label label1;
    }
}
