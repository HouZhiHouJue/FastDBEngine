using FastDBEngine;
using FastDBEngineProfilerLib;
using FastDBEngineSQLProfiler.Properties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
public class SQLProfilerForm : Form
{
	[CompilerGenerated]
	private sealed class Class0
	{
		public SQLProfilerForm sqlprofilerForm_0;
		public ExecuteInfo executeInfo_0;
		public void method_0(object object_0)
		{
			this.sqlprofilerForm_0.method_3(this.executeInfo_0);
		}
	}
	private const int int_0 = 0;
	private const int int_1 = 1;
	private const int int_2 = 2;
	private const int int_3 = 3;
	private const int int_4 = 4;
	private bool bool_0;
	private int int_5 = 0;
	private int int_6 = -1;
	private SynchronizationContext synchronizationContext_0;
	private bool bool_1 = true;
    private static readonly string string_0 = "SOFTWARE\\fish-li\\FastDBEngineSQLProfiler";
	private static readonly string string_1 = "ColWidthList";
	private static readonly string string_2 = "TextBoxHeight";
	private static readonly string string_3 = "FormSizeLocation";
	private IContainer icontainer_0 = null;
	private ToolStrip toolStrip1;
	private TextBox txtExecuteInfo;
	private Splitter splitter1;
	private ListView listView1;
	private ImageList imageList_0;
	private ToolStripDropDownButton btnState;
	private ToolStripMenuItem 监听消息ToolStripMenuItem;
	private ToolStripMenuItem 停止监听ToolStripMenuItem;
	private ToolStripSeparator toolStripSeparator1;
	private ToolStripButton btnExit;
	private ColumnHeader columnHeader_0;
	private ColumnHeader columnHeader_1;
	private ColumnHeader columnHeader_2;
	private ColumnHeader columnHeader_3;
	private ColumnHeader columnHeader_4;
	private ToolStripDropDownButton btnDelete;
	private ToolStripMenuItem 删除选定项ToolStripMenuItem;
	private ToolStripMenuItem 删除全部ToolStripMenuItem;
	private ColumnHeader columnHeader_5;
	private ToolStripSeparator toolStripSeparator2;
	private ToolStripButton btnTopMost;
	private ToolStripDropDownButton btnExport;
	private ToolStripMenuItem btnExportText;
	private ToolStripMenuItem btnExportXml;
	private ToolStripSeparator toolStripMenuItem1;
	private ToolStripMenuItem btnImportXml;

    protected override void Dispose(bool disposing)
    {
        if (disposing && this.icontainer_0 != null)
        {
            this.icontainer_0.Dispose();
        }
        base.Dispose(disposing);
    }
    private void InitializeComponent()
    {
        this.icontainer_0 = new Container();
        this.toolStrip1 = new ToolStrip();
        this.btnState = new ToolStripDropDownButton();
        this.监听消息ToolStripMenuItem = new ToolStripMenuItem();
        this.停止监听ToolStripMenuItem = new ToolStripMenuItem();
        this.btnExport = new ToolStripDropDownButton();
        this.btnExportText = new ToolStripMenuItem();
        this.btnExportXml = new ToolStripMenuItem();
        this.toolStripMenuItem1 = new ToolStripSeparator();
        this.btnImportXml = new ToolStripMenuItem();
        this.btnDelete = new ToolStripDropDownButton();
        this.删除选定项ToolStripMenuItem = new ToolStripMenuItem();
        this.删除全部ToolStripMenuItem = new ToolStripMenuItem();
        this.toolStripSeparator2 = new ToolStripSeparator();
        this.btnTopMost = new ToolStripButton();
        this.toolStripSeparator1 = new ToolStripSeparator();
        this.btnExit = new ToolStripButton();
        this.txtExecuteInfo = new TextBox();
        this.splitter1 = new Splitter();
        this.listView1 = new ListView();
        this.columnHeader_5 = new ColumnHeader();
        this.columnHeader_0 = new ColumnHeader();
        this.columnHeader_1 = new ColumnHeader();
        this.columnHeader_2 = new ColumnHeader();
        this.columnHeader_3 = new ColumnHeader();
        this.columnHeader_4 = new ColumnHeader();
        this.imageList_0 = new ImageList(this.icontainer_0);
        this.toolStrip1.SuspendLayout();
        base.SuspendLayout();
        this.toolStrip1.Items.AddRange(new ToolStripItem[]
		{
			this.btnState,
			this.btnExport,
			this.btnDelete,
			this.toolStripSeparator2,
			this.btnTopMost,
			this.toolStripSeparator1,
			this.btnExit
		});
        this.toolStrip1.Location = new Point(0, 0);
        this.toolStrip1.Name = "toolStrip1";
        this.toolStrip1.Size = new Size(881, 25);
        this.toolStrip1.TabIndex = 0;
        this.toolStrip1.Text = "toolStrip1";
        this.btnState.DropDownItems.AddRange(new ToolStripItem[]
		{
			this.监听消息ToolStripMenuItem,
			this.停止监听ToolStripMenuItem
		});
        //this.btnState.Image = Resources.ball3;
        this.btnState.Name = "btnState";
        this.btnState.Size = new Size(84, 22);
        this.btnState.Text = "监听消息";
        //this.监听消息ToolStripMenuItem.Image = Resources.ball3;
        this.监听消息ToolStripMenuItem.Name = "监听消息ToolStripMenuItem";
        this.监听消息ToolStripMenuItem.Size = new Size(122, 22);
        this.监听消息ToolStripMenuItem.Text = "监听消息";
        this.监听消息ToolStripMenuItem.Click += new EventHandler(this.监听消息ToolStripMenuItem_Click);
        //this.停止监听ToolStripMenuItem.Image = Resources.ball2;
        this.停止监听ToolStripMenuItem.Name = "停止监听ToolStripMenuItem";
        this.停止监听ToolStripMenuItem.Size = new Size(122, 22);
        this.停止监听ToolStripMenuItem.Text = "停止监听";
        this.停止监听ToolStripMenuItem.Click += new EventHandler(this.停止监听ToolStripMenuItem_Click);
        this.btnExport.DropDownItems.AddRange(new ToolStripItem[]
		{
			this.btnExportText,
			this.btnExportXml,
			this.toolStripMenuItem1,
			this.btnImportXml
		});
        //this.btnExport.Image = Resources.grid7;
        this.btnExport.Name = "btnExport";
        this.btnExport.Size = new Size(108, 22);
        this.btnExport.Text = "消息导入导出";
        this.btnExportText.Name = "btnExportText";
        this.btnExportText.Size = new Size(182, 22);
        this.btnExportText.Text = "以普通文本格式导出";
        this.btnExportText.Click += new EventHandler(this.btnExportText_Click);
        this.btnExportXml.Name = "btnExportXml";
        this.btnExportXml.Size = new Size(182, 22);
        this.btnExportXml.Text = "以XML格式导出";
        this.btnExportXml.Click += new EventHandler(this.btnExportXml_Click);
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        this.toolStripMenuItem1.Size = new Size(179, 6);
        this.btnImportXml.Name = "btnImportXml";
        this.btnImportXml.Size = new Size(182, 22);
        this.btnImportXml.Text = "导入XML格式消息";
        this.btnImportXml.Click += new EventHandler(this.btnImportXml_Click);
        this.btnDelete.DropDownItems.AddRange(new ToolStripItem[]
		{
			this.删除选定项ToolStripMenuItem,
			this.删除全部ToolStripMenuItem
		});
        //this.btnDelete.Image = Resources.dele2;
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.Size = new Size(84, 22);
        this.btnDelete.Text = "删除消息";
        this.删除选定项ToolStripMenuItem.Name = "删除选定项ToolStripMenuItem";
        this.删除选定项ToolStripMenuItem.Size = new Size(158, 22);
        this.删除选定项ToolStripMenuItem.Text = "删除选定项";
        this.删除选定项ToolStripMenuItem.Click += new EventHandler(this.删除选定项ToolStripMenuItem_Click);
        this.删除全部ToolStripMenuItem.Name = "删除全部ToolStripMenuItem";
        this.删除全部ToolStripMenuItem.Size = new Size(158, 22);
        this.删除全部ToolStripMenuItem.Text = "删除全部列表项";
        this.删除全部ToolStripMenuItem.Click += new EventHandler(this.删除全部ToolStripMenuItem_Click);
        this.toolStripSeparator2.Name = "toolStripSeparator2";
        this.toolStripSeparator2.Size = new Size(6, 25);
        this.btnTopMost.CheckOnClick = true;
        //this.btnTopMost.Image = Resources.unlockWnd;
        this.btnTopMost.Name = "btnTopMost";
        this.btnTopMost.Size = new Size(75, 22);
        this.btnTopMost.Text = "窗口置顶";
        this.btnTopMost.CheckStateChanged += new EventHandler(this.btnTopMost_CheckStateChanged);
        this.toolStripSeparator1.Name = "toolStripSeparator1";
        this.toolStripSeparator1.Size = new Size(6, 25);
        //this.btnExit.Image = Resources.exit2;
        this.btnExit.Name = "btnExit";
        this.btnExit.Size = new Size(75, 22);
        this.btnExit.Text = "退出程序";
        this.btnExit.Click += new EventHandler(this.btnExit_Click);
        this.txtExecuteInfo.AcceptsReturn = true;
        this.txtExecuteInfo.Dock = DockStyle.Bottom;
        this.txtExecuteInfo.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
        this.txtExecuteInfo.Location = new Point(0, 303);
        this.txtExecuteInfo.Multiline = true;
        this.txtExecuteInfo.Name = "txtExecuteInfo";
        this.txtExecuteInfo.ReadOnly = true;
        this.txtExecuteInfo.ScrollBars = ScrollBars.Both;
        this.txtExecuteInfo.Size = new Size(881, 201);
        this.txtExecuteInfo.TabIndex = 1;
        this.txtExecuteInfo.WordWrap = false;
        this.splitter1.Dock = DockStyle.Bottom;
        this.splitter1.Location = new Point(0, 296);
        this.splitter1.Name = "splitter1";
        this.splitter1.Size = new Size(881, 7);
        this.splitter1.TabIndex = 2;
        this.splitter1.TabStop = false;
        this.listView1.Columns.AddRange(new ColumnHeader[]
		{
			this.columnHeader_5,
			this.columnHeader_0,
			this.columnHeader_1,
			this.columnHeader_2,
			this.columnHeader_3,
			this.columnHeader_4
		});
        this.listView1.Dock = DockStyle.Fill;
        this.listView1.FullRowSelect = true;
        this.listView1.HideSelection = false;
        this.listView1.Location = new Point(0, 25);
        this.listView1.Name = "listView1";
        this.listView1.Size = new Size(881, 271);
        this.listView1.SmallImageList = this.imageList_0;
        this.listView1.TabIndex = 3;
        this.listView1.UseCompatibleStateImageBehavior = false;
        this.listView1.View = View.Details;
        this.listView1.SelectedIndexChanged += new EventHandler(this.listView1_SelectedIndexChanged);
        this.columnHeader_5.Text = "序号";
        this.columnHeader_5.Width = 54;
        this.columnHeader_0.Text = "命令文本";
        this.columnHeader_0.Width = 196;
        this.columnHeader_1.Text = "命令参数";
        this.columnHeader_1.Width = 210;
        this.columnHeader_2.Text = "开始时间";
        this.columnHeader_2.Width = 115;
        this.columnHeader_3.Text = "完成时间";
        this.columnHeader_3.Width = 168;
        this.columnHeader_4.Text = "应用程序标识";
        this.columnHeader_4.Width = 110;
        this.imageList_0.ColorDepth = ColorDepth.Depth8Bit;
        this.imageList_0.ImageSize = new Size(16, 16);
        this.imageList_0.TransparentColor = Color.Transparent;
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.ClientSize = new Size(881, 504);
        base.Controls.Add(this.listView1);
        base.Controls.Add(this.splitter1);
        base.Controls.Add(this.txtExecuteInfo);
        base.Controls.Add(this.toolStrip1);
        base.Name = "SQLProfilerForm";
        this.Text = "FastDBEngineSQLProfiler";
        base.Load += new EventHandler(this.SQLProfilerForm_Load);
        base.Shown += new EventHandler(this.SQLProfilerForm_Shown);
        base.FormClosing += new FormClosingEventHandler(this.SQLProfilerForm_FormClosing);
        this.toolStrip1.ResumeLayout(false);
        this.toolStrip1.PerformLayout();
        base.ResumeLayout(false);
        base.PerformLayout();
    }
	public SQLProfilerForm()
	{
		this.InitializeComponent();
		this.synchronizationContext_0 = SynchronizationContext.Current;
	}
	private void SQLProfilerForm_Load(object sender, EventArgs e)
	{
        //base.Icon = Resources._039s;
		this.Text = this.Text + " V " + FileVersionInfo.GetVersionInfo(Application.ExecutablePath).FileVersion;
        //this.imageList_0.Images.Add(Resources.Bitmap_1);
        //this.imageList_0.Images.Add(Resources.Bitmap_2);
        //this.imageList_0.Images.Add(Resources.Stop16);
        //this.imageList_0.Images.Add(Resources.event2);
        //this.imageList_0.Images.Add(Resources.xcmd);
		this.method_0();
	}
	private void SQLProfilerForm_FormClosing(object sender, FormClosingEventArgs e)
	{
		this.method_1();
	}
	private void method_0()
	{
		Hashtable hashtable = new Hashtable();
		hashtable.Add(SQLProfilerForm.string_1, string.Empty);
		hashtable.Add(SQLProfilerForm.string_2, 0);
		hashtable.Add(SQLProfilerForm.string_3, string.Empty);
		Exception ex = RegisterHelper.RegisterHashtable(SQLProfilerForm.string_0, hashtable);
		if (ex == null)
		{
			string text = hashtable[SQLProfilerForm.string_1].ToString();
			if (!string.IsNullOrEmpty(text))
			{
				string[] array = text.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length == this.listView1.Columns.Count)
				{
					try
					{
						for (int i = 0; i < array.Length; i++)
						{
							int num = int.Parse(array[i]);
							if (num > 0)
							{
								this.listView1.Columns[i].Width = num;
							}
						}
					}
					catch
					{
					}
				}
			}
			string text2 = hashtable[SQLProfilerForm.string_3].ToString();
			if (!string.IsNullOrEmpty(text2))
			{
				string[] array2 = text2.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				if (array2.Length == 4)
				{
					try
					{
						base.Left = int.Parse(array2[0]);
						base.Top = int.Parse(array2[1]);
						base.Height = int.Parse(array2[2]);
						base.Width = int.Parse(array2[3]);
					}
					catch
					{
					}
				}
			}
			try
			{
				int num2 = (int)hashtable[SQLProfilerForm.string_2];
				if (num2 > 0)
				{
					this.txtExecuteInfo.Height = num2;
				}
			}
			catch
			{
			}
		}
	}
	private void method_1()
	{
		StringBuilder stringBuilder = new StringBuilder();
		for (int i = 0; i < this.listView1.Columns.Count; i++)
		{
			stringBuilder.Append(this.listView1.Columns[i].Width.ToString()).Append(',');
		}
		if (stringBuilder.Length > 0)
		{
			stringBuilder.Remove(stringBuilder.Length - 1, 1);
		}
		Hashtable hashtable = new Hashtable();
		hashtable.Add(SQLProfilerForm.string_1, stringBuilder.ToString());
		hashtable.Add(SQLProfilerForm.string_2, this.txtExecuteInfo.Height);
		if (base.WindowState != FormWindowState.Minimized)
		{
			hashtable.Add(SQLProfilerForm.string_3, string.Format("{0},{1},{2},{3}", new object[]
			{
				base.Left,
				base.Top,
				base.Height,
				base.Width
			}));
		}
		Exception ex = RegisterHelper.SetRegistryKeyValue(SQLProfilerForm.string_0, hashtable);
		if (ex != null)
		{
			MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
	}
	private void SQLProfilerForm_Shown(object sender, EventArgs e)
	{
		ProfilerService.OnMessageReceive += new MessageReceive(this.method_2);
	}
	private void method_2(ExecuteInfo executeInfo_0)
	{
		SQLProfilerForm.Class0 @class = new SQLProfilerForm.Class0();
		@class.executeInfo_0 = executeInfo_0;
		@class.sqlprofilerForm_0 = this;
		if (this.bool_1)
		{
			this.synchronizationContext_0.Send(new SendOrPostCallback(@class.method_0), @class.executeInfo_0);
		}
	}
	private void method_3(ExecuteInfo executeInfo_0)
	{
		if (executeInfo_0.CommandType == (CommandType)0)
		{
			executeInfo_0.CommandType = CommandType.Text;
		}
		switch (executeInfo_0.InfoType)
		{
		case InfoType.OpenConnection:
			this.method_4(executeInfo_0);
			break;
		case InfoType.StartExecuteSP:
		case InfoType.StartExecuteSQL:
			this.method_5(executeInfo_0);
			break;
		case InfoType.ExecuteFinished:
		case InfoType.ExecuteFailed:
			this.method_6(executeInfo_0);
			break;
		}
	}
	private void method_4(ExecuteInfo executeInfo_0)
	{
		ListViewItem listViewItem = new ListViewItem();
		ListViewItem arg_20_0 = listViewItem;
		int num = ++this.int_5;
		arg_20_0.Text = num.ToString();
		listViewItem.ImageIndex = 3;
		listViewItem.SubItems.Add("<Open connection>");
		listViewItem.SubItems.Add(string.Empty);
		listViewItem.SubItems.Add(executeInfo_0.StartTime.ToString("HH:mm:ss.fff"));
		listViewItem.SubItems.Add(string.Empty);
		listViewItem.SubItems.Add(executeInfo_0.AppName);
		listViewItem.Tag = executeInfo_0;
		listViewItem.ForeColor = Color.Gainsboro;
		this.listView1.Items.Add(listViewItem);
		this.listView1.SelectedIndices.Clear();
		listViewItem.Selected = true;
		listViewItem.EnsureVisible();
	}
	private void method_5(ExecuteInfo executeInfo_0)
	{
		ListViewItem listViewItem = new ListViewItem();
		ListViewItem arg_20_0 = listViewItem;
		int num = ++this.int_5;
		arg_20_0.Text = num.ToString();
		listViewItem.ImageIndex = ((executeInfo_0.InfoType == InfoType.StartExecuteSP) ? 0 : 1);
		if (!string.IsNullOrEmpty(executeInfo_0.XmlCommandName))
		{
			listViewItem.SubItems.Add(executeInfo_0.XmlCommandName);
			listViewItem.ImageIndex = 4;
		}
		else
		{
			listViewItem.SubItems.Add(executeInfo_0.CommandText);
		}
		listViewItem.SubItems.Add(executeInfo_0.GetParameterValuesShowText());
		listViewItem.SubItems.Add(executeInfo_0.StartTime.ToString("HH:mm:ss.fff"));
		listViewItem.SubItems.Add(string.Empty);
		listViewItem.SubItems.Add(executeInfo_0.AppName);
		listViewItem.Tag = executeInfo_0;
		if (executeInfo_0.IsWithTranscation)
		{
			listViewItem.ForeColor = Color.Blue;
		}
		if (this.bool_0)
		{
			if (!string.IsNullOrEmpty(executeInfo_0.ExceptionMessage))
			{
				listViewItem.ImageIndex = 2;
				listViewItem.SubItems[4].Text = "执行失败";
				listViewItem.ForeColor = Color.Red;
			}
			else
			{
				TimeSpan timeSpan = executeInfo_0.EndTime.Value - executeInfo_0.StartTime;
				string text = string.Format("{0} ({1}.{2})", executeInfo_0.EndTime.Value.ToString("HH:mm:ss.fff"), timeSpan.Seconds, timeSpan.Milliseconds);
				listViewItem.SubItems[4].Text = text;
			}
		}
		this.listView1.Items.Add(listViewItem);
		this.listView1.SelectedIndices.Clear();
		listViewItem.Selected = true;
		listViewItem.EnsureVisible();
	}
	private void method_6(ExecuteInfo executeInfo_0)
	{
		for (int i = this.listView1.Items.Count - 1; i >= 0; i--)
		{
			ListViewItem listViewItem = this.listView1.Items[i];
			ExecuteInfo executeInfo = listViewItem.Tag as ExecuteInfo;
			if (executeInfo.InfoKey == executeInfo_0.InfoKey)
			{
				if (executeInfo_0.InfoType == InfoType.ExecuteFailed)
				{
					listViewItem.ImageIndex = 2;
					listViewItem.SubItems[4].Text = "执行失败";
					listViewItem.ForeColor = Color.Red;
					executeInfo.ExceptionMessage = executeInfo_0.ExceptionMessage;
				}
				else
				{
					TimeSpan timeSpan = executeInfo_0.EndTime.Value - executeInfo.StartTime;
					string text = string.Format("{0} ({1}.{2})", executeInfo_0.EndTime.Value.ToString("HH:mm:ss.fff"), timeSpan.Seconds, timeSpan.Milliseconds);
					listViewItem.SubItems[4].Text = text;
					executeInfo.EndTime = new DateTime?(executeInfo_0.EndTime.Value);
				}
				if (this.int_6 == listViewItem.Index)
				{
					this.listView1_SelectedIndexChanged(null, null);
				}
				return;
			}
		}
	}
	private void 监听消息ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		this.btnState.Text = this.监听消息ToolStripMenuItem.Text;
		this.btnState.Image = this.监听消息ToolStripMenuItem.Image;
		this.bool_1 = true;
	}
	private void 停止监听ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		this.btnState.Text = this.停止监听ToolStripMenuItem.Text;
		this.btnState.Image = this.停止监听ToolStripMenuItem.Image;
		this.bool_1 = false;
	}
	private void btnExit_Click(object sender, EventArgs e)
	{
		base.Close();
	}
	private void listView1_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (this.listView1.SelectedItems.Count == 0)
		{
			this.method_7();
		}
		else
		{
			ListViewItem listViewItem = this.listView1.SelectedItems[0];
			this.txtExecuteInfo.Text = this.method_8(listViewItem.Tag as ExecuteInfo);
			this.int_6 = listViewItem.Index;
		}
	}
	private void method_7()
	{
		this.int_6 = -1;
		this.txtExecuteInfo.Text = string.Empty;
	}
	private string method_8(ExecuteInfo executeInfo_0)
	{
		StringBuilder stringBuilder = new StringBuilder();
		stringBuilder.Append("ConnectionString: ").AppendLine(executeInfo_0.ConnectionString);
		stringBuilder.Append("StartTime: ").AppendLine(executeInfo_0.StartTime.ToString("yyyy-MM-dd HH:mm:ss.fff"));
		if (executeInfo_0.EndTime.HasValue)
		{
			TimeSpan timeSpan = executeInfo_0.EndTime.Value - executeInfo_0.StartTime;
			string value = string.Format("{0} ({1}.{2})", executeInfo_0.EndTime.Value.ToString("yyyy-MM-dd HH:mm:ss.fff"), timeSpan.Seconds, timeSpan.Milliseconds);
			stringBuilder.Append("EndTime:   ").AppendLine(value);
		}
		if (!string.IsNullOrEmpty(executeInfo_0.ExceptionMessage))
		{
			stringBuilder.Append("ExceptionMessage: ").AppendLine(executeInfo_0.ExceptionMessage);
		}
		if (executeInfo_0.ParameterValues != null && executeInfo_0.ParameterValues.Count > 0)
		{
			stringBuilder.AppendLine("Parameters:");
			foreach (ParamValuePair current in executeInfo_0.ParameterValues)
			{
				stringBuilder.AppendFormat("   {0} = ({1}) {2}\r\n", current.Name, current.Description, current.Value);
			}
		}
		if (!string.IsNullOrEmpty(executeInfo_0.CommandText))
		{
			stringBuilder.AppendLine();
			if (executeInfo_0.IsWithTranscation)
			{
				stringBuilder.Append("[Transcation] ");
			}
			if (!string.IsNullOrEmpty(executeInfo_0.XmlCommandName))
			{
				stringBuilder.Append("[XmlCommand] ");
			}
			else
			{
				if (executeInfo_0.CommandType == CommandType.StoredProcedure)
				{
					stringBuilder.Append("[StoredProcedure] ");
				}
			}
			stringBuilder.AppendLine("CommandText: ");
			stringBuilder.AppendLine("---------------------------------------------------------------------------------");
			stringBuilder.Append(executeInfo_0.CommandText.Replace("\n", "\r\n"));
		}
		return stringBuilder.ToString();
	}
	private void 删除选定项ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		while (this.listView1.SelectedIndices.Count > 0)
		{
			this.listView1.Items.RemoveAt(this.listView1.SelectedIndices[0]);
		}
		this.method_7();
	}
	private void 删除全部ToolStripMenuItem_Click(object sender, EventArgs e)
	{
		while (this.listView1.Items.Count > 0)
		{
			this.listView1.Items.RemoveAt(0);
		}
		this.method_7();
		this.int_5 = 0;
	}
	private void btnTopMost_CheckStateChanged(object sender, EventArgs e)
	{
		base.TopMost = this.btnTopMost.Checked;
		if (this.btnTopMost.Checked)
		{
            //this.btnTopMost.Image = Resources.lockWnd;
		}
		else
		{
            //this.btnTopMost.Image = Resources.unlockWnd;
		}
	}
	private void btnExportText_Click(object sender, EventArgs e)
	{
		if (this.listView1.Items.Count == 0)
		{
			MessageBox.Show("没有需要导出的记录。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "text files(*.txt)|*.txt";
            saveFileDialog.FileName = "FastDBEngineSQLProfiler_Message_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (ListViewItem listViewItem in this.listView1.Items)
				{
					stringBuilder.AppendLine("###").AppendLine(this.method_8(listViewItem.Tag as ExecuteInfo)).AppendLine("\r\n");
				}
				File.WriteAllText(saveFileDialog.FileName, stringBuilder.ToString(), Encoding.Unicode);
				MessageBox.Show("导出操作已成功完成。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
			}
		}
	}
	private void btnExportXml_Click(object sender, EventArgs e)
	{
		if (this.listView1.Items.Count == 0)
		{
			MessageBox.Show("没有需要导出的记录。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}
		else
		{
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Filter = "xml files(*.xml)|*.xml";
            saveFileDialog.FileName = "FastDBEngineSQLProfiler_Message_" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".xml";
			if (saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				List<ExecuteInfo> list = new List<ExecuteInfo>(this.listView1.Items.Count);
				foreach (ListViewItem listViewItem in this.listView1.Items)
				{
					list.Add(listViewItem.Tag as ExecuteInfo);
				}
				try
				{
					XmlHelper.XmlSerializeToFile(list, saveFileDialog.FileName, Encoding.UTF8);
					MessageBox.Show("导出操作已成功完成。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
				}
			}
		}
	}
	private void btnImportXml_Click(object sender, EventArgs e)
	{
		OpenFileDialog openFileDialog = new OpenFileDialog();
		openFileDialog.Filter = "xml files(*.xml)|*.xml";
		openFileDialog.CheckFileExists = true;
		if (openFileDialog.ShowDialog() == DialogResult.OK)
		{
			this.listView1.BeginUpdate();
			this.bool_0 = true;
			try
			{
				List<ExecuteInfo> list = XmlHelper.XmlDeserializeFromFile<List<ExecuteInfo>>(openFileDialog.FileName, Encoding.UTF8);
				this.listView1.Items.Clear();
				foreach (ExecuteInfo current in list)
				{
					this.method_3(current);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
			}
			finally
			{
				this.bool_0 = false;
				this.listView1.EndUpdate();
			}
		}
	}

}
