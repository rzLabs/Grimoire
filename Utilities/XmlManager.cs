using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Globalization;
using Grimoire.Localization.Structures;
using Grimoire.Localization.Enums;
using Grimoire.Logs.Enums;

namespace Grimoire.Utilities
{
    public class XmlManager
    {
        Logs.Manager lManager = Logs.Manager.Instance;
        Configuration.ConfigMan ConfigMan = GUI.Main.Instance.ConfigMan;

        List<Locale> locales = new List<Locale>();
        Locale locale
        {   
            get { return locales.Find(l => l.Name == key); }
        }
        //string key = OPT.GetString("locale");
        //string localeDir = OPT.GetString("locale.directory") ?? string.Format(@"{0}\Localization", Directory.GetCurrentDirectory());

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

            lManager.Enter(Sender.MANAGER, Level.NOTICE, "Localization Manager Started.");

            compileLocales();

            lManager.Enter(Sender.MANAGER, Level.DEBUG, string.Format("{0} locales loaded from:\n\t- {1}", locales.Count, localeDir));
        }

        void compileLocales()
        {
            if (string.IsNullOrEmpty(key))
            {
                string msg = "compileLocales() key is null! Please check your grimpoite.opt \"locale\" value! (example value: en-US)";
                lManager.Enter(Sender.MANAGER, Level.ERROR, msg);
                System.Windows.Forms.MessageBox.Show(msg, "XML Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            if (!Directory.Exists(localeDir))
            {
                string msg = "compileLocales() localeDir does not exist! Please check your grimoire.opt \"locale.directory\" value! (example value: C:\\Grimoire\\Localization)";
                lManager.Enter(Sender.MANAGER, Level.ERROR, msg);
                System.Windows.Forms.MessageBox.Show(msg, "XML Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }

            string[] filePaths = Directory.GetFiles(localeDir);
            for (int i = 0; i < filePaths.Length; i++)
            {
                lManager.Enter(Sender.MANAGER, Level.DEBUG, "{0} locales found in:\n\t- {1}", filePaths.Length, localeDir);

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
                lManager.Enter(Sender.MANAGER, Level.NOTICE, msg);
                System.Windows.Forms.MessageBox.Show(msg, "XML Warning", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Hand);
            }

            switch (type)
            {
                case SenderType.GUI:
                    {
                        System.Windows.Forms.Form f = (System.Windows.Forms.Form)sender;

                        if (f != null)
                        {
                            localizeControls(f.Controls);

                            lManager.Enter(Sender.MANAGER, Level.DEBUG, string.Format("{0} control configurations loaded for gui: {1}", f.Controls.Count, f.Name));
                        }
                    }
                    break;

                case SenderType.Tab:
                    {
                        System.Windows.Forms.UserControl u = (System.Windows.Forms.UserControl)sender;

                        if (u != null)
                        {
                            localizeControls(u.Controls);

                            lManager.Enter(Sender.MANAGER, Level.DEBUG, string.Format("{0} control configurations loaded for tab: {1}", u.Controls.Count, u.Name));
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
                lManager.Enter(Sender.MANAGER, Level.DEBUG, "<locale/> found for locale:{0}", key);

                Locale locale = new Locale();

                if (!element[0].HasAttributes)
                {
                    lManager.Enter(Sender.MANAGER, Level.ERROR, "<locale/> element does not have expected attributes!");
                    return;
                }

                locale.Name = element[0].FirstAttribute.Value;

                List<XElement> childNodes = element[0].Elements().ToList();
                locale.DisplayName = childNodes[0].Value;
                locale.Encoding = Convert.ToInt32(childNodes[1].Value);

                FontConfig globalFont = new FontConfig();
                System.Windows.Forms.RightToLeft rightToLeft = System.Windows.Forms.RightToLeft.No;

                if (childNodes.Count == 4) // Global font is likely defined
                {
                    if (childNodes[2].Name == "font")
                    {
                        List<XElement> fontElements = childNodes[2].Elements().ToList();

                        globalFont.Style = (System.Drawing.FontStyle)Enum.Parse(typeof(System.Drawing.FontStyle), fontElements[0].Value);
                        globalFont.Size = Convert.ToDouble(fontElements[1].Value, CultureInfo.InvariantCulture);

                        lManager.Enter(Sender.MANAGER, Level.DEBUG, "Global <font/> is defined.\nFamily:{0}\nStyle:{1}\nSize:{2}", globalFont.Name,
                                                                                                                                             globalFont.Style.ToString(),
                                                                                                                                             globalFont.Size);
                    }
                }
                else if (childNodes.Count == 5) // Global right to left is likely defined
                {
                    if (childNodes[3].Name == "rightToLeft")
                    {
                        rightToLeft = (System.Windows.Forms.RightToLeft)Enum.Parse(typeof(System.Windows.Forms.RightToLeft), childNodes[3].Value);

                        lManager.Enter(Sender.MANAGER, Level.DEBUG, "Global <rightToLeft/> defined.");
                    }
                }

                // Get the <control/> nodes in the <controls/> elemenent
                childNodes = childNodes[childNodes.Count - 1].Elements().ToList();

                lManager.Enter(Sender.MANAGER, Level.DEBUG, "{0} <control/> nodes found.", childNodes.Count);

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

                        lManager.Enter(Sender.MANAGER, Level.DEBUG, "<control/> {0} has expected attributes.", control.Name);
                    }
                    else
                    {
                        string msg = string.Format("<control/> at index: {0} does not have attributes! Ignoring!", c);
                        lManager.Enter(Sender.MANAGER, Level.WARNING, msg);
                        System.Windows.Forms.MessageBox.Show(msg, "XML Exception", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
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

                        lManager.Enter(Sender.MANAGER, Level.DEBUG, "<font/> detected.\nFamily:{0}\nStyle:{2}\nSize:{1}", controlFont.Name,
                                                                                                                                                        controlFont.Style.ToString(),
                                                                                                                                                        controlFont.Size);
                    }
                    else
                        lManager.Enter(Sender.MANAGER, Level.DEBUG, "No <font/> detected for control: {0}", control.Name);

                    control.Font = (controlFont != null) ? controlFont : globalFont;

                    List<XElement> locationElements = childNodes[c].Elements("location").ToList();
                    if (locationElements.Count == 1)
                    {
                        string[] location = childNodes[c].Elements("location").ToList()[0].Value.Split(',');
                        control.Location = new System.Drawing.Point(int.Parse(location[0]), int.Parse(location[1]));

                        lManager.Enter(Sender.MANAGER, Level.DEBUG, "<location/> detected.\nx:{0}\ny:{1}", control.Location.X,
                                                                                                                     control.Location.Y);
                    }
                    else
                        control.Location = new System.Drawing.Point(0, 0);

                    List<XElement> sizeElements = childNodes[c].Elements("size").ToList();
                    if (sizeElements.Count == 1)
                    {
                        string[] size = childNodes[c].Elements("size").ToList()[0].Value.Split(',');
                        control.Size = new System.Drawing.Size(int.Parse(size[0]), int.Parse(size[1]));

                        lManager.Enter(Sender.MANAGER, Level.DEBUG, "<size/> detected. \nheight:{0}\nwidth:{1}", control.Size.Height,
                                                                                                                           control.Size.Width);
                    }
                    else
                        control.Size = new System.Drawing.Size(0, 0);

                    TextConfig text = new TextConfig();
                    text.Alignment = System.Drawing.ContentAlignment.MiddleLeft;

                    List<XElement> textElements = childNodes[c].Elements("text").ToList();

                    if (textElements.Count > 0)
                    {
                        lManager.Enter(Sender.MANAGER, Level.DEBUG, "<text/> element detected!");

                        if (textElements[0].HasAttributes)
                        {
                            List<XAttribute> attributes = textElements[0].Attributes().ToList();

                            XAttribute attribute = attributes.Find(a => a.Name == "align");
                            if (attribute != null)
                                text.Alignment = (System.Drawing.ContentAlignment)Enum.Parse(typeof(System.Drawing.ContentAlignment), attribute.Value);

                            attribute = attributes.Find(a => a.Name == "rightToLeft");
                            if (attribute != null)
                                text.RightToLeft = (System.Windows.Forms.RightToLeft)Enum.Parse(typeof(System.Windows.Forms.RightToLeft), attribute.Value);
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

                lManager.Enter(Sender.MANAGER, Level.DEBUG, string.Format("{0} controls configurations loaded from locale: {1} from\n\t- {2}", locale.Controls.Count, locale.Name, filePath));
            }
        }

        void localizeControls(System.Windows.Forms.Control.ControlCollection controls)
        {
            foreach (System.Windows.Forms.Control control in controls)
            {
                ControlConfig config = null;

                // If the control is a MenuStrip we have to loop through its base controls (toolstripmenuitem)
                // then if applicable we must loop through its children
                if (control.GetType() == typeof(System.Windows.Forms.MenuStrip))
                {
                    System.Windows.Forms.MenuStrip ms = (System.Windows.Forms.MenuStrip)control;

                    foreach (System.Windows.Forms.ToolStripMenuItem tsmi in ms.Items)
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
                            foreach (System.Windows.Forms.ToolStripMenuItem subTSMI in tsmi.DropDownItems)
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
                else if (control.GetType() == typeof(System.Windows.Forms.GroupBox))
                {
                    // Define the base group_box and set his text
                    System.Windows.Forms.GroupBox grpBx = (System.Windows.Forms.GroupBox)control;

                    config = locale.Controls.Find(c => c.Name == grpBx.Name);
                    if (config != null)
                        grpBx.Text = config.Text.Text;

                    // Define each child control inside of the group box
                    // set their attributes accordingly
                    for (int i = 0; i < grpBx.Controls.Count; i++)
                    {
                        System.Windows.Forms.Control ctrl = grpBx.Controls[i];

                        if (ctrl.GetType() == typeof(System.Windows.Forms.Button))
                        {
                            System.Windows.Forms.Button btn = (System.Windows.Forms.Button)ctrl;

                            config = locale.Controls.Find(c => c.Name == btn.Name);

                            if (config != null)
                                btn.TextAlign = config.Text.Alignment;
                        }
                        else if (ctrl.GetType() == typeof(System.Windows.Forms.RadioButton))
                        {
                            System.Windows.Forms.RadioButton rBtn = (System.Windows.Forms.RadioButton)ctrl;

                            config = locale.Controls.Find(c => c.Name == rBtn.Name);

                            if (config != null)
                                rBtn.TextAlign = config.Text.Alignment;
                        }
                        else if (ctrl.GetType() == typeof(System.Windows.Forms.Label))
                        {
                            System.Windows.Forms.Label lbl = (System.Windows.Forms.Label)ctrl;

                            config = locale.Controls.Find(c => c.Name == lbl.Name);

                            if (config != null)
                                lbl.TextAlign = config.Text.Alignment;
                        }
                        else if (ctrl.GetType() == typeof(System.Windows.Forms.CheckBox))
                        {
                            System.Windows.Forms.CheckBox chkBx = (System.Windows.Forms.CheckBox)ctrl;

                            config = locale.Controls.Find(c => c.Name == chkBx.Name);

                            if (config != null)
                                chkBx.TextAlign = config.Text.Alignment;
                        }
                        else if (ctrl.GetType() == typeof(System.Windows.Forms.TextBox)) { /*Textbox shouldn't be touched*/ }

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
                else if (control.GetType() == typeof(System.Windows.Forms.ToolStrip))
                {                 
                    foreach (object tso in ((System.Windows.Forms.ToolStrip)control).Items)
                    {
                        if (tso.GetType() == typeof(System.Windows.Forms.ToolStripDropDownButton))
                        {
                            System.Windows.Forms.ToolStripDropDownButton tsb = (System.Windows.Forms.ToolStripDropDownButton)tso;

                            config = locale.Controls.Find(c => c.Name == tsb.Name);

                            if (config != null)
                            {
                                tsb.Text = config.Text.Text;
                                tsb.TextAlign = config.Text.Alignment;
                                tsb.Font = new System.Drawing.Font(config.Font.Name, (float)config.Font.Size, config.Font.Style);
                                tsb.RightToLeft = config.Text.RightToLeft;
                            }
                        }
                        else if (tso.GetType() == typeof(System.Windows.Forms.ToolStripButton))
                        {
                            System.Windows.Forms.ToolStripButton tsb = (System.Windows.Forms.ToolStripButton)tso;

                            config = locale.Controls.Find(c => c.Name == tsb.Name);

                            if (config != null)
                            {
                                tsb.Text = config.Text.Text;
                                tsb.TextAlign = config.Text.Alignment;
                                tsb.Font = new System.Drawing.Font(config.Font.Name, (float)config.Font.Size, config.Font.Style);
                                tsb.RightToLeft = config.Text.RightToLeft;
                            }
                        }
                        else if (tso.GetType() == typeof(System.Windows.Forms.ToolStripMenuItem))
                        {
                            System.Windows.Forms.ToolStripMenuItem tsi = (System.Windows.Forms.ToolStripMenuItem)tso;

                            config = locale.Controls.Find(c => c.Name == tsi.Name);

                            if (config != null)
                            {
                                tsi.Text = config.Text.Text;
                                tsi.TextAlign = config.Text.Alignment;
                                tsi.Font = new System.Drawing.Font(config.Font.Name, (float)config.Font.Size, config.Font.Style);
                                tsi.RightToLeft = config.Text.RightToLeft;
                            }
                        }
                        else if (tso.GetType() == typeof(System.Windows.Forms.ToolStripLabel))
                        {
                            System.Windows.Forms.ToolStripLabel tsl = (System.Windows.Forms.ToolStripLabel)tso;

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
                        if (t == typeof(System.Windows.Forms.CheckBox))
                        {
                            System.Windows.Forms.CheckBox chkBx = (System.Windows.Forms.CheckBox)control;
                            chkBx.TextAlign = config.Text.Alignment;
                        }
                        else if (t == typeof(System.Windows.Forms.Label))
                        {
                            System.Windows.Forms.Label lbl = (System.Windows.Forms.Label)control;
                            lbl.TextAlign = config.Text.Alignment;
                        }
                        else if (t == typeof(System.Windows.Forms.Button))
                        {
                            System.Windows.Forms.Button lbl = (System.Windows.Forms.Button)control;
                            lbl.TextAlign = config.Text.Alignment;
                        }
                        else if (t == typeof(System.Windows.Forms.ToolStripMenuItem))
                        {
                            System.Windows.Forms.ToolStrip tsm = (System.Windows.Forms.ToolStrip)control;
                            System.Windows.Forms.ToolStripItem tsi = tsm.Items[config.Name];

                            tsi.TextAlign = config.Text.Alignment;
                        }
                        else if (t == typeof(System.Windows.Forms.ToolStripDropDownButton))
                        {
                            System.Windows.Forms.ToolStrip tsm = (System.Windows.Forms.ToolStrip)control;
                            System.Windows.Forms.ToolStripItem tsi = tsm.Items[config.Name];

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
                        lManager.Enter(Sender.MANAGER, Level.DEBUG, "Control: {0} is not configured! It will default.", control.Name);
                }
            }
        }
    }
}
