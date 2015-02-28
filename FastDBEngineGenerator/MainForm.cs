using FastDBEngine;
using FastDBEngineGenerator;
using FastDBEngineGenerator.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

public class MainForm : Form
{
    private Button btnConnect;
    private ComboBox cboConnectionString;
    private ContextMenuStrip contextMenuStrip1;
    private ContextMenuStrip contextMenuStrip2;
    private ContextMenuStrip contextMenuStrip3;
    private ContextMenuStrip contextMenuStrip4;
    private ContextMenuStrip contextMenuStrip5;
    private IContainer icontainer_0 = null;
    private ImageList imageList_0;
    private static readonly int int_0 = 0;
    private static readonly int int_1 = 1;
    private static readonly int int_2 = 2;
    private static readonly int int_3 = 3;
    private static readonly int int_4 = 4;
    private static readonly int int_5 = 5;
    private static readonly int int_6 = 6;
    private Label label1;
    private Label label2;
    private LinkLabel linkLabel1;
    private ToolStripMenuItem menuCopyDbName;
    private ToolStripMenuItem menuCopySpName;
    private ToolStripMenuItem menuCopyTableName;
    private ToolStripMenuItem menuCopyViewName;
    private ToolStripMenuItem menuGetXmlCommandBySP;
    private Panel panel1;
    private Panel panel2;
    private Panel panel3;
    private Panel panel4;
    private Panel panel5;
    private Splitter splitter1;
    private Splitter splitter2;
    private static readonly string string_0 = "[None]";
    private static readonly string string_1 = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "FastDBEngineGenerator.txt");
    private string conn;
    private static readonly string string_3 = "DataBase";
    private static readonly string string_4 = "Tables";
    private static readonly string string_5 = "Views";
    private static readonly string string_6 = "Procedures";
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripSeparator toolStripMenuItem3;
    private TreeNode treeNode_0;
    private TreeView treeView1;
    private SyntaxHighlighterControlFix txtCsCode;
    private SyntaxHighlighterControlFix txtSqlScript;
    private UcCsClassStyleFix ucCsClassStyle1;
    private ucParameterStyleFix ucParameterStyle1;
    private ToolStripMenuItem 定位到指定对象ToolStripMenuItem;
    private ToolStripMenuItem 根据查询生成数据实体类ToolStripMenuItem;
    private ToolStripSeparator 名称ToolStripMenuItem;
    private ToolStripMenuItem 生成增删改命令到剪切板ToolStripMenuItem;
    private ToolStripMenuItem 显示隐藏代码窗口ToolStripMenuItem;

    public MainForm()
    {
        this.InitializeComponent();
    }
    private void InitializeComponent()
    {
        this.icontainer_0 = new Container();
        this.panel1 = new Panel();
        this.cboConnectionString = new ComboBox();
        this.btnConnect = new Button();
        this.label1 = new Label();
        this.panel2 = new Panel();
        this.splitter2 = new Splitter();
        this.panel5 = new Panel();
        this.linkLabel1 = new LinkLabel();
        this.splitter1 = new Splitter();
        this.panel3 = new Panel();
        this.treeView1 = new TreeView();
        this.imageList_0 = new ImageList(this.icontainer_0);
        this.panel4 = new Panel();
        this.contextMenuStrip5 = new ContextMenuStrip(this.icontainer_0);
        this.显示隐藏代码窗口ToolStripMenuItem = new ToolStripMenuItem();
        this.label2 = new Label();
        this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
        this.menuCopySpName = new ToolStripMenuItem();
        this.toolStripMenuItem1 = new ToolStripSeparator();
        this.menuGetXmlCommandBySP = new ToolStripMenuItem();
        this.contextMenuStrip2 = new ContextMenuStrip(this.icontainer_0);
        this.menuCopyTableName = new ToolStripMenuItem();
        this.toolStripMenuItem2 = new ToolStripSeparator();
        this.生成增删改命令到剪切板ToolStripMenuItem = new ToolStripMenuItem();
        this.contextMenuStrip3 = new ContextMenuStrip(this.icontainer_0);
        this.menuCopyDbName = new ToolStripMenuItem();
        this.名称ToolStripMenuItem = new ToolStripSeparator();
        this.根据查询生成数据实体类ToolStripMenuItem = new ToolStripMenuItem();
        this.toolStripMenuItem3 = new ToolStripSeparator();
        this.定位到指定对象ToolStripMenuItem = new ToolStripMenuItem();
        this.contextMenuStrip4 = new ContextMenuStrip(this.icontainer_0);
        this.menuCopyViewName = new ToolStripMenuItem();
        this.txtSqlScript = new SyntaxHighlighterControlFix();
        this.txtCsCode = new SyntaxHighlighterControlFix();
        this.ucParameterStyle1 = new ucParameterStyleFix();
        this.ucCsClassStyle1 = new UcCsClassStyleFix();
        this.panel1.SuspendLayout();
        this.panel2.SuspendLayout();
        this.panel5.SuspendLayout();
        this.panel3.SuspendLayout();
        this.panel4.SuspendLayout();
        this.contextMenuStrip5.SuspendLayout();
        this.contextMenuStrip1.SuspendLayout();
        this.contextMenuStrip2.SuspendLayout();
        this.contextMenuStrip3.SuspendLayout();
        this.contextMenuStrip4.SuspendLayout();
        base.SuspendLayout();
        this.panel1.Controls.Add(this.cboConnectionString);
        this.panel1.Controls.Add(this.btnConnect);
        this.panel1.Controls.Add(this.label1);
        this.panel1.Dock = DockStyle.Top;
        this.panel1.Location = new Point(0, 0);
        this.panel1.Name = "panel1";
        this.panel1.Size = new Size(0x38f, 0x24);
        this.panel1.TabIndex = 0;
        this.cboConnectionString.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
        this.cboConnectionString.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
        this.cboConnectionString.FormattingEnabled = true;
        this.cboConnectionString.Location = new Point(0x4d, 7);
        this.cboConnectionString.Name = "cboConnectionString";
        this.cboConnectionString.Size = new Size(0x2ca, 0x17);
        this.cboConnectionString.TabIndex = 2;
        this.btnConnect.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        this.btnConnect.FlatStyle = FlatStyle.Popup;
        this.btnConnect.Location = new Point(0x328, 8);
        this.btnConnect.Name = "btnConnect";
        this.btnConnect.Size = new Size(0x5e, 0x15);
        this.btnConnect.TabIndex = 0;
        this.btnConnect.Text = "连接数据库(&C)";
        this.btnConnect.UseVisualStyleBackColor = true;
        this.btnConnect.Click += new EventHandler(this.btnConnect_Click);
        this.label1.AutoSize = true;
        this.label1.Location = new Point(4, 11);
        this.label1.Name = "label1";
        this.label1.Size = new Size(0x41, 12);
        this.label1.TabIndex = 1;
        this.label1.Text = "连接字符串";
        this.panel2.Controls.Add(this.txtSqlScript);
        this.panel2.Controls.Add(this.splitter2);
        this.panel2.Controls.Add(this.txtCsCode);
        this.panel2.Controls.Add(this.panel5);
        this.panel2.Controls.Add(this.splitter1);
        this.panel2.Controls.Add(this.panel3);
        this.panel2.Dock = DockStyle.Fill;
        this.panel2.Location = new Point(0, 0x24);
        this.panel2.Name = "panel2";
        this.panel2.Size = new Size(0x38f, 0x221);
        this.panel2.TabIndex = 1;
        this.splitter2.Dock = DockStyle.Top;
        this.splitter2.Location = new Point(0xe1, 0x18a);
        this.splitter2.Name = "splitter2";
        this.splitter2.Size = new Size(0x2ae, 7);
        this.splitter2.TabIndex = 4;
        this.splitter2.TabStop = false;
        this.panel5.Controls.Add(this.ucParameterStyle1);
        this.panel5.Controls.Add(this.ucCsClassStyle1);
        this.panel5.Controls.Add(this.linkLabel1);
        this.panel5.Dock = DockStyle.Top;
        this.panel5.Location = new Point(0xe1, 0);
        this.panel5.Name = "panel5";
        this.panel5.Size = new Size(0x2ae, 0x1a);
        this.panel5.TabIndex = 2;
        this.linkLabel1.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        //this.linkLabel1.Image = Resources.Help;
        this.linkLabel1.ImageAlign = ContentAlignment.MiddleLeft;
        this.linkLabel1.LinkBehavior = LinkBehavior.NeverUnderline;
        this.linkLabel1.Location = new Point(0x24b, 4);
        this.linkLabel1.Name = "linkLabel1";
        this.linkLabel1.Size = new Size(0x60, 0x11);
        this.linkLabel1.TabIndex = 7;
        this.linkLabel1.TabStop = true;
        this.linkLabel1.Text = "查看帮助页面";
        this.linkLabel1.TextAlign = ContentAlignment.MiddleRight;
        this.linkLabel1.LinkClicked += new LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
        this.splitter1.Location = new Point(0xdb, 0);
        this.splitter1.Name = "splitter1";
        this.splitter1.Size = new Size(6, 0x221);
        this.splitter1.TabIndex = 1;
        this.splitter1.TabStop = false;
        this.panel3.Controls.Add(this.treeView1);
        this.panel3.Controls.Add(this.panel4);
        this.panel3.Dock = DockStyle.Left;
        this.panel3.Location = new Point(0, 0);
        this.panel3.Name = "panel3";
        this.panel3.Size = new Size(0xdb, 0x221);
        this.panel3.TabIndex = 0;
        this.treeView1.Dock = DockStyle.Fill;
        this.treeView1.HideSelection = false;
        this.treeView1.ImageIndex = 0;
        this.treeView1.ImageList = this.imageList_0;
        this.treeView1.Location = new Point(0, 0x1a);
        this.treeView1.Name = "treeView1";
        this.treeView1.SelectedImageIndex = 0;
        this.treeView1.Size = new Size(0xdb, 0x207);
        this.treeView1.TabIndex = 1;
        this.treeView1.AfterCollapse += new TreeViewEventHandler(this.treeView1_AfterCollapse);
        this.treeView1.BeforeExpand += new TreeViewCancelEventHandler(this.treeView1_BeforeExpand);
        this.treeView1.AfterSelect += new TreeViewEventHandler(this.treeView1_AfterSelect);
        this.treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
        this.treeView1.KeyDown += new KeyEventHandler(this.treeView1_KeyDown);
        this.treeView1.AfterExpand += new TreeViewEventHandler(this.treeView1_AfterExpand);
        this.imageList_0.ColorDepth = ColorDepth.Depth8Bit;
        this.imageList_0.ImageSize = new Size(0x10, 0x10);
        this.imageList_0.TransparentColor = Color.Transparent;
        this.panel4.ContextMenuStrip = this.contextMenuStrip5;
        this.panel4.Controls.Add(this.label2);
        this.panel4.Dock = DockStyle.Top;
        this.panel4.Location = new Point(0, 0);
        this.panel4.Name = "panel4";
        this.panel4.Size = new Size(0xdb, 0x1a);
        this.panel4.TabIndex = 0;
        this.contextMenuStrip5.Items.AddRange(new ToolStripItem[] { this.显示隐藏代码窗口ToolStripMenuItem });
        this.contextMenuStrip5.Name = "contextMenuStrip5";
        this.contextMenuStrip5.Size = new Size(0x99, 0x30);
        this.显示隐藏代码窗口ToolStripMenuItem.Name = "显示隐藏代码窗口ToolStripMenuItem";
        this.显示隐藏代码窗口ToolStripMenuItem.Size = new Size(0x98, 0x16);
        this.显示隐藏代码窗口ToolStripMenuItem.Text = "隐藏 代码窗口";
        this.显示隐藏代码窗口ToolStripMenuItem.Click += new EventHandler(this.显示隐藏代码窗口ToolStripMenuItem_Click);
        this.label2.AutoSize = true;
        this.label2.Location = new Point(4, 6);
        this.label2.Name = "label2";
        this.label2.Size = new Size(0x53, 12);
        this.label2.TabIndex = 0;
        this.label2.Text = "数据表列表(&D)";
        this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.menuCopySpName, this.toolStripMenuItem1, this.menuGetXmlCommandBySP });
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new Size(210, 0x36);
        this.menuCopySpName.Name = "menuCopySpName";
        this.menuCopySpName.Size = new Size(0xd1, 0x16);
        this.menuCopySpName.Text = "复制名称";
        this.menuCopySpName.Click += new EventHandler(this.menuCopyViewName_Click);
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        this.toolStripMenuItem1.Size = new Size(0xce, 6);
        this.menuGetXmlCommandBySP.Name = "menuGetXmlCommandBySP";
        this.menuGetXmlCommandBySP.Size = new Size(0xd1, 0x16);
        this.menuGetXmlCommandBySP.Text = "生成XmlCommand到剪切板";
        this.menuGetXmlCommandBySP.Click += new EventHandler(this.menuGetXmlCommandBySP_Click);
        this.contextMenuStrip2.Items.AddRange(new ToolStripItem[] { this.menuCopyTableName, this.toolStripMenuItem2, this.生成增删改命令到剪切板ToolStripMenuItem });
        this.contextMenuStrip2.Name = "contextMenuStrip2";
        this.contextMenuStrip2.Size = new Size(0xf6, 0x36);
        this.menuCopyTableName.Name = "menuCopyTableName";
        this.menuCopyTableName.Size = new Size(0xf5, 0x16);
        this.menuCopyTableName.Text = "复制名称";
        this.menuCopyTableName.Click += new EventHandler(this.menuCopyViewName_Click);
        this.toolStripMenuItem2.Name = "toolStripMenuItem2";
        this.toolStripMenuItem2.Size = new Size(0xf2, 6);
        this.生成增删改命令到剪切板ToolStripMenuItem.Name = "生成增删改命令到剪切板ToolStripMenuItem";
        this.生成增删改命令到剪切板ToolStripMenuItem.Size = new Size(0xf5, 0x16);
        this.生成增删改命令到剪切板ToolStripMenuItem.Text = "生成增删改XmlCommand到剪切板";
        this.生成增删改命令到剪切板ToolStripMenuItem.Click += new EventHandler(this.生成增删改命令到剪切板ToolStripMenuItem_Click);
        this.contextMenuStrip3.Items.AddRange(new ToolStripItem[] { this.menuCopyDbName, this.名称ToolStripMenuItem, this.根据查询生成数据实体类ToolStripMenuItem, this.toolStripMenuItem3, this.定位到指定对象ToolStripMenuItem });
        this.contextMenuStrip3.Name = "contextMenuStrip3";
        this.contextMenuStrip3.Size = new Size(0xcf, 0x52);
        this.menuCopyDbName.Name = "menuCopyDbName";
        this.menuCopyDbName.Size = new Size(0xce, 0x16);
        this.menuCopyDbName.Text = "复制名称";
        this.menuCopyDbName.Click += new EventHandler(this.menuCopyViewName_Click);
        this.名称ToolStripMenuItem.Name = "名称ToolStripMenuItem";
        this.名称ToolStripMenuItem.Size = new Size(0xcb, 6);
        this.根据查询生成数据实体类ToolStripMenuItem.Name = "根据查询生成数据实体类ToolStripMenuItem";
        this.根据查询生成数据实体类ToolStripMenuItem.Size = new Size(0xce, 0x16);
        this.根据查询生成数据实体类ToolStripMenuItem.Text = "根据查询生成数据实体类";
        this.根据查询生成数据实体类ToolStripMenuItem.Click += new EventHandler(this.根据查询生成数据实体类ToolStripMenuItem_Click);
        this.toolStripMenuItem3.Name = "toolStripMenuItem3";
        this.toolStripMenuItem3.Size = new Size(0xcb, 6);
        this.定位到指定对象ToolStripMenuItem.Name = "定位到指定对象ToolStripMenuItem";
        this.定位到指定对象ToolStripMenuItem.Size = new Size(0xce, 0x16);
        this.定位到指定对象ToolStripMenuItem.Text = "定位到指定对象";
        this.定位到指定对象ToolStripMenuItem.Click += new EventHandler(this.定位到指定对象ToolStripMenuItem_Click);
        this.contextMenuStrip4.Items.AddRange(new ToolStripItem[] { this.menuCopyViewName });
        this.contextMenuStrip4.Name = "contextMenuStrip4";
        this.contextMenuStrip4.Size = new Size(0x7b, 0x1a);
        this.menuCopyViewName.Name = "menuCopyViewName";
        this.menuCopyViewName.Size = new Size(0x7a, 0x16);
        this.menuCopyViewName.Text = "复制名称";
        this.menuCopyViewName.Click += new EventHandler(this.menuCopyViewName_Click);
        this.txtSqlScript.Dock = DockStyle.Fill;
        this.txtSqlScript.Location = new Point(0xe1, 0x191);
        this.txtSqlScript.Name = "txtSqlScript";
        this.txtSqlScript.Size = new Size(0x2ae, 0x90);
        this.txtSqlScript.TabIndex = 6;
        this.txtCsCode.Dock = DockStyle.Top;
        this.txtCsCode.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
        this.txtCsCode.method_0("cs");
        this.txtCsCode.Location = new Point(0xe1, 0x1a);
        this.txtCsCode.Margin = new Padding(3, 4, 3, 4);
        this.txtCsCode.Name = "txtCsCode";
        this.txtCsCode.method_6(false);
        this.txtCsCode.Size = new Size(0x2ae, 0x170);
        this.txtCsCode.TabIndex = 5;
        this.ucParameterStyle1.Location = new Point(0x41, 0);
        this.ucParameterStyle1.Name = "ucParameterStyle1";
        this.ucParameterStyle1.Size = new Size(0x18d, 0x19);
        this.ucParameterStyle1.TabIndex = 9;
        this.ucParameterStyle1.Visible = false;
        this.ucParameterStyle1.method_0(new EventHandler(this.method_7));
        this.ucCsClassStyle1.Location = new Point(3, 0);
        this.ucCsClassStyle1.Name = "ucCsClassStyle1";
        this.ucCsClassStyle1.Size = new Size(0x1f6, 0x19);
        this.ucCsClassStyle1.TabIndex = 8;
        this.ucCsClassStyle1.Visible = false;
        this.ucCsClassStyle1.method_0(new EventHandler(this.method_7));
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(0x38f, 0x245);
        base.Controls.Add(this.panel2);
        base.Controls.Add(this.panel1);
        base.KeyPreview = true;
        this.MinimumSize = new Size(700, 400);
        base.Name = "MainForm";
        base.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "FastDBEngine CodeGenerator for ORACLE";
        base.WindowState = FormWindowState.Maximized;
        base.Load += new EventHandler(this.MainForm_Load);
        base.Shown += new EventHandler(this.MainForm_Shown);
        base.FormClosing += new FormClosingEventHandler(this.MainForm_FormClosing);
        base.KeyDown += new KeyEventHandler(this.MainForm_KeyDown);
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        this.panel2.ResumeLayout(false);
        this.panel5.ResumeLayout(false);
        this.panel3.ResumeLayout(false);
        this.panel4.ResumeLayout(false);
        this.panel4.PerformLayout();
        this.contextMenuStrip5.ResumeLayout(false);
        this.contextMenuStrip1.ResumeLayout(false);
        this.contextMenuStrip2.ResumeLayout(false);
        this.contextMenuStrip3.ResumeLayout(false);
        this.contextMenuStrip4.ResumeLayout(false);
        base.ResumeLayout(false);
    }

    bool register = false;
    private void btnConnect_Click(object sender, EventArgs e)
    {

        if (this.cboConnectionString.Text.Trim().Length == 0)
        {
            MessageBox.Show("连接字符串不能为空。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        else
        {
            if (!register)
            {
                DbContext.RegisterDbConnectionInfo("oracle", "Oracle.DataAccess.Client", ":", this.cboConnectionString.Text.Trim());
                register = true;
            }
            this.method_0(new Action(this.method_1));
            this.treeView1.Focus();
        }
    }

  

    private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
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

    private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
    {
        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < this.cboConnectionString.Items.Count; i++)
        {
            builder.AppendLine(this.cboConnectionString.Items[i].ToString());
        }
        try
        {
            if (builder.Length > 0)
            {
                File.WriteAllText(string_1, builder.ToString(), Encoding.Unicode);
            }
            else if (File.Exists(string_1))
            {
                File.Delete(string_1);
            }
        }
        catch
        {
        }
    }

    private void MainForm_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Alt && (e.KeyCode == Keys.D))
        {
            this.treeView1.Focus();
        }
    }

    private void MainForm_Load(object sender, EventArgs e)
    {
        this.Text = this.Text + " V" + FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion;
        //base.Icon = Resources._003b;
        //this.imageList_0.Images.Add(Resources.database);
        //this.imageList_0.Images.Add(Resources.table);
        //this.imageList_0.Images.Add(Resources.notInclude);
        //this.imageList_0.Images.Add(Resources.folderclosed2);
        //this.imageList_0.Images.Add(Resources.folderopened2);
        //this.imageList_0.Images.Add(Resources.SqlTemplate);
        //this.imageList_0.Images.Add(Resources.view);
        this.ucParameterStyle1.Location = this.ucCsClassStyle1.Location;
    }

    private void MainForm_Shown(object sender, EventArgs e)
    {
        try
        {
            foreach (string str in File.ReadAllText(string_1, Encoding.Unicode).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                this.cboConnectionString.Items.Add(str);
            }
        }
        catch
        {
        }
        if (this.cboConnectionString.Items.Count == 0)
        {
            this.cboConnectionString.Text = @"Data Source=127.0.0.1/orcl;Persist Security Info=True;User ID=fx;Password=fx";
        }
        else
        {
            this.cboConnectionString.SelectedIndex = 0;
        }
    }

    private void menuCopyViewName_Click(object sender, EventArgs e)
    {
        if (this.treeNode_0 != null)
        {
            Clipboard.SetText(this.treeNode_0.Text);
        }
    }

    private void menuGetXmlCommandBySP_Click(object sender, EventArgs e)
    {
        Class5 class2 = new Class5
        {
            mainForm_0 = this
        };
        if (this.treeNode_0 != null)
        {
            class2.string_0 = this.treeNode_0.Parent.Parent.Text;
            this.method_0(new Action(class2.method_0));
        }
    }

    private void method_0(Action action_0)
    {
        try
        {
            action_0();
        }
        catch (Exception exception)
        {
            this.method_8(exception.ToString(), "txt");
        }
    }

    private void method_1()
    {
        string str = this.cboConnectionString.Text.Trim();
        //if (GClass1.smethod_3(str) < 9)
        //{
        //    MessageBox.Show("本程序只支持 SQL Server 2005 及以上版本。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        //}
        //else
        {
            List<string> list = new List<string> { "Oracle" };// GClass1.smethod_6(str);
            this.treeView1.Nodes.Clear();
            foreach (string str2 in list)
            {
                TreeNode node = new TreeNode(str2, int_0, int_0)
                {
                    Tag = string_3
                };
                TreeNode node2 = new TreeNode(string_4, int_3, int_3);
                node2.Nodes.Add(new TreeNode("loading...", int_2, int_2));
                node.Nodes.Add(node2);
                TreeNode node3 = new TreeNode(string_5, int_3, int_3);
                node3.Nodes.Add(new TreeNode("loading...", int_2, int_2));
                node.Nodes.Add(node3);
                TreeNode node4 = new TreeNode(string_6, int_3, int_3);
                node4.Nodes.Add(new TreeNode("loading...", int_2, int_2));
                node.Nodes.Add(node4);
                this.treeView1.Nodes.Add(node);
            }
            this.conn = str;
            this.method_2(str);
        }
    }

    private void method_10()
    {
        Class3 class2 = new Class3
        {
            mainForm_0 = this
        };
        if ((this.treeView1.SelectedNode != null) && (this.treeView1.SelectedNode.ImageIndex == int_6))
        {
            class2.string_0 = this.treeView1.SelectedNode.Parent.Parent.Text;
            class2.string_1 = this.treeView1.SelectedNode.Text;
            this.method_0(new Action(class2.method_0));
        }
    }

    private void method_11()
    {
        Class4 class2 = new Class4
        {
            mainForm_0 = this
        };
        if ((this.treeView1.SelectedNode != null) && (this.treeView1.SelectedNode.ImageIndex == int_5))
        {
            class2.string_0 = this.treeView1.SelectedNode.Parent.Parent.Text;
            class2.string_1 = this.treeView1.SelectedNode.Text;
            this.method_8(string.Empty, "cs");
            this.method_0(new Action(class2.method_0));
        }
    }

    private void method_2(string string_7)
    {
        for (int i = 0; i < this.cboConnectionString.Items.Count; i++)
        {
            if (string.Compare(string_7, this.cboConnectionString.Items[i].ToString(), StringComparison.OrdinalIgnoreCase) == 0)
            {
                return;
            }
        }
        this.cboConnectionString.Items.Add(string_7);
    }

    private void method_3(TreeNode treeNode_1, List<string> list_0, int int_7)
    {
        treeNode_1.Tag = "OK";
        treeNode_1.Nodes.Clear();
        if (list_0.Count == 0)
        {
            treeNode_1.Nodes.Add(new TreeNode(string_0, int_2, int_2));
        }
        else
        {
            foreach (string str in list_0)
            {
                treeNode_1.Nodes.Add(new TreeNode(str, int_7, int_7));
            }
        }
    }

    private void method_4(TreeViewEventArgs treeViewEventArgs_0)
    {
        List<string> list = GeneratorDbHelper.GetList(this.conn, treeViewEventArgs_0.Node.Parent.Text);
        this.method_3(treeViewEventArgs_0.Node, list, int_1);
    }

    private void method_5(TreeViewEventArgs treeViewEventArgs_0)
    {
        List<string> list = GeneratorDbHelper.GetAllViews(this.conn, treeViewEventArgs_0.Node.Parent.Text);
        this.method_3(treeViewEventArgs_0.Node, list, int_6);
    }

    private void method_6(TreeViewEventArgs treeViewEventArgs_0)
    {
        List<string> list = GeneratorDbHelper.GetprocNames(this.conn, treeViewEventArgs_0.Node.Parent.Text);
        this.method_3(treeViewEventArgs_0.Node, list, int_5);
    }

    private void method_7(object sender, EventArgs e)
    {
        if (this.treeView1.SelectedNode != null)
        {
            this.treeView1_AfterSelect(sender, new TreeViewEventArgs(this.treeView1.SelectedNode));
        }
    }

    private void method_8(string string_7, string string_8)
    {
        this.txtCsCode.method_0(string_8);
        this.txtCsCode.SetText(string_7);
    }

    private void method_9()
    {
        Class2 class2 = new Class2
        {
            mainForm_0 = this
        };
        if ((this.treeView1.SelectedNode != null) && (this.treeView1.SelectedNode.ImageIndex == int_1))
        {
            class2.configName = this.treeView1.SelectedNode.Parent.Parent.Text;
            class2.tableName = this.treeView1.SelectedNode.Text;
            this.method_0(new Action(class2.method_0));
        }
    }

    void Dispose(bool disposing)
    {
        if (disposing && (this.icontainer_0 != null))
        {
            this.icontainer_0.Dispose();
        }
        base.Dispose(disposing);
    }

    private void treeView1_AfterCollapse(object sender, TreeViewEventArgs e)
    {
        if (e.Node.ImageIndex == int_4)
        {
            e.Node.ImageIndex = int_3;
            e.Node.SelectedImageIndex = int_3;
        }
    }

    private void treeView1_AfterExpand(object sender, TreeViewEventArgs e)
    {
        if (this.conn != null)
        {
            Application.DoEvents();
            if ((e.Node.Text == string_4) && (e.Node.Tag == null))
            {
                this.method_4(e);
            }
            else if ((e.Node.Text == string_5) && (e.Node.Tag == null))
            {
                this.method_5(e);
            }
            else if ((e.Node.Text == string_6) && (e.Node.Tag == null))
            {
                this.method_6(e);
            }
        }
    }

    private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
    {
        if (this.txtCsCode.Visible)
        {
            this.ucCsClassStyle1.Visible = (e.Node.ImageIndex == int_1) || (e.Node.ImageIndex == int_6);
            this.ucParameterStyle1.Visible = e.Node.ImageIndex == int_5;
        }
        if (e.Node.ImageIndex == int_1)
        {
            this.method_9();
        }
        else if (e.Node.ImageIndex == int_5)
        {
            this.method_11();
        }
        else if (e.Node.ImageIndex == int_6)
        {
            this.method_10();
        }
    }

    private void treeView1_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
        if (e.Node.ImageIndex == int_3)
        {
            e.Node.ImageIndex = int_4;
            e.Node.SelectedImageIndex = int_4;
        }
    }

    private void treeView1_KeyDown(object sender, KeyEventArgs e)
    {
        if (((this.treeView1.SelectedNode != null) && e.Control) && (e.KeyCode == Keys.C))
        {
            Clipboard.SetText(this.treeView1.SelectedNode.Text);
        }
    }

    private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Button == MouseButtons.Right)
        {
            this.treeNode_0 = e.Node;
            if (e.Node.ImageIndex == int_5)
            {
                this.contextMenuStrip1.Show(this.treeView1, e.Location);
            }
            else if (e.Node.ImageIndex == int_1)
            {
                this.contextMenuStrip2.Show(this.treeView1, e.Location);
            }
            else if (e.Node.ImageIndex == int_0)
            {
                this.contextMenuStrip3.Show(this.treeView1, e.Location);
            }
            else if (e.Node.ImageIndex == int_6)
            {
                this.contextMenuStrip4.Show(this.treeView1, e.Location);
            }
        }
    }

    private void 定位到指定对象ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if ((this.treeNode_0 != null) && (this.treeNode_0.Nodes.Count == 3))
        {
            string str = InputNameDialogFix.smethod_0();
            if (!string.IsNullOrEmpty(str))
            {
                foreach (TreeNode node2 in this.treeNode_0.Nodes)
                {
                    TreeViewEventArgs args = new TreeViewEventArgs(node2);
                    this.treeView1_AfterExpand(null, args);
                    IEnumerator enumerator2 = node2.Nodes.GetEnumerator();
                    {
                        TreeNode current;
                        while (enumerator2.MoveNext())
                        {
                            current = (TreeNode)enumerator2.Current;
                            if (string.Compare(current.Text, str, StringComparison.OrdinalIgnoreCase) == 0)
                            {
                                goto Label_00AE;
                            }
                        }
                        continue;
                    Label_00AE:
                        node2.Expand();
                        this.treeView1.SelectedNode = current;
                        current.EnsureVisible();
                        break;
                    }
                }
            }
        }
    }

    private void 根据查询生成数据实体类ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (this.treeNode_0 != null)
        {
            new QueryDialogFix(this.conn, this.treeNode_0.Text) { Owner = this }.Show();
        }
    }

    private void 生成增删改命令到剪切板ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        Class6 class2 = new Class6
        {
            mainForm_0 = this
        };
        if (this.treeNode_0 != null)
        {
            class2.string_0 = this.treeNode_0.Parent.Parent.Text;
            this.method_0(new Action(class2.SetClipBoardText));
        }
    }

    private void 显示隐藏代码窗口ToolStripMenuItem_Click(object sender, EventArgs e)
    {
        if (this.txtCsCode.Visible)
        {
            this.ucParameterStyle1.Visible = false;
            this.ucCsClassStyle1.Visible = false;
            this.txtCsCode.Visible = false;
            this.splitter2.Visible = false;
            this.显示隐藏代码窗口ToolStripMenuItem.Text = "显示 代码窗口";
        }
        else
        {
            this.txtCsCode.Visible = true;
            this.splitter2.Visible = true;
            if (this.treeView1.SelectedNode != null)
            {
                TreeNode selectedNode = this.treeView1.SelectedNode;
                this.ucCsClassStyle1.Visible = (selectedNode.ImageIndex == int_1) || (selectedNode.ImageIndex == int_6);
                this.ucParameterStyle1.Visible = selectedNode.ImageIndex == int_5;
            }
            this.显示隐藏代码窗口ToolStripMenuItem.Text = "隐藏 代码窗口";
        }
    }

    [CompilerGenerated]
    private sealed class Class2
    {
        private static Func<Field, string> func_0;
        public MainForm mainForm_0;
        public string configName;
        public string tableName;

        public void method_0()
        {
            List<Field> list = GeneratorDbHelper.GetFieldList(this.mainForm_0.conn, this.configName, this.tableName);
            if (this.mainForm_0.ucCsClassStyle1.method_2().SortByName)
            {
                if (func_0 == null)
                {
                    func_0 = new Func<Field, string>(MainForm.Class2.smethod_0);
                }
                list = Enumerable.OrderBy<Field, string>(list, func_0).ToList<Field>();
            }
            this.mainForm_0.method_8(GeneratorClassHelper.smethod_1(this.tableName.FilterStr(), list, this.mainForm_0.ucCsClassStyle1.method_2()), "cs");
            this.mainForm_0.txtSqlScript.SetText(GeneratorDbHelper.GetTableDefinitionText(list, this.tableName));
        }

        private static string smethod_0(Field field_0)
        {
            return field_0.Name;
        }
    }

    [CompilerGenerated]
    private sealed class Class3
    {
        private static Func<Field, string> func_0;
        public MainForm mainForm_0;
        public string string_0;
        public string string_1;

        public void method_0()
        {
            string str = string.Format("select   * from {0} where 1>2", this.string_1);
            List<Field> list = GeneratorDbHelper.GetFieldListFromReader(this.mainForm_0.conn, this.string_0, str);
            if (this.mainForm_0.ucCsClassStyle1.method_2().SortByName)
            {
                if (func_0 == null)
                {
                    func_0 = new Func<Field, string>(MainForm.Class3.smethod_0);
                }
                list = Enumerable.OrderBy<Field, string>(list, func_0).ToList<Field>();
            }
            this.mainForm_0.method_8(GeneratorClassHelper.smethod_1(this.string_1.FilterStr(), list, this.mainForm_0.ucCsClassStyle1.method_2()), "cs");
            this.mainForm_0.txtSqlScript.SetText(GeneratorDbHelper.ExecuteScalarViewDefinition(this.mainForm_0.conn, this.string_0, this.string_1));
        }

        private static string smethod_0(Field field_0)
        {
            return field_0.Name;
        }
    }

    [CompilerGenerated]
    private sealed class Class4
    {
        public MainForm mainForm_0;
        public string string_0;
        public string string_1;

        public void method_0()
        {
            DbParameter[] parameterArray = GeneratorDbHelper.GetParameters(this.mainForm_0.conn, this.string_0, this.string_1);
            this.mainForm_0.method_8(GeneratorClassHelper.CallProcMethodGenerator(parameterArray, this.string_1, 0, this.mainForm_0.ucParameterStyle1.method_2()), "cs");
            this.mainForm_0.txtSqlScript.SetText(GeneratorDbHelper.GetCommandText(this.mainForm_0.conn, this.string_0, this.string_1));
        }
    }

    [CompilerGenerated]
    private sealed class Class5
    {
        public MainForm mainForm_0;
        public string string_0;

        public void method_0()
        {
            Clipboard.SetText(XmlHelper.XmlSerializerObject(GeneratorDbHelper.AddParametersToCommand(this.mainForm_0.conn, this.string_0, this.mainForm_0.treeNode_0.Text)));
        }
    }

    [CompilerGenerated]
    private sealed class Class6
    {
        public MainForm mainForm_0;
        public string string_0;

        public void SetClipBoardText()
        {
            Clipboard.SetText(XmlHelper.XmlSerializerObject(GeneratorDbHelper.GetListXmlCommands(this.mainForm_0.conn, this.string_0, this.mainForm_0.treeNode_0.Text)));
        }
    }
}

