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
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "Data.001",
            "9999999999",
            "999999"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Data.002");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Data.003");
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Data.004");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("Data.005");
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem("Data.006");
            System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem("Data.007");
            System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem("Data.008");
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.dataList = new System.Windows.Forms.ListView();
            this.rebuiltBtn = new System.Windows.Forms.Button();
            this.dataName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.dataSize = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.fileCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.listView1 = new System.Windows.Forms.ListView();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(270, 12);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(362, 368);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(12, 415);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(620, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // dataList
            // 
            this.dataList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.dataName,
            this.dataSize,
            this.fileCount});
            this.dataList.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5,
            listViewItem6,
            listViewItem7,
            listViewItem8});
            this.dataList.Location = new System.Drawing.Point(12, 12);
            this.dataList.Name = "dataList";
            this.dataList.Size = new System.Drawing.Size(252, 193);
            this.dataList.TabIndex = 2;
            this.dataList.UseCompatibleStateImageBehavior = false;
            this.dataList.View = System.Windows.Forms.View.Details;
            // 
            // rebuiltBtn
            // 
            this.rebuiltBtn.Location = new System.Drawing.Point(557, 386);
            this.rebuiltBtn.Name = "rebuiltBtn";
            this.rebuiltBtn.Size = new System.Drawing.Size(75, 23);
            this.rebuiltBtn.TabIndex = 3;
            this.rebuiltBtn.Text = "Rebuild";
            this.rebuiltBtn.UseVisualStyleBackColor = true;
            // 
            // dataName
            // 
            this.dataName.Text = "Name";
            this.dataName.Width = 68;
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
            this.fileCount.Width = 76;
            // 
            // listView1
            // 
            this.listView1.Location = new System.Drawing.Point(12, 225);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(252, 155);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // DataRebuild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(645, 450);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.rebuiltBtn);
            this.Controls.Add(this.dataList);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.chart1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "DataRebuild";
            this.Text = "Rebuild Wizard";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ListView dataList;
        private System.Windows.Forms.ColumnHeader dataName;
        private System.Windows.Forms.ColumnHeader dataSize;
        private System.Windows.Forms.ColumnHeader fileCount;
        private System.Windows.Forms.Button rebuiltBtn;
        private System.Windows.Forms.ListView listView1;
    }
}