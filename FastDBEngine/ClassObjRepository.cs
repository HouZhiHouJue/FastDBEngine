using FastDBEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

internal static class ClassObjRepository
{
    private static Hashtable HashMeberOperationHelperContainer = new Hashtable(0x1000);

    public static MeberOperationHelperContainer GetMeberOperationHelperContainer(this Type type, bool bool_0)
    {
        if (type == null)
        {
            throw new ArgumentNullException("itemType");
        }
        MeberOperationHelperContainer meberOperationHelperContainer = HashMeberOperationHelperContainer[type] as MeberOperationHelperContainer;
        if (meberOperationHelperContainer == null)
        {
            bool flag = false;
            lock (HashMeberOperationHelperContainer.SyncRoot)
            {
                meberOperationHelperContainer = HashMeberOperationHelperContainer[type] as MeberOperationHelperContainer;
                if (meberOperationHelperContainer == null)
                {
                    meberOperationHelperContainer = GetMeberOperationHelperContainer(type);
                    HashMeberOperationHelperContainer[type] = meberOperationHelperContainer;
                    flag = type.IsTypeContainer();
                }
            }
            if (flag)
            {
                meberOperationHelperContainer.InitAll();
            }
        }
        if ((bool_0 && (BuildManager.int1 == 1)) && (meberOperationHelperContainer.GetOriginValue() == Enum0.const_0))
        {
            BuildManager.EnqueueWaitForCompiledType(type);
        }
        return meberOperationHelperContainer;
    }

    private static MeberOperationHelperContainer GetMeberOperationHelperContainer(Type type)
    {
        MemberInfo[] members = type.GetMembers(BindingFlags.Public | BindingFlags.Instance);
        int num = 0;
        foreach (MemberInfo info in members)
        {
            if ((info.MemberType == MemberTypes.Field) || (info.MemberType == MemberTypes.Property))
            {
                num++;
            }
        }
        MeberOperationHelperContainer meberOperationHelperContainer = new MeberOperationHelperContainer(type, num);
        foreach (MemberInfo info in members)
        {
            if (((info.MemberType == MemberTypes.Field) || (info.MemberType == MemberTypes.Property)) && ((info.MemberType != MemberTypes.Property) || !(info as PropertyInfo).NeedExtraParameters()))
            {
                DbColumnAttribute attribute = info.GetAttribute<DbColumnAttribute>() ?? new DbColumnAttribute();
                MemberOperationBase memberOperationBase = MemberOperationBaseRepository.GetMemberOperationBase(info);
                if (!memberOperationBase.GetPropertyType().IsArray())
                {
                    if (!(attribute.IgnoreLoad || memberOperationBase.CanWrite()))
                    {
                        attribute.IgnoreLoad = true;
                    }
                    if (!attribute.IgnoreLoad)
                    {
                        if (memberOperationBase.GetPropertyType().IsValid())
                        {
                            attribute.HasPrefix = true;
                            if (!memberOperationBase.GetPropertyType().HasPublicConstructor())
                            {
                                attribute.IgnoreLoad = true;
                            }
                        }
                        else if (string.IsNullOrEmpty(attribute.Alias))
                        {
                            attribute.Alias = info.Name;
                        }
                    }
                    meberOperationHelperContainer.SetMeberOperationHelperValue(attribute, memberOperationBase);
                }
            }
        }
        return meberOperationHelperContainer;
    }

    public static void SetPropertyValueFromReaderAdapeter(DataReaderAdapeter dataReaderAdapeter, object obj)
    {
        if (dataReaderAdapeter == null)
        {
            throw new ArgumentNullException("row");
        }
        if (obj == null)
        {
            throw new ArgumentNullException("item");
        }
        if (dataReaderAdapeter.obj == null)
        {
            ClassDelegateInfo classDelegateInfo = new ClassDelegateInfo {
                ColumnNames = dataReaderAdapeter.GetColumnNames(),
                MeberOperationHelperContainer = obj.GetType().GetMeberOperationHelperContainer(true)
            };
            dataReaderAdapeter.obj = classDelegateInfo;
        }
        SetPropertyValueFromReader(dataReaderAdapeter, obj, null);
    }

    private static void SetPropertyValueFromReader(DataReaderAdapeter dataReaderAdapeter, object obj, string subItemPrefix)
    {
        Type type = obj.GetType();
        ClassDelegateInfo classDelegateInfo = (ClassDelegateInfo) dataReaderAdapeter.obj;
        MeberOperationHelperContainer meberOperationHelperContainer = classDelegateInfo.MeberOperationHelperContainer;
        if (!(classDelegateInfo.MeberOperationHelperContainer.GetModelType() == type))
        {
            classDelegateInfo.MeberOperationHelperContainer = type.GetMeberOperationHelperContainer(true);
        }
        foreach (KeyValuePair<string, MeberOperationHelper> pair in classDelegateInfo.MeberOperationHelperContainer.GetDict())
        {
            if (!pair.Value.GetDbColumnAttribute().IgnoreLoad)
            {
                if (pair.Value.GetDbColumnAttribute().HasPrefix)
                {
                    object obj2 = Activator.CreateInstance(pair.Value.GetPropertyType());
                    pair.Value.SetPropertyValue(obj, obj2);
                    string str = null;
                    if (string.IsNullOrEmpty(pair.Value.GetDbColumnAttribute().SubItemPrefix))
                    {
                        str = subItemPrefix;
                    }
                    else if (pair.Value.GetDbColumnAttribute().SubItemPrefix == "*")
                    {
                        str = subItemPrefix + (pair.Key + ".");
                    }
                    else
                    {
                        str = subItemPrefix + pair.Value.GetDbColumnAttribute().SubItemPrefix;
                    }
                    SetPropertyValueFromReader(dataReaderAdapeter, obj2, str);
                    classDelegateInfo.MeberOperationHelperContainer = meberOperationHelperContainer;
                }
                else
                {
                    InnerSetPropertyValueFromReader(dataReaderAdapeter, obj, pair.Value, subItemPrefix);
                }
            }
        }
    }

    private static bool InnerSetPropertyValueFromReader(DataReaderAdapeter dataReaderAdapeter, object obj, MeberOperationHelper meberOperationHelper, string string_0)
    {
        string[] array = ((ClassDelegateInfo) dataReaderAdapeter.obj).ColumnNames;
        string str = string_0 + meberOperationHelper.GetDbColumnAttribute().Alias;
        int num = array.FindIndex(str);
        if (num < 0)
        {
            return false;
        }
        object dbValue = dataReaderAdapeter.GetValue(num);
        return (DBNull.Value.Equals(dbValue) || meberOperationHelper.SetPropertyValue(obj, dbValue));
    }

    private sealed class ClassDelegateInfo
    {
        public MeberOperationHelperContainer MeberOperationHelperContainer;
        public string[] ColumnNames;
    }
}

