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
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.inst_grpBx = new System.Windows.Forms.GroupBox();
            this.inst_flipBtn = new System.Windows.Forms.Button();
            this.output = new System.Windows.Forms.TextBox();
            this.input = new System.Windows.Forms.TextBox();
            this.inst_outLbl = new System.Windows.Forms.Label();
            this.inst_inputLbl = new System.Windows.Forms.Label();
            this.optName_grpBx = new System.Windows.Forms.GroupBox();
            this.optRemove_ascii_rBtn = new System.Windows.Forms.RadioButton();
            this.optAppend_ascii_rBtn = new System.Windows.Forms.RadioButton();
            this.optNone_rBtn = new System.Windows.Forms.RadioButton();
            this.multi_grpBx = new System.Windows.Forms.GroupBox();
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
            this.cMenu_convert_all = new System.Windows.Forms.ToolStripMenuItem();
            this.cMenu_clear = new System.Windows.Forms.ToolStripMenuItem();
            this.autoClear_chk = new System.Windows.Forms.CheckBox();
            this.optGrid_grpBx = new System.Windows.Forms.GroupBox();
            this.autoConvert_chk = new System.Windows.Forms.CheckBox();
            this.inst_grpBx.SuspendLayout();
            this.optName_grpBx.SuspendLayout();
            this.multi_grpBx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.fileGrid)).BeginInit();
            this.cMenu_multi.SuspendLayout();
            this.optGrid_grpBx.SuspendLayout();
            this.SuspendLayout();
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prgBar.Location = new System.Drawing.Point(690, 433);
            this.prgBar.Margin = new System.Windows.Forms.Padding(2);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(75, 19);
            this.prgBar.TabIndex = 5;
            // 
            // inst_grpBx
            // 
            this.inst_grpBx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inst_grpBx.Controls.Add(this.inst_flipBtn);
            this.inst_grpBx.Controls.Add(this.output);
            this.inst_grpBx.Controls.Add(this.input);
            this.inst_grpBx.Controls.Add(this.inst_outLbl);
            this.inst_grpBx.Controls.Add(this.inst_inputLbl);
            this.inst_grpBx.Location = new System.Drawing.Point(15, 2);
            this.inst_grpBx.Margin = new System.Windows.Forms.Padding(2);
            this.inst_grpBx.Name = "inst_grpBx";
            this.inst_grpBx.Padding = new System.Windows.Forms.Padding(2);
            this.inst_grpBx.Size = new System.Drawing.Size(518, 86);
            this.inst_grpBx.TabIndex = 6;
            this.inst_grpBx.TabStop = false;
            this.inst_grpBx.Text = "Instant";
            // 
            // inst_flipBtn
            // 
            this.inst_flipBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.inst_flipBtn.Location = new System.Drawing.Point(452, 63);
            this.inst_flipBtn.Margin = new System.Windows.Forms.Padding(2);
            this.inst_flipBtn.Name = "inst_flipBtn";
            this.inst_flipBtn.Size = new System.Drawing.Size(56, 19);
            this.inst_flipBtn.TabIndex = 9;
            this.inst_flipBtn.Text = "Flip";
            this.inst_flipBtn.UseVisualStyleBackColor = true;
            this.inst_flipBtn.Click += new System.EventHandler(this.flipBtn_Click);
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
            // inst_outLbl
            // 
            this.inst_outLbl.AutoSize = true;
            this.inst_outLbl.Location = new System.Drawing.Point(10, 42);
            this.inst_outLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.inst_outLbl.Name = "inst_outLbl";
            this.inst_outLbl.Size = new System.Drawing.Size(39, 13);
            this.inst_outLbl.TabIndex = 6;
            this.inst_outLbl.Text = "Ouput:";
            // 
            // inst_inputLbl
            // 
            this.inst_inputLbl.AutoSize = true;
            this.inst_inputLbl.Location = new System.Drawing.Point(10, 20);
            this.inst_inputLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.inst_inputLbl.Name = "inst_inputLbl";
            this.inst_inputLbl.Size = new System.Drawing.Size(34, 13);
            this.inst_inputLbl.TabIndex = 5;
            this.inst_inputLbl.Text = "Input:";
            // 
            // optName_grpBx
            // 
            this.optName_grpBx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optName_grpBx.Controls.Add(this.optRemove_ascii_rBtn);
            this.optName_grpBx.Controls.Add(this.optAppend_ascii_rBtn);
            this.optName_grpBx.Controls.Add(this.optNone_rBtn);
            this.optName_grpBx.Location = new System.Drawing.Point(537, 2);
            this.optName_grpBx.Margin = new System.Windows.Forms.Padding(2);
            this.optName_grpBx.Name = "optName_grpBx";
            this.optName_grpBx.Padding = new System.Windows.Forms.Padding(2);
            this.optName_grpBx.Size = new System.Drawing.Size(117, 86);
            this.optName_grpBx.TabIndex = 7;
            this.optName_grpBx.TabStop = false;
            this.optName_grpBx.Text = "Name Options";
            // 
            // optRemove_ascii_rBtn
            // 
            this.optRemove_ascii_rBtn.AutoSize = true;
            this.optRemove_ascii_rBtn.Location = new System.Drawing.Point(14, 61);
            this.optRemove_ascii_rBtn.Name = "optRemove_ascii_rBtn";
            this.optRemove_ascii_rBtn.Size = new System.Drawing.Size(95, 17);
            this.optRemove_ascii_rBtn.TabIndex = 2;
            this.optRemove_ascii_rBtn.TabStop = true;
            this.optRemove_ascii_rBtn.Text = "Remove (ascii)";
            this.optRemove_ascii_rBtn.UseVisualStyleBackColor = true;
            // 
            // optAppend_ascii_rBtn
            // 
            this.optAppend_ascii_rBtn.AutoSize = true;
            this.optAppend_ascii_rBtn.Location = new System.Drawing.Point(13, 38);
            this.optAppend_ascii_rBtn.Name = "optAppend_ascii_rBtn";
            this.optAppend_ascii_rBtn.Size = new System.Drawing.Size(92, 17);
            this.optAppend_ascii_rBtn.TabIndex = 1;
            this.optAppend_ascii_rBtn.TabStop = true;
            this.optAppend_ascii_rBtn.Text = "Append (ascii)";
            this.optAppend_ascii_rBtn.UseVisualStyleBackColor = true;
            // 
            // optNone_rBtn
            // 
            this.optNone_rBtn.AutoSize = true;
            this.optNone_rBtn.Location = new System.Drawing.Point(14, 18);
            this.optNone_rBtn.Name = "optNone_rBtn";
            this.optNone_rBtn.Size = new System.Drawing.Size(51, 17);
            this.optNone_rBtn.TabIndex = 0;
            this.optNone_rBtn.TabStop = true;
            this.optNone_rBtn.Text = "None";
            this.optNone_rBtn.UseVisualStyleBackColor = true;
            // 
            // multi_grpBx
            // 
            this.multi_grpBx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.multi_grpBx.Controls.Add(this.fileGrid);
            this.multi_grpBx.Location = new System.Drawing.Point(15, 87);
            this.multi_grpBx.Margin = new System.Windows.Forms.Padding(2);
            this.multi_grpBx.Name = "multi_grpBx";
            this.multi_grpBx.Padding = new System.Windows.Forms.Padding(2);
            this.multi_grpBx.Size = new System.Drawing.Size(752, 333);
            this.multi_grpBx.TabIndex = 8;
            this.multi_grpBx.TabStop = false;
            this.multi_grpBx.Text = "Multi-File";
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
            this.cMenu_add.Size = new System.Drawing.Size(116, 22);
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
            this.cMenu_add_folder.Size = new System.Drawing.Size(107, 22);
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
            // cMenu_convert_all
            // 
            this.cMenu_convert_all.Name = "cMenu_convert_all";
            this.cMenu_convert_all.Size = new System.Drawing.Size(88, 22);
            this.cMenu_convert_all.Text = "All";
            this.cMenu_convert_all.Click += new System.EventHandler(this.cMenu_convert_all_Click);
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
            // optGrid_grpBx
            // 
            this.optGrid_grpBx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.optGrid_grpBx.Controls.Add(this.autoConvert_chk);
            this.optGrid_grpBx.Controls.Add(this.autoClear_chk);
            this.optGrid_grpBx.Location = new System.Drawing.Point(659, 3);
            this.optGrid_grpBx.Name = "optGrid_grpBx";
            this.optGrid_grpBx.Size = new System.Drawing.Size(108, 85);
            this.optGrid_grpBx.TabIndex = 9;
            this.optGrid_grpBx.TabStop = false;
            this.optGrid_grpBx.Text = "Grid Options";
            // 
            // autoConvert_chk
            // 
            this.autoConvert_chk.AutoSize = true;
            this.autoConvert_chk.Location = new System.Drawing.Point(16, 37);
            this.autoConvert_chk.Name = "autoConvert_chk";
            this.autoConvert_chk.Size = new System.Drawing.Size(88, 17);
            this.autoConvert_chk.TabIndex = 5;
            this.autoConvert_chk.Text = "Auto Convert";
            this.autoConvert_chk.UseVisualStyleBackColor = true;
            this.autoConvert_chk.CheckStateChanged += new System.EventHandler(this.opt_auto_convert_CheckStateChanged);
            // 
            // Hasher
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.optGrid_grpBx);
            this.Controls.Add(this.multi_grpBx);
            this.Controls.Add(this.optName_grpBx);
            this.Controls.Add(this.inst_grpBx);
            this.Controls.Add(this.prgBar);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Hasher";
            this.Size = new System.Drawing.Size(781, 454);
            this.inst_grpBx.ResumeLayout(false);
            this.inst_grpBx.PerformLayout();
            this.optName_grpBx.ResumeLayout(false);
            this.optName_grpBx.PerformLayout();
            this.multi_grpBx.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.fileGrid)).EndInit();
            this.cMenu_multi.ResumeLayout(false);
            this.optGrid_grpBx.ResumeLayout(false);
            this.optGrid_grpBx.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.GroupBox inst_grpBx;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.TextBox input;
        private System.Windows.Forms.Label inst_outLbl;
        private System.Windows.Forms.Label inst_inputLbl;
        private System.Windows.Forms.GroupBox optName_grpBx;
        private System.Windows.Forms.GroupBox multi_grpBx;
        private System.Windows.Forms.DataGridView fileGrid;
        private System.Windows.Forms.ContextMenuStrip cMenu_multi;
        private System.Windows.Forms.ToolStripMenuItem cMenu_clear;
        private System.Windows.Forms.Button inst_flipBtn;
        private System.Windows.Forms.DataGridViewTextBoxColumn originalName;
        private System.Windows.Forms.DataGridViewTextBoxColumn convertedName;
        private System.Windows.Forms.DataGridViewTextBoxColumn filePath;
        private System.Windows.Forms.DataGridViewTextBoxColumn status;
        private System.Windows.Forms.ToolStripMenuItem cMenu_add;
        private System.Windows.Forms.ToolStripMenuItem cMenu_add_file;
        private System.Windows.Forms.ToolStripMenuItem cMenu_add_folder;
        private System.Windows.Forms.RadioButton optRemove_ascii_rBtn;
        private System.Windows.Forms.RadioButton optAppend_ascii_rBtn;
        private System.Windows.Forms.RadioButton optNone_rBtn;
        private System.Windows.Forms.ToolStripMenuItem cMenu_convert;
        private System.Windows.Forms.CheckBox autoClear_chk;
        private System.Windows.Forms.GroupBox optGrid_grpBx;
        private System.Windows.Forms.CheckBox autoConvert_chk;
        private System.Windows.Forms.ToolStripMenuItem cMenu_convert_all;
    }
}
