using System.Windows.Forms;
using Grimoire.Utilities;

namespace Grimoire.GUI
{
    public partial class Settings : Form
    {
        object properties;

        public Settings()
        {
            InitializeComponent();
            properties = new Structures.Settings();
            propertyGrid.SelectedObject = properties;
        }
    }
}
