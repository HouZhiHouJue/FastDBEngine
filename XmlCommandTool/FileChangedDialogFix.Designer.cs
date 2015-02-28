using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace XmlCommandTool
{
    partial class FileChangedDialogFix
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new Container();
            this.label1 = new Label();
            this.listView1 = new ListView();
            this.columnHeader_0 = new ColumnHeader();
            this.columnHeader_1 = new ColumnHeader();
            this.imageList_0 = new ImageList(this.components);
            this.btnNO = new Button();
            this.btnYES = new Button();
            this.label2 = new Label();
            this.textBox1 = new TextBox();
            this.btnOpen = new Button();
            base.SuspendLayout();
            this.label1.AutoSize = true;
            this.label1.Location = new Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new Size(0x1a3, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "XmlCommandTool 检测到以下文件被其它应用程序更新了，是否重新加载它们？";
            this.listView1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Top;
            this.listView1.Columns.AddRange(new ColumnHeader[] { this.columnHeader_0, this.columnHeader_1 });
            this.listView1.FullRowSelect = true;
            this.listView1.Location = new Point(13, 0x26);
            this.listView1.Name = "listView1";
            this.listView1.ShowItemToolTips = true;
            this.listView1.Size = new Size(0x25d, 0xc0);
            this.listView1.SmallImageList = this.imageList_0;
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = View.Details;
            this.columnHeader_0.Text = "文件名";
            this.columnHeader_0.Width = 0x18a;
            this.columnHeader_1.Text = "修改类别";
            this.columnHeader_1.Width = 140;
            this.imageList_0.ColorDepth = ColorDepth.Depth8Bit;
            this.imageList_0.ImageSize = new Size(0x10, 0x10);
            this.imageList_0.TransparentColor = Color.Transparent;
            this.btnNO.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnNO.Location = new Point(0x1ee, 310);
            this.btnNO.Name = "btnNO";
            this.btnNO.Size = new Size(0x7e, 0x17);
            this.btnNO.TabIndex = 6;
            this.btnNO.Text = "不，忽略所有修改";
            this.btnNO.UseVisualStyleBackColor = true;
            this.btnNO.Click += new EventHandler(this.btnNO_Click);
            this.btnYES.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnYES.Location = new Point(0x13c, 310);
            this.btnYES.Name = "btnYES";
            this.btnYES.Size = new Size(0xa1, 0x17);
            this.btnYES.TabIndex = 5;
            this.btnYES.Text = "是的，重新加载所有文件";
            this.btnYES.UseVisualStyleBackColor = true;
            this.btnYES.Click += new EventHandler(this.btnYES_Click);
            this.label2.Anchor = AnchorStyles.Left | AnchorStyles.Bottom;
            this.label2.AutoSize = true;
            this.label2.Location = new Point(13, 0xf9);
            this.label2.Name = "label2";
            this.label2.Size = new Size(0x209, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "为了防止文件更新冲突，您可以在下面的文本框内指定一个目录将当前所有节点的内容保存起来。";
            this.textBox1.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Bottom;
            this.textBox1.Location = new Point(13, 0x10c);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new Size(0x239, 0x15);
            this.textBox1.TabIndex = 8;
            this.btnOpen.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            this.btnOpen.FlatStyle = FlatStyle.Popup;
            // this.btnOpen.Image = Resources.openfolderHS;
            this.btnOpen.ImageAlign = ContentAlignment.BottomRight;
            this.btnOpen.Location = new Point(0x24f, 0x10a);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new Size(0x1b, 0x17);
            this.btnOpen.TabIndex = 9;
            this.btnOpen.UseVisualStyleBackColor = true;
            this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x278, 0x159);
            base.Controls.Add(this.btnOpen);
            base.Controls.Add(this.textBox1);
            base.Controls.Add(this.label2);
            base.Controls.Add(this.btnNO);
            base.Controls.Add(this.btnYES);
            base.Controls.Add(this.listView1);
            base.Controls.Add(this.label1);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "FileChangedDialog";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "文件修改通知";
            base.Load += new EventHandler(this.FileChangedDialog_Load);
            base.FormClosing += new FormClosingEventHandler(this.FileChangedDialog_FormClosing);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        #endregion

        private Button btnNO;
        private Button btnOpen;
        private Button btnYES;
        private ColumnHeader columnHeader_0;
        private ColumnHeader columnHeader_1;
        private ImageList imageList_0;
        private Label label1;
        private Label label2;
        private ListView listView1;
        private TextBox textBox1;
    }
}