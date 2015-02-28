using FastDBEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;

namespace FastDBEngineGenerator
{
    public partial class MainFormFix : Form
    {
        public MainFormFix()
        {
            InitializeComponent();
            this.cboConnectionString.Text = System.Configuration.ConfigurationManager.AppSettings["oracle"];
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
                this.GenerateParameters(new Action(this.LoadTreeNode));
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
                    File.WriteAllText(connText, builder.ToString(), Encoding.Unicode);
                }
                else if (File.Exists(connText))
                {
                    File.Delete(connText);
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
                foreach (string str in File.ReadAllText(connText, Encoding.Unicode).Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    this.cboConnectionString.Items.Add(str);
                }
            }
            catch
            {
            }
            if (this.cboConnectionString.Items.Count == 0)
            {
                this.cboConnectionString.Text = System.Configuration.ConfigurationManager.AppSettings["oracle"];
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
            TempParametersToClipBoardContainer class2 = new TempParametersToClipBoardContainer
            {
                mainForm = this
            };
            if (this.treeNode_0 != null)
            {
                class2.database = this.treeNode_0.Parent.Parent.Text;
                this.GenerateParameters(new Action(class2.SetClipboardText));
            }
        }

        private void GenerateParameters(Action action)
        {
            try
            {
                action();
            }
            catch (Exception exception)
            {
                this.SetUpText(exception.ToString(), "txt");
            }
        }

