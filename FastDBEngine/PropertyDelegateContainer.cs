using System;
using System.Collections;
using System.Reflection;

internal static class PropertyDelegateContainer
{
    private static readonly Hashtable PropertyGetContainer = Hashtable.Synchronized(new Hashtable(0x2800));
    private static readonly Hashtable PropertySetContainer = Hashtable.Synchronized(new Hashtable(0x2800));

    internal static IPropertyGet GetIPropertyGet(PropertyInfo propertyInfo)
    {
        IPropertyGet interface2 = (IPropertyGet) PropertyGetContainer[propertyInfo];
        if (interface2 == null)
        {
            interface2 = CreateIPropertyGet(propertyInfo);
            PropertyGetContainer[propertyInfo] = interface2;
        }
        return interface2;
    }

    internal static IPropertySet GetIPropertySet(PropertyInfo propertyInfo)
    {
        IPropertySet interface2 = (IPropertySet) PropertySetContainer[propertyInfo];
        if (interface2 == null)
        {
            interface2 = CreateIPropertySet(propertyInfo);
            PropertySetContainer[propertyInfo] = interface2;
        }
        return interface2;
    }

    public static IPropertyGet CreateIPropertyGet(PropertyInfo propertyInfo)
    {
        if (propertyInfo == null)
        {
            throw new ArgumentNullException("propertyInfo");
        }
        if (!propertyInfo.CanRead)
        {
            throw new InvalidOperationException("属性不支持读操作。");
        }
        MethodInfo getMethod = propertyInfo.GetGetMethod(true);
        if (getMethod.GetParameters().Length > 0)
        {
            throw new NotSupportedException("不支持构造索引器属性的委托。");
        }
        if (getMethod.IsStatic)
        {
            return (IPropertyGet) Activator.CreateInstance(typeof(StaticPropertyGetDelegate<>).MakeGenericType(new Type[] { propertyInfo.PropertyType }), new object[] { propertyInfo });
        }
        return (IPropertyGet) Activator.CreateInstance(typeof(PropertyGetDelegate<,>).MakeGenericType(new Type[] { propertyInfo.DeclaringType, propertyInfo.PropertyType }), new object[] { propertyInfo });
    }

    public static IPropertySet CreateIPropertySet(PropertyInfo propertyInfo)
    {
        if (propertyInfo == null)
        {
            throw new ArgumentNullException("propertyInfo");
        }
        if (!propertyInfo.CanWrite)
        {
            throw new NotSupportedException("属性不支持写操作。");
        }
        MethodInfo setMethod = propertyInfo.GetSetMethod(true);
        if (setMethod.GetParameters().Length > 1)
        {
            throw new NotSupportedException("不支持构造索引器属性的委托。");
        }
        if (setMethod.IsStatic)
        {
            return (IPropertySet) Activator.CreateInstance(typeof(StaticPropertySetDelegate<>).MakeGenericType(new Type[] { propertyInfo.PropertyType }), new object[] { propertyInfo });
        }
        return (IPropertySet) Activator.CreateInstance(typeof(PropertySetDelegate<,>).MakeGenericType(new Type[] { propertyInfo.DeclaringType, propertyInfo.PropertyType }), new object[] { propertyInfo });
    }
}

