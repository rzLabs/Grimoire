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
using rdbCore;
using rdbCore.Structures;
using Grimoire.Utilities;

namespace Grimoire.GUI
{
    public partial class StructureEditor : Form
    {
    //    StructureInfo structure = null;
    //    Core core = new Core();

    //    public StructureEditor()
    //    {
    //        InitializeComponent();
    //    }

    //    public StructureEditor(string key)
    //    {
    //        InitializeComponent();

    //        structure = StructureManager.Find(key);

    //        populateColumns();
    //        setInfo();
    //    }

    //    private void setInfo()
    //    {
    //        try { core.Initialize(structure.Path); }
    //        catch (MoonSharp.Interpreter.SyntaxErrorException sEX) { LuaException.Print(sEX.DecoratedMessage, structure.Key); return; }           

    //        key.Text = structure.Key;
    //        path.Text = structure.Path;

    //        ext.Text = core.Extension;

    //        specialCase_list.Text = (core.SpecialCase) ? core.Case : string.Empty;

    //        ReadHeader = core.ReadHeader;

    //        fileName.Text = structure.FileName;
    //        tableName.Text = structure.TableName;

    //        List<LuaField> hFields = core.HeaderList;
    //        List<LuaField> dFields = core.FieldList;

    //        if (hFields != null)
    //        {
    //            foreach (LuaField hField in hFields)
    //            {
    //                int idx = hField_grid.Rows.Add();
    //                hField_grid.Rows[idx].Cells[0].Value = hField.Name;
    //                ((DataGridViewComboBoxCell)hField_grid.Rows[idx].Cells[1]).Value = new DataType()[hField.Type];
    //            }
    //        }
    //        foreach (LuaField dField in dFields)
    //        {
    //            int idx = dField_grid.Rows.Add();
    //            dField_grid.Rows[idx].Cells[0].Value = dField.Name;
    //            ((DataGridViewComboBoxCell)dField_grid.Rows[idx].Cells[1]).Value = new DataType()[dField.Type];
    //        }
    //    }

    //    private bool ReadHeader
    //    {
    //        get { return (readHead_on.Checked) ? true : false; }
    //        set
    //        {
    //            if (value) { readHead_on.Checked = true; }
    //            else { readHead_off.Checked = true; }
    //        }
    //    }

    //    private DataGridViewComboBoxColumn comboboxColumn(string name, string headerText, int width)
    //    {
    //        DataGridViewComboBoxColumn col = new DataGridViewComboBoxColumn
    //        {
    //            Name = name,
    //            HeaderText = headerText,
    //            Width = width
    //        };
    //        col.Items.AddRange(typeList);

    //        return col;
    //    }

    //    private string[] typeList
    //    {
    //        get
    //        {
    //            return new string[]
    //            {
    //                "BYTE",
    //                "BIT_VECTOR",
    //                "BIT_FROM_VECTOR",
    //                "INT16",
    //                "SHORT",
    //                "UINT16",
    //                "USHORT",
    //                "INT32",
    //                "INT",
    //                "UINT32",
    //                "UINT",
    //                "INT64",
    //                "LONG",
    //                "SINGLE",
    //                "FLOAT",
    //                "FLOAT32",
    //                "DOUBLE",
    //                "FLOAT64",
    //                "DECIMAL",
    //                "DATETIME",
    //                "SID",
    //                "STRING",
    //                "STRING_LEN",
    //                "STRING_BY_LEN",
    //                "STRING_BY_REF"
    //            };
    //        }
    //    }

    //    private void populateColumns()
    //    {
    //        hField_grid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "hName", HeaderText = "Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
    //        hField_grid.Columns.Add(comboboxColumn("hType", "Type", 120));
    //        hField_grid.Columns.Add(new DataGridViewButtonColumn() { Name = "hEdit", HeaderText = "Edit", Width = 60 });

    //        dField_grid.Columns.Add(new DataGridViewTextBoxColumn() { Name = "dName", HeaderText = "Name", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
    //        dField_grid.Columns.Add(comboboxColumn("dType", "Type", 120));
    //        dField_grid.Columns.Add(new DataGridViewButtonColumn() { Name = "dEdit", HeaderText = "Edit", Width = 60 });

    //    }

    //    private void editColumns_btn_Click(object sender, EventArgs e)
    //    {
    //        using (InputBox input = new InputBox("SQL Columns", true))
    //        {
    //            input.Value = (core.UseSqlColumns) ? core.SqlColumns : string.Empty;
    //            input.ShowDialog(this);
    //        }
    //    }

    //    private void editSelect_btn_Click(object sender, EventArgs e)
    //    {
    //        using (InputBox input = new InputBox("Select Statement", true))
    //        {
    //            input.Value = (core.UseSelectStatement) ? core.SelectStatement : string.Empty;
    //            input.ShowDialog(this);
    //        }
    //    }

    //    private async void verify_btn_Click(object sender, EventArgs e)
    //    {
    //        if (File.Exists(path.Text))
    //        {
    //            if (LUAUtils.VerifyScript(File.ReadAllText(path.Text))) { verifyLuaSyn_btn.BackColor = Color.Green; }
    //            else { verifyLuaSyn_btn.BackColor = Color.Red; }

    //            await Task.Run(() => { System.Threading.Thread.Sleep(1500); verifyLuaSyn_btn.BackColor = ButtonBase.DefaultBackColor; });
    //        }
    //    }
    }
}
