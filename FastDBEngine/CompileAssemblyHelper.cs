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
        CompilerParameters options = new CompilerParameters
        {
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

        string[] sources = Enumerable.Select<GenerateCodeInfo, string>(list, t => { return t.ClassCodeString; }).ToArray<string>();
        CompilerResults results = ((CSharpCodeProvider)CodeDomProvider.CreateProvider("CSharp")).CompileAssemblyFromSource(options, sources);
        if ((results.Errors == null) || !results.Errors.HasErrors)
        {
            return results.CompiledAssembly;
        }
        Type[] typeArray = Enumerable.Select<GenerateCodeInfo, Type>(list, t => { return t.GenerateParameters.ModelType; }).ToArray<Type>();
        throw new CompileException("FastDBEngine's AutoLoader Code Compile Error.", results.Errors, typeArray);
    }

}

