using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class InputNameDialog : Form
{
    private Button btnCancel;
    private Button btnOK;
    private IContainer icontainer_0 = null;
    private Label label1;
    private TextBox txtName;

    public InputNameDialog()
    {
        this.InitializeComponent();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (this.method_0().Length == 0)
        {
            MessageBox.Show(this.label1.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.txtName.Focus();
        }
        else
        {
            base.DialogResult = DialogResult.OK;
        }
    }

   

    public string method_0()
    {
        return this.txtName.Text.Trim();
    }

    public void method_1(string string_0)
    {
        this.txtName.Text = string_0;
    }

    public static string smethod_0()
    {
        InputNameDialog dialog = new InputNameDialog();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            return dialog.method_0();
        }
        return null;
    }

    private void InitializeComponent()
    {
        this.label1 = new Label();
        this.txtName = new TextBox();
        this.btnCancel = new Button();
        this.btnOK = new Button();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new Point(15, 13);
        this.label1.Name = "label1";
        this.label1.Size = new Size(0x7d, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "请输入一个有效的名称";
        this.txtName.Location = new Point(15, 0x1d);
        this.txtName.Name = "txtName";
        this.txtName.Size = new Size(0x1ce, 0x15);
        this.txtName.TabIndex = 1;
        this.btnCancel.DialogResult = DialogResult.Cancel;
        this.btnCancel.Location = new Point(0x192, 0x41);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new Size(0x4b, 0x17);
        this.btnCancel.TabIndex = 6;
        this.btnCancel.Text = "取消";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnOK.Location = new Point(0x11a, 0x41);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new Size(0x4b, 0x17);
        this.btnOK.TabIndex = 5;
        this.btnOK.Text = "确定";
        this.btnOK.UseVisualStyleBackColor = true;
        this.btnOK.Click += new EventHandler(this.btnOK_Click);
        base.AcceptButton = this.btnOK;
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.CancelButton = this.btnCancel;
        base.ClientSize = new Size(500, 0x6b);
        base.Controls.Add(this.btnCancel);
        base.Controls.Add(this.btnOK);
        base.Controls.Add(this.txtName);
        base.Controls.Add(this.label1);
        base.FormBorderStyle = FormBorderStyle.FixedDialog;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "InputNameDialog";
        base.ShowInTaskbar = false;
        base.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "请输入名称";
        base.ResumeLayout(false);
        base.PerformLayout();
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

