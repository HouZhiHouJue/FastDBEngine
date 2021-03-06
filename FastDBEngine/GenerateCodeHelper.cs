﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

internal static class GenerateCodeHelper
{
    internal static readonly int dataReaderState = 1;
    internal static readonly int dataTableState = 2;
    internal static readonly int dataReaderPagedState = 3;
    internal static readonly int dataTablePagedState = 4;
    internal static readonly string string_0 = "FastDBEngineAutoCode";
    private static readonly string string_1 = "___";

    private static string GetGuid()
    {
        return ("f" + Guid.NewGuid().ToString("N"));
    }

    private static GenerateParametersInfo GetGenerateParametersInfo(Type type)
    {
        return new GenerateParametersInfo { ModelType = type, NamespaceName = string_0, ClassName = GetGuid(), NameIndexClassInfo = GetGuid(), AssignNameIndexClassValueMethod = GetGuid(), DbDataReaderGenerateModelMethod = GetGuid(), DbDataReaderPagedGenerateModelMethod = GetGuid(), DataTableGenerateModelMethod = GetGuid(), DataTablePagedGenerateModelMethod = GetGuid(), AssignModelValueFromReaderMethod = GetGuid(), AssignModelValueFromDataRowMethod = GetGuid(), GenerateModelMethod = GetGuid(), GetModelPropertyValueMethod = GetGuid(), SetModelPropertyValueByChangeTypeMethod = GetGuid(), NameIndexDict = new Dictionary<string, string>() };
    }

    private static string LoadDatatableMethod(GenerateParametersInfo generateParametersInfo)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("public static {0} {1}(DataTable table) {{\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString(), generateParametersInfo.DataTableGenerateModelMethod);
        builder.AppendLine("if( table.Rows.Count == 0 ) return null;");
        builder.AppendFormat("{0} xx = {1}(table);\r\n", generateParametersInfo.NameIndexClassInfo, generateParametersInfo.AssignNameIndexClassValueMethod);
        builder.AppendFormat("{0} item = new {0}();\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString());
        builder.AppendLine("DataRow row = table.Rows[0];");
        builder.AppendFormat("{0}(row, xx, item);\r\n", generateParametersInfo.AssignModelValueFromDataRowMethod);
        builder.AppendLine("return item;");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static string LoadListMethod(GenerateParametersInfo generateParametersInfo)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("public static List<{0}> {1}(DataTable table, int capacity) {{\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString(), generateParametersInfo.DataTablePagedGenerateModelMethod);
        builder.AppendLine("if( table.Rows.Count == 0 )");
        builder.AppendFormat("return new List<{0}>(0);\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString());
        builder.AppendFormat("{0} xx = {1}(table);\r\n", generateParametersInfo.NameIndexClassInfo, generateParametersInfo.AssignNameIndexClassValueMethod);
        builder.AppendFormat("List<{0}> list = new List<{0}>(capacity);\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString());
        builder.AppendLine("foreach( DataRow row in table.Rows ) {");
        builder.AppendFormat("{0} item = new {0}();\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString());
        builder.AppendFormat("{0}(row, xx, item);\r\n", generateParametersInfo.AssignModelValueFromDataRowMethod);
        builder.AppendLine("list.Add(item);");
        builder.AppendLine("}");
        builder.AppendLine("return list;");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static string LoadObjMethod(GenerateParametersInfo generateParametersInfo)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("private static {0} {1}(object dataSource)\r\n", generateParametersInfo.NameIndexClassInfo, generateParametersInfo.AssignNameIndexClassValueMethod);
        builder.AppendLine("{");
        builder.AppendLine("string[] cc = null;");
        builder.AppendLine("DbDataReader reader = dataSource as DbDataReader;");
        builder.AppendLine("if( reader != null ) cc = FastDBEngine.DbHelper.GetColumnNames(reader);");
        builder.AppendLine("else cc = FastDBEngine.DbHelper.GetColumnNames((DataTable)dataSource);");
        builder.AppendLine();
        builder.AppendFormat("{0} xx = new {0}();\r\n", generateParametersInfo.NameIndexClassInfo);
        FindIndexMethod(generateParametersInfo, generateParametersInfo.ModelType, builder, string.Empty, string.Empty);
        builder.AppendLine("return xx;");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static void FindIndexMethod(GenerateParametersInfo generateParametersInfo, Type type, StringBuilder stringBuilder, string string_2, string string_3)
    {
        MeberOperationHelperContainer class2 = type.GetMeberOperationHelperContainer(false);
        foreach (KeyValuePair<string, MeberOperationHelper> pair in class2.GetDict())
        {
            if (!(pair.Value.GetDbColumnAttribute().IgnoreLoad || pair.Value.GetDbColumnAttribute().HasPrefix))
            {
                stringBuilder.AppendFormat("xx.{0} = FastDBEngine.MyExtensions.FindIndex(cc, \"{1}\");\r\n", generateParametersInfo.NameIndexDict[string_2 + pair.Key], string_3 + pair.Value.GetDbColumnAttribute().Alias);
            }
        }
        foreach (KeyValuePair<string, MeberOperationHelper> pair in class2.GetDict())
        {
            if (!(pair.Value.GetDbColumnAttribute().IgnoreLoad || !pair.Value.GetDbColumnAttribute().HasPrefix))
            {
                FindIndexMethod(generateParametersInfo, pair.Value.GetPropertyType(), stringBuilder, string_2 + pair.Key + string_1, string_3 + pair.Value.GetDbColumnAttribute().SubItemPrefix);
            }
        }
    }

    private static string GenerateMethod(GenerateParametersInfo parameters)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("private static void {0}(DbDataReader reader, {1} xx, {2} item) {{\r\n", parameters.AssignModelValueFromReaderMethod, parameters.NameIndexClassInfo, parameters.ModelType.GetGenericArgumentsString());
        GenerateMethodBody(parameters, parameters.ModelType, builder, string.Empty, "item.");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static string GetReaderStr(MeberOperationHelper meberOperationHelper)
    {
        Type type = meberOperationHelper.GetPropertyType();
        if (type == TypeInfo.TypeInt)
        {
            return "reader.GetInt32";
        }
        if (type == TypeInfo.TypeString)
        {
            return "reader.GetString";
        }
        if (type == TypeInfo.TypeDatetime)
        {
            return "reader.GetDateTime";
        }
        if (type == TypeInfo.TypeDecimal)
        {
            return "reader.GetDecimal";
        }
        if (type == TypeInfo.TypeBool)
        {
            return "reader.GetBoolean";
        }
        if (type == TypeInfo.TypeLong)
        {
            return "reader.GetInt64";
        }
        if (type == TypeInfo.TypeShort)
        {
            return "reader.GetInt16";
        }
        if (type == TypeInfo.TypeFloat)
        {
            return "reader.GetFloat";
        }
        if (type == TypeInfo.TypeDouble)
        {
            return "reader.GetDouble";
        }
        if (type == TypeInfo.TypeGuid)
        {
            return "reader.GetGuid";
        }
        if (type == TypeInfo.TypeByte)
        {
            return "reader.GetByte";
        }
        return string.Format("({0})reader.GetValue", meberOperationHelper.GetPropertyType().GetGenericArgumentsString());
    }

    private static void GenerateMethodBody(GenerateParametersInfo generateParametersInfo, Type type, StringBuilder stringBuilder, string prefix, string modelName)
    {
        MeberOperationHelperContainer meberOperationHelperContainer = type.GetMeberOperationHelperContainer(false);
        //stringBuilder_0.AppendLine("try");
        //stringBuilder_0.AppendLine("{ ");
        // stringBuilder_0.AppendLine("System.Console.WriteLine(\"111111111111111111111111111\");");
        foreach (KeyValuePair<string, MeberOperationHelper> pair in meberOperationHelperContainer.GetDict())
        {
            //   stringBuilder_0.AppendFormat("System.Console.WriteLine(\"{0}\");\r\n", pair.Key);
            if (!pair.Value.GetDbColumnAttribute().IgnoreLoad && !pair.Value.GetDbColumnAttribute().HasPrefix)
            {
                stringBuilder.AppendFormat("if( xx.{0} >= 0 ) {{\r\n", generateParametersInfo.NameIndexDict[prefix + pair.Key]);
                if (pair.Value.GetPropertyType() == TypeInfo.TypeChar)
                {
                    stringBuilder.AppendFormat("{0}{1} = reader.GetString(xx.{2})[0];\r\n", modelName, pair.Key, generateParametersInfo.NameIndexDict[prefix + pair.Key]);
                }
                else if (pair.Value.GetPropertyType() == typeof(char?))
                {
                    stringBuilder.AppendFormat("object val = reader.GetValue(xx.{0});\r\n", generateParametersInfo.NameIndexDict[prefix + pair.Key]);
                    stringBuilder.AppendLine("if( val != null && DBNull.Value.Equals(val) == false ) {");
                    stringBuilder.AppendLine("string str = val.ToString();");
                    stringBuilder.AppendLine("if( str.Length > 0 ) ");
                    stringBuilder.AppendFormat("{0}{1} = str[0];\r\n", modelName, pair.Key);
                    stringBuilder.AppendLine("}");
                }
                else if (!(!pair.Value.GetPropertyType().IsValueType || pair.Value.GetPropertyType().IsNullable()))
                {
                    if (!pair.Value.GetPropertyType().FullName.Contains("System.Int32"))
                        stringBuilder.AppendFormat("{0}{1} = {2}(xx.{3});\r\n", new object[] { modelName, pair.Key, GetReaderStr(pair.Value), generateParametersInfo.NameIndexDict[prefix + pair.Key] });
                    else
                        stringBuilder.AppendFormat("{0}{1} =Convert.ToInt32(reader.GetDecimal(xx.{2}));\r\n", new object[] { modelName, pair.Key, generateParametersInfo.NameIndexDict[prefix + pair.Key] });//, GetReaderStr(pair.Value)
                }
                else
                {
                    stringBuilder.AppendFormat("object val = reader.GetValue(xx.{0});\r\n", generateParametersInfo.NameIndexDict[prefix + pair.Key]);
                    stringBuilder.AppendLine("if( val != null && DBNull.Value.Equals(val) == false )");
                    if (!pair.Value.GetPropertyType().FullName.Contains("System.Int32"))
                        stringBuilder.AppendFormat("{0}{1} = ({2})(val);\r\n", modelName, pair.Key, pair.Value.GetPropertyType().IsNullable() ? pair.Value.GetPropertyType().GetFirstGenericArgumentsOrDefault().GetGenericArgumentsString() : pair.Value.GetPropertyType().GetGenericArgumentsString());
                    else
                        stringBuilder.AppendFormat("{0}{1} = Convert.ToInt32((val));\r\n", modelName, pair.Key);//, pair.Value.method_3().IsNullable() ? pair.Value.method_3().smethod_3().smethod_7() : pair.Value.method_3().smethod_7());
                }
                stringBuilder.AppendLine("}");
            }
        }
        //stringBuilder_0.AppendLine(" } ");
        //stringBuilder_0.AppendLine("catch(Exception ex) { ");
        //stringBuilder_0.AppendLine(" System.Diagnostics.Trace.WriteLine(ex.StackTrace);");
        //stringBuilder_0.AppendLine(" } ");

        foreach (KeyValuePair<string, MeberOperationHelper> pair in meberOperationHelperContainer.GetDict())
        {
            if (!(pair.Value.GetDbColumnAttribute().IgnoreLoad || !pair.Value.GetDbColumnAttribute().HasPrefix))
            {
                stringBuilder.AppendFormat("{0}{1} = new {2}();\r\n", modelName, pair.Key, pair.Value.GetPropertyType().GetGenericArgumentsString());
                GenerateMethodBody(generateParametersInfo, pair.Value.GetPropertyType(), stringBuilder, prefix + pair.Key + string_1, modelName + pair.Key + ".");
            }
        }
    }

    private static string LoadDatarowMethodWrapper(GenerateParametersInfo generateParametersInfo)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("private static void {0}(DataRow row, {1} xx, {2} item) {{\r\n", generateParametersInfo.AssignModelValueFromDataRowMethod, generateParametersInfo.NameIndexClassInfo, generateParametersInfo.ModelType.GetGenericArgumentsString());
        LoadDatarowMethod(generateParametersInfo, generateParametersInfo.ModelType, builder, string.Empty, "item.");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static void LoadDatarowMethod(GenerateParametersInfo generateParametersInfo, Type type, StringBuilder stringBuilder, string string_2, string string_3)
    {
        MeberOperationHelperContainer meberOperationHelperContainer = type.GetMeberOperationHelperContainer(false);
        foreach (KeyValuePair<string, MeberOperationHelper> pair in meberOperationHelperContainer.GetDict())
        {
            if (!pair.Value.GetDbColumnAttribute().IgnoreLoad && !pair.Value.GetDbColumnAttribute().HasPrefix)
            {
                stringBuilder.AppendFormat("if( xx.{0} >= 0 ) {{\r\n", generateParametersInfo.NameIndexDict[string_2 + pair.Key]);
                if (!(!pair.Value.GetPropertyType().IsValueType || pair.Value.GetPropertyType().IsNullable()))
                {
                    stringBuilder.AppendFormat("{0}{1} = ({2})(row[xx.{3}]);\r\n", new object[] { string_3, pair.Key, pair.Value.GetPropertyType().GetGenericArgumentsString(), generateParametersInfo.NameIndexDict[string_2 + pair.Key] });
                }
                else
                {
                    stringBuilder.AppendFormat("object val = row[xx.{0}];\r\n", generateParametersInfo.NameIndexDict[string_2 + pair.Key]);
                    stringBuilder.AppendLine("if( val != null && DBNull.Value.Equals(val) == false )");
                    stringBuilder.AppendFormat("{0}{1} = ({2})(val);\r\n", string_3, pair.Key, pair.Value.GetPropertyType().IsNullable() ? pair.Value.GetPropertyType().GetFirstGenericArgumentsOrDefault().GetGenericArgumentsString() : pair.Value.GetPropertyType().GetGenericArgumentsString());
                }
                stringBuilder.AppendLine("}");
            }
        }
        foreach (KeyValuePair<string, MeberOperationHelper> pair in meberOperationHelperContainer.GetDict())
        {
            if (!(pair.Value.GetDbColumnAttribute().IgnoreLoad || !pair.Value.GetDbColumnAttribute().HasPrefix))
            {
                stringBuilder.AppendFormat("{0}{1} = new {2}();\r\n", string_3, pair.Key, pair.Value.GetPropertyType().GetGenericArgumentsString());
                LoadDatarowMethod(generateParametersInfo, pair.Value.GetPropertyType(), stringBuilder, string_2 + pair.Key + string_1, string_3 + pair.Key + ".");
            }
        }
    }

    public static List<GenerateCodeInfo> GetGenerateCodeInfoList(List<Type> typeList)
    {
        if ((typeList == null) || (typeList.Count == 0))
        {
            return new List<GenerateCodeInfo>();
        }
        List<GenerateCodeInfo> list = new List<GenerateCodeInfo>(typeList.Count);
        foreach (Type type in typeList)
        {
            GenerateCodeInfo class2 = new GenerateCodeInfo
            {
                GenerateParameters = GetGenerateParametersInfo(type)
            };
            class2.ClassCodeString = GenerateClass(class2.GenerateParameters);
            list.Add(class2);
        }
        return list;
    }

    private static string GenerateClass(GenerateParametersInfo generateParametersInfo)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendLine("using System;");
        builder.AppendLine("using System.Collections.Generic;");
        builder.AppendLine("using System.Text;");
        builder.AppendLine("using System.Data;");
        builder.AppendLine("using System.Data.Common;");
        builder.AppendLine("using FastDBEngine;");
        builder.AppendLine("");
        builder.AppendLine("");
        builder.Append("namespace ").AppendLine(generateParametersInfo.NamespaceName);
        builder.AppendLine("{");
        builder.AppendFormat("public static class {0}\r\n", generateParametersInfo.ClassName);
        builder.AppendLine("{");
        if (generateParametersInfo.ModelType.HasPublicConstructor())
        {
            builder.AppendLine(GenerateReaderMethod(generateParametersInfo));
            builder.AppendLine(GenerateClassHead(generateParametersInfo));
            builder.AppendLine(GetValueFromReaderMethod(generateParametersInfo));
            builder.AppendLine(GetListMethod(generateParametersInfo));
            builder.AppendLine(LoadDatatableMethod(generateParametersInfo));
            builder.AppendLine(LoadListMethod(generateParametersInfo));
            builder.AppendLine(LoadObjMethod(generateParametersInfo));
            builder.AppendLine(GenerateMethod(generateParametersInfo));
            builder.AppendLine(LoadDatarowMethodWrapper(generateParametersInfo));
        }
        else
        {
            builder.AppendLine(GenerateMethodHead(generateParametersInfo));
        }
        builder.AppendLine(GetValueByChangeTypeMethod(generateParametersInfo));
        builder.AppendLine("}\r\n}");
        return builder.ToString();
    }

    private static string GetValueByChangeTypeMethod(GenerateParametersInfo generateParametersInfo)
    {
        FieldInfo[] fields = generateParametersInfo.ModelType.GetFields(BindingFlags.Public | BindingFlags.Instance);
        PropertyInfo[] properties = generateParametersInfo.ModelType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("public static object {0}(object obj, string name){{\r\n", generateParametersInfo.GetModelPropertyValueMethod);
        builder.AppendFormat("{0} item = ({0})obj;\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString());
        builder.AppendLine("switch( name ) {");
        foreach (FieldInfo info2 in fields)
        {
            builder.AppendFormat("case \"{0}\": ", info2.Name);
            if (info2.FieldType.IsNullable())
            {
                builder.AppendFormat("{{if (item.{0}.HasValue) return item.{0}.Value; else return null;}}\r\n", info2.Name);
            }
            else
            {
                builder.AppendFormat("return item.{0};\r\n", info2.Name);
            }
        }
        foreach (PropertyInfo info in properties)
        {
            if (!info.NeedExtraParameters() && info.CanRead)
            {
                builder.AppendFormat("case \"{0}\": ", info.Name);
                if (info.PropertyType.IsNullable())
                {
                    builder.AppendFormat("{{if (item.{0}.HasValue) return item.{0}.Value; else return null;}}\r\n", info.Name);
                }
                else
                {
                    builder.AppendFormat("return item.{0};\r\n", info.Name);
                }
            }
        }
        builder.AppendLine("default: throw new ArgumentOutOfRangeException(\"name\", string.Format(\"Property or field {0} not found.\", name)); }");
        builder.AppendLine("}");
        builder.AppendFormat("public static void {0}(object obj, string name, object value){{\r\n", generateParametersInfo.SetModelPropertyValueByChangeTypeMethod);
        builder.AppendFormat("{0} item = ({0})obj;\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString());
        builder.AppendLine("switch( name ) {");
        foreach (FieldInfo info2 in fields)
        {
            builder.AppendFormat("case \"{0}\": ", info2.Name);
            if (info2.FieldType.IsNullable())
            {
                builder.AppendFormat("item.{0} = ({1})Convert.ChangeType(value, typeof({1})); break;\r\n", info2.Name, info2.FieldType.GetFirstGenericArgumentsOrDefault().GetGenericArgumentsString());
            }
            else if (!(!info2.FieldType.IsValueType || info2.FieldType.IsEnum))
            {
                builder.AppendFormat("item.{0} = ({1})Convert.ChangeType(value, typeof({1})); break;\r\n", info2.Name, info2.FieldType.GetGenericArgumentsString());
            }
            else
            {
                builder.AppendFormat("item.{0} = ({1})value; break;\r\n", info2.Name, info2.FieldType.GetGenericArgumentsString());
            }
        }
        foreach (PropertyInfo info in properties)
        {
            if (!info.NeedExtraParameters() && info.CanWrite)
            {
                builder.AppendFormat("case \"{0}\": ", info.Name);
                if (info.PropertyType.IsNullable())
                {
                    builder.AppendFormat("item.{0} = ({1})Convert.ChangeType(value, typeof({1})); break;\r\n", info.Name, info.PropertyType.GetFirstGenericArgumentsOrDefault().GetGenericArgumentsString());
                }
                else if (!(!info.PropertyType.IsValueType || info.PropertyType.IsEnum))
                {
                    builder.AppendFormat("item.{0} = ({1})Convert.ChangeType(value, typeof({1})); break;\r\n", info.Name, info.PropertyType.GetGenericArgumentsString());
                }
                else
                {
                    builder.AppendFormat("item.{0} = ({1})value; break;\r\n", info.Name, info.PropertyType.GetGenericArgumentsString());
                }
            }
        }
        builder.AppendLine("default: throw new ArgumentOutOfRangeException(\"name\", string.Format(\"Property or field {0} not found.\", name)); }");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static string GenerateReaderMethod(GenerateParametersInfo generateParametersInfo)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("public static object {0}(int key, object dataSource, int state){{\r\n", generateParametersInfo.GenerateModelMethod);
        builder.AppendLine("switch( key ) {");
        builder.AppendLine("case 1:");
        builder.AppendFormat("return {0}((DbDataReader)dataSource);\r\n", generateParametersInfo.DbDataReaderGenerateModelMethod);
        builder.AppendLine("case 2:");
        builder.AppendFormat("return {0}((DataTable)dataSource);\r\n", generateParametersInfo.DataTableGenerateModelMethod);
        builder.AppendLine("case 3:");
        builder.AppendFormat("return {0}((DbDataReader)dataSource, state);\r\n", generateParametersInfo.DbDataReaderPagedGenerateModelMethod);
        builder.AppendLine("case 4:");
        builder.AppendFormat("return {0}((DataTable)dataSource, state);\r\n", generateParametersInfo.DataTablePagedGenerateModelMethod);
        builder.AppendLine("default:");
        builder.AppendLine("throw new ArgumentOutOfRangeException(\"key\");");
        builder.AppendLine("}");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static string GenerateMethodHead(GenerateParametersInfo generateParametersInfo)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("public static object {0}(int key, object dataSource, int state){{\r\n", generateParametersInfo.GenerateModelMethod);
        builder.AppendLine("throw new NotSupportedException();");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static string GenerateClassHead(GenerateParametersInfo generateParametersInfo)
    {
        StringBuilder builder = new StringBuilder();
        builder.Append("private sealed class ").Append(generateParametersInfo.NameIndexClassInfo).AppendLine(" {");
        SetGuidMethod(generateParametersInfo.ModelType, generateParametersInfo.NameIndexDict, string.Empty);
        foreach (KeyValuePair<string, string> pair in generateParametersInfo.NameIndexDict)
        {
            builder.AppendFormat("public int {0};\r\n", pair.Value);
        }
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static void SetGuidMethod(Type type, Dictionary<string, string> dict, string string_2)
    {
        MeberOperationHelperContainer class2 = type.GetMeberOperationHelperContainer(false);
        foreach (KeyValuePair<string, MeberOperationHelper> pair in class2.GetDict())
        {
            if (!(pair.Value.GetDbColumnAttribute().IgnoreLoad || pair.Value.GetDbColumnAttribute().HasPrefix))
            {
                dict[string_2 + pair.Key] = GetGuid();
            }
        }
        foreach (KeyValuePair<string, MeberOperationHelper> pair in class2.GetDict())
        {
            if (!(pair.Value.GetDbColumnAttribute().IgnoreLoad || !pair.Value.GetDbColumnAttribute().HasPrefix))
            {
                SetGuidMethod(pair.Value.GetPropertyType(), dict, string_2 + pair.Key + string_1);
            }
        }
    }

    private static string GetValueFromReaderMethod(GenerateParametersInfo generateParametersInfo)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("public static {0} {1}(DbDataReader reader) {{\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString(), generateParametersInfo.DbDataReaderGenerateModelMethod);
        builder.AppendFormat("{0} xx = {1}(reader);\r\n", generateParametersInfo.NameIndexClassInfo, generateParametersInfo.AssignNameIndexClassValueMethod);
        builder.AppendLine("if( reader.Read() ) {");
        builder.AppendFormat("{0} item = new {0}();\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString());
        builder.AppendFormat("{0}(reader, xx, item);\r\n", generateParametersInfo.AssignModelValueFromReaderMethod);
        builder.AppendLine("return item;");
        builder.AppendLine("}");
        builder.AppendLine("return null;");
        builder.AppendLine("}");
        return builder.ToString();
    }

    private static string GetListMethod(GenerateParametersInfo generateParametersInfo)
    {
        StringBuilder builder = new StringBuilder();
        builder.AppendFormat("public static List<{0}> {1}(DbDataReader reader, int capacity) {{\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString(), generateParametersInfo.DbDataReaderPagedGenerateModelMethod);
        builder.AppendFormat("{0} xx = {1}(reader);\r\n", generateParametersInfo.NameIndexClassInfo, generateParametersInfo.AssignNameIndexClassValueMethod);
        builder.AppendFormat("List<{0}> list = new List<{0}>(capacity);\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString());
        builder.AppendLine("while( reader.Read() ) {");
        builder.AppendFormat("{0} item = new {0}();\r\n", generateParametersInfo.ModelType.GetGenericArgumentsString());
        builder.AppendFormat("{0}(reader, xx, item);\r\n", generateParametersInfo.AssignModelValueFromReaderMethod);
        builder.AppendLine("list.Add(item);");
        builder.AppendLine("}");
        builder.AppendLine("return list;");
        builder.AppendLine("}");
        return builder.ToString();
    }
}

