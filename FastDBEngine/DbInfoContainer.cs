using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

internal static class DbInfoContainer
{
    private static Hashtable DbProviderInfoContainer = Hashtable.Synchronized(new Hashtable(50));
    private static Hashtable dbProviderFactoryContainer = Hashtable.Synchronized(new Hashtable(50));

    [MethodImpl(MethodImplOptions.Synchronized)]
    public static void Register(string configName, string providerName, string cmdParamNamePrefix, string defaultConnString, Func<string, string> func_3)
    {
        if (DbProviderInfoContainer.ContainsKey(configName))
        {
            throw new InvalidOperationException(string.Format("配置项 [{0}] 已经注册过了。", configName));
        }
        DbProviderInfo dbProviderInfo = new DbProviderInfo
        {
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
        Type type = Enumerable.Select(Enumerable.Where(Enumerable.SelectMany(AssemblyHelper.GetAppAssemblys(), t => { return t.GetExportedTypes(); },
            (a, t) => { return new TypeContainer<Assembly, Type>(a, t); }),
            new Func<TypeContainer<Assembly, Type>, bool>(typeContainer =>
            {
                return ((typeContainer.Ttype.Namespace == providerName) && typeof(DbProviderFactory).IsAssignableFrom(typeContainer.Ttype));
            })), t => { return t.Ttype; }).FirstOrDefault<Type>();
        if (type == null)
        {
            return null;
        }
        return (DbProviderFactory)type.InvokeMember("Instance", BindingFlags.GetField | BindingFlags.Public | BindingFlags.Static, null, null, null);
    }
}

