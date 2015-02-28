using System;
using System.Reflection;

internal class StaticPropertyGetDelegate<T> : IPropertyGet
{
    private Func<T> func;

    public StaticPropertyGetDelegate(PropertyInfo propertyInfo)
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
        this.func = (Func<T>) Delegate.CreateDelegate(typeof(Func<T>), getMethod);
    }

    public object GetPropertyValue(object obj)
    {
        return this.func();
    }

    public T method_0()
    {
        return this.func();
    }
}

