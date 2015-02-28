using FastDBEngineGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;

public static class TypeConverter
{
    public static string FilterStr(this string originStr)
    {
        return originStr.Replace(" ", "").Replace("[", "").Replace("]", "").Replace("'", "").Replace("\"", "").Replace("_", "");
    }

    public static string GetShortTypeName(string typeStr)
    {
        string str;
        int num;
        if (typeStr.StartsWith("System."))
        {
            str = typeStr;
            if (str != null)
            {
                if (DictContainer.ShortTypeNameDict == null)
                {
                    Dictionary<string, int> dictionary1 = new Dictionary<string, int>(8);
                    dictionary1.Add("System.Int32", 0);
                    dictionary1.Add("System.Int64", 1);
                    dictionary1.Add("System.String", 2);
                    dictionary1.Add("System.Decimal", 3);
                    dictionary1.Add("System.Boolean", 4);
                    dictionary1.Add("System.Int16", 5);
                    dictionary1.Add("System.Single", 6);
                    dictionary1.Add("System.Double", 7);
                    DictContainer.ShortTypeNameDict = dictionary1;
                }
                if (DictContainer.ShortTypeNameDict.TryGetValue(str, out num))
                {
                    switch (num)
                    {
                        case 0:
                            return "int";

                        case 1:
                            return "long";

                        case 2:
                            return "string";

                        case 3:
                            return "decimal";

                        case 4:
                            return "bool";

                        case 5:
                            return "short";

                        case 6:
                            return "float";

                        case 7:
                            return "double";
                    }
                }
            }
            return typeStr.Substring(7);
        }
        str = typeStr.ToLower();
        if (str != null)
        {
            if (DictContainer.DbToClrTypeContainer == null)
            {
                Dictionary<string, int> dictionary2 = new Dictionary<string, int>(0x1d);
                dictionary2.Add("bigint", 0);
                dictionary2.Add("binary", 1);
                dictionary2.Add("varbinary", 2);
                dictionary2.Add("image", 3);
                dictionary2.Add("timestamp", 4);
                dictionary2.Add("bit", 5);
                dictionary2.Add("char", 6);
                dictionary2.Add("varchar", 7);
                dictionary2.Add("nchar", 8);
                dictionary2.Add("nvarchar", 9);
                dictionary2.Add("text", 10);
                dictionary2.Add("ntext", 11);
                dictionary2.Add("xml", 12);
                dictionary2.Add("date", 13);
                dictionary2.Add("datetime", 14);
                dictionary2.Add("datetime2", 15);
                dictionary2.Add("smalldatetime", 0x10);
                dictionary2.Add("datetimeoffset", 0x11);
                dictionary2.Add("time", 0x12);
                dictionary2.Add("int", 0x13);
                dictionary2.Add("smallint", 20);
                dictionary2.Add("tinyint", 0x15);
                dictionary2.Add("real", 0x16);
                dictionary2.Add("float", 0x17);
                dictionary2.Add("numeric", 0x18);
                dictionary2.Add("decimal", 0x19);
                dictionary2.Add("money", 0x1a);
                dictionary2.Add("smallmoney", 0x1b);
                dictionary2.Add("uniqueidentifier", 0x1c);
                DictContainer.DbToClrTypeContainer = dictionary2;
            }
            if (DictContainer.DbToClrTypeContainer.TryGetValue(str, out num))
            {
                switch (num)
                {
                    case 0:
                        return "long";

                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return "byte[]";

                    case 5:
                        return "bool";

                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                        return "string";

                    case 13:
                    case 14:
                    case 15:
                    case 0x10:
                        return "DateTime";

                    case 0x11:
                        return "DateTimeOffset";

                    case 0x12:
                        return "TimeSpan";

                    case 0x13:
                        return "int";

                    case 20:
                        return "short";

                    case 0x15:
                        return "byte";

                    case 0x16:
                        return "float";

                    case 0x17:
                        return "double";

                    case 0x18:
                    case 0x19:
                    case 0x1a:
                    case 0x1b:
                        return "decimal";

                    case 0x1c:
                        return "Guid";
                }
            }
        }
        return "object";
    }

