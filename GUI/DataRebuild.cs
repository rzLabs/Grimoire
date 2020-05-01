using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;
using System.Windows.Forms;
using DataCore;
using Grimoire.Utilities;
using Grimoire.Logs.Enums;

namespace Grimoire.GUI
{
    public partial class DataRebuild : Form
    {
        Tabs.Manager tManager = Tabs.Manager.Instance;
        Logs.Manager lManager = Logs.Manager.Instance;
        Core core = null;
        XmlManager xMan = XmlManager.Instance;
        
        public DataRebuild()
        {
            InitializeComponent();
            core = tManager.DataCore;
            hook_core_events();
            dataChart.Series.Add(new Series() { Name = "All Data", ChartType = SeriesChartType.Pie });
            dataList.Items[0].Selected = true;
            localize();
        }

        private void hook_core_events()
        {
            core.CurrentMaxDetermined += Core_CurrentMaxDetermined;
            core.CurrentProgressChanged += Core_CurrentProgressChanged;
            core.CurrentProgressReset += Core_CurrentProgressReset;
            core.MessageOccured += Core_MessageOccured;
        }

        private void Core_MessageOccured(object sender, MessageArgs e)
        {
            Invoke(new MethodInvoker(delegate { status.Text = e.Message; }));
            lManager.Enter(Sender.DATA, Level.NOTICE, e.Message);
        }

        private void Core_CurrentMaxDetermined(object sender, CurrentMaxArgs e)
        {
            Invoke(new MethodInvoker(delegate { currentProgress.Maximum = (int)e.Maximum; }));
        }

        private void Core_CurrentProgressChanged(object sender, CurrentChangedArgs e)
        {
            Invoke(new MethodInvoker(delegate { currentProgress.Value = (int)e.Value; }));
        }

        private void Core_CurrentProgressReset(object sender, CurrentResetArgs e)
        {
            Invoke(new MethodInvoker(delegate
            {
                currentProgress.Maximum = 100;
                currentProgress.Value = 0;
            }));
        }

        private void DataRebuild_Load(object sender, EventArgs e)
        {
            status.Text = "Analyzing data.000 index...";

            for (int dataId = 0; dataId <= 8; dataId++)
                set_dataList_info(dataId);

            status.ResetText();
        }

        private void set_dataList_info(int dataId)
        {
            string dataPath = string.Format(@"{0}\data.00{1}", core.DataDirectory, dataId);
            long physicalSize = core.GetPhysicalSize(dataId);
            long storedSize = core.GetStoredSize(dataId);
            double diffSize = physicalSize - storedSize;
            double fragPercent = diffSize / physicalSize;

            if (dataList.Items[dataId].SubItems.Count == 4)
            {
                dataList.Items[dataId].SubItems[1].Text = Utilities.StringExt.FormatToSize(physicalSize);
                dataList.Items[dataId].SubItems[2].Text = core.GetFileCount(dataId).ToString();
                dataList.Items[dataId].SubItems[3].Text = fragPercent.ToString("0.0%");
            }
            else
            {
                dataList.Items[dataId].SubItems.Add(Utilities.StringExt.FormatToSize(physicalSize));
                dataList.Items[dataId].SubItems.Add(core.GetFileCount(dataId).ToString());
                dataList.Items[dataId].SubItems.Add(fragPercent.ToString("0.0%"));
            }            
        }

        private void dataList_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            int idx = e.ItemIndex;

            if (dataChart.Series[0].Points.Count > 0)
                dataChart.Series[0].Points.Clear();                

            string dataPath = string.Format(@"{0}\data.00{1}", core.DataDirectory, idx);
            long physicalSize = core.GetPhysicalSize(idx);
            long storedSize = core.GetStoredSize(idx);
            double diffSize = physicalSize - storedSize;
            double fragPercent = diffSize / physicalSize;

            dataChart.Series[0].Points.Add(physicalSize);
            dataChart.Series[0].Points[0].LegendText = (idx > 0) ? string.Format("Data.00{0}", idx) : "All Data";
            dataChart.Series[0].Points[0].Color = Color.Green;
            dataChart.Series[0].Points.AddY(diffSize);
            dataChart.Series[0].Points[1].LegendText = "Fragmented";
            dataChart.Series[0].Points[1].Color = Color.Red;
            dataChart.Series[0].Label = string.Format("{0:P2}", (1.0 - fragPercent));
            dataChart.Series[0].Points[1].Label = string.Format("{0:P2}", fragPercent);
        }

        private async void rebuildBtn_Click(object sender, EventArgs e)
        {
            string warningMsg = string.Format("You are about to rebuild your client data storage files!\n\n Backups are currently: {0}\n\nAre you sure you want to continue?",
                (Utilities.OPT.GetBool("data.backup") ? "ON" : "OFF"));
            if (MessageBox.Show(warningMsg, "Input Required", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            string statusMsg = "Rebuilding Data Files...";
            status.Text = statusMsg;
            lManager.Enter(Sender.DATA, Level.NOTICE, statusMsg);

            try
            {
                dataList.Items[0].SubItems[1].Text = string.Empty;
                dataList.Items[0].SubItems[3].Text = string.Empty;

                for (int i = 1; i <= 8; i++)
                {
                    await Task.Run(() => { core.RebuildDataFile(i, core.DataDirectory); });

                    cleanup(i);

                    set_dataList_info(i);
                    dataList.Items[i].Selected = true;
                    totalProgress.Value++;
                }

                set_dataList_info(0);
                core.Save(core.DataDirectory);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Rebuild Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lManager.Enter(Sender.DATA, Level.ERROR, ex);
            }
            finally
            {
                statusMsg = "Rebuild complete!";
                MessageBox.Show(statusMsg, "Rebuild Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lManager.Enter(Sender.DATA, Level.NOTICE, statusMsg);
                status.ResetText();
            }

        }

        private void cleanup(int dataId)
        {
            string path = string.Format(@"{0}\data.00{1}", core.DataDirectory, dataId);
            string newPath = path + "_NEW";
            string bakPath = path + "_BAK";

            if (File.Exists(path) & File.Exists(newPath))
            {
                if (!File.Exists(bakPath))
                    if (MessageBox.Show(string.Format("Backup Not Detected! Are you sure you want to delete the original at\n\n{0}", path)) != DialogResult.Yes)
                        return;

                File.Delete(path);
                File.Move(newPath, path);

                lManager.Enter(Sender.DATA, Level.NOTICE, "Backup cleaning completed!\n\tFile Deleted: {0}\n\tFile Moved\n\t\tFrom:{1}\n\t\tTo:{2}", path, newPath, path);
            }

        }

        public void localize()
        {
            xMan.Localize(this, Localization.Enums.SenderType.GUI);
        }
    }
}
