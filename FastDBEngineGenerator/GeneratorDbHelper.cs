using FastDBEngine;
using FastDBEngineGenerator;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;

public static class GeneratorDbHelper
{
    [CompilerGenerated]
    private static Func<Field, bool> funcBoolIdentity;
    [CompilerGenerated]
    private static Func<Field, string> funcGetFieldName = new Func<Field, string>(t => { return t.Name; });
    [CompilerGenerated]
    private static Func<Field, bool> funcIsTimestamp= new Func<Field, bool>(t => { return (string.Compare(t.DataType, "timestamp", StringComparison.OrdinalIgnoreCase) == 0); });
    [CompilerGenerated]
    private static Func<Field, bool> funcBoolDefaultValue;

    private static readonly string OracletableNameSql = "SELECT tname NAME from tab";//"\r\nselect name from ( SELECT obj.name AS [Name],  \r\n\tCAST( case when obj.is_ms_shipped = 1 then 1     \r\n\t\t\twhen ( select major_id from sys.extended_properties          \r\n\t\t\t\t\twhere major_id = obj.object_id and  minor_id = 0 and class = 1 and name = N'microsoft_database_tools_support')          \r\n\t\t\tis not null then 1  else 0 end  AS bit) AS [IsSystemObject] \r\n\tFROM sys.all_objects AS obj where obj.type in (N'U') ) as tables \r\n\twhere [IsSystemObject] = 0 ORDER BY [Name] ASC";
    private static readonly string AllViewsSql = @"select t.VIEW_NAME AS NAME from user_views t";//"\r\nselect name from ( SELECT obj.name AS [Name],  \r\n\tCAST( case when obj.is_ms_shipped = 1 then 1     \r\n\t\t\twhen ( select major_id from sys.extended_properties          \r\n\t\t\t\t\twhere major_id = obj.object_id and  minor_id = 0 and class = 1 and name = N'microsoft_database_tools_support')          \r\n\t\t\tis not null then 1  else 0 end  AS bit) AS [IsSystemObject] \r\n\tFROM sys.all_objects AS obj where obj.type in (N'V') ) as tables \r\n\twhere [IsSystemObject] = 0 ORDER BY [Name] ASC";
    private static readonly string DetailTableInfoSql = @"SELECT
       COL.COLUMN_NAME AS NAME,
       COL.DATA_TYPE AS datatype,
         
