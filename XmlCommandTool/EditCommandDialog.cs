using FastDBEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using XmlCommandTool.Properties;

public class EditCommandDialog : Form
{
    private ToolStripButton btnAdd;
    private Button btnCancel;
    private ToolStripButton btnDelete;
    private ToolStripButton btnEdit;
    private Button btnImport;
    private Button btnOK;
    private ComboBox cboCommandType;
    private ColumnHeader columnHeader_0;
    private ColumnHeader columnHeader_1;
    private ColumnHeader columnHeader_2;
    private ColumnHeader columnHeader_3;
    private GroupBox groupBox1;
    private GroupBox groupBox2;
    private IContainer icontainer_0 = null;
    private ImageList imageList_0;
    private Label label1;
    private Label label2;
    private Label label3;
    private Label label4;
    private ListView listView1;
    private NumericUpDown nudTimeout;
    private Panel panel1;
    private Panel panel2;
    private Splitter splitter1;
    private static readonly string string_0 = "<XmlCommand Name=";
    private ToolStrip toolStrip1;
    private SyntaxHighlighterControl txtCode;
    private TextBox txtDataBase;
    private TextBox txtName;

    private void InitializeComponent()
    {
        this.icontainer_0 = new Container();
        this.panel1 = new Panel();
        this.groupBox2 = new GroupBox();
        this.listView1 = new ListView();
        this.columnHeader_0 = new ColumnHeader();
        this.columnHeader_1 = new ColumnHeader();
        this.columnHeader_2 = new ColumnHeader();
        this.columnHeader_3 = new ColumnHeader();
        this.imageList_0 = new ImageList(this.icontainer_0);
        this.toolStrip1 = new ToolStrip();
        this.btnAdd = new ToolStripButton();
        this.btnEdit = new ToolStripButton();
        this.btnDelete = new ToolStripButton();
        this.panel2 = new Panel();
        this.btnImport = new Button();
        this.btnCancel = new Button();
        this.btnOK = new Button();
        this.groupBox1 = new GroupBox();
        this.cboCommandType = new ComboBox();
        this.label3 = new Label();
        this.txtDataBase = new TextBox();
        this.label2 = new Label();
        this.txtName = new TextBox();
        this.label1 = new Label();
        this.splitter1 = new Splitter();
        this.txtCode = new SyntaxHighlighterControl();
        this.label4 = new Label();
        this.nudTimeout = new NumericUpDown();
        this.panel1.SuspendLayout();
        this.groupBox2.SuspendLayout();
        this.toolStrip1.SuspendLayout();
        this.panel2.SuspendLayout();
        this.groupBox1.SuspendLayout();
        this.nudTimeout.BeginInit();
        base.SuspendLayout();
        this.panel1.Controls.Add(this.groupBox2);
        this.panel1.Controls.Add(this.panel2);
        this.panel1.Controls.Add(this.groupBox1);
        this.panel1.Dock = DockStyle.Right;
        this.panel1.Location = new Point(0x296, 3);
        this.panel1.Name = "panel1";
        this.panel1.Size = new Size(0x189, 0x214);
        this.panel1.TabIndex = 0;
        this.groupBox2.Controls.Add(this.listView1);
        this.groupBox2.Controls.Add(this.toolStrip1);
        this.groupBox2.Dock = DockStyle.Fill;
        this.groupBox2.Location = new Point(0, 0x73);
        this.groupBox2.Name = "groupBox2";
        this.groupBox2.Size = new Size(0x189, 0x176);
        this.groupBox2.TabIndex = 2;
        this.groupBox2.TabStop = false;
        this.groupBox2.Text = "命令参数";
        this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1, this.columnHeader_2, this.columnHeader_3 });
        this.listView1.Dock = DockStyle.Fill;
        this.listView1.FullRowSelect = true;
        this.listView1.HideSelection = false;
        this.listView1.Location = new Point(3, 0x2a);
        this.listView1.MultiSelect = false;
        this.listView1.Name = "listView1";
        this.listView1.Size = new Size(0x183, 0x149);
        this.listView1.SmallImageList = this.imageList_0;
        this.listView1.TabIndex = 1;
        this.listView1.UseCompatibleStateImageBehavior = false;
        this.listView1.View = View.Details;
        this.listView1.ItemActivate += new EventHandler(this.btnEdit_Click);
        this.listView1.Resize += new EventHandler(this.listView1_Resize);
        this.columnHeader_0.Text = "Name";
        this.columnHeader_0.Width = 0x8e;
        this.columnHeader_1.Text = "Type";
        this.columnHeader_1.Width = 0x54;
        this.columnHeader_2.Text = "Direction";
        this.columnHeader_2.Width = 0x4e;
        this.columnHeader_3.Text = "Size";
        this.columnHeader_3.TextAlign = HorizontalAlignment.Right;
        this.columnHeader_3.Width = 0x37;
        this.imageList_0.ColorDepth = ColorDepth.Depth8Bit;
        this.imageList_0.ImageSize = new Size(0x10, 0x10);
        this.imageList_0.TransparentColor = Color.Transparent;
        this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.btnAdd, this.btnEdit, this.btnDelete });
        this.toolStrip1.Location = new Point(3, 0x11);
        this.toolStrip1.Name = "toolStrip1";
        this.toolStrip1.Size = new Size(0x183, 0x19);
        this.toolStrip1.TabIndex = 0;
        this.toolStrip1.Text = "toolStrip1";
        //this.btnAdd.Image = Resources.NewDocumentHS;
        this.btnAdd.ImageTransparentColor = Color.Magenta;
        this.btnAdd.Name = "btnAdd";
        this.btnAdd.Size = new Size(0x33, 0x16);
        this.btnAdd.Text = "新增";
        this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
        // this.btnEdit.Image = Resources.EditTableHS;
        this.btnEdit.ImageTransparentColor = Color.Magenta;
        this.btnEdit.Name = "btnEdit";
        this.btnEdit.Size = new Size(0x33, 0x16);
        this.btnEdit.Text = "修改";
        this.btnEdit.Click += new EventHandler(this.btnEdit_Click);
        //this.btnDelete.Image = Resources.DeleteHS;
        this.btnDelete.ImageTransparentColor = Color.Magenta;
        this.btnDelete.Name = "btnDelete";
        this.btnDelete.Size = new Size(0x33, 0x16);
        this.btnDelete.Text = "删除";
        this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
        this.panel2.Controls.Add(this.btnImport);
        this.panel2.Controls.Add(this.btnCancel);
        this.panel2.Controls.Add(this.btnOK);
        this.panel2.Dock = DockStyle.Bottom;
        this.panel2.Location = new Point(0, 0x1e9);
        this.panel2.Name = "panel2";
        this.panel2.Size = new Size(0x189, 0x2b);
        this.panel2.TabIndex = 1;
        this.btnImport.Location = new Point(7, 10);
        this.btnImport.Name = "btnImport";
        this.btnImport.Size = new Size(100, 0x17);
        this.btnImport.TabIndex = 2;
        this.btnImport.Text = "从剪切板导入";
        this.btnImport.UseVisualStyleBackColor = true;
        this.btnImport.Visible = false;
        this.btnImport.Click += new EventHandler(this.btnImport_Click);
        this.btnCancel.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        this.btnCancel.DialogResult = DialogResult.Cancel;
        this.btnCancel.Location = new Point(0x12d, 10);
        this.btnCancel.Name = "btnCancel";
        this.btnCancel.Size = new Size(0x4b, 0x17);
        this.btnCancel.TabIndex = 1;
        this.btnCancel.Text = "取消(&C)";
        this.btnCancel.UseVisualStyleBackColor = true;
        this.btnOK.Anchor = AnchorStyles.Right | AnchorStyles.Top;
        this.btnOK.Location = new Point(0xce, 10);
        this.btnOK.Name = "btnOK";
        this.btnOK.Size = new Size(0x4b, 0x17);
        this.btnOK.TabIndex = 0;
        this.btnOK.Text = "确定(&K)";
        this.btnOK.UseVisualStyleBackColor = true;
        this.btnOK.Click += new EventHandler(this.btnOK_Click);
        this.groupBox1.Controls.Add(this.nudTimeout);
        this.groupBox1.Controls.Add(this.label4);
        this.groupBox1.Controls.Add(this.cboCommandType);
        this.groupBox1.Controls.Add(this.label3);
        this.groupBox1.Controls.Add(this.txtDataBase);
        this.groupBox1.Controls.Add(this.label2);
        this.groupBox1.Controls.Add(this.txtName);
        this.groupBox1.Controls.Add(this.label1);
        this.groupBox1.Dock = DockStyle.Top;
        this.groupBox1.Location = new Point(0, 0);
        this.groupBox1.Name = "groupBox1";
        this.groupBox1.Size = new Size(0x189, 0x73);
        this.groupBox1.TabIndex = 0;
        this.groupBox1.TabStop = false;
        this.groupBox1.Text = "基本信息";
        this.cboCommandType.DropDownStyle = ComboBoxStyle.DropDownList;
        this.cboCommandType.FormattingEnabled = true;
        this.cboCommandType.Location = new Point(0x3e, 0x4c);
        this.cboCommandType.Name = "cboCommandType";
        this.cboCommandType.Size = new Size(0xae, 20);
        this.cboCommandType.TabIndex = 5;
        this.label3.AutoSize = true;
        this.label3.Location = new Point(10, 80);
        this.label3.Name = "label3";
        this.label3.Size = new Size(0x1d, 12);
        this.label3.TabIndex = 4;
        this.label3.Text = "类型";
        this.txtDataBase.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
        this.txtDataBase.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
        this.txtDataBase.Location = new Point(0x3e, 0x2d);
        this.txtDataBase.Name = "txtDataBase";
        this.txtDataBase.Size = new Size(0x13e, 0x15);
        this.txtDataBase.TabIndex = 3;
        this.label2.AutoSize = true;
        this.label2.Location = new Point(10, 50);
        this.label2.Name = "label2";
        this.label2.Size = new Size(0x29, 12);
        this.label2.TabIndex = 2;
        this.label2.Text = "数据库";
        this.txtName.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
        this.txtName.Font = new Font("Courier New", 9f, FontStyle.Regular, GraphicsUnit.Point, 0);
        this.txtName.Location = new Point(0x3e, 0x12);
        this.txtName.Name = "txtName";
        this.txtName.Size = new Size(0x13e, 0x15);
        this.txtName.TabIndex = 1;
        this.label1.AutoSize = true;
        this.label1.ForeColor = Color.Red;
        this.label1.Location = new Point(10, 0x17);
        this.label1.Name = "label1";
        this.label1.Size = new Size(0x23, 12);
        this.label1.TabIndex = 0;
        this.label1.Text = "名称*";
        this.splitter1.Dock = DockStyle.Right;
        this.splitter1.Location = new Point(0x28f, 3);
        this.splitter1.Name = "splitter1";
        this.splitter1.Size = new Size(7, 0x214);
        this.splitter1.TabIndex = 1;
        this.splitter1.TabStop = false;
        this.txtCode.Dock = DockStyle.Fill;
        this.txtCode.Location = new Point(3, 3);
        this.txtCode.Name = "txtCode";
        this.txtCode.method_6(false);
        this.txtCode.Size = new Size(0x28c, 0x214);
        this.txtCode.TabIndex = 2;
        this.label4.AutoSize = true;
        this.label4.Location = new Point(0xf3, 80);
        this.label4.Name = "label4";
        this.label4.Size = new Size(0x35, 12);
        this.label4.TabIndex = 6;
        this.label4.Text = "超时(秒)";
        this.nudTimeout.Font = new Font("Courier New", 9f);
        this.nudTimeout.Location = new Point(0x12d, 0x4c);
        int[] bits = new int[4];
        bits[0] = 0x186a0;
        this.nudTimeout.Maximum = new decimal(bits);
        this.nudTimeout.Name = "nudTimeout";
        this.nudTimeout.Size = new Size(0x4f, 0x15);
        this.nudTimeout.TabIndex = 7;
        bits = new int[4];
        bits[0] = 30;
        this.nudTimeout.Value = new decimal(bits);
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.CancelButton = this.btnCancel;
        base.ClientSize = new Size(0x422, 0x21a);
        base.Controls.Add(this.txtCode);
        base.Controls.Add(this.splitter1);
        base.Controls.Add(this.panel1);
        base.MinimizeBox = false;
        base.Name = "EditCommandDialog";
        base.Padding = new Padding(3);
        base.ShowIcon = false;
        base.ShowInTaskbar = false;
        base.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "新增命令";
        base.Load += new EventHandler(this.EditCommandDialog_Load);
        base.Shown += new EventHandler(this.EditCommandDialog_Shown);
        this.panel1.ResumeLayout(false);
        this.groupBox2.ResumeLayout(false);
        this.groupBox2.PerformLayout();
        this.toolStrip1.ResumeLayout(false);
        this.toolStrip1.PerformLayout();
        this.panel2.ResumeLayout(false);
        this.groupBox1.ResumeLayout(false);
        this.groupBox1.PerformLayout();
        this.nudTimeout.EndInit();
        base.ResumeLayout(false);
    }

    public EditCommandDialog()
    {
        this.InitializeComponent();
        foreach (string str in Enum.GetNames(typeof(CommandType)))
        {
            this.cboCommandType.Items.Add(str);
        }
        this.cboCommandType.SelectedIndex = 0;
    }

    private void btnAdd_Click(object sender, EventArgs e)
    {
        ParameterDialog dialog = new ParameterDialog();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            this.listView1.Items.Add(this.method_0(dialog.method_1()));
        }
    }

    private void btnDelete_Click(object sender, EventArgs e)
    {
        if (this.listView1.SelectedItems.Count != 0)
        {
            int index = this.listView1.SelectedIndices[0];
            int num2 = (index > 0) ? index : 0;
            this.listView1.Items.RemoveAt(index);
            if ((this.listView1.Items.Count > 0) && (num2 <= (this.listView1.Items.Count - 1)))
            {
                this.listView1.Items[num2].Selected = true;
            }
            this.listView1.Focus();
        }
    }

    private void btnEdit_Click(object sender, EventArgs e)
    {
        if (this.listView1.SelectedItems.Count != 0)
        {
            ListViewItem item = this.listView1.SelectedItems[0];
            ParameterDialog dialog = new ParameterDialog();
            dialog.method_2((XmlCmdParameter) item.Tag);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.method_1(item, dialog.method_1());
            }
        }
    }

    private void btnImport_Click(object sender, EventArgs e)
    {
        try
        {
            string text = Clipboard.GetText();
            if (!string.IsNullOrEmpty(text) && (text.IndexOf(string_0) >= 0))
            {
                XmlCommand command = XmlHelper.XmlDeserialize<XmlCommand>(text, Encoding.UTF8);
                if (command != null)
                {
                    this.method_5(command);
                }
            }
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        finally
        {
            this.btnImport.Visible = false;
        }
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (this.txtName.Text.Trim().Length == 0)
        {
            MessageBox.Show("命令名称不能为空。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.txtName.Focus();
        }
        else if (this.txtCode.method_3().Length == 0)
        {
            MessageBox.Show("命令代码不能为空。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.txtCode.Focus();
        }
        else
        {
            base.DialogResult = DialogResult.OK;
        }
    }

    private void EditCommandDialog_Load(object sender, EventArgs e)
    {
        this.imageList_0.Images.Add(Resources.Bitmap_1);
        if (this.txtName.Text.Length > 0)
        {
            this.Text = "编辑命令";
        }
        try
        {
            this.btnImport.Visible = Clipboard.GetText().IndexOf(string_0) >= 0;
        }
        catch
        {
        }
    }

    private void EditCommandDialog_Shown(object sender, EventArgs e)
    {
        if (this.txtName.Text.Length > 0)
        {
            this.txtCode.Focus();
        }
    }



    private void listView1_Resize(object sender, EventArgs e)
    {
        if (this.listView1.Width > (((((this.columnHeader_1.Width + this.columnHeader_2.Width) + this.columnHeader_3.Width) + SystemInformation.VerticalScrollBarWidth) + SystemInformation.BorderSize.Width) + 10))
        {
            try
            {
                this.columnHeader_0.Width = (((((this.listView1.Width - (SystemInformation.BorderSize.Width * 2)) - this.columnHeader_1.Width) - this.columnHeader_2.Width) - this.columnHeader_3.Width) - SystemInformation.VerticalScrollBarWidth) - 5;
            }
            catch
            {
            }
        }
    }

    private ListViewItem method_0(XmlCmdParameter xmlCmdParameter_0)
    {
        ListViewItem item = new ListViewItem(xmlCmdParameter_0.Name, 0);
        item.SubItems.Add(xmlCmdParameter_0.Type.ToString());
        item.SubItems.Add(xmlCmdParameter_0.Direction.ToString());
        item.SubItems.Add(xmlCmdParameter_0.Size.ToString());
        item.Tag = xmlCmdParameter_0;
        return item;
    }

    private void method_1(ListViewItem listViewItem_0, XmlCmdParameter xmlCmdParameter_0)
    {
        listViewItem_0.Text = xmlCmdParameter_0.Name;
        listViewItem_0.SubItems[1].Text = xmlCmdParameter_0.Type.ToString();
        listViewItem_0.SubItems[2].Text = xmlCmdParameter_0.Direction.ToString();
        listViewItem_0.SubItems[3].Text = xmlCmdParameter_0.Size.ToString();
        listViewItem_0.Tag = xmlCmdParameter_0;
    }

    private List<XmlCmdParameter> method_2()
    {
        List<XmlCmdParameter> list = new List<XmlCmdParameter>(this.listView1.Items.Count);
        for (int i = 0; i < this.listView1.Items.Count; i++)
        {
            list.Add((XmlCmdParameter) this.listView1.Items[i].Tag);
        }
        return list;
    }

    private void method_3(List<XmlCmdParameter> list_0)
    {
        if (list_0 != null)
        {
            this.listView1.Items.Clear();
            foreach (XmlCmdParameter parameter in list_0)
            {
                this.listView1.Items.Add(this.method_0(parameter));
            }
        }
    }

    public XmlCommand method_4()
    { 
        return new XmlCommand { CommandName = this.txtName.Text.Trim(), CommandText = this.txtCode.method_3(), Database = (this.txtDataBase.Text.Trim().Length == 0) ? null : this.txtDataBase.Text.Trim(), Parameters = this.method_2(), CommandType = (CommandType) Enum.Parse(typeof(CommandType), this.cboCommandType.Text), Timeout = (int) this.nudTimeout.Value };
    }

    public void method_5(XmlCommand xmlCommand_0)
    {
        if (xmlCommand_0 != null)
        {
            this.txtName.Text = xmlCommand_0.CommandName;
            this.txtCode.method_2((string) xmlCommand_0.CommandText);
            this.txtDataBase.Text = xmlCommand_0.Database;
            this.method_3(xmlCommand_0.Parameters);
            this.nudTimeout.Value = xmlCommand_0.Timeout;
            this.cboCommandType.Text = xmlCommand_0.CommandType.ToString();
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

