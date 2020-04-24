namespace Grimoire.GUI
{
    partial class DataRebuild
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("All Data");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Data.001");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Data.002");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Data.003");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Data.004");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Data.005");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Data.006");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Data.007");
            System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem("Data.008");
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DataRebuild));
            this.dataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.currentProgress = new System.Windows.Forms.ProgressBar();
            this.dataList = new System.Windows.Forms.ListView();
            this.dataName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fragPercent = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.rebuildBtn = new System.Windows.Forms.Button();
            this.status = new System.Windows.Forms.Label();
            this.totalProgress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.dataChart)).BeginInit();
            this.SuspendLayout();
            // 
            // dataChart
            // 
            this.dataChart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.dataChart.ChartAreas.Add(chartArea1);
            legend1.Name = "dataLegend";
            this.dataChart.Legends.Add(legend1);
            this.dataChart.Location = new System.Drawing.Point(12, 246);
            this.dataChart.Name = "dataChart";
            this.dataChart.Size = new System.Drawing.Size(543, 326);
            this.dataChart.TabIndex = 0;
            this.dataChart.Text = "dataChart";
            // 
            // currentProgress
            // 
            this.currentProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.currentProgress.Location = new System.Drawing.Point(12, 658);
            this.currentProgress.Name = "currentProgress";
            this.currentProgress.Size = new System.Drawing.Size(543, 23);
            this.currentProgress.TabIndex = 1;
            // 
            // dataList
            // 
            this.dataList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dataName,
            this.dataSize,
            this.fileCount,
            this.fragPercent});
            this.dataList.FullRowSelect = true;
            this.dataList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.dataList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8,
            listViewItem9});
            this.dataList.Location = new System.Drawing.Point(12, 12);
            this.dataList.MultiSelect = false;
            this.dataList.Name = "dataList";
            this.dataList.Size = new System.Drawing.Size(543, 228);
            this.dataList.TabIndex = 2;
            this.dataList.UseCompatibleStateImageBehavior = false;
            this.dataList.View = System.Windows.Forms.View.Details;
            this.dataList.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.dataList_ItemSelectionChanged);
            // 
            // dataName
            // 
            this.dataName.Text = "Name";
            this.dataName.Width = 80;
            // 
            // dataSize
            // 
            this.dataSize.Text = "Size";
            this.dataSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.dataSize.Width = 100;
            // 
            // fileCount
            // 
            this.fileCount.Text = "File Count";
            this.fileCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fileCount.Width = 100;
            // 
            // fragPercent
            // 
            this.fragPercent.Text = "Fragmentation";
            this.fragPercent.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.fragPercent.Width = 110;
            // 
            // rebuildBtn
            // 
            this.rebuildBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.rebuildBtn.Location = new System.Drawing.Point(480, 588);
            this.rebuildBtn.Name = "rebuildBtn";
            this.rebuildBtn.Size = new System.Drawing.Size(75, 23);
            this.rebuildBtn.TabIndex = 3;
            this.rebuildBtn.Text = "Rebuild";
            this.rebuildBtn.UseVisualStyleBackColor = true;
            this.rebuildBtn.Click += new System.EventHandler(this.rebuildBtn_Click);
            // 
            // status
            // 
            this.status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.status.AutoSize = true;
            this.status.Location = new System.Drawing.Point(9, 594);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(0, 17);
            this.status.TabIndex = 4;
            // 
            // totalProgress
            // 
            this.totalProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.totalProgress.Location = new System.Drawing.Point(12, 620);
            this.totalProgress.Maximum = 8;
            this.totalProgress.Name = "totalProgress";
            this.totalProgress.Size = new System.Drawing.Size(543, 23);
            this.totalProgress.TabIndex = 5;
            // 
            // DataRebuild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(568, 693);
            this.Controls.Add(this.totalProgress);
            this.Controls.Add(this.status);
            this.Controls.Add(this.rebuildBtn);
            this.Controls.Add(this.dataList);
            this.Controls.Add(this.currentProgress);
            this.Controls.Add(this.dataChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            //this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DataRebuild";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rebuild Wizard";
            this.Load += new System.EventHandler(this.DataRebuild_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataChart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart dataChart;
        private System.Windows.Forms.ProgressBar currentProgress;
        private System.Windows.Forms.ListView dataList;
        private System.Windows.Forms.ColumnHeader dataName;
        private System.Windows.Forms.ColumnHeader dataSize;
        private System.Windows.Forms.ColumnHeader fileCount;
        private System.Windows.Forms.Button rebuildBtn;
        private System.Windows.Forms.ColumnHeader fragPercent;
        private System.Windows.Forms.Label status;
        private System.Windows.Forms.ProgressBar totalProgress;
    }
}