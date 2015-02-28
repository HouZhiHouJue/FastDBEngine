using FastDBEngine;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace FastDBEngineOracleDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            System.Diagnostics.Trace.WriteLine("1");
            Register();
        }

        void Register()
        {
            string connection = System.Configuration.ConfigurationManager.AppSettings["oracle"];
            DbContextDefaultSetting.AutoRetrieveOutputValues = true;
            BuildManager.OnBuildException += new BuildExceptionHandler(BuildManager_OnBuildException);
            DbContext.RegisterDbConnectionInfo("oracle", "Oracle.DataAccess.Client", ":", connection);
            BuildManager.StartAutoCompile(() => BuildManager.RequestCount > 0 || BuildManager.WaitTypesCount > 0);
            string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XmlCommand");
            XmlCommandManager.LoadCommnads(path);
            Profiler.ApplicationName = "FastDBEngineDemo";
            Profiler.TryStartFastDBEngineProfiler();
        }

        static void BuildManager_OnBuildException(Exception ex)
        {
            CompileException ce = ex as CompileException;
            if (ce != null)
                SafeLogException(ce.GetDetailMessages());
            else
                // 未知的异常类型
                SafeLogException(ex.ToString());
        }

        public static void SafeLogException(string message)
        {
            try
            {
                string logfilePath = Path.Combine(HttpRuntime.AppDomainAppPath, @"App_Data\ErrorLog.txt");

                File.AppendAllText(logfilePath, "=========================================\r\n" + message, System.Text.Encoding.UTF8);
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connection = System.Configuration.ConfigurationManager.AppSettings["oracle"];
            Oracle.DataAccess.Client.OracleConnection conn = new Oracle.DataAccess.Client.OracleConnection(connection);
            conn.Open();
            Oracle.DataAccess.Client.OracleCommand cmd = new Oracle.DataAccess.Client.OracleCommand("select * from WEBSITE", conn);
            //cmd.CommandText="select * from WEBSITE";
            //Oracle.DataAccess.Client.OracleDataAdapter da = new Oracle.DataAccess.Client.OracleDataAdapter(cmd);
            //System.Data.DataTable dt = new DataTable();
            //da.Fill(dt);
            Oracle.DataAccess.Client.OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int value = (System.Int32)reader["SORTCODE"];

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            st.Start();
            //List<Model.WEBSITE> list4 = DbHelper.FillList<Model.WEBSITE>("select * from WEBSITE ", null);
            using (DbContext dbContext = new DbContext("oracle"))
            {
                List<Model.WEBSITE> list = FastDBEngine.DbHelper.FillList<Model.WEBSITE>(
                  "select * from WEBSITE", null, dbContext, FastDBEngine.CommandKind.SqlTextNoParams);
                List<WEBSITE> list2 = DbHelper.FillList<WEBSITE>(
                    "select * from WEBSITE where ID = :Id",
                     new { Id = 387 },
                     dbContext,
                     CommandKind.SqlTextWithParams);
                var query = "select * from WEBSITE where ID =".AsCPQuery(true);
                query += 387;
                List<WEBSITE> list7 = DbHelper.FillList<WEBSITE>(query);
            }


            List<Model.WEBSITE> list3 = DbHelper.FillList<Model.WEBSITE>("SelectWebsite", new { Id = 387 });
            var parameters = new WebsiteParameters { P_WEBSITETYPe = "境外", P_RESULT = 0 };
            int result = DbHelper.ExecuteNonQuery("PKG_SPIDERCONFIG.PRO_UPDATEWEBSITE", parameters);
            //var parameters2 = new ConfigParamters { P_REGUALRID = 5684 };
            //DbHelper.ExecuteNonQuery("PKG_SPIDERCONFIG.PRO_GETCONFIG_RULE", parameters2);
            st.Stop();
            MessageBox.Show(st.ElapsedMilliseconds.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DbContext.RegisterDbConnectionInfo("sqlserver", "System.Data.SqlClient", "@", "Password=fx1988fx;Persist Security Info=True;User ID=sa;Initial Catalog=MyNorthwind;Data Source=127.0.0.1");
            using (DbContext dbContext = new DbContext("sqlserver"))
            {
                var parameters = new { TopN = 2 };
                List<NestDemoItem> list1 = DbHelper.FillList<NestDemoItem>("GetMostValuableCustomers", parameters, dbContext);
            }
        }
    }

    public class WebsiteParameters
    {
        public string P_WEBSITETYPe { get; set; }
        public int P_RESULT { get; set; }
    }

    class ConfigParamters
    {
        public int P_REGUALRID { get; set; }
        public DataTable P_CUR1 { get; set; }
        public DataTable P_CUR2 { get; set; }
    }
}
