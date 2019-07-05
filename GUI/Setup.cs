using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Grimoire.GUI
{
    public partial class Setup : Form
    {
        List<Panel> panels = new List<Panel>();
        int pnl_idx = 0;

        public Setup()
        {
            InitializeComponent();
        }

        private void Setup_Shown(object sender, EventArgs e)
        {
            info.Text = string.Format("Welcome to Grimoire v{0}\n\n", System.Windows.Forms.Application.ProductVersion) +
                        "Before you get into the chocolately goodness that is Grimoire, we will need to setup the grimoire.opt!\n\n" +
                        "First I need to know the locations for some of my awesome features to work right.";
        }

        private void next_btn_Click(object sender, EventArgs e)
        {
            if (pnl_idx < panels.Count - 1)
                panels[++pnl_idx].Visible = true;
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            if (pnl_idx > 0)
            {
                panels[pnl_idx--].Visible = false;
                panels[pnl_idx].Visible = true;
            }
        }

        private void Setup_Load(object sender, EventArgs e)
        {
            panels.Add(paths_pnl);
            panels.Add(db_pnl);


        }
    }
}
