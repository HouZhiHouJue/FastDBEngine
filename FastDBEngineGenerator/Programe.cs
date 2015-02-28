using FastDBEngineGenerator;
using System;
using System.Windows.Forms;

internal static class Programe
{
    [STAThread]
    private static void Main()
    {
        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
       // GeneratorDbHelper.RegisterSqlServer();
        Application.Run(new MainFormFix());
    }
}

