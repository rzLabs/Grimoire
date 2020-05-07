namespace Grimoire.Tabs.Styles
{
    partial class rdbTab
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rdbTab));
            this.grid = new System.Windows.Forms.DataGridView();
            this.ts = new System.Windows.Forms.ToolStrip();
            this.ts_load = new System.Windows.Forms.ToolStripDropDownButton();
            this.ts_load_file = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_load_sql = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save = new System.Windows.Forms.ToolStripDropDownButton();
            this.ts_save_file = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save_file_rdb = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save_file_csv = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save_file_sql = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save_sql = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_encLbl = new System.Windows.Forms.ToolStripLabel();
            this.ts_enc_list = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_structLb = new System.Windows.Forms.ToolStripLabel();
            this.ts_struct_list = new System.Windows.Forms.ToolStripComboBox();
            this.ts_struct_status = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_prog = new System.Windows.Forms.ToolStripProgressBar();
            this.ts_save_enc = new System.Windows.Forms.ToolStripButton();
            this.ts_save_w_ascii = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.grid_cs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.grid_cs_open_flag_editor = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.ts.SuspendLayout();
            this.grid_cs.SuspendLayout();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.AllowUserToResizeRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Location = new System.Drawing.Point(0, 26);
            this.grid.MultiSelect = false;
            this.grid.Name = "grid";
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grid.Size = new System.Drawing.Size(780, 428);
            this.grid.TabIndex = 17;
            this.grid.VirtualMode = true;
            this.grid.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_CellMouseClick);
            this.grid.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grid_ColumnHeaderMouseClick);
            // 
            // ts
            // 
            this.ts.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_load,
            this.ts_save,
            this.toolStripSeparator1,
            this.ts_encLbl,
            this.ts_enc_list,
            this.toolStripSeparator2,
            this.ts_structLb,
            this.ts_struct_list,
            this.ts_struct_status,
            this.toolStripSeparator3,
            this.ts_prog,
            this.ts_save_enc,
            this.ts_save_w_ascii,
            this.toolStripSeparator4});
            this.ts.Location = new System.Drawing.Point(0, 0);
            this.ts.Name = "ts";
            this.ts.Size = new System.Drawing.Size(780, 25);
            this.ts.TabIndex = 25;
            this.ts.Text = "toolStrip1";
            // 
            // ts_load
            // 
            this.ts_load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_load.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_load_file,
            this.ts_load_sql});
            this.ts_load.Image = ((System.Drawing.Image)(resources.GetObject("ts_load.Image")));
            this.ts_load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_load.Name = "ts_load";
            this.ts_load.Size = new System.Drawing.Size(46, 22);
            this.ts_load.Text = "Load";
            // 
            // ts_load_file
            // 
            this.ts_load_file.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_load_file.Name = "ts_load_file";
            this.ts_load_file.Size = new System.Drawing.Size(95, 22);
            this.ts_load_file.Text = "File";
            this.ts_load_file.Click += new System.EventHandler(this.TS_Load_File_Click);
            // 
            // ts_load_sql
            // 
            this.ts_load_sql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_load_sql.Name = "ts_load_sql";
            this.ts_load_sql.Size = new System.Drawing.Size(95, 22);
            this.ts_load_sql.Text = "SQL";
            this.ts_load_sql.Click += new System.EventHandler(this.ts_load_sql_Click);
            // 
            // ts_save
            // 
            this.ts_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_save.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_save_file,
            this.ts_save_sql});
            this.ts_save.Image = ((System.Drawing.Image)(resources.GetObject("ts_save.Image")));
            this.ts_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_save.Name = "ts_save";
            this.ts_save.Size = new System.Drawing.Size(44, 22);
            this.ts_save.Text = "Save";
            // 
            // ts_save_file
            // 
            this.ts_save_file.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_save_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_save_file_rdb,
            this.ts_save_file_csv,
            this.ts_save_file_sql});
            this.ts_save_file.Name = "ts_save_file";
            this.ts_save_file.Size = new System.Drawing.Size(95, 22);
            this.ts_save_file.Text = "File";
            // 
            // ts_save_file_rdb
            // 
            this.ts_save_file_rdb.Name = "ts_save_file_rdb";
            this.ts_save_file_rdb.Size = new System.Drawing.Size(96, 22);
            this.ts_save_file_rdb.Text = "RDB";
            this.ts_save_file_rdb.Click += new System.EventHandler(this.TS_Save_File_Click);
            // 
            // ts_save_file_csv
            // 
            this.ts_save_file_csv.Name = "ts_save_file_csv";
            this.ts_save_file_csv.Size = new System.Drawing.Size(96, 22);
            this.ts_save_file_csv.Text = "CSV";
            this.ts_save_file_csv.Click += new System.EventHandler(this.ts_save_file_csv_Click);
            // 
            // ts_save_file_sql
            // 
            this.ts_save_file_sql.Name = "ts_save_file_sql";
            this.ts_save_file_sql.Size = new System.Drawing.Size(96, 22);
            this.ts_save_file_sql.Text = "SQL";
            this.ts_save_file_sql.Click += new System.EventHandler(this.ts_save_file_sql_Click);
            // 
            // ts_save_sql
            // 
            this.ts_save_sql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_save_sql.Name = "ts_save_sql";
            this.ts_save_sql.Size = new System.Drawing.Size(95, 22);
            this.ts_save_sql.Text = "SQL";
            this.ts_save_sql.Click += new System.EventHandler(this.ts_save_sql_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ts_encLbl
            // 
            this.ts_encLbl.Name = "ts_encLbl";
            this.ts_encLbl.Size = new System.Drawing.Size(60, 22);
            this.ts_encLbl.Text = "Encoding:";
            // 
            // ts_enc_list
            // 
            this.ts_enc_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ts_enc_list.Name = "ts_enc_list";
            this.ts_enc_list.Size = new System.Drawing.Size(114, 25);
            this.ts_enc_list.Click += new System.EventHandler(this.ts_enc_list_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ts_structLb
            // 
            this.ts_structLb.Name = "ts_structLb";
            this.ts_structLb.Size = new System.Drawing.Size(58, 22);
            this.ts_structLb.Text = "Structure:";
            // 
            // ts_struct_list
            // 
            this.ts_struct_list.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ts_struct_list.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ts_struct_list.Name = "ts_struct_list";
            this.ts_struct_list.Size = new System.Drawing.Size(151, 25);
            this.ts_struct_list.ToolTipText = "List of rdb structure files";
            this.ts_struct_list.SelectedIndexChanged += new System.EventHandler(this.ts_struct_list_SelectedIndexChanged);
            // 
            // ts_struct_status
            // 
            this.ts_struct_status.Name = "ts_struct_status";
            this.ts_struct_status.Size = new System.Drawing.Size(0, 22);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // ts_prog
            // 
            this.ts_prog.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_prog.Name = "ts_prog";
            this.ts_prog.Size = new System.Drawing.Size(75, 22);
            // 
            // ts_save_enc
            // 
            this.ts_save_enc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_save_enc.Image = ((System.Drawing.Image)(resources.GetObject("ts_save_enc.Image")));
            this.ts_save_enc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_save_enc.Name = "ts_save_enc";
            this.ts_save_enc.Size = new System.Drawing.Size(64, 22);
            this.ts_save_enc.Text = "Encrypted";
            this.ts_save_enc.Click += new System.EventHandler(this.ts_save_enc_Click);
            // 
            // ts_save_w_ascii
            // 
            this.ts_save_w_ascii.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_save_w_ascii.Image = ((System.Drawing.Image)(resources.GetObject("ts_save_w_ascii.Image")));
            this.ts_save_w_ascii.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_save_w_ascii.Name = "ts_save_w_ascii";
            this.ts_save_w_ascii.Size = new System.Drawing.Size(47, 22);
            this.ts_save_w_ascii.Text = "(ASCII)";
            this.ts_save_w_ascii.Click += new System.EventHandler(this.ts_save_w_ascii_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // grid_cs
            // 
            this.grid_cs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grid_cs_open_flag_editor});
            this.grid_cs.Name = "grid_cs";
            this.grid_cs.Size = new System.Drawing.Size(180, 26);
            // 
            // grid_cs_open_flag_editor
            // 
            this.grid_cs_open_flag_editor.Name = "grid_cs_open_flag_editor";
            this.grid_cs_open_flag_editor.Size = new System.Drawing.Size(179, 22);
            this.grid_cs_open_flag_editor.Text = "Open w/ Flag Editor";
            this.grid_cs_open_flag_editor.Click += new System.EventHandler(this.grid_cs_open_flag_editor_Click);
            // 
            // rdbTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ts);
            this.Controls.Add(this.grid);
            this.Name = "rdbTab";
            this.Size = new System.Drawing.Size(780, 454);
            this.Load += new System.EventHandler(this.rdbTab_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ts.ResumeLayout(false);
            this.ts.PerformLayout();
            this.grid_cs.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip ts;
        private System.Windows.Forms.ToolStripDropDownButton ts_load;
        private System.Windows.Forms.ToolStripMenuItem ts_load_file;
        private System.Windows.Forms.ToolStripMenuItem ts_load_sql;
        private System.Windows.Forms.ToolStripDropDownButton ts_save;
        private System.Windows.Forms.ToolStripMenuItem ts_save_file;
        private System.Windows.Forms.ToolStripMenuItem ts_save_sql;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel ts_encLbl;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel ts_structLb;
        private System.Windows.Forms.ToolStripComboBox ts_struct_list;
        private System.Windows.Forms.ToolStripLabel ts_struct_status;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripProgressBar ts_prog;
        private System.Windows.Forms.ToolStripComboBox ts_enc_list;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.ToolStripButton ts_save_enc;
        private System.Windows.Forms.ToolStripButton ts_save_w_ascii;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ts_save_file_rdb;
        private System.Windows.Forms.ToolStripMenuItem ts_save_file_csv;
        private System.Windows.Forms.ToolStripMenuItem ts_save_file_sql;
        private System.Windows.Forms.ContextMenuStrip grid_cs;
        private System.Windows.Forms.ToolStripMenuItem grid_cs_open_flag_editor;
    }
}
