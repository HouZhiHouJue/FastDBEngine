using System;
using System.Collections.Generic;

internal sealed class GenerateParametersInfo
{
    public Dictionary<string, string> NameIndexDict;
    public string NamespaceName;
    public string ClassName;
    public string DataTablePagedGenerateModelMethod;
    public string AssignModelValueFromReaderMethod;
    public string AssignModelValueFromDataRowMethod;
    public string GenerateModelMethod;
    public string GetModelPropertyValueMethod;
    public string SetModelPropertyValueByChangeTypeMethod;
    public string NameIndexClassInfo;
    public string AssignNameIndexClassValueMethod;
    public string DbDataReaderGenerateModelMethod;
    public string DbDataReaderPagedGenerateModelMethod;
    public string DataTableGenerateModelMethod;
    public Type ModelType;
}