       DECODE(COL.DATA_TYPE, 'NUMBER', COL.DATA_PRECISION, COL.DATA_LENGTH) AS LENGTH,
       COL.DATA_SCALE AS SCALE,
       COL.DATA_PRECISION AS PRECISION,
       CASE
         WHEN PKCOL.COLUMN_POSITION > 0 THEN
         1
         ELSE
          0
       END AS IDENTITY,
       CASE
         WHEN COL.NULLABLE = 'Y' THEN
          1
         ELSE
          0
       END AS Nullable
  FROM USER_TAB_COLUMNS COL,
       USER_COL_COMMENTS CCOM,
       (SELECT AA.TABLE_NAME,
               AA.INDEX_NAME,
               AA.COLUMN_NAME,
               AA.COLUMN_POSITION
          FROM USER_IND_COLUMNS AA, USER_CONSTRAINTS BB
         WHERE BB.CONSTRAINT_TYPE = 'P'
           AND AA.TABLE_NAME = BB.TABLE_NAME
           AND AA.INDEX_NAME = BB.CONSTRAINT_NAME
           AND AA.TABLE_NAME IN ('{0}')) PKCOL
 WHERE COL.TABLE_NAME = CCOM.TABLE_NAME
   AND COL.COLUMN_NAME = CCOM.COLUMN_NAME
   AND COL.TABLE_NAME = '{0}'
   AND COL.COLUMN_NAME = PKCOL.COLUMN_NAME(+)
   AND COL.TABLE_NAME = PKCOL.TABLE_NAME(+)
 ORDER BY COL.COLUMN_ID";// "\r\nSELECT clmns.name AS [Name], baset.name AS [DataType], \r\n\t\tCAST(CASE WHEN baset.name IN (N'nchar', N'nvarchar') AND clmns.max_length <> -1 \r\n\t\t\tTHEN clmns.max_length/2 ELSE clmns.max_length END AS int) AS [Length], clmns.scale,\r\n\t\tCAST(clmns.precision AS int) AS [Precision], clmns.is_identity AS [Identity], \r\n\t\tclmns.is_nullable AS [Nullable] ,clmns.is_computed as [Computed],cmc.is_persisted as [IsPersisted] ,\r\n\t\tdefCst.definition as [DefaultValue], cmc.definition as [Formular],\r\n\t\tidc.seed_value as [SeedValue], idc.increment_value as [IncrementValue]\r\nFROM sys.tables AS tbl \r\nINNER JOIN sys.all_columns AS clmns ON clmns.object_id=tbl.object_id \r\nLEFT OUTER JOIN sys.types AS baset ON baset.user_type_id = clmns.system_type_id and baset.user_type_id = baset.system_type_id \r\nLEFT OUTER JOIN sys.schemas AS sclmns ON sclmns.schema_id = baset.schema_id \r\nLEFT OUTER JOIN sys.identity_columns AS ic ON ic.object_id = clmns.object_id and ic.column_id = clmns.column_id \r\nleft outer join sys.default_constraints defCst on defCst.parent_object_id = clmns.object_id and defCst.parent_column_id = clmns.column_id \r\nleft outer join sys.computed_columns cmc on cmc.object_id = clmns.object_id and cmc.column_id = clmns.column_id \r\nleft outer join sys.identity_columns idc on idc.object_id = clmns.object_id and idc.column_id = clmns.column_id \r\nWHERE (tbl.name= @TableName ) ORDER BY clmns.column_id ASC";
    private static readonly string tableColumnNamesSql = @"SELECT COL.COLUMN_NAME AS NAME
  FROM USER_TAB_COLUMNS COL,
       USER_COL_COMMENTS CCOM,
       (SELECT AA.TABLE_NAME,
               AA.INDEX_NAME,
               AA.COLUMN_NAME,
               AA.COLUMN_POSITION
          FROM USER_IND_COLUMNS AA, USER_CONSTRAINTS BB
         WHERE BB.CONSTRAINT_TYPE = 'P'
           AND AA.TABLE_NAME = BB.TABLE_NAME
           AND AA.INDEX_NAME = BB.CONSTRAINT_NAME
           AND AA.TABLE_NAME IN ('{0}')) PKCOL
 WHERE COL.TABLE_NAME = CCOM.TABLE_NAME
   AND COL.COLUMN_NAME = CCOM.COLUMN_NAME
   AND PKCOL.COLUMN_POSITION > 0
   AND COL.TABLE_NAME = '{0}'
   AND COL.COLUMN_NAME = PKCOL.COLUMN_NAME(+)
   AND COL.TABLE_NAME = PKCOL.TABLE_NAME(+)

";//"\r\nselect col.name as column_nName \r\nfrom sys.indexes ind \r\nleft outer join (sys.index_columns ind_col inner join sys.columns col on col.object_id = ind_col.object_id and col.column_id = ind_col.column_id )  on ind_col.object_id = ind.object_id and ind_col.index_id = ind.index_id \r\nwhere ind.object_id = object_id( @TableName )  and ind.index_id >= 0 and ind.type = 1 \r\nand ind.is_hypothetical = 0   order by ind.index_id, ind_col.key_ordinal\r\n";
    private static readonly string tableNamesSql = "\r\nSELECT dtb.name AS [Database_Name] FROM master.sys.databases AS dtb \r\nWHERE (CAST(case when dtb.name in ('master','model','msdb','tempdb') then 1 else dtb.is_distributor end AS bit)=0 \r\n\t\tand CAST(isnull(dtb.source_database_id, 0) AS bit)=0) \r\nORDER BY [Database_Name] ASC";
    private static readonly string procNamesSql = @"SELECT T.OBJECT_NAME NAME
  FROM USER_PROCEDURES T
 WHERE T.OBJECT_TYPE = 'PROCEDURE'
UNION
SELECT T.OBJECT_NAME || '.' || T.PROCEDURE_NAME NAME
  FROM USER_PROCEDURES T
 WHERE T.OBJECT_TYPE = 'PACKAGE'
   AND T.PROCEDURE_NAME IS NOT NULL";//"SELECT name FROM sys.sql_modules JOIN sys.objects ON sys.sql_modules.object_id = sys.objects.object_id AND type = 'P' where name not like 'sp_%' order by name";
    private static readonly string ProcDefinitionSql = @"SELECT LISTAGG(TEXT,'') WITHIN GROUP(ORDER BY line) AS definition
  FROM ALL_SOURCE
 WHERE NAME = :ObjectName";//"select definition from sys.sql_modules JOIN sys.objects ON sys.sql_modules.object_id = sys.objects.object_id where name = @ObjectName";
    private static readonly string ViewDefinitionSql =  @"select dbms_metadata.get_ddl('VIEW',:ObjectName,'{0}') from dual";// "SELECT definition FROM sys.sql_modules JOIN sys.objects ON sys.sql_modules.object_id = sys.objects.object_id AND type = N'V' and name = @ObjectName";
    private static readonly string tableDefinitionSql = @"select dbms_metadata.get_ddl('TABLE',:TableName,'{0}') from dual";
    private static readonly string dbName = "oracle";

    internal static void RegisterSqlServer()
    {
       // DbContext.RegisterDbConnectionInfo(dbName, "System.Data.SqlClient", "@", "connectionstring placeholder.");
    }

    private static DbContext GetDbContext(string conn, string initialCatalog)
    {
        if (string.IsNullOrEmpty(initialCatalog))
        {
            return new DbContext(dbName, conn, false);
        }
        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(conn)
        {
            InitialCatalog = initialCatalog
        };
        return new DbContext(dbName, builder.ToString(), false);
    }

    public static string ExecuteScalar(string nameOrSql, string conn, string configName, string objectName)
    {
        using (DbContext context = new DbContext("oracle"))// smethod_1(string_10, string_11))
        {
            return DbHelper.ExecuteScalar<string>(nameOrSql, new { ObjectName = objectName }, context, CommandKind.SqlTextWithParams);
        }
    }

    public static string GetCommandText(string conn, string configName, string procName)
    {
        return ExecuteScalar(ProcDefinitionSql, conn, configName, procName);
    }

    public static string ExecuteScalarViewDefinition(string conn, string configName, string name)
    {
        return ExecuteScalar(string.Format(ViewDefinitionSql,Util.GetConnUserName(conn)), conn, configName, name);
    }

    public static List<XmlCommand> AddParametersToCommand(string conn, string database, string commandName)
    {
        DbParameter[] parameterArray = GetParameters(conn, database, commandName);
        string str = GetCommandText(conn, database, commandName);
        XmlCommand command = new XmlCommand
        {
            CommandName = commandName,
            CommandText = FilterCommandText(str),
            Database = database
        };
        foreach (DbParameter parameter in parameterArray)
        {
            XmlCmdParameter item = new XmlCmdParameter
            {
                Name = parameter.ParameterName,
                Type = parameter.DbType,
                Direction = parameter.Direction,
                Size = parameter.Size
            };
            command.Parameters.Add(item);
        }
        return new List<XmlCommand>(1) { command };
    }

    private static string FilterCommandText(string commandText)
    {
        if (!string.IsNullOrEmpty(commandText))
        {
            string pattern = @"\s+as\s+";
            Match match = Regex.Match(commandText, pattern, RegexOptions.Multiline | RegexOptions.IgnoreCase);
            if (match.Success)
            {
                return ("\r\n" + commandText.Substring(match.Index + match.Length).Trim() + "\r\n");
            }
        }
        return commandText;
    }

    public static string GetTableDefinitionText(List<Field> list, string tableName)
    {
        using (DbContext dbcontext = new DbContext("oracle"))
        {
            return DbHelper.ExecuteScalar<string>(string.Format(tableDefinitionSql, Util.GetConnUserName(dbcontext.Connection.ConnectionString)), new { TableName = tableName },dbcontext,CommandKind.SqlTextWithParams);
        }
        //return string.Empty;
        //if (((list == null) || (list.Count == 0)) || string.IsNullOrEmpty(tableName))
        //{
        //    return string.Empty;
        //}
        //StringBuilder builder = new StringBuilder();
        //builder.AppendFormat("create table {0} (\r\n", tableName);
        //int num = 0;
        //foreach (Field field in list)
        //{
        //    num++;
        //    if (string.IsNullOrEmpty(field.Formular))
        //    {
        //        builder.AppendFormat("\t{0} {1}{2}{3}{4}{5}\r\n", new object[] { field.Name, field.DataType, field.Identity ? "" : "", field.Nullable ? "" : " not null", string.IsNullOrEmpty(field.DefaultValue) ? "" : (" default " + field.DefaultValue), (num < list.Count) ? "," : "" });//string.Format(" identity({0},{1})", field.SeedValue, field.IncrementValue)
        //    }
        //    else
        //    {
        //        builder.AppendFormat("\t{0} as {1}{2}{3}\r\n", new object[] { field.Name, field.Formular, field.IsPersisted ? " Persisted" : "", (num < list.Count) ? "," : "" });
        //    }
        //}
        //builder.AppendLine(")");
        //return builder.ToString();
    }

