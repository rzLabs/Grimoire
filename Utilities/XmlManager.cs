using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Globalization;
using Grimoire.Localization.Structures;
using Grimoire.Localization.Enums;

using Serilog;

namespace Grimoire.Utilities
{
    public class XmlManager
    {
        Configuration.ConfigManager ConfigMan = GUI.Main.Instance.ConfigMan;

        List<Locale> locales = new List<Locale>();
        Locale locale
        {   
            get { return locales.Find(l => l.Name == key); }
        }

        string key;
        string localeDir;

        static XmlManager instance;
        public static XmlManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new XmlManager();

                return instance;
            }
            set { instance = value; }
        }

        public XmlManager()
        {
            key = ConfigMan["Locale"];
            localeDir = ConfigMan.GetDirectory("Directory", "Localization");

            Log.Information("Localization Manager Started.");

            compileLocales();

            Log.Information($"{locales.Count} loaded from:\n\t- {localeDir}");
        }

        void compileLocales()
        {
            if (string.IsNullOrEmpty(key))
            {
                string msg = "compileLocales() key is null! Please check your Config.json \"locale\" value! (example value: en-US)";

                Log.Error(msg);

                MessageBox.Show(msg, "XML Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if (!Directory.Exists(localeDir))
            {
                string msg = "compileLocales() localeDir does not exist! Please check your grimoire.opt \"locale.directory\" value! (example value: C:\\Grimoire\\Localization)";

                Log.Error(msg);

                MessageBox.Show(msg, "XML Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            string[] filePaths = Directory.GetFiles(localeDir);
            for (int i = 0; i < filePaths.Length; i++)
            {
                Log.Debug($"{filePaths.Length} locales found in:\n\t- {localeDir}");

                parseXML(filePaths[i]);
            }
        }

        public void RefreshLocale()
        {
            string localePath = string.Format(@"{0}\{1}.xml", localeDir, key);

            if (File.Exists(localePath))
            {
                locales.Remove(locale);
                parseXML(localePath);
            }
        }

        public void Localize(object sender, SenderType type)
        {
            if (locales.FindIndex(l => l.Name == key) == -1)
            {
                key = "en-US";
                string msg = string.Format("Requested Locale: {0} does not exist!\n\t Defaulting to en-US", key);

                Log.Warning(msg);

                MessageBox.Show(msg, "XML Warning", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }

            switch (type)
            {
                case SenderType.GUI:
                    {
                        Form f = (Form)sender;

                        if (f != null)
                        {
                            localizeControls(f.Controls);

                            Log.Debug($"{f.Controls.Count} control configurations loaded for gui: {f.Name}");
                        }
                    }
                    break;

                case SenderType.Tab:
                    {
                        UserControl u = (UserControl)sender;

                        if (u != null)
                        {
                            localizeControls(u.Controls);

                            Log.Debug($"{u.Controls.Count} control configurations loaded for tab: {u.Name}");
                        }
                    }
                    break;
            }
        }

        void parseXML(string filePath)
        {
            XDocument xDoc = XDocument.Load(filePath);

            List<XElement> element = xDoc.Elements("locale").ToList();
            if (element != null)
            {

                Locale locale = new Locale();

                if (!element[0].HasAttributes)
                {
                    Log.Error("<locale/> element does not have expected attributes!");

                    return;
                }

                locale.Name = element[0].FirstAttribute.Value;

                List<XElement> childNodes = element[0].Elements().ToList();
                locale.DisplayName = childNodes[0].Value;
                locale.Encoding = Convert.ToInt32(childNodes[1].Value);

                FontConfig globalFont = new FontConfig();
                RightToLeft rightToLeft = RightToLeft.No;

                if (childNodes.Count == 4) // Global font is likely defined
                {
                    if (childNodes[2].Name == "font")
                    {
                        List<XElement> fontElements = childNodes[2].Elements().ToList();

                        globalFont.Style = (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), fontElements[0].Value);
                        globalFont.Size = Convert.ToDouble(fontElements[1].Value, CultureInfo.InvariantCulture);

                        Log.Debug($"Global font defined.\nFamily: {globalFont.Name}\nStyle: {globalFont.Style.ToString()}\nNSize: {globalFont.Size}");
                    }
                }
                else if (childNodes.Count == 5) // Global right to left is likely defined
                {
                    if (childNodes[3].Name == "rightToLeft")
                    {
                        rightToLeft = (RightToLeft)Enum.Parse(typeof(RightToLeft), childNodes[3].Value);

                        Log.Debug("Global rightToLeft defined.");
                    }
                }

                // Get the <control/> nodes in the <controls/> elemenent
                childNodes = childNodes[childNodes.Count - 1].Elements().ToList();

                List<ControlConfig> controls = new List<ControlConfig>();
                for (int c = 0; c < childNodes.Count; c++)
                {
                    ControlConfig control = new ControlConfig();

                    if (childNodes[c].HasAttributes)
                    {
                        List<XAttribute> attributes = childNodes[c].Attributes().ToList();
                        if (attributes.Count >= 1)
                            control.Name = attributes[0].Value;

                        if (attributes.Count >= 2)
                            control.Comment = attributes[1].Value;
                    }
                    else
                    {
                        string msg = string.Format("<control/> at index: {0} does not have attributes! Ignoring!", c);

                        Log.Warning(msg);

                        MessageBox.Show(msg, "XML Exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    FontConfig controlFont = null;

                    List<XElement> fontElements = childNodes[c].Elements("font").ToList();
                    if (fontElements.Count > 0)
                    {
                        controlFont = new FontConfig();
                        controlFont.Name = fontElements[0].FirstAttribute.Value;

                        fontElements = fontElements[0].Elements().ToList();
                        controlFont.Style = (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), fontElements[0].Value);
                        controlFont.Size = Convert.ToDouble(fontElements[1].Value, CultureInfo.InvariantCulture);

                        Log.Debug($"Font detected.\n\t- Family: {controlFont.Name}\n\t- Style: {controlFont.Style.ToString()}\n\t- Size: {controlFont.Size}");
                    }

                    control.Font = (controlFont != null) ? controlFont : globalFont;

                    List<XElement> locationElements = childNodes[c].Elements("location").ToList();
                    if (locationElements.Count == 1)
                    {
                        string[] location = childNodes[c].Elements("location").ToList()[0].Value.Split(',');
                        control.Location = new System.Drawing.Point(int.Parse(location[0]), int.Parse(location[1]));
                    }
                    else
                        control.Location = new System.Drawing.Point(0, 0);

                    List<XElement> sizeElements = childNodes[c].Elements("size").ToList();
                    if (sizeElements.Count == 1)
                    {
                        string[] size = childNodes[c].Elements("size").ToList()[0].Value.Split(',');
                        control.Size = new System.Drawing.Size(int.Parse(size[0]), int.Parse(size[1]));
                    }
                    else
                        control.Size = new System.Drawing.Size(0, 0);

                    TextConfig text = new TextConfig();
                    text.Alignment = System.Drawing.ContentAlignment.MiddleLeft;

                    List<XElement> textElements = childNodes[c].Elements("text").ToList();

                    if (textElements.Count > 0)
                    {
                        if (textElements[0].HasAttributes)
                        {
                            List<XAttribute> attributes = textElements[0].Attributes().ToList();

                            XAttribute attribute = attributes.Find(a => a.Name == "align");
                            if (attribute != null)
                                text.Alignment = (System.Drawing.ContentAlignment)Enum.Parse(typeof(System.Drawing.ContentAlignment), attribute.Value);

                            attribute = attributes.Find(a => a.Name == "rightToLeft");
                            if (attribute != null)
                                text.RightToLeft = (RightToLeft)Enum.Parse(typeof(RightToLeft), attribute.Value);
                        }

                        text.Text = textElements[0].Value;
                    }
                    else
                        text.Text = string.Empty;

                    control.Text = text;

                    if (control.Populated)
                        controls.Add(control);
                }

                locale.Controls = controls;

                if (locale.Populated)
                    locales.Add(locale);

                Log.Debug($"{locale.Controls.Count} control configurations loaded from locale: {locale.Name} located at:\n\t- {filePath}");
            }
        }

        void localizeControls(Control.ControlCollection controls)
        {
            foreach (Control control in controls)
            {
                ControlConfig config = null;

                // If the control is a MenuStrip we have to loop through its base controls (toolstripmenuitem)
                // then if applicable we must loop through its children
                if (control.GetType() == typeof(MenuStrip))
                {
                    MenuStrip ms = (MenuStrip)control;

                    foreach (ToolStripMenuItem tsmi in ms.Items)
                    {
                        config = locale.Controls.Find(c => c.Name == tsmi.Name);

                        if (config != null)
                        {
                            tsmi.Text = config.Text.Text;
                            tsmi.TextAlign = config.Text.Alignment;
                            tsmi.RightToLeft = config.Text.RightToLeft;

                            if (!control.Size.IsEmpty)
                                tsmi.Size = config.Size;
                        }

                        if (tsmi.HasDropDownItems)
                            foreach (ToolStripMenuItem subTSMI in tsmi.DropDownItems)
                            {
                                config = locale.Controls.Find(c => c.Name == subTSMI.Name);

                                if (config != null)
                                {
                                    subTSMI.Text = config.Text.Text;
                                    subTSMI.TextAlign = config.Text.Alignment;
                                    subTSMI.RightToLeft = config.Text.RightToLeft;

                                    if (!control.Size.IsEmpty)
                                        subTSMI.Size = config.Size;
                                }
                            }
                    }
                }
                else if (control.GetType() == typeof(GroupBox))
                {
                    // Define the base group_box and set his text
                    GroupBox grpBx = (GroupBox)control;

                    config = locale.Controls.Find(c => c.Name == grpBx.Name);
                    if (config != null)
                        grpBx.Text = config.Text.Text;

                    // Define each child control inside of the group box
                    // set their attributes accordingly
                    for (int i = 0; i < grpBx.Controls.Count; i++)
                    {
                        Control ctrl = grpBx.Controls[i];

                        if (ctrl.GetType() == typeof(Button))
                        {
                            Button btn = (Button)ctrl;

                            config = locale.Controls.Find(c => c.Name == btn.Name);

                            if (config != null)
                                btn.TextAlign = config.Text.Alignment;
                        }
                        else if (ctrl.GetType() == typeof(RadioButton))
                        {
                            RadioButton rBtn = (RadioButton)ctrl;

                            config = locale.Controls.Find(c => c.Name == rBtn.Name);

                            if (config != null)
                                rBtn.TextAlign = config.Text.Alignment;
                        }
                        else if (ctrl.GetType() == typeof(Label))
                        {
                            Label lbl = (Label)ctrl;

                            config = locale.Controls.Find(c => c.Name == lbl.Name);

                            if (config != null)
                                lbl.TextAlign = config.Text.Alignment;
                        }
                        else if (ctrl.GetType() == typeof(CheckBox))
                        {
                            CheckBox chkBx = (CheckBox)ctrl;

                            config = locale.Controls.Find(c => c.Name == chkBx.Name);

                            if (config != null)
                                chkBx.TextAlign = config.Text.Alignment;
                        }
                        else if (ctrl.GetType() == typeof(TextBox)) { /*Textbox shouldn't be touched*/ }

                        if (config != null)
                        { 
                            ctrl.Font = new System.Drawing.Font(config.Font.Name, (float)config.Font.Size, config.Font.Style);
                            ctrl.RightToLeft = config.Text.RightToLeft;

                            if (!config.Location.IsEmpty)
                                ctrl.Location = config.Location;

                            if (!config.Size.IsEmpty)
                                ctrl.Size = config.Size;

                            ctrl.Text = config.Text.Text;
                        }

                        config = null;
                    }
                }
                else if (control.GetType() == typeof(ToolStrip))
                {                 
                    foreach (object tso in ((ToolStrip)control).Items)
                    {
                        if (tso.GetType() == typeof(ToolStripDropDownButton))
                        {
                            ToolStripDropDownButton tsb = (ToolStripDropDownButton)tso;

                            config = locale.Controls.Find(c => c.Name == tsb.Name);

                            if (config != null)
                            {
                                tsb.Text = config.Text.Text;
                                tsb.TextAlign = config.Text.Alignment;
                                tsb.Font = new System.Drawing.Font(config.Font.Name, (float)config.Font.Size, config.Font.Style);
                                tsb.RightToLeft = config.Text.RightToLeft;
                            }
                        }
                        else if (tso.GetType() == typeof(ToolStripButton))
                        {
                            ToolStripButton tsb = (ToolStripButton)tso;

                            config = locale.Controls.Find(c => c.Name == tsb.Name);

                            if (config != null)
                            {
                                tsb.Text = config.Text.Text;
                                tsb.TextAlign = config.Text.Alignment;
                                tsb.Font = new System.Drawing.Font(config.Font.Name, (float)config.Font.Size, config.Font.Style);
                                tsb.RightToLeft = config.Text.RightToLeft;
                            }
                        }
                        else if (tso.GetType() == typeof(ToolStripMenuItem))
                        {
                            ToolStripMenuItem tsi = (ToolStripMenuItem)tso;

                            config = locale.Controls.Find(c => c.Name == tsi.Name);

                            if (config != null)
                            {
                                tsi.Text = config.Text.Text;
                                tsi.TextAlign = config.Text.Alignment;
                                tsi.Font = new System.Drawing.Font(config.Font.Name, (float)config.Font.Size, config.Font.Style);
                                tsi.RightToLeft = config.Text.RightToLeft;
                            }
                        }
                        else if (tso.GetType() == typeof(ToolStripLabel))
                        {
                            ToolStripLabel tsl = (ToolStripLabel)tso;

                            config = locale.Controls.Find(c => c.Name == tsl.Name);

                            if (config != null)
                                {
                                tsl.Text = config.Text.Text;
                                tsl.TextAlign = config.Text.Alignment;
                                tsl.Font = new System.Drawing.Font(config.Font.Name, (float)config.Font.Size, config.Font.Style);
                                tsl.RightToLeft = config.Text.RightToLeft;
                            }
                        }

                        if (config != null)
                        {
                            if (!config.Location.IsEmpty)
                                control.Location = config.Location;

                            if (!config.Size.IsEmpty)
                                control.Size = config.Size;

                            control.Text = config.Text.Text;
                        }

                        config = null;
                    }
                }
                else
                {
                    config = locale.Controls.Find(c => c.Name == control.Name);

                    if (config != null)
                    {
                        System.Drawing.Font font = new System.Drawing.Font(config.Font.Name, (float)config.Font.Size, config.Font.Style);

                        control.Font = font;

                        Type t = control.GetType();
                        if (t == typeof(CheckBox))
                        {
                            CheckBox chkBx = (CheckBox)control;
                            chkBx.TextAlign = config.Text.Alignment;
                        }
                        else if (t == typeof(Label))
                        {
                            Label lbl = (Label)control;
                            lbl.TextAlign = config.Text.Alignment;
                        }
                        else if (t == typeof(Button))
                        {
                            Button lbl = (Button)control;
                            lbl.TextAlign = config.Text.Alignment;
                        }
                        else if (t == typeof(ToolStripMenuItem))
                        {
                            ToolStrip tsm = (ToolStrip)control;
                            ToolStripItem tsi = tsm.Items[config.Name];

                            tsi.TextAlign = config.Text.Alignment;
                        }
                        else if (t == typeof(ToolStripDropDownButton))
                        {
                            ToolStrip tsm = (ToolStrip)control;
                            ToolStripItem tsi = tsm.Items[config.Name];

                            tsi.TextAlign = config.Text.Alignment;
                        }
                        else
                        {
                            /*Alignment doesn't exist bruh*/
                        }

                        if (!config.Location.IsEmpty)
                            control.Location = config.Location;

                        if (!config.Size.IsEmpty)
                            control.Size = config.Size;

                        control.Text = config.Text.Text;
                    }
                    else
                        Log.Warning($"Control: {control.Name} is not configured! It will default.");
                }
            }
        }
    }
}
