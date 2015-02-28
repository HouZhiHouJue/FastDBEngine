using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

internal static class DbInfoContainer
{
    [CompilerGenerated]
    private static Func<Assembly, IEnumerable<Type>> func;
    [CompilerGenerated]
    private static Func<Assembly, Type, TypeContainer<Assembly, Type>> func_1;
    [CompilerGenerated]
    private static Func<TypeContainer<Assembly, Type>, Type> func_2;
    private static Hashtable DbProviderInfoContainer = Hashtable.Synchronized(new Hashtable(50));
    private static Hashtable dbProviderFactoryContainer = Hashtable.Synchronized(new Hashtable(50));

    [MethodImpl(MethodImplOptions.Synchronized)]
    public static void Register(string configName, string providerName, string cmdParamNamePrefix, string defaultConnString, Func<string, string> func_3)
    {
        if (DbProviderInfoContainer.ContainsKey(configName))
        {
            throw new InvalidOperationException(string.Format("配置项 [{0}] 已经注册过了。", configName));
        }
        DbProviderInfo dbProviderInfo = new DbProviderInfo {
            dbProviderFactory = GetDbProviderFactory(providerName),
            ProviderName = providerName,
            CmdParamNamePrefix = cmdParamNamePrefix ?? string.Empty,
            DefaultConnString = defaultConnString,
            func = func_3
        };
        DbProviderInfoContainer.Add(configName, dbProviderInfo);
        Type type = dbProviderInfo.dbProviderFactory.CreateConnection().GetType();
        dbProviderFactoryContainer[type] = dbProviderInfo.dbProviderFactory;
    }

    public static DbProviderInfo GetDbProviderInfo(string providerName)
    {
        return (DbProviderInfoContainer[providerName] as DbProviderInfo);
    }

    public static DbProviderFactory GetDbProviderFactory(Type type)
    {
        return (dbProviderFactoryContainer[type] as DbProviderFactory);
    }

    public static DbProviderFactory GetDbProviderFactory(string providerName)
    {
        if (string.IsNullOrEmpty(providerName))
        {
            throw new ArgumentNullException("providerName");
        }
        try
        {
            return DbProviderFactories.GetFactory(providerName);
        }
        catch
        {
            DbProviderFactory factory2 = ReflectDbProviderFactory(providerName);
            if (factory2 == null)
            {
                throw;
            }
            return factory2;
        }
    }

    private static DbProviderFactory ReflectDbProviderFactory(string providerName)
    {
        ProviderHelper class2 = new ProviderHelper {
            ProviderName = providerName
        };
        if (func == null)
        {
            func = new Func<Assembly, IEnumerable<Type>>(DbInfoContainer.GetExportedTypes);
        }
        if (func_1 == null)
        {
            func_1 = new Func<Assembly, Type, TypeContainer<Assembly, Type>>(DbInfoContainer.GetTypeContainer);
        }
        if (func_2 == null)
        {
            func_2 = new Func<TypeContainer<Assembly, Type>, Type>(DbInfoContainer.GetTtype);
        }
        Type type = Enumerable.Select(Enumerable.Where(Enumerable.SelectMany(AssemblyHelper.GetAppAssemblys(), func, func_1), new Func<TypeContainer<Assembly, Type>, bool>(class2.CanGetDbProviderFactory)), func_2).FirstOrDefault<Type>();
        if (type == null)
        {
            return null;
        }
        return (DbProviderFactory) type.InvokeMember("Instance", BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static, null, null, null);
    }

    [CompilerGenerated]
    private static IEnumerable<Type> GetExportedTypes(Assembly assembly)
    {
        return assembly.GetExportedTypes();
    }

    [CompilerGenerated]
    private static TypeContainer<Assembly, Type> GetTypeContainer(Assembly assembly, Type type)
    {
        return new TypeContainer<Assembly, Type>(assembly, type);
    }

    [CompilerGenerated]
    private static Type GetTtype(TypeContainer<Assembly, Type> typeContainer)
    {
        return typeContainer.Ttype;
    }

    [CompilerGenerated]
    private sealed class ProviderHelper
    {
        public string ProviderName;

        public bool CanGetDbProviderFactory(TypeContainer<Assembly, Type> typeContainer)
        {
            return ((typeContainer.Ttype.Namespace == this.ProviderName) && typeof(DbProviderFactory).IsAssignableFrom(typeContainer.Ttype));
        }
    }
}

