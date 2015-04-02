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
            this.chkWCF = new System.Windows.Forms.CheckBox();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.chkSortByName = new System.Windows.Forms.CheckBox();
            this.ckEF = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // chkWCF
            // 
            this.chkWCF.AutoSize = true;
            this.chkWCF.Location = new System.Drawing.Point(347, 5);
            this.chkWCF.Name = "chkWCF";
            this.chkWCF.Size = new System.Drawing.Size(42, 16);
            this.chkWCF.TabIndex = 15;
            this.chkWCF.Text = "&WCF";
            this.chkWCF.UseVisualStyleBackColor = true;
            this.chkWCF.CheckedChanged += new System.EventHandler(this.chkSortByName_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Location = new System.Drawing.Point(260, 5);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(71, 16);
            this.radioButton3.TabIndex = 14;
            this.radioButton3.Text = "&Property";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.chkSortByName_CheckedChanged);
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(143, 5);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(101, 16);
            this.radioButton2.TabIndex = 13;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "&Auto Property";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.chkSortByName_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(74, 5);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(53, 16);
            this.radioButton1.TabIndex = 12;
            this.radioButton1.Text = "&Field";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.chkSortByName_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 11;
            this.label3.Text = "代码风格";
            // 
            // chkSortByName
            // 
            this.chkSortByName.AutoSize = true;
            this.chkSortByName.Location = new System.Drawing.Point(410, 5);
            this.chkSortByName.Name = "chkSortByName";
            this.chkSortByName.Size = new System.Drawing.Size(84, 16);
            this.chkSortByName.TabIndex = 16;
            this.chkSortByName.Text = "按名称排序";
            this.chkSortByName.UseVisualStyleBackColor = true;
            this.chkSortByName.CheckedChanged += new System.EventHandler(this.chkSortByName_CheckedChanged);
            // 
            // ckEF
            // 
            this.ckEF.AutoSize = true;
            this.ckEF.Checked = true;
            this.ckEF.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckEF.Location = new System.Drawing.Point(515, 4);
            this.ckEF.Name = "ckEF";
            this.ckEF.Size = new System.Drawing.Size(36, 16);
            this.ckEF.TabIndex = 17;
            this.ckEF.Text = "EF";
            this.ckEF.UseVisualStyleBackColor = true;
            // 
            // UcCsClassStyleFix
            // 
            this.Controls.Add(this.ckEF);
            this.Controls.Add(this.chkSortByName);
            this.Controls.Add(this.chkWCF);
            this.Controls.Add(this.radioButton3);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label3);
            this.Name = "UcCsClassStyleFix";
            this.Size = new System.Drawing.Size(601, 25);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox chkSortByName;
        private CheckBox chkWCF;
        private EventHandler OnchkSortByNameCheckedChanged;
        private Label label3;
        private RadioButton radioButton1;
        private RadioButton radioButton2;
        private RadioButton radioButton3;
        private CheckBox ckEF;
    }
}
