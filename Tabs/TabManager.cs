using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Grimoire.Configuration;
using Grimoire.GUI;
using Grimoire.Structures;

using Serilog;

namespace Grimoire.Tabs
{
    public class TabManager
    {
        #region Properties

        readonly Main main = null;
        readonly ConfigManager configMgr = Main.Instance.ConfigMgr;
        readonly TabControl tabs = null;
        readonly TabControl.TabPageCollection pages = null;
        static TabManager instance;

        public static TabManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new TabManager();

                return instance;
            }
        }

        public int RightClick_TabIdx = 0;

        public int SelectedIndex
        {
            get => tabs.SelectedIndex;
            set => tabs.SelectedIndex = value;
        }

        public int Count => tabs.TabCount;

        public string Text
        {
            get
            {
                string ret = null;
                main.Invoke(new MethodInvoker(delegate {
                    ret = pages[tabs.SelectedIndex].Text;
                }));

                return ret;
            }
            set
            {
                main.Invoke(new MethodInvoker(delegate {
                    Log.Information($"Tab {pages[tabs.SelectedIndex]?.Text} text set to: {value}");

                    pages[tabs.SelectedIndex].Text = value;
                }));
            }
        }

        public TabPage Page => pages?[tabs.SelectedIndex];

        public TabPage this[string key] => pages?[key];

        public Style Style
        {
            get
            {
                string key = null;
                main.Invoke(new MethodInvoker(delegate {
                    if (tabs.SelectedIndex == -1)
                    {
                        Log.Error("Selected tab index is out of range!");
                        return;
                    }

                    key = pages[tabs.SelectedIndex].Name;
                }));

                return getStyle(key);
            }
        }

        public DataCore.Core DataCore
        {
            get
            {
                if (Style == Style.DATA)
                {
                    DataCore.Core ret = null;

                    GUI.Main.Instance.Invoke(new MethodInvoker(delegate {
                        ret = ((Styles.Data)pages?[tabs.SelectedIndex].Controls[0]).Core;
                    }));

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

        public StructureObject ArcInstance
        {
            get
            {
                if (Style == Style.RDB2)
                {
                    StructureObject ret = null;

                    GUI.Main.Instance.Invoke(new MethodInvoker(delegate {
                        ret = ((Styles.RDB2)pages?[tabs.SelectedIndex].Controls[0]).StructObject;
                    }));

                    return ret;
                }

                return null;
            }
        }

        public StructureObject ArcInstanceByKey(string key)
        {
            if (pages == null || pages.Count == 0)
                return null;

            if (!pages.ContainsKey(key))
                return null;

            StructureObject ret = null;

            GUI.Main.Instance.Invoke(new MethodInvoker(delegate {
                ret = ((Styles.RDB2)pages[key].Controls[0]).StructObject;
            }));

            return ret;
        }

        public Styles.Data DataTab
        {
            get
            {
                Styles.Data ret = null;

                if (Style == Style.DATA)
                {
                    main.Invoke(new MethodInvoker(delegate {
                        ret = (Styles.Data)pages?[tabs.SelectedIndex].Controls[0];
                    }));

                }

                return ret;
            }
        }

        public Styles.Data DataTabByKey(string key)
        {
            if (pages.ContainsKey(key))
                return (Styles.Data)pages?[key].Controls[0];

            return null;
        }

        public Styles.RDB2 RDBTab
        {
            get
            {
                Styles.RDB2 ret = null;

                if (Style == Style.RDB2)
                {
                    main.Invoke(new MethodInvoker(delegate {
                        if (tabs.SelectedIndex != -1)
                            ret = (Styles.RDB2)pages[tabs.SelectedIndex].Controls[0];
                    }));

                }

                return ret;
            }
        }

        public Styles.RDB2 RDBTabByKey(string key)
        {
            if (pages.ContainsKey(key))
                return (Styles.RDB2)pages?[key].Controls[0];

            return null;
        }

        public Styles.Hasher HashTab
        {
            get
            {
                Styles.Hasher ret = null;

                if (Style == Style.HASHER)
                {
                    main.Invoke(new MethodInvoker(delegate {
                        ret = (Styles.Hasher)pages[tabs.SelectedIndex].Controls[0];
                    }));
                }

                return ret;
            }
        }

        public Styles.Hasher HashTabByKey(string key)
        {
            if (pages.ContainsKey(key))
                return (Styles.Hasher)pages?[key].Controls[0];

            return null;
        }

        public Styles.Item ItemTab
        {
            get
            {
                Styles.Item ret = null;

                if (Style == Style.ITEM)
                {
                    main.Invoke(new MethodInvoker(delegate {
                        ret = (Styles.Item)pages?[tabs.SelectedIndex]?.Controls[0];
                    }));
                }

                return ret;
            }
        }

        #endregion

        public TabManager()
        {
            main = GUI.Main.Instance;
            tabs = main.TabControl;
            pages = tabs.TabPages;

            Log.Information("Tab Manager initialized!");
        }

        #region Methods (public)

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

        public string Create(Style style)
        {
            string key = string.Format("{0}_{1}", pages.Count, (int)style);
            string text = string.Empty;

            TabPage tab = new TabPage() { Name = key };

            switch (style)
            {
                //case Style.RDB:
                //    tab.Controls.Add(new Styles.rdbTab(key) { Dock = DockStyle.Fill });
                //    text = "RDB Utility";
                //    break;

                case Style.DATA:
                    tab.Controls.Add(new Styles.Data(key, true) { Dock = DockStyle.Fill });
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

                case Style.MARKET:
                    tab.Controls.Add(new Styles.MarketEditor() { Dock = DockStyle.Fill });
                    text = "Market Utility";
                    break;

                case Style.LAUNCHER:
                    tab.Controls.Add(new Styles.Launcher() { Dock = DockStyle.Fill });
                    text = "Launcher";
                    break;

                case Style.RDB2:
                    tab.Controls.Add(new Styles.RDB2() { Dock = DockStyle.Fill });
                    text = "RDB2 Utility";
                    break;
            }

            int styleCnt = 0;
            for (int i = 0; i < pages.Count; i++)
                if (pages[i].Text == text || pages[i].Text.Contains(text))
                    styleCnt++;

            if (styleCnt > 0)
                text += $" ({styleCnt})";

            pages.Add(tab);
            SetText(key, text);

            Log.Verbose($"New {((Style)style).ToString()} tab named {text} created!");

            if (configMgr.Get<bool>("FocusNewTabs", "Tab", true))
                tabs.SelectedTab = pages[key];

            return key;
        }

        public void Clear()
        {
            switch (Style)
            {
                case Style.RDB:
                    RDBTab.Clear();
                    ArcInstance.Rows.Clear();
                    break;

                case Style.DATA:
                    DataTab.Clear();
                    DataCore.Clear();
                    break;

                case Style.HASHER:
                    HashTab.Clear();
                    break;

                case Style.ITEM:
                    ItemTab.Clear();
                    break;
            }

            Log.Verbose($"{Text} has been cleared!");
        }

        public void Destroy()
        {
            pages.RemoveAt(RightClick_TabIdx);
            tabs.SelectedIndex = Math.Max(pages.Count - 1, 0);

            Log.Information($"{Text} has been closed!");
        }

        public void SetText(string key, string text)
        {
            if (pages.ContainsKey(key))
                pages[key].Text = text;
            else
                Log.Error($"Could not set text because tab {key} does not exist!");
        }

        #endregion

        private Style getStyle(string key)
        {
            if (string.IsNullOrEmpty(key))
                return Style.NONE;

            string[] keyBlocks = key.Split('_');

            if (keyBlocks.Length != 2)
            {
                Log.Error($"Failed to get tab style for {key}");

                return Style.NONE;
            }

            int styleID = Convert.ToInt32(keyBlocks[1]);

            return (Style)styleID;
        }
    }
}