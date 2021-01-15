namespace Grimoire.GUI
{
    partial class SPRGenerator
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
            this.typeGrpBx = new System.Windows.Forms.GroupBox();
            this.equipBtn = new System.Windows.Forms.RadioButton();
            this.itemBtn = new System.Windows.Forms.RadioButton();
            this.skillBtn = new System.Windows.Forms.RadioButton();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.genBtn = new System.Windows.Forms.Button();
            this.statusLb = new System.Windows.Forms.Label();
            this.typeGrpBx.SuspendLayout();
            this.SuspendLayout();
            // 
            // typeGrpBx
            // 
            this.typeGrpBx.Controls.Add(this.skillBtn);
            this.typeGrpBx.Controls.Add(this.itemBtn);
            this.typeGrpBx.Controls.Add(this.equipBtn);
            this.typeGrpBx.Location = new System.Drawing.Point(12, 12);
            this.typeGrpBx.Name = "typeGrpBx";
            this.typeGrpBx.Size = new System.Drawing.Size(188, 66);
            this.typeGrpBx.TabIndex = 0;
            this.typeGrpBx.TabStop = false;
            this.typeGrpBx.Text = "Type";
            // 
            // equipBtn
            // 
            this.equipBtn.AutoSize = true;
            this.equipBtn.Location = new System.Drawing.Point(19, 29);
            this.equipBtn.Name = "equipBtn";
            this.equipBtn.Size = new System.Drawing.Size(52, 17);
            this.equipBtn.TabIndex = 0;
            this.equipBtn.TabStop = true;
            this.equipBtn.Text = "Equip";
            this.equipBtn.UseVisualStyleBackColor = true;
            // 
            // itemBtn
            // 
            this.itemBtn.AutoSize = true;
            this.itemBtn.Location = new System.Drawing.Point(77, 29);
            this.itemBtn.Name = "itemBtn";
            this.itemBtn.Size = new System.Drawing.Size(45, 17);
            this.itemBtn.TabIndex = 1;
            this.itemBtn.TabStop = true;
            this.itemBtn.Text = "Item";
            this.itemBtn.UseVisualStyleBackColor = true;
            // 
            // skillBtn
            // 
            this.skillBtn.AutoSize = true;
            this.skillBtn.Location = new System.Drawing.Point(128, 29);
            this.skillBtn.Name = "skillBtn";
            this.skillBtn.Size = new System.Drawing.Size(44, 17);
            this.skillBtn.TabIndex = 2;
            this.skillBtn.TabStop = true;
            this.skillBtn.Text = "Skill";
            this.skillBtn.UseVisualStyleBackColor = true;
            // 
            // prgBar
            // 
            this.prgBar.Location = new System.Drawing.Point(12, 103);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(279, 23);
            this.prgBar.TabIndex = 1;
            // 
            // genBtn
            // 
            this.genBtn.Location = new System.Drawing.Point(206, 17);
            this.genBtn.Name = "genBtn";
            this.genBtn.Size = new System.Drawing.Size(85, 61);
            this.genBtn.TabIndex = 2;
            this.genBtn.Text = "Generate";
            this.genBtn.UseVisualStyleBackColor = true;
            this.genBtn.Click += new System.EventHandler(this.genBtn_Click);
            // 
            // statusLb
            // 
            this.statusLb.AutoSize = true;
            this.statusLb.Location = new System.Drawing.Point(12, 86);
            this.statusLb.Name = "statusLb";
            this.statusLb.Size = new System.Drawing.Size(0, 13);
            this.statusLb.TabIndex = 3;
            // 
            // SPRGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(303, 140);
            this.Controls.Add(this.statusLb);
            this.Controls.Add(this.genBtn);
            this.Controls.Add(this.prgBar);
            this.Controls.Add(this.typeGrpBx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SPRGenerator";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "SPR Generator";
            this.typeGrpBx.ResumeLayout(false);
            this.typeGrpBx.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox typeGrpBx;
        private System.Windows.Forms.RadioButton skillBtn;
        private System.Windows.Forms.RadioButton itemBtn;
        private System.Windows.Forms.RadioButton equipBtn;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Button genBtn;
        private System.Windows.Forms.Label statusLb;
    }
}