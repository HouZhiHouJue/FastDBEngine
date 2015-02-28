using FastDBEngine;
using FastDBEngineProfilerLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace FastDBEngineSQLProfiler
{
    public partial class SQLProfilerFormFix : Form
    {
        public SQLProfilerFormFix()
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
            hashtable.Add(SQLProfilerFormFix.string_1, string.Empty);
            hashtable.Add(SQLProfilerFormFix.string_2, 0);
            hashtable.Add(SQLProfilerFormFix.string_3, string.Empty);
            Exception ex = RegisterHelper.RegisterHashtable(SQLProfilerFormFix.string_0, hashtable);
            if (ex == null)
            {
                string text = hashtable[SQLProfilerFormFix.string_1].ToString();
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
                string text2 = hashtable[SQLProfilerFormFix.string_3].ToString();
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
                    int num2 = (int)hashtable[SQLProfilerFormFix.string_2];
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
            hashtable.Add(SQLProfilerFormFix.string_1, stringBuilder.ToString());
            hashtable.Add(SQLProfilerFormFix.string_2, this.txtExecuteInfo.Height);
            if (base.WindowState != FormWindowState.Minimized)
            {
                hashtable.Add(SQLProfilerFormFix.string_3, string.Format("{0},{1},{2},{3}", new object[]
			{
				base.Left,
				base.Top,
				base.Height,
				base.Width
			}));
            }
            Exception ex = RegisterHelper.SetRegistryKeyValue(SQLProfilerFormFix.string_0, hashtable);
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
            SQLProfilerFormFix.Class0 @class = new SQLProfilerFormFix.Class0();
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
        [CompilerGenerated]
        private sealed class Class0
        {
            public SQLProfilerFormFix sqlprofilerForm_0;
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
    }
}
