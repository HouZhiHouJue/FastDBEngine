using System;
using System.Reflection;
using System.Runtime.CompilerServices;

internal static class PropertyInfoExtensions
{
    public static object GetPropertyValue(this PropertyInfo propertyInfo, object obj)
    {
        if (propertyInfo == null)
        {
            throw new ArgumentNullException("propertyInfo");
        }
        return PropertyDelegateContainer.GetIPropertyGet(propertyInfo).GetPropertyValue(obj);
    }

    public static void SetPropertyValue(this PropertyInfo propertyInfo, object obj, object propertyValue)
    {
        if (propertyInfo == null)
        {
            throw new ArgumentNullException("propertyInfo");
        }
        PropertyDelegateContainer.GetIPropertySet(propertyInfo).SetPropertyValue(obj, propertyValue);
    }
}

