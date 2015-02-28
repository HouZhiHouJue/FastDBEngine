using System;
using System.Reflection;

internal class PropertyGetDelegate<TClass, TProperty> : IPropertyGet
{
    private Func<TClass, TProperty> func;

    public PropertyGetDelegate(PropertyInfo propertyInfo)
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
        this.func = (Func<TClass, TProperty>) Delegate.CreateDelegate(typeof(Func<TClass, TProperty>), null, getMethod);
    }

    public object GetPropertyValue(object objClass)
    {
        return this.func((TClass) objClass);
    }

    public TProperty method_0(TClass objClass)
    {
        return this.func(objClass);
    }
}

