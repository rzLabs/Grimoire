using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;

namespace Grimoire.GUI
{
    public partial class MessageListBox : Form
    {
        public MessageListBox(string title, string msg, string[] filePaths)
        {
            InitializeComponent();
            Text = title;
            this.msg.Text = msg;
            populateList(filePaths);
            load_strings();
        }

        private void populateList(string[] filePaths)
        {
            foreach (string filePath in filePaths)
            {
                list.Items.Add(Path.GetFileName(filePath));
            }
        }

        private void yes_btn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void no_btn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Hide();
        }

        private void load_strings()
        {
            msg_grpBx.Text = strings.msg_grpBx;
            yesBtn.Text = strings.yesBtn;
            noBtn.Text = strings.noBtn;
        }
    }
}
