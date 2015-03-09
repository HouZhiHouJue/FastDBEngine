namespace FastDBEngine
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Web;
    using System.Web.Caching;

    public static class XmlCommandManager
    {
        private static Dictionary<string, XmlCommand> DictXmlCommand = null;
        private static Exception ex = null;
        private static readonly string guid = Guid.NewGuid().ToString();

        public static XmlCommand GetCommand(string sqlOrProcedureName)
        {
            XmlCommand command;
            if (ex != null)
            {
                throw ex;
            }
            if ((DictXmlCommand != null) && DictXmlCommand.TryGetValue(sqlOrProcedureName, out command))
            {
                return command;
            }
            return null;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void LoadCommnads(string directoryPath)
        {
            if ((DictXmlCommand != null) && AssemblyHelper.IsWebApp)
            {
                throw new InvalidOperationException("不允许重复调用这个方法。");
            }
            if (!Directory.Exists(directoryPath))
            {
                throw new DirectoryNotFoundException(string.Format("目录 {0} 不存在。", directoryPath));
            }
            Exception exception = null;
            DictXmlCommand = LoadXmlConfig(directoryPath, out exception);
            if (exception != null)
            {
                ex = exception;
            }
            if (ex != null)
            {
                throw ex;
            }
        }

        internal static void AddElement<T, U>(this Dictionary<T, U> dictionary, T key, U Value)
        {
            try
            {
                dictionary.Add(key, Value);
            }
            catch (ArgumentException exception)
            {
                throw new ArgumentException(string.Format("往集合中插入元素时发生了异常，当前Key={0}", key), exception);
            }
        }

        private static Dictionary<string, XmlCommand> LoadXmlConfig(string path, out Exception ex)
        {
            Dictionary<string, XmlCommand> dict = null;
            ex = null;
            try
            {
                string[] strArray = Directory.GetFiles(path, "*.config", SearchOption.AllDirectories);
                if (strArray.Length > 0)
                {
                    dict = new Dictionary<string, XmlCommand>(0x800);
                    foreach (string str in strArray)
                    {
                        XmlHelper.XmlDeserializeFromFile<List<XmlCommand>>(str, Encoding.UTF8).ForEach(t => { dict.AddElement(t.CommandName, t); });
                    }
                }
            }
            catch (Exception exception)
            {
                ex = exception;
                dict = null;
            }
            if (AssemblyHelper.IsWebApp)
            {
                CacheDependency dependencies = new CacheDependency(path);
                HttpRuntime.Cache.Insert(guid, path, dependencies, Cache.NoAbsoluteExpiration, Cache.NoSlidingExpiration, CacheItemPriority.NotRemovable, new CacheItemRemovedCallback(XmlCommandManager.LoadXmlCommand));
            }
            return dict;
        }

        private static void LoadXmlCommand(string string_1, object path, CacheItemRemovedReason cacheItemRemovedReason)
        {
            Exception exception = null;
            string str = (string)path;
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(0xbb8);
                Dictionary<string, XmlCommand> dictionary = LoadXmlConfig(str, out exception);
                if (exception == null)
                {
                    try
                    {
                    }
                    finally
                    {
                        DictXmlCommand = dictionary;
                        ex = null;
                    }
                    return;
                }
            }
            if (exception != null)
            {
                ex = exception;
            }
        }
    }
}

