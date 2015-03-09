using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Compilation;

internal static class AssemblyHelper
{
    internal static readonly bool IsWebApp = (HttpRuntime.AppDomainAppId != null);
    public static Assembly[] GetAppAssemblys()
    {
        if (IsWebApp)
        {
          
            return Enumerable.Select<Assembly, Assembly>(BuildManager.GetReferencedAssemblies().Cast<Assembly>(), t => { return t; }).ToArray<Assembly>();
        }
        return AppDomain.CurrentDomain.GetAssemblies();
    }
}

