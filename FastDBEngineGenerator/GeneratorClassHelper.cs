﻿using FastDBEngine;
using FastDBEngineGenerator;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Text;

public static class GeneratorClassHelper
{
    public static string SimpfyWord(this string originWord)
    {
        if ((originWord.Length > 3) && originWord.EndsWith("ies"))
        {
            return (originWord.Substring(0, originWord.Length - 3) + "y");
        }
        if ((originWord.Length > 1) && originWord.EndsWith("s"))
        {
            return originWord.Substring(0, originWord.Length - 1);
        }
        return originWord;
    }

    public static string GenerateModel(string originWord, List<Field> list, CsClassStyle csClassStyle)
    {
        if ((string.IsNullOrEmpty(originWord) || (list == null)) || (list.Count == 0))
        {
            return string.Empty;
        }
        if (csClassStyle.MemberStyle == CsClassMemberStyle.Field)
        {
            return GenerateFieldModel(list, originWord, csClassStyle.SupportWCF);
        }
        if (csClassStyle.MemberStyle == CsClassMemberStyle.AutoProperty)
        {
            return GenerateAutoPropertyModel(list, originWord, csClassStyle.SupportWCF);
        }
        return GeneratePropertyModel(list, originWord, csClassStyle.SupportWCF);
    }

    private static string GenerateFieldModel(List<Field> list, string originword, bool IsContract) 
    {
        StringBuilder builder = new StringBuilder();
        if (IsContract)
        {
            builder.AppendLine("[DataContract]");
        }
        builder.Append("public class ").AppendLine(originword.SimpfyWord()).AppendLine("{");
        foreach (Field field in list)
        {
            if (IsContract)
            {
                builder.AppendLine("\t[DataMember]");
            }
            builder.AppendFormat("\tpublic {0} {1};\r\n", field.GetCsDataType(), field.Name.FilterStr());
        }
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static string GenerateAutoPropertyModel(List<Field> list, string originWord, bool IsContract)
    {
        StringBuilder builder = new StringBuilder();
        if (IsContract)
        {
            builder.AppendLine("[DataContract]");
        }
        builder.Append("public class ").AppendLine(originWord.SimpfyWord()).AppendLine("{");
        foreach (Field field in list)
        {
            if (IsContract)
            {
                builder.AppendLine("\t[DataMember]");
            }
            builder.AppendFormat("\tpublic {0} {1} {{ get; set; }}\r\n", field.GetCsDataType(), field.Name.FilterStr());
        }
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static string GeneratePropertyModel(List<Field> list, string originWord, bool IsContract)
    {
        StringBuilder builder = new StringBuilder();
        if (IsContract)
        {
            builder.AppendLine("[DataContract]");
        }
        builder.Append("public class ").AppendLine(originWord.SimpfyWord()).AppendLine("{");
        foreach (Field field in list)
        {
            builder.AppendFormat("\tprivate {0} _{1};\r\n", field.GetCsDataType(), field.Name.FilterStr());
            if (IsContract)
            {
                builder.AppendLine("\t[DataMember]");
            }
            builder.AppendFormat("\tpublic {0} {1} {{\r\n", field.GetCsDataType(), field.Name.FilterStr());
            builder.AppendFormat("\t\tget {{ return _{0}; }}\r\n", field.Name.FilterStr());
            builder.AppendFormat("\t\tset {{ _{0} = value; }}\r\n", field.Name.FilterStr());
            builder.AppendLine("\t}\r\n");
        }
        builder.AppendLine("}");
        return builder.ToString();
    }

    public static string CallProcMethodGenerator(DbParameter[] dbParameter, string spname, int prefiexIndex, bool isPaging)
    {
        if (string.IsNullOrEmpty(spname))
        {
            throw new ArgumentNullException("spname");
        }
        if ((dbParameter == null) || (dbParameter.Length == 0))
        {
            if (((spname.IndexOf("get", StringComparison.OrdinalIgnoreCase) >= 0) || (spname.IndexOf("query", StringComparison.OrdinalIgnoreCase) >= 0)) || (spname.IndexOf("search", StringComparison.OrdinalIgnoreCase) >= 0))
            {
                return (string.Format("//TModel item = DbHelper.GetDataItem<TModel>(\"{0}\", null);\r\n", spname) + string.Format("List<TModel> list = DbHelper.FillList<TModel>(\"{0}\", null);\r\n", spname));
            }
            return string.Format("DbHelper.ExecuteNonQuery(\"{0}\", null);", spname);
        }
        bool flag = false;
        bool flag2 = false;
        bool flag3 = false;
        bool flag4 = false;
        foreach (DbParameter parameter in dbParameter)
        {
            if ((parameter.Direction == ParameterDirection.Output) || (parameter.Direction == ParameterDirection.InputOutput))
            {
                flag = true;
                if (string.Compare(parameter.ParameterName.Substring(prefiexIndex), DbContext.STR_TotalRecords, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    flag4 = true;
                }
            }
            else
            {
                if (string.Compare(parameter.ParameterName.Substring(prefiexIndex), DbContext.STR_PageIndex, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    flag2 = true;
                }
                if (string.Compare(parameter.ParameterName.Substring(prefiexIndex), DbContext.STR_PageSize, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    flag3 = true;
                }
            }
        }
        string str2 = (isPaging || flag) ? (spname.FilterStr() + "Parameters") : string.Empty;
        StringBuilder builder = new StringBuilder();
        if (!string.IsNullOrEmpty(str2))
        {
            if ((flag2 && flag3) && flag4)
            {
                builder.Append("public class ").Append(str2).AppendLine(" : PagingInfo {");
                foreach (DbParameter parameter in dbParameter)
                {
                    if (((string.Compare(parameter.ParameterName.Substring(prefiexIndex), DbContext.STR_TotalRecords, StringComparison.OrdinalIgnoreCase) != 0) && (string.Compare(parameter.ParameterName.Substring(prefiexIndex), DbContext.STR_PageIndex, StringComparison.OrdinalIgnoreCase) != 0)) && (string.Compare(parameter.ParameterName.Substring(prefiexIndex), DbContext.STR_PageSize, StringComparison.OrdinalIgnoreCase) != 0))
                    {
                        builder.AppendFormat("\tpublic {0} {1} {{ get; set; }}\r\n", TypeConverter.ConvertToClrType(parameter.DbType), parameter.ParameterName.Substring(prefiexIndex));
                    }
                }
            }
            else
            {
                builder.Append("public class ").Append(str2).AppendLine(" {");
                foreach (DbParameter parameter in dbParameter)
                {
                    builder.AppendFormat("\tpublic {0} {1} {{ get; set; }}\r\n", TypeConverter.ConvertToClrType(parameter.DbType), parameter.ParameterName.Substring(prefiexIndex));
                }
            }
            builder.AppendLine("}\r\n");
        }
        builder.AppendFormat("var parameters = new {0}{{\r\n", str2);
        int num2 = 1;
        foreach (DbParameter parameter in dbParameter)
        {
            builder.AppendFormat("\t{0} = xxxxxxx{1}{2}\r\n", parameter.ParameterName.Substring(prefiexIndex), (num2++ < dbParameter.Length) ? "," : "", ((parameter.Direction == ParameterDirection.Output) || (parameter.Direction == ParameterDirection.InputOutput)) ? "\t\t// output" : "");
        }
        builder.AppendLine("};");
        if ((flag && flag2) && flag3)
        {
            builder.AppendFormat("List<TModel> list = DbHelper.FillListPaged<TModel>(\"{0}\", parameters);\r\n", spname);
            builder.AppendFormat("//List<TModel> list = DbHelper.FillList<TModel>(\"{0}\", parameters);\r\n", spname);
        }
        else if (((spname.IndexOf("get", StringComparison.OrdinalIgnoreCase) >= 0) || (spname.IndexOf("query", StringComparison.OrdinalIgnoreCase) >= 0)) || (spname.IndexOf("search", StringComparison.OrdinalIgnoreCase) >= 0))
        {
            builder.AppendFormat("TModel item = DbHelper.GetDataItem<TModel>(\"{0}\", parameters);\r\n", spname);
            builder.AppendFormat("//List<TModel> list = DbHelper.FillList<TModel>(\"{0}\", parameters);\r\n", spname);
        }
        else
        {
            builder.AppendFormat("DbHelper.ExecuteNonQuery(\"{0}\", parameters);", spname);
        }
        return builder.ToString();
    }

    public static string CallXmlCommandMethodGenerator(XmlCommand xmlCommand, string spname, int prefiexIndex, bool isPaging)
    {
        DbCommand command = new SqlCommand();
        return CallProcMethodGenerator(xmlCommand.GetXmlDbParameter(command), spname, prefiexIndex, isPaging);
    }

    internal static int UnicodeCharLength(XmlCommand xmlCommand)
    {
        if (xmlCommand.Parameters.Count != 0)
        {
            XmlCmdParameter parameter = xmlCommand.Parameters[0];
            int num2 = 0;
            while (num2 < parameter.Name.Length)
            {
                if (char.IsLetter(parameter.Name[num2]))
                {
                    break;
                }
                num2++;
            }
            if (num2 < parameter.Name.Length)
            {
                return num2;
            }
        }
        return 0;
    }
}

