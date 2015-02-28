using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace FastDBEngineSQLProfiler
{
    partial class SQLProfilerFormFix
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
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
            this.imageList_0 = new ImageList(this.components);
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
//            base.AutoScaleMode = AutoScaleMode.Font;
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

        #endregion

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
    }
}