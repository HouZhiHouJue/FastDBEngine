using System;
using System.Data.Common;

internal sealed class DbProviderInfo
{
    public DbProviderFactory dbProviderFactory;
    public Func<string, string> func;
    public string ProviderName;
    public string CmdParamNamePrefix;
    public string DefaultConnString;
}

