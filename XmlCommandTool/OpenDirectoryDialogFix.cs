using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XmlCommandTool
{
    public partial class OpenDirectoryDialogFix : Form
    {
        public OpenDirectoryDialogFix()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtPath.Text.Trim().Length == 0)
            {
                MessageBox.Show(this.label1.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtPath.Focus();
            }
            else
            {
                try
                {
                    if (!Directory.Exists(this.txtPath.Text))
                    {
                        MessageBox.Show("指定的目录不存在。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.txtPath.Focus();
                        return;
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    return;
                }
                base.DialogResult = DialogResult.OK;
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                Description = "请选择数据访问配置文件所在路径",
                ShowNewFolderButton = false
            };
            if (this.txtPath.Text.Length > 0)
            {
                dialog.SelectedPath = this.txtPath.Text;
            }
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = dialog.SelectedPath;
            }
        }



        public string method_0()
        {
            return this.txtPath.Text.Trim();
        }

        private void OpenDirectoryDialog_Shown(object sender, EventArgs e)
        {
            string path = RegistryHelper.GetValue("XmlCommandFilePath", string.Empty).ToString();
            try
            {
                if (Directory.Exists(path))
                {
                    this.txtPath.Text = path;
                }
                else
                {
                    string text = Clipboard.GetText();
                    if (!string.IsNullOrEmpty(text) && Directory.Exists(text))
                    {
                        this.txtPath.Text = text;
                    }
                }
            }
            catch
            {
            }
        }
    }
}
