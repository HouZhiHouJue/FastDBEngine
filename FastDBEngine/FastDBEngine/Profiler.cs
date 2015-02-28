namespace FastDBEngine
{
    using System;
    using System.IO;
    using System.Reflection;

    public static class Profiler
    {
        private static string appName = "MyWebSite";

        public static bool TryStartFastDBEngineProfiler()
        {
            try
            {
                string path = AssemblyHelper.IsWebApp ? Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\FastDBEngineProfilerLib.dll") : Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "FastDBEngineProfilerLib.dll");
                if (File.Exists(path))
                {
                    Type type = Type.GetType("FastDBEngineProfilerLib.EventHelper, FastDBEngineProfilerLib");
                    if (type == null)
                    {
                        return false;
                    }
                    type.InvokeMember("SubscribeNotify", BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Static, null, null, null);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        public static string ApplicationName
        {
            get
            {
                return appName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    appName = value;
                }
            }
        }
    }
}

