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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(rdbTab));
            this.grid = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ts_load = new System.Windows.Forms.ToolStripDropDownButton();
            this.ts_load_file = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_load_sql = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_load_data = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save = new System.Windows.Forms.ToolStripDropDownButton();
            this.ts_save_file = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_save_sql = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ts_enc_list = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.ts_struct_list = new System.Windows.Forms.ToolStripComboBox();
            this.ts_struct_status = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_prog = new System.Windows.Forms.ToolStripProgressBar();
            this.ts_save_enc = new System.Windows.Forms.ToolStripButton();
            this.ts_save_w_ascii = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.toolStrip1.SuspendLayout();
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
            this.grid.Location = new System.Drawing.Point(0, 32);
            this.grid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.grid.Name = "grid";
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.grid.Size = new System.Drawing.Size(1040, 527);
            this.grid.TabIndex = 17;
            this.grid.VirtualMode = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_load,
            this.ts_save,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.ts_enc_list,
            this.toolStripSeparator2,
            this.toolStripLabel2,
            this.ts_struct_list,
            this.ts_struct_status,
            this.toolStripSeparator3,
            this.ts_prog,
            this.ts_save_enc,
            this.ts_save_w_ascii,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1040, 30);
            this.toolStrip1.TabIndex = 25;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ts_load
            // 
            this.ts_load.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_load.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_load_file,
            this.ts_load_sql,
            this.ts_load_data});
            this.ts_load.Image = ((System.Drawing.Image)(resources.GetObject("ts_load.Image")));
            this.ts_load.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_load.Name = "ts_load";
            this.ts_load.Size = new System.Drawing.Size(56, 27);
            this.ts_load.Text = "Load";
            // 
            // ts_load_file
            // 
            this.ts_load_file.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_load_file.Name = "ts_load_file";
            this.ts_load_file.Size = new System.Drawing.Size(116, 26);
            this.ts_load_file.Text = "File";
            this.ts_load_file.Click += new System.EventHandler(this.ts_load_file_Click);
            // 
            // ts_load_sql
            // 
            this.ts_load_sql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_load_sql.Name = "ts_load_sql";
            this.ts_load_sql.Size = new System.Drawing.Size(116, 26);
            this.ts_load_sql.Text = "SQL";
            this.ts_load_sql.Click += new System.EventHandler(this.ts_load_sql_Click);
            // 
            // ts_load_data
            // 
            this.ts_load_data.Name = "ts_load_data";
            this.ts_load_data.Size = new System.Drawing.Size(116, 26);
            this.ts_load_data.Text = "Data";
            this.ts_load_data.Click += new System.EventHandler(this.ts_load_data_Click);
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
            this.ts_save.Size = new System.Drawing.Size(54, 27);
            this.ts_save.Text = "Save";
            // 
            // ts_save_file
            // 
            this.ts_save_file.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_save_file.Name = "ts_save_file";
            this.ts_save_file.Size = new System.Drawing.Size(110, 26);
            this.ts_save_file.Text = "File";
            this.ts_save_file.Click += new System.EventHandler(this.ts_save_file_Click);
            // 
            // ts_save_sql
            // 
            this.ts_save_sql.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_save_sql.Name = "ts_save_sql";
            this.ts_save_sql.Size = new System.Drawing.Size(110, 26);
            this.ts_save_sql.Text = "SQL";
            this.ts_save_sql.Click += new System.EventHandler(this.ts_save_sql_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 30);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(74, 27);
            this.toolStripLabel1.Text = "Encoding:";
            // 
            // ts_enc_list
            // 
            this.ts_enc_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ts_enc_list.Name = "ts_enc_list";
            this.ts_enc_list.Size = new System.Drawing.Size(151, 30);
            this.ts_enc_list.Click += new System.EventHandler(this.ts_enc_list_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 30);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(71, 27);
            this.toolStripLabel2.Text = "Structure:";
            // 
            // ts_struct_list
            // 
            this.ts_struct_list.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ts_struct_list.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ts_struct_list.Name = "ts_struct_list";
            this.ts_struct_list.Size = new System.Drawing.Size(200, 30);
            this.ts_struct_list.ToolTipText = "List of rdb structure files";
            this.ts_struct_list.SelectedIndexChanged += new System.EventHandler(this.ts_struct_list_SelectedIndexChanged);
            // 
            // ts_struct_status
            // 
            this.ts_struct_status.Name = "ts_struct_status";
            this.ts_struct_status.Size = new System.Drawing.Size(0, 27);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 30);
            // 
            // ts_prog
            // 
            this.ts_prog.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_prog.Name = "ts_prog";
            this.ts_prog.Size = new System.Drawing.Size(100, 27);
            // 
            // ts_save_enc
            // 
            this.ts_save_enc.Checked = true;
            this.ts_save_enc.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ts_save_enc.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_save_enc.Image = ((System.Drawing.Image)(resources.GetObject("ts_save_enc.Image")));
            this.ts_save_enc.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_save_enc.Name = "ts_save_enc";
            this.ts_save_enc.Size = new System.Drawing.Size(79, 27);
            this.ts_save_enc.Text = "Encrypted";
            this.ts_save_enc.Click += new System.EventHandler(this.ts_save_enc_Click);
            // 
            // ts_save_w_ascii
            // 
            this.ts_save_w_ascii.Checked = true;
            this.ts_save_w_ascii.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ts_save_w_ascii.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_save_w_ascii.Image = ((System.Drawing.Image)(resources.GetObject("ts_save_w_ascii.Image")));
            this.ts_save_w_ascii.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_save_w_ascii.Name = "ts_save_w_ascii";
            this.ts_save_w_ascii.Size = new System.Drawing.Size(58, 27);
            this.ts_save_w_ascii.Text = "(ASCII)";
            this.ts_save_w_ascii.Click += new System.EventHandler(this.ts_save_w_ascii_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 30);
            // 
            // rdbTab
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.grid);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "rdbTab";
            this.Size = new System.Drawing.Size(1040, 559);
            this.Load += new System.EventHandler(this.rdbTab_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripDropDownButton ts_load;
        private System.Windows.Forms.ToolStripMenuItem ts_load_file;
        private System.Windows.Forms.ToolStripMenuItem ts_load_sql;
        private System.Windows.Forms.ToolStripDropDownButton ts_save;
        private System.Windows.Forms.ToolStripMenuItem ts_save_file;
        private System.Windows.Forms.ToolStripMenuItem ts_save_sql;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripComboBox ts_struct_list;
        private System.Windows.Forms.ToolStripLabel ts_struct_status;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripProgressBar ts_prog;
        private System.Windows.Forms.ToolStripComboBox ts_enc_list;
        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.ToolStripButton ts_save_enc;
        private System.Windows.Forms.ToolStripButton ts_save_w_ascii;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem ts_load_data;
    }
}
