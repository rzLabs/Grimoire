namespace Grimoire.GUI
{
    partial class StructureEditor
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.readHead_off = new System.Windows.Forms.RadioButton();
            this.readHead_on = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.specialCase_list = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ext = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.fileName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tableName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.path = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.key = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.remHField_btn = new System.Windows.Forms.Button();
            this.addHField_btn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.hField_grid = new System.Windows.Forms.DataGridView();
            this.remDField_btn = new System.Windows.Forms.Button();
            this.addDField_btn = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.dField_grid = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.editSelect_btn = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.editColumns_btn = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.proessRow_btn = new System.Windows.Forms.Button();
            this.verifyLuaSyn_btn = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.editLuaFunc_btn = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.hField_grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dField_grid)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.readHead_off);
            this.groupBox1.Controls.Add(this.readHead_on);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.specialCase_list);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.ext);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 156);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(290, 118);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Engine Options";
            // 
            // readHead_off
            // 
            this.readHead_off.AutoSize = true;
            this.readHead_off.Location = new System.Drawing.Point(223, 83);
            this.readHead_off.Name = "readHead_off";
            this.readHead_off.Size = new System.Drawing.Size(48, 21);
            this.readHead_off.TabIndex = 5;
            this.readHead_off.TabStop = true;
            this.readHead_off.Text = "Off";
            this.readHead_off.UseVisualStyleBackColor = true;
            // 
            // readHead_on
            // 
            this.readHead_on.AutoSize = true;
            this.readHead_on.Checked = true;
            this.readHead_on.Location = new System.Drawing.Point(169, 83);
            this.readHead_on.Name = "readHead_on";
            this.readHead_on.Size = new System.Drawing.Size(48, 21);
            this.readHead_on.TabIndex = 4;
            this.readHead_on.TabStop = true;
            this.readHead_on.Text = "On";
            this.readHead_on.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Read Header";
            // 
            // specialCase_list
            // 
            this.specialCase_list.FormattingEnabled = true;
            this.specialCase_list.Items.AddRange(new object[] {
            "DOUBLELOOP"});
            this.specialCase_list.Location = new System.Drawing.Point(135, 53);
            this.specialCase_list.Name = "specialCase_list";
            this.specialCase_list.Size = new System.Drawing.Size(136, 24);
            this.specialCase_list.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 17);
            this.label2.TabIndex = 2;
            this.label2.Text = "Special Case";
            // 
            // ext
            // 
            this.ext.Location = new System.Drawing.Point(171, 25);
            this.ext.Name = "ext";
            this.ext.Size = new System.Drawing.Size(100, 22);
            this.ext.TabIndex = 2;
            this.ext.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(69, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Extension";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.fileName);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.tableName);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(12, 280);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(290, 88);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Default Names";
            // 
            // fileName
            // 
            this.fileName.Location = new System.Drawing.Point(89, 53);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(182, 22);
            this.fileName.TabIndex = 7;
            this.fileName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(30, 17);
            this.label5.TabIndex = 2;
            this.label5.Text = "File";
            // 
            // tableName
            // 
            this.tableName.Location = new System.Drawing.Point(89, 25);
            this.tableName.Name = "tableName";
            this.tableName.Size = new System.Drawing.Size(182, 22);
            this.tableName.TabIndex = 6;
            this.tableName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 28);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Table";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.path);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.key);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Location = new System.Drawing.Point(12, 12);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(290, 138);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Basic";
            // 
            // path
            // 
            this.path.Location = new System.Drawing.Point(9, 79);
            this.path.Multiline = true;
            this.path.Name = "path";
            this.path.ReadOnly = true;
            this.path.Size = new System.Drawing.Size(260, 41);
            this.path.TabIndex = 4;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 59);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 17);
            this.label7.TabIndex = 3;
            this.label7.Text = "Path";
            // 
            // key
            // 
            this.key.Location = new System.Drawing.Point(77, 26);
            this.key.Name = "key";
            this.key.Size = new System.Drawing.Size(192, 22);
            this.key.TabIndex = 1;
            this.key.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 17);
            this.label6.TabIndex = 0;
            this.label6.Text = "Key";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(308, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.remHField_btn);
            this.splitContainer1.Panel1.Controls.Add(this.addHField_btn);
            this.splitContainer1.Panel1.Controls.Add(this.label8);
            this.splitContainer1.Panel1.Controls.Add(this.hField_grid);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.remDField_btn);
            this.splitContainer1.Panel2.Controls.Add(this.addDField_btn);
            this.splitContainer1.Panel2.Controls.Add(this.label9);
            this.splitContainer1.Panel2.Controls.Add(this.dField_grid);
            this.splitContainer1.Size = new System.Drawing.Size(574, 625);
            this.splitContainer1.SplitterDistance = 312;
            this.splitContainer1.TabIndex = 4;
            // 
            // remHField_btn
            // 
            this.remHField_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.remHField_btn.Location = new System.Drawing.Point(545, 3);
            this.remHField_btn.Name = "remHField_btn";
            this.remHField_btn.Size = new System.Drawing.Size(26, 23);
            this.remHField_btn.TabIndex = 14;
            this.remHField_btn.Text = "-";
            this.remHField_btn.UseVisualStyleBackColor = true;
            // 
            // addHField_btn
            // 
            this.addHField_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addHField_btn.Location = new System.Drawing.Point(513, 3);
            this.addHField_btn.Name = "addHField_btn";
            this.addHField_btn.Size = new System.Drawing.Size(26, 23);
            this.addHField_btn.TabIndex = 13;
            this.addHField_btn.Text = "+";
            this.addHField_btn.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(3, 6);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 17);
            this.label8.TabIndex = 1;
            this.label8.Text = "Header Fields";
            // 
            // hField_grid
            // 
            this.hField_grid.AllowUserToAddRows = false;
            this.hField_grid.AllowUserToDeleteRows = false;
            this.hField_grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.hField_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.hField_grid.Location = new System.Drawing.Point(3, 30);
            this.hField_grid.Name = "hField_grid";
            this.hField_grid.RowTemplate.Height = 24;
            this.hField_grid.Size = new System.Drawing.Size(571, 275);
            this.hField_grid.TabIndex = 15;
            // 
            // remDField_btn
            // 
            this.remDField_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.remDField_btn.Location = new System.Drawing.Point(545, 3);
            this.remDField_btn.Name = "remDField_btn";
            this.remDField_btn.Size = new System.Drawing.Size(26, 23);
            this.remDField_btn.TabIndex = 17;
            this.remDField_btn.Text = "-";
            this.remDField_btn.UseVisualStyleBackColor = true;
            // 
            // addDField_btn
            // 
            this.addDField_btn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addDField_btn.Location = new System.Drawing.Point(513, 3);
            this.addDField_btn.Name = "addDField_btn";
            this.addDField_btn.Size = new System.Drawing.Size(26, 23);
            this.addDField_btn.TabIndex = 16;
            this.addDField_btn.Text = "+";
            this.addDField_btn.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(2, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 17);
            this.label9.TabIndex = 3;
            this.label9.Text = "Data Fields";
            // 
            // dField_grid
            // 
            this.dField_grid.AllowUserToAddRows = false;
            this.dField_grid.AllowUserToDeleteRows = false;
            this.dField_grid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dField_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dField_grid.Location = new System.Drawing.Point(2, 30);
            this.dField_grid.Name = "dField_grid";
            this.dField_grid.RowTemplate.Height = 24;
            this.dField_grid.Size = new System.Drawing.Size(571, 274);
            this.dField_grid.TabIndex = 18;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.editSelect_btn);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.editColumns_btn);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Location = new System.Drawing.Point(12, 374);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(290, 84);
            this.groupBox4.TabIndex = 5;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "SQL";
            // 
            // editSelect_btn
            // 
            this.editSelect_btn.Location = new System.Drawing.Point(223, 50);
            this.editSelect_btn.Name = "editSelect_btn";
            this.editSelect_btn.Size = new System.Drawing.Size(46, 23);
            this.editSelect_btn.TabIndex = 9;
            this.editSelect_btn.Text = "...";
            this.editSelect_btn.UseVisualStyleBackColor = true;
            //this.editSelect_btn.Click += new System.EventHandler(this.editSelect_btn_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 53);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(115, 17);
            this.label11.TabIndex = 2;
            this.label11.Text = "Select Statement";
            // 
            // editColumns_btn
            // 
            this.editColumns_btn.Location = new System.Drawing.Point(223, 21);
            this.editColumns_btn.Name = "editColumns_btn";
            this.editColumns_btn.Size = new System.Drawing.Size(46, 23);
            this.editColumns_btn.TabIndex = 8;
            this.editColumns_btn.Text = "...";
            this.editColumns_btn.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 24);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(62, 17);
            this.label10.TabIndex = 0;
            this.label10.Text = "Columns";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.proessRow_btn);
            this.groupBox5.Controls.Add(this.verifyLuaSyn_btn);
            this.groupBox5.Controls.Add(this.label13);
            this.groupBox5.Controls.Add(this.label12);
            this.groupBox5.Controls.Add(this.editLuaFunc_btn);
            this.groupBox5.Location = new System.Drawing.Point(12, 464);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(290, 122);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "LUA";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 53);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(86, 17);
            this.label14.TabIndex = 12;
            this.label14.Text = "ProcessRow";
            // 
            // proessRow_btn
            // 
            this.proessRow_btn.Location = new System.Drawing.Point(223, 50);
            this.proessRow_btn.Name = "proessRow_btn";
            this.proessRow_btn.Size = new System.Drawing.Size(48, 23);
            this.proessRow_btn.TabIndex = 13;
            this.proessRow_btn.Text = "...";
            this.proessRow_btn.UseVisualStyleBackColor = true;
            // 
            // verifyLuaSyn_btn
            // 
            this.verifyLuaSyn_btn.Location = new System.Drawing.Point(223, 79);
            this.verifyLuaSyn_btn.Name = "verifyLuaSyn_btn";
            this.verifyLuaSyn_btn.Size = new System.Drawing.Size(48, 23);
            this.verifyLuaSyn_btn.TabIndex = 11;
            this.verifyLuaSyn_btn.Text = "...";
            this.verifyLuaSyn_btn.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 82);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(146, 17);
            this.label13.TabIndex = 2;
            this.label13.Text = "Verify Syntax/Runtime";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(6, 24);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(101, 17);
            this.label12.TabIndex = 1;
            this.label12.Text = "Misc Functions";
            // 
            // editLuaFunc_btn
            // 
            this.editLuaFunc_btn.Location = new System.Drawing.Point(223, 21);
            this.editLuaFunc_btn.Name = "editLuaFunc_btn";
            this.editLuaFunc_btn.Size = new System.Drawing.Size(48, 23);
            this.editLuaFunc_btn.TabIndex = 10;
            this.editLuaFunc_btn.Text = "...";
            this.editLuaFunc_btn.UseVisualStyleBackColor = true;
            // 
            // StructureEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(892, 649);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(909, 595);
            this.Name = "StructureEditor";
            this.Text = "Structure Editor";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.hField_grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dField_grid)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton readHead_off;
        private System.Windows.Forms.RadioButton readHead_on;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox specialCase_list;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox ext;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox fileName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tableName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox key;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.DataGridView hField_grid;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dField_grid;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button editSelect_btn;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button editColumns_btn;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Button verifyLuaSyn_btn;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Button editLuaFunc_btn;
        private System.Windows.Forms.Button addHField_btn;
        private System.Windows.Forms.Button remHField_btn;
        private System.Windows.Forms.Button remDField_btn;
        private System.Windows.Forms.Button addDField_btn;
        private System.Windows.Forms.TextBox path;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button proessRow_btn;
    }
}