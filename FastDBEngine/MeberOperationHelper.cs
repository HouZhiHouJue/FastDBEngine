using FastDBEngine;
using System;
using System.Reflection;

internal sealed class MeberOperationHelper
{
    private MemberOperationBase MemberOperationBase;
    public MeberOperationHelperContainer MeberOperationHelperContainer;
    private DbColumnAttribute dbColumnAttribute;
    private IPropertyGet IPropertyGet;
    private IPropertySet IPropertySet;

    public MeberOperationHelper(DbColumnAttribute dbColumnAttribute, MemberOperationBase memberOperationBase)
    {
        if (dbColumnAttribute == null)
        {
            throw new ArgumentNullException("attr");
        }
        if (memberOperationBase == null)
        {
            throw new ArgumentNullException("member");
        }
        this.dbColumnAttribute = dbColumnAttribute;
        this.MemberOperationBase = memberOperationBase;
    }

    public void Init()
    {
        PropertyInfo info = this.GetMemberOperationBase().GetMemberInfo() as PropertyInfo;
        if (info != null)
        {
            if ((this.IPropertyGet == null) && info.CanRead)
            {
                this.IPropertyGet = PropertyDelegateContainer.CreateIPropertyGet(info);
            }
            if ((this.IPropertySet == null) && info.CanWrite)
            {
                this.IPropertySet = PropertyDelegateContainer.CreateIPropertySet(info);
            }
        }
    }

    public DbColumnAttribute GetDbColumnAttribute()
    {
        return this.dbColumnAttribute;
    }

    public MemberOperationBase GetMemberOperationBase()
    {
        return this.MemberOperationBase;
    }

    public Type GetPropertyType()
    {
        return this.MemberOperationBase.GetPropertyType();
    }

    public object GetPropertyValue(object obj)
    {
        if (this.MeberOperationHelperContainer != null)
        {
            GetPropertyValueByNameDelegate GetPropertyValueByNameDelegate = this.MeberOperationHelperContainer.GetPropertyValueByNameDelegate();
            if (GetPropertyValueByNameDelegate != null)
            {
                return GetPropertyValueByNameDelegate(obj, this.MemberOperationBase.GetPropertyName());
            }
        }
        if (this.IPropertyGet != null)
        {
            return this.IPropertyGet.GetPropertyValue(obj);
        }
        return this.MemberOperationBase.GetPropertyValue(obj);
    }

    public bool SetPropertyValue(object obj, object propertyValue)
    {
        if (this.MeberOperationHelperContainer != null)
        {
            SetPropertyValueByNameDelegate setPropertyValueByNameDelegate = this.MeberOperationHelperContainer.SetPropertyValueByNameDelegate();
            if (setPropertyValueByNameDelegate != null)
            {
                setPropertyValueByNameDelegate(obj, this.MemberOperationBase.GetPropertyName(), propertyValue);
                return true;
            }
        }
        try
        {
            Type conversionType = this.GetPropertyType().GetFirstGenericArgumentsOrDefault();
            if (conversionType.IsEnum)
            {
                if (this.IPropertySet != null)
                {
                    this.IPropertySet.SetPropertyValue(obj, Convert.ToInt32(propertyValue));
                }
                else
                {
                    this.MemberOperationBase.SetPropertyValue(obj, Convert.ToInt32(propertyValue));
                }
            }
            else if (propertyValue.GetType() == conversionType)
            {
                if (this.IPropertySet != null)
                {
                    this.IPropertySet.SetPropertyValue(obj, propertyValue);
                }
                else
                {
                    this.MemberOperationBase.SetPropertyValue(obj, propertyValue);
                }
            }
            else if (this.IPropertySet != null)
            {
                this.IPropertySet.SetPropertyValue(obj, Convert.ChangeType(propertyValue, conversionType));
            }
            else
            {
                this.MemberOperationBase.SetPropertyValue(obj, Convert.ChangeType(propertyValue, conversionType));
            }
            return true;
        }
        catch
        {
            return false;
        }
    }
}

