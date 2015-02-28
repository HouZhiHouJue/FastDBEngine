using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Configuration;
using FastDBEngine;

namespace PerformanceTestApp
{
	static class Program
	{
		/// <summary>
		/// 应用程序的主入口点。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

			AppInit();

			OptionControl ctrl = new OptionControl();
			TestObject executor = new TestObject();

			MyTestAppFramework.TestMainForm mainForm = new MyTestAppFramework.TestMainForm(ctrl, executor);
			Application.Run(mainForm);
		}

		public static string ConnectionString = null;

		static void AppInit()
		{
			ConnectionStringSettings setting = ConfigurationManager.ConnectionStrings["MyNorthwind"];
			ConnectionString = setting.ConnectionString;

            // 配置 FastDBEngine
			DbContext.RegisterDbConnectionInfo("default", setting.ProviderName, "@", setting.ConnectionString);

			Type[] types = BuildManager.FindModelTypesFromCurrentApplication(x => x.Namespace == "PerformanceTestApp.Model");
			BuildManager.CompileModelTypesSync(types, true);

			// 配置 FishWebLib
			FishWebLib.FishDbContext.Init(setting.ProviderName, "@", setting.ConnectionString);
		}

	}
}
