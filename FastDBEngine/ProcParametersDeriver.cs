using FastDBEngine;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;

internal static class ProcParametersDeriver
{
    private static Hashtable hashtable = new Hashtable(0xc00);

    public static DbParameter[] DeriveParameters(DbConnection dbConnection, string commandText)
    {
        if (dbConnection == null)
        {
            throw new ArgumentNullException("dbConn");
        }
        if (string.IsNullOrEmpty(commandText))
        {
            throw new ArgumentNullException("spName");
        }
        DbCommand command = null;
        using (DbConnection connection = (DbConnection)((ICloneable)dbConnection).Clone())
        {
            command = connection.CreateCommand();
            command.CommandText = commandText;
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            if (command is SqlCommand)
            {
                SqlCommandBuilder.DeriveParameters((SqlCommand)command);
                connection.Close();

            }
            else if (command is OracleCommand)
            {
              OracleCommandBuilder.DeriveParameters((OracleCommand)command);
                connection.Close();
            }
            else
                throw new NotSupportedException();

        }
        if ((command.Parameters.Count > 0) && (command.Parameters[0].Direction == ParameterDirection.ReturnValue))
        {
            command.Parameters.RemoveAt(0);
        }
        DbParameter[] array = new DbParameter[command.Parameters.Count];
        command.Parameters.CopyTo(array, 0);
        return array;
    }

    public static DbParameter[] CloneParameters(DbParameter[] dbParameters)
    {
        int length = dbParameters.Length;
        DbParameter[] parameterArray = new DbParameter[length];
        for (int i = 0; i < length; i++)
        {
            parameterArray[i] = (DbParameter)((ICloneable)dbParameters[i]).Clone();
        }
        return parameterArray;
    }

    public static DbParameter[] GetCachedParametersWrapper(DbContext dbContext)
    {
        if (((dbContext == null) || (dbContext.Connection == null)) || (dbContext.CurrentCommand == null))
        {
            throw new ArgumentNullException("dbContext");
        }
        if ((dbContext.CurrentCommand.CommandType != CommandType.StoredProcedure) || string.IsNullOrEmpty(dbContext.CurrentCommand.CommandText))
        {
            throw new InvalidOperationException("命令不是存储过程，或没有设置存储过程。");
        }
        return GetCachedParameters(dbContext.Connection, dbContext.CurrentCommand.CommandText);
    }

    public static DbParameter[] GetCachedParameters(DbConnection dbConnection, string name)
    {
        if (dbConnection == null)
        {
            throw new ArgumentNullException("dbConn");
        }
        if (string.IsNullOrEmpty(name))
        {
            throw new ArgumentNullException("spName");
        }
        string str = name + "###" + dbConnection.ConnectionString + "###" + dbConnection.GetType().ToString();
        DbParameter[] parameterArray = hashtable[str] as DbParameter[];
        if (parameterArray == null)
        {
            lock (hashtable.SyncRoot)
            {
                parameterArray = hashtable[str] as DbParameter[];
                if (parameterArray == null)
                {
                    parameterArray = DeriveParameters(dbConnection, name);
                    hashtable[str] = parameterArray;
                }
            }
        }
        return CloneParameters(parameterArray);
    }

    public static DbParameter[] GenerateXmlCmdParameters(this XmlCommand xmlCommand, DbCommand dbCommand)
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
        DbParameter[] parameterArray2 = new DbParameter[xmlCommand.Parameters.Count];
        for (int i = 0; i < xmlCommand.Parameters.Count; i++)
        {
            XmlCmdParameter parameter = xmlCommand.Parameters[i];
            DbParameter parameter2 = dbCommand.CreateParameter();
            parameter2.ParameterName = parameter.Name;
            parameter2.DbType = parameter.Type;
            parameter2.Direction = parameter.Direction;
            parameter2.Size = parameter.Size;
            parameterArray2[i] = parameter2;
        }
        return parameterArray2;
    }
}

