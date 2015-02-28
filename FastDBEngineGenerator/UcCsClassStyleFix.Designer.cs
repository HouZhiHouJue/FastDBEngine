using System;
using System.Drawing;
using System.Windows.Forms;
namespace FastDBEngineGenerator
{
    partial class UcCsClassStyleFix
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
            this.chkWCF = new CheckBox();
            this.radioButton3 = new RadioButton();
            this.radioButton2 = new RadioButton();
            this.radioButton1 = new RadioButton();
            this.label3 = new Label();
            this.chkSortByName = new CheckBox();
            base.SuspendLayout();
            this.chkWCF.AutoSize = true;
            this.chkWCF.Location = new Point(0x15b, 5);
            this.chkWCF.Name = "chkWCF";
            this.chkWCF.Size = new Size(0x2a, 0x10);
            this.chkWCF.TabIndex = 15;
            this.chkWCF.Text = "&WCF";
            this.chkWCF.UseVisualStyleBackColor = true;
            this.chkWCF.CheckedChanged += new EventHandler(this.chkSortByName_CheckedChanged);
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new Point(260, 5);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new Size(0x47, 0x10);
            this.radioButton3.TabIndex = 14;
            this.radioButton3.Text = "&Property";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new EventHandler(this.chkSortByName_CheckedChanged);
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new Point(0x8f, 5);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new Size(0x65, 0x10);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "&Auto Property";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new EventHandler(this.chkSortByName_CheckedChanged);
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new Point(0x4a, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new Size(0x35, 0x10);
            this.radioButton1.TabIndex = 12;
            this.radioButton1.Text = "&Field";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new EventHandler(this.chkSortByName_CheckedChanged);
            this.label3.AutoSize = true;
            this.label3.Location = new Point(5, 8);
            this.label3.Name = "label3";
            this.label3.Size = new Size(0x35, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "代码风格";
            this.chkSortByName.AutoSize = true;
            this.chkSortByName.Location = new Point(410, 5);
            this.chkSortByName.Name = "chkSortByName";
            this.chkSortByName.Size = new Size(0x54, 0x10);
            this.chkSortByName.TabIndex = 0x10;
            this.chkSortByName.Text = "按名称排序";
            this.chkSortByName.UseVisualStyleBackColor = true;
            this.chkSortByName.CheckedChanged += new EventHandler(this.chkSortByName_CheckedChanged);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.Controls.Add(this.chkSortByName);
            base.Controls.Add(this.chkWCF);
            base.Controls.Add(this.radioButton3);
            base.Controls.Add(this.radioButton2);
            base.Controls.Add(this.radioButton1);
            base.Controls.Add(this.label3);
            base.Name = "UcCsClassStyle";
            base.Size = new Size(0x1f6, 0x19);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private CheckBox chkSortByName;
        private CheckBox chkWCF;
        private EventHandler OnchkSortByNameCheckedChanged;
        private Label label3;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
    }
}
