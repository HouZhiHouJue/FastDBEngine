using FastDBEngineGenerator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class QueryDialog : Form
{
    private Button btnGenerate;
    private IContainer icontainer_0 = null;
    private Label label1;
    private Panel panel1;
    private Panel panel2;
    private Splitter splitter1;
    private string string_0;
    private string string_1;
    private SyntaxHighlighterControl txtCsCode;
    private SyntaxHighlighterControl txtSql;
    private UcCsClassStyle ucCsClassStyle1;
    private void InitializeComponent()
    {
        this.splitter1 = new Splitter();
        this.panel1 = new Panel();
        this.txtCsCode = new SyntaxHighlighterControl();
        this.panel2 = new Panel();
        this.btnGenerate = new Button();
        this.ucCsClassStyle1 = new UcCsClassStyle();
        this.label1 = new Label();
        this.txtSql = new SyntaxHighlighterControl();
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
        this.txtCsCode.method_0("cs");
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
        this.ucCsClassStyle1.method_0(new EventHandler(this.method_0));
        this.label1.Dock = DockStyle.Top;
        this.label1.Location = new Point(0, 0);
        this.label1.Name = "label1";
        this.label1.Padding = new Padding(7, 5, 0, 0);
        this.label1.Size = new Size(0x3f3, 0x19);
        this.label1.TabIndex = 3;
        this.label1.Text = "请输入一个查询语句： （注意：语句将会提交给SQLSERVER执行，建议不要执行修改数据的语句。）";
        this.txtSql.Dock = DockStyle.Top;
        this.txtSql.Location = new Point(0, 0x19);
        this.txtSql.Name = "txtSql";
        this.txtSql.method_6(false);
        this.txtSql.Size = new Size(0x3f3, 0x8d);
        this.txtSql.TabIndex = 0;
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
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
    public QueryDialog(string string_2, string string_3)
    {
        this.InitializeComponent();
        this.string_0 = string_2;
        this.string_1 = string_3;
    }

    private void btnGenerate_Click(object sender, EventArgs e)
    {
        if (this.txtSql.method_3().Trim().Length != 0)
        {
            try
            {
                string str = this.txtSql.method_3().Trim();
                List<Field> list = GeneratorDbHelper.GetFieldListFromReader(this.string_0, this.string_1, str);
                this.txtCsCode.SetText(GeneratorClassHelper.smethod_1("YourModelClassName", list, this.ucCsClassStyle1.method_2()));
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
    }

  

    private void method_0(object sender, EventArgs e)
    {
        if (this.txtCsCode.method_3().Length != 0)
        {
            this.btnGenerate_Click(null, null);
        }
    }

    private void QueryDialog_Shown(object sender, EventArgs e)
    {
        this.txtSql.SetText("SELECT  t.* from WEBSITE t");
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

