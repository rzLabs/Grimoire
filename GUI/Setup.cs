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

        List<string> infoStrings = new List<string>()
        {
            string.Format("Welcome to Grimoire v{0}\n\n", System.Windows.Forms.Application.ProductVersion) +
                        "Before you get into the chocolately goodness that is Grimoire, we will need to setup the grimoire.opt!\n\n" +
                        "First I need to know the locations for some of my awesome features to work right.",
            "Alright!\n\nGreat, now that I know where to find things, I will need you to help me understand how you want your SQL to work."        };

        public Setup()
        {
            InitializeComponent();
        }

        private void Setup_Shown(object sender, EventArgs e)
        {
            info.Text = infoStrings[0];
        }

        private void next_btn_Click(object sender, EventArgs e)
        {
            if (pnl_idx < panels.Count - 1)
            {
                ++pnl_idx;
                panels[pnl_idx].Visible = true;
                info.Text = infoStrings[pnl_idx];
            }
        }

        private void back_btn_Click(object sender, EventArgs e)
        {
            if (pnl_idx > 0)
            {
                panels[pnl_idx].Visible = false;
                pnl_idx--;

                info.Text = infoStrings[pnl_idx];

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
