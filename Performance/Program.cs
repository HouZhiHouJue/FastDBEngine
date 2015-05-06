using FastDBEngine;
using Performance.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance
{
    class Program
    {
        static void Main(string[] args)
        {
            Init();
            CodeTimer.Time("EF", 50, EFQuery);
            CodeTimer.Time("FastDBEngine", 50, FastQuery);
            Console.ReadKey();
        }


        static void EFQuery()
        {
            using (DBContext dbContext = new DBContext())
            {
                List<Warehouse> list = dbContext.WarehouseTable.ToList();
            }
        }

        static void FastQuery()
        {
            using (DbContext dbContext = new DbContext("oracle"))
            {
                List<Warehouse> list = FastDBEngine.DbHelper.FillList<Warehouse>(
              "select * from dic_warehouses t", null, dbContext, FastDBEngine.CommandKind.SqlTextNoParams);
            }
        }


        static void Init()
        {
            CodeTimer.Initialize();
            Register();
        }

        static void Register()
        {
            string connection = System.Configuration.ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
            DbContextDefaultSetting.AutoRetrieveOutputValues = true;
            BuildManager.OnBuildException += new BuildExceptionHandler(BuildManager_OnBuildException);
            DbContext.RegisterDbConnectionInfo("oracle", "Oracle.ManagedDataAccess.Client", ":", connection);
            BuildManager.StartAutoCompile(() => BuildManager.RequestCount > 0 || BuildManager.WaitTypesCount > 0);
            //string path = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"XmlCommand");
            //XmlCommandManager.LoadCommnads(path);
            //Profiler.ApplicationName = "FastDBEngineDemo";
            //Profiler.TryStartFastDBEngineProfiler();
        }

        static void BuildManager_OnBuildException(Exception ex)
        {
            Console.WriteLine(ex);
        }

    }
}
