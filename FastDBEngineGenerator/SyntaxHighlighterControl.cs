using ICSharpCode.TextEditor;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

public class SyntaxHighlighterControl : UserControl
{
    private ContextMenuStrip contextMenuStrip1;
    private IContainer icontainer_0 = null;
    private ToolStripMenuItem menuCopy;
    private ToolStripMenuItem menuCopyAll;
    private ToolStripMenuItem menuCut;
    private ToolStripMenuItem menuDelete;
    private ToolStripMenuItem menuFind;
    private ToolStripMenuItem menuPaste;
    private ToolStripMenuItem menuRedo;
    private ToolStripMenuItem menuSelectAll;
    private ToolStripMenuItem menuUndo;
    private string string_0;
    private TextEditorControl textEditorControl1;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripSeparator toolStripMenuItem3;

    private void InitializeComponent()
    {
        this.icontainer_0 = new Container();
        this.textEditorControl1 = new TextEditorControl();
        this.contextMenuStrip1 = new ContextMenuStrip(this.icontainer_0);
        this.menuUndo = new ToolStripMenuItem();
        this.menuRedo = new ToolStripMenuItem();
        this.toolStripMenuItem1 = new ToolStripSeparator();
        this.menuCut = new ToolStripMenuItem();
        this.menuCopy = new ToolStripMenuItem();
        this.menuPaste = new ToolStripMenuItem();
        this.menuDelete = new ToolStripMenuItem();
        this.toolStripMenuItem2 = new ToolStripSeparator();
        this.menuSelectAll = new ToolStripMenuItem();
        this.toolStripMenuItem3 = new ToolStripSeparator();
        this.menuFind = new ToolStripMenuItem();
        this.menuCopyAll = new ToolStripMenuItem();
        this.contextMenuStrip1.SuspendLayout();
        base.SuspendLayout();
        this.textEditorControl1.BorderStyle = BorderStyle.Fixed3D;
        this.textEditorControl1.ContextMenuStrip = this.contextMenuStrip1;
        this.textEditorControl1.Dock = DockStyle.Fill;
        this.textEditorControl1.IsReadOnly = false;
        this.textEditorControl1.Location = new Point(0, 0);
        this.textEditorControl1.Name = "textEditorControl1";
        this.textEditorControl1.ShowLineNumbers = false;
        this.textEditorControl1.ShowVRuler = false;
        this.textEditorControl1.Size = new Size(0x199, 0xd9);
        this.textEditorControl1.TabIndex = 0;
        this.contextMenuStrip1.Items.AddRange(new ToolStripItem[] { this.menuUndo, this.menuRedo, this.toolStripMenuItem1, this.menuCut, this.menuCopy, this.menuPaste, this.menuDelete, this.toolStripMenuItem2, this.menuSelectAll, this.menuCopyAll, this.toolStripMenuItem3, this.menuFind });
        this.contextMenuStrip1.Name = "contextMenuStrip1";
        this.contextMenuStrip1.Size = new Size(0xc3, 0xf2);
        this.contextMenuStrip1.Opening += new CancelEventHandler(this.contextMenuStrip1_Opening);
        this.menuUndo.Name = "menuUndo";
        this.menuUndo.Size = new Size(0xc2, 0x16);
        this.menuUndo.Text = "撤消";
        this.menuUndo.Click += new EventHandler(this.menuUndo_Click);
        this.menuRedo.Name = "menuRedo";
        this.menuRedo.Size = new Size(0xc2, 0x16);
        this.menuRedo.Text = "重做";
        this.menuRedo.Click += new EventHandler(this.menuRedo_Click);
        this.toolStripMenuItem1.Name = "toolStripMenuItem1";
        this.toolStripMenuItem1.Size = new Size(0xbf, 6);
        this.menuCut.Name = "menuCut";
        this.menuCut.Size = new Size(0xc2, 0x16);
        this.menuCut.Text = "剪切";
        this.menuCut.Click += new EventHandler(this.menuCut_Click);
        this.menuCopy.Name = "menuCopy";
        this.menuCopy.Size = new Size(0xc2, 0x16);
        this.menuCopy.Text = "复制";
        this.menuCopy.Click += new EventHandler(this.menuCopy_Click);
        this.menuPaste.Name = "menuPaste";
        this.menuPaste.Size = new Size(0xc2, 0x16);
        this.menuPaste.Text = "粘贴";
        this.menuPaste.Click += new EventHandler(this.menuPaste_Click);
        this.menuDelete.Name = "menuDelete";
        this.menuDelete.Size = new Size(0xc2, 0x16);
        this.menuDelete.Text = "删除";
        this.menuDelete.Click += new EventHandler(this.menuDelete_Click);
        this.toolStripMenuItem2.Name = "toolStripMenuItem2";
        this.toolStripMenuItem2.Size = new Size(0xbf, 6);
        this.menuSelectAll.Name = "menuSelectAll";
        this.menuSelectAll.Size = new Size(0xc2, 0x16);
        this.menuSelectAll.Text = "全选";
        this.menuSelectAll.Click += new EventHandler(this.menuSelectAll_Click);
        this.toolStripMenuItem3.Name = "toolStripMenuItem3";
        this.toolStripMenuItem3.Size = new Size(0xbf, 6);
        this.menuFind.Name = "menuFind";
        this.menuFind.Size = new Size(0xc2, 0x16);
        this.menuFind.Text = "查找 ...";
        this.menuFind.Click += new EventHandler(this.menuFind_Click);
        this.menuCopyAll.Name = "menuCopyAll";
        this.menuCopyAll.Size = new Size(0xc2, 0x16);
        this.menuCopyAll.Text = "复制全部文本到剪切板";
        this.menuCopyAll.Click += new EventHandler(this.menuCopyAll_Click);
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.Controls.Add(this.textEditorControl1);
        base.Name = "SyntaxHighlighterControl";
        base.Size = new Size(0x199, 0xd9);
        this.contextMenuStrip1.ResumeLayout(false);
        base.ResumeLayout(false);
    }

    public SyntaxHighlighterControl()
    {
        this.InitializeComponent();
        if (!base.DesignMode)
        {
            this.textEditorControl1.Font = new Font("Courier New", 9.5f);
            this.method_6(true);
            this.method_0(this.method_1());
        }
    }

    private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
    {
        bool hasSomethingSelected = this.textEditorControl1.HasSomethingSelected;
        bool flag2 = this.method_5();
        this.menuUndo.Enabled = !flag2 && this.textEditorControl1.ActiveTextAreaControl.Document.UndoStack.CanUndo;
        this.menuRedo.Enabled = !flag2 && this.textEditorControl1.ActiveTextAreaControl.Document.UndoStack.CanRedo;
        this.menuCut.Enabled = !flag2 && hasSomethingSelected;
        this.menuCopy.Enabled = hasSomethingSelected;
        this.menuPaste.Enabled = !flag2 && Clipboard.ContainsText();
        this.menuDelete.Enabled = this.menuCut.Enabled;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.icontainer_0 != null))
        {
            this.icontainer_0.Dispose();
        }
        base.Dispose(disposing);
    }

   

    private void menuCopy_Click(object sender, EventArgs e)
    {
        this.textEditorControl1.ExecuteAction(Keys.Control | Keys.C);
    }

    private void menuCopyAll_Click(object sender, EventArgs e)
    {
        if (this.textEditorControl1.Text.Length > 0)
        {
            Clipboard.SetText(this.textEditorControl1.Text);
        }
    }

    private void menuCut_Click(object sender, EventArgs e)
    {
        this.textEditorControl1.ExecuteAction(Keys.Control | Keys.X);
    }

    private void menuDelete_Click(object sender, EventArgs e)
    {
        this.textEditorControl1.ExecuteAction(Keys.Delete);
    }

    private void menuFind_Click(object sender, EventArgs e)
    {
        this.textEditorControl1.ShowSearchTextDialog();
    }

    private void menuPaste_Click(object sender, EventArgs e)
    {
        this.textEditorControl1.ExecuteAction(Keys.Control | Keys.V);
    }

    private void menuRedo_Click(object sender, EventArgs e)
    {
        this.textEditorControl1.ExecuteAction(Keys.Control | Keys.Y);
    }

    private void menuSelectAll_Click(object sender, EventArgs e)
    {
        this.textEditorControl1.ExecuteAction(Keys.Control | Keys.A);
    }

    private void menuUndo_Click(object sender, EventArgs e)
    {
        this.textEditorControl1.ExecuteAction(Keys.Control | Keys.Z);
    }

    public void method_0(string string_1)
    {
        if (this.string_0 != string_1)
        {
            this.string_0 = string_1;
            this.textEditorControl1.SetLanguage(string_1);
        }
    }

    public string method_1()
    {
        return (string.IsNullOrEmpty(this.string_0) ? "sql" : this.string_0);
    }

    public void SetText(string string_1)
    {
        this.textEditorControl1.SetText(string_1);
    }

    public string method_3()
    {
        return this.textEditorControl1.Text;
    }

    public void method_4(string string_1)
    {
        this.textEditorControl1.SetText(string_1);
    }

    public bool method_5()
    {
        return this.textEditorControl1.IsReadOnly;
    }

    public void method_6(bool bool_0)
    {
        this.textEditorControl1.IsReadOnly = bool_0;
    }
}