    public static DbParameter[] GetParameters(string conn, string dataBase, string commandName)
    {
        //SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(string_9)
        //{
        //    InitialCatalog = string_10
        //};
       // using (SqlConnection connection = new SqlConnection(builder.ToString()))
        using (Oracle.DataAccess.Client.OracleConnection connection=new OracleConnection(conn))
        {
            return DBParametersManager.DeriveParameters(connection, commandName);
        }
    }

    public static List<XmlCommand> GetListXmlCommands(string conn, string configName, string tableName)
    {
        string str;
        List<XmlCommand> listXmlCommands = new List<XmlCommand>(3);
        List<Field> listFields = GetFieldList(conn, configName, tableName);
        List<string> list3 = null;
        using (DbContext context = new DbContext("oracle"))//  smethod_1(string_9, string_10))
        {
            list3 = DbHelper.FillScalarList<string>(string.Format(tableColumnNamesSql, tableName), null, context, CommandKind.SqlTextWithParams);
        }
        if (list3.Count == 0)
        {
            if (funcBoolIdentity == null)
            {
                funcBoolIdentity = new Func<Field, bool>(t => { return t.Identity; });
            }
            str = Enumerable.Select<Field, string>(Enumerable.Where<Field>(listFields, funcBoolIdentity), funcGetFieldName).FirstOrDefault<string>();
            if (str != null)
            {
                list3.Add(str);
            }
        }
        if (list3.Count == 0)
        {
            str = Enumerable.Select<Field, string>(Enumerable.Where<Field>(listFields, funcIsTimestamp), funcGetFieldName).FirstOrDefault<string>();
            if (str != null)
            {
                list3.Add(str);
            }
        }
        if (list3.Count == 0)
        {
            if (funcBoolDefaultValue == null)
            {
                funcBoolDefaultValue = new Func<Field, bool>(t => { return ((t.DefaultValue != null) && (t.DefaultValue.IndexOf("newsequentialid()", StringComparison.OrdinalIgnoreCase) >= 0)); });
            }
            str = Enumerable.Select<Field, string>(Enumerable.Where<Field>(listFields, funcBoolDefaultValue), funcGetFieldName).FirstOrDefault<string>();
            if (str != null)
            {
                list3.Add(str);
            }
        }
        XmlCommand item = new XmlCommand
        {
            CommandName = "Insert" + tableName.FilterStr(),
            CommandType = CommandType.Text
        };
        StringBuilder builder = new StringBuilder();
        StringBuilder builder2 = new StringBuilder();
        builder.AppendFormat("\r\ninsert into {0} (", tableName);
        builder2.Append("values (");
        foreach (Field field in listFields)
        {
            if (((!field.Identity && !field.Computed) && (string.Compare(field.DataType, "timestamp", StringComparison.OrdinalIgnoreCase) != 0)) && ((field.DefaultValue == null) || (field.DefaultValue.IndexOf("newsequentialid()", StringComparison.OrdinalIgnoreCase) < 0)))
            {
                builder.AppendFormat("{0},", field.Name);
                builder2.AppendFormat(":{0},", field.Name);
                item.Parameters.Add(GetXmlCmdParameter(field));
            }
        }
        builder.Remove(builder.Length - 1, 1).AppendLine(")");
        builder2.Remove(builder2.Length - 1, 1).AppendLine(");");
        item.CommandText = builder.ToString() + builder2.ToString();
        listXmlCommands.Add(item);
        if (list3.Count > 0)
        {
            XmlCommand command2 = new XmlCommand
            {
                CommandName = "Update" + tableName.FilterStr(),
                CommandType = CommandType.Text
            };
            StringBuilder builder3 = new StringBuilder();
            builder3.AppendFormat("\r\nupdate {0} set \r\n", tableName);
            foreach (Field field in listFields)
            {
                if ((((!field.Identity && !field.Computed) && (string.Compare(field.DataType, "timestamp", StringComparison.OrdinalIgnoreCase) != 0)) && ((field.DefaultValue == null) || (field.DefaultValue.IndexOf("newsequentialid()", StringComparison.OrdinalIgnoreCase) < 0))) && (list3.FindIndex(field.Name) < 0))
                {
                    builder3.AppendFormat("{0} = :{0}, ", field.Name);
                    command2.Parameters.Add(GetXmlCmdParameter(field));
                }
            }
            builder3.Remove(builder3.Length - 2, 2).Append("\r\nWhere ");
            foreach (Field field in listFields)
            {
                if (list3.FindIndex(field.Name) >= 0)
                {
                    builder3.AppendFormat(" {0} = :{0} and", field.Name);
                    command2.Parameters.Add(GetXmlCmdParameter(field));
                }
            }
            builder3.Remove(builder3.Length - 4, 4).AppendLine();
            command2.CommandText = builder3.ToString();
            listXmlCommands.Add(command2);
            XmlCommand command3 = new XmlCommand
            {
                CommandName = "Delete" + tableName.FilterStr(),
                CommandType = CommandType.Text
            };
            StringBuilder builder4 = new StringBuilder();
            builder4.AppendFormat("\r\ndelete from {0} where \r\n", tableName);
            foreach (Field field in listFields)
            {
                if (list3.FindIndex(field.Name) >= 0)
                {
                    builder4.AppendFormat(" {0} = :{0} and", field.Name);
                    command3.Parameters.Add(GetXmlCmdParameter(field));
                }
            }
            builder4.Remove(builder4.Length - 4, 4).AppendLine();
            command3.CommandText = builder4.ToString();
            listXmlCommands.Add(command3);
        }
        return listXmlCommands;
    }

