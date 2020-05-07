using System.Windows.Forms;
using Grimoire.Utilities;
using Grimoire.Configuration;

namespace Grimoire.GUI
{
    public partial class Settings : Form
    {
        ConfigMan configMan = GUI.Main.Instance.ConfigMan;
        object properties;

        public Settings()
        {
            InitializeComponent();
            properties = new Structures.Settings();
            propertyGrid.SelectedObject = properties;
        }

        private async void Settings_FormClosing(object sender, FormClosingEventArgs e) =>
            await configMan.Save();
    }
}
