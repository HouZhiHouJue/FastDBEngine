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
    public partial class FileChangedDialogFix : Form
    {
        public FileChangedDialogFix()
        {
            InitializeComponent();
        }

        private void btnNO_Click(object sender, EventArgs e)
        {
            (base.Owner as MainForm).method_6();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenDirectoryDialogFix dialog = new OpenDirectoryDialogFix();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.textBox1.Text = dialog.method_0();
            }
        }

        private void btnYES_Click(object sender, EventArgs e)
        {
            List<FileSystemEventArgs> list = new List<FileSystemEventArgs>(this.listView1.Items.Count);
            foreach (ListViewItem item in this.listView1.Items)
            {
                list.Add(item.Tag as FileSystemEventArgs);
            }
            (base.Owner as MainForm).method_5(this.textBox1.Text, list);
        }

        private void FileChangedDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.btnNO_Click(null, null);
            }
        }

        private void FileChangedDialog_Load(object sender, EventArgs e)
        {
            //  this.imageList_0.Images.Add(Resources.config);
        }



        public void method_0(FileSystemEventArgs fileSystemEventArgs_0)
        {
            foreach (ListViewItem item2 in this.listView1.Items)
            {
                if (item2.ToolTipText.CompareIgnoreCase(fileSystemEventArgs_0.FullPath))
                {
                    return;
                }
            }
            ListViewItem item = new ListViewItem(fileSystemEventArgs_0.Name, 0)
            {
                ToolTipText = fileSystemEventArgs_0.FullPath
            };
            item.SubItems.Add(fileSystemEventArgs_0.ChangeType.ToString());
            item.Tag = fileSystemEventArgs_0;
            this.listView1.Items.Add(item);
        }

        public bool method_1()
        {
            return (this.listView1.Items.Count > 0);
        }

        public void method_2()
        {
            this.listView1.Items.Clear();
        }
    }
}
