namespace Grimoire.Tabs.Styles
{
    partial class UseFlag
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
            this.flagList.Location = new System.Drawing.Point(14, 13);
            this.flagList.Name = "flagList";
            this.flagList.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.flagList.Size = new System.Drawing.Size(720, 379);
            this.flagList.TabIndex = 4;
            // 
            // UseFlag
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.flagList);
            this.Controls.Add(this.flagIO);
            this.Controls.Add(this.reverse);
            this.Controls.Add(this.calculate);
            this.Name = "UseFlag";
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
    }
}
