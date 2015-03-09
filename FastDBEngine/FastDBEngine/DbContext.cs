namespace FastDBEngine
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;

    public class DbContext : IDisposable
    {
        private bool IsErrored;
        private bool useTransaction;
        internal static readonly char[] char_0 = new char[] { '@', '?', ':', '_', '#', '$' };
        private DbProviderInfo dbProviderInfo;
        internal OutParametersInfo OutParametersInfo;
        private DbCommand dbCommand;
        private DbConnection dbConnection;
        private DbTransaction dbTransaction;
        internal int capacity;
        public static readonly string STR_PageIndex = "PageIndex";
        public static readonly string STR_PageSize = "PageSize";
        public static readonly string STR_TotalRecords = "TotalRecords";
        private static string ConfigName { get; set; }
        private string cmdParamNamePrefix;

        public static DbContextEventHandler OnBeforeExecute;
        public static DbContextEventHandler OnAfterExecute;
        public static DbContextEventHandler OnOpenConnection;
        public static DbContextExceptionHandler OnException;




        public DbContext(bool useTransaction)
            : this(ConfigName, null, useTransaction)
        {
        }

        public DbContext(string configName)
            : this(configName, null, false)
        {
        }

        public DbContext(string configName, bool useTransaction)
            : this(configName, null, useTransaction)
        {
        }

        public DbContext(string configName, string connectionString, bool useTransaction)
        {
            if (string.IsNullOrEmpty(configName))
            {
                throw new ArgumentNullException("configName");
            }
            DbProviderInfo dbProviderInfo = DbInfoContainer.GetDbProviderInfo(configName);
            if (dbProviderInfo == null)
            {
                throw new InvalidOperationException(string.Format("配置项: [{0}] 不存在。", configName));
            }
            this.dbProviderInfo = dbProviderInfo;
            this.dbConnection = dbProviderInfo.dbProviderFactory.CreateConnection();
            this.cmdParamNamePrefix = dbProviderInfo.CmdParamNamePrefix;
            if (string.IsNullOrEmpty(connectionString))
            {
                if (dbProviderInfo.func != null)
                {
                    this.dbConnection.ConnectionString = dbProviderInfo.func(configName);
                }
                else
                {
                    this.dbConnection.ConnectionString = dbProviderInfo.DefaultConnString;
                }
            }
            else
            {
                this.dbConnection.ConnectionString = connectionString;
            }
            if (string.IsNullOrEmpty(this.dbConnection.ConnectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            this.useTransaction = useTransaction;
            this.AutoRetrieveOutputValues = DbContextDefaultSetting.AutoRetrieveOutputValues;
            this.capacity = DbContextDefaultSetting.ListResultCapacity;
        }

        public DbParameter AddParameter(string paraName, object paraValue, DbType paraType)
        {
            return this.AddParameter(paraName, paraValue, paraType, null, ParameterDirection.Input);
        }

        public DbParameter AddParameter(string paraName, object paraValue, DbType paraType, int size)
        {
            return this.AddParameter(paraName, paraValue, paraType, new int?(size), ParameterDirection.Input);
        }

        public DbParameter AddParameter(string paraName, object paraValue, DbType paraType, int? size, ParameterDirection inout)
        {
            this.ValidateCmd();
            DbParameter parameter = this.dbCommand.CreateParameter();
            parameter.ParameterName = this.cmdParamNamePrefix + paraName;
            parameter.DbType = paraType;
            parameter.Direction = inout;
            if (size.HasValue)
            {
                parameter.Size = size.Value;
            }
            if (paraValue != null)
            {
                parameter.Value = paraValue;
            }
            this.dbCommand.Parameters.Add(parameter);
            return parameter;
        }

        public void AddParameterWithValue(string paraName, object paraValue)
        {
            DbParameter parameter = this.dbCommand.CreateParameter();
            parameter.ParameterName = this.cmdParamNamePrefix + paraName;
            parameter.Value = paraValue;
            this.CurrentCommand.Parameters.Add(parameter);
        }

        public void CommitTransaction()
        {
            this.ValidateTran();
            this.dbTransaction.Commit();
        }

        public DbCommand CreateCommand(CPQuery query)
        {
            if (query == null)
            {
                throw new ArgumentNullException("query");
            }
            this.CreateCommand(string.Empty, CommandType.Text);
            query.BindToCommand(this.CurrentCommand);
            return this.CurrentCommand;
        }

        public DbCommand CreateCommand(string sqlOrName, CommandType commandType)
        {
            this.ValidateDbConnection();
            DbCommand command = this.dbConnection.CreateCommand();
            command.CommandText = sqlOrName.Trim();
            command.CommandType = commandType;
            this.dbCommand = command;
            this.CurrentXmlCommand = null;
            return command;
        }

        public T CreateHolder<T>() where T : class, IDbContextHolder, new()
        {
            T local = Activator.CreateInstance(typeof(T)) as T;
            local.DbContext = this;
            return local;
        }

        public DbCommand CreateXmlCommand(string commandName)
        {
            if (string.IsNullOrEmpty(commandName))
            {
                throw new ArgumentNullException("commandName");
            }
            XmlCommand command = XmlCommandManager.GetCommand(commandName);
            if (command == null)
            {
                throw new ArgumentOutOfRangeException("commandName");
            }
            return this.CreateDbCommand(command, commandName);
        }

        public void Dispose()
        {
            this.CloseConn();
            this.CloseTransaction();
        }

        public int ExecuteNonQuery()
        {
            return this.GetModel<int>(() => { return this.dbCommand.ExecuteNonQuery(); });
        }

        public object ExecuteScalar()
        {
            return this.GetModel<object>(() => { return this.dbCommand.ExecuteScalar(); });
        }

        public DataSet FillDataSet()
        {
            return this.GetModel<DataSet>(this.GetDataset);
        }

        public DataSet FillDataSet(params string[] tableNames)
        {
            DataSetHelper dataSetHelper = new DataSetHelper
            {
                tables = tableNames,
                dbContext = this
            };
            return this.GetModel<DataSet>(dataSetHelper.GetSpecDataset);
        }

        public DataTable FillDataTable()
        {
            return this.GetModel<DataTable>(this.GetTable);
        }

        public List<T> FillList<T>() where T : class, new()
        {
            typeof(T).ValidateType();
            return this.GetModel<List<T>>(this.GetModelList<T>);
        }

        public List<T> FillScalarList<T>()
        {
            return this.GetModel<List<T>>(this.GetList<T>);
        }

        public DbParameter GetCommandParameter(string parameterName, bool appendPrefix = true)
        {
            if (string.IsNullOrEmpty(parameterName))
            {
                throw new ArgumentNullException("parameterName");
            }
            this.ValidateCmd();
            parameterName = appendPrefix ? this.ParamNamePrefix + parameterName : parameterName;
            DbParameterCollection parameters = this.CurrentCommand.Parameters;
            int index = parameters.IndexOf(parameterName);
            if (index >= 0)
            {
                return parameters[index];
            }
            foreach (DbParameter parameter2 in parameters)
            {
                if ((parameter2.ParameterName.EndsWith(parameterName) && (parameter2.ParameterName.Length == (parameterName.Length + 1))) && (Array.IndexOf<char>(char_0, parameter2.ParameterName[0]) >= 0))
                {
                    return parameter2;
                }
            }
            return null;
        }

        public T GetDataItem<T>() where T : class, new()
        {
            typeof(T).ValidateType();
            return this.GetModel<T>(this.GetModel<T>);
        }

        internal DbCommand CreateDbCommand(XmlCommand xmlCommand, string str)
        {
            if (xmlCommand == null)
            {
                throw new ArgumentNullException("name");
            }
            DbCommand command = this.CreateCommand((string)xmlCommand.CommandText, xmlCommand.CommandType);
            command.CommandTimeout = xmlCommand.Timeout;
            this.CurrentXmlCommand = xmlCommand;
            DbParameter[] values = xmlCommand.GenerateXmlCmdParameters(command);
            command.Parameters.AddRange(values);
            return command;
        }

        private void CloseConn()
        {
            if (this.dbConnection != null)
            {
                try
                {
                    this.dbConnection.Close();
                    this.dbConnection.Dispose();
                }
                catch
                {
                }
                finally
                {
                    this.dbConnection = null;
                }
            }
        }

        private void OnEventHandler()
        {
            DbContextEventHandler handler = OnBeforeExecute;
            if (handler != null)
            {
                handler(this);
            }
        }

        private void OnOnAfterExecuteEvent()
        {
            if (!this.IsErrored)
            {
                DbContextEventHandler handler = OnAfterExecute;
                if (handler != null)
                {
                    handler(this);
                }
                if (this.OutParametersInfo != null)
                {
                    DbHelper.OnAfterExcute(this);
                }
            }
        }


        private List<T> GetList<T>()
        {
            List<T> list = new List<T>(DbContextDefaultSetting.ListResultCapacity);
            using (DbDataReader reader = this.dbCommand.ExecuteReader())
            {
                while (reader.Read())
                {
                    list.Add(DbHelper.ChangeType<T>(reader.GetValue(0)));
                }
                reader.Close();
            }
            return list;
        }

        private DataTable GetTable()
        {
            using (DbDataReader reader = this.dbCommand.ExecuteReader())
            {
                DataTable table = new DataTable();
                table.Load(reader);
                reader.Close();
                return table;
            }
        }

        private DataSet GetDataset()
        {
            DbDataAdapter adapter = this.dbProviderInfo.dbProviderFactory.CreateDataAdapter();
            adapter.SelectCommand = this.dbCommand;
            DataSet dataSet = new DataSet();
            adapter.Fill(dataSet);
            return dataSet;
        }

        private List<T> GetModelList<T>() where T : class, new()
        {
            using (DbDataReader reader = this.dbCommand.ExecuteReader())
            {
                GenerateModelDelegate generateModelDelegate = typeof(T).GetMeberOperationHelperContainer(true).GetModelDelegate;
                if (generateModelDelegate == null)
                {
                    List<T> list = new List<T>(this.capacity);
                    DataReaderAdapeter dataReaderAdapeter = new DataReaderAdapeter(reader);
                    while (reader.Read())
                    {
                        T local = Activator.CreateInstance(typeof(T)) as T;
                        ClassObjRepository.SetPropertyValueFromReaderAdapeter(dataReaderAdapeter, local);
                        list.Add(local);
                    }
                    reader.Close();
                    return list;
                }
                return (List<T>)generateModelDelegate(GenerateCodeHelper.dataReaderPagedState, reader, this.capacity);
            }
        }

        private T GetModel<T>() where T : class, new()
        {
            using (DbDataReader reader = this.dbCommand.ExecuteReader(CommandBehavior.SingleRow))
            {
                GenerateModelDelegate generateModelDelegate = typeof(T).GetMeberOperationHelperContainer(true).GetModelDelegate;
                if (generateModelDelegate == null)
                {
                    T local = default(T);
                    DataReaderAdapeter dataReaderAdapeter = new DataReaderAdapeter(reader);
                    if (reader.Read())
                    {
                        local = Activator.CreateInstance(typeof(T)) as T;
                        ClassObjRepository.SetPropertyValueFromReaderAdapeter(dataReaderAdapeter, local);
                    }
                    reader.Close();
                    return local;
                }
                return (T)generateModelDelegate(GenerateCodeHelper.dataReaderState, reader, 1);
            }
        }

        private void RollBack()
        {
            if (this.dbTransaction != null)
            {
                try
                {
                    this.dbTransaction.Rollback();
                }
                catch
                {
                }
                finally
                {
                    this.dbTransaction = null;
                }
            }
        }

        private void CloseTransaction()
        {
            if (this.dbTransaction != null)
            {
                try
                {
                    this.dbTransaction.Dispose();
                }
                catch
                {
                }
                finally
                {
                    this.dbTransaction = null;
                }
            }
        }

        private void ValidateDbConnection()
        {
            if (this.dbConnection == null)
            {
                throw new InvalidOperationException("connection is null.");
            }
        }

        private void ValidateCmd()
        {
            if (this.dbCommand == null)
            {
                throw new InvalidOperationException("command is null.");
            }
        }

        private void ValidateTran()
        {
            if (this.dbTransaction == null)
            {
                throw new InvalidOperationException("transcation is null.");
            }
        }

        private T GetModel<T>(Func<T> GetModelFunc)
        {
            T local;
            this.ValidateCmd();
            if (this.dbConnection.State != ConnectionState.Open)
            {
                this.dbConnection.Open();
                this.OnDbContextEventHandler();
            }
            if (this.useTransaction)
            {
                if (this.dbTransaction == null)
                {
                    this.dbTransaction = this.dbConnection.BeginTransaction();
                }
                if (this.dbCommand.Transaction == null)
                {
                    this.dbCommand.Transaction = this.dbTransaction;
                }
            }
            this.OnEventHandler();
            try
            {
                if ((this.CurrentXmlCommand != null) && !(string.IsNullOrEmpty(this.CurrentXmlCommand.Database) || (string.Compare(this.CurrentXmlCommand.Database, this.dbConnection.Database, true) == 0)))
                {
                    this.dbConnection.ChangeDatabase(this.CurrentXmlCommand.Database);
                }
                local = GetModelFunc();
            }
            catch (Exception exception)
            {
                this.OnDbContextExceptionEvent(exception);
                throw;
            }
            finally
            {
                this.OnOnAfterExecuteEvent();
            }
            return local;
        }

        private void OnDbContextExceptionEvent(Exception exception)
        {
            if (!this.KeepConnectionOnException)
            {
                this.RollBack();
                this.CloseConn();
            }
            this.IsErrored = true;
            if (!this.IgnoreErrorEvent)
            {
                DbContextExceptionHandler handler = OnException;
                if (handler != null)
                {
                    handler(this, exception);
                }
            }
        }

        private void OnDbContextEventHandler()
        {
            DbContextEventHandler handler = OnOpenConnection;
            if (handler != null)
            {
                handler(this);
            }
        }

        public static void RegisterDbConnectionInfo(string configName, string providerName, string cmdParamNamePrefix, Func<string, string> obtainFunc)
        {
            if (obtainFunc == null)
            {
                throw new ArgumentNullException("obtainFunc");
            }
            RegisterConfigName(configName, providerName, cmdParamNamePrefix, null, obtainFunc);
        }

        public static void RegisterDbConnectionInfo(string configName, string providerName, string cmdParamNamePrefix, string defaultConnString)
        {
            if (string.IsNullOrEmpty(defaultConnString))
            {
                throw new ArgumentNullException("defaultConnString");
            }
            RegisterConfigName(configName, providerName, cmdParamNamePrefix, defaultConnString, null);
        }

        private static void RegisterConfigName(string configName, string providerName, string cmdParamNamePrefix, string defaultConnString, Func<string, string> func)
        {
            if (string.IsNullOrEmpty(configName))
            {
                throw new ArgumentNullException("configName");
            }
            if (string.IsNullOrEmpty(providerName))
            {
                throw new ArgumentNullException("providerName");
            }
            DbInfoContainer.Register(configName, providerName, cmdParamNamePrefix, defaultConnString, func);
            if (string.IsNullOrEmpty(DbContext.ConfigName))
            {
                DbContext.ConfigName = configName;
            }
        }

        //private static string GetConfigName()
        //{
        //    return ConfigName;
        //}

        public bool AutoRetrieveOutputValues { get; set; }

        public DbConnection Connection
        {
            get
            {
                return this.dbConnection;
            }
        }

        public DbCommand CurrentCommand
        {
            get
            {
                return this.dbCommand;
            }
        }

        public XmlCommand CurrentXmlCommand { get; set; }

        public bool IgnoreErrorEvent { get; set; }

        public bool KeepConnectionOnException { get; set; }

        public string ParamNamePrefix
        {
            get
            {
                return this.cmdParamNamePrefix;
            }
        }

        public object Tag { get; set; }

        public DbTransaction Transaction
        {
            get
            {
                return this.dbTransaction;
            }
        }

        private sealed class DataSetHelper
        {
            public DbContext dbContext;
            public string[] tables;

            public DataSet GetSpecDataset()
            {
                using (DbDataReader reader = this.dbContext.dbCommand.ExecuteReader(CommandBehavior.Default))
                {
                    DataSet set = new DataSet();
                    set.Load(reader, LoadOption.PreserveChanges, this.tables);
                    reader.Close();
                    return set;
                }
            }
        }
    }
}