    public static bool IsKnowedType(string str)
    {
        string key = str;
        if (key != null)
        {
            int num;
            if (DictContainer.ClrTypeDict == null)
            {
                Dictionary<string, int> dictionary1 = new Dictionary<string, int>(0x12);
                dictionary1.Add("bigint", 0);
                dictionary1.Add("bit", 1);
                dictionary1.Add("date", 2);
                dictionary1.Add("time", 3);
                dictionary1.Add("datetime", 4);
                dictionary1.Add("datetime2", 5);
                dictionary1.Add("smalldatetime", 6);
                dictionary1.Add("datetimeoffset", 7);
                dictionary1.Add("int", 8);
                dictionary1.Add("smallint", 9);
                dictionary1.Add("tinyint", 10);
                dictionary1.Add("float", 11);
                dictionary1.Add("real", 12);
                dictionary1.Add("numeric", 13);
                dictionary1.Add("decimal", 14);
                dictionary1.Add("money", 15);
                dictionary1.Add("smallmoney", 0x10);
                dictionary1.Add("uniqueidentifier", 0x11);
                DictContainer.ClrTypeDict = dictionary1;
            }
            if (DictContainer.ClrTypeDict.TryGetValue(key, out num))
            {
                switch (num)
                {
                    case 0:
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                    case 5:
                    case 6:
                    case 7:
                    case 8:
                    case 9:
                    case 10:
                    case 11:
                    case 12:
                    case 13:
                    case 14:
                    case 15:
                    case 0x10:
                    case 0x11:
                        return true;
                }
            }
        }
        return false;
    }

    public static string ConvertToDbTypeString(this Field field)
    {
        string dataType = field.DataType;
        if (dataType != null)
        {
            int num;
            if (DictContainer.ClrTypeToDBTypeStringDict == null)
            {
                Dictionary<string, int> dictionary1 = new Dictionary<string, int>(0x1d);
                dictionary1.Add("bigint", 0);
                dictionary1.Add("binary", 1);
                dictionary1.Add("varbinary", 2);
                dictionary1.Add("image", 3);
                dictionary1.Add("bit", 4);
                dictionary1.Add("char", 5);
                dictionary1.Add("varchar", 6);
                dictionary1.Add("nchar", 7);
                dictionary1.Add("nvarchar", 8);
                dictionary1.Add("text", 9);
                dictionary1.Add("ntext", 10);
                dictionary1.Add("xml", 11);
                dictionary1.Add("date", 12);
                dictionary1.Add("time", 13);
                dictionary1.Add("datetime", 14);
                dictionary1.Add("datetime2", 15);
                dictionary1.Add("smalldatetime", 0x10);
                dictionary1.Add("timestamp", 0x11);
                dictionary1.Add("datetimeoffset", 0x12);
                dictionary1.Add("int", 0x13);
                dictionary1.Add("smallint", 20);
                dictionary1.Add("tinyint", 0x15);
                dictionary1.Add("float", 0x16);
                dictionary1.Add("real", 0x17);
                dictionary1.Add("numeric", 0x18);
                dictionary1.Add("decimal", 0x19);
                dictionary1.Add("money", 0x1a);
                dictionary1.Add("smallmoney", 0x1b);
                dictionary1.Add("uniqueidentifier", 0x1c);
                DictContainer.ClrTypeToDBTypeStringDict = dictionary1;
            }
            if (DictContainer.ClrTypeToDBTypeStringDict.TryGetValue(dataType, out num))
            {
                switch (num)
                {
                    case 0:
                        return "bigint";

                    case 1:
                        return string.Format("binary({0})", (field.Length == -1) ? "max" : field.Length.ToString());

                    case 2:
                        return string.Format("varbinary({0})", (field.Length == -1) ? "max" : field.Length.ToString());

                    case 3:
                        return "image";

                    case 4:
                        return "bit";

                    case 5:
                        return string.Format("char({0})", (field.Length == -1) ? "max" : field.Length.ToString());

                    case 6:
                        return string.Format("varchar({0})", (field.Length == -1) ? "max" : field.Length.ToString());

                    case 7:
                        return string.Format("nchar({0})", (field.Length == -1) ? "max" : field.Length.ToString());

                    case 8:
                        return string.Format("nvarchar({0})", (field.Length == -1) ? "max" : field.Length.ToString());

                    case 9:
                        return "text";

                    case 10:
                        return "ntext";

                    case 11:
                        return "xml";

                    case 12:
                        return "date";

                    case 13:
                        return "time";

                    case 14:
                        return "datetime";

                    case 15:
                        return "datetime2";

                    case 0x10:
                        return "smalldatetime";

                    case 0x11:
                        return "timestamp";

                    case 0x12:
                        return "datetimeoffset";

                    case 0x13:
                        return "int";

                    case 20:
                        return "smallint";

                    case 0x15:
                        return "tinyint";

                    case 0x16:
                        return "float";

                    case 0x17:
                        return "real";

                    case 0x18:
                        return string.Format("numeric({0}, {1})", field.Precision, field.int_0);

                    case 0x19:
                        return string.Format("decimal({0}, {1})", field.Precision, field.int_0);

                    case 0x1a:
                        return "money";

                    case 0x1b:
                        return "smallmoney";

                    case 0x1c:
                        return "uniqueidentifier";
                }
            }
        }
        return "object";
    }

