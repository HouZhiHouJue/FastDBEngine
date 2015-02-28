using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Compilation;

internal static class AssemblyHelper
{
    internal static readonly bool IsWebApp = (HttpRuntime.AppDomainAppId != null);
    [CompilerGenerated]
    private static Func<Assembly, Assembly> func;

    public static Assembly[] GetAppAssemblys()
    {
        if (IsWebApp)
        {
            if (func == null)
            {
                func = new Func<Assembly, Assembly>(t => { return t; });
            }
            return Enumerable.Select<Assembly, Assembly>(BuildManager.GetReferencedAssemblies().Cast<Assembly>(), func).ToArray<Assembly>();
        }
        return AppDomain.CurrentDomain.GetAssemblies();
    }
}

