namespace FastDBEngine
{
    using System;
    using System.Data;
    using System.Data.OleDb;

    public static class DataConverter
    {
        public static DataTable ExcelToTable(string excelFilePath)
        {
            return ExcelToTable(excelFilePath, 0);
        }

        public static DataTable ExcelToTable(string excelFilePath, int sheetIndex)
        {
            if (string.IsNullOrEmpty(excelFilePath))
            {
                throw new ArgumentNullException("excelFilePath");
            }
            string connectionString = excelFilePath.EndsWith("xls", StringComparison.OrdinalIgnoreCase) ? ("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + excelFilePath + ";Extended Properties=Excel 8.0;") : ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath + ";Extended Properties=Excel 12.0;");
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                object[] restrictions = new object[4];
                restrictions[3] = "TABLE";
                DataTable oleDbSchemaTable = connection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, restrictions);
                if (sheetIndex >= 0)
                {
                    if (sheetIndex >= (oleDbSchemaTable.Rows.Count - 1))
                    {
                        throw new ArgumentOutOfRangeException("sheetIndex");
                    }
                    OleDbCommand command = new OleDbCommand("select * from [" + oleDbSchemaTable.Rows[sheetIndex]["TABLE_NAME"] + "]", connection);
                    using (OleDbDataReader reader = command.ExecuteReader())
                    {
                        DataTable table3 = new DataTable();
                        table3.Load(reader);
                        reader.Close();
                        return table3;
                    }
                }
                return oleDbSchemaTable;
            }
        }

        public static DataTable ExcelToTable(string excelFilePath, string sheetName)
        {
            DataTable table2;
            if (string.IsNullOrEmpty(excelFilePath))
            {
                throw new ArgumentNullException("excelFilePath");
            }
            if (string.IsNullOrEmpty(sheetName))
            {
                throw new ArgumentNullException("sheetName");
            }
            string connectionString = excelFilePath.EndsWith("xls", StringComparison.OrdinalIgnoreCase) ? ("Provider=Microsoft.Jet.OLEDB.4.0; Data Source=" + excelFilePath + ";Extended Properties=Excel 8.0;") : ("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelFilePath + ";Extended Properties=Excel 12.0;");
            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                OleDbCommand command = new OleDbCommand("select * from [" + sheetName + "]", connection);
                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    DataTable table = new DataTable();
                    table.Load(reader);
                    reader.Close();
                    table2 = table;
                }
            }
            return table2;
        }
    }
}

