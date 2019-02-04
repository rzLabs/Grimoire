using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            lManager.Enter(Logs.Sender.MANAGER, Logs.Level.NOTICE, "Tab Manager Initialized.");
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
                    lManager.Enter(Logs.Sender.MANAGER, Logs.Level.NOTICE,"Tab: {0} name updated to {1}", pages[tabs.SelectedIndex].Text, value);
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
                int style_id = (!string.IsNullOrEmpty(key)) ? Convert.ToInt32(key.Split('_')[1]) : 99;

                return (Style)style_id;
            }
        }

        public DataCore.Core DataCore
        {
            get
            {
                if (Style == Style.DATA)
                {
                    DataCore.Core ret = null;
                    GUI.Main.Instance.Invoke(new MethodInvoker(delegate { ret = ((Tabs.Styles.Data)pages[tabs.SelectedIndex].Controls[0]).Core; }));
                    return ret;
                }

                return null;
            }
        }

        public rdbCore.Core RDBCore
        {
            get
            {
                if (Style == Style.RDB)
                {
                    rdbCore.Core ret = null;
                    GUI.Main.Instance.Invoke(new MethodInvoker(delegate { ret = ((Tabs.Styles.rdbTab)pages[tabs.SelectedIndex].Controls[0]).Core; }));
                    return ret;
                }

                return null;
            }
        }

        public Tabs.Styles.Data DataTab
        {
            get
            {
                Tabs.Styles.Data ret = null;

                if (Style == Style.DATA)
                {
                    main.Invoke(new MethodInvoker(delegate
                    {
                        ret = (Tabs.Styles.Data)pages[tabs.SelectedIndex].Controls[0];
                    }));

                }

                return ret;
            }
        }

        public Tabs.Styles.rdbTab RDBTab
        {
            get
            {
                Tabs.Styles.rdbTab ret = null;

                if (Style == Style.RDB)
                {
                    main.Invoke(new MethodInvoker(delegate
                    {
                        ret = (Tabs.Styles.rdbTab)pages[tabs.SelectedIndex].Controls[0];
                    }));

                }

                return ret;
            }
        }

        public Tabs.Styles.Hasher HashTab
        {
            get
            {
                Tabs.Styles.Hasher ret = null;

                if (Style == Style.HASHER)
                {
                    
                    main.Invoke(new MethodInvoker(delegate 
                    {
                        ret = (Tabs.Styles.Hasher)pages[tabs.SelectedIndex].Controls[0];
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

                case Style.USEFLAG:
                    tab.Controls.Add(new Styles.UseFlag() { Dock = DockStyle.Fill });
                    text = "Use Flag Utility";
                    break;

                //case Style.DROPS:
                //    tab.Controls.Add(new Styles.DropEditor() { Dock = DockStyle.Fill });
                //    text = "Drop Utility";
                //    break;

                case Style.LOG:
                    tab.Controls.Add(new Styles.Log() { Dock = DockStyle.Fill });
                    text = "Log Utility";
                    break;
            }

            pages.Add(tab);
            tabs.SelectedTab = pages[key];
            tabs.SelectedTab.Text = text;
            lManager.Enter(Logs.Sender.MANAGER, Logs.Level.NOTICE,"Tab created with name: {0} and style: {1}", text, ((Style)style).ToString());
        }

        public void Clear()
        {
            switch (Style)
            {
                case Style.RDB:
                    RDBTab.Clear();
                    RDBCore.ClearData();
                    break;

                case Style.HASHER:
                    //TODO: Time to implement me bruh
                    break;

                case Style.DATA:
                    DataTab.Clear();
                    DataCore.Clear();
                    break;
            }

            lManager.Enter(Logs.Sender.MANAGER, Logs.Level.NOTICE,"Tab: {0} contents have been cleared.", Page.Text);
        }

        public void Destroy()
        {
            lManager.Enter(Logs.Sender.MANAGER, Logs.Level.NOTICE,"Tab: {0} has been closed.", Page.Text);
            pages.RemoveAt(RightClick_TabIdx);
            tabs.SelectedIndex = pages.Count - 1;
            
        }
    }
}
