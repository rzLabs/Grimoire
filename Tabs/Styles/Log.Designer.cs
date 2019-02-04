namespace Grimoire.Tabs.Styles
{
    partial class Log
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.logGrid = new System.Windows.Forms.DataGridView();
            this.sender = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.level = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dateTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.message = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.stackTrace = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.logGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // logGrid
            // 
            this.logGrid.AllowUserToAddRows = false;
            this.logGrid.AllowUserToDeleteRows = false;
            this.logGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.logGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.logGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.sender,
            this.level,
            this.dateTime,
            this.message,
            this.stackTrace});
            this.logGrid.Location = new System.Drawing.Point(16, 54);
            this.logGrid.Name = "logGrid";
            this.logGrid.ReadOnly = true;
            this.logGrid.RowTemplate.Height = 24;
            this.logGrid.Size = new System.Drawing.Size(985, 465);
            this.logGrid.TabIndex = 0;
            // 
            // sender
            // 
            this.sender.HeaderText = "Sender";
            this.sender.Name = "sender";
            this.sender.ReadOnly = true;
            // 
            // level
            // 
            this.level.HeaderText = "Level";
            this.level.Name = "level";
            this.level.ReadOnly = true;
            // 
            // dateTime
            // 
            this.dateTime.HeaderText = "Date Time";
            this.dateTime.Name = "dateTime";
            this.dateTime.ReadOnly = true;
            this.dateTime.Width = 120;
            // 
            // message
            // 
            this.message.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.message.DefaultCellStyle = dataGridViewCellStyle3;
            this.message.HeaderText = "Message";
            this.message.Name = "message";
            this.message.ReadOnly = true;
            this.message.Width = 94;
            // 
            // stackTrace
            // 
            this.stackTrace.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.stackTrace.DefaultCellStyle = dataGridViewCellStyle4;
            this.stackTrace.HeaderText = "Stack Trace";
            this.stackTrace.Name = "stackTrace";
            this.stackTrace.ReadOnly = true;
            // 
            // Log
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.logGrid);
            this.Name = "Log";
            this.Size = new System.Drawing.Size(1004, 551);
            ((System.ComponentModel.ISupportInitialize)(this.logGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView logGrid;
        private System.Windows.Forms.DataGridViewTextBoxColumn sender;
        private System.Windows.Forms.DataGridViewTextBoxColumn level;
        private System.Windows.Forms.DataGridViewTextBoxColumn dateTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn message;
        private System.Windows.Forms.DataGridViewTextBoxColumn stackTrace;
    }
}
