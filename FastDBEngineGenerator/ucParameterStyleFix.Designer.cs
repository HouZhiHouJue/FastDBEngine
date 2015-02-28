using System;
using System.Drawing;
using System.Windows.Forms;
namespace FastDBEngineGenerator
{
    partial class ucParameterStyleFix
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
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
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.rbtnAnonymous);
            base.Controls.Add(this.rbtnNamed);
            base.Controls.Add(this.label3);
            base.Name = "ucParameterStyle";
            base.Size = new Size(0x18d, 0x19);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private EventHandler OnCheckedChangedEvent;
        private Label label3;
        private RadioButton rbtnAnonymous;
        private RadioButton rbtnNamed;
    }
}
