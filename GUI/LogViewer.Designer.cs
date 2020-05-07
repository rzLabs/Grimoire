namespace Grimoire.GUI
{
    partial class LogViewer
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
            this.logView = new BrightIdeasSoftware.DataListView();
            this.displayType_lst = new System.Windows.Forms.ComboBox();
            this.displayType = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.logView)).BeginInit();
            this.SuspendLayout();
            // 
            // logView
            // 
            this.logView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logView.CellEditUseWholeCell = false;
            this.logView.Cursor = System.Windows.Forms.Cursors.Default;
            this.logView.DataSource = null;
            this.logView.HideSelection = false;
            this.logView.Location = new System.Drawing.Point(0, 34);
            this.logView.Name = "logView";
            this.logView.Size = new System.Drawing.Size(800, 387);
            this.logView.TabIndex = 0;
            this.logView.UseCompatibleStateImageBehavior = false;
            this.logView.View = System.Windows.Forms.View.Details;
            // 
            // displayType_lst
            // 
            this.displayType_lst.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.displayType_lst.FormattingEnabled = true;
            this.displayType_lst.Location = new System.Drawing.Point(62, 7);
            this.displayType_lst.Name = "displayType_lst";
            this.displayType_lst.Size = new System.Drawing.Size(121, 21);
            this.displayType_lst.TabIndex = 1;
            this.displayType_lst.SelectedIndexChanged += new System.EventHandler(this.displayType_lst_SelectedIndexChanged);
            // 
            // displayType
            // 
            this.displayType.AutoSize = true;
            this.displayType.Location = new System.Drawing.Point(12, 10);
            this.displayType.Name = "displayType";
            this.displayType.Size = new System.Drawing.Size(44, 13);
            this.displayType.TabIndex = 2;
            this.displayType.Text = "Display:";
            // 
            // LogViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 421);
            this.Controls.Add(this.displayType);
            this.Controls.Add(this.displayType_lst);
            this.Controls.Add(this.logView);
            this.Name = "LogViewer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Log Viewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogViewer_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.logView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private BrightIdeasSoftware.DataListView logView;
        private System.Windows.Forms.ComboBox displayType_lst;
        private System.Windows.Forms.Label displayType;
    }
}