using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using XmlCommandTool.Properties;

public class OpenDirectoryDialog : Form
{
    private Button btnCancel;
    private Button btnOK;
    private Button btnOpen;
    private IContainer icontainer_0 = null;
    private Label label1;
    private TextBox txtPath;

    private void InitializeComponent()
    {
        this.label1 = new Label();
        this.txtPath = new TextBox();
        this.btnOpen = new Button();
        this.btnOK = new Button();
        this.btnCancel = new Button();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new Point(15, 0x11);
        this.label1.Name = "label1";
        this.label1.Size = new Size(0xb9, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "请输入或者选择要打开的目录路径";
        this.txtPath.Location = new Point(15, 0x2b);
        this.txtPath.Name = "txtPath";
        this.txtPath.Size = new Size(650, 0x15);
        this.txtPath.TabIndex = 1;
        this.btnOpen.FlatStyle = FlatStyle.Popup;
        //  this.btnOpen.Image = Resources.openfolderHS;
        this.btnOpen.ImageAlign = ContentAlignment.BottomRight;
        this.btnOpen.Location = new Point(0x29f, 0x2b);
        this.btnOpen.Name = "btnOpen";
        this.btnOpen.Size = new Size(0x1b, 0x17);
        this.btnOpen.TabIndex = 2;
        this.btnOpen.UseVisualStyleBackColor = true;
        this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
        this.btnOK.Location = new Point(470, 90);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new Size(0x4b, 0x17);
        this.btnOK.TabIndex = 3;
        this.btnOK.Text = "确定";
        this.btnOK.UseVisualStyleBackColor = true;
        this.btnOK.Click += new EventHandler(this.btnOK_Click);
        this.btnCancel.DialogResult = DialogResult.Cancel;
        this.btnCancel.Location = new Point(590, 90);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new Size(0x4b, 0x17);
        this.btnCancel.TabIndex = 4;
        this.btnCancel.Text = "取消";
        this.btnCancel.UseVisualStyleBackColor = true;
        base.AcceptButton = this.btnOK;
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.CancelButton = this.btnCancel;
        base.ClientSize = new Size(720, 0x8b);
        base.Controls.Add(this.btnCancel);
        base.Controls.Add(this.btnOK);
        base.Controls.Add(this.btnOpen);
        base.Controls.Add(this.txtPath);
        base.Controls.Add(this.label1);
        base.FormBorderStyle = FormBorderStyle.FixedDialog;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "OpenDirectoryDialog";
        base.ShowInTaskbar = false;
        base.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "打开目录对话框";
        base.Shown += new EventHandler(this.OpenDirectoryDialog_Shown);
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    public OpenDirectoryDialog()
    {
        this.InitializeComponent();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (this.txtPath.Text.Trim().Length == 0)
        {
            MessageBox.Show(this.label1.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.txtPath.Focus();
        }
        else
        {
            try
            {
                if (!Directory.Exists(this.txtPath.Text))
                {
                    MessageBox.Show("指定的目录不存在。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.txtPath.Focus();
                    return;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                return;
            }
            base.DialogResult = DialogResult.OK;
        }
    }

    private void btnOpen_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog dialog = new FolderBrowserDialog {
            Description = "请选择数据访问配置文件所在路径",
            ShowNewFolderButton = false
        };
        if (this.txtPath.Text.Length > 0)
        {
            dialog.SelectedPath = this.txtPath.Text;
        }
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            this.txtPath.Text = dialog.SelectedPath;
        }
    }

  

    public string method_0()
    {
        return this.txtPath.Text.Trim();
    }

    private void OpenDirectoryDialog_Shown(object sender, EventArgs e)
    {
        string path = RegistryHelper.GetValue("XmlCommandFilePath", string.Empty).ToString();
        try
        {
            if (Directory.Exists(path))
            {
                this.txtPath.Text = path;
            }
            else
            {
                string text = Clipboard.GetText();
                if (!string.IsNullOrEmpty(text) && Directory.Exists(text))
                {
                    this.txtPath.Text = text;
                }
            }
        }
        catch
        {
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
}