    private static XmlCmdParameter GetXmlCmdParameter(Field field)
    {
        XmlCmdParameter parameter = new XmlCmdParameter
        {
            Name = ":" + field.Name,
            Type = field.ConvertToDbType()
        };
        if ((parameter.Type == DbType.String) && (field.Length > 0))
        {
            parameter.Size = field.Length;
        }
        return parameter;
    }

    public static List<Field> GetFieldList(string conn, string configName, string tableName)
    {
        using (DbContext context = new DbContext("oracle"))// smethod_1(string_9, string_10))
        {
            return DbHelper.FillList<Field>(string.Format(DetailTableInfoSql, tableName), null, context, CommandKind.SqlTextWithParams);
        }
    }

    //public static int smethod_3(string string_9)
    //{
    //    using (DbContext context = GetDbContext(string_9, null))
    //    {
    //        string nameOrSql = "select (@@microsoftversion / 0x01000000);";
    //        return Convert.ToInt32(DbHelper.ExecuteScalar(nameOrSql, null, context, CommandKind.SqlTextNoParams));
    //    }
    //}

    public static List<Field> GetFieldListFromReader(string connStr, string configName, string sql)
    {
        List<Field> list = new List<Field>();
        using (OracleConnection conn = new OracleConnection(connStr))// smethod_1(string_9, string_10))
        {
            conn.Open();
            using (Oracle.DataAccess.Client.OracleCommand command = new OracleCommand(sql, conn))
            {
                using (OracleDataReader reader = command.ExecuteReader())
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        Field item = new Field
                        {
                            Name = reader.GetName(i),
                            DataType = reader.GetFieldType(i).ToString(),
                            Nullable = false
                        };
                        list.Add(item);
                    }
                }
            }
        }
        foreach (Field field2 in list)
        {
            if (string.IsNullOrEmpty(field2.Name))
            {
                field2.Name = "无名列";
            }
        }
        return list;
    }

    public static List<string> GetList(string tableNamesSql, string conn, string configName)
    {
        using (DbContext context = new DbContext("oracle"))// smethod_1(string_10, string_11))
        {
            return DbHelper.FillScalarList<string>(tableNamesSql, null, context, CommandKind.SqlTextNoParams);
        }
    }

    public static List<string> GetList(string conn)
    {
        return GetList(tableNamesSql, conn, null);
    }

    public static List<string> GetList(string conn, string configName)
    {
        return GetList(OracletableNameSql, conn, configName);
    }

    public static List<string> GetAllViews(string conn, string configName)
    {
        return GetList(AllViewsSql, conn, configName);
    }

    public static List<string> GetprocNames(string conn, string configName)
    {
        return GetList(procNamesSql, conn, configName);
    }
}