    public static DbType ConvertToDbType(this Field field)
    {
        string dataType = field.DataType;
        if (dataType != null)
        {
            int num;
            if (DictContainer.ClrTypeToDBTypeDict == null)
            {
                Dictionary<string, int> dictionary1 = new Dictionary<string, int>(0x1c);
                dictionary1.Add("bigint", 0);
                dictionary1.Add("binary", 1);
                dictionary1.Add("varbinary", 2);
                dictionary1.Add("image", 3);
                dictionary1.Add("timestamp", 4);
                dictionary1.Add("bit", 5);
                dictionary1.Add("char", 6);
                dictionary1.Add("varchar", 7);
                dictionary1.Add("text", 8);
                dictionary1.Add("nchar", 9);
                dictionary1.Add("nvarchar", 10);
                dictionary1.Add("ntext", 11);
                dictionary1.Add("xml", 12);
                dictionary1.Add("date", 13);
                dictionary1.Add("datetime2", 14);
                dictionary1.Add("time", 15);
                dictionary1.Add("datetime", 0x10);
                dictionary1.Add("smalldatetime", 0x11);
                dictionary1.Add("int", 0x12);
                dictionary1.Add("smallint", 0x13);
                dictionary1.Add("tinyint", 20);
                dictionary1.Add("real", 0x15);
                dictionary1.Add("float", 0x16);
                dictionary1.Add("numeric", 0x17);
                dictionary1.Add("decimal", 0x18);
                dictionary1.Add("money", 0x19);
                dictionary1.Add("smallmoney", 0x1a);
                dictionary1.Add("uniqueidentifier", 0x1b);
                DictContainer.ClrTypeToDBTypeDict = dictionary1;
            }
            if (DictContainer.ClrTypeToDBTypeDict.TryGetValue(dataType, out num))
            {
                switch (num)
                {
                    case 0:
                        return DbType.Int64;

                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        return DbType.Binary;

                    case 5:
                        return DbType.Boolean;

                    case 6:
                    case 7:
                    case 8:
                        return DbType.AnsiString;

                    case 9:
                    case 10:
                    case 11:
                        return DbType.String;

                    case 12:
                        return DbType.Xml;

                    case 13:
                        return DbType.Date;

                    case 14:
                        return DbType.DateTime2;

                    case 15:
                        return DbType.Time;

                    case 0x10:
                    case 0x11:
                        return DbType.DateTime;

                    case 0x12:
                        return DbType.Int32;

                    case 0x13:
                        return DbType.Int16;

                    case 20:
                        return DbType.Byte;

                    case 0x15:
                        return DbType.Single;

                    case 0x16:
                    case 0x17:
                        return DbType.Double;

                    case 0x18:
                        return DbType.Decimal;

                    case 0x19:
                    case 0x1a:
                        return DbType.Currency;

                    case 0x1b:
                        return DbType.Guid;
                }
            }
        }
        return DbType.Object;
    }

    public static string ConvertToClrType(DbType dbType)
    {
        switch (dbType)
        {
            case DbType.AnsiString:
            case DbType.String:
            case DbType.AnsiStringFixedLength:
            case DbType.StringFixedLength:
            case DbType.Xml:
                return "string";

            case DbType.Binary:
                return "byte[]";

            case DbType.Byte:
                return "byte";

            case DbType.Boolean:
                return "bool";

            case DbType.Currency:
            case DbType.Decimal:
                return "decimal";

            case DbType.Date:
            case DbType.DateTime:
            case DbType.DateTime2:
                return "DateTime";

            case DbType.Double:
            case DbType.VarNumeric:
                return "double";

            case DbType.Guid:
                return "Guid";

            case DbType.Int16:
                return "short";

            case DbType.Int32:
                return "int";

            case DbType.Int64:
                return "long";

            case DbType.Object:
                return "object";

            case DbType.SByte:
                return "sbyte";

            case DbType.Single:
                return "float";

            case DbType.Time:
                return "TimeSpan";

            case DbType.UInt16:
                return "ushort";

            case DbType.UInt32:
                return "uint";

            case DbType.UInt64:
                return "ulong";

            case DbType.DateTimeOffset:
                return "DateTimeOffset";
        }
        return "object";
    }
}

