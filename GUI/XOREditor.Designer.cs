namespace Grimoire.GUI
{
    partial class XOREditor
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
            this.hexBox = new Be.Windows.Forms.HexBox();
            this.unKeyLb = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ts_file = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_file_load = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_file_save = new System.Windows.Forms.ToolStripMenuItem();
            this.unKey_txtBox = new System.Windows.Forms.RichTextBox();
            this.ts_file_load_key = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_file_load_config = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_reset = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_file_load_def = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_def = new System.Windows.Forms.ToolStripMenuItem();
            this.resEncKeyLbl = new System.Windows.Forms.Label();
            this.ts_file_save_key = new System.Windows.Forms.ToolStripMenuItem();
            this.ts_file_save_config = new System.Windows.Forms.ToolStripMenuItem();
            this.hex_cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.hex_cms_clear = new System.Windows.Forms.ToolStripMenuItem();
            this.unkey_cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.unkey_cms_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.hex_cms_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.hex_cms.SuspendLayout();
            this.unkey_cms.SuspendLayout();
            this.SuspendLayout();
            // 
            // hexBox
            // 
            this.hexBox.ColumnInfoVisible = true;
            this.hexBox.ContextMenuStrip = this.hex_cms;
            this.hexBox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hexBox.LineInfoVisible = true;
            this.hexBox.Location = new System.Drawing.Point(12, 45);
            this.hexBox.Name = "hexBox";
            this.hexBox.SelectionBackColor = System.Drawing.Color.LightSteelBlue;
            this.hexBox.ShadowSelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(60)))), ((int)(((byte)(188)))), ((int)(((byte)(255)))));
            this.hexBox.Size = new System.Drawing.Size(641, 295);
            this.hexBox.StringViewVisible = true;
            this.hexBox.TabIndex = 0;
            this.hexBox.UseFixedBytesPerLine = true;
            this.hexBox.VScrollBarVisible = true;
            // 
            // unKeyLb
            // 
            this.unKeyLb.AutoSize = true;
            this.unKeyLb.Location = new System.Drawing.Point(12, 343);
            this.unKeyLb.Name = "unKeyLb";
            this.unKeyLb.Size = new System.Drawing.Size(123, 13);
            this.unKeyLb.TabIndex = 2;
            this.unKeyLb.Text = "unResourceEncodeKey:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_file,
            this.ts_reset,
            this.ts_def});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(665, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // ts_file
            // 
            this.ts_file.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_file_load,
            this.ts_file_save});
            this.ts_file.Name = "ts_file";
            this.ts_file.Size = new System.Drawing.Size(37, 20);
            this.ts_file.Text = "File";
            // 
            // ts_file_load
            // 
            this.ts_file_load.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_file_load_key,
            this.ts_file_load_config,
            this.ts_file_load_def});
            this.ts_file_load.Name = "ts_file_load";
            this.ts_file_load.Size = new System.Drawing.Size(180, 22);
            this.ts_file_load.Text = "Load";
            // 
            // ts_file_save
            // 
            this.ts_file_save.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_file_save_key,
            this.ts_file_save_config});
            this.ts_file_save.Name = "ts_file_save";
            this.ts_file_save.Size = new System.Drawing.Size(180, 22);
            this.ts_file_save.Text = "Save";
            // 
            // unKey_txtBox
            // 
            this.unKey_txtBox.ContextMenuStrip = this.unkey_cms;
            this.unKey_txtBox.Location = new System.Drawing.Point(12, 359);
            this.unKey_txtBox.Name = "unKey_txtBox";
            this.unKey_txtBox.ReadOnly = true;
            this.unKey_txtBox.Size = new System.Drawing.Size(641, 229);
            this.unKey_txtBox.TabIndex = 5;
            this.unKey_txtBox.Text = "";
            // 
            // ts_file_load_key
            // 
            this.ts_file_load_key.Name = "ts_file_load_key";
            this.ts_file_load_key.Size = new System.Drawing.Size(180, 22);
            this.ts_file_load_key.Text = "Key File";
            this.ts_file_load_key.Click += new System.EventHandler(this.ts_file_load_key_Click);
            // 
            // ts_file_load_config
            // 
            this.ts_file_load_config.Name = "ts_file_load_config";
            this.ts_file_load_config.Size = new System.Drawing.Size(180, 22);
            this.ts_file_load_config.Text = "Config.json";
            this.ts_file_load_config.Click += new System.EventHandler(this.ts_file_load_config_Click);
            // 
            // ts_reset
            // 
            this.ts_reset.Name = "ts_reset";
            this.ts_reset.Size = new System.Drawing.Size(47, 20);
            this.ts_reset.Text = "Reset";
            this.ts_reset.Click += new System.EventHandler(this.ts_reset_Click);
            // 
            // ts_file_load_def
            // 
            this.ts_file_load_def.Name = "ts_file_load_def";
            this.ts_file_load_def.Size = new System.Drawing.Size(180, 22);
            this.ts_file_load_def.Text = "Default";
            this.ts_file_load_def.Click += new System.EventHandler(this.ts_file_load_def_Click);
            // 
            // ts_def
            // 
            this.ts_def.Name = "ts_def";
            this.ts_def.Size = new System.Drawing.Size(57, 20);
            this.ts_def.Text = "Default";
            this.ts_def.Click += new System.EventHandler(this.ts_def_Click);
            // 
            // resEncKeyLbl
            // 
            this.resEncKeyLbl.AutoSize = true;
            this.resEncKeyLbl.Location = new System.Drawing.Point(12, 29);
            this.resEncKeyLbl.Name = "resEncKeyLbl";
            this.resEncKeyLbl.Size = new System.Drawing.Size(121, 13);
            this.resEncKeyLbl.TabIndex = 6;
            this.resEncKeyLbl.Text = "szResourceEncodeKey:";
            // 
            // ts_file_save_key
            // 
            this.ts_file_save_key.Name = "ts_file_save_key";
            this.ts_file_save_key.Size = new System.Drawing.Size(180, 22);
            this.ts_file_save_key.Text = "Key File";
            this.ts_file_save_key.Click += new System.EventHandler(this.ts_file_save_key_Click);
            // 
            // ts_file_save_config
            // 
            this.ts_file_save_config.Name = "ts_file_save_config";
            this.ts_file_save_config.Size = new System.Drawing.Size(180, 22);
            this.ts_file_save_config.Text = "Config.json";
            this.ts_file_save_config.Click += new System.EventHandler(this.ts_file_save_config_Click);
            // 
            // hex_cms
            // 
            this.hex_cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hex_cms_clear,
            this.hex_cms_copy});
            this.hex_cms.Name = "hex_cms";
            this.hex_cms.Size = new System.Drawing.Size(181, 70);
            // 
            // hex_cms_clear
            // 
            this.hex_cms_clear.Name = "hex_cms_clear";
            this.hex_cms_clear.Size = new System.Drawing.Size(180, 22);
            this.hex_cms_clear.Text = "Clear";
            this.hex_cms_clear.Click += new System.EventHandler(this.hex_cms_clear_Click);
            // 
            // unkey_cms
            // 
            this.unkey_cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.unkey_cms_copy});
            this.unkey_cms.Name = "unkey_cms";
            this.unkey_cms.Size = new System.Drawing.Size(103, 26);
            // 
            // unkey_cms_copy
            // 
            this.unkey_cms_copy.Name = "unkey_cms_copy";
            this.unkey_cms_copy.Size = new System.Drawing.Size(102, 22);
            this.unkey_cms_copy.Text = "Copy";
            this.unkey_cms_copy.Click += new System.EventHandler(this.unkey_cms_copy_Click);
            // 
            // hex_cms_copy
            // 
            this.hex_cms_copy.Name = "hex_cms_copy";
            this.hex_cms_copy.Size = new System.Drawing.Size(180, 22);
            this.hex_cms_copy.Text = "Copy";
            this.hex_cms_copy.Click += new System.EventHandler(this.hex_cms_copy_Click);
            // 
            // XOREditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(665, 600);
            this.Controls.Add(this.resEncKeyLbl);
            this.Controls.Add(this.unKey_txtBox);
            this.Controls.Add(this.unKeyLb);
            this.Controls.Add(this.hexBox);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "XOREditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "XOR Key Editor";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.hex_cms.ResumeLayout(false);
            this.unkey_cms.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Be.Windows.Forms.HexBox hexBox;
        private System.Windows.Forms.Label unKeyLb;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ts_file;
        private System.Windows.Forms.ToolStripMenuItem ts_file_load;
        private System.Windows.Forms.ToolStripMenuItem ts_file_save;
        private System.Windows.Forms.RichTextBox unKey_txtBox;
        private System.Windows.Forms.ToolStripMenuItem ts_file_load_key;
        private System.Windows.Forms.ToolStripMenuItem ts_file_load_config;
        private System.Windows.Forms.ToolStripMenuItem ts_reset;
        private System.Windows.Forms.ToolStripMenuItem ts_file_load_def;
        private System.Windows.Forms.ToolStripMenuItem ts_def;
        private System.Windows.Forms.Label resEncKeyLbl;
        private System.Windows.Forms.ToolStripMenuItem ts_file_save_key;
        private System.Windows.Forms.ToolStripMenuItem ts_file_save_config;
        private System.Windows.Forms.ContextMenuStrip hex_cms;
        private System.Windows.Forms.ToolStripMenuItem hex_cms_clear;
        private System.Windows.Forms.ContextMenuStrip unkey_cms;
        private System.Windows.Forms.ToolStripMenuItem unkey_cms_copy;
        private System.Windows.Forms.ToolStripMenuItem hex_cms_copy;
    }
}