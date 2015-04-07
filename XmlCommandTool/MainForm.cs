using FastDBEngine;
using FastDBEngineGenerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using XmlCommandTool;
using XmlCommandTool.Properties;

public class MainForm : Form
{
    private bool bool_0;
    private ToolStripButton btnAddCommand;
    private ToolStripButton btnAddFile;
    private ToolStripButton btnDeleteCommnad;
    private ToolStripButton btnDeleteFile;
    private ToolStripButton btnEditCommand;
    private ToolStripButton btnFindCommand;
    private ToolStripButton btnHelp;
    private ToolStripButton btnOpenDirectory;
    private ToolStripButton btnSaveAll;
    
    private static Comparison<XmlCommand> comparison_0;
    private ContextMenuStrip contextMenuStrip1;
    private FileChangedDialogFix fileChangedDialog_0;
    private FileSystemWatcher fileSystemWatcher_0;
    
    private static Func<FileSystemEventArgs, bool> func_0;
    
    private static Func<FileSystemEventArgs, string> func_1;
    private IContainer icontainer_0 = null;
    private ImageList imageList_0;
    private static readonly int int_0 = 0;
    private static readonly int int_1 = 1;
    private static readonly int int_2 = 2;
    private static readonly int int_3 = 3;
    private ToolStripStatusLabel labCurrentPath;
    private ToolStripStatusLabel labMessage;
    private List<string> list_0 = new List<string>();
    private ToolStripMenuItem menuAdd;
    private ToolStripMenuItem menuCopyName;
    private ToolStripMenuItem menuCopyXml;
    private ToolStripMenuItem menuDelete;
    private ToolStripMenuItem menuEdit;
    private ToolStripMenuItem menuGenerateCallCode;
    private ToolStripMenuItem menuPaste;
    private Panel panel1;
    private Splitter splitter1;
    private Splitter splitter2;
    private StatusStrip statusStrip1;
    private string string_0;
    private static readonly string string_1 = "<ArrayOfXmlCommand>\r\n";
    private Timer timer_0;
    private ToolStrip toolStrip1;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripSeparator toolStripSeparator1;
    private ToolStripSeparator toolStripSeparator2;
    private ToolStripSeparator toolStripSeparator3;
    private ToolStripSeparator toolStripSeparator4;
    private ToolStripSeparator toolStripSeparator5;
    private TreeView treeView1;
    private SyntaxHighlighterControlFix txtSQL;
    private SyntaxHighlighterControlFix txtXML;

    public MainForm()
    {
        this.InitializeComponent();
        this.fileSystemWatcher_0.EnableRaisingEvents = false;
    }

