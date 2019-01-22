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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.extensions = new System.Windows.Forms.TreeView();
            this.extensions_cs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extensions_cs_export = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grid = new System.Windows.Forms.DataGridView();
            this.ig_name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grid_cs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.grid_cs_compare = new System.Windows.Forms.ToolStripMenuItem();
            this.grid_cs_export = new System.Windows.Forms.ToolStripMenuItem();
            this.grid_cs_insert = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.uploadPath = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.encrypted = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.extension = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.size = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.offset = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dataId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.extStatus = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.searchInput = new System.Windows.Forms.TextBox();
            this.ts_file_rebuild = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.extensions_cs.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.grid_cs.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
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
            this.toolStrip1.Size = new System.Drawing.Size(1004, 30);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ts_file_load
            // 
            this.ts_file_load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_file_load.Image = ((System.Drawing.Image)(resources.GetObject("ts_file_load.Image")));
            this.ts_file_load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_file_load.Name = "ts_file_load";
            this.ts_file_load.Size = new System.Drawing.Size(46, 27);
            this.ts_file_load.Text = "Load";
            this.ts_file_load.Click += new System.EventHandler(this.ts_file_load_Click);
            // 
            // ts_progress
            // 
            this.ts_progress.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_progress.Name = "ts_progress";
            this.ts_progress.Size = new System.Drawing.Size(100, 27);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // ts_file_new
            // 
            this.ts_file_new.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_file_new.Image = ((System.Drawing.Image)(resources.GetObject("ts_file_new.Image")));
            this.ts_file_new.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_file_new.Name = "ts_file_new";
            this.ts_file_new.Size = new System.Drawing.Size(43, 27);
            this.ts_file_new.Text = "New";
            this.ts_file_new.Click += new System.EventHandler(this.ts_file_new_Click);
            // 
            // ts_status
            // 
            this.ts_status.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_status.Name = "ts_status";
            this.ts_status.Size = new System.Drawing.Size(0, 27);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.extensions);
            this.groupBox1.Location = new System.Drawing.Point(736, 30);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox1.Size = new System.Drawing.Size(268, 256);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Extensions";
            // 
            // extensions
            // 
            this.extensions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.extensions.Location = new System.Drawing.Point(3, 17);
            this.extensions.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.extensions.Name = "extensions";
            this.extensions.Size = new System.Drawing.Size(262, 237);
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
            this.extensions_cs.Size = new System.Drawing.Size(122, 28);
            // 
            // extensions_cs_export
            // 
            this.extensions_cs_export.Enabled = false;
            this.extensions_cs_export.Name = "extensions_cs_export";
            this.extensions_cs_export.Size = new System.Drawing.Size(121, 24);
            this.extensions_cs_export.Text = "Export";
            this.extensions_cs_export.Click += new System.EventHandler(this.extensions_cs_export_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.grid);
            this.groupBox2.Location = new System.Drawing.Point(19, 30);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox2.Size = new System.Drawing.Size(712, 436);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Index";
            // 
            // grid
            // 
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ig_name});
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.Location = new System.Drawing.Point(3, 17);
            this.grid.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.RowTemplate.Height = 24;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(706, 417);
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
            this.grid_cs.Enabled = false;
            this.grid_cs.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.grid_cs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grid_cs_compare,
            this.grid_cs_export,
            this.grid_cs_insert});
            this.grid_cs.Name = "grid_cs";
            this.grid_cs.Size = new System.Drawing.Size(140, 76);
            // 
            // grid_cs_compare
            // 
            this.grid_cs_compare.Enabled = false;
            this.grid_cs_compare.Name = "grid_cs_compare";
            this.grid_cs_compare.Size = new System.Drawing.Size(139, 24);
            this.grid_cs_compare.Text = "Compare";
            this.grid_cs_compare.Click += new System.EventHandler(this.grid_cs_compare_Click);
            // 
            // grid_cs_export
            // 
            this.grid_cs_export.Enabled = false;
            this.grid_cs_export.Name = "grid_cs_export";
            this.grid_cs_export.Size = new System.Drawing.Size(139, 24);
            this.grid_cs_export.Text = "Export";
            this.grid_cs_export.Click += new System.EventHandler(this.grid_cs_export_Click);
            // 
            // grid_cs_insert
            // 
            this.grid_cs_insert.Enabled = false;
            this.grid_cs_insert.Name = "grid_cs_insert";
            this.grid_cs_insert.Size = new System.Drawing.Size(139, 24);
            this.grid_cs_insert.Text = "Insert";
            this.grid_cs_insert.Click += new System.EventHandler(this.grid_cs_insert_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.uploadPath);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.encrypted);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.extension);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.size);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.offset);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.dataId);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(736, 315);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox3.Size = new System.Drawing.Size(265, 222);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Statistics";
            // 
            // uploadPath
            // 
            this.uploadPath.Location = new System.Drawing.Point(164, 175);
            this.uploadPath.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.uploadPath.Name = "uploadPath";
            this.uploadPath.ReadOnly = true;
            this.uploadPath.Size = new System.Drawing.Size(84, 22);
            this.uploadPath.TabIndex = 12;
            this.uploadPath.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(15, 178);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(90, 17);
            this.label7.TabIndex = 11;
            this.label7.Text = "Upload Path:";
            // 
            // encrypted
            // 
            this.encrypted.Location = new System.Drawing.Point(164, 116);
            this.encrypted.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.encrypted.Name = "encrypted";
            this.encrypted.ReadOnly = true;
            this.encrypted.Size = new System.Drawing.Size(84, 22);
            this.encrypted.TabIndex = 10;
            this.encrypted.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(15, 119);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "Encrypted:";
            // 
            // extension
            // 
            this.extension.Location = new System.Drawing.Point(164, 145);
            this.extension.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.extension.Name = "extension";
            this.extension.ReadOnly = true;
            this.extension.Size = new System.Drawing.Size(84, 22);
            this.extension.TabIndex = 8;
            this.extension.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(15, 149);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(73, 17);
            this.label5.TabIndex = 7;
            this.label5.Text = "Extension:";
            // 
            // size
            // 
            this.size.Location = new System.Drawing.Point(113, 86);
            this.size.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.size.Name = "size";
            this.size.ReadOnly = true;
            this.size.Size = new System.Drawing.Size(135, 22);
            this.size.TabIndex = 6;
            this.size.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 90);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 17);
            this.label4.TabIndex = 5;
            this.label4.Text = "Size:";
            // 
            // offset
            // 
            this.offset.Location = new System.Drawing.Point(113, 57);
            this.offset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.offset.Name = "offset";
            this.offset.ReadOnly = true;
            this.offset.Size = new System.Drawing.Size(135, 22);
            this.offset.TabIndex = 4;
            this.offset.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 60);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 17);
            this.label3.TabIndex = 3;
            this.label3.Text = "Offset:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 2;
            // 
            // dataId
            // 
            this.dataId.Location = new System.Drawing.Point(113, 27);
            this.dataId.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dataId.Name = "dataId";
            this.dataId.ReadOnly = true;
            this.dataId.Size = new System.Drawing.Size(135, 22);
            this.dataId.TabIndex = 1;
            this.dataId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data ID:";
            // 
            // extStatus
            // 
            this.extStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.extStatus.AutoSize = true;
            this.extStatus.Location = new System.Drawing.Point(743, 286);
            this.extStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.extStatus.Name = "extStatus";
            this.extStatus.Size = new System.Drawing.Size(0, 17);
            this.extStatus.TabIndex = 4;
            // 
            // groupBox4
            // 
            this.groupBox4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox4.Controls.Add(this.searchInput);
            this.groupBox4.Location = new System.Drawing.Point(21, 469);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.groupBox4.Size = new System.Drawing.Size(707, 68);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Search";
            // 
            // searchInput
            // 
            this.searchInput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.searchInput.Enabled = false;
            this.searchInput.Location = new System.Drawing.Point(79, 25);
            this.searchInput.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.searchInput.MaxLength = 50;
            this.searchInput.Name = "searchInput";
            this.searchInput.Size = new System.Drawing.Size(535, 22);
            this.searchInput.TabIndex = 0;
            this.searchInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.searchInput.TextChanged += new System.EventHandler(this.searchInput_TextChanged);
            // 
            // ts_file_rebuild
            // 
            this.ts_file_rebuild.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_file_rebuild.Image = ((System.Drawing.Image)(resources.GetObject("ts_file_rebuild.Image")));
            this.ts_file_rebuild.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_file_rebuild.Name = "ts_file_rebuild";
            this.ts_file_rebuild.Size = new System.Drawing.Size(64, 27);
            this.ts_file_rebuild.Text = "Rebuild";
            this.ts_file_rebuild.Visible = false;
            this.ts_file_rebuild.Click += new System.EventHandler(this.ts_file_rebuild_Click);
            // 
            // Data
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.extStatus);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Data";
            this.Size = new System.Drawing.Size(1004, 551);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.extensions_cs.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.grid_cs.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton ts_file_load;
        private System.Windows.Forms.ToolStripProgressBar ts_progress;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView extensions;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox encrypted;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox extension;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox size;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox offset;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dataId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ig_name;
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
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.TextBox searchInput;
        private System.Windows.Forms.TextBox uploadPath;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ToolStripButton ts_file_rebuild;
    }
}
