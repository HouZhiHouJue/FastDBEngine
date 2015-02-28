using FastDBEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XmlCommandTool
{
    public partial class ParameterDialogFix : Form
    {
        public ParameterDialogFix()
    {
        this.InitializeComponent();
        this.method_0();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        string str = this.txtName.Text.Trim();
        if ((str.Length == 0) || ((str.Length == 1) && !char.IsLetter(str[0])))
        {
            MessageBox.Show("参数Name不能为空。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.txtName.Focus();
        }
        else if (this.cboDbType.SelectedIndex < 0)
        {
            MessageBox.Show("DbType必须要选择。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.cboDbType.Focus();
        }
        else
        {
            base.DialogResult = DialogResult.OK;
        }
    }

    private void cboDbType_DrawItem(object sender, DrawItemEventArgs e)
    {
        e.DrawBackground();
        e.DrawFocusRectangle();
        if (e.Index >= 0)
        {
            string s = this.cboDbType.Items[e.Index].ToString();
            if ((((s == "DateTime") || (s == "Currency")) || ((s == "Int32") || (s == "String"))) || (s == "AnsiStringFixedLength"))
            {
                e.Graphics.DrawString(s, e.Font, Brushes.Red, e.Bounds);
            }
            else
            {
                e.Graphics.DrawString(s, e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            }
        }
    }

   

    private void method_0()
    {
        string[] names = Enum.GetNames(typeof(DbType));
        Array.Sort<string>(names, StringComparer.OrdinalIgnoreCase);
        foreach (string str2 in names)
        {
            this.cboDbType.Items.Add(str2);
        }
        foreach (string str in Enum.GetNames(typeof(ParameterDirection)))
        {
            this.cboDirection.Items.Add(str);
        }
        this.cboDirection.SelectedIndex = 0;
        base.Location = new Point((SystemInformation.WorkingArea.Width - base.Width) / 2, 50);
    }

    public XmlCmdParameter method_1()
    {
        return new XmlCmdParameter { Name = this.txtName.Text.Trim(), Type = (DbType) Enum.Parse(typeof(DbType), this.cboDbType.Text), Direction = (ParameterDirection) Enum.Parse(typeof(ParameterDirection), this.cboDirection.Text), Size = Convert.ToInt32(this.nudSize.Value) };
    }

    public void method_2(XmlCmdParameter xmlCmdParameter_0)
    {
        try
        {
            this.txtName.Text = xmlCmdParameter_0.Name;
            this.cboDbType.Text = xmlCmdParameter_0.Type.ToString();
            this.cboDirection.Text = xmlCmdParameter_0.Direction.ToString();
            this.nudSize.Value = xmlCmdParameter_0.Size;
        }
        catch
        {
        }
    }

    private void ParameterDialog_Load(object sender, EventArgs e)
    {
        if (this.txtName.Text.Trim().Length == 0)
        {
            this.txtName.Text = ":";
            this.txtName.SelectionStart = 1;
            this.txtName.Focus();
        }
    }
    }
}
