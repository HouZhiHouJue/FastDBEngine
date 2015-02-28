using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FastDBEngineGenerator
{
    public partial class InputNameDialogFix : Form
    {
        public InputNameDialogFix()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.GetText().Length == 0)
            {
                MessageBox.Show(this.label1.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.txtName.Focus();
            }
            else
            {
                base.DialogResult = DialogResult.OK;
            }
        }



        public string GetText()
        {
            return this.txtName.Text.Trim();
        }

        public void SetText(string text)
        {
            this.txtName.Text = text;
        }

        public static string GetDialogText()
        {
            InputNameDialogFix dialog = new InputNameDialogFix();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                return dialog.GetText();
            }
            return null;
        }
    }
}
