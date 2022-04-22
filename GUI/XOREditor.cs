using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Be.Windows.Forms;

namespace Grimoire.GUI
{
    public partial class XOREditor : Form
    {
        byte[] defXORKey = new byte[]
        {
            0x77, 0xe8, 0x5e, 0xec, 0xb7, 0x4e, 0xc1, 0x87, 0x4f, 0xe6, 0xf5, 0x3c, 0x1f, 0xb3, 0x15, 0x43,
            0x6a, 0x49, 0x30, 0xa6, 0xbf, 0x53, 0xa8, 0x35, 0x5b, 0xe5, 0x9e, 0x0e, 0x41, 0xec, 0x22, 0xb8,
            0xd4, 0x80, 0xa4, 0x8c, 0xce, 0x65, 0x13, 0x1d, 0x4b, 0x08, 0x5a, 0x6a, 0xbb, 0x6f, 0xad, 0x25,
            0xb8, 0xdd, 0xcc, 0x77, 0x30, 0x74, 0xac, 0x8c, 0x5a, 0x4a, 0x9a, 0x9b, 0x36, 0xbc, 0x53, 0x0a,
            0x3c, 0xf8, 0x96, 0x0b, 0x5d, 0xaa, 0x28, 0xa9, 0xb2, 0x82, 0x13, 0x6e, 0xf1, 0xc1, 0x93, 0xa9,
            0x9e, 0x5f, 0x20, 0xcf, 0xd4, 0xcc, 0x5b, 0x2e, 0x16, 0xf5, 0xc9, 0x4c, 0xb2, 0x1c, 0x57, 0xee,
            0x14, 0xed, 0xf9, 0x72, 0x97, 0x22, 0x1b, 0x4a, 0xa4, 0x2e, 0xb8, 0x96, 0xef, 0x4b, 0x3f, 0x8e,
            0xab, 0x60, 0x5d, 0x7f, 0x2c, 0xb8, 0xad, 0x43, 0xad, 0x76, 0x8f, 0x5f, 0x92, 0xe6, 0x4e, 0xa7,
            0xd4, 0x47, 0x19, 0x6b, 0x69, 0x34, 0xb5, 0x0e, 0x62, 0x6d, 0xa4, 0x52, 0xb9, 0xe3, 0xe0, 0x64,
            0x43, 0x3d, 0xe3, 0x70, 0xf5, 0x90, 0xb3, 0xa2, 0x06, 0x42, 0x02, 0x98, 0x29, 0x50, 0x3f, 0xfd,
            0x97, 0x58, 0x68, 0x01, 0x8c, 0x1e, 0x0f, 0xef, 0x8b, 0xb3, 0x41, 0x44, 0x96, 0x21, 0xa8, 0xda,
            0x5e, 0x8b, 0x4a, 0x53, 0x1b, 0xfd, 0xf5, 0x21, 0x3f, 0xf7, 0xba, 0x68, 0x47, 0xf9, 0x65, 0xdf,
            0x52, 0xce, 0xe0, 0xde, 0xec, 0xef, 0xcd, 0x77, 0xa2, 0x0e, 0xbc, 0x38, 0x2f, 0x64, 0x12, 0x8d,
            0xf0, 0x5c, 0xe0, 0x0b, 0x59, 0xd6, 0x2d, 0x99, 0xcd, 0xe7, 0x01, 0x15, 0xe0, 0x67, 0xf4, 0x32,
            0x35, 0xd4, 0x11, 0x21, 0xc3, 0xde, 0x98, 0x65, 0xed, 0x54, 0x9d, 0x1c, 0xb9, 0xb0, 0xaa, 0xa9,
            0x0c, 0x8a, 0xb4, 0x66, 0x60, 0xe1, 0xff, 0x2e, 0xc8, 0x00, 0x43, 0xa9, 0x67, 0x37, 0xdb, 0x9c
        };

        byte[] xorKey = null;

        DynamicByteProvider provider = null;

        public XOREditor()
        {
            InitializeComponent();
        }

        private void ts_file_load_key_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofDlg = new OpenFileDialog())
            {
                if (ofDlg.ShowDialog(this) == DialogResult.OK)
                    xorKey = File.ReadAllBytes(ofDlg.FileName);
            }

