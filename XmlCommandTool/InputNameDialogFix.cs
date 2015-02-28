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
    public partial class InputNameDialogFix : Form
    {
        public InputNameDialogFix()
        {
            InitializeComponent();
        }

    private void btnOK_Click(object sender, EventArgs e)
    {
        if (this.method_0().Length == 0)
        {
            MessageBox.Show(this.label1.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            this.txtName.Focus();
        }
        else
        {
            base.DialogResult = DialogResult.OK;
        }
    }

  

    public string method_0()
    {
        return this.txtName.Text.Trim();
    }

    public void method_1(string string_0)
    {
        this.txtName.Text = string_0;
    }

    public static string smethod_0()
    {
        InputNameDialogFix dialog = new InputNameDialogFix();
        if (dialog.ShowDialog() == DialogResult.OK)
        {
            return dialog.method_0();
        }
        return null;
    }

    }
}
