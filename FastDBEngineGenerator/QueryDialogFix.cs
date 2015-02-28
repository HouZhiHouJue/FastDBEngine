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
    public partial class QueryDialogFix : Form
    {
        public QueryDialogFix()
        {
            InitializeComponent();
        }

        public QueryDialogFix(string string_2, string string_3)
        {
            this.InitializeComponent();
            this.string_0 = string_2;
            this.string_1 = string_3;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (this.txtSql.GetText().Trim().Length != 0)
            {
                try
                {
                    string str = this.txtSql.GetText().Trim();
                    List<Field> list = GeneratorDbHelper.GetFieldListFromReader(this.string_0, this.string_1, str);
                    this.txtCsCode.SetText(GeneratorClassHelper.GenerateModel("YourModelClassName", list, this.ucCsClassStyle1.GetCsClassStyle()));
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
                }
            }
        }



        private void btnGenerateClickEvent(object sender, EventArgs e)
        {
            if (this.txtCsCode.GetText().Length != 0)
            {
                this.btnGenerate_Click(null, null);
            }
        }

        private void QueryDialog_Shown(object sender, EventArgs e)
        {
           // this.txtSql.SetText("SELECT  t.* from WEBSITE t");
        }
    }
}
