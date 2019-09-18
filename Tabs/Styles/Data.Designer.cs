namespace Grimoire.Tabs.Styles
{
    partial class Data
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Data));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ts_file_load = new System.Windows.Forms.ToolStripButton();
            this.ts_progress = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_file_new = new System.Windows.Forms.ToolStripButton();
            this.ts_status = new System.Windows.Forms.ToolStripLabel();
            this.ts_file_rebuild = new System.Windows.Forms.ToolStripButton();
            this.ext_grpBx = new System.Windows.Forms.GroupBox();
            this.extensions = new System.Windows.Forms.TreeView();
            this.extensions_cs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extensions_cs_export = new System.Windows.Forms.ToolStripMenuItem();
            this.grid_grpBx = new System.Windows.Forms.GroupBox();
            this.grid = new System.Windows.Forms.DataGridView();
            this.ig_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grid_cs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.grid_cs_compare = new System.Windows.Forms.ToolStripMenuItem();
            this.grid_cs_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.grid_cs_export = new System.Windows.Forms.ToolStripMenuItem();
            this.grid_cs_insert = new System.Windows.Forms.ToolStripMenuItem();
            this.stats_grpBx = new System.Windows.Forms.GroupBox();
            this.uploadPath = new System.Windows.Forms.TextBox();
            this.upPathLbl = new System.Windows.Forms.Label();
            this.encrypted = new System.Windows.Forms.TextBox();
            this.encLbl = new System.Windows.Forms.Label();
            this.extension = new System.Windows.Forms.TextBox();
            this.extLbl = new System.Windows.Forms.Label();
            this.size = new System.Windows.Forms.TextBox();
            this.sizeLbl = new System.Windows.Forms.Label();
            this.offset = new System.Windows.Forms.TextBox();
            this.offsetLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataId = new System.Windows.Forms.TextBox();
            this.dataId_lbl = new System.Windows.Forms.Label();
            this.extStatus = new System.Windows.Forms.Label();
            this.search_grpBx = new System.Windows.Forms.GroupBox();
            this.searchInput = new System.Windows.Forms.TextBox();
            this.toolStrip1.SuspendLayout();
            this.ext_grpBx.SuspendLayout();
            this.extensions_cs.SuspendLayout();
            this.grid_grpBx.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.grid_cs.SuspendLayout();
            this.stats_grpBx.SuspendLayout();
            this.search_grpBx.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_file_load,
            this.ts_progress,
            this.toolStripSeparator1,
            this.ts_file_new,
            this.ts_status,
            this.ts_file_rebuild});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(753, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ts_file_load
            // 
            this.ts_file_load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_file_load.Image = ((System.Drawing.Image)(resources.GetObject("ts_file_load.Image")));
            this.ts_file_load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_file_load.Name = "ts_file_load";
            this.ts_file_load.Size = new System.Drawing.Size(37, 22);
            this.ts_file_load.Text = "Load";
            this.ts_file_load.Click += new System.EventHandler(this.TS_File_Load_Click);
            // 
            // ts_progress
            // 
            this.ts_progress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_progress.Name = "ts_progress";
            this.ts_progress.Size = new System.Drawing.Size(75, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ts_file_new
            // 
            this.ts_file_new.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_file_new.Image = ((System.Drawing.Image)(resources.GetObject("ts_file_new.Image")));
            this.ts_file_new.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_file_new.Name = "ts_file_new";
            this.ts_file_new.Size = new System.Drawing.Size(35, 22);
            this.ts_file_new.Text = "New";
            this.ts_file_new.Click += new System.EventHandler(this.TS_File_New_Click);
            // 
            // ts_status
            // 
            this.ts_status.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_status.Name = "ts_status";
            this.ts_status.Size = new System.Drawing.Size(0, 22);
            // 
            // ts_file_rebuild
            // 
            this.ts_file_rebuild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_file_rebuild.Image = ((System.Drawing.Image)(resources.GetObject("ts_file_rebuild.Image")));
            this.ts_file_rebuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_file_rebuild.Name = "ts_file_rebuild";
            this.ts_file_rebuild.Size = new System.Drawing.Size(51, 22);
            this.ts_file_rebuild.Text = "Rebuild";
            this.ts_file_rebuild.Visible = false;
            this.ts_file_rebuild.Click += new System.EventHandler(this.TS_File_Rebuild_Click);
            // 
            // ext_grpBx
            // 
            this.ext_grpBx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ext_grpBx.Controls.Add(this.extensions);
            this.ext_grpBx.Location = new System.Drawing.Point(552, 24);
            this.ext_grpBx.Margin = new System.Windows.Forms.Padding(2);
            this.ext_grpBx.Name = "ext_grpBx";
            this.ext_grpBx.Padding = new System.Windows.Forms.Padding(2);
            this.ext_grpBx.Size = new System.Drawing.Size(201, 208);
            this.ext_grpBx.TabIndex = 1;
            this.ext_grpBx.TabStop = false;
            this.ext_grpBx.Text = "Extensions";
            // 
            // extensions
            // 
            this.extensions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extensions.Location = new System.Drawing.Point(2, 15);
            this.extensions.Margin = new System.Windows.Forms.Padding(2);
            this.extensions.Name = "extensions";
            this.extensions.Size = new System.Drawing.Size(197, 191);
            this.extensions.TabIndex = 0;
            this.extensions.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.extensions_BeforeExpand);
            this.extensions.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.extensions_AfterSelect);
            this.extensions.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.extensions_NodeMouseClick);
            // 
            // extensions_cs
            // 
            this.extensions_cs.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.extensions_cs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extensions_cs_export});
            this.extensions_cs.Name = "extensions_cs";
            this.extensions_cs.Size = new System.Drawing.Size(108, 26);
            // 
            // extensions_cs_export
            // 
            this.extensions_cs_export.Enabled = false;
            this.extensions_cs_export.Name = "extensions_cs_export";
            this.extensions_cs_export.Size = new System.Drawing.Size(107, 22);
            this.extensions_cs_export.Text = "Export";
            this.extensions_cs_export.Click += new System.EventHandler(this.extensions_cs_export_Click);
            // 
            // grid_grpBx
            // 
            this.grid_grpBx.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid_grpBx.Controls.Add(this.grid);
            this.grid_grpBx.Location = new System.Drawing.Point(14, 24);
            this.grid_grpBx.Margin = new System.Windows.Forms.Padding(2);
            this.grid_grpBx.Name = "grid_grpBx";
            this.grid_grpBx.Padding = new System.Windows.Forms.Padding(2);
            this.grid_grpBx.Size = new System.Drawing.Size(534, 354);
            this.grid_grpBx.TabIndex = 2;
            this.grid_grpBx.TabStop = false;
            this.grid_grpBx.Text = "Index";
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ig_name});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(2, 15);
            this.grid.Margin = new System.Windows.Forms.Padding(2);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowTemplate.Height = 24;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(530, 337);
            this.grid.TabIndex = 0;
            this.grid.VirtualMode = true;
            this.grid.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_CellMouseDown);
            this.grid.SelectionChanged += new System.EventHandler(this.grid_SelectionChanged);
            this.grid.DoubleClick += new System.EventHandler(this.grid_DoubleClick);
            // 
            // ig_name
            // 
            this.ig_name.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.ig_name.HeaderText = "File Name";
            this.ig_name.Name = "ig_name";
            this.ig_name.ReadOnly = true;
            // 
            // grid_cs
            // 
            this.grid_cs.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.grid_cs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grid_cs_compare,
            this.grid_cs_delete,
            this.grid_cs_export,
            this.grid_cs_insert});
            this.grid_cs.Name = "grid_cs";
            this.grid_cs.Size = new System.Drawing.Size(124, 92);
            // 
            // grid_cs_compare
            // 
            this.grid_cs_compare.Name = "grid_cs_compare";
            this.grid_cs_compare.Size = new System.Drawing.Size(123, 22);
            this.grid_cs_compare.Text = "Compare";
            this.grid_cs_compare.Click += new System.EventHandler(this.grid_cs_compare_Click);
            // 
            // grid_cs_delete
            // 
            this.grid_cs_delete.Name = "grid_cs_delete";
            this.grid_cs_delete.Size = new System.Drawing.Size(123, 22);
            this.grid_cs_delete.Text = "Delete";
            this.grid_cs_delete.Click += new System.EventHandler(this.grid_cs_delete_Click);
            // 
            // grid_cs_export
            // 
            this.grid_cs_export.Name = "grid_cs_export";
            this.grid_cs_export.Size = new System.Drawing.Size(123, 22);
            this.grid_cs_export.Text = "Export";
            this.grid_cs_export.Click += new System.EventHandler(this.grid_cs_export_Click);
            // 
            // grid_cs_insert
            // 
            this.grid_cs_insert.Name = "grid_cs_insert";
            this.grid_cs_insert.Size = new System.Drawing.Size(123, 22);
            this.grid_cs_insert.Text = "Insert";
            this.grid_cs_insert.Click += new System.EventHandler(this.grid_cs_insert_Click);
            // 
            // stats_grpBx
            // 
            this.stats_grpBx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.stats_grpBx.Controls.Add(this.uploadPath);
            this.stats_grpBx.Controls.Add(this.upPathLbl);
            this.stats_grpBx.Controls.Add(this.encrypted);
            this.stats_grpBx.Controls.Add(this.encLbl);
            this.stats_grpBx.Controls.Add(this.extension);
            this.stats_grpBx.Controls.Add(this.extLbl);
            this.stats_grpBx.Controls.Add(this.size);
            this.stats_grpBx.Controls.Add(this.sizeLbl);
            this.stats_grpBx.Controls.Add(this.offset);
            this.stats_grpBx.Controls.Add(this.offsetLbl);
            this.stats_grpBx.Controls.Add(this.label2);
            this.stats_grpBx.Controls.Add(this.dataId);
            this.stats_grpBx.Controls.Add(this.dataId_lbl);
            this.stats_grpBx.Location = new System.Drawing.Point(552, 256);
            this.stats_grpBx.Margin = new System.Windows.Forms.Padding(2);
            this.stats_grpBx.Name = "stats_grpBx";
            this.stats_grpBx.Padding = new System.Windows.Forms.Padding(2);
            this.stats_grpBx.Size = new System.Drawing.Size(199, 180);
            this.stats_grpBx.TabIndex = 3;
            this.stats_grpBx.TabStop = false;
            this.stats_grpBx.Text = "Statistics";
            // 
            // uploadPath
            // 
            this.uploadPath.Location = new System.Drawing.Point(123, 142);
            this.uploadPath.Margin = new System.Windows.Forms.Padding(2);
            this.uploadPath.Name = "uploadPath";
            this.uploadPath.ReadOnly = true;
            this.uploadPath.Size = new System.Drawing.Size(64, 20);
            this.uploadPath.TabIndex = 12;
            this.uploadPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // upPathLbl
            // 
            this.upPathLbl.AutoSize = true;
            this.upPathLbl.Location = new System.Drawing.Point(11, 145);
            this.upPathLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.upPathLbl.Name = "upPathLbl";
            this.upPathLbl.Size = new System.Drawing.Size(69, 13);
            this.upPathLbl.TabIndex = 11;
            this.upPathLbl.Text = "Upload Path:";
            // 
            // encrypted
            // 
            this.encrypted.Location = new System.Drawing.Point(123, 94);
            this.encrypted.Margin = new System.Windows.Forms.Padding(2);
            this.encrypted.Name = "encrypted";
            this.encrypted.ReadOnly = true;
            this.encrypted.Size = new System.Drawing.Size(64, 20);
            this.encrypted.TabIndex = 10;
            this.encrypted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // encLbl
            // 
            this.encLbl.AutoSize = true;
            this.encLbl.Location = new System.Drawing.Point(11, 97);
            this.encLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.encLbl.Name = "encLbl";
            this.encLbl.Size = new System.Drawing.Size(58, 13);
            this.encLbl.TabIndex = 9;
            this.encLbl.Text = "Encrypted:";
            // 
            // extension
            // 
            this.extension.Location = new System.Drawing.Point(123, 118);
            this.extension.Margin = new System.Windows.Forms.Padding(2);
            this.extension.Name = "extension";
            this.extension.ReadOnly = true;
            this.extension.Size = new System.Drawing.Size(64, 20);
            this.extension.TabIndex = 8;
            this.extension.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // extLbl
            // 
            this.extLbl.AutoSize = true;
            this.extLbl.Location = new System.Drawing.Point(11, 121);
            this.extLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.extLbl.Name = "extLbl";
            this.extLbl.Size = new System.Drawing.Size(56, 13);
            this.extLbl.TabIndex = 7;
            this.extLbl.Text = "Extension:";
            // 
            // size
            // 
            this.size.Location = new System.Drawing.Point(85, 70);
            this.size.Margin = new System.Windows.Forms.Padding(2);
            this.size.Name = "size";
            this.size.ReadOnly = true;
            this.size.Size = new System.Drawing.Size(102, 20);
            this.size.TabIndex = 6;
            this.size.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sizeLbl
            // 
            this.sizeLbl.AutoSize = true;
            this.sizeLbl.Location = new System.Drawing.Point(11, 73);
            this.sizeLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.sizeLbl.Name = "sizeLbl";
            this.sizeLbl.Size = new System.Drawing.Size(30, 13);
            this.sizeLbl.TabIndex = 5;
            this.sizeLbl.Text = "Size:";
            // 
            // offset
            // 
            this.offset.Location = new System.Drawing.Point(85, 46);
            this.offset.Margin = new System.Windows.Forms.Padding(2);
            this.offset.Name = "offset";
            this.offset.ReadOnly = true;
            this.offset.Size = new System.Drawing.Size(102, 20);
            this.offset.TabIndex = 4;
            this.offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // offsetLbl
            // 
            this.offsetLbl.AutoSize = true;
            this.offsetLbl.Location = new System.Drawing.Point(11, 49);
            this.offsetLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.offsetLbl.Name = "offsetLbl";
            this.offsetLbl.Size = new System.Drawing.Size(38, 13);
            this.offsetLbl.TabIndex = 3;
            this.offsetLbl.Text = "Offset:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 87);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 2;
            // 
            // dataId
            // 
            this.dataId.Location = new System.Drawing.Point(85, 22);
            this.dataId.Margin = new System.Windows.Forms.Padding(2);
            this.dataId.Name = "dataId";
            this.dataId.ReadOnly = true;
            this.dataId.Size = new System.Drawing.Size(102, 20);
            this.dataId.TabIndex = 1;
            this.dataId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dataId_lbl
            // 
            this.dataId_lbl.AutoSize = true;
            this.dataId_lbl.Location = new System.Drawing.Point(11, 25);
            this.dataId_lbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.dataId_lbl.Name = "dataId_lbl";
            this.dataId_lbl.Size = new System.Drawing.Size(47, 13);
            this.dataId_lbl.TabIndex = 0;
            this.dataId_lbl.Text = "Data ID:";
            // 
            // extStatus
            // 
            this.extStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.extStatus.AutoSize = true;
            this.extStatus.Location = new System.Drawing.Point(557, 232);
            this.extStatus.Name = "extStatus";
            this.extStatus.Size = new System.Drawing.Size(0, 13);
            this.extStatus.TabIndex = 4;
            // 
            // search_grpBx
            // 
            this.search_grpBx.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.search_grpBx.Controls.Add(this.searchInput);
            this.search_grpBx.Location = new System.Drawing.Point(16, 381);
            this.search_grpBx.Name = "search_grpBx";
            this.search_grpBx.Size = new System.Drawing.Size(530, 55);
            this.search_grpBx.TabIndex = 5;
            this.search_grpBx.TabStop = false;
            this.search_grpBx.Text = "Search";
            // 
            // searchInput
            // 
            this.searchInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.searchInput.Enabled = false;
            this.searchInput.Location = new System.Drawing.Point(59, 20);
            this.searchInput.MaxLength = 50;
            this.searchInput.Name = "searchInput";
            this.searchInput.Size = new System.Drawing.Size(402, 20);
            this.searchInput.TabIndex = 0;
            this.searchInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.searchInput.TextChanged += new System.EventHandler(this.searchInput_TextChanged);
            // 
            // Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.search_grpBx);
            this.Controls.Add(this.extStatus);
            this.Controls.Add(this.stats_grpBx);
            this.Controls.Add(this.grid_grpBx);
            this.Controls.Add(this.ext_grpBx);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Data";
            this.Size = new System.Drawing.Size(753, 448);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ext_grpBx.ResumeLayout(false);
            this.extensions_cs.ResumeLayout(false);
            this.grid_grpBx.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.grid_cs.ResumeLayout(false);
            this.stats_grpBx.ResumeLayout(false);
            this.stats_grpBx.PerformLayout();
            this.search_grpBx.ResumeLayout(false);
            this.search_grpBx.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ts_file_load;
        private System.Windows.Forms.ToolStripProgressBar ts_progress;
        private System.Windows.Forms.GroupBox ext_grpBx;
        private System.Windows.Forms.TreeView extensions;
        private System.Windows.Forms.GroupBox grid_grpBx;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.GroupBox stats_grpBx;
        private System.Windows.Forms.TextBox encrypted;
        private System.Windows.Forms.Label encLbl;
        private System.Windows.Forms.TextBox extension;
        private System.Windows.Forms.Label extLbl;
        private System.Windows.Forms.TextBox size;
        private System.Windows.Forms.Label sizeLbl;
        private System.Windows.Forms.TextBox offset;
        private System.Windows.Forms.Label offsetLbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dataId;
        private System.Windows.Forms.Label dataId_lbl;
        private System.Windows.Forms.ContextMenuStrip grid_cs;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Label extStatus;
        private System.Windows.Forms.ToolStripMenuItem grid_cs_export;
        private System.Windows.Forms.ToolStripMenuItem grid_cs_compare;
        private System.Windows.Forms.ContextMenuStrip extensions_cs;
        private System.Windows.Forms.ToolStripMenuItem extensions_cs_export;
        private System.Windows.Forms.ToolStripButton ts_file_new;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel ts_status;
        private System.Windows.Forms.ToolStripMenuItem grid_cs_insert;
        private System.Windows.Forms.GroupBox search_grpBx;
        private System.Windows.Forms.TextBox searchInput;
        private System.Windows.Forms.TextBox uploadPath;
        private System.Windows.Forms.Label upPathLbl;
        private System.Windows.Forms.ToolStripButton ts_file_rebuild;
        private System.Windows.Forms.DataGridViewTextBoxColumn ig_name;
        private System.Windows.Forms.ToolStripMenuItem grid_cs_delete;
    }
}
