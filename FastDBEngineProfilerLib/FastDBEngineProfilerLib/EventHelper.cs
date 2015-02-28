namespace FastDBEngineProfilerLib
{
    using FastDBEngine;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Runtime.CompilerServices;
    using System.Security;

    public static class EventHelper
    {
        private static bool s_isSubscribed = false;

        private static ParamValuePair ConvertDbParameterToParamValuePair(DbParameter param)
        {
            ParamValuePair pair = new ParamValuePair {
                Name = param.ParameterName,
                Description = EnumNameDict<ParameterDirection>.GetString(param.Direction) + " " + EnumNameDict<DbType>.GetString(param.DbType)
            };
            if (param.Value == null)
            {
                pair.Value = "null";
                return pair;
            }
            try
            {
                string str;
                switch (param.DbType)
                {
                    case DbType.AnsiString:
                    case DbType.String:
                    case DbType.AnsiStringFixedLength:
                    case DbType.StringFixedLength:
                    case DbType.Xml:
                        str = param.Value.ToString();
                        if (str.Length <= 50)
                        {
                            break;
                        }
                        pair.Value = "'" + str.Substring(0, 50) + "...'";
                        return pair;

                    case DbType.Boolean:
                    case DbType.Currency:
                    case DbType.Date:
                    case DbType.DateTime:
                    case DbType.Decimal:
                    case DbType.Double:
                    case DbType.Guid:
                    case DbType.Int16:
                    case DbType.Int32:
                    case DbType.Int64:
                    case DbType.Single:
                    case DbType.UInt16:
                    case DbType.UInt32:
                    case DbType.UInt64:
                    case DbType.VarNumeric:
                    case DbType.DateTime2:
                    case DbType.DateTimeOffset:
                        pair.Value = param.Value.ToString();
                        return pair;

                    default:
                        goto Label_0148;
                }
                pair.Value = "'" + str + "'";
                return pair;
            Label_0148:
                pair.Value = "(...)";
            }
            catch
            {
                pair.Value = "(...)";
            }
            return pair;
        }

        private static void DbContext_OnAfterExecute(DbContext context)
        {
            if (context.Tag != null)
            {
                ContextInfo tag = context.Tag as ContextInfo;
                if (tag != null)
                {
                    ExecuteInfo info = new ExecuteInfo {
                        AppName = Profiler.ApplicationName,
                        CommandText = null,
                        ConnectionString = null,
                        EndTime = new DateTime?(DateTime.Now),
                        ExceptionMessage = null,
                        InfoKey = tag.InfoKey,
                        InfoType = InfoType.ExecuteFinished,
                        IsWithTranscation = false,
                        ParameterValues = null,
                        StartTime = tag.StartTime
                    };
                    ClientHelper.SendExecuteInfo(info);
                }
            }
        }

        private static void DbContext_OnBeforeExecute(DbContext context)
        {
            ContextInfo info = new ContextInfo {
                InfoKey = Guid.NewGuid().ToString(),
                StartTime = DateTime.Now
            };
            context.Tag = info;
            ExecuteInfo info2 = new ExecuteInfo {
                AppName = Profiler.ApplicationName,
                CommandText = context.CurrentCommand.CommandText,
                CommandType = context.CurrentCommand.CommandType,
                ConnectionString = context.Connection.ConnectionString,
                XmlCommandName = (context.CurrentXmlCommand != null) ? context.CurrentXmlCommand.CommandName : string.Empty,
                EndTime = null,
                ExceptionMessage = null,
                InfoKey = info.InfoKey,
                InfoType = (context.CurrentCommand.CommandType == CommandType.StoredProcedure) ? InfoType.StartExecuteSP : InfoType.StartExecuteSQL,
                IsWithTranscation = context.CurrentCommand.Transaction != null,
                ParameterValues = GetParameterList(context.CurrentCommand),
                StartTime = info.StartTime
            };
            ClientHelper.SendExecuteInfo(info2);
        }

        private static void DbContext_OnException(DbContext context, Exception ex)
        {
            if (context.Tag != null)
            {
                ContextInfo tag = context.Tag as ContextInfo;
                if (tag != null)
                {
                    ExecuteInfo info = new ExecuteInfo {
                        AppName = Profiler.ApplicationName,
                        CommandText = null,
                        ConnectionString = null,
                        EndTime = new DateTime?(DateTime.Now),
                        ExceptionMessage = ex.Message,
                        InfoKey = tag.InfoKey,
                        InfoType = InfoType.ExecuteFailed,
                        IsWithTranscation = false,
                        ParameterValues = null,
                        StartTime = tag.StartTime
                    };
                    ClientHelper.SendExecuteInfo(info);
                }
            }
        }

        private static void DbContext_OnOpenConnection(DbContext context)
        {
            ExecuteInfo info = new ExecuteInfo {
                AppName = Profiler.ApplicationName,
                CommandText = null,
                ConnectionString = context.Connection.ConnectionString,
                EndTime = null,
                ExceptionMessage = null,
                InfoKey = Guid.NewGuid().ToString(),
                InfoType = InfoType.OpenConnection,
                IsWithTranscation = false,
                ParameterValues = null,
                StartTime = DateTime.Now
            };
            ClientHelper.SendExecuteInfo(info);
        }

        private static List<ParamValuePair> GetParameterList(DbCommand command)
        {
            if ((command.Parameters == null) || (command.Parameters.Count == 0))
            {
                return null;
            }
            List<ParamValuePair> list = new List<ParamValuePair>();
            foreach (DbParameter parameter in command.Parameters)
            {
                ParamValuePair item = ConvertDbParameterToParamValuePair(parameter);
                list.Add(item);
            }
            return list;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void SubscribeNotify()
        {
            if (!s_isSubscribed)
            {
                s_isSubscribed = true;
                DbContext.OnOpenConnection += new DbContextEventHandler(EventHelper.DbContext_OnOpenConnection);
                DbContext.OnBeforeExecute += new DbContextEventHandler(EventHelper.DbContext_OnBeforeExecute);
                DbContext.OnAfterExecute += new DbContextEventHandler(EventHelper.DbContext_OnAfterExecute);
                DbContext.OnException += new DbContextExceptionHandler(EventHelper.DbContext_OnException);
                ClientHelper.StartClientThread();
            }
        }
    }
}

