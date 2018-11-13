using System;
using System.Windows.Forms;

namespace Grimoire.GUI
{
    public partial class InputBox : Form
    {
        public InputBox(string description, string defaultText)
        {
            InitializeComponent();
            Text = description;
            input.Text = defaultText;
            DialogResult = DialogResult.Cancel;
        }

        public InputBox(string description, bool resizable)
        {
            InitializeComponent();
            Text = description;
            this.FormBorderStyle = (resizable) ? FormBorderStyle.SizableToolWindow : FormBorderStyle.FixedToolWindow;
            input.Multiline = resizable;
            DialogResult = DialogResult.Cancel;
        }

        public string Value { get { return (input.Text.Length > 0) ? input.Text : null; } set { input.Text = value; } }

        private void okBtn_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            this.Hide();
        }

        private void InputBox_Shown(object sender, EventArgs e)
        {
            input.Focus();
        }
    }
}
