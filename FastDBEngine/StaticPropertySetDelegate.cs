using System;
using System.Reflection;

internal class StaticPropertySetDelegate<T> : IPropertySet
{
    private Action<T> action;

    public StaticPropertySetDelegate(PropertyInfo propertyInfo)
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
        this.action = (Action<T>) Delegate.CreateDelegate(typeof(Action<T>), setMethod);
    }

    public void SetPropertyValue(object obj, object propertyValue)
    {
        this.action((T) propertyValue);
    }

    public void SetPropertyValue(T obj)
    {
        this.action(obj);
    }
}

