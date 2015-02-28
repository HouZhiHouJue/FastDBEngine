namespace FastDBEngine
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.IO;
    using System.Runtime.CompilerServices;

    public static class DbHelper
    {
        [CompilerGenerated]
        private static CommandKind DefaultCommandKind { get; set; }
        private static Func<string, DbContext> func = new Func<string, DbContext>(DbHelper.GetDbContext);

        public static int ExecuteNonQuery(this CPQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            using (DbContext context = func(query.ToString()))
            {
                return query.ExecuteNonQuery(context);
            }
        }

        public static int ExecuteNonQuery(this CPQuery query, DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            dbContext.CreateCommand(query);
            return dbContext.ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(string nameOrSql, object inputParams)
        {
            return ExecuteNonQuery(nameOrSql, inputParams, DefaultCommandKind);
        }

        public static int ExecuteNonQuery(string nameOrSql, object inputParams, CommandKind cmdKind)
        {
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("nameOrSql");
            }
            using (DbContext context = func(nameOrSql))
            {
                return ExecuteNonQuery(nameOrSql, inputParams, context, cmdKind);
            }
        }

        public static int ExecuteNonQuery(string nameOrSql, object inputParams, DbContext dbContext)
        {
            return ExecuteNonQuery(nameOrSql, inputParams, dbContext, DefaultCommandKind);
        }

        public static int ExecuteNonQuery(string nameOrSql, object inputParams, DbContext dbContext, CommandKind cmdKind)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("nameOrSql");
            }
            CreateCommand(dbContext, nameOrSql, inputParams, cmdKind);
            return dbContext.ExecuteNonQuery();
        }

        public static object ExecuteScalar(this CPQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            using (DbContext context = func(query.ToString()))
            {
                return query.ExecuteScalar(context);
            }
        }

        public static T ExecuteScalar<T>(this CPQuery query)
        {
            return ChangeType<T>(query.ExecuteScalar());
        }

        public static object ExecuteScalar(this CPQuery query, DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            dbContext.CreateCommand(query);
            return dbContext.ExecuteScalar();
        }

        public static T ExecuteScalar<T>(this CPQuery query, DbContext dbContext)
        {
            return ChangeType<T>(query.ExecuteScalar(dbContext));
        }

        public static object ExecuteScalar(string nameOrSql, object inputParams)
        {
            return ExecuteScalar(nameOrSql, inputParams, DefaultCommandKind);
        }

        public static T ExecuteScalar<T>(string nameOrSql, object inputParams)
        {
            return ChangeType<T>(ExecuteScalar(nameOrSql, inputParams));
        }

        public static object ExecuteScalar(string nameOrSql, object inputParams, CommandKind cmdKind)
        {
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("name");
            }
            using (DbContext context = func(nameOrSql))
            {
                return ExecuteScalar(nameOrSql, inputParams, context, cmdKind);
            }
        }

        public static T ExecuteScalar<T>(string nameOrSql, object inputParams, CommandKind cmdKind)
        {
            return ChangeType<T>(ExecuteScalar(nameOrSql, inputParams, cmdKind));
        }

        public static object ExecuteScalar(string nameOrSql, object inputParams, DbContext dbContext)
        {
            return ExecuteScalar(nameOrSql, inputParams, dbContext, DefaultCommandKind);
        }

        public static T ExecuteScalar<T>(string nameOrSql, object inputParams, DbContext dbContext)
        {
            return ChangeType<T>(ExecuteScalar(nameOrSql, inputParams, dbContext));
        }

        public static object ExecuteScalar(string nameOrSql, object inputParams, DbContext dbContext, CommandKind cmdKind)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("nameOrSql");
            }
            CreateCommand(dbContext, nameOrSql, inputParams, cmdKind);
            return dbContext.ExecuteScalar();
        }

        public static T ExecuteScalar<T>(string nameOrSql, object inputParams, DbContext dbContext, CommandKind cmdKind)
        {
            return ChangeType<T>(ExecuteScalar(nameOrSql, inputParams, dbContext, cmdKind));
        }

        public static DataTable FillDataTable(this CPQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            using (DbContext context = func(query.ToString()))
            {
                return query.FillDataTable(context);
            }
        }

        public static DataTable FillDataTable(this CPQuery query, DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            dbContext.CreateCommand(query);
            return dbContext.FillDataTable();
        }

        public static DataTable FillDataTable(string nameOrSql, object inputParams)
        {
            return FillDataTable(nameOrSql, inputParams, DefaultCommandKind);
        }

        public static DataTable FillDataTable(string nameOrSql, object inputParams, CommandKind cmdKind)
        {
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("name");
            }
            using (DbContext context = func(nameOrSql))
            {
                return FillDataTable(nameOrSql, inputParams, context, cmdKind);
            }
        }

        public static DataTable FillDataTable(string nameOrSql, object inputParams, DbContext dbContext)
        {
            return FillDataTable(nameOrSql, inputParams, dbContext, DefaultCommandKind);
        }

        public static DataTable FillDataTable(string nameOrSql, object inputParams, DbContext dbContext, CommandKind cmdKind)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("nameOrSql");
            }
            CreateCommand(dbContext, nameOrSql, inputParams, cmdKind);
            return dbContext.FillDataTable();
        }

        public static List<T> FillList<T>(this CPQuery query) where T : class, new()
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            using (DbContext context = func(query.ToString()))
            {
                return query.FillList<T>(context);
            }
        }

        public static List<T> FillList<T>(this CPQuery query, DbContext dbContext) where T : class, new()
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            dbContext.CreateCommand(query);
            return dbContext.FillList<T>();
        }

        public static List<T> FillList<T>(string nameOrSql, object inputParams) where T : class, new()
        {
            return FillList<T>(nameOrSql, inputParams, DefaultCommandKind);
        }

        public static List<T> FillList<T>(string nameOrSql, object inputParams, CommandKind cmdKind) where T : class, new()
        {
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("nameOrSql");
            }
            using (DbContext context = func(nameOrSql))
            {
                return FillList<T>(nameOrSql, inputParams, context, cmdKind);
            }
        }

        public static List<T> FillList<T>(string nameOrSql, object inputParams, DbContext dbContext) where T : class, new()
        {
            return FillList<T>(nameOrSql, inputParams, dbContext, DefaultCommandKind);
        }

        public static List<T> FillList<T>(string nameOrSql, object inputParams, DbContext dbContext, CommandKind cmdKind) where T : class, new()
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("name");
            }
            CreateCommand(dbContext, nameOrSql, inputParams, cmdKind);
            SetPageSize(dbContext, inputParams);
            return dbContext.FillList<T>();
        }

        public static List<T> FillListFromTable<T>(DataTable table) where T : class, new()
        {
            if (table == null)
            {
                throw new ArgumentNullException("table");
            }
            if (table.Rows.Count == 0)
            {
                return new List<T>();
            }
            GenerateModelDelegate delegate2 = typeof(T).GetMeberOperationHelperContainer(true).GetModelDelegates();
            if (delegate2 == null)
            {
                List<T> list2 = new List<T>(table.Rows.Count);
                DataReaderAdapeter class2 = new DataReaderAdapeter(table.Rows[0]);
                foreach (DataRow row in table.Rows)
                {
                    class2.SetDataRow(row);
                    T local = Activator.CreateInstance(typeof(T)) as T;
                    ClassObjRepository.SetPropertyValueFromReaderAdapeter(class2, local);
                    list2.Add(local);
                }
                return list2;
            }
            return (List<T>)delegate2(GenerateCodeHelper.dataTablePagedState, table, table.Rows.Count);
        }

        public static List<T> FillListFromXmlFile<T>(string xmlPath) where T : class, new()
        {
            if (!File.Exists(xmlPath))
            {
                return null;
            }
            DataSet set = new DataSet();
            set.ReadXml(xmlPath);
            if (set.Tables.Count == 0)
            {
                return null;
            }
            return FillListFromTable<T>(set.Tables[0]);
        }

        public static List<T> FillListPaged<T>(string nameOrSql, PagingInfo inputParams) where T : class, new()
        {
            return FillListPaged<T>(nameOrSql, inputParams, DefaultCommandKind);
        }

        public static List<T> FillListPaged<T>(string nameOrSql, PagingInfo inputParams, CommandKind cmdKind) where T : class, new()
        {
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("nameOrSql");
            }
            if (inputParams == null)
            {
                throw new ArgumentNullException("inputParams");
            }
            using (DbContext context = func(nameOrSql))
            {
                return FillListPaged<T>(nameOrSql, inputParams, context, cmdKind);
            }
        }

        public static List<T> FillListPaged<T>(string nameOrSql, PagingInfo inputParams, DbContext dbContext) where T : class, new()
        {
            return FillListPaged<T>(nameOrSql, inputParams, dbContext, DefaultCommandKind);
        }

        public static List<T> FillListPaged<T>(string nameOrSql, PagingInfo inputParams, DbContext dbContext, CommandKind cmdKind) where T : class, new()
        {
            List<T> list2;
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("nameOrSql");
            }
            if (inputParams == null)
            {
                throw new ArgumentNullException("inputParams");
            }
            bool autoRetrieveOutputValues = dbContext.AutoRetrieveOutputValues;
            try
            {
                if (!autoRetrieveOutputValues)
                {
                    dbContext.AutoRetrieveOutputValues = true;
                }
                CreateCommand(dbContext, nameOrSql, inputParams, cmdKind);
                SetPageSize(dbContext, inputParams);
                List<T> list = dbContext.FillList<T>();
                if (((list.Count == 0) && (inputParams.PageIndex > 0)) && (inputParams.TotalRecords > 0))
                {
                    DbParameter commandParameter = dbContext.GetCommandParameter(DbContext.STR_PageIndex);
                    if (commandParameter != null)
                    {
                        commandParameter.Value = 0;
                        inputParams.PageIndex = 0;
                        list = dbContext.FillList<T>();
                    }
                }
                list2 = list;
            }
            finally
            {
                dbContext.AutoRetrieveOutputValues = autoRetrieveOutputValues;
            }
            return list2;
        }

        public static List<T> FillScalarList<T>(this CPQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            using (DbContext context = func(query.ToString()))
            {
                return query.FillScalarList<T>(context);
            }
        }

        public static List<T> FillScalarList<T>(this CPQuery query, DbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            dbContext.CreateCommand(query);
            return dbContext.FillScalarList<T>();
        }

        public static List<T> FillScalarList<T>(string nameOrSql, object inputParams)
        {
            return FillScalarList<T>(nameOrSql, inputParams, DefaultCommandKind);
        }

        public static List<T> FillScalarList<T>(string nameOrSql, object inputParams, CommandKind cmdKind)
        {
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("nameOrSql");
            }
            using (DbContext context = func(nameOrSql))
            {
                return FillScalarList<T>(nameOrSql, inputParams, context, cmdKind);
            }
        }

        public static List<T> FillScalarList<T>(string nameOrSql, object inputParams, DbContext dbContext)
        {
            return FillScalarList<T>(nameOrSql, inputParams, dbContext, DefaultCommandKind);
        }

        public static List<T> FillScalarList<T>(string nameOrSql, object inputParams, DbContext dbContext, CommandKind cmdKind)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("name");
            }
            CreateCommand(dbContext, nameOrSql, inputParams, cmdKind);
            SetPageSize(dbContext, inputParams);
            return dbContext.FillScalarList<T>();
        }

        public static string[] GetColumnNames(this DbDataReader reader)
        {
            int fieldCount = reader.FieldCount;
            string[] strArray = new string[fieldCount];
            for (int i = 0; i < fieldCount; i++)
            {
                strArray[i] = reader.GetName(i).FilterStr();
            }
            return strArray;
        }

        public static string[] GetColumnNames(this DataTable table)
        {
            string[] strArray = new string[table.Columns.Count];
            for (int i = 0; i < table.Columns.Count; i++)
            {
                strArray[i] = table.Columns[i].ColumnName.FilterStr();
            }
            return strArray;
        }

        public static T GetDataItem<T>(this CPQuery query) where T : class, new()
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            using (DbContext context = func(query.ToString()))
            {
                return query.GetDataItem<T>(context);
            }
        }

        public static T GetDataItem<T>(this CPQuery query, DbContext dbContext) where T : class, new()
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            dbContext.CreateCommand(query);
            return dbContext.GetDataItem<T>();
        }

        public static T GetDataItem<T>(string nameOrSql, object inputParams) where T : class, new()
        {
            return GetDataItem<T>(nameOrSql, inputParams, DefaultCommandKind);
        }

        public static T GetDataItem<T>(string nameOrSql, object inputParams, CommandKind cmdKind) where T : class, new()
        {
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("nameOrSql");
            }
            using (DbContext context = func(nameOrSql))
            {
                return GetDataItem<T>(nameOrSql, inputParams, context, cmdKind);
            }
        }

        public static T GetDataItem<T>(string nameOrSql, object inputParams, DbContext dbContext) where T : class, new()
        {
            return GetDataItem<T>(nameOrSql, inputParams, dbContext, DefaultCommandKind);
        }

        public static T GetDataItem<T>(string nameOrSql, object inputParams, DbContext dbContext, CommandKind cmdKind) where T : class, new()
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext");
            }
            if (string.IsNullOrEmpty(nameOrSql))
            {
                throw new ArgumentNullException("nameOrSql");
            }
            CreateCommand(dbContext, nameOrSql, inputParams, cmdKind);
            return dbContext.GetDataItem<T>();
        }

        public static Guid NewSequentialGuid()
        {
            byte[] destinationArray = Guid.NewGuid().ToByteArray();
            DateTime time = new DateTime(0x76c, 1, 1);
            DateTime now = DateTime.Now;
            TimeSpan span = new TimeSpan(now.Date.Ticks - time.Ticks);
            TimeSpan span2 = new TimeSpan(now.Ticks - now.Date.Ticks);
            byte[] bytes = BitConverter.GetBytes(span.Days);
            byte[] array = BitConverter.GetBytes((long)(span2.TotalMilliseconds / 3.333333));
            Array.Reverse(bytes);
            Array.Reverse(array);
            for (int i = 15; i >= 6; i--)
            {
                destinationArray[i] = destinationArray[i - 6];
            }
            Array.Copy(bytes, bytes.Length - 2, destinationArray, 0, 2);
            Array.Copy(array, array.Length - 4, destinationArray, 2, 4);
            return new Guid(destinationArray);
        }

        public static void SetCommandParameters(this DbContext dbContext, object inputParams)
        {
            if (((dbContext == null) || (dbContext.Connection == null)) || (dbContext.CurrentCommand == null))
            {
                throw new ArgumentNullException("dbContext");
            }
            if (inputParams != null)
            {
                if ((dbContext.CurrentCommand.Parameters.Count == 0) && (dbContext.CurrentCommand.CommandType == CommandType.StoredProcedure))
                {
                    foreach (DbParameter parameter in ProcParametersDeriver.GetCachedParametersWrapper(dbContext))
                    {
                        dbContext.CurrentCommand.Parameters.Add(parameter);
                    }
                }
                if (dbContext.CurrentCommand.Parameters.Count != 0)
                {
                    string str;
                    int num2 = string.IsNullOrEmpty(dbContext.ParamNamePrefix) ? -1 : dbContext.CurrentCommand.Parameters[0].ParameterName.IndexOf(dbContext.ParamNamePrefix);
                    IDictionary dictionary = inputParams as IDictionary;
                    if (dictionary != null)
                    {
                        foreach (DbParameter parameter2 in dbContext.CurrentCommand.Parameters)
                        {
                            if ((parameter2.Direction == ParameterDirection.Input) || (parameter2.Direction == ParameterDirection.InputOutput))
                            {
                                str = parameter2.ParameterName.Substring(num2 + 1);
                                parameter2.Value = dictionary[parameter2] ?? DBNull.Value;
                            }
                        }
                    }
                    else
                    {
                        inputParams.GetType().ValidateType();
                        MeberOperationHelperContainer meberOperationHelperContainer = inputParams.GetType().GetMeberOperationHelperContainer(true);
                        OutParametersInfo outParametersInfo = dbContext.AutoRetrieveOutputValues ? new OutParametersInfo() : null;
                        foreach (DbParameter parameter2 in dbContext.CurrentCommand.Parameters)
                        {
                            if ((parameter2.Direction == ParameterDirection.Input) || (parameter2.Direction == ParameterDirection.InputOutput))
                            {
                                str = parameter2.ParameterName.Substring(num2 + 1);
                                MeberOperationHelper class4 = null;
                                if (meberOperationHelperContainer.GetMeberOperationHelper(str, out class4))
                                {
                                    parameter2.Value = class4.GetPropertyValue(inputParams) ?? DBNull.Value;
                                }
                            }
                            if (dbContext.AutoRetrieveOutputValues && ((parameter2.Direction == ParameterDirection.Output) || (parameter2.Direction == ParameterDirection.InputOutput)))
                            {
                                if (parameter2.DbType != DbType.Object)
                                    outParametersInfo.AddOutParameter(parameter2.ParameterName.Substring(num2 + 1));
                            }
                        }
                        if (((outParametersInfo != null) && (outParametersInfo.GetOutParameterList() != null)) && (outParametersInfo.GetOutParameterList().Count > 0))
                        {
                            outParametersInfo.objClass = inputParams;
                            dbContext.OutParametersInfo = outParametersInfo;
                        }
                        else
                        {
                            dbContext.OutParametersInfo = null;
                        }
                    }
                }
            }
        }

        private static DbContext GetDbContext(string name)
        {
            return new DbContext(false);
        }

        internal static void OnAfterExcute(DbContext dbContext)
        {
            if ((dbContext != null) && (dbContext.OutParametersInfo != null))
            {
                OutParametersInfo outParametersInfo = dbContext.OutParametersInfo;
                dbContext.OutParametersInfo = null;
                if (outParametersInfo.objClass == null)
                {
                    throw new InvalidOperationException("Internal Error, SpOutParamDescription.DataItem is null.");
                }
                MeberOperationHelperContainer meberOperationHelperContainer = outParametersInfo.objClass.GetType().GetMeberOperationHelperContainer(true);
                foreach (string str in outParametersInfo.GetOutParameterList())
                {
                    DbParameter commandParameter = dbContext.GetCommandParameter(str, false);
                    if (commandParameter == null)
                    {
                        throw new InvalidOperationException(string.Format("设置存储过程输出参数值失败，因为命令参数 [{0}] 不存在。", str));
                    }
                    MeberOperationHelper meberOperationHelper = null;
                    if (!meberOperationHelperContainer.GetMeberOperationHelper(str, out meberOperationHelper))
                    {
                        throw new ArgumentOutOfRangeException(string.Format("设置存储过程输出参数值失败，因为实体成员 [{0}] 不存在。", str));
                    }
                    if (!meberOperationHelper.GetMemberOperationBase().CanWrite())
                    {
                        throw new InvalidOperationException(string.Format("设置存储过程输出参数值失败，因为实体成员 [{0}] 不可写。", str));
                    }
                    if (!meberOperationHelper.SetPropertyValue(outParametersInfo.objClass, commandParameter.Value))
                    {
                        throw new InvalidOperationException(string.Format("设置存储过程输出参数值失败，因为写实体成员 [{0}] 失败。", str));
                    }
                }
            }
        }

        private static void smethod_2(DbContext dbContext_0, object object_0)
        {
            if (((dbContext_0 == null) || (dbContext_0.Connection == null)) || (dbContext_0.CurrentCommand == null))
            {
                throw new ArgumentNullException("dbContext");
            }
            if (object_0 != null)
            {
                IDictionary dictionary = object_0 as IDictionary;
                if (dictionary != null)
                {
                    foreach (DictionaryEntry entry in dictionary)
                    {
                        string paraName = entry.Key.ToString();
                        dbContext_0.AddParameterWithValue(paraName, entry.Value);
                    }
                }
                else
                {
                    object_0.GetType().ValidateType();
                    foreach (KeyValuePair<string, MeberOperationHelper> pair in object_0.GetType().GetMeberOperationHelperContainer(true).GetDict())
                    {
                        dbContext_0.AddParameterWithValue(pair.Key, pair.Value.GetMemberOperationBase().GetPropertyValue(object_0));
                    }
                }
            }
        }

        private static void CreateCommand(DbContext dbContext, string sqlOrProcedureName, object obj, CommandKind commandKind)
        {
            switch (commandKind)
            {
                case CommandKind.StoreProcedure:
                    dbContext.CreateCommand(sqlOrProcedureName, CommandType.StoredProcedure);
                    break;

                case CommandKind.XmlCommand:
                    dbContext.CreateXmlCommand(sqlOrProcedureName);
                    break;

                case CommandKind.SqlTextNoParams:
                    dbContext.CreateCommand(sqlOrProcedureName, CommandType.Text);
                    return;

                case CommandKind.SqlTextWithParams:
                    dbContext.CreateCommand(sqlOrProcedureName, CommandType.Text);
                    smethod_2(dbContext, obj);
                    return;

                default:
                    {
                        XmlCommand command = XmlCommandManager.GetCommand(sqlOrProcedureName);
                        if (command != null)
                        {
                            dbContext.CreateDbCommand(command, sqlOrProcedureName);
                        }
                        else
                        {
                            dbContext.CreateCommand(sqlOrProcedureName, CommandType.StoredProcedure);
                        }
                        break;
                    }
            }
            dbContext.SetCommandParameters(obj);
        }

        internal static T ChangeType<T>(object obj)
        {
            if ((obj == null) || DBNull.Value.Equals(obj))
            {
                return default(T);
            }
            return (T)Convert.ChangeType(obj, typeof(T));
        }

        private static void SetPageSize(DbContext dbContext, object obj)
        {
            PagingInfo info = obj as PagingInfo;
            if (info == null)
            {
                dbContext.capacity = DbContextDefaultSetting.ListResultCapacity;
            }
            else
            {
                dbContext.capacity = info.PageSize;
            }
        }

        public static Func<string, DbContext> CreateDefaultDbContext
        {
            get
            {
                return func;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                func = value;
            }
        }
    }
}

