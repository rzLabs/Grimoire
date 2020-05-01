namespace Grimoire.Tabs.Styles
{
    partial class Item
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
            this.itemList = new System.Windows.Forms.ListView();
            this.loadBtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.itemList_style = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tooltip = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lvInput = new System.Windows.Forms.NumericUpDown();
            this.rankInput = new System.Windows.Forms.NumericUpDown();
            this.rankLbl = new System.Windows.Forms.Label();
            this.encInput = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.socketInput = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.wearType_lst = new System.Windows.Forms.ComboBox();
            this.wearTypeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.class_lst = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.group_lst = new System.Windows.Forms.ComboBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rankInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.encInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.socketInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.wearTypeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // itemList
            // 
            this.itemList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.itemList.GridLines = true;
            this.itemList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.itemList.LabelEdit = true;
            this.itemList.Location = new System.Drawing.Point(16, 15);
            this.itemList.MultiSelect = false;
            this.itemList.Name = "itemList";
            this.itemList.Size = new System.Drawing.Size(446, 390);
            this.itemList.TabIndex = 0;
            this.itemList.UseCompatibleStateImageBehavior = false;
            this.itemList.VirtualMode = true;
            // 
            // loadBtn
            // 
            this.loadBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.loadBtn.Location = new System.Drawing.Point(952, 517);
            this.loadBtn.Name = "loadBtn";
            this.loadBtn.Size = new System.Drawing.Size(75, 23);
            this.loadBtn.TabIndex = 1;
            this.loadBtn.Text = "Load";
            this.loadBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.group_lst);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.class_lst);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.wearType_lst);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.socketInput);
            this.groupBox1.Controls.Add(this.encInput);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.rankInput);
            this.groupBox1.Controls.Add(this.rankLbl);
            this.groupBox1.Controls.Add(this.lvInput);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(468, 15);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(559, 199);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            // 
            // itemList_style
            // 
            this.itemList_style.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.itemList_style.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.itemList_style.FormattingEnabled = true;
            this.itemList_style.Items.AddRange(new object[] {
            "Small Icon",
            "Large Icon",
            "Details"});
            this.itemList_style.Location = new System.Drawing.Point(341, 411);
            this.itemList_style.Name = "itemList_style";
            this.itemList_style.Size = new System.Drawing.Size(121, 24);
            this.itemList_style.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 414);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 17);
            this.label1.TabIndex = 4;
            this.label1.Text = "Tooltip";
            // 
            // tooltip
            // 
            this.tooltip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tooltip.Location = new System.Drawing.Point(16, 441);
            this.tooltip.Multiline = true;
            this.tooltip.Name = "tooltip";
            this.tooltip.Size = new System.Drawing.Size(446, 99);
            this.tooltip.TabIndex = 5;
            this.tooltip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(128, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Level:";
            // 
            // lvInput
            // 
            this.lvInput.Location = new System.Drawing.Point(180, 19);
            this.lvInput.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.lvInput.Name = "lvInput";
            this.lvInput.Size = new System.Drawing.Size(54, 22);
            this.lvInput.TabIndex = 1;
            this.lvInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.lvInput.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // rankInput
            // 
            this.rankInput.Location = new System.Drawing.Point(58, 19);
            this.rankInput.Name = "rankInput";
            this.rankInput.Size = new System.Drawing.Size(54, 22);
            this.rankInput.TabIndex = 3;
            this.rankInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // rankLbl
            // 
            this.rankLbl.AutoSize = true;
            this.rankLbl.Location = new System.Drawing.Point(6, 21);
            this.rankLbl.Name = "rankLbl";
            this.rankLbl.Size = new System.Drawing.Size(45, 17);
            this.rankLbl.TabIndex = 2;
            this.rankLbl.Text = "Rank:";
            // 
            // encInput
            // 
            this.encInput.Location = new System.Drawing.Point(347, 19);
            this.encInput.Maximum = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.encInput.Name = "encInput";
            this.encInput.Size = new System.Drawing.Size(54, 22);
            this.encInput.TabIndex = 5;
            this.encInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(251, 21);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Enhance (+):";
            // 
            // socketInput
            // 
            this.socketInput.Location = new System.Drawing.Point(486, 19);
            this.socketInput.Maximum = new decimal(new int[] {
            4,
            0,
            0,
            0});
            this.socketInput.Name = "socketInput";
            this.socketInput.Size = new System.Drawing.Size(54, 22);
            this.socketInput.TabIndex = 7;
            this.socketInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(418, 21);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Sockets:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(82, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Wear Type:";
            // 
            // wearType_lst
            // 
            this.wearType_lst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.wearType_lst.FormattingEnabled = true;
            this.wearType_lst.Location = new System.Drawing.Point(94, 54);
            this.wearType_lst.Name = "wearType_lst";
            this.wearType_lst.Size = new System.Drawing.Size(163, 24);
            this.wearType_lst.TabIndex = 9;
            // 
            // wearTypeBindingSource
            // 
            this.wearTypeBindingSource.DataSource = typeof(Grimoire.Tabs.Enums.ItemBase.WearType);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(295, 57);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Class:";
            // 
            // class_lst
            // 
            this.class_lst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.class_lst.FormattingEnabled = true;
            this.class_lst.Location = new System.Drawing.Point(347, 54);
            this.class_lst.Name = "class_lst";
            this.class_lst.Size = new System.Drawing.Size(193, 24);
            this.class_lst.TabIndex = 11;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 91);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 17);
            this.label7.TabIndex = 12;
            this.label7.Text = "Group:";
            // 
            // group_lst
            // 
            this.group_lst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.group_lst.FormattingEnabled = true;
            this.group_lst.Location = new System.Drawing.Point(94, 88);
            this.group_lst.Name = "group_lst";
            this.group_lst.Size = new System.Drawing.Size(163, 24);
            this.group_lst.TabIndex = 13;
            // 
            // Item
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tooltip);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.itemList_style);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.loadBtn);
            this.Controls.Add(this.itemList);
            this.Name = "Item";
            this.Size = new System.Drawing.Size(1041, 559);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lvInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rankInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.encInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.socketInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.wearTypeBindingSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView itemList;
        private System.Windows.Forms.Button loadBtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox itemList_style;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tooltip;
        private System.Windows.Forms.NumericUpDown socketInput;
        private System.Windows.Forms.NumericUpDown encInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown rankInput;
        private System.Windows.Forms.Label rankLbl;
        private System.Windows.Forms.NumericUpDown lvInput;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox wearType_lst;
        private System.Windows.Forms.BindingSource wearTypeBindingSource;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox class_lst;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox group_lst;
    }
}
