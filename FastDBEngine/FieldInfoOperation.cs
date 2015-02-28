using System;
using System.Reflection;

internal sealed class FieldInfoOperation : MemberOperationBase
{
    private FieldInfo fieldInfo;

    public FieldInfoOperation(FieldInfo fieldInfo)
    {
        if (fieldInfo == null)
        {
            throw new ArgumentNullException("fi");
        }
        this.fieldInfo = fieldInfo;
    }

    public override object GetPropertyValue(object obj)
    {
        return this.fieldInfo.GetValue(obj);
    }

    public override void SetPropertyValue(object obj, object propertyValue)
    {
        this.fieldInfo.SetValue(obj, propertyValue);
    }

    public override Type GetPropertyType()
    {
        return this.fieldInfo.FieldType;
    }

    public override string GetPropertyName()
    {
        return this.fieldInfo.Name;
    }

    public override bool CanWrite()
    {
        return true;
    }

    public override bool CanRead()
    {
        return true;
    }

    public override MemberInfo GetMemberInfo()
    {
        return this.fieldInfo;
    }
}

