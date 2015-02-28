namespace FastDBEngine
{
    using System;
    using System.Data;
    using System.Data.SqlClient;
    using System.Text.RegularExpressions;

    public static class SqlServerHelper
    {
        private static Regex reg = new Regex(@"^\s*GO\s*$", RegexOptions.Multiline | RegexOptions.IgnoreCase);

        public static void ExecuteTsqlScript(SqlConnection connection, string SqlText, Action<string> execNotify)
        {
            if (!string.IsNullOrEmpty(SqlText))
            {
                if (connection == null)
                {
                    throw new ArgumentNullException("connection");
                }
                string[] strArray = reg.Split(SqlText);
                bool flag = false;
                try
                {
                    if (connection.State != ConnectionState.Open)
                    {
                        connection.Open();
                        flag = true;
                    }
                    SqlCommand command = connection.CreateCommand();
                    command.CommandType = CommandType.Text;
                    command.CommandTimeout = 0;
                    foreach (string str in strArray)
                    {
                        string str2 = str.Trim();
                        if (str2.Length > 0)
                        {
                            command.CommandText = str2;
                            if (execNotify != null)
                            {
                                execNotify(str2);
                            }
                            command.ExecuteNonQuery();
                        }
                    }
                }
                finally
                {
                    if (flag)
                    {
                        connection.Close();
                    }
                }
            }
        }
    }
}

