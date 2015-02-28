using Microsoft.Win32;
using System;

public static class RegistryHelper
{
    private static readonly string registerPath = @"HKEY_CURRENT_USER\Software\FastDBEngine\XmlCommandTool";

    public static object GetValue(string key, object defaultValue)
    {
        object obj = null;
        try
        {
            obj = Registry.GetValue(registerPath, key, defaultValue);
        }
        catch
        {
        }
        return (obj ?? defaultValue);
    }

    public static void SetValue(string key, object value)
    {
        try
        {
            Registry.SetValue(registerPath, key, value);
        }
        catch
        {
        }
    }
}

