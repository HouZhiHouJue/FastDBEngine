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
        private string string_0;
        private string string_1;
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
            this.splitter1 = new Splitter();
            this.panel1 = new Panel();
            this.txtCsCode = new SyntaxHighlighterControlFix();
            this.panel2 = new Panel();
            this.btnGenerate = new Button();
            this.ucCsClassStyle1 = new UcCsClassStyleFix();
            this.label1 = new Label();
            this.txtSql = new SyntaxHighlighterControlFix();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            base.SuspendLayout();
            this.splitter1.Dock = DockStyle.Top;
            this.splitter1.Location = new Point(0, 0xa6);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new Size(0x3f3, 7);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            this.panel1.Controls.Add(this.txtCsCode);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = DockStyle.Fill;
            this.panel1.Location = new Point(0, 0xad);
            this.panel1.Name = "panel1";
            this.panel1.Size = new Size(0x3f3, 370);
            this.panel1.TabIndex = 2;
            this.txtCsCode.Dock = DockStyle.Fill;
            this.txtCsCode.SetLanguage("cs");
            this.txtCsCode.Location = new Point(0, 0x20);
            this.txtCsCode.Name = "txtCsCode";
            this.txtCsCode.Size = new Size(0x3f3, 0x152);
            this.txtCsCode.TabIndex = 1;
            this.panel2.Controls.Add(this.btnGenerate);
            this.panel2.Controls.Add(this.ucCsClassStyle1);
            this.panel2.Dock = DockStyle.Top;
            this.panel2.Location = new Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new Size(0x3f3, 0x20);
            this.panel2.TabIndex = 0;
            this.btnGenerate.FlatStyle = FlatStyle.Popup;
            this.btnGenerate.Location = new Point(0x1e5, 4);
            this.btnGenerate.Name = "btnGenerate";
            this.btnGenerate.Size = new Size(0x65, 0x17);
            this.btnGenerate.TabIndex = 1;
            this.btnGenerate.Text = "生成代码(&G)";
            this.btnGenerate.UseVisualStyleBackColor = true;
            this.btnGenerate.Click += new EventHandler(this.btnGenerate_Click);
            this.ucCsClassStyle1.Location = new Point(3, 3);
            this.ucCsClassStyle1.Name = "ucCsClassStyle1";
            this.ucCsClassStyle1.Size = new Size(0x18d, 0x19);
            this.ucCsClassStyle1.TabIndex = 0;
            this.ucCsClassStyle1.AddEvent(new EventHandler(this.btnGenerateClickEvent));
            this.label1.Dock = DockStyle.Top;
            this.label1.Location = new Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Padding = new Padding(7, 5, 0, 0);
            this.label1.Size = new Size(0x3f3, 0x19);
            this.label1.TabIndex = 3;
            this.label1.Text = "请输入一个查询语句： （注意：语句将会提交给DB执行，建议不要执行修改数据的语句。）";
            this.txtSql.Dock = DockStyle.Top;
            this.txtSql.Location = new Point(0, 0x19);
            this.txtSql.Name = "txtSql";
            this.txtSql.SetReadOnly(false);
            this.txtSql.Size = new Size(0x3f3, 0x8d);
            this.txtSql.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x3f3, 0x21f);
            base.Controls.Add(this.panel1);
            base.Controls.Add(this.splitter1);
            base.Controls.Add(this.txtSql);
            base.Controls.Add(this.label1);
            base.MinimizeBox = false;
            base.Name = "QueryDialog";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "根据查询生成数据实体类";
            base.Shown += new EventHandler(this.QueryDialog_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        #endregion
    }
}