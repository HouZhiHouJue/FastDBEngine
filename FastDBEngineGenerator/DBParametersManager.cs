using FastDBEngine;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

internal static class DBParametersManager
{
    private static Hashtable hashtable = new Hashtable(0xc00);

    public static DbParameter[] DeriveParameters(DbConnection dbConnection, string spName)
    {
        if (dbConnection == null)
        {
            throw new ArgumentNullException("dbConn");
        }
        if (string.IsNullOrEmpty(spName))
        {
            throw new ArgumentNullException("spName");
        }
        DbCommand command = null;
        using (DbConnection connection = (DbConnection)((ICloneable)dbConnection).Clone())
        {
            command = connection.CreateCommand();
            command.CommandText = spName;
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            if (command is SqlCommand)
            {
                SqlCommandBuilder.DeriveParameters((SqlCommand)command);
            }
            else if (command is Oracle.DataAccess.Client.OracleCommand)
            {
                Oracle.DataAccess.Client.OracleCommandBuilder.DeriveParameters((Oracle.DataAccess.Client.OracleCommand)command);
            }
            else
                throw new NotSupportedException();
            connection.Close();

        }
        if ((command.Parameters.Count > 0) && (command.Parameters[0].Direction == ParameterDirection.ReturnValue))
        {
            command.Parameters.RemoveAt(0);
        }
        DbParameter[] array = new DbParameter[command.Parameters.Count];
        command.Parameters.CopyTo(array, 0);
        return array;
    }

    public static DbParameter[] CloneParameterArray(DbParameter[] dbParameter)
    {
        int length = dbParameter.Length;
        DbParameter[] parameterArray = new DbParameter[length];
        for (int i = 0; i < length; i++)
        {
            parameterArray[i] = (DbParameter)((ICloneable)dbParameter[i]).Clone();
        }
        return parameterArray;
    }

    public static DbParameter[] GetCachedParameters(DbContext dbContext)
    {
        if (((dbContext == null) || (dbContext.Connection == null)) || (dbContext.CurrentCommand == null))
        {
            throw new ArgumentNullException("dbContext");
        }
        if ((dbContext.CurrentCommand.CommandType != CommandType.StoredProcedure) || string.IsNullOrEmpty(dbContext.CurrentCommand.CommandText))
        {
            throw new InvalidOperationException("命令不是存储过程，或没有设置存储过程。");
        }
        return InnerGetCachedParameters(dbContext.Connection, dbContext.CurrentCommand.CommandText);
    }

    public static DbParameter[] InnerGetCachedParameters(DbConnection dbConnection, string spName)
    {
        if (dbConnection == null)
        {
            throw new ArgumentNullException("dbConn");
        }
        if (string.IsNullOrEmpty(spName))
        {
            throw new ArgumentNullException("spName");
        }
        string str = spName + "###" + dbConnection.ConnectionString + "###" + dbConnection.GetType().ToString();
        DbParameter[] parameterArray = hashtable[str] as DbParameter[];
        if (parameterArray == null)
        {
            lock (hashtable.SyncRoot)
            {
                parameterArray = hashtable[str] as DbParameter[];
                if (parameterArray == null)
                {
                    parameterArray = DeriveParameters(dbConnection, spName);
                    hashtable[str] = parameterArray;
                }
            }
        }
        return CloneParameterArray(parameterArray);
    }

    public static DbParameter[] GetXmlDbParameter(this XmlCommand xmlCommand, DbCommand dbCommand)
    {
        if (xmlCommand == null)
        {
            throw new ArgumentNullException("xmlCommand");
        }
        if (dbCommand == null)
        {
            throw new ArgumentNullException("dbCommand");
        }
        if (xmlCommand.Parameters.Count == 0)
        {
            return new DbParameter[0];
        }
        DbParameter[] parameterArray = new DbParameter[xmlCommand.Parameters.Count];
        for (int i = 0; i < xmlCommand.Parameters.Count; i++)
        {
            XmlCmdParameter parameter = xmlCommand.Parameters[i];
            DbParameter parameter2 = dbCommand.CreateParameter();
            parameter2.ParameterName = parameter.Name;
            parameter2.DbType = parameter.Type;
            parameter2.Direction = parameter.Direction;
            parameter2.Size = parameter.Size;
            parameterArray[i] = parameter2;
        }
        return parameterArray;
    }
}

