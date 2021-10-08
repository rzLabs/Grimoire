
namespace Grimoire.GUI
{
    partial class ItemSelect
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
            this.itemGrid = new System.Windows.Forms.DataGridView();
            this.itemGrid_cs = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.itemGrid_cs_select_btn = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.notEqual_btn = new System.Windows.Forms.RadioButton();
            this.clearSearch_btn = new System.Windows.Forms.Button();
            this.leBtn = new System.Windows.Forms.RadioButton();
            this.geBtn = new System.Windows.Forms.RadioButton();
            this.containsBtn = new System.Windows.Forms.RadioButton();
            this.matchesBtn = new System.Windows.Forms.RadioButton();
            this.searchBtn = new System.Windows.Forms.Button();
            this.inputTxt = new System.Windows.Forms.TextBox();
            this.searchType = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.applyFilters_btn = new System.Windows.Forms.Button();
            this.clearFilters_btn = new System.Windows.Forms.Button();
            this.applyTypeFilter_btn = new System.Windows.Forms.CheckBox();
            this.applyGroupFilter_btn = new System.Windows.Forms.CheckBox();
            this.applyClassFilter_btn = new System.Windows.Forms.CheckBox();
            this.applyWearType_btn = new System.Windows.Forms.CheckBox();
            this.wear_type_lst = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.class_lst = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.group_lst = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.type_lst = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.itemGrid)).BeginInit();
            this.itemGrid_cs.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // itemGrid
            // 
            this.itemGrid.AllowUserToAddRows = false;
            this.itemGrid.AllowUserToDeleteRows = false;
            this.itemGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.itemGrid.ContextMenuStrip = this.itemGrid_cs;
            this.itemGrid.Location = new System.Drawing.Point(12, 25);
            this.itemGrid.Name = "itemGrid";
            this.itemGrid.ReadOnly = true;
            this.itemGrid.RowHeadersVisible = false;
            this.itemGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.itemGrid.Size = new System.Drawing.Size(452, 252);
            this.itemGrid.TabIndex = 0;
            this.itemGrid.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.itemGrid_CellMouseEnter);
            this.itemGrid.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.itemGrid_MouseDoubleClick);
            // 
            // itemGrid_cs
            // 
            this.itemGrid_cs.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.itemGrid_cs_select_btn});
            this.itemGrid_cs.Name = "itemGrid_cs";
            this.itemGrid_cs.Size = new System.Drawing.Size(106, 26);
            // 
            // itemGrid_cs_select_btn
            // 
            this.itemGrid_cs_select_btn.Name = "itemGrid_cs_select_btn";
            this.itemGrid_cs_select_btn.Size = new System.Drawing.Size(105, 22);
            this.itemGrid_cs_select_btn.Text = "Select";
            this.itemGrid_cs_select_btn.Click += new System.EventHandler(this.itemGrid_cs_select_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Items:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.notEqual_btn);
            this.groupBox1.Controls.Add(this.clearSearch_btn);
            this.groupBox1.Controls.Add(this.leBtn);
            this.groupBox1.Controls.Add(this.geBtn);
            this.groupBox1.Controls.Add(this.containsBtn);
            this.groupBox1.Controls.Add(this.matchesBtn);
            this.groupBox1.Controls.Add(this.searchBtn);
            this.groupBox1.Controls.Add(this.inputTxt);
            this.groupBox1.Controls.Add(this.searchType);
            this.groupBox1.Location = new System.Drawing.Point(12, 287);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(452, 79);
            this.groupBox1.TabIndex = 17;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // notEqual_btn
            // 
            this.notEqual_btn.AutoSize = true;
            this.notEqual_btn.Location = new System.Drawing.Point(153, 20);
            this.notEqual_btn.Name = "notEqual_btn";
            this.notEqual_btn.Size = new System.Drawing.Size(34, 17);
            this.notEqual_btn.TabIndex = 20;
            this.notEqual_btn.TabStop = true;
            this.notEqual_btn.Text = "!=";
            this.notEqual_btn.UseVisualStyleBackColor = true;
            // 
            // clearSearch_btn
            // 
            this.clearSearch_btn.Enabled = false;
            this.clearSearch_btn.Location = new System.Drawing.Point(354, 47);
            this.clearSearch_btn.Name = "clearSearch_btn";
            this.clearSearch_btn.Size = new System.Drawing.Size(19, 23);
            this.clearSearch_btn.TabIndex = 19;
            this.clearSearch_btn.Text = "X";
            this.clearSearch_btn.UseVisualStyleBackColor = true;
            this.clearSearch_btn.Click += new System.EventHandler(this.clearSearch_btn_Click);
            // 
            // leBtn
            // 
            this.leBtn.AutoSize = true;
            this.leBtn.Location = new System.Drawing.Point(277, 20);
            this.leBtn.Name = "leBtn";
            this.leBtn.Size = new System.Drawing.Size(31, 17);
            this.leBtn.TabIndex = 18;
            this.leBtn.Text = "<";
            this.leBtn.UseVisualStyleBackColor = true;
            // 
            // geBtn
            // 
            this.geBtn.AutoSize = true;
            this.geBtn.Location = new System.Drawing.Point(240, 20);
            this.geBtn.Name = "geBtn";
            this.geBtn.Size = new System.Drawing.Size(31, 17);
            this.geBtn.TabIndex = 17;
            this.geBtn.Text = ">";
            this.geBtn.UseVisualStyleBackColor = true;
            // 
            // containsBtn
            // 
            this.containsBtn.AutoSize = true;
            this.containsBtn.Location = new System.Drawing.Point(193, 20);
            this.containsBtn.Name = "containsBtn";
            this.containsBtn.Size = new System.Drawing.Size(41, 17);
            this.containsBtn.TabIndex = 16;
            this.containsBtn.Text = "like";
            this.containsBtn.UseVisualStyleBackColor = true;
            // 
            // matchesBtn
            // 
            this.matchesBtn.AutoSize = true;
            this.matchesBtn.Checked = true;
            this.matchesBtn.Location = new System.Drawing.Point(116, 20);
            this.matchesBtn.Name = "matchesBtn";
            this.matchesBtn.Size = new System.Drawing.Size(31, 17);
            this.matchesBtn.TabIndex = 15;
            this.matchesBtn.TabStop = true;
            this.matchesBtn.Text = "=";
            this.matchesBtn.UseVisualStyleBackColor = true;
            // 
            // searchBtn
            // 
            this.searchBtn.Location = new System.Drawing.Point(379, 47);
            this.searchBtn.Name = "searchBtn";
            this.searchBtn.Size = new System.Drawing.Size(61, 23);
            this.searchBtn.TabIndex = 14;
            this.searchBtn.Text = "Search";
            this.searchBtn.UseVisualStyleBackColor = true;
            this.searchBtn.Click += new System.EventHandler(this.searchBtn_Click);
            // 
            // inputTxt
            // 
            this.inputTxt.Location = new System.Drawing.Point(6, 49);
            this.inputTxt.Name = "inputTxt";
            this.inputTxt.Size = new System.Drawing.Size(342, 20);
            this.inputTxt.TabIndex = 13;
            this.inputTxt.TextChanged += new System.EventHandler(this.inputTxt_TextChanged);
            // 
            // searchType
            // 
            this.searchType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.searchType.FormattingEnabled = true;
            this.searchType.Items.AddRange(new object[] {
            "id",
            "name",
            "icon_file_name",
            "script_text"});
            this.searchType.Location = new System.Drawing.Point(6, 19);
            this.searchType.Name = "searchType";
            this.searchType.Size = new System.Drawing.Size(95, 21);
            this.searchType.TabIndex = 12;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.applyFilters_btn);
            this.groupBox2.Controls.Add(this.clearFilters_btn);
            this.groupBox2.Controls.Add(this.applyTypeFilter_btn);
            this.groupBox2.Controls.Add(this.applyGroupFilter_btn);
            this.groupBox2.Controls.Add(this.applyClassFilter_btn);
            this.groupBox2.Controls.Add(this.applyWearType_btn);
            this.groupBox2.Controls.Add(this.wear_type_lst);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.class_lst);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.group_lst);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.type_lst);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(470, 9);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(174, 357);
            this.groupBox2.TabIndex = 18;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filters";
            // 
            // applyFilters_btn
            // 
            this.applyFilters_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.applyFilters_btn.Location = new System.Drawing.Point(9, 295);
            this.applyFilters_btn.Name = "applyFilters_btn";
            this.applyFilters_btn.Size = new System.Drawing.Size(155, 23);
            this.applyFilters_btn.TabIndex = 30;
            this.applyFilters_btn.Text = "Apply Filters";
            this.applyFilters_btn.UseVisualStyleBackColor = true;
            this.applyFilters_btn.Click += new System.EventHandler(this.applyFilters_btn_Click);
            // 
            // clearFilters_btn
            // 
            this.clearFilters_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.clearFilters_btn.Location = new System.Drawing.Point(9, 325);
            this.clearFilters_btn.Name = "clearFilters_btn";
            this.clearFilters_btn.Size = new System.Drawing.Size(155, 23);
            this.clearFilters_btn.TabIndex = 29;
            this.clearFilters_btn.Text = "Clear Filters";
            this.clearFilters_btn.UseVisualStyleBackColor = true;
            this.clearFilters_btn.Click += new System.EventHandler(this.clearFilters_btn_Click);
            // 
            // applyTypeFilter_btn
            // 
            this.applyTypeFilter_btn.AutoSize = true;
            this.applyTypeFilter_btn.Location = new System.Drawing.Point(149, 28);
            this.applyTypeFilter_btn.Name = "applyTypeFilter_btn";
            this.applyTypeFilter_btn.Size = new System.Drawing.Size(15, 14);
            this.applyTypeFilter_btn.TabIndex = 28;
            this.applyTypeFilter_btn.UseVisualStyleBackColor = true;
            // 
            // applyGroupFilter_btn
            // 
            this.applyGroupFilter_btn.AutoSize = true;
            this.applyGroupFilter_btn.Location = new System.Drawing.Point(149, 80);
            this.applyGroupFilter_btn.Name = "applyGroupFilter_btn";
            this.applyGroupFilter_btn.Size = new System.Drawing.Size(15, 14);
            this.applyGroupFilter_btn.TabIndex = 27;
            this.applyGroupFilter_btn.UseVisualStyleBackColor = true;
            // 
            // applyClassFilter_btn
            // 
            this.applyClassFilter_btn.AutoSize = true;
            this.applyClassFilter_btn.Location = new System.Drawing.Point(149, 130);
            this.applyClassFilter_btn.Name = "applyClassFilter_btn";
            this.applyClassFilter_btn.Size = new System.Drawing.Size(15, 14);
            this.applyClassFilter_btn.TabIndex = 26;
            this.applyClassFilter_btn.UseVisualStyleBackColor = true;
            // 
            // applyWearType_btn
            // 
            this.applyWearType_btn.AutoSize = true;
            this.applyWearType_btn.Location = new System.Drawing.Point(149, 181);
            this.applyWearType_btn.Name = "applyWearType_btn";
            this.applyWearType_btn.Size = new System.Drawing.Size(15, 14);
            this.applyWearType_btn.TabIndex = 25;
            this.applyWearType_btn.UseVisualStyleBackColor = true;
            // 
            // wear_type_lst
            // 
            this.wear_type_lst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wear_type_lst.FormattingEnabled = true;
            this.wear_type_lst.Location = new System.Drawing.Point(9, 197);
            this.wear_type_lst.Name = "wear_type_lst";
            this.wear_type_lst.Size = new System.Drawing.Size(155, 21);
            this.wear_type_lst.TabIndex = 24;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "Wear Type:";
            // 
            // class_lst
            // 
            this.class_lst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.class_lst.FormattingEnabled = true;
            this.class_lst.Location = new System.Drawing.Point(9, 146);
            this.class_lst.Name = "class_lst";
            this.class_lst.Size = new System.Drawing.Size(155, 21);
            this.class_lst.TabIndex = 22;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Class:";
            // 
            // group_lst
            // 
            this.group_lst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.group_lst.FormattingEnabled = true;
            this.group_lst.Location = new System.Drawing.Point(9, 96);
            this.group_lst.Name = "group_lst";
            this.group_lst.Size = new System.Drawing.Size(155, 21);
            this.group_lst.TabIndex = 20;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 80);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Group:";
            // 
            // type_lst
            // 
            this.type_lst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type_lst.FormattingEnabled = true;
            this.type_lst.Location = new System.Drawing.Point(9, 45);
            this.type_lst.Name = "type_lst";
            this.type_lst.Size = new System.Drawing.Size(155, 21);
            this.type_lst.TabIndex = 18;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Type:";
            // 
            // ItemSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 377);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.itemGrid);
            this.KeyPreview = true;
            this.Name = "ItemSelect";
            this.Text = "ItemSelect";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ItemSelect_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.itemGrid)).EndInit();
            this.itemGrid_cs.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView itemGrid;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button searchBtn;
        private System.Windows.Forms.TextBox inputTxt;
        private System.Windows.Forms.ComboBox searchType;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button clearFilters_btn;
        private System.Windows.Forms.CheckBox applyTypeFilter_btn;
        private System.Windows.Forms.CheckBox applyGroupFilter_btn;
        private System.Windows.Forms.CheckBox applyClassFilter_btn;
        private System.Windows.Forms.CheckBox applyWearType_btn;
        private System.Windows.Forms.ComboBox wear_type_lst;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox class_lst;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox group_lst;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox type_lst;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton leBtn;
        private System.Windows.Forms.RadioButton geBtn;
        private System.Windows.Forms.RadioButton containsBtn;
        private System.Windows.Forms.RadioButton matchesBtn;
        private System.Windows.Forms.Button clearSearch_btn;
        private System.Windows.Forms.RadioButton notEqual_btn;
        private System.Windows.Forms.Button applyFilters_btn;
        private System.Windows.Forms.ContextMenuStrip itemGrid_cs;
        private System.Windows.Forms.ToolStripMenuItem itemGrid_cs_select_btn;
    }
}