namespace Grimoire.Tabs.Styles
{
    partial class MarketEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MarketEditor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.ts_load_btn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ts_save_btn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.ts_market_list = new System.Windows.Forms.ToolStripComboBox();
            this.ts_new_btn = new System.Windows.Forms.ToolStripButton();
            this.grid_cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.grid_cms_add = new System.Windows.Forms.ToolStripMenuItem();
            this.marketList = new System.Windows.Forms.ListView();
            this.label1 = new System.Windows.Forms.Label();
            this.info_grpBox = new System.Windows.Forms.GroupBox();
            this.save_edits_btn = new System.Windows.Forms.Button();
            this.arenaRatio = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.arenaPoint = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.huntaholicRatio = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.huntaholicPoint = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.priceRatio = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.price = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.search_btn = new System.Windows.Forms.Button();
            this.itemCode = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.marketName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sortID = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.addMarket_btn = new System.Windows.Forms.Button();
            this.remMarket_btn = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.toolStrip1.SuspendLayout();
            this.grid_cms.SuspendLayout();
            this.info_grpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.arenaRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.arenaPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huntaholicRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.huntaholicPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceRatio)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.price)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortID)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_load_btn,
            this.toolStripSeparator2,
            this.ts_save_btn,
            this.toolStripSeparator1,
            this.toolStripLabel1,
            this.ts_market_list,
            this.ts_new_btn});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(780, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // ts_load_btn
            // 
            this.ts_load_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_load_btn.Image = ((System.Drawing.Image)(resources.GetObject("ts_load_btn.Image")));
            this.ts_load_btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_load_btn.Name = "ts_load_btn";
            this.ts_load_btn.Size = new System.Drawing.Size(37, 22);
            this.ts_load_btn.Text = "Load";
            this.ts_load_btn.Click += new System.EventHandler(this.ts_load_btn_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // ts_save_btn
            // 
            this.ts_save_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_save_btn.Enabled = false;
            this.ts_save_btn.Image = ((System.Drawing.Image)(resources.GetObject("ts_save_btn.Image")));
            this.ts_save_btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_save_btn.Name = "ts_save_btn";
            this.ts_save_btn.Size = new System.Drawing.Size(35, 22);
            this.ts_save_btn.Text = "Save";
            this.ts_save_btn.Click += new System.EventHandler(this.ts_save_btn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(47, 22);
            this.toolStripLabel1.Text = "Market:";
            // 
            // ts_market_list
            // 
            this.ts_market_list.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.ts_market_list.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.ts_market_list.Enabled = false;
            this.ts_market_list.Name = "ts_market_list";
            this.ts_market_list.Size = new System.Drawing.Size(162, 25);
            this.ts_market_list.SelectedIndexChanged += new System.EventHandler(this.ts_market_list_SelectedIndexChanged);
            // 
            // ts_new_btn
            // 
            this.ts_new_btn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.ts_new_btn.Enabled = false;
            this.ts_new_btn.Image = ((System.Drawing.Image)(resources.GetObject("ts_new_btn.Image")));
            this.ts_new_btn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_new_btn.Name = "ts_new_btn";
            this.ts_new_btn.Size = new System.Drawing.Size(35, 22);
            this.ts_new_btn.Text = "New";
            this.ts_new_btn.Click += new System.EventHandler(this.ts_new_btn_Click);
            // 
            // grid_cms
            // 
            this.grid_cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.grid_cms_add});
            this.grid_cms.Name = "grid_cms";
            this.grid_cms.Size = new System.Drawing.Size(97, 26);
            // 
            // grid_cms_add
            // 
            this.grid_cms_add.Name = "grid_cms_add";
            this.grid_cms_add.Size = new System.Drawing.Size(96, 22);
            this.grid_cms_add.Text = "Add";
            // 
            // marketList
            // 
            this.marketList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.marketList.FullRowSelect = true;
            this.marketList.HideSelection = false;
            this.marketList.Location = new System.Drawing.Point(3, 50);
            this.marketList.MultiSelect = false;
            this.marketList.Name = "marketList";
            this.marketList.Size = new System.Drawing.Size(351, 362);
            this.marketList.TabIndex = 1;
            this.marketList.TabStop = false;
            this.marketList.UseCompatibleStateImageBehavior = false;
            this.marketList.View = System.Windows.Forms.View.List;
            this.marketList.SelectedIndexChanged += new System.EventHandler(this.marketList_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Entries:";
            // 
            // info_grpBox
            // 
            this.info_grpBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.info_grpBox.Controls.Add(this.save_edits_btn);
            this.info_grpBox.Controls.Add(this.arenaRatio);
            this.info_grpBox.Controls.Add(this.label9);
            this.info_grpBox.Controls.Add(this.arenaPoint);
            this.info_grpBox.Controls.Add(this.label10);
            this.info_grpBox.Controls.Add(this.huntaholicRatio);
            this.info_grpBox.Controls.Add(this.label7);
            this.info_grpBox.Controls.Add(this.huntaholicPoint);
            this.info_grpBox.Controls.Add(this.label8);
            this.info_grpBox.Controls.Add(this.priceRatio);
            this.info_grpBox.Controls.Add(this.label6);
            this.info_grpBox.Controls.Add(this.price);
            this.info_grpBox.Controls.Add(this.label5);
            this.info_grpBox.Controls.Add(this.search_btn);
            this.info_grpBox.Controls.Add(this.itemCode);
            this.info_grpBox.Controls.Add(this.label4);
            this.info_grpBox.Controls.Add(this.marketName);
            this.info_grpBox.Controls.Add(this.label3);
            this.info_grpBox.Controls.Add(this.sortID);
            this.info_grpBox.Controls.Add(this.label2);
            this.info_grpBox.Location = new System.Drawing.Point(360, 34);
            this.info_grpBox.Name = "info_grpBox";
            this.info_grpBox.Size = new System.Drawing.Size(408, 378);
            this.info_grpBox.TabIndex = 3;
            this.info_grpBox.TabStop = false;
            this.info_grpBox.Text = "Info";
            // 
            // save_edits_btn
            // 
            this.save_edits_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.save_edits_btn.Enabled = false;
            this.save_edits_btn.Location = new System.Drawing.Point(327, 355);
            this.save_edits_btn.Name = "save_edits_btn";
            this.save_edits_btn.Size = new System.Drawing.Size(75, 23);
            this.save_edits_btn.TabIndex = 18;
            this.save_edits_btn.Text = "Save";
            this.save_edits_btn.UseVisualStyleBackColor = true;
            this.save_edits_btn.Click += new System.EventHandler(this.save_edits_btn_Click);
            // 
            // arenaRatio
            // 
            this.arenaRatio.DecimalPlaces = 3;
            this.arenaRatio.Enabled = false;
            this.arenaRatio.Location = new System.Drawing.Point(293, 189);
            this.arenaRatio.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.arenaRatio.Name = "arenaRatio";
            this.arenaRatio.Size = new System.Drawing.Size(77, 20);
            this.arenaRatio.TabIndex = 10;
            this.arenaRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.arenaRatio.ValueChanged += new System.EventHandler(this.arenaRatio_ValueChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(205, 191);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(82, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Arena Pt. Ratio:";
            // 
            // arenaPoint
            // 
            this.arenaPoint.Enabled = false;
            this.arenaPoint.Location = new System.Drawing.Point(91, 189);
            this.arenaPoint.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.arenaPoint.Name = "arenaPoint";
            this.arenaPoint.Size = new System.Drawing.Size(77, 20);
            this.arenaPoint.TabIndex = 9;
            this.arenaPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.arenaPoint.ThousandsSeparator = true;
            this.arenaPoint.ValueChanged += new System.EventHandler(this.arenaPoint_ValueChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(20, 191);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(65, 13);
            this.label10.TabIndex = 15;
            this.label10.Text = "Arena Point:";
            // 
            // huntaholicRatio
            // 
            this.huntaholicRatio.DecimalPlaces = 3;
            this.huntaholicRatio.Location = new System.Drawing.Point(293, 161);
            this.huntaholicRatio.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.huntaholicRatio.Name = "huntaholicRatio";
            this.huntaholicRatio.Size = new System.Drawing.Size(77, 20);
            this.huntaholicRatio.TabIndex = 8;
            this.huntaholicRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.huntaholicRatio.ValueChanged += new System.EventHandler(this.huntaholicRatio_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(205, 163);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 13);
            this.label7.TabIndex = 13;
            this.label7.Text = "Gen Pt. Ratio:";
            // 
            // huntaholicPoint
            // 
            this.huntaholicPoint.Location = new System.Drawing.Point(91, 161);
            this.huntaholicPoint.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.huntaholicPoint.Name = "huntaholicPoint";
            this.huntaholicPoint.Size = new System.Drawing.Size(77, 20);
            this.huntaholicPoint.TabIndex = 7;
            this.huntaholicPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.huntaholicPoint.ThousandsSeparator = true;
            this.huntaholicPoint.ValueChanged += new System.EventHandler(this.huntaholicPoint_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(20, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Gen Point:";
            // 
            // priceRatio
            // 
            this.priceRatio.DecimalPlaces = 3;
            this.priceRatio.Location = new System.Drawing.Point(293, 131);
            this.priceRatio.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.priceRatio.Name = "priceRatio";
            this.priceRatio.Size = new System.Drawing.Size(77, 20);
            this.priceRatio.TabIndex = 6;
            this.priceRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.priceRatio.ValueChanged += new System.EventHandler(this.priceRatio_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(205, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Price Ratio:";
            // 
            // price
            // 
            this.price.Location = new System.Drawing.Point(91, 131);
            this.price.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.price.Name = "price";
            this.price.Size = new System.Drawing.Size(77, 20);
            this.price.TabIndex = 5;
            this.price.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.price.ThousandsSeparator = true;
            this.price.ValueChanged += new System.EventHandler(this.price_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 133);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Price:";
            // 
            // search_btn
            // 
            this.search_btn.Location = new System.Drawing.Point(216, 95);
            this.search_btn.Name = "search_btn";
            this.search_btn.Size = new System.Drawing.Size(49, 23);
            this.search_btn.TabIndex = 6;
            this.search_btn.Text = "Search";
            this.search_btn.UseVisualStyleBackColor = true;
            this.search_btn.Click += new System.EventHandler(this.search_btn_Click);
            // 
            // itemCode
            // 
            this.itemCode.Location = new System.Drawing.Point(91, 97);
            this.itemCode.Name = "itemCode";
            this.itemCode.ReadOnly = true;
            this.itemCode.Size = new System.Drawing.Size(119, 20);
            this.itemCode.TabIndex = 4;
            this.itemCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.itemCode.TextChanged += new System.EventHandler(this.itemCode_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 100);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Code:";
            // 
            // marketName
            // 
            this.marketName.Location = new System.Drawing.Point(101, 62);
            this.marketName.Name = "marketName";
            this.marketName.Size = new System.Drawing.Size(218, 20);
            this.marketName.TabIndex = 3;
            this.marketName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.marketName.TextChanged += new System.EventHandler(this.marketName_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Market Name:";
            // 
            // sortID
            // 
            this.sortID.Location = new System.Drawing.Point(83, 27);
            this.sortID.Maximum = new decimal(new int[] {
            2000000000,
            0,
            0,
            0});
            this.sortID.Name = "sortID";
            this.sortID.Size = new System.Drawing.Size(77, 20);
            this.sortID.TabIndex = 2;
            this.sortID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.sortID.ValueChanged += new System.EventHandler(this.sortID_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(43, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Sort ID:";
            // 
            // addMarket_btn
            // 
            this.addMarket_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.addMarket_btn.Enabled = false;
            this.addMarket_btn.Location = new System.Drawing.Point(3, 418);
            this.addMarket_btn.Name = "addMarket_btn";
            this.addMarket_btn.Size = new System.Drawing.Size(75, 23);
            this.addMarket_btn.TabIndex = 0;
            this.addMarket_btn.Text = "Add";
            this.addMarket_btn.UseVisualStyleBackColor = true;
            this.addMarket_btn.Click += new System.EventHandler(this.addMarket_btn_Click);
            // 
            // remMarket_btn
            // 
            this.remMarket_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.remMarket_btn.Enabled = false;
            this.remMarket_btn.Location = new System.Drawing.Point(84, 418);
            this.remMarket_btn.Name = "remMarket_btn";
            this.remMarket_btn.Size = new System.Drawing.Size(75, 23);
            this.remMarket_btn.TabIndex = 1;
            this.remMarket_btn.Text = "Remove";
            this.remMarket_btn.UseVisualStyleBackColor = true;
            this.remMarket_btn.Click += new System.EventHandler(this.remMarket_btn_Click);
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(668, 418);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(100, 23);
            this.progressBar.TabIndex = 6;
            // 
            // MarketEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.remMarket_btn);
            this.Controls.Add(this.addMarket_btn);
            this.Controls.Add(this.info_grpBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.marketList);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MarketEditor";
            this.Size = new System.Drawing.Size(780, 454);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.grid_cms.ResumeLayout(false);
            this.info_grpBox.ResumeLayout(false);
            this.info_grpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.arenaRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.arenaPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huntaholicRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.huntaholicPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.priceRatio)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.price)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sortID)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripComboBox ts_market_list;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton ts_load_btn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ts_save_btn;
        private System.Windows.Forms.ContextMenuStrip grid_cms;
        private System.Windows.Forms.ToolStripMenuItem grid_cms_add;
        private System.Windows.Forms.ListView marketList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox info_grpBox;
        private System.Windows.Forms.ToolStripButton ts_new_btn;
        private System.Windows.Forms.Button addMarket_btn;
        private System.Windows.Forms.Button remMarket_btn;
        private System.Windows.Forms.NumericUpDown arenaRatio;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown arenaPoint;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.NumericUpDown huntaholicRatio;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown huntaholicPoint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.NumericUpDown priceRatio;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown price;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button search_btn;
        private System.Windows.Forms.TextBox itemCode;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox marketName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown sortID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Button save_edits_btn;
    }
}
