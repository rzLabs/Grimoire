using System;
using System.Collections.Generic;
using System.Windows.Forms;

using Serilog;

namespace Grimoire.Tabs
{
    public class Manager
    {
        #region Properties

        readonly GUI.Main main = null;
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

        public Daedalus.Core RDBCore
        {
            get
            {
                if (Style == Style.RDB)
                {
                    Daedalus.Core ret = null;

                    GUI.Main.Instance.Invoke(new MethodInvoker(delegate {
                        ret = ((Styles.rdbTab)pages?[tabs.SelectedIndex].Controls[0]).Core;
                    }));

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

            GUI.Main.Instance.Invoke(new MethodInvoker(delegate {
                ret = ((Styles.rdbTab)pages[key].Controls[0]).Core;
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

        public Styles.rdbTab RDBTab
        {
            get
            {
                Styles.rdbTab ret = null;

                if (Style == Style.RDB)
                {
                    main.Invoke(new MethodInvoker(delegate {
                        if (tabs.SelectedIndex != -1)
                            ret = (Styles.rdbTab)pages[tabs.SelectedIndex].Controls[0];
                    }));

                }

                return ret;
            }
        }

        public Styles.rdbTab RDBTabByKey(string key)
        {
            if (pages.ContainsKey(key))
                return (Styles.rdbTab)pages?[key].Controls[0];

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

        public Manager()
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

                case Style.MARKET:
                    tab.Controls.Add(new Styles.MarketEditor() { Dock = DockStyle.Fill });
                    text = "Market Utility";
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
            tabs.SelectedTab = pages[key];

            Log.Information($"New {((Style)style).ToString()} tab named {text} created!");
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

                case Style.ITEM:
                    ItemTab.Clear();
                    break;
            }

            Log.Information($"{Text} has been cleared!");
        }

        public void Destroy()
        {
            Log.Information($"{Text} has been closed!");

            pages.RemoveAt(RightClick_TabIdx);
            tabs.SelectedIndex = pages.Count - 1;
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