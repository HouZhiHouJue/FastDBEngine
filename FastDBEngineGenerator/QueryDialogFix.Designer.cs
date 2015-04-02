using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace FastDBEngineGenerator
{
    partial class QueryDialogFix
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private Button btnGenerate;
        private IContainer components = null;
        private Label label1;
        private Panel panel1;
        private Panel panel2;
        private Splitter splitter1;
        private string connStr;
        private string configName;
        private SyntaxHighlighterControlFix txtCsCode;
        private SyntaxHighlighterControlFix txtSql;
        private UcCsClassStyleFix ucCsClassStyle1;
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
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCsCode = new FastDBEngineGenerator.SyntaxHighlighterControlFix();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnGenerate = new System.Windows.Forms.Button();
            this.ucCsClassStyle1 = new FastDBEngineGenerator.UcCsClassStyleFix();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSql = new FastDBEngineGenerator.SyntaxHighlighterControlFix();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitter1
            // 
            this.splitter1.Dock = System.Windows.Forms.DockStyle.Top;
            this.splitter1.Location = new System.Drawing.Point(0, 166);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(1011, 7);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.txtCsCode);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 173);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1011, 370);
            this.panel1.TabIndex = 2;
            // 
            // txtCsCode
            // 
            this.txtCsCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtCsCode.Location = new System.Drawing.Point(0, 32);
            this.txtCsCode.Name = "txtCsCode";
            this.txtCsCode.Size = new System.Drawing.Size(1011, 338);
            this.txtCsCode.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnGenerate);
            this.panel2.Controls.Add(this.ucCsClassStyle1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1011, 32);
            this.panel2.TabIndex = 0;
            // 
            // btnGenerate
            // 
            this.btnGenerate.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnGenerate.Location = new System.Drawing.Point(670, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new System.Drawing.Size(101, 23);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "生成代码(&G)";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
            // 
            // ucCsClassStyle1
            // 
            this.ucCsClassStyle1.Location = new System.Drawing.Point(3, 3);
            this.ucCsClassStyle1.Name = "ucCsClassStyle1";
            this.ucCsClassStyle1.Size = new System.Drawing.Size(622, 25);
            this.ucCsClassStyle1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(7, 5, 0, 0);
            this.label1.Size = new System.Drawing.Size(1011, 25);
            this.label1.TabIndex = 3;
            this.label1.Text = "请输入一个查询语句： （注意：语句将会提交给DB执行，建议不要执行修改数据的语句。）";
            // 
            // txtSql
            // 
            this.txtSql.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtSql.Location = new System.Drawing.Point(0, 25);
            this.txtSql.Name = "txtSql";
            this.txtSql.Size = new System.Drawing.Size(1011, 141);
            this.txtSql.TabIndex = 0;
            // 
            // QueryDialogFix
            // 
            this.ClientSize = new System.Drawing.Size(1011, 543);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.splitter1);
            this.Controls.Add(this.txtSql);
            this.Controls.Add(this.label1);
            this.MinimizeBox = false;
            this.Name = "QueryDialogFix";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "根据查询生成数据实体类";
            this.Shown += new System.EventHandler(this.QueryDialog_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

    }
}