
namespace Grimoire.Tabs.Styles
{
    partial class RDB2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RDB2));
            this.ts = new System.Windows.Forms.ToolStrip();
            this.ts_load = new System.Windows.Forms.ToolStripDropDownButton();
            this.ts_load_rdb = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_load_sql = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save = new System.Windows.Forms.ToolStripDropDownButton();
            this.ts_save_file = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save_file_rdb = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save_file_sql = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save_file_sql_insert = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save_file_sql_update = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save_file_csv = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save_sql = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_encLbl = new System.Windows.Forms.ToolStripLabel();
            this.ts_enc_list = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_struct_status = new System.Windows.Forms.ToolStripLabel();
            this.ts_prog = new System.Windows.Forms.ToolStripProgressBar();
            this.ts_sel_struct_btn = new System.Windows.Forms.ToolStripButton();
            this.grid = new System.Windows.Forms.DataGridView();
            this.gridCMS = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.rows_txBx = new System.Windows.Forms.TextBox();
            this.date_txBx = new System.Windows.Forms.TextBox();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_search = new System.Windows.Forms.ToolStripButton();
            this.gridCMS_clear = new System.Windows.Forms.ToolStripMenuItem();
            this.ts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.gridCMS.SuspendLayout();
            this.SuspendLayout();
            // 
            // ts
            // 
            this.ts.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_load,
            this.ts_save,
            this.toolStripSeparator1,
            this.ts_search,
            this.toolStripSeparator3,
            this.ts_encLbl,
            this.ts_enc_list,
            this.toolStripSeparator2,
            this.ts_struct_status,
            this.ts_prog,
            this.ts_sel_struct_btn});
            this.ts.Location = new System.Drawing.Point(0, 0);
            this.ts.Name = "ts";
            this.ts.Size = new System.Drawing.Size(780, 25);
            this.ts.TabIndex = 26;
            this.ts.Text = "toolStrip1";
            // 
            // ts_load
            // 
            this.ts_load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_load.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_load_rdb,
            this.ts_load_sql});
            this.ts_load.Enabled = false;
            this.ts_load.Image = ((System.Drawing.Image)(resources.GetObject("ts_load.Image")));
            this.ts_load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_load.Name = "ts_load";
            this.ts_load.Size = new System.Drawing.Size(46, 22);
            this.ts_load.Text = "Load";
            // 
            // ts_load_rdb
            // 
            this.ts_load_rdb.Name = "ts_load_rdb";
            this.ts_load_rdb.Size = new System.Drawing.Size(96, 22);
            this.ts_load_rdb.Text = "RDB";
            this.ts_load_rdb.Click += new System.EventHandler(this.TS_Load_Click);
            // 
            // ts_load_sql
            // 
            this.ts_load_sql.Name = "ts_load_sql";
            this.ts_load_sql.Size = new System.Drawing.Size(96, 22);
            this.ts_load_sql.Text = "SQL";
            this.ts_load_sql.Click += new System.EventHandler(this.ts_load_sql_Click);
            // 
            // ts_save
            // 
            this.ts_save.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_save.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_save_file,
            this.ts_save_sql});
            this.ts_save.Enabled = false;
            this.ts_save.Image = ((System.Drawing.Image)(resources.GetObject("ts_save.Image")));
            this.ts_save.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_save.Name = "ts_save";
            this.ts_save.ShowDropDownArrow = false;
            this.ts_save.Size = new System.Drawing.Size(35, 22);
            this.ts_save.Text = "Save";
            // 
            // ts_save_file
            // 
            this.ts_save_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_save_file_rdb,
            this.ts_save_file_sql,
            this.ts_save_file_csv});
            this.ts_save_file.Name = "ts_save_file";
            this.ts_save_file.Size = new System.Drawing.Size(95, 22);
            this.ts_save_file.Text = "File";
            this.ts_save_file.Click += new System.EventHandler(this.ts_save_file_Click);
            // 
            // ts_save_file_rdb
            // 
            this.ts_save_file_rdb.Name = "ts_save_file_rdb";
            this.ts_save_file_rdb.Size = new System.Drawing.Size(96, 22);
            this.ts_save_file_rdb.Text = "RDB";
            this.ts_save_file_rdb.Click += new System.EventHandler(this.TS_Save_File_RDB_Click);
            // 
            // ts_save_file_sql
            // 
            this.ts_save_file_sql.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_save_file_sql_insert,
            this.ts_save_file_sql_update});
            this.ts_save_file_sql.Name = "ts_save_file_sql";
            this.ts_save_file_sql.Size = new System.Drawing.Size(96, 22);
            this.ts_save_file_sql.Text = "SQL";
            this.ts_save_file_sql.Click += new System.EventHandler(this.ts_save_file_sql_Click);
            // 
            // ts_save_file_sql_insert
            // 
            this.ts_save_file_sql_insert.Name = "ts_save_file_sql_insert";
            this.ts_save_file_sql_insert.Size = new System.Drawing.Size(112, 22);
            this.ts_save_file_sql_insert.Text = "Insert";
            this.ts_save_file_sql_insert.Click += new System.EventHandler(this.ts_save_file_sql_insert_Click);
            // 
            // ts_save_file_sql_update
            // 
            this.ts_save_file_sql_update.Name = "ts_save_file_sql_update";
            this.ts_save_file_sql_update.Size = new System.Drawing.Size(112, 22);
            this.ts_save_file_sql_update.Text = "Update";
            this.ts_save_file_sql_update.Click += new System.EventHandler(this.ts_save_file_sql_update_Click);
            // 
            // ts_save_file_csv
            // 
            this.ts_save_file_csv.Name = "ts_save_file_csv";
            this.ts_save_file_csv.Size = new System.Drawing.Size(96, 22);
            this.ts_save_file_csv.Text = "CSV";
            this.ts_save_file_csv.Click += new System.EventHandler(this.ts_save_file_csv_Click);
            // 
            // ts_save_sql
            // 
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
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ts_struct_status
            // 
            this.ts_struct_status.Name = "ts_struct_status";
            this.ts_struct_status.Size = new System.Drawing.Size(0, 22);
            // 
            // ts_prog
            // 
            this.ts_prog.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_prog.Name = "ts_prog";
            this.ts_prog.Size = new System.Drawing.Size(100, 22);
            // 
            // ts_sel_struct_btn
            // 
            this.ts_sel_struct_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_sel_struct_btn.Image = ((System.Drawing.Image)(resources.GetObject("ts_sel_struct_btn.Image")));
            this.ts_sel_struct_btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_sel_struct_btn.Name = "ts_sel_struct_btn";
            this.ts_sel_struct_btn.Size = new System.Drawing.Size(93, 22);
            this.ts_sel_struct_btn.Text = "Select Structure";
            this.ts_sel_struct_btn.Click += new System.EventHandler(this.ts_sel_struct_btn_Click);
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.AllowUserToDeleteRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.ContextMenuStrip = this.gridCMS;
            this.grid.Location = new System.Drawing.Point(3, 28);
            this.grid.Name = "grid";
            this.grid.Size = new System.Drawing.Size(774, 391);
            this.grid.TabIndex = 28;
            this.grid.VirtualMode = true;
            this.grid.CellValueNeeded += new System.Windows.Forms.DataGridViewCellValueEventHandler(this.rowGrid_CellValueNeeded);
            // 
            // gridCMS
            // 
            this.gridCMS.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gridCMS_clear});
            this.gridCMS.Name = "gridCMS";
            this.gridCMS.Size = new System.Drawing.Size(102, 26);
            // 
            // rows_txBx
            // 
            this.rows_txBx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rows_txBx.BackColor = System.Drawing.Color.LightGray;
            this.rows_txBx.Enabled = false;
            this.rows_txBx.Location = new System.Drawing.Point(708, 425);
            this.rows_txBx.MaxLength = 7;
            this.rows_txBx.Name = "rows_txBx";
            this.rows_txBx.ReadOnly = true;
            this.rows_txBx.Size = new System.Drawing.Size(69, 20);
            this.rows_txBx.TabIndex = 35;
            this.rows_txBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // date_txBx
            // 
            this.date_txBx.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.date_txBx.BackColor = System.Drawing.Color.LightGray;
            this.date_txBx.Enabled = false;
            this.date_txBx.Location = new System.Drawing.Point(3, 425);
            this.date_txBx.MaxLength = 7;
            this.date_txBx.Name = "date_txBx";
            this.date_txBx.ReadOnly = true;
            this.date_txBx.Size = new System.Drawing.Size(69, 20);
            this.date_txBx.TabIndex = 36;
            this.date_txBx.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // ts_search
            // 
            this.ts_search.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_search.Image = ((System.Drawing.Image)(resources.GetObject("ts_search.Image")));
            this.ts_search.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_search.Name = "ts_search";
            this.ts_search.Size = new System.Drawing.Size(46, 22);
            this.ts_search.Text = "Search";
            this.ts_search.Click += new System.EventHandler(this.ts_search_Click);
            // 
            // gridCMS_clear
            // 
            this.gridCMS_clear.Enabled = false;
            this.gridCMS_clear.Name = "gridCMS_clear";
            this.gridCMS_clear.Size = new System.Drawing.Size(180, 22);
            this.gridCMS_clear.Text = "Clear";
            // 
            // RDB2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.date_txBx);
            this.Controls.Add(this.rows_txBx);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.ts);
            this.Name = "RDB2";
            this.Size = new System.Drawing.Size(780, 454);
            this.ts.ResumeLayout(false);
            this.ts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.gridCMS.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip ts;
        private System.Windows.Forms.ToolStripDropDownButton ts_load;
        private System.Windows.Forms.ToolStripDropDownButton ts_save;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel ts_encLbl;
        private System.Windows.Forms.ToolStripComboBox ts_enc_list;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel ts_struct_status;
        private System.Windows.Forms.ToolStripProgressBar ts_prog;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.ToolStripButton ts_sel_struct_btn;
        private System.Windows.Forms.ContextMenuStrip gridCMS;
        private System.Windows.Forms.TextBox rows_txBx;
        private System.Windows.Forms.TextBox date_txBx;
        private System.Windows.Forms.ToolStripMenuItem ts_load_sql;
        private System.Windows.Forms.ToolStripMenuItem ts_load_rdb;
        private System.Windows.Forms.ToolStripMenuItem ts_save_file;
        private System.Windows.Forms.ToolStripMenuItem ts_save_file_rdb;
        private System.Windows.Forms.ToolStripMenuItem ts_save_file_sql;
        private System.Windows.Forms.ToolStripMenuItem ts_save_file_csv;
        private System.Windows.Forms.ToolStripMenuItem ts_save_sql;
        private System.Windows.Forms.ToolStripMenuItem ts_save_file_sql_insert;
        private System.Windows.Forms.ToolStripMenuItem ts_save_file_sql_update;
        private System.Windows.Forms.ToolStripButton ts_search;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem gridCMS_clear;
    }
}