        private void LoadTreeNode()
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
                        Tag = database
                    };
                    TreeNode node2 = new TreeNode(table, int_3, int_3);
                    node2.Nodes.Add(new TreeNode("loading...", int_2, int_2));
                    node.Nodes.Add(node2);
                    TreeNode node3 = new TreeNode(views, int_3, int_3);
                    node3.Nodes.Add(new TreeNode("loading...", int_2, int_2));
                    node.Nodes.Add(node3);
                    TreeNode node4 = new TreeNode(Procedures, int_3, int_3);
                    node4.Nodes.Add(new TreeNode("loading...", int_2, int_2));
                    node.Nodes.Add(node4);
                    this.treeView1.Nodes.Add(node);
                }
                this.conn = str;
                this.AddComboxItem(str);
            }
        }

        private void method_10()
        {
            TempMainFormFixContainer class2 = new TempMainFormFixContainer
            {
                mainForm = this
            };
            if ((this.treeView1.SelectedNode != null) && (this.treeView1.SelectedNode.ImageIndex == int_6))
            {
                class2.configName = this.treeView1.SelectedNode.Parent.Parent.Text;
                class2.tableName = this.treeView1.SelectedNode.Text;
                this.GenerateParameters(new Action(class2.SetText));
            }
        }

        private void SetGenerateParametersText()
        {
            TempParameterContainer tempParameterContainer = new TempParameterContainer
            {
                mainForm = this
            };
            if ((this.treeView1.SelectedNode != null) && (this.treeView1.SelectedNode.ImageIndex == int_5))
            {
                tempParameterContainer.database = this.treeView1.SelectedNode.Parent.Parent.Text;
                tempParameterContainer.commmandName = this.treeView1.SelectedNode.Text;
                this.SetUpText(string.Empty, "cs");
                this.GenerateParameters(new Action(tempParameterContainer.GenerateParameter));
            }
        }

        private void AddComboxItem(string conn)
        {
            for (int i = 0; i < this.cboConnectionString.Items.Count; i++)
            {
                if (string.Compare(conn, this.cboConnectionString.Items[i].ToString(), StringComparison.OrdinalIgnoreCase) == 0)
                {
                    return;
                }
            }
            this.cboConnectionString.Items.Add(conn);
        }

        private void AddUITreeNode(TreeNode treeNode, List<string> list, int imageIndex)
        {
            treeNode.Tag = "OK";
            treeNode.Nodes.Clear();
            if (list.Count == 0)
            {
                treeNode.Nodes.Add(new TreeNode(str, int_2, int_2));
            }
            else
            {
                foreach (string text in list)
                {
                    treeNode.Nodes.Add(new TreeNode(text, imageIndex, imageIndex));
                }
            }
        }

        private void GetDbList(TreeViewEventArgs treeViewEventArgs)
        {
            List<string> list = GeneratorDbHelper.GetList(this.conn, treeViewEventArgs.Node.Parent.Text);
            this.AddUITreeNode(treeViewEventArgs.Node, list, int_1);
        }

        private void GetAllViews(TreeViewEventArgs treeViewEventArgs)
        {
            List<string> list = GeneratorDbHelper.GetAllViews(this.conn, treeViewEventArgs.Node.Parent.Text);
            this.AddUITreeNode(treeViewEventArgs.Node, list, int_6);
        }

        private void GetprocNames(TreeViewEventArgs treeViewEventArgs)
        {
            List<string> list = GeneratorDbHelper.GetprocNames(this.conn, treeViewEventArgs.Node.Parent.Text);
            this.AddUITreeNode(treeViewEventArgs.Node, list, int_5);
        }

        private void OntreeViewAfterSelect(object sender, EventArgs e)
        {
            if (this.treeView1.SelectedNode != null)
            {
                this.treeView1_AfterSelect(sender, new TreeViewEventArgs(this.treeView1.SelectedNode));
            }
        }

        private void SetUpText(string string_7, string string_8)
        {
            this.txtCsCode.SetLanguage(string_8);
            this.txtCsCode.SetText(string_7);
        }

        private void GenerateParametersWrapper()
        {
            TempTextGenerate tempTextGenerate = new TempTextGenerate
            {
                mainForm = this
            };
            if ((this.treeView1.SelectedNode != null) && (this.treeView1.SelectedNode.ImageIndex == int_1))
            {
                tempTextGenerate.configName = this.treeView1.SelectedNode.Parent.Parent.Text;
                tempTextGenerate.tableName = this.treeView1.SelectedNode.Text;
                this.GenerateParameters(new Action(tempTextGenerate.SetUIText));
            }
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
                if ((e.Node.Text == table) && (e.Node.Tag == null))
                {
                    this.GetDbList(e);
                }
                else if ((e.Node.Text == views) && (e.Node.Tag == null))
                {
                    this.GetAllViews(e);
                }
                else if ((e.Node.Text == Procedures) && (e.Node.Tag == null))
                {
                    this.GetprocNames(e);
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
                this.GenerateParametersWrapper();
            }
            else if (e.Node.ImageIndex == int_5)
            {
                this.SetGenerateParametersText();
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
                string str = InputNameDialogFix.GetDialogText();
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
            TempXmlCommandsToClipBoardContainer class2 = new TempXmlCommandsToClipBoardContainer
            {
                mainForm = this
            };
            if (this.treeNode_0 != null)
            {
                class2.configName = this.treeNode_0.Parent.Parent.Text;
                this.GenerateParameters(new Action(class2.SetClipBoardText));
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
        private sealed class TempTextGenerate
        {
            private static Func<Field, string> func;
            public MainFormFix mainForm;
            public string configName;
            public string tableName;

            public void SetUIText()
            {
                List<Field> list = GeneratorDbHelper.GetFieldList(this.mainForm.conn, this.configName, this.tableName);
                if (this.mainForm.ucCsClassStyle1.GetCsClassStyle().SortByName)
                {
                    if (func == null)
                    {
                        func = new Func<Field, string>(t => { return t.Name; });
                    }
                    list = Enumerable.OrderBy<Field, string>(list, func).ToList<Field>();
                }
                this.mainForm.SetUpText(GeneratorClassHelper.GenerateModel(this.tableName.FilterStr(), list, this.mainForm.ucCsClassStyle1.GetCsClassStyle()), "cs");
                this.mainForm.txtSqlScript.SetText(GeneratorDbHelper.GetTableDefinitionText(list, this.tableName));
            }
        }

        [CompilerGenerated]
        private sealed class TempMainFormFixContainer
        {
            private static Func<Field, string> func;
            public MainFormFix mainForm;
            public string configName;
            public string tableName;

            public void SetText()
            {
                string str = string.Format("select   * from {0} where 1>2", this.tableName);
                List<Field> list = GeneratorDbHelper.GetFieldListFromReader(this.mainForm.conn, this.configName, str);
                if (this.mainForm.ucCsClassStyle1.GetCsClassStyle().SortByName)
                {
                    if (func == null)
                    {
                        func = new Func<Field, string>(t => { return t.Name; });
                    }
                    list = Enumerable.OrderBy<Field, string>(list, func).ToList<Field>();
                }
                this.mainForm.SetUpText(GeneratorClassHelper.GenerateModel(this.tableName.FilterStr(), list, this.mainForm.ucCsClassStyle1.GetCsClassStyle()), "cs");
                this.mainForm.txtSqlScript.SetText(GeneratorDbHelper.ExecuteScalarViewDefinition(this.mainForm.conn, this.configName, this.tableName));
            }

        }

        [CompilerGenerated]
        private sealed class TempParameterContainer
        {
            public MainFormFix mainForm;
            public string database;
            public string commmandName;

            public void GenerateParameter()
            {
                DbParameter[] parameterArray = GeneratorDbHelper.GetParameters(this.mainForm.conn, this.database, this.commmandName);
               // parameterArray = parameterArray.Where(t => t.DbType != DbType.Object).ToArray();
                this.mainForm.SetUpText(GeneratorClassHelper.CallProcMethodGenerator(parameterArray, this.commmandName, 0, this.mainForm.ucParameterStyle1.RadioIsChecked()), "cs");
                this.mainForm.txtSqlScript.SetText(GeneratorDbHelper.GetCommandText(this.mainForm.conn, this.database, this.commmandName));
            }
        }

        [CompilerGenerated]
        private sealed class TempParametersToClipBoardContainer
        {
            public MainFormFix mainForm;
            public string database;

            public void SetClipboardText()
            {
                Clipboard.SetText(XmlHelper.XmlSerializerObject(GeneratorDbHelper.AddParametersToCommand(this.mainForm.conn, this.database, this.mainForm.treeNode_0.Text)));
            }
        }

        [CompilerGenerated]
        private sealed class TempXmlCommandsToClipBoardContainer
        {
            public MainFormFix mainForm;
            public string configName;

            public void SetClipBoardText()
            {
                Clipboard.SetText(XmlHelper.XmlSerializerObject(GeneratorDbHelper.GetListXmlCommands(this.mainForm.conn, this.configName, this.mainForm.treeNode_0.Text)));
            }
        }
    }
}
