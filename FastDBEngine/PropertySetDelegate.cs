using System;
using System.Reflection;

internal class PropertySetDelegate<TClass, TPropertyValue> : IPropertySet
{
    private Action<TClass, TPropertyValue> action;

    public PropertySetDelegate(PropertyInfo propertyInfo)
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
        this.action = (Action<TClass, TPropertyValue>) Delegate.CreateDelegate(typeof(Action<TClass, TPropertyValue>), null, setMethod);
    }

    public void SetPropertyValue(object obj, object propertyValue)
    {
        this.action((TClass) obj, (TPropertyValue) propertyValue);
    }

    public void SetPropertyValue(TClass obj, TPropertyValue propertyValue)
    {
        this.action(obj, propertyValue);
    }
}

