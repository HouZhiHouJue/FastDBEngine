using FastDBEngine;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

public class ParameterDialog : Form
{
    private Button btnCancel;
    private Button btnOK;
    private ComboBox cboDbType;
    private ComboBox cboDirection;
    private IContainer icontainer_0 = null;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private NumericUpDown nudSize;
    private TextBox txtName;

    private void InitializeComponent()
    {
        this.label1 = new Label();
        this.label2 = new Label();
        this.label3 = new Label();
        this.label4 = new Label();
        this.txtName = new TextBox();
        this.cboDbType = new ComboBox();
        this.cboDirection = new ComboBox();
        this.nudSize = new NumericUpDown();
        this.btnCancel = new Button();
        this.btnOK = new Button();
        this.nudSize.BeginInit();
        base.SuspendLayout();
        this.label1.AutoSize = true;
        this.label1.Location = new Point(13, 12);
        this.label1.Name = "label1";
        this.label1.Size = new Size(0x1d, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "Name";
        this.label2.AutoSize = true;
        this.label2.Location = new Point(13, 0x26);
        this.label2.Name = "label2";
        this.label2.Size = new Size(0x29, 12);
        this.label2.TabIndex = 2;
        this.label2.Text = "DbType";
        this.label3.AutoSize = true;
        this.label3.Location = new Point(13, 0x42);
        this.label3.Name = "label3";
        this.label3.Size = new Size(0x3b, 12);
        this.label3.TabIndex = 4;
        this.label3.Text = "Direction";
        this.label4.AutoSize = true;
        this.label4.Location = new Point(13, 0x5d);
        this.label4.Name = "label4";
        this.label4.Size = new Size(0x1d, 12);
        this.label4.TabIndex = 6;
        this.label4.Text = "Size";
        this.txtName.Location = new Point(0x4f, 9);
        this.txtName.Name = "txtName";
        this.txtName.Size = new Size(0xfb, 0x15);
        this.txtName.TabIndex = 1;
        this.cboDbType.DrawMode = DrawMode.OwnerDrawFixed;
        this.cboDbType.DropDownStyle = ComboBoxStyle.DropDownList;
        this.cboDbType.FormattingEnabled = true;
        this.cboDbType.Location = new Point(0x4f, 0x23);
        this.cboDbType.MaxDropDownItems = 30;
        this.cboDbType.Name = "cboDbType";
        this.cboDbType.Size = new Size(0xfb, 0x16);
        this.cboDbType.TabIndex = 3;
        this.cboDbType.DrawItem += new DrawItemEventHandler(this.cboDbType_DrawItem);
        this.cboDirection.DropDownStyle = ComboBoxStyle.DropDownList;
        this.cboDirection.FormattingEnabled = true;
        this.cboDirection.Location = new Point(0x4f, 0x40);
        this.cboDirection.Name = "cboDirection";
        this.cboDirection.Size = new Size(0xfb, 20);
        this.cboDirection.TabIndex = 5;
        this.nudSize.Location = new Point(0x4f, 0x5c);
        int[] bits = new int[4];
        bits[0] = 0x2710;
        this.nudSize.Maximum = new decimal(bits);
        bits = new int[4];
        bits[0] = 10;
        bits[3] = -2147483648;
        this.nudSize.Minimum = new decimal(bits);
        this.nudSize.Name = "nudSize";
        this.nudSize.Size = new Size(0xfb, 0x15);
        this.nudSize.TabIndex = 7;
        this.btnCancel.DialogResult = DialogResult.Cancel;
        this.btnCancel.Location = new Point(0xff, 0x7d);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new Size(0x4b, 0x15);
        this.btnCancel.TabIndex = 9;
        this.btnCancel.Text = "取消";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnOK.Location = new Point(0x93, 0x7d);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new Size(0x4b, 0x15);
        this.btnOK.TabIndex = 8;
        this.btnOK.Text = "确定";
        this.btnOK.UseVisualStyleBackColor = true;
        this.btnOK.Click += new EventHandler(this.btnOK_Click);
        base.AcceptButton = this.btnOK;
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.CancelButton = this.btnCancel;
        base.ClientSize = new Size(0x16a, 0xa7);
        base.Controls.Add(this.btnCancel);
        base.Controls.Add(this.btnOK);
        base.Controls.Add(this.nudSize);
        base.Controls.Add(this.cboDirection);
        base.Controls.Add(this.cboDbType);
        base.Controls.Add(this.txtName);
        base.Controls.Add(this.label4);
        base.Controls.Add(this.label3);
        base.Controls.Add(this.label2);
        base.Controls.Add(this.label1);
        base.FormBorderStyle = FormBorderStyle.FixedDialog;
        base.MaximizeBox = false;
        base.MinimizeBox = false;
        base.Name = "ParameterDialog";
        base.ShowInTaskbar = false;
        base.StartPosition = FormStartPosition.Manual;
        this.Text = "设置命令参数";
        base.Load += new EventHandler(this.ParameterDialog_Load);
        this.nudSize.EndInit();
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    public ParameterDialog()
    {
        this.InitializeComponent();
        this.method_0();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        string str = this.txtName.Text.Trim();
        if ((str.Length == 0) || ((str.Length == 1) && !char.IsLetter(str[0])))
        {
            MessageBox.Show("参数Name不能为空。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.txtName.Focus();
        }
        else if (this.cboDbType.SelectedIndex < 0)
        {
            MessageBox.Show("DbType必须要选择。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.cboDbType.Focus();
        }
        else
        {
            base.DialogResult = DialogResult.OK;
        }
    }

    private void cboDbType_DrawItem(object sender, DrawItemEventArgs e)
    {
        e.DrawBackground();
        e.DrawFocusRectangle();
        if (e.Index >= 0)
        {
            string s = this.cboDbType.Items[e.Index].ToString();
            if ((((s == "DateTime") || (s == "Currency")) || ((s == "Int32") || (s == "String"))) || (s == "AnsiStringFixedLength"))
            {
                e.Graphics.DrawString(s, e.Font, Brushes.Red, e.Bounds);
            }
            else
            {
                e.Graphics.DrawString(s, e.Font, new SolidBrush(e.ForeColor), e.Bounds);
            }
        }
    }

   

    private void method_0()
    {
        string[] names = Enum.GetNames(typeof(DbType));
        Array.Sort<string>(names, StringComparer.OrdinalIgnoreCase);
        foreach (string str2 in names)
        {
            this.cboDbType.Items.Add(str2);
        }
        foreach (string str in Enum.GetNames(typeof(ParameterDirection)))
        {
            this.cboDirection.Items.Add(str);
        }
        this.cboDirection.SelectedIndex = 0;
        base.Location = new Point((SystemInformation.WorkingArea.Width - base.Width) / 2, 50);
    }

    public XmlCmdParameter method_1()
    {
        return new XmlCmdParameter { Name = this.txtName.Text.Trim(), Type = (DbType) Enum.Parse(typeof(DbType), this.cboDbType.Text), Direction = (ParameterDirection) Enum.Parse(typeof(ParameterDirection), this.cboDirection.Text), Size = Convert.ToInt32(this.nudSize.Value) };
    }

    public void method_2(XmlCmdParameter xmlCmdParameter_0)
    {
        try
        {
            this.txtName.Text = xmlCmdParameter_0.Name;
            this.cboDbType.Text = xmlCmdParameter_0.Type.ToString();
            this.cboDirection.Text = xmlCmdParameter_0.Direction.ToString();
            this.nudSize.Value = xmlCmdParameter_0.Size;
        }
        catch
        {
        }
    }

    private void ParameterDialog_Load(object sender, EventArgs e)
    {
        if (this.txtName.Text.Trim().Length == 0)
        {
            this.txtName.Text = ":";
            this.txtName.SelectionStart = 1;
            this.txtName.Focus();
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

