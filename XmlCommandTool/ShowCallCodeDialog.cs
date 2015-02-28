using FastDBEngine;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class ShowCallCodeDialog : Form
{
    private Button btnClose;
    private Button btnCopyAll;
    private IContainer icontainer_0 = null;
    private Panel panel1;
    private string string_0;
    private SyntaxHighlighterControl txtCode;
    private ucParameterStyle ucParameterStyle1;
    private XmlCommand xmlCommand_0;

    private void InitializeComponent()
    {
        this.panel1 = new Panel();
        this.btnClose = new Button();
        this.btnCopyAll = new Button();
        this.ucParameterStyle1 = new ucParameterStyle();
        this.txtCode = new SyntaxHighlighterControl();
        this.panel1.SuspendLayout();
        base.SuspendLayout();
        this.panel1.Controls.Add(this.btnClose);
        this.panel1.Controls.Add(this.btnCopyAll);
        this.panel1.Dock = DockStyle.Bottom;
        this.panel1.Location = new Point(0, 0x1e1);
        this.panel1.Name = "panel1";
        this.panel1.Size = new Size(0x3b1, 0x29);
        this.panel1.TabIndex = 1;
        this.btnClose.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        this.btnClose.DialogResult = DialogResult.Cancel;
        this.btnClose.Location = new Point(0x34f, 8);
        this.btnClose.Name = "btnClose";
        this.btnClose.Size = new Size(0x4b, 0x17);
        this.btnClose.TabIndex = 1;
        this.btnClose.Text = "关闭(&C)";
        this.btnClose.UseVisualStyleBackColor = true;
        this.btnClose.Click += new EventHandler(this.btnClose_Click);
        this.btnCopyAll.FlatStyle = FlatStyle.Popup;
        this.btnCopyAll.Location = new Point(12, 8);
        this.btnCopyAll.Name = "btnCopyAll";
        this.btnCopyAll.Size = new Size(0xfb, 0x17);
        this.btnCopyAll.TabIndex = 0;
        this.btnCopyAll.Text = "复制全部代码到剪切板，并关闭窗口";
        this.btnCopyAll.UseVisualStyleBackColor = true;
        this.btnCopyAll.Click += new EventHandler(this.btnCopyAll_Click);
        this.ucParameterStyle1.Dock = DockStyle.Top;
        this.ucParameterStyle1.Location = new Point(0, 0);
        this.ucParameterStyle1.Name = "ucParameterStyle1";
        this.ucParameterStyle1.Size = new Size(0x3b1, 0x19);
        this.ucParameterStyle1.TabIndex = 2;
        this.ucParameterStyle1.method_0(new EventHandler(this.method_0));
        this.txtCode.Dock = DockStyle.Fill;
        this.txtCode.method_0("cs");
        this.txtCode.Location = new Point(0, 0x19);
        this.txtCode.Name = "txtCode";
        this.txtCode.Size = new Size(0x3b1, 0x1c8);
        this.txtCode.TabIndex = 3;
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.CancelButton = this.btnClose;
        base.ClientSize = new Size(0x3b1, 0x20a);
        base.Controls.Add(this.txtCode);
        base.Controls.Add(this.ucParameterStyle1);
        base.Controls.Add(this.panel1);
        base.MinimizeBox = false;
        base.Name = "ShowCallCodeDialog";
        base.ShowIcon = false;
        base.ShowInTaskbar = false;
        base.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "自动生成的调用代码";
        base.Load += new EventHandler(this.ShowCallCodeDialog_Load);
        this.panel1.ResumeLayout(false);
        base.ResumeLayout(false);
    }


    public ShowCallCodeDialog(XmlCommand xmlCommand_1, string string_1)
    {
        this.InitializeComponent();
        this.xmlCommand_0 = xmlCommand_1;
        this.string_0 = string_1;
    }

    private void btnClose_Click(object sender, EventArgs e)
    {
        base.Close();
    }

    private void btnCopyAll_Click(object sender, EventArgs e)
    {
        if (this.txtCode.method_3().Length > 0)
        {
            Clipboard.SetText(this.txtCode.method_3());
        }
        base.Close();
    }

 
    private void method_0(object sender, EventArgs e)
    {
        this.method_1();
    }

    private void method_1()
    {
        int num = GeneratorClassHelper.UnicodeCharLength(this.xmlCommand_0);
        this.txtCode.method_2(GeneratorClassHelper.CallXmlCommandMethodGenerator(this.xmlCommand_0, this.string_0, num, this.ucParameterStyle1.method_2()));
    }

    private void ShowCallCodeDialog_Load(object sender, EventArgs e)
    {
        this.method_1();
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

