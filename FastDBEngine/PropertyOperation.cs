using System;
using System.Reflection;

internal sealed class PropertyOperation : MemberOperationBase
{
    private PropertyInfo propertyInfo;

    public PropertyOperation(PropertyInfo propertyInfo)
    {
        if (propertyInfo == null)
        {
            throw new ArgumentNullException("pi");
        }
        this.propertyInfo = propertyInfo;
    }

    public override object GetPropertyValue(object obj)
    {
        return this.propertyInfo.GetPropertyValue(obj);
    }

    public override void SetPropertyValue(object obj, object propertyValue)
    {
        this.propertyInfo.SetPropertyValue(obj, propertyValue);
    }

    public override Type GetPropertyType()
    {
        return this.propertyInfo.PropertyType;
    }

    public override string GetPropertyName()
    {
        return this.propertyInfo.Name;
    }

    public override bool CanWrite()
    {
        return this.propertyInfo.CanWrite;
    }

    public override bool CanRead()
    {
        return this.propertyInfo.CanRead;
    }

    public override MemberInfo GetMemberInfo()
    {
        return this.propertyInfo;
    }
}

