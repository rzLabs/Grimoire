
namespace Grimoire.GUI
{
    partial class RdbSearch
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
            this.cells_lst = new System.Windows.Forms.ComboBox();
            this.cell_lb = new System.Windows.Forms.Label();
            this.input_lb = new System.Windows.Forms.Label();
            this.input_txtBx = new System.Windows.Forms.TextBox();
            this.params_grpBx = new System.Windows.Forms.GroupBox();
            this.notEqual_btn = new System.Windows.Forms.RadioButton();
            this.notlike_btn = new System.Windows.Forms.RadioButton();
            this.like_btn = new System.Windows.Forms.RadioButton();
            this.between_btn = new System.Windows.Forms.RadioButton();
            this.below_btn = new System.Windows.Forms.RadioButton();
            this.above_btn = new System.Windows.Forms.RadioButton();
            this.equals_btn = new System.Windows.Forms.RadioButton();
            this.search_btn = new System.Windows.Forms.Button();
            this.params_grpBx.SuspendLayout();
            this.SuspendLayout();
            // 
            // cells_lst
            // 
            this.cells_lst.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cells_lst.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cells_lst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cells_lst.FormattingEnabled = true;
            this.cells_lst.Location = new System.Drawing.Point(52, 7);
            this.cells_lst.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.cells_lst.Name = "cells_lst";
            this.cells_lst.Size = new System.Drawing.Size(181, 23);
            this.cells_lst.TabIndex = 0;
            // 
            // cell_lb
            // 
            this.cell_lb.AutoSize = true;
            this.cell_lb.Location = new System.Drawing.Point(14, 10);
            this.cell_lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.cell_lb.Name = "cell_lb";
            this.cell_lb.Size = new System.Drawing.Size(30, 15);
            this.cell_lb.TabIndex = 1;
            this.cell_lb.Text = "Cell:";
            // 
            // input_lb
            // 
            this.input_lb.AutoSize = true;
            this.input_lb.Location = new System.Drawing.Point(241, 10);
            this.input_lb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.input_lb.Name = "input_lb";
            this.input_lb.Size = new System.Drawing.Size(38, 15);
            this.input_lb.TabIndex = 2;
            this.input_lb.Text = "Input:";
            // 
            // input_txtBx
            // 
            this.input_txtBx.Location = new System.Drawing.Point(288, 7);
            this.input_txtBx.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.input_txtBx.Name = "input_txtBx";
            this.input_txtBx.Size = new System.Drawing.Size(274, 23);
            this.input_txtBx.TabIndex = 3;
            this.input_txtBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // params_grpBx
            // 
            this.params_grpBx.Controls.Add(this.notEqual_btn);
            this.params_grpBx.Controls.Add(this.notlike_btn);
            this.params_grpBx.Controls.Add(this.like_btn);
            this.params_grpBx.Controls.Add(this.between_btn);
            this.params_grpBx.Controls.Add(this.below_btn);
            this.params_grpBx.Controls.Add(this.above_btn);
            this.params_grpBx.Controls.Add(this.equals_btn);
            this.params_grpBx.Location = new System.Drawing.Point(14, 38);
            this.params_grpBx.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.params_grpBx.Name = "params_grpBx";
            this.params_grpBx.Padding = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.params_grpBx.Size = new System.Drawing.Size(548, 58);
            this.params_grpBx.TabIndex = 4;
            this.params_grpBx.TabStop = false;
            this.params_grpBx.Text = "Parameters";
            // 
            // notEqual_btn
            // 
            this.notEqual_btn.AutoSize = true;
            this.notEqual_btn.Location = new System.Drawing.Point(80, 22);
            this.notEqual_btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.notEqual_btn.Name = "notEqual_btn";
            this.notEqual_btn.Size = new System.Drawing.Size(77, 19);
            this.notEqual_btn.TabIndex = 6;
            this.notEqual_btn.Text = "Not Equal";
            this.notEqual_btn.UseVisualStyleBackColor = true;
            // 
            // notlike_btn
            // 
            this.notlike_btn.AutoSize = true;
            this.notlike_btn.Location = new System.Drawing.Point(458, 22);
            this.notlike_btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.notlike_btn.Name = "notlike_btn";
            this.notlike_btn.Size = new System.Drawing.Size(69, 19);
            this.notlike_btn.TabIndex = 5;
            this.notlike_btn.Text = "Not Like";
            this.notlike_btn.UseVisualStyleBackColor = true;
            // 
            // like_btn
            // 
            this.like_btn.AutoSize = true;
            this.like_btn.Location = new System.Drawing.Point(399, 22);
            this.like_btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.like_btn.Name = "like_btn";
            this.like_btn.Size = new System.Drawing.Size(46, 19);
            this.like_btn.TabIndex = 4;
            this.like_btn.Text = "Like";
            this.like_btn.UseVisualStyleBackColor = true;
            // 
            // between_btn
            // 
            this.between_btn.AutoSize = true;
            this.between_btn.Location = new System.Drawing.Point(314, 22);
            this.between_btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.between_btn.Name = "between_btn";
            this.between_btn.Size = new System.Drawing.Size(70, 19);
            this.between_btn.TabIndex = 3;
            this.between_btn.Text = "Between";
            this.between_btn.UseVisualStyleBackColor = true;
            // 
            // below_btn
            // 
            this.below_btn.AutoSize = true;
            this.below_btn.Location = new System.Drawing.Point(244, 22);
            this.below_btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.below_btn.Name = "below_btn";
            this.below_btn.Size = new System.Drawing.Size(57, 19);
            this.below_btn.TabIndex = 2;
            this.below_btn.Text = "Below";
            this.below_btn.UseVisualStyleBackColor = true;
            // 
            // above_btn
            // 
            this.above_btn.AutoSize = true;
            this.above_btn.Location = new System.Drawing.Point(172, 22);
            this.above_btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.above_btn.Name = "above_btn";
            this.above_btn.Size = new System.Drawing.Size(59, 19);
            this.above_btn.TabIndex = 1;
            this.above_btn.Text = "Above";
            this.above_btn.UseVisualStyleBackColor = true;
            // 
            // equals_btn
            // 
            this.equals_btn.AutoSize = true;
            this.equals_btn.Checked = true;
            this.equals_btn.Location = new System.Drawing.Point(7, 22);
            this.equals_btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.equals_btn.Name = "equals_btn";
            this.equals_btn.Size = new System.Drawing.Size(54, 19);
            this.equals_btn.TabIndex = 0;
            this.equals_btn.TabStop = true;
            this.equals_btn.Text = "Equal";
            this.equals_btn.UseVisualStyleBackColor = true;
            // 
            // search_btn
            // 
            this.search_btn.Location = new System.Drawing.Point(475, 115);
            this.search_btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(88, 27);
            this.search_btn.TabIndex = 5;
            this.search_btn.Text = "Search";
            this.search_btn.UseVisualStyleBackColor = true;
            this.search_btn.Click += new System.EventHandler(this.search_btn_Click);
            // 
            // RdbSearch
            // 
            this.AcceptButton = this.search_btn;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(576, 156);
            this.Controls.Add(this.search_btn);
            this.Controls.Add(this.params_grpBx);
            this.Controls.Add(this.input_txtBx);
            this.Controls.Add(this.input_lb);
            this.Controls.Add(this.cell_lb);
            this.Controls.Add(this.cells_lst);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimizeBox = false;
            this.Name = "RdbSearch";
            this.Text = "RDB Search";
            this.params_grpBx.ResumeLayout(false);
            this.params_grpBx.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cells_lst;
        private System.Windows.Forms.Label cell_lb;
        private System.Windows.Forms.Label input_lb;
        private System.Windows.Forms.TextBox input_txtBx;
        private System.Windows.Forms.GroupBox params_grpBx;
        private System.Windows.Forms.RadioButton notlike_btn;
        private System.Windows.Forms.RadioButton like_btn;
        private System.Windows.Forms.RadioButton between_btn;
        private System.Windows.Forms.RadioButton below_btn;
        private System.Windows.Forms.RadioButton above_btn;
        private System.Windows.Forms.RadioButton equals_btn;
        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.RadioButton notEqual_btn;
    }
}