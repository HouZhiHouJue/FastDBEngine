using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

public class ucParameterStyle : UserControl
{
    private EventHandler eventHandler_0;
    private IContainer icontainer_0 = null;
    private Label label3;
    private RadioButton rbtnAnonymous;
    private RadioButton rbtnNamed;

    public ucParameterStyle()
    {
        this.InitializeComponent();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing && (this.icontainer_0 != null))
        {
            this.icontainer_0.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.label3 = new Label();
        this.rbtnNamed = new RadioButton();
        this.rbtnAnonymous = new RadioButton();
        base.SuspendLayout();
        this.label3.AutoSize = true;
        this.label3.Location = new Point(5, 8);
        this.label3.Name = "label3";
        this.label3.Size = new Size(0x89, 12);
        this.label3.TabIndex = 12;
        this.label3.Text = "类型风格（优先采用）：";
        this.rbtnNamed.AutoSize = true;
        this.rbtnNamed.Location = new Point(0x9e, 6);
        this.rbtnNamed.Name = "rbtnNamed";
        this.rbtnNamed.Size = new Size(0x59, 0x10);
        this.rbtnNamed.TabIndex = 13;
        this.rbtnNamed.Text = "命名类型(&N)";
        this.rbtnNamed.UseVisualStyleBackColor = true;
        this.rbtnNamed.CheckedChanged += new EventHandler(this.rbtnNamed_CheckedChanged);
        this.rbtnAnonymous.AutoSize = true;
        this.rbtnAnonymous.Checked = true;
        this.rbtnAnonymous.Location = new Point(0x116, 6);
        this.rbtnAnonymous.Name = "rbtnAnonymous";
        this.rbtnAnonymous.Size = new Size(0x59, 0x10);
        this.rbtnAnonymous.TabIndex = 14;
        this.rbtnAnonymous.TabStop = true;
        this.rbtnAnonymous.Text = "匿名类型(&A)";
        this.rbtnAnonymous.UseVisualStyleBackColor = true;
        base.AutoScaleDimensions = new SizeF(6f, 12f);
        base.AutoScaleMode = AutoScaleMode.Font;
        base.Controls.Add(this.rbtnAnonymous);
        base.Controls.Add(this.rbtnNamed);
        base.Controls.Add(this.label3);
        base.Name = "ucParameterStyle";
        base.Size = new Size(0x18d, 0x19);
        base.ResumeLayout(false);
        base.PerformLayout();
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void method_0(EventHandler eventHandler_1)
    {
        this.eventHandler_0 = (EventHandler) Delegate.Combine(this.eventHandler_0, eventHandler_1);
    }

    [MethodImpl(MethodImplOptions.Synchronized)]
    public void method_1(EventHandler eventHandler_1)
    {
        this.eventHandler_0 = (EventHandler) Delegate.Remove(this.eventHandler_0, eventHandler_1);
    }

    public bool method_2()
    {
        return this.rbtnNamed.Checked;
    }

    private void rbtnNamed_CheckedChanged(object sender, EventArgs e)
    {
        if (this.eventHandler_0 != null)
        {
            this.eventHandler_0(sender, e);
        }
    }
}

