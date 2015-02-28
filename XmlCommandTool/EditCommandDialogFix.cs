using FastDBEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XmlCommandTool.Properties;

namespace XmlCommandTool
{
    public partial class EditCommandDialogFix : Form
    {

        public EditCommandDialogFix()
        {
            this.InitializeComponent();
            foreach (string str in Enum.GetNames(typeof(CommandType)))
            {
                this.cboCommandType.Items.Add(str);
            }
            this.cboCommandType.SelectedIndex = 0;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ParameterDialogFix dialog = new ParameterDialogFix();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.listView1.Items.Add(this.method_0(dialog.method_1()));
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                int index = this.listView1.SelectedIndices[0];
                int num2 = (index > 0) ? index : 0;
                this.listView1.Items.RemoveAt(index);
                if ((this.listView1.Items.Count > 0) && (num2 <= (this.listView1.Items.Count - 1)))
                {
                    this.listView1.Items[num2].Selected = true;
                }
                this.listView1.Focus();
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (this.listView1.SelectedItems.Count != 0)
            {
                ListViewItem item = this.listView1.SelectedItems[0];
                ParameterDialogFix dialog = new ParameterDialogFix();
                dialog.method_2((XmlCmdParameter)item.Tag);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    this.method_1(item, dialog.method_1());
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                string text = Clipboard.GetText();
                if (!string.IsNullOrEmpty(text) && (text.IndexOf(string_0) >= 0))
                {
                    XmlCommand command = XmlHelper.XmlDeserialize<XmlCommand>(text, Encoding.UTF8);
                    if (command != null)
                    {
                        this.method_5(command);
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
            finally
            {
                this.btnImport.Visible = false;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtName.Text.Trim().Length == 0)
            {
                MessageBox.Show("命令名称不能为空。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtName.Focus();
            }
            else if (this.txtCode.GetText().Length == 0)
            {
                MessageBox.Show("命令代码不能为空。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtCode.Focus();
            }
            else
            {
                base.DialogResult = DialogResult.OK;
            }
        }

        private void EditCommandDialog_Load(object sender, EventArgs e)
        {
           // this.imageList_0.Images.Add(Resources.Bitmap_1);
            if (this.txtName.Text.Length > 0)
            {
                this.Text = "编辑命令";
            }
            try
            {
                this.btnImport.Visible = Clipboard.GetText().IndexOf(string_0) >= 0;
            }
            catch
            {
            }
        }

        private void EditCommandDialog_Shown(object sender, EventArgs e)
        {
            if (this.txtName.Text.Length > 0)
            {
                this.txtCode.Focus();
            }
        }



        private void listView1_Resize(object sender, EventArgs e)
        {
            if (this.listView1.Width > (((((this.columnHeader_1.Width + this.columnHeader_2.Width) + this.columnHeader_3.Width) + SystemInformation.VerticalScrollBarWidth) + SystemInformation.BorderSize.Width) + 10))
            {
                try
                {
                    this.columnHeader_0.Width = (((((this.listView1.Width - (SystemInformation.BorderSize.Width * 2)) - this.columnHeader_1.Width) - this.columnHeader_2.Width) - this.columnHeader_3.Width) - SystemInformation.VerticalScrollBarWidth) - 5;
                }
                catch
                {
                }
            }
        }

        private ListViewItem method_0(XmlCmdParameter xmlCmdParameter_0)
        {
            ListViewItem item = new ListViewItem(xmlCmdParameter_0.Name, 0);
            item.SubItems.Add(xmlCmdParameter_0.Type.ToString());
            item.SubItems.Add(xmlCmdParameter_0.Direction.ToString());
            item.SubItems.Add(xmlCmdParameter_0.Size.ToString());
            item.Tag = xmlCmdParameter_0;
            return item;
        }

        private void method_1(ListViewItem listViewItem_0, XmlCmdParameter xmlCmdParameter_0)
        {
            listViewItem_0.Text = xmlCmdParameter_0.Name;
            listViewItem_0.SubItems[1].Text = xmlCmdParameter_0.Type.ToString();
            listViewItem_0.SubItems[2].Text = xmlCmdParameter_0.Direction.ToString();
            listViewItem_0.SubItems[3].Text = xmlCmdParameter_0.Size.ToString();
            listViewItem_0.Tag = xmlCmdParameter_0;
        }

        private List<XmlCmdParameter> method_2()
        {
            List<XmlCmdParameter> list = new List<XmlCmdParameter>(this.listView1.Items.Count);
            for (int i = 0; i < this.listView1.Items.Count; i++)
            {
                list.Add((XmlCmdParameter)this.listView1.Items[i].Tag);
            }
            return list;
        }

        private void method_3(List<XmlCmdParameter> list_0)
        {
            if (list_0 != null)
            {
                this.listView1.Items.Clear();
                foreach (XmlCmdParameter parameter in list_0)
                {
                    this.listView1.Items.Add(this.method_0(parameter));
                }
            }
        }

        public XmlCommand method_4()
        {
            return new XmlCommand { CommandName = this.txtName.Text.Trim(), CommandText = this.txtCode.GetText(), Database = (this.txtDataBase.Text.Trim().Length == 0) ? null : this.txtDataBase.Text.Trim(), Parameters = this.method_2(), CommandType = (CommandType)Enum.Parse(typeof(CommandType), this.cboCommandType.Text), Timeout = (int)this.nudTimeout.Value };
        }

        public void method_5(XmlCommand xmlCommand_0)
        {
            if (xmlCommand_0 != null)
            {
                this.txtName.Text = xmlCommand_0.CommandName;
                this.txtCode.SetText((string)xmlCommand_0.CommandText);
                this.txtDataBase.Text = xmlCommand_0.Database;
                this.method_3(xmlCommand_0.Parameters);
                this.nudTimeout.Value = xmlCommand_0.Timeout;
                this.cboCommandType.Text = xmlCommand_0.CommandType.ToString();
            }
        }
    }
}
