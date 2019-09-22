namespace Grimoire.GUI
{
    partial class Main
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabs_cMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tabs_cMenu_clear = new System.Windows.Forms.ToolStripMenuItem();
            this.tabs_cMenu_close = new System.Windows.Forms.ToolStripMenuItem();
            this.newLbl = new System.Windows.Forms.Label();
            this.new_list = new System.Windows.Forms.ComboBox();
            this.settingsBtn = new System.Windows.Forms.Button();
            this.aboutLbl = new System.Windows.Forms.Label();
            this.ms = new System.Windows.Forms.MenuStrip();
            this.ts_utilities = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_bitflag_editor = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_log_viewer = new System.Windows.Forms.ToolStripMenuItem();
            this.tabs_cMenu.SuspendLayout();
            this.ms.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabs
            // 
            this.tabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabs.Location = new System.Drawing.Point(9, 32);
            this.tabs.Margin = new System.Windows.Forms.Padding(2);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(788, 472);
            this.tabs.TabIndex = 1;
            this.tabs.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tabs_MouseClick);
            // 
            // tabs_cMenu
            // 
            this.tabs_cMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.tabs_cMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tabs_cMenu_clear,
            this.tabs_cMenu_close});
            this.tabs_cMenu.Name = "grid_cMenu";
            this.tabs_cMenu.Size = new System.Drawing.Size(104, 48);
            // 
            // tabs_cMenu_clear
            // 
            this.tabs_cMenu_clear.Name = "tabs_cMenu_clear";
            this.tabs_cMenu_clear.Size = new System.Drawing.Size(103, 22);
            this.tabs_cMenu_clear.Text = "Clear";
            this.tabs_cMenu_clear.Click += new System.EventHandler(this.tabs_cMenu_clear_Click);
            // 
            // tabs_cMenu_close
            // 
            this.tabs_cMenu_close.Name = "tabs_cMenu_close";
            this.tabs_cMenu_close.Size = new System.Drawing.Size(103, 22);
            this.tabs_cMenu_close.Text = "Close";
            this.tabs_cMenu_close.Click += new System.EventHandler(this.tabs_cMenu_close_Click);
            // 
            // newLbl
            // 
            this.newLbl.AutoSize = true;
            this.newLbl.Location = new System.Drawing.Point(9, 10);
            this.newLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.newLbl.Name = "newLbl";
            this.newLbl.Size = new System.Drawing.Size(32, 13);
            this.newLbl.TabIndex = 2;
            this.newLbl.Text = "New:";
            // 
            // new_list
            // 
            this.new_list.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.new_list.FormattingEnabled = true;
            this.new_list.Items.AddRange(new object[] {
            "RDB",
            "HASHER"});
            this.new_list.Location = new System.Drawing.Point(43, 7);
            this.new_list.Margin = new System.Windows.Forms.Padding(2);
            this.new_list.Name = "new_list";
            this.new_list.Size = new System.Drawing.Size(92, 21);
            this.new_list.TabIndex = 3;
            this.new_list.SelectedIndexChanged += new System.EventHandler(this.new_list_SelectedIndexChanged);
            // 
            // settingsBtn
            // 
            this.settingsBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.settingsBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.settingsBtn.Location = new System.Drawing.Point(740, 5);
            this.settingsBtn.Margin = new System.Windows.Forms.Padding(2);
            this.settingsBtn.Name = "settingsBtn";
            this.settingsBtn.Size = new System.Drawing.Size(56, 23);
            this.settingsBtn.TabIndex = 4;
            this.settingsBtn.Text = "Settings";
            this.settingsBtn.UseVisualStyleBackColor = true;
            this.settingsBtn.Click += new System.EventHandler(this.settings_Click);
            // 
            // aboutLbl
            // 
            this.aboutLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutLbl.AutoSize = true;
            this.aboutLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Underline);
            this.aboutLbl.Location = new System.Drawing.Point(762, 505);
            this.aboutLbl.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.aboutLbl.Name = "aboutLbl";
            this.aboutLbl.Size = new System.Drawing.Size(35, 13);
            this.aboutLbl.TabIndex = 5;
            this.aboutLbl.Text = "About";
            this.aboutLbl.Click += new System.EventHandler(this.aboutLbl_Click);
            // 
            // ms
            // 
            this.ms.Dock = System.Windows.Forms.DockStyle.None;
            this.ms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_utilities});
            this.ms.Location = new System.Drawing.Point(141, 5);
            this.ms.Name = "ms";
            this.ms.Size = new System.Drawing.Size(186, 24);
            this.ms.TabIndex = 6;
            this.ms.Text = "menuStrip1";
            // 
            // ts_utilities
            // 
            this.ts_utilities.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_bitflag_editor,
            this.ts_log_viewer});
            this.ts_utilities.Name = "ts_utilities";
            this.ts_utilities.Size = new System.Drawing.Size(58, 20);
            this.ts_utilities.Text = "Utilities";
            // 
            // ts_bitflag_editor
            // 
            this.ts_bitflag_editor.Name = "ts_bitflag_editor";
            this.ts_bitflag_editor.Size = new System.Drawing.Size(180, 22);
            this.ts_bitflag_editor.Text = "BitFlag Editor";
            this.ts_bitflag_editor.Click += new System.EventHandler(this.ts_bitflag_editor_Click);
            // 
            // ts_log_viewer
            // 
            this.ts_log_viewer.Name = "ts_log_viewer";
            this.ts_log_viewer.Size = new System.Drawing.Size(180, 22);
            this.ts_log_viewer.Text = "Log Viewer";
            this.ts_log_viewer.Click += new System.EventHandler(this.ts_log_viewer_Click);
            // 
            // Main
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 521);
            this.Controls.Add(this.ms);
            this.Controls.Add(this.aboutLbl);
            this.Controls.Add(this.settingsBtn);
            this.Controls.Add(this.new_list);
            this.Controls.Add(this.newLbl);
            this.Controls.Add(this.tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.ms;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(794, 558);
            this.Name = "Main";
            this.Text = "Grimoire";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
            this.Shown += new System.EventHandler(this.Main_Shown);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Main_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Main_DragEnter);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Main_KeyDown);
            this.tabs_cMenu.ResumeLayout(false);
            this.ms.ResumeLayout(false);
            this.ms.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TabControl tabs;
        private System.Windows.Forms.Label newLbl;
        private System.Windows.Forms.ComboBox new_list;
        private System.Windows.Forms.ContextMenuStrip tabs_cMenu;
        private System.Windows.Forms.ToolStripMenuItem tabs_cMenu_clear;
        private System.Windows.Forms.ToolStripMenuItem tabs_cMenu_close;
        private System.Windows.Forms.Button settingsBtn;
        private System.Windows.Forms.Label aboutLbl;
        private System.Windows.Forms.MenuStrip ms;
        private System.Windows.Forms.ToolStripMenuItem ts_utilities;
        private System.Windows.Forms.ToolStripMenuItem ts_bitflag_editor;
        private System.Windows.Forms.ToolStripMenuItem ts_log_viewer;
    }
}