            if (xorKey != null && xorKey.Length > 0)
            {
                provider = new DynamicByteProvider(xorKey);

                if (provider != null)
                    set_provider();
            }
        }

        private void ts_file_load_def_Click(object sender, EventArgs e)
        {
            clear_provider();

            xorKey = defXORKey;

            if (xorKey != null && xorKey.Length > 0)
            {
                provider = new DynamicByteProvider(defXORKey);

                if (provider != null)
                    set_provider();
            }
        }

        private void ts_file_load_config_Click(object sender, EventArgs e)
        {
            xorKey = Main.Instance.ConfigMgr.GetByteArray("ModifiedXORKey");

            if (xorKey != null && xorKey.Length > 0)
            {
                provider = new DynamicByteProvider(xorKey);

                if (provider != null)
                    set_provider();
            }
            else //TODO: BAD MESSAGE!
                MessageBox.Show("No XOR key has been defined in the Config.json! Please load from key file or default.", "Nothing Loaded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void ts_reset_Click(object sender, EventArgs e)
        {
            clear_provider();

            if (xorKey is null)
                return;

            provider = new DynamicByteProvider(xorKey);

            if (provider != null)
                set_provider();
        }

        private void ts_def_Click(object sender, EventArgs e) => ts_file_load_def_Click(this, null);

        private async void ts_file_save_config_Click(object sender, EventArgs e)
        {
            if (provider != null && provider.Length > 0)
            {
                Main.Instance.ConfigMgr.UpdateByteArray("ModifiedXORKey", provider.Bytes.ToArray());
                await Main.Instance.ConfigMgr.Save();
            }
        }

        private void ts_file_save_key_Click(object sender, EventArgs e)
        {
            string keyPath = $"{Directory.GetCurrentDirectory()}\\grimoire_xor_key";

            if (provider != null && provider.Length > 0)
            {
                if (File.Exists(keyPath))
                    File.Delete(keyPath);

                using (FileStream fs = File.Create(keyPath))
                {
                    byte[] buffer = provider.Bytes.ToArray();

                    fs.Write(buffer, 0, buffer.Length);
                }
            }
        }

        private void Provider_Changed(object sender, EventArgs e) => generate_unKey();

        void clear_provider()
        {
            if (provider != null)
            {
                provider.Bytes.Clear();
                provider = null;
                hexBox.ByteProvider = null;
                unKey_txtBox.Clear();
            }
        }

        void set_provider()
        {
            provider.Changed += Provider_Changed;

            hexBox.ByteProvider = provider;
            hexBox.Refresh();

            generate_unKey();
        }

        void generate_unKey()
        {
            int colCount = 0;
            int byteCount = 0; //3 per round
            string uintStr = null;
            string outStr = null;

            for (int i = 0; i < provider.Length; i++)
            {
                byte b = provider.Bytes[i];

                string byteStr = b.ToString("X2").ToLower();
                char[] byteArray = byteStr.ToCharArray();
                Array.Reverse(byteArray);

                uintStr += new string(byteArray);
                byteCount++;

                if (byteCount == 4)
                {
                    byteCount = 0;

                    char[] uintArray = uintStr.ToCharArray();
                    Array.Reverse(uintArray);

                    string colStr = string.Format("0x{0}, ", new string(uintArray));

                    if (colCount++ == 3)
                    {
                        colCount = 0;
                        colStr += "\n";
                        outStr += colStr;
                    }
                    else
                        outStr += colStr;


                    uintStr = null;
                }
            }

            outStr = outStr.Remove(outStr.Length - 3, 1);

            unKey_txtBox.Text = outStr;
        }

        private void hex_cms_clear_Click(object sender, EventArgs e)
        {
            if (provider != null)
                clear_provider();
        }

        private void unkey_cms_copy_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(unKey_txtBox.Text))
                Clipboard.SetText(unKey_txtBox.Text);
        }

        private void hex_cms_copy_Click(object sender, EventArgs e)
        {
            string byteStr = null;
            string outStr = null;
            string curRowStr = null;
            int curCol = 0;

            for (int i = 0; i <= provider.Length; i++)
            {
                if (i == provider.Length)
                {
                    outStr = outStr.Remove(outStr.Length - 2, 1);
                    break;
                }

                byteStr = $"0x{provider.Bytes[i].ToString("x2")}";
                curRowStr += string.Format("{0},", byteStr);
                curCol++;

                if (curCol == 16)
                {
                    outStr += string.Format("{0}\n", curRowStr);
                    curRowStr = null;
                    curCol = 0;
                }
            }

            Clipboard.SetText(outStr);
        }
    }
}
