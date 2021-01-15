using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Grimoire.Logs.Enums;

namespace Grimoire.Tabs
{
    public class Manager
    {
        readonly GUI.Main main = null;
        readonly Logs.Manager lManager = Logs.Manager.Instance;
        readonly TabControl tabs = null;
        readonly TabControl.TabPageCollection pages = null;
        static Manager instance;
        public static Manager Instance
        {
            get
            {
                if (instance == null)
                    instance = new Manager();

                return instance;
            }
        }

        public int RightClick_TabIdx = 0;

        public Manager()
        {
            main = GUI.Main.Instance;
            tabs = main.TabControl;
            pages = tabs.TabPages;
            lManager.Enter(Sender.MANAGER, Level.NOTICE, "Tab Manager Initialized.");
        }

        public string Text
        {
            get
            {
                string ret = null;
                main.Invoke(new MethodInvoker(delegate
                {
                    ret = pages[tabs.SelectedIndex].Text;
                }));

                return ret;
            }
            set {
                main.Invoke(new MethodInvoker(delegate
                {
                    lManager.Enter(Sender.MANAGER, Level.NOTICE,"Tab: {0} name updated to {1}", pages[tabs.SelectedIndex].Text, value);
                    pages[tabs.SelectedIndex].Text = value;
                }));
            }
        }

        public TabPage Page
        {
            get { return pages[tabs.SelectedIndex]; }
        }

        public Style Style
        {
            get
            {
                string key = null;
                main.Invoke(new MethodInvoker(delegate
                {
                    key = pages[tabs.SelectedIndex].Name;
                }));

                return getStyle(key);
            }
        }

        public string[] GetKeysByStyle(Style style)
        {
            List<string> keys = new List<string>();

            if (pages == null || pages.Count == 0)
                return null;

            foreach (TabPage page in pages)
            {
                Style cmpStyle = getStyle(page.Name);

                if (style == cmpStyle)
                    keys.Add(page.Name);
            }

            return keys.ToArray();
        }

        public DataCore.Core DataCore
        {
            get
            {
                if (Style == Style.DATA)
                {
                    DataCore.Core ret = null;
                    GUI.Main.Instance.Invoke(new MethodInvoker(delegate { ret = ((Styles.Data)pages[tabs.SelectedIndex].Controls[0]).Core; }));
                    return ret;
                }

                return null;
            }
        }

        public DataCore.Core DataCoreByKey(string key)
        {
            if (pages == null || pages.Count == 0)
                return null;

            if (!pages.ContainsKey(key))
                return null;

            DataCore.Core ret = null;
            GUI.Main.Instance.Invoke(new MethodInvoker(delegate { ret = ((Styles.Data)pages[key].Controls[0]).Core; }));

            return ret;
        }

        public Daedalus.Core RDBCore
        {
            get
            {
                if (Style == Style.RDB)
                {
                    Daedalus.Core ret = null;
                    GUI.Main.Instance.Invoke(new MethodInvoker(delegate { ret = ((Styles.rdbTab)pages[tabs.SelectedIndex].Controls[0]).Core; }));
                    return ret;
                }

                return null;
            }
        }

        public Daedalus.Core RDBCoreByKey(string key)
        {
            if (pages == null || pages.Count == 0)
                return null;

            if (!pages.ContainsKey(key))
                return null;

            Daedalus.Core ret = null;
            GUI.Main.Instance.Invoke(new MethodInvoker(delegate { ret = ((Styles.rdbTab)pages[key].Controls[0]).Core; }));

            return ret;
        }

        public Styles.Data DataTab
        {
            get
            {
                Styles.Data ret = null;

                if (Style == Style.DATA)
                {
                    main.Invoke(new MethodInvoker(delegate
                    {
                        ret = (Styles.Data)pages[tabs.SelectedIndex].Controls[0];
                    }));

                }

                return ret;
            }
        }

        public Styles.rdbTab RDBTab
        {
            get
            {
                Styles.rdbTab ret = null;

                if (Style == Style.RDB)
                {
                    main.Invoke(new MethodInvoker(delegate
                    {
                        ret = (Styles.rdbTab)pages[tabs.SelectedIndex].Controls[0];
                    }));

                }

                return ret;
            }
        }

        public Styles.Hasher HashTab
        {
            get
            {
                Styles.Hasher ret = null;

                if (Style == Style.HASHER)
                {
                    
                    main.Invoke(new MethodInvoker(delegate 
                    {
                        ret = (Styles.Hasher)pages[tabs.SelectedIndex].Controls[0];
                    }));

                }

                return ret;
            }
        }

        public void Create(Style style)
        {
            string key = string.Format("{0}_{1}", pages.Count, (int)style);
            string text = string.Empty;
            TabPage tab = new TabPage() { Name = key };

            switch (style)
            {
                case Style.RDB:
                    tab.Controls.Add(new Styles.rdbTab(key) { Dock = DockStyle.Fill });
                    text = "RDB Utility";
                    break;

                case Style.DATA:
                    tab.Controls.Add(new Styles.Data(key) { Dock = DockStyle.Fill });
                    text = "Data Utility";
                    break;

                case Style.HASHER:
                    tab.Controls.Add(new Styles.Hasher() { Dock = DockStyle.Fill });
                    text = "Hash Utility";
                    break;

                case Style.ITEM:
                    tab.Controls.Add(new Styles.Item() { Dock = DockStyle.Fill });
                    text = "Item Utility";
                    break;

                //case Style.DROPS:
                //    tab.Controls.Add(new Styles.DropEditor() { Dock = DockStyle.Fill });
                //    text = "Drop Utility";
                //    break;
            }

            pages.Add(tab);
            SetText(key, text);
            tabs.SelectedTab = pages[key];
            lManager.Enter(Sender.MANAGER, Level.NOTICE,"Tab created with name: {0} and style: {1}", text, ((Style)style).ToString());
        }

        public void Clear()
        {
            switch (Style)
            {
                case Style.RDB:
                    RDBTab.Clear();
                    RDBCore.ClearData();
                    break;

                case Style.DATA:
                    DataTab.Clear();
                    DataCore.Clear();
                    break;

                case Style.HASHER:
                    HashTab.Clear();
                    break;
            }

            lManager.Enter(Sender.MANAGER, Level.NOTICE,"Tab: {0} contents have been cleared.", Page.Text);
        }

        public void Destroy()
        {
            lManager.Enter(Sender.MANAGER, Level.NOTICE,"Tab: {0} has been closed.", Page.Text);
            pages.RemoveAt(RightClick_TabIdx);
            tabs.SelectedIndex = pages.Count - 1;          
        }

        public void SetText(string key, string text)
        {
            if (pages.ContainsKey(key))
                pages[key].Text = text;
            else
                lManager.Enter(Sender.MANAGER, Level.ERROR, "Tab Manager could not SetText because tab with key: {0} does not exist!", key);
        }

        private Style getStyle(string key)
        {
            if (string.IsNullOrEmpty(key))
                return Style.NONE;

            string[] keyBlocks = key.Split('_');

            if (keyBlocks.Length != 2)
            {
                lManager.Enter(Sender.MANAGER, Level.ERROR, $"Failed to get tab style for provided key: {key}");
                return Style.NONE;
            }

            int styleID = Convert.ToInt32(keyBlocks[1]);

            return (Style)styleID;
        }
    }
}