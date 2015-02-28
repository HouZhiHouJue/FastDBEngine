using FastDBEngine;
using FastDBEngineOracleDemo.DTO;
using FastDBEngineOracleDemo.Model;
using Model;
using Oracle.ManagedDataAccess.Client;
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
    public partial class FrmMain : Form
    {
        public FrmMain()
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
            DbContext.RegisterDbConnectionInfo("oracle", "Oracle.ManagedDataAccess.Client", ":", connection);
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
            OracleConnection conn = new OracleConnection(connection);
            conn.Open();
            OracleCommand cmd = new OracleCommand("select * from WEBSITE", conn);
            //cmd.CommandText="select * from WEBSITE";
            //Oracle.DataAccess.Client.OracleDataAdapter da = new Oracle.DataAccess.Client.OracleDataAdapter(cmd);
            //System.Data.DataTable dt = new DataTable();
            //da.Fill(dt);
            OracleDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                int value = (System.Int32)reader["SORTCODE"];

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            st.Start();
            using (DbContext dbContext = new DbContext("oracle"))
            {
                List<TbasicPrice> list = FastDBEngine.DbHelper.FillList<TbasicPrice>(
                  "select * from TBASIC_PRICE t", null, dbContext, FastDBEngine.CommandKind.SqlTextNoParams);
                List<TbasicPrice> list2 = DbHelper.FillList<TbasicPrice>(
                    "select * from TBASIC_PRICE where PKID = :Id",
                     new { Id = 10000068 },
                     dbContext,
                     CommandKind.SqlTextWithParams);
                var query = "select * from TBASIC_PRICE where PKID =".AsCPQuery(true);
                query += 10000068;
                List<TbasicPrice> list7 = DbHelper.FillList<TbasicPrice>(query);
            }
            var parameters = new PKGPRODUCTGETTCATEGORYBYPKIDParameters
            {
                V_PKID = 64// output
            };
            List<TCATEGORY> list5 = DbHelper.FillList<TCATEGORY>("PKG_PRODUCT.GETTCATEGORYBYPKID", parameters);

            //List<Model.WEBSITE> list3 = DbHelper.FillList<Model.WEBSITE>("SelectWebsite", new { Id = 387 });
            //var parameters = new WebsiteParameters { P_WEBSITETYPe = "境外", P_RESULT = 0 };
            //int result = DbHelper.ExecuteNonQuery("PKG_SPIDERCONFIG.PRO_UPDATEWEBSITE", parameters);
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

        private void button1_Click_1(object sender, EventArgs e)
        {
            System.Diagnostics.Stopwatch st = new System.Diagnostics.Stopwatch();
            st.Start();
            using (Entity DbContext = new Entity())
            {
                List<TBASIC_PRICE> data = (from m in DbContext.TBASIC_PRICE select m).ToList();
            }
            st.Stop();
            MessageBox.Show(st.ElapsedMilliseconds.ToString());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            using (Entity DbContext = new Entity())
            {
                TBASIC_PRICE data = (from m in DbContext.TBASIC_PRICE where m.PKID == 10000011 select m).ToList().First();
                data.SPECTIFICATION_NAME = "20*9T";
                DbContext.TBASIC_PRICE.Attach(data);
                DbContext.Entry(data).State = System.Data.Entity.EntityState.Modified;
                int result = DbContext.SaveChanges();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (Entity DbContext = new Entity())
            {
                TBASIC_PRICE data = (from m in DbContext.TBASIC_PRICE where m.PKID == 10000013 select m).ToList().First();
                DbContext.TBASIC_PRICE.Attach(data);
                DbContext.Entry(data).State = System.Data.Entity.EntityState.Deleted;
                int result = DbContext.SaveChanges();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            var param1 = new Oracle.ManagedDataAccess.Client.OracleParameter("p_Pkid", Oracle.ManagedDataAccess.Client.OracleDbType.Int32, 10000030, ParameterDirection.Input);
            using (Entity DbContext = new Entity())
            {
                //DbContext.Database.SqlQuery<object>("BEGIN Pkg_Product.Deletetbasic_Price(:p_Pkid); end;", param1);
                int result = DbContext.Database.ExecuteSqlCommand("BEGIN Pkg_Product.Deletetbasic_Price(:p_Pkid); end;", param1);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var parameters = new
            {
                P_PKID = 10000031
            };
            int result = DbHelper.ExecuteNonQuery("PKG_PRODUCT.DELETETBASIC_PRICE", parameters);
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

    public class PKGPRODUCTGETTCATEGORYBYPKIDParameters
    {
        public int V_PKID { get; set; }
    }

    public class TCATEGORY
    {
        public int Pkid { get; set; }
        public string Categorycode { get; set; }
        public string Categoryname { get; set; }
        public string Mnemonic { get; set; }
        public int? Parentid { get; set; }
        public string Pathcode { get; set; }
        public string Picture { get; set; }
        public int? Sortno { get; set; }
        public int? Dealweight { get; set; }
        public string Remarks { get; set; }
        public int? Addedby { get; set; }
        public DateTime? Addedtime { get; set; }
        public int? Lastmodifiedby { get; set; }
        public DateTime? Lastmodifiedtime { get; set; }
        public string Lastmodifiedip { get; set; }
        public string Valid { get; set; }
    }
}
