using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using XmlCommandTool.Properties;

public class FileChangedDialog : Form
{
    private Button btnNO;
    private Button btnOpen;
    private Button btnYES;
    private ColumnHeader columnHeader_0;
    private ColumnHeader columnHeader_1;
    private IContainer icontainer_0 = null;
    private ImageList imageList_0;
    private Label label1;
    private Label label2;
    private ListView listView1;
    private TextBox textBox1;

    public FileChangedDialog()
    {
        this.InitializeComponent();
    }

    private void InitializeComponent()
    {
        this.icontainer_0 = new Container();
        this.label1 = new Label();
        this.listView1 = new ListView();
        this.columnHeader_0 = new ColumnHeader();
        this.columnHeader_1 = new ColumnHeader();
        this.imageList_0 = new ImageList(this.icontainer_0);
        this.btnNO = new Button();
        this.btnYES = new Button();
        this.label2 = new Label();
        this.textBox1 = new TextBox();
        this.btnOpen = new Button();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new Point(13, 13);
        this.label1.Name = "label1";
        this.label1.Size = new Size(0x1a3, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "XmlCommandTool 检测到以下文件被其它应用程序更新了，是否重新加载它们？";
        this.listView1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
        this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
        this.listView1.FullRowSelect = true;
        this.listView1.Location = new Point(13, 0x26);
        this.listView1.Name = "listView1";
        this.listView1.ShowItemToolTips = true;
        this.listView1.Size = new Size(0x25d, 0xc0);
        this.listView1.SmallImageList = this.imageList_0;
        this.listView1.TabIndex = 1;
        this.listView1.UseCompatibleStateImageBehavior = false;
        this.listView1.View = View.Details;
        this.columnHeader_0.Text = "文件名";
        this.columnHeader_0.Width = 0x18a;
        this.columnHeader_1.Text = "修改类别";
        this.columnHeader_1.Width = 140;
        this.imageList_0.ColorDepth = ColorDepth.Depth8Bit;
        this.imageList_0.ImageSize = new Size(0x10, 0x10);
        this.imageList_0.TransparentColor = Color.Transparent;
        this.btnNO.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
        this.btnNO.Location = new Point(0x1ee, 310);
        this.btnNO.Name = "btnNO";
        this.btnNO.Size = new Size(0x7e, 0x17);
        this.btnNO.TabIndex = 6;
        this.btnNO.Text = "不，忽略所有修改";
        this.btnNO.UseVisualStyleBackColor = true;
        this.btnNO.Click += new EventHandler(this.btnNO_Click);
        this.btnYES.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
        this.btnYES.Location = new Point(0x13c, 310);
        this.btnYES.Name = "btnYES";
        this.btnYES.Size = new Size(0xa1, 0x17);
        this.btnYES.TabIndex = 5;
        this.btnYES.Text = "是的，重新加载所有文件";
        this.btnYES.UseVisualStyleBackColor = true;
        this.btnYES.Click += new EventHandler(this.btnYES_Click);
        this.label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
        this.label2.AutoSize = true;
        this.label2.Location = new Point(13, 0xf9);
        this.label2.Name = "label2";
        this.label2.Size = new Size(0x209, 12);
        this.label2.TabIndex = 7;
        this.label2.Text = "为了防止文件更新冲突，您可以在下面的文本框内指定一个目录将当前所有节点的内容保存起来。";
        this.textBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
        this.textBox1.Location = new Point(13, 0x10c);
        this.textBox1.Name = "textBox1";
        this.textBox1.Size = new Size(0x239, 0x15);
        this.textBox1.TabIndex = 8;
        this.btnOpen.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
        this.btnOpen.FlatStyle = FlatStyle.Popup;
        // this.btnOpen.Image = Resources.openfolderHS;
        this.btnOpen.ImageAlign = ContentAlignment.BottomRight;
        this.btnOpen.Location = new Point(0x24f, 0x10a);
        this.btnOpen.Name = "btnOpen";
        this.btnOpen.Size = new Size(0x1b, 0x17);
        this.btnOpen.TabIndex = 9;
        this.btnOpen.UseVisualStyleBackColor = true;
        this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(0x278, 0x159);
        base.Controls.Add(this.btnOpen);
        base.Controls.Add(this.textBox1);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.btnNO);
        base.Controls.Add(this.btnYES);
        base.Controls.Add(this.listView1);
        base.Controls.Add(this.label1);
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "FileChangedDialog";
        base.ShowIcon = false;
        base.ShowInTaskbar = false;
        base.StartPosition = FormStartPosition.CenterParent;
        this.Text = "文件修改通知";
        base.Load += new EventHandler(this.FileChangedDialog_Load);
        base.FormClosing += new FormClosingEventHandler(this.FileChangedDialog_FormClosing);
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    private void btnNO_Click(object sender, EventArgs e)
    {
        (base.Owner as MainForm).method_6();
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
        OpenDirectoryDialog dialog = new OpenDirectoryDialog();
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
        ListViewItem item = new ListViewItem(fileSystemEventArgs_0.Name, 0) {
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

    void Dispose(bool disposing)
    {
        if (disposing && (this.icontainer_0 != null))
        {
            this.icontainer_0.Dispose();
        }
        base.Dispose(disposing);
    }
}

