﻿using System;
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

        public QueryDialogFix(string connStr, string configName)
        {
            this.InitializeComponent();
            this.connStr = connStr;
            this.configName = configName;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (this.txtSql.GetText().Trim().Length != 0)
            {
                try
                {
                    string str = this.txtSql.GetText().Trim();
                    List<Field> list = GeneratorDbHelper.GetFieldListFromReader(this.connStr, this.configName, str);
                    this.txtCsCode.SetLanguage("C#");
                    this.txtCsCode.SetText(GeneratorClassHelper.GenerateModel("YourModelClassName", list, this.ucCsClassStyle1.GetCsClassStyle(), Util.GetConnUserName(connStr)));
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
