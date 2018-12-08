namespace Grimoire.Tabs.Styles
{
    partial class Hasher
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
            this.components = new System.ComponentModel.Container();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.instBox = new System.Windows.Forms.GroupBox();
            this.flipBtn = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.TextBox();
            this.input = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.opt_name_box = new System.Windows.Forms.GroupBox();
            this.opt_remove_ascii = new System.Windows.Forms.RadioButton();
            this.opt_append_ascii = new System.Windows.Forms.RadioButton();
            this.opt_none = new System.Windows.Forms.RadioButton();
            this.multiBox = new System.Windows.Forms.GroupBox();
            this.fileGrid = new System.Windows.Forms.DataGridView();
            this.originalName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.convertedName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.filePath = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cMenu_multi = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cMenu_add = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenu_add_file = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenu_add_folder = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenu_convert = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenu_clear = new System.Windows.Forms.ToolStripMenuItem();
            this.autoClear_chk = new System.Windows.Forms.CheckBox();
            this.opt_grid_box = new System.Windows.Forms.GroupBox();
            this.opt_auto_convert = new System.Windows.Forms.CheckBox();
            this.cMenu_convert_all = new System.Windows.Forms.ToolStripMenuItem();
            this.instBox.SuspendLayout();
            this.opt_name_box.SuspendLayout();
            this.multiBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileGrid)).BeginInit();
            this.cMenu_multi.SuspendLayout();
            this.opt_grid_box.SuspendLayout();
            this.SuspendLayout();
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar1.Location = new System.Drawing.Point(690, 433);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(2);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(75, 19);
            this.progressBar1.TabIndex = 5;
            // 
            // instBox
            // 
            this.instBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.instBox.Controls.Add(this.flipBtn);
            this.instBox.Controls.Add(this.output);
            this.instBox.Controls.Add(this.input);
            this.instBox.Controls.Add(this.label2);
            this.instBox.Controls.Add(this.label1);
            this.instBox.Location = new System.Drawing.Point(15, 2);
            this.instBox.Margin = new System.Windows.Forms.Padding(2);
            this.instBox.Name = "instBox";
            this.instBox.Padding = new System.Windows.Forms.Padding(2);
            this.instBox.Size = new System.Drawing.Size(518, 86);
            this.instBox.TabIndex = 6;
            this.instBox.TabStop = false;
            this.instBox.Text = "Instant";
            // 
            // flipBtn
            // 
            this.flipBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.flipBtn.Location = new System.Drawing.Point(452, 63);
            this.flipBtn.Margin = new System.Windows.Forms.Padding(2);
            this.flipBtn.Name = "flipBtn";
            this.flipBtn.Size = new System.Drawing.Size(56, 19);
            this.flipBtn.TabIndex = 9;
            this.flipBtn.Text = "Flip";
            this.flipBtn.UseVisualStyleBackColor = true;
            this.flipBtn.Click += new System.EventHandler(this.flipBtn_Click);
            // 
            // output
            // 
            this.output.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.output.Location = new System.Drawing.Point(53, 40);
            this.output.Margin = new System.Windows.Forms.Padding(2);
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.Size = new System.Drawing.Size(456, 20);
            this.output.TabIndex = 8;
            this.output.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // input
            // 
            this.input.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.input.Location = new System.Drawing.Point(53, 17);
            this.input.Margin = new System.Windows.Forms.Padding(2);
            this.input.Name = "input";
            this.input.Size = new System.Drawing.Size(456, 20);
            this.input.TabIndex = 7;
            this.input.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.input.TextChanged += new System.EventHandler(this.input_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(39, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Ouput:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 20);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Input:";
            // 
            // opt_name_box
            // 
            this.opt_name_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.opt_name_box.Controls.Add(this.opt_remove_ascii);
            this.opt_name_box.Controls.Add(this.opt_append_ascii);
            this.opt_name_box.Controls.Add(this.opt_none);
            this.opt_name_box.Location = new System.Drawing.Point(537, 2);
            this.opt_name_box.Margin = new System.Windows.Forms.Padding(2);
            this.opt_name_box.Name = "opt_name_box";
            this.opt_name_box.Padding = new System.Windows.Forms.Padding(2);
            this.opt_name_box.Size = new System.Drawing.Size(117, 86);
            this.opt_name_box.TabIndex = 7;
            this.opt_name_box.TabStop = false;
            this.opt_name_box.Text = "Name Options";
            // 
            // opt_remove_ascii
            // 
            this.opt_remove_ascii.AutoSize = true;
            this.opt_remove_ascii.Location = new System.Drawing.Point(14, 61);
            this.opt_remove_ascii.Name = "opt_remove_ascii";
            this.opt_remove_ascii.Size = new System.Drawing.Size(95, 17);
            this.opt_remove_ascii.TabIndex = 2;
            this.opt_remove_ascii.TabStop = true;
            this.opt_remove_ascii.Text = "Remove (ascii)";
            this.opt_remove_ascii.UseVisualStyleBackColor = true;
            // 
            // opt_append_ascii
            // 
            this.opt_append_ascii.AutoSize = true;
            this.opt_append_ascii.Location = new System.Drawing.Point(13, 38);
            this.opt_append_ascii.Name = "opt_append_ascii";
            this.opt_append_ascii.Size = new System.Drawing.Size(92, 17);
            this.opt_append_ascii.TabIndex = 1;
            this.opt_append_ascii.TabStop = true;
            this.opt_append_ascii.Text = "Append (ascii)";
            this.opt_append_ascii.UseVisualStyleBackColor = true;
            // 
            // opt_none
            // 
            this.opt_none.AutoSize = true;
            this.opt_none.Location = new System.Drawing.Point(14, 18);
            this.opt_none.Name = "opt_none";
            this.opt_none.Size = new System.Drawing.Size(51, 17);
            this.opt_none.TabIndex = 0;
            this.opt_none.TabStop = true;
            this.opt_none.Text = "None";
            this.opt_none.UseVisualStyleBackColor = true;
            // 
            // multiBox
            // 
            this.multiBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.multiBox.Controls.Add(this.fileGrid);
            this.multiBox.Location = new System.Drawing.Point(15, 87);
            this.multiBox.Margin = new System.Windows.Forms.Padding(2);
            this.multiBox.Name = "multiBox";
            this.multiBox.Padding = new System.Windows.Forms.Padding(2);
            this.multiBox.Size = new System.Drawing.Size(752, 333);
            this.multiBox.TabIndex = 8;
            this.multiBox.TabStop = false;
            this.multiBox.Text = "Multi-File";
            // 
            // fileGrid
            // 
            this.fileGrid.AllowUserToAddRows = false;
            this.fileGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.fileGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.originalName,
            this.convertedName,
            this.filePath,
            this.status});
            this.fileGrid.ContextMenuStrip = this.cMenu_multi;
            this.fileGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileGrid.Location = new System.Drawing.Point(2, 15);
            this.fileGrid.Margin = new System.Windows.Forms.Padding(2);
            this.fileGrid.Name = "fileGrid";
            this.fileGrid.ReadOnly = true;
            this.fileGrid.RowTemplate.Height = 24;
            this.fileGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.fileGrid.Size = new System.Drawing.Size(748, 316);
            this.fileGrid.TabIndex = 1;
            // 
            // originalName
            // 
            this.originalName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.originalName.HeaderText = "Original Name";
            this.originalName.Name = "originalName";
            this.originalName.ReadOnly = true;
            // 
            // convertedName
            // 
            this.convertedName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.convertedName.HeaderText = "Converted Name";
            this.convertedName.Name = "convertedName";
            this.convertedName.ReadOnly = true;
            // 
            // filePath
            // 
            this.filePath.HeaderText = "File Path";
            this.filePath.Name = "filePath";
            this.filePath.ReadOnly = true;
            this.filePath.Visible = false;
            // 
            // status
            // 
            this.status.HeaderText = "Status";
            this.status.Name = "status";
            this.status.ReadOnly = true;
            this.status.Width = 75;
            // 
            // cMenu_multi
            // 
            this.cMenu_multi.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cMenu_multi.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenu_add,
            this.cMenu_convert,
            this.cMenu_clear});
            this.cMenu_multi.Name = "cMenu_multi";
            this.cMenu_multi.Size = new System.Drawing.Size(117, 70);
            // 
            // cMenu_add
            // 
            this.cMenu_add.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenu_add_file,
            this.cMenu_add_folder});
            this.cMenu_add.Name = "cMenu_add";
            this.cMenu_add.Size = new System.Drawing.Size(180, 22);
            this.cMenu_add.Text = "Add";
            // 
            // cMenu_add_file
            // 
            this.cMenu_add_file.Name = "cMenu_add_file";
            this.cMenu_add_file.Size = new System.Drawing.Size(107, 22);
            this.cMenu_add_file.Text = "File";
            this.cMenu_add_file.Click += new System.EventHandler(this.cMenu_add_file_Click);
            // 
            // cMenu_add_folder
            // 
            this.cMenu_add_folder.Name = "cMenu_add_folder";
            this.cMenu_add_folder.Size = new System.Drawing.Size(180, 22);
            this.cMenu_add_folder.Text = "Folder";
            this.cMenu_add_folder.Click += new System.EventHandler(this.cMenu_add_folder_Click);
            // 
            // cMenu_convert
            // 
            this.cMenu_convert.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cMenu_convert_all});
            this.cMenu_convert.Name = "cMenu_convert";
            this.cMenu_convert.Size = new System.Drawing.Size(116, 22);
            this.cMenu_convert.Text = "Convert";
            this.cMenu_convert.Click += new System.EventHandler(this.cMenu_convert_Click);
            // 
            // cMenu_clear
            // 
            this.cMenu_clear.Name = "cMenu_clear";
            this.cMenu_clear.Size = new System.Drawing.Size(116, 22);
            this.cMenu_clear.Text = "Clear";
            this.cMenu_clear.Click += new System.EventHandler(this.cMenu_clear_Click);
            // 
            // autoClear_chk
            // 
            this.autoClear_chk.AutoSize = true;
            this.autoClear_chk.Location = new System.Drawing.Point(16, 17);
            this.autoClear_chk.Margin = new System.Windows.Forms.Padding(2);
            this.autoClear_chk.Name = "autoClear_chk";
            this.autoClear_chk.Size = new System.Drawing.Size(75, 17);
            this.autoClear_chk.TabIndex = 4;
            this.autoClear_chk.Text = "Auto Clear";
            this.autoClear_chk.UseVisualStyleBackColor = true;
            this.autoClear_chk.CheckStateChanged += new System.EventHandler(this.autoClear_chk_CheckStateChanged);
            // 
            // opt_grid_box
            // 
            this.opt_grid_box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.opt_grid_box.Controls.Add(this.opt_auto_convert);
            this.opt_grid_box.Controls.Add(this.autoClear_chk);
            this.opt_grid_box.Location = new System.Drawing.Point(659, 3);
            this.opt_grid_box.Name = "opt_grid_box";
            this.opt_grid_box.Size = new System.Drawing.Size(108, 85);
            this.opt_grid_box.TabIndex = 9;
            this.opt_grid_box.TabStop = false;
            this.opt_grid_box.Text = "Grid Options";
            // 
            // opt_auto_convert
            // 
            this.opt_auto_convert.AutoSize = true;
            this.opt_auto_convert.Location = new System.Drawing.Point(16, 37);
            this.opt_auto_convert.Name = "opt_auto_convert";
            this.opt_auto_convert.Size = new System.Drawing.Size(88, 17);
            this.opt_auto_convert.TabIndex = 5;
            this.opt_auto_convert.Text = "Auto Convert";
            this.opt_auto_convert.UseVisualStyleBackColor = true;
            this.opt_auto_convert.CheckStateChanged += new System.EventHandler(this.opt_auto_convert_CheckStateChanged);
            // 
            // cMenu_convert_all
            // 
            this.cMenu_convert_all.Name = "cMenu_convert_all";
            this.cMenu_convert_all.Size = new System.Drawing.Size(180, 22);
            this.cMenu_convert_all.Text = "All";
            this.cMenu_convert_all.Click += new System.EventHandler(this.cMenu_convert_all_Click);
            // 
            // Hasher
            // 
            this.AllowDrop = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.opt_grid_box);
            this.Controls.Add(this.multiBox);
            this.Controls.Add(this.opt_name_box);
            this.Controls.Add(this.instBox);
            this.Controls.Add(this.progressBar1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Hasher";
            this.Size = new System.Drawing.Size(781, 454);
            this.instBox.ResumeLayout(false);
            this.instBox.PerformLayout();
            this.opt_name_box.ResumeLayout(false);
            this.opt_name_box.PerformLayout();
            this.multiBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileGrid)).EndInit();
            this.cMenu_multi.ResumeLayout(false);
            this.opt_grid_box.ResumeLayout(false);
            this.opt_grid_box.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.GroupBox instBox;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox opt_name_box;
        private System.Windows.Forms.GroupBox multiBox;
        private System.Windows.Forms.DataGridView fileGrid;
        private System.Windows.Forms.ContextMenuStrip cMenu_multi;
        private System.Windows.Forms.ToolStripMenuItem cMenu_clear;
        private System.Windows.Forms.Button flipBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn originalName;
        private System.Windows.Forms.DataGridViewTextBoxColumn convertedName;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.ToolStripMenuItem cMenu_add;
        private System.Windows.Forms.ToolStripMenuItem cMenu_add_file;
        private System.Windows.Forms.ToolStripMenuItem cMenu_add_folder;
        private System.Windows.Forms.RadioButton opt_remove_ascii;
        private System.Windows.Forms.RadioButton opt_append_ascii;
        private System.Windows.Forms.RadioButton opt_none;
        private System.Windows.Forms.ToolStripMenuItem cMenu_convert;
        private System.Windows.Forms.CheckBox autoClear_chk;
        private System.Windows.Forms.GroupBox opt_grid_box;
        private System.Windows.Forms.CheckBox opt_auto_convert;
        private System.Windows.Forms.ToolStripMenuItem cMenu_convert_all;
    }
}
