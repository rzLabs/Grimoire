
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StructureSelect));
            this.struct_select_btn = new System.Windows.Forms.Button();
            this.remSelect_chkBx = new System.Windows.Forms.CheckBox();
            this.structGrid = new System.Windows.Forms.DataGridView();
            this.ts_main = new System.Windows.Forms.ToolStrip();
            this.ts_sel_epics_btn = new System.Windows.Forms.ToolStripButton();
            this.ts_reload_struct_btn = new System.Windows.Forms.ToolStripButton();
            this.ts_status = new System.Windows.Forms.ToolStrip();
            this.ts_prgbar = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_status_lb = new System.Windows.Forms.ToolStripLabel();
            this.structName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.fileName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.structEpic = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.structAuthor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.structGrid)).BeginInit();
            this.ts_main.SuspendLayout();
            this.ts_status.SuspendLayout();
            this.SuspendLayout();
            // 
            // struct_select_btn
            // 
            this.struct_select_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.struct_select_btn.Location = new System.Drawing.Point(544, 334);
            this.struct_select_btn.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.struct_select_btn.Name = "struct_select_btn";
            this.struct_select_btn.Size = new System.Drawing.Size(88, 27);
            this.struct_select_btn.TabIndex = 4;
            this.struct_select_btn.Text = "Select";
            this.struct_select_btn.UseVisualStyleBackColor = true;
            // 
            // remSelect_chkBx
            // 
            this.remSelect_chkBx.Location = new System.Drawing.Point(0, 0);
            this.remSelect_chkBx.Name = "remSelect_chkBx";
            this.remSelect_chkBx.Size = new System.Drawing.Size(104, 24);
            this.remSelect_chkBx.TabIndex = 0;
            // 
            // structGrid
            // 
            this.structGrid.AllowUserToAddRows = false;
            this.structGrid.AllowUserToDeleteRows = false;
            this.structGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.structGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.structGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.structName,
            this.fileName,
            this.structEpic,
            this.structAuthor});
            this.structGrid.Location = new System.Drawing.Point(0, 28);
            this.structGrid.Name = "structGrid";
            this.structGrid.ReadOnly = true;
            this.structGrid.RowTemplate.Height = 25;
            this.structGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.structGrid.Size = new System.Drawing.Size(707, 360);
            this.structGrid.TabIndex = 0;
            this.structGrid.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.structGrid_CellDoubleClick);
            // 
            // ts_main
            // 
            this.ts_main.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_sel_epics_btn,
            this.ts_reload_struct_btn});
            this.ts_main.Location = new System.Drawing.Point(0, 0);
            this.ts_main.Name = "ts_main";
            this.ts_main.Size = new System.Drawing.Size(707, 25);
            this.ts_main.TabIndex = 1;
            this.ts_main.Text = "toolStrip1";
            // 
            // ts_sel_epics_btn
            // 
            this.ts_sel_epics_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_sel_epics_btn.Image = ((System.Drawing.Image)(resources.GetObject("ts_sel_epics_btn.Image")));
            this.ts_sel_epics_btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_sel_epics_btn.Name = "ts_sel_epics_btn";
            this.ts_sel_epics_btn.Size = new System.Drawing.Size(72, 22);
            this.ts_sel_epics_btn.Text = "Select Epics";
            this.ts_sel_epics_btn.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.ts_sel_epics_btn.Click += new System.EventHandler(this.ts_sel_epics_btn_Click);
            // 
            // ts_reload_struct_btn
            // 
            this.ts_reload_struct_btn.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_reload_struct_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_reload_struct_btn.Image = ((System.Drawing.Image)(resources.GetObject("ts_reload_struct_btn.Image")));
            this.ts_reload_struct_btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_reload_struct_btn.Name = "ts_reload_struct_btn";
            this.ts_reload_struct_btn.Size = new System.Drawing.Size(103, 22);
            this.ts_reload_struct_btn.Text = "Reload Structures";
            this.ts_reload_struct_btn.Click += new System.EventHandler(this.ts_reload_struct_btn_Click);
            // 
            // ts_status
            // 
            this.ts_status.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ts_status.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_prgbar,
            this.toolStripSeparator1,
            this.ts_status_lb});
            this.ts_status.Location = new System.Drawing.Point(0, 391);
            this.ts_status.Name = "ts_status";
            this.ts_status.Size = new System.Drawing.Size(707, 25);
            this.ts_status.TabIndex = 2;
            this.ts_status.Text = "toolStrip2";
            // 
            // ts_prgbar
            // 
            this.ts_prgbar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.ts_prgbar.Name = "ts_prgbar";
            this.ts_prgbar.Size = new System.Drawing.Size(100, 22);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // ts_status_lb
            // 
            this.ts_status_lb.Name = "ts_status_lb";
            this.ts_status_lb.Size = new System.Drawing.Size(0, 22);
            // 
            // structName
            // 
            this.structName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.structName.HeaderText = "Name";
            this.structName.Name = "structName";
            this.structName.ReadOnly = true;
            // 
            // fileName
            // 
            this.fileName.HeaderText = "FileName";
            this.fileName.Name = "fileName";
            this.fileName.ReadOnly = true;
            this.fileName.Visible = false;
            // 
            // structEpic
            // 
            this.structEpic.HeaderText = "Epic";
            this.structEpic.Name = "structEpic";
            this.structEpic.ReadOnly = true;
            // 
            // structAuthor
            // 
            this.structAuthor.HeaderText = "Author";
            this.structAuthor.Name = "structAuthor";
            this.structAuthor.ReadOnly = true;
            // 
            // StructureSelect
            // 
            this.ClientSize = new System.Drawing.Size(707, 416);
            this.Controls.Add(this.ts_status);
            this.Controls.Add(this.ts_main);
            this.Controls.Add(this.structGrid);
            this.Name = "StructureSelect";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Structure Select";
            ((System.ComponentModel.ISupportInitialize)(this.structGrid)).EndInit();
            this.ts_main.ResumeLayout(false);
            this.ts_main.PerformLayout();
            this.ts_status.ResumeLayout(false);
            this.ts_status.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button struct_select_btn;
        private System.Windows.Forms.CheckBox remSelect_chkBx;
        private System.Windows.Forms.DataGridView structGrid;
        private System.Windows.Forms.ToolStrip ts_main;
        private System.Windows.Forms.ToolStrip ts_status;
        private System.Windows.Forms.ToolStripProgressBar ts_prgbar;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel ts_status_lb;
        private System.Windows.Forms.ToolStripButton ts_sel_epics_btn;
        private System.Windows.Forms.ToolStripButton ts_reload_struct_btn;
        private System.Windows.Forms.DataGridViewTextBoxColumn structName;
        private System.Windows.Forms.DataGridViewTextBoxColumn fileName;
        private System.Windows.Forms.DataGridViewTextBoxColumn structEpic;
        private System.Windows.Forms.DataGridViewTextBoxColumn structAuthor;
    }
}