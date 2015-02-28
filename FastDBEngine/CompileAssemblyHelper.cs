using FastDBEngine;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

internal static class CompileAssemblyHelper
{
    [CompilerGenerated]
    private static Func<GenerateCodeInfo, string> funcClassString;
    [CompilerGenerated]
    private static Func<GenerateCodeInfo, Type> func;

    private static void GetAllAssembly(Assembly assembly, StringCollection stringCollection)
    {
        string location = assembly.Location;
        if (!stringCollection.Contains(location))
        {
            stringCollection.Add(location);
        }
    }

    public static Assembly CompileAssembly(List<GenerateCodeInfo> list)
    {
        if ((list == null) || (list.Count == 0))
        {
            throw new ArgumentException("list is null or empty.");
        }
        CompilerParameters options = new CompilerParameters {
            GenerateExecutable = false,
            GenerateInMemory = true
        };
        Assembly[] assemblyArray = AssemblyHelper.GetAppAssemblys();
        foreach (Assembly assembly in assemblyArray)
        {
            if (assembly.FullName.StartsWith("Microsoft.GeneratedCode"))
                continue;
            GetAllAssembly(assembly, options.ReferencedAssemblies);
        }
        GetAllAssembly(typeof(string).Assembly, options.ReferencedAssemblies);
        GetAllAssembly(typeof(DataTable).Assembly, options.ReferencedAssemblies);
        GetAllAssembly(typeof(Queryable).Assembly, options.ReferencedAssemblies);
        GetAllAssembly(typeof(DbContext).Assembly, options.ReferencedAssemblies);
        if (funcClassString == null)
        {
            funcClassString = new Func<GenerateCodeInfo, string>(CompileAssemblyHelper.GetClassCodeString);
        }
        string[] sources = Enumerable.Select<GenerateCodeInfo, string>(list, funcClassString).ToArray<string>();
        CompilerResults results = ((CSharpCodeProvider) CodeDomProvider.CreateProvider("CSharp")).CompileAssemblyFromSource(options, sources);
        if ((results.Errors == null) || !results.Errors.HasErrors)
        {
            return results.CompiledAssembly;
        }
        if (func == null)
        {
            func = new Func<GenerateCodeInfo, Type>(CompileAssemblyHelper.GetGenerateParametersType);
        }
        Type[] typeArray = Enumerable.Select<GenerateCodeInfo, Type>(list, func).ToArray<Type>();
        throw new CompileException("FastDBEngine's AutoLoader Code Compile Error.", results.Errors, typeArray);
    }

    [CompilerGenerated]
    private static string GetClassCodeString(GenerateCodeInfo generateCodeInfo)
    {
        return generateCodeInfo.ClassCodeString;
    }

    [CompilerGenerated]
    private static Type GetGenerateParametersType(GenerateCodeInfo generateCodeInfo)
    {
        return generateCodeInfo.GenerateParameters.ModelType;
    }
}

