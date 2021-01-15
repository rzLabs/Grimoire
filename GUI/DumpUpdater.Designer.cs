namespace Grimoire.GUI
{
    partial class DumpUpdater
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
            this.grid = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.source = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.destination = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.exists = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dumpDirTxtBox = new System.Windows.Forms.TextBox();
            this.dumpDirLbl = new System.Windows.Forms.Label();
            this.copyBtn = new System.Windows.Forms.Button();
            this.prgBar = new System.Windows.Forms.ProgressBar();
            this.statusLb = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            this.SuspendLayout();
            // 
            // grid
            // 
            this.grid.AllowUserToAddRows = false;
            this.grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name,
            this.source,
            this.destination,
            this.exists});
            this.grid.Location = new System.Drawing.Point(12, 44);
            this.grid.Name = "grid";
            this.grid.ReadOnly = true;
            this.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grid.Size = new System.Drawing.Size(755, 418);
            this.grid.TabIndex = 7;
            this.grid.RowsRemoved += new System.Windows.Forms.DataGridViewRowsRemovedEventHandler(this.grid_RowsRemoved);
            // 
            // name
            // 
            this.name.HeaderText = "File Name";
            this.name.Name = "name";
            this.name.ReadOnly = true;
            this.name.Width = 250;
            // 
            // source
            // 
            this.source.HeaderText = "Source";
            this.source.Name = "source";
            this.source.ReadOnly = true;
            this.source.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.source.Width = 315;
            // 
            // destination
            // 
            this.destination.HeaderText = "Destination";
            this.destination.Name = "destination";
            this.destination.ReadOnly = true;
            this.destination.Width = 65;
            // 
            // exists
            // 
            this.exists.HeaderText = "Exists";
            this.exists.Name = "exists";
            this.exists.ReadOnly = true;
            this.exists.ToolTipText = "If yes the file already exists in the dump folder";
            this.exists.Width = 50;
            // 
            // dumpDirTxtBox
            // 
            this.dumpDirTxtBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dumpDirTxtBox.Enabled = false;
            this.dumpDirTxtBox.Location = new System.Drawing.Point(106, 15);
            this.dumpDirTxtBox.Name = "dumpDirTxtBox";
            this.dumpDirTxtBox.ReadOnly = true;
            this.dumpDirTxtBox.Size = new System.Drawing.Size(661, 20);
            this.dumpDirTxtBox.TabIndex = 12;
            this.dumpDirTxtBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // dumpDirLbl
            // 
            this.dumpDirLbl.AutoSize = true;
            this.dumpDirLbl.Location = new System.Drawing.Point(12, 18);
            this.dumpDirLbl.Name = "dumpDirLbl";
            this.dumpDirLbl.Size = new System.Drawing.Size(83, 13);
            this.dumpDirLbl.TabIndex = 10;
            this.dumpDirLbl.Text = "Dump Directory:";
            // 
            // copyBtn
            // 
            this.copyBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.copyBtn.Enabled = false;
            this.copyBtn.Location = new System.Drawing.Point(692, 473);
            this.copyBtn.Name = "copyBtn";
            this.copyBtn.Size = new System.Drawing.Size(75, 23);
            this.copyBtn.TabIndex = 9;
            this.copyBtn.Text = "Copy";
            this.copyBtn.UseVisualStyleBackColor = true;
            this.copyBtn.Click += new System.EventHandler(this.copyBtn_Click);
            // 
            // prgBar
            // 
            this.prgBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.prgBar.Location = new System.Drawing.Point(12, 473);
            this.prgBar.Name = "prgBar";
            this.prgBar.Size = new System.Drawing.Size(100, 23);
            this.prgBar.TabIndex = 8;
            // 
            // statusLb
            // 
            this.statusLb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.statusLb.AutoSize = true;
            this.statusLb.Location = new System.Drawing.Point(118, 478);
            this.statusLb.Name = "statusLb";
            this.statusLb.Size = new System.Drawing.Size(0, 13);
            this.statusLb.TabIndex = 13;
            // 
            // DumpUpdater
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 506);
            this.Controls.Add(this.statusLb);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.dumpDirTxtBox);
            this.Controls.Add(this.dumpDirLbl);
            this.Controls.Add(this.copyBtn);
            this.Controls.Add(this.prgBar);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DumpUpdater";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dump Updater";
            this.Load += new System.EventHandler(this.DumpUpdater_Load);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.DumpUpdater_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.DumpUpdater_DragEnter);
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView grid;
        private System.Windows.Forms.DataGridViewTextBoxColumn name;
        private System.Windows.Forms.DataGridViewTextBoxColumn source;
        private System.Windows.Forms.DataGridViewTextBoxColumn destination;
        private System.Windows.Forms.DataGridViewTextBoxColumn exists;
        private System.Windows.Forms.TextBox dumpDirTxtBox;
        private System.Windows.Forms.Label dumpDirLbl;
        private System.Windows.Forms.Button copyBtn;
        private System.Windows.Forms.ProgressBar prgBar;
        private System.Windows.Forms.Label statusLb;
    }
}