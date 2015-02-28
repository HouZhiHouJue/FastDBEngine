using System;
using System.Reflection;

internal abstract class MemberOperationBase
{
    protected MemberOperationBase()
    {
    }

    public abstract object GetPropertyValue(object obj);
    public abstract void SetPropertyValue(object obj, object propertyValue);
    public abstract Type GetPropertyType();
    public abstract string GetPropertyName();
    public abstract bool CanWrite();
    public abstract bool CanRead();
    public abstract MemberInfo GetMemberInfo();
}

