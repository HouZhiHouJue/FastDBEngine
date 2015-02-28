using FastDBEngine;
using System;
using System.Data;
using System.Data.Common;
using System.Reflection;

[DefaultMember("Item")]
internal sealed class DataReaderAdapeter
{
    private IDataReaderHelper DataReaderHelper;
    public object obj;

    public DataReaderAdapeter(DbDataReader dbDataReader)
    {
        this.DataReaderHelper = new DbDataReaderHelper(dbDataReader);
    }

    public DataReaderAdapeter(DataRow dataRow)
    {
        this.DataReaderHelper = new DataRowReaderHelper(dataRow);
    }

    public object GetValue(string str)
    {
        return this.DataReaderHelper.GetValue(str);
    }

    public object GetValue(int index)
    {
        return this.DataReaderHelper.GetValue(index);
    }

    public void SetDataRow(object obj)
    {
        this.DataReaderHelper.SetDataRow(obj);
    }

    public string[] GetColumnNames()
    {
        return this.DataReaderHelper.GetColumnNames();
    }

    private sealed class DataRowReaderHelper : DataReaderAdapeter.IDataReaderHelper
    {
        private DataRow dataRow;

        public DataRowReaderHelper(DataRow dataRow)
        {
            if (dataRow == null)
            {
                throw new ArgumentNullException("row");
            }
            this.dataRow = dataRow;
        }

        public object GetValue(string str)
        {
            return this.dataRow[str];
        }

        public object GetValue(int index)
        {
            return this.dataRow[index];
        }

        public void SetDataRow(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("row");
            }
            DataRow row = (DataRow) obj;
            if (row.Table != this.dataRow.Table)
            {
                throw new ArithmeticException("新的数据行与构造函数传入的数据行不属于同一个表。");
            }
            this.dataRow = row;
        }

        public string[] GetColumnNames()
        {
            return this.dataRow.Table.GetColumnNames();
        }
    }

    private sealed class DbDataReaderHelper : DataReaderAdapeter.IDataReaderHelper
    {
        private DbDataReader dbDataReader;

        public DbDataReaderHelper(DbDataReader dbDataReader)
        {
            if (dbDataReader == null)
            {
                throw new ArgumentNullException("reader");
            }
            this.dbDataReader = dbDataReader;
        }

        public object GetValue(string str)
        {
            return this.dbDataReader[str];
        }

        public object GetValue(int index)
        {
            return this.dbDataReader[index];
        }

        public void SetDataRow(object obj)
        {
        }

        public string[] GetColumnNames()
        {
            return this.dbDataReader.GetColumnNames();
        }
    }

    private interface IDataReaderHelper
    {
        object GetValue(string str);
        object GetValue(int index);
        void SetDataRow(object obj);
        string[] GetColumnNames();
    }
}

