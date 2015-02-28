using FastDBEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace XmlCommandTool
{
    public partial class ShowCallCodeDialogFix : Form
    {

        public ShowCallCodeDialogFix(XmlCommand xmlCommand_1, string string_1)
        {
            this.InitializeComponent();
            this.xmlCommand_0 = xmlCommand_1;
            this.string_0 = string_1;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnCopyAll_Click(object sender, EventArgs e)
        {
            if (this.txtCode.GetText().Length > 0)
            {
                Clipboard.SetText(this.txtCode.GetText());
            }
            base.Close();
        }


        private void method_0(object sender, EventArgs e)
        {
            this.method_1();
        }

        private void method_1()
        {
            int num = GeneratorClassHelper.UnicodeCharLength(this.xmlCommand_0);
            this.txtCode.SetText(GeneratorClassHelper.CallXmlCommandMethodGenerator(this.xmlCommand_0, this.string_0, num, this.ucParameterStyle1.RadioIsChecked()));
        }

        private void ShowCallCodeDialog_Load(object sender, EventArgs e)
        {
            this.method_1();
        }
    }
}