    private void btnAddFile_Click(object sender, EventArgs e)
    {
        InputNameDialogFix dialog = new InputNameDialogFix();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            TreeNode node = new TreeNode(dialog.method_0(), int_0, int_1);
            this.treeView1.Nodes.Add(node);
            this.treeView1.SelectedNode = node;
            node.EnsureVisible();
            this.method_1();
        }
    }

    private void btnDeleteFile_Click(object sender, EventArgs e)
    {
        if (((this.treeView1.SelectedNode != null) && !this.treeView1.SelectedNode.HasTagValue()) && (MessageBox.Show("确定要删除当前文件吗？\r\n" + this.treeView1.SelectedNode.Text, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No))
        {
            this.list_0.Add(this.treeView1.SelectedNode.Text);
            this.treeView1.SetSelectNode();
            this.method_1();
        }
    }

    private void btnFindCommand_Click(object sender, EventArgs e)
    {
        InputNameDialogFix dialog = new InputNameDialogFix();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            string strB = dialog.method_0();
            foreach (TreeNode node2 in this.treeView1.Nodes)
            {
                IEnumerator enumerator2 = node2.Nodes.GetEnumerator();
                {
                    TreeNode current;
                    while (enumerator2.MoveNext())
                    {
                        current = (TreeNode) enumerator2.Current;
                        if (string.Compare(current.Text, strB, StringComparison.OrdinalIgnoreCase) == 0)
                        {
                            goto Label_0079;
                        }
                    }
                    continue;
                Label_0079:
                    this.treeView1.SelectedNode = current;
                    current.EnsureVisible();
                    return;
                }
            }
            MessageBox.Show("指定的命令名称不存在。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
    }

    private void btnHelp_Click(object sender, EventArgs e)
    {
        try
        {
            Process.Start("http://www.cnblogs.com/fish-li/archive/2012/07/17/ClownFish.html");
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }

    private void btnOpenDirectory_Click(object sender, EventArgs e)
    {
        if (!this.bool_0 || (MessageBox.Show("当前所做的修改还没有保存，如果继续，那么所做的修改将会丢失，确定还要继续吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No))
        {
            OpenDirectoryDialogFix dialog = new OpenDirectoryDialogFix();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.fileSystemWatcher_0.EnableRaisingEvents = false;
                string str = dialog.method_0();
                this.treeView1.BeginUpdate();
                this.treeView1.Nodes.Clear();
                try
                {
                    this.method_3(str);
                    this.labCurrentPath.Text = str;
                    RegistryHelper.SetValue("XmlCommandFilePath", str);
                    this.string_0 = str;
                    this.bool_0 = false;
                    this.list_0.Clear();
                    this.txtSQL.SetText(string.Empty);
                    this.txtXML.SetText(string.Empty);
                    this.method_0();
                    this.treeView1.Focus();
                    this.fileChangedDialog_0.method_2();
                    this.fileSystemWatcher_0.Path = str;
                    this.fileSystemWatcher_0.EnableRaisingEvents = true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
                finally
                {
                    this.treeView1.EndUpdate();
                }
            }
        }
    }

    private void btnSaveAll_Click(object sender, EventArgs e)
    {
        this.fileSystemWatcher_0.EnableRaisingEvents = false;
        this.fileChangedDialog_0.method_2();
        try
        {
            if (this.method_2(this.string_0, true))
            {
                this.bool_0 = false;
                this.list_0.Clear();
                this.method_0();
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        finally
        {
            this.fileSystemWatcher_0.EnableRaisingEvents = true;
        }
    }

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
        this.menuAdd.Enabled = this.btnAddCommand.Enabled;
        this.menuEdit.Enabled = this.btnEditCommand.Enabled;
        this.menuDelete.Enabled = this.btnDeleteCommnad.Enabled;
        this.menuGenerateCallCode.Enabled = this.btnEditCommand.Enabled;
        if (this.treeView1.SelectedNode.IsTagEmpty())
        {
            try
            {
                string text = Clipboard.GetText();
                this.menuPaste.Enabled = !string.IsNullOrEmpty(text) && text.StartsWith(string_1);
            }
            catch
            {
                this.menuPaste.Enabled = false;
            }
        }
        else
        {
            this.menuPaste.Enabled = false;
        }
    }

    private void fileSystemWatcher_0_Changed(object sender, FileSystemEventArgs e)
    {
        this.fileChangedDialog_0.method_0(e);
    }

    private void InitializeComponent()
    {
        this.icontainer_0 = new Container();
        ComponentResourceManager manager = new ComponentResourceManager(typeof(MainForm));
        this.toolStrip1 = new ToolStrip();
        this.btnOpenDirectory = new ToolStripButton();
        this.toolStripSeparator1 = new ToolStripSeparator();
        this.btnAddFile = new ToolStripButton();
        this.btnDeleteFile = new ToolStripButton();
        this.toolStripSeparator2 = new ToolStripSeparator();
        this.btnAddCommand = new ToolStripButton();
        this.btnEditCommand = new ToolStripButton();
        this.btnDeleteCommnad = new ToolStripButton();
        this.toolStripSeparator3 = new ToolStripSeparator();
        this.btnSaveAll = new ToolStripButton();
        this.toolStripSeparator4 = new ToolStripSeparator();
        this.btnFindCommand = new ToolStripButton();
        this.toolStripSeparator5 = new ToolStripSeparator();
        this.btnHelp = new ToolStripButton();
        this.statusStrip1 = new StatusStrip();
        this.labCurrentPath = new ToolStripStatusLabel();
        this.labMessage = new ToolStripStatusLabel();
        this.treeView1 = new TreeView();
        this.imageList_0 = new ImageList(this.icontainer_0);
        this.splitter1 = new Splitter();
        this.panel1 = new Panel();
        this.txtSQL = new SyntaxHighlighterControlFix();
        this.splitter2 = new Splitter();
        this.txtXML = new SyntaxHighlighterControlFix();
        this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
        this.menuAdd = new ToolStripMenuItem();
        this.menuEdit = new ToolStripMenuItem();
        this.menuDelete = new ToolStripMenuItem();
        this.menuPaste = new ToolStripMenuItem();
        this.toolStripMenuItem1 = new ToolStripSeparator();
        this.menuCopyName = new ToolStripMenuItem();
        this.menuCopyXml = new ToolStripMenuItem();
        this.toolStripMenuItem2 = new ToolStripSeparator();
        this.menuGenerateCallCode = new ToolStripMenuItem();
        this.fileSystemWatcher_0 = new FileSystemWatcher();
        this.timer_0 = new Timer(this.icontainer_0);
        this.toolStrip1.SuspendLayout();
        this.statusStrip1.SuspendLayout();
        this.panel1.SuspendLayout();
        this.contextMenuStrip1.SuspendLayout();
        this.fileSystemWatcher_0.BeginInit();
        base.SuspendLayout();
        this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.btnOpenDirectory, this.toolStripSeparator1, this.btnAddFile, this.btnDeleteFile, this.toolStripSeparator2, this.btnAddCommand, this.btnEditCommand, this.btnDeleteCommnad, this.toolStripSeparator3, this.btnSaveAll, this.toolStripSeparator4, this.btnFindCommand, this.toolStripSeparator5, this.btnHelp });
        this.toolStrip1.Location = new Point(0, 0);
        this.toolStrip1.Name = "toolStrip1";
        this.toolStrip1.Size = new Size(0x39b, 0x19);
        this.toolStrip1.TabIndex = 0;
        this.toolStrip1.Text = "toolStrip1";
       // this.btnOpenDirectory.Image = Resources.openfolderHS;
        this.btnOpenDirectory.ImageTransparentColor = Color.Magenta;
        this.btnOpenDirectory.Name = "btnOpenDirectory";
        this.btnOpenDirectory.Size = new Size(0x5c, 0x16);
        this.btnOpenDirectory.Text = "打开目录(&D)";
        this.btnOpenDirectory.Click += new EventHandler(this.btnOpenDirectory_Click);
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        this.toolStripSeparator1.Size = new Size(6, 0x19);
        //this.btnAddFile.Image = Resources.NewFolderHS;
        this.btnAddFile.ImageTransparentColor = Color.Magenta;
        this.btnAddFile.Name = "btnAddFile";
        this.btnAddFile.Size = new Size(0x5d, 0x16);
        this.btnAddFile.Text = "新增文件(&N)";
        this.btnAddFile.Click += new EventHandler(this.btnAddFile_Click);
        //this.btnDeleteFile.Image = Resources.DeleteFolderHS;
        this.btnDeleteFile.ImageTransparentColor = Color.Magenta;
        this.btnDeleteFile.Name = "btnDeleteFile";
        this.btnDeleteFile.Size = new Size(0x4b, 0x16);
        this.btnDeleteFile.Text = "删除文件";
        this.btnDeleteFile.Click += new EventHandler(this.btnDeleteFile_Click);
        this.toolStripSeparator2.Name = "toolStripSeparator2";
        this.toolStripSeparator2.Size = new Size(6, 0x19);
       // this.btnAddCommand.Image = Resources.NewDocumentHS;
        this.btnAddCommand.ImageTransparentColor = Color.Magenta;
        this.btnAddCommand.Name = "btnAddCommand";
        this.btnAddCommand.Size = new Size(0x5b, 0x16);
        this.btnAddCommand.Text = "新增命令(&C)";
        this.btnAddCommand.Click += new EventHandler(this.menuAdd_Click);
        //this.btnEditCommand.Image = (Image) manager.GetObject("btnEditCommand.Image");
        this.btnEditCommand.ImageTransparentColor = Color.Magenta;
        this.btnEditCommand.Name = "btnEditCommand";
        this.btnEditCommand.Size = new Size(90, 0x16);
        this.btnEditCommand.Text = "修改命令(&E)";
        this.btnEditCommand.Click += new EventHandler(this.menuEdit_Click);
      //  this.btnDeleteCommnad.Image = Resources.DeleteHS;
        this.btnDeleteCommnad.ImageTransparentColor = Color.Magenta;
        this.btnDeleteCommnad.Name = "btnDeleteCommnad";
        this.btnDeleteCommnad.Size = new Size(0x4b, 0x16);
        this.btnDeleteCommnad.Text = "删除命令";
        this.btnDeleteCommnad.Click += new EventHandler(this.menuDelete_Click);
        this.toolStripSeparator3.Name = "toolStripSeparator3";
        this.toolStripSeparator3.Size = new Size(6, 0x19);
      //  this.btnSaveAll.Image = Resources.SaveAllHS;
        this.btnSaveAll.ImageTransparentColor = Color.Magenta;
        this.btnSaveAll.Name = "btnSaveAll";
        this.btnSaveAll.Size = new Size(0x72, 0x16);
        this.btnSaveAll.Text = "保存所有修改(&S)";
        this.btnSaveAll.Click += new EventHandler(this.btnSaveAll_Click);
        this.toolStripSeparator4.Name = "toolStripSeparator4";
        this.toolStripSeparator4.Size = new Size(6, 0x19);
      //  this.btnFindCommand.Image = Resources.FindHS;
        this.btnFindCommand.ImageTransparentColor = Color.Magenta;
        this.btnFindCommand.Name = "btnFindCommand";
        this.btnFindCommand.Size = new Size(0x59, 0x16);
        this.btnFindCommand.Text = "查找命令(&F)";
        this.btnFindCommand.Click += new EventHandler(this.btnFindCommand_Click);
        this.toolStripSeparator5.Name = "toolStripSeparator5";
        this.toolStripSeparator5.Size = new Size(6, 0x19);
        this.btnHelp.DisplayStyle = ToolStripItemDisplayStyle.Image;
     //   this.btnHelp.Image = Resources.Help;
        this.btnHelp.ImageTransparentColor = Color.Magenta;
        this.btnHelp.Name = "btnHelp";
        this.btnHelp.Size = new Size(0x17, 0x16);
        this.btnHelp.Text = "帮助页面";
        this.btnHelp.ToolTipText = "查看帮助页面";
        this.btnHelp.Click += new EventHandler(this.btnHelp_Click);
        this.statusStrip1.Items.AddRange(new ToolStripItem[] { this.labCurrentPath, this.labMessage });
        this.statusStrip1.Location = new Point(0, 0x1dd);
        this.statusStrip1.Name = "statusStrip1";
        this.statusStrip1.Size = new Size(0x39b, 0x18);
        this.statusStrip1.TabIndex = 1;
        this.statusStrip1.Text = "statusStrip1";
        this.labCurrentPath.BorderSides = ToolStripStatusLabelBorderSides.All;
        this.labCurrentPath.BorderStyle = Border3DStyle.SunkenOuter;
        this.labCurrentPath.ForeColor = Color.Tomato;
        this.labCurrentPath.IsLink = true;
        this.labCurrentPath.LinkBehavior = LinkBehavior.NeverUnderline;
        this.labCurrentPath.LinkColor = Color.Tomato;
        this.labCurrentPath.Name = "labCurrentPath";
        this.labCurrentPath.Size = new Size(0x61, 0x13);
        this.labCurrentPath.Text = "labCurrentPath";
        this.labCurrentPath.TextAlign = ContentAlignment.MiddleLeft;
        this.labCurrentPath.Click += new EventHandler(this.labCurrentPath_Click);
        this.labMessage.Name = "labMessage";
        this.labMessage.Size = new Size(0x32b, 0x13);
        this.labMessage.Spring = true;
        this.labMessage.Text = "Ready.";
        this.labMessage.TextAlign = ContentAlignment.MiddleLeft;
        this.treeView1.Dock = DockStyle.Left;
        this.treeView1.ImageIndex = 0;
        this.treeView1.ImageList = this.imageList_0;
        this.treeView1.Location = new Point(0, 0x19);
        this.treeView1.Name = "treeView1";
        this.treeView1.SelectedImageIndex = 0;
        this.treeView1.Size = new Size(0xe3, 0x1c4);
        this.treeView1.TabIndex = 2;
        this.treeView1.NodeMouseDoubleClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
        this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
        this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
        this.treeView1.KeyDown += new KeyEventHandler(this.treeView1_KeyDown);
        this.imageList_0.ColorDepth = ColorDepth.Depth8Bit;
        this.imageList_0.ImageSize = new Size(0x10, 0x10);
        this.imageList_0.TransparentColor = Color.Transparent;
        this.splitter1.Location = new Point(0xe3, 0x19);
        this.splitter1.Name = "splitter1";
        this.splitter1.Size = new Size(7, 0x1c4);
        this.splitter1.TabIndex = 3;
        this.splitter1.TabStop = false;
        this.panel1.Controls.Add(this.txtSQL);
        this.panel1.Controls.Add(this.splitter2);
        this.panel1.Controls.Add(this.txtXML);
        this.panel1.Dock = DockStyle.Fill;
        this.panel1.Location = new Point(0xea, 0x19);
        this.panel1.Name = "panel1";
        this.panel1.Size = new Size(0x2b1, 0x1c4);
        this.panel1.TabIndex = 4;
        this.txtSQL.Dock = DockStyle.Fill;
        this.txtSQL.Location = new Point(0, 0);
        this.txtSQL.Name = "txtSQL";
        this.txtSQL.Size = new Size(0x2b1, 0xc0);
        this.txtSQL.TabIndex = 2;
        this.splitter2.Dock = DockStyle.Bottom;
        this.splitter2.Location = new Point(0, 0xc0);
        this.splitter2.Name = "splitter2";
        this.splitter2.Size = new Size(0x2b1, 7);
        this.splitter2.TabIndex = 1;
        this.splitter2.TabStop = false;
        this.txtXML.Dock = DockStyle.Bottom;
        this.txtXML.SetLanguage("xml");
        this.txtXML.Location = new Point(0, 0xc7);
        this.txtXML.Name = "txtXML";
        this.txtXML.Size = new Size(0x2b1, 0xfd);
        this.txtXML.TabIndex = 0;
        this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.menuAdd, this.menuEdit, this.menuDelete, this.menuPaste, this.toolStripMenuItem1, this.menuCopyName, this.menuCopyXml, this.toolStripMenuItem2, this.menuGenerateCallCode });
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new Size(0xb3, 170);
        this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
       // this.menuAdd.Image = Resources.NewDocumentHS;
        this.menuAdd.Name = "menuAdd";
        this.menuAdd.Size = new Size(0xb2, 0x16);
        this.menuAdd.Text = "新增命令";
        this.menuAdd.Click += new EventHandler(this.menuAdd_Click);
        //this.menuEdit.Image = Resources.EditTableHS;
        this.menuEdit.Name = "menuEdit";
        this.menuEdit.Size = new Size(0xb2, 0x16);
        this.menuEdit.Text = "修改命令";
        this.menuEdit.Click += new EventHandler(this.menuEdit_Click);
       // this.menuDelete.Image = Resources.DeleteHS;
        this.menuDelete.Name = "menuDelete";
        this.menuDelete.Size = new Size(0xb2, 0x16);
        this.menuDelete.Text = "删除命令";
        this.menuDelete.Click += new EventHandler(this.menuDelete_Click);
       // this.menuPaste.Image = Resources.PasteHS;
        this.menuPaste.Name = "menuPaste";
        this.menuPaste.Size = new Size(0xb2, 0x16);
        this.menuPaste.Text = "粘贴命令";
        this.menuPaste.Click += new EventHandler(this.menuPaste_Click);
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        this.toolStripMenuItem1.Size = new Size(0xaf, 6);
       // this.menuCopyName.Image = Resources.CopyHS;
        this.menuCopyName.Name = "menuCopyName";
        this.menuCopyName.Size = new Size(0xb2, 0x16);
        this.menuCopyName.Text = "复制名称   Ctrl-C";
        this.menuCopyName.Click += new EventHandler(this.menuCopyName_Click);
        this.menuCopyXml.Name = "menuCopyXml";
        this.menuCopyXml.Size = new Size(0xb2, 0x16);
        this.menuCopyXml.Text = "复制节点XML";
        this.menuCopyXml.Click += new EventHandler(this.menuCopyXml_Click);
        this.toolStripMenuItem2.Name = "toolStripMenuItem2";
        this.toolStripMenuItem2.Size = new Size(0xaf, 6);
       // this.menuGenerateCallCode.Image = Resources.Bitmap_0;
        this.menuGenerateCallCode.Name = "menuGenerateCallCode";
        this.menuGenerateCallCode.Size = new Size(0xb2, 0x16);
        this.menuGenerateCallCode.Text = "生成调用代码   F12";
        this.menuGenerateCallCode.Click += new EventHandler(this.menuGenerateCallCode_Click);
        this.fileSystemWatcher_0.EnableRaisingEvents = true;
        this.fileSystemWatcher_0.SynchronizingObject = this;
        this.fileSystemWatcher_0.Deleted += new FileSystemEventHandler(this.fileSystemWatcher_0_Changed);
        this.fileSystemWatcher_0.Created += new FileSystemEventHandler(this.fileSystemWatcher_0_Changed);
        this.fileSystemWatcher_0.Changed += new FileSystemEventHandler(this.fileSystemWatcher_0_Changed);
        this.timer_0.Enabled = true;
        this.timer_0.Interval = 500;
        this.timer_0.Tick += new EventHandler(this.timer_0_Tick);
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(0x39b, 0x1f5);
        base.Controls.Add(this.panel1);
        base.Controls.Add(this.splitter1);
        base.Controls.Add(this.treeView1);
        base.Controls.Add(this.statusStrip1);
        base.Controls.Add(this.toolStrip1);
        this.MinimumSize = new Size(700, 400);
        base.Name = "MainForm";
        this.Text = "FastDBEngine XmlCommandTool";
        base.WindowState = FormWindowState.Maximized;
        base.Load += new EventHandler(this.MainForm_Load);
        base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
        this.toolStrip1.ResumeLayout(false);
        this.toolStrip1.PerformLayout();
        this.statusStrip1.ResumeLayout(false);
        this.statusStrip1.PerformLayout();
        this.panel1.ResumeLayout(false);
        this.contextMenuStrip1.ResumeLayout(false);
        this.fileSystemWatcher_0.EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    private void labCurrentPath_Click(object sender, EventArgs e)
    {
        if (this.labCurrentPath.Text.Length != 0)
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo {
                    FileName = "explorer.exe",
                    Arguments = string.Format("\"{0}\"", this.labCurrentPath.Text)
                };
                Process.Start(startInfo);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        if ((this.btnSaveAll.Enabled && (e.CloseReason == CloseReason.UserClosing)) && (MessageBox.Show("当前所做的修改还没有保存，确定就直接退出程序吗？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No))
        {
            e.Cancel = true;
        }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
      //  base.Icon = Resources._039b;
        this.Text = this.Text + " V" + FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion;
        this.labCurrentPath.Text = string.Empty;
        this.method_0();
        //this.imageList_0.Images.Add(Resources.config);
        //this.imageList_0.Images.Add(Resources.config2);
        //this.imageList_0.Images.Add(Resources.EditCodeHS);
        //this.imageList_0.Images.Add(Resources.EditCodeHS2);
        this.fileSystemWatcher_0.Filter = "*.config";
        this.fileSystemWatcher_0.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
        this.fileChangedDialog_0 = new FileChangedDialogFix();
        this.fileChangedDialog_0.Owner = this;
    }

    private void menuAdd_Click(object sender, EventArgs e)
    {
        TreeNode selectedNode = this.treeView1.SelectedNode;
        if (selectedNode != null)
        {
            TreeNode node3 = selectedNode.IsTagEmpty() ? selectedNode : selectedNode.Parent;
            EditCommandDialogFix dialog = new EditCommandDialogFix();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                XmlCommand command = dialog.method_4();
                TreeNode node2 = new TreeNode(command.CommandName, int_2, int_3);
                node2.SetTagValue(command);
                node3.Nodes.Add(node2);
                this.treeView1.SelectedNode = node2;
                node2.EnsureVisible();
                this.method_1();
            }
        }
    }

    private void menuCopyName_Click(object sender, EventArgs e)
    {
        if (this.treeView1.SelectedNode != null)
        {
            Clipboard.SetText(this.treeView1.SelectedNode.Text);
        }
    }

    private void menuCopyXml_Click(object sender, EventArgs e)
    {
        if (this.treeView1.SelectedNode != null)
        {
            if (this.treeView1.SelectedNode.IsTagEmpty())
            {
                Clipboard.SetText(XmlHelper.XmlSerialize(this.treeView1.SelectedNode.GetTagList(), Encoding.UTF8));
            }
            else
            {
                Clipboard.SetText(XmlHelper.XmlSerializerObject(this.treeView1.SelectedNode.GetTagValue()));
            }
        }
    }

    private void menuDelete_Click(object sender, EventArgs e)
    {
        if (((this.treeView1.SelectedNode != null) && !this.treeView1.SelectedNode.IsTagEmpty()) && (MessageBox.Show("确定要删除当前命令节点吗？\r\n" + this.treeView1.SelectedNode.Text, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.No))
        {
            this.treeView1.SetSelectNode();
            this.method_1();
        }
    }

    private void menuEdit_Click(object sender, EventArgs e)
    {
        if (this.treeView1.SelectedNode != null)
        {
            XmlCommand command = this.treeView1.SelectedNode.GetTagValue();
            if (command != null)
            {
                EditCommandDialogFix dialog = new EditCommandDialogFix();
                dialog.method_5(command);
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    command = dialog.method_4();
                    this.treeView1.SelectedNode.SetTagValue(command);
                    this.treeView1.SelectedNode.Text = command.CommandName;
                    this.bool_0 = true;
                    this.treeView1_AfterSelect(null, null);
                }
            }
        }
    }

    private void menuGenerateCallCode_Click(object sender, EventArgs e)
    {
        if (((this.treeView1.SelectedNode != null) && !this.treeView1.SelectedNode.IsTagEmpty()) && this.btnEditCommand.Enabled)
        {
            new ShowCallCodeDialogFix(this.treeView1.SelectedNode.GetTagValue(), this.treeView1.SelectedNode.Text) { Owner = this }.Show();
        }
    }

    private void menuPaste_Click(object sender, EventArgs e)
    {
        if ((this.treeView1.SelectedNode != null) && !this.treeView1.SelectedNode.HasTagValue())
        {
            try
            {
                string text = Clipboard.GetText();
                if (!string.IsNullOrEmpty(text))
                {
                    TreeNode selectedNode = this.treeView1.SelectedNode;
                    foreach (XmlCommand command in XmlHelper.XmlDeserialize<List<XmlCommand>>(text, Encoding.UTF8))
                    {
                        TreeNode node = new TreeNode(command.CommandName, int_2, int_3);
                        node.SetTagValue(command);
                        selectedNode.Nodes.Add(node);
                        this.method_1();
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }

    private void method_0()
    {
        TreeNode selectedNode = this.treeView1.SelectedNode;
        this.btnAddFile.Enabled = !string.IsNullOrEmpty(this.string_0);
        this.btnDeleteFile.Enabled = selectedNode != null;
        this.btnAddCommand.Enabled = selectedNode != null;
        this.btnEditCommand.Enabled = (selectedNode != null) && selectedNode.HasTagValue();
        this.btnDeleteCommnad.Enabled = this.btnEditCommand.Enabled;
        this.btnFindCommand.Enabled = this.treeView1.Nodes.Count > 0;
        this.labCurrentPath.Visible = !string.IsNullOrEmpty(this.string_0);
        this.btnSaveAll.Enabled = this.bool_0 && this.labCurrentPath.Visible;
    }

    private void method_1()
    {
        this.treeView1.Focus();
        this.bool_0 = true;
        this.method_0();
    }

    private bool method_2(string string_2, bool bool_1)
    {
        if (bool_1)
        {
            Dictionary<string, int> dictionary = new Dictionary<string, int>();
            string key = null;
            try
            {
                foreach (TreeNode node2 in this.treeView1.Nodes)
                {
                    foreach (TreeNode node in node2.Nodes)
                    {
                        key = node.Text;
                        dictionary.Add(key, 1);
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show(string.Format("命令名称 [{0}] 有重复，请修改后再执行保存操作。", key), this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return false;
            }
        }
        foreach (string str2 in this.list_0)
        {
            string path = Path.Combine(string_2, str2 + ".config");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
        foreach (TreeNode node2 in this.treeView1.Nodes)
        {
            string str4 = Path.Combine(string_2, node2.Text + ".config");
            List<XmlCommand> list = node2.GetTagList();
            if (File.Exists(str4))
            {
                string contents = XmlHelper.XmlSerialize(list, Encoding.UTF8);
                string str6 = File.ReadAllText(str4, Encoding.UTF8);
                if (contents != str6)
                {
                    File.WriteAllText(str4, contents, Encoding.UTF8);
                }
            }
            else
            {
                XmlHelper.XmlSerializeToFile(list, str4, Encoding.UTF8);
            }
        }
        return true;
    }

    private void method_3(string string_2)
    {
        string[] strArray = Directory.GetFiles(string_2, "*.config", SearchOption.TopDirectoryOnly);
        this.method_4(strArray, false);
    }

    private void method_4(string[] string_2, bool bool_1)
    {
        foreach (string str in string_2)
        {
            List<XmlCommand> list = null;
            try
            {
                list = XmlHelper.XmlDeserializeFromFile<List<XmlCommand>>(str, Encoding.UTF8);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException(string.Format("在读取文件 {0} 时失败，错误原因：\r\n{1}", str, exception.Message), exception);
            }
            if (comparison_0 == null)
            {
                comparison_0 = new Comparison<XmlCommand>(MainForm.smethod_0);
            }
            list.Sort(comparison_0);
            TreeNode node = null;
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(str);
            if (bool_1)
            {
                node = this.treeView1.Nodes.CompareText(fileNameWithoutExtension);
            }
            if (node == null)
            {
                node = new TreeNode(fileNameWithoutExtension, int_0, int_1);
                this.treeView1.Nodes.Add(node);
            }
            else
            {
                node.Text = fileNameWithoutExtension;
                node.Nodes.Clear();
            }
            foreach (XmlCommand command in list)
            {
                TreeNode node2 = new TreeNode(command.CommandName, int_2, int_3);
                node2.SetTagValue(command);
                node.Nodes.Add(node2);
            }
        }
    }

    internal void method_5(string string_2, List<FileSystemEventArgs> list_1)
    {
        Exception exception;
        if (!string.IsNullOrEmpty(string_2))
        {
            if (string_2.smethod_3(this.string_0))
            {
                MessageBox.Show("临时输出目录不能是当前目录。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                if (!this.method_2(string_2, false))
                {
                    return;
                }
            }
            catch (Exception exception1)
            {
                exception = exception1;
                MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
        }
        string text = null;
        string str2 = null;
        if (this.treeView1.SelectedNode != null)
        {
            if (this.treeView1.SelectedNode.IsTagEmpty())
            {
                text = this.treeView1.SelectedNode.Text;
            }
            else
            {
                str2 = this.treeView1.SelectedNode.Text;
                text = this.treeView1.SelectedNode.Parent.Text;
            }
        }
        foreach (FileSystemEventArgs args in list_1)
        {
            if (args.ChangeType == WatcherChangeTypes.Deleted)
            {
                this.treeView1.Nodes.RemoveNode(Path.GetFileNameWithoutExtension(args.Name));
            }
        }
        try
        {
            if (func_0 == null)
            {
                func_0 = new Func<FileSystemEventArgs, bool>(MainForm.smethod_1);
            }
            if (func_1 == null)
            {
                func_1 = new Func<FileSystemEventArgs, string>(MainForm.smethod_2);
            }
            string[] strArray = Enumerable.Select<FileSystemEventArgs, string>(Enumerable.Where<FileSystemEventArgs>(list_1, func_0), func_1).ToArray<string>();
            this.method_4(strArray, true);
        }
        catch (Exception exception2)
        {
            exception = exception2;
            MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        if (text == null)
        {
            goto Label_0286;
        }
        IEnumerator enumerator = this.treeView1.Nodes.GetEnumerator();
        {
            TreeNode current;
            while (enumerator.MoveNext())
            {
                current = (TreeNode) enumerator.Current;
                if (current.Text.CompareIgnoreCase(text))
                {
                    goto Label_01CF;
                }
            }
            goto Label_025E;
        Label_01CF:
            this.treeView1.SelectedNode = current;
            if (str2 != null)
            {
                IEnumerator enumerator3 = current.Nodes.GetEnumerator();
                {
                    TreeNode node2;
                    while (enumerator3.MoveNext())
                    {
                        node2 = (TreeNode) enumerator3.Current;
                        if (node2.Text.CompareIgnoreCase(str2))
                        {
                            goto Label_021E;
                        }
                    }
                    goto Label_025E;
                Label_021E:
                    this.treeView1.SelectedNode = node2;
                }
            }
        }
    Label_025E:
        if (this.treeView1.SelectedNode != null)
        {
            this.treeView1.SelectedNode.EnsureVisible();
        }
        this.treeView1_AfterSelect(null, null);
    Label_0286:
        this.method_6();
    }

    internal void method_6()
    {
        this.fileChangedDialog_0.method_2();
        this.fileChangedDialog_0.Hide();
    }

    
    private static int smethod_0(XmlCommand xmlCommand_0, XmlCommand xmlCommand_1)
    {
        return string.Compare(xmlCommand_0.CommandName, xmlCommand_1.CommandName, StringComparison.OrdinalIgnoreCase);
    }

    
    private static bool smethod_1(FileSystemEventArgs fileSystemEventArgs_0)
    {
        return (fileSystemEventArgs_0.ChangeType != WatcherChangeTypes.Deleted);
    }

    
    private static string smethod_2(FileSystemEventArgs fileSystemEventArgs_0)
    {
        return fileSystemEventArgs_0.FullPath;
    }

    void Dispose(bool disposing)
    {
        if (disposing && (this.icontainer_0 != null))
        {
            this.icontainer_0.Dispose();
        }
        base.Dispose(disposing);
    }

    private void timer_0_Tick(object sender, EventArgs e)
    {
        if (this.fileSystemWatcher_0.EnableRaisingEvents && ((((this.fileChangedDialog_0 != null) && !this.fileChangedDialog_0.Visible) && this.fileChangedDialog_0.method_1()) && (Form.ActiveForm == this)))
        {
            this.fileChangedDialog_0.ShowDialog();
        }
    }

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (this.treeView1.SelectedNode == null)
        {
            this.labMessage.Text = "Ready.";
        }
        else
        {
            this.labMessage.Text = this.treeView1.SelectedNode.FullPath;
            XmlCommand command = this.treeView1.SelectedNode.GetTagValue();
            if (command == null)
            {
                this.txtSQL.SetText(string.Format("\r\n共 {0} 个命令子节点。", this.treeView1.SelectedNode.Nodes.Count));
                this.txtXML.SetText(string.Empty);
            }
            else
            {
                string commandText = (string) command.CommandText;
                command.CommandText = "....................";
                this.txtXML.SetText(XmlHelper.XmlSerializerObject(command));
                this.txtSQL.SetText(commandText);
                command.CommandText = commandText;
            }
            this.method_0();
        }
    }

    private void treeView1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Delete)
        {
            if (this.treeView1.SelectedNode != null)
            {
                if (this.treeView1.SelectedNode.IsTagEmpty())
                {
                    this.btnDeleteFile_Click(sender, null);
                }
                else
                {
                    this.menuDelete_Click(sender, null);
                }
            }
        }
        else if (e.KeyCode == Keys.F12)
        {
            if (this.btnEditCommand.Enabled)
            {
                this.menuGenerateCallCode_Click(null, null);
            }
        }
        else if (e.Control && (e.KeyCode == Keys.C))
        {
            this.menuCopyName_Click(null, null);
        }
    }

    private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            if (e.Node != this.treeView1.SelectedNode)
            {
                this.treeView1.SelectedNode = e.Node;
            }
            this.contextMenuStrip1.Show(this.treeView1, e.Location);
        }
    }

    private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if ((this.treeView1.SelectedNode != null) && !this.treeView1.SelectedNode.IsTagEmpty())
        {
            this.menuGenerateCallCode_Click(null, null);
        }
    }
}

