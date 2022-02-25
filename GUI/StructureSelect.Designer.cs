
namespace Grimoire.GUI
{
    partial class StructureSelect
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
            this.struct_select_btn = new System.Windows.Forms.Button();
            this.remSelect_chkBx = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // struct_select_btn
            // 
            this.struct_select_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.struct_select_btn.Location = new System.Drawing.Point(558, 330);
            this.struct_select_btn.Name = "struct_select_btn";
            this.struct_select_btn.Size = new System.Drawing.Size(75, 23);
            this.struct_select_btn.TabIndex = 4;
            this.struct_select_btn.Text = "Select";
            this.struct_select_btn.UseVisualStyleBackColor = true;
            this.struct_select_btn.Click += new System.EventHandler(this.struct_select_btn_Click);
            // 
            // remSelect_chkBx
            // 
            this.remSelect_chkBx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.remSelect_chkBx.AutoSize = true;
            this.remSelect_chkBx.Location = new System.Drawing.Point(12, 334);
            this.remSelect_chkBx.Name = "remSelect_chkBx";
            this.remSelect_chkBx.Size = new System.Drawing.Size(122, 17);
            this.remSelect_chkBx.TabIndex = 5;
            this.remSelect_chkBx.Text = "Remember selection";
            this.remSelect_chkBx.UseVisualStyleBackColor = true;
            this.remSelect_chkBx.CheckStateChanged += new System.EventHandler(this.selectLast_CheckStateChanged);
            // 
            // StructureSelect
            // 
            this.AcceptButton = this.struct_select_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 362);
            this.Controls.Add(this.remSelect_chkBx);
            this.Controls.Add(this.struct_select_btn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.MinimumSize = new System.Drawing.Size(469, 317);
            this.Name = "StructureSelect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Structure Select";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StructureSelect_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button struct_select_btn;
        private System.Windows.Forms.CheckBox remSelect_chkBx;
    }
}