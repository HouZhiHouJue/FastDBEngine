using FastDBEngine;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

internal sealed class MeberOperationHelperContainer
{
    [CompilerGenerated]
    private GenerateModelDelegate getModelDelegate;
    [CompilerGenerated]
    private GetPropertyValueByNameDelegate getPropertyValueByNameDelegate;
    [CompilerGenerated]
    private SetPropertyValueByNameDelegate setPropertyValueByNameDelegate;
    private Dictionary<string, MeberOperationHelper> dict;
    private int IsInit = 0;
    private int OriginValue;
    private Type modelType;

    public MeberOperationHelperContainer(Type type, int capacity)
    {
        this.modelType = type;
        this.dict = new Dictionary<string, MeberOperationHelper>(capacity, StringComparer.OrdinalIgnoreCase);
    }

    [CompilerGenerated]
    internal GenerateModelDelegate GetModelDelegates()
    {
        return this.getModelDelegate;
    }

    [CompilerGenerated]
    private void SetModelDelegates(GenerateModelDelegate delegate0_1)
    {
        this.getModelDelegate = delegate0_1;
    }

    public Type GetModelType()
    {
        return this.modelType;
    }

    public bool GetMeberOperationHelper(string key, out MeberOperationHelper meberOperationHelper)
    {
        if (this.dict.TryGetValue(key, out meberOperationHelper))
        {
            return true;
        }
        using (Dictionary<string, MeberOperationHelper>.Enumerator enumerator = this.dict.GetEnumerator())
        {
            KeyValuePair<string, MeberOperationHelper> current;
            while (enumerator.MoveNext())
            {
                current = enumerator.Current;
                if (string.Compare(current.Value.GetDbColumnAttribute().Alias, key, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    goto Label_0058;
                }
            }
            goto Label_0073;
        Label_0058:
            meberOperationHelper = current.Value;
            return true;
        }
    Label_0073:
        return false;
    }

    public EnumState GetOriginValue()
    {
        return (EnumState) this.OriginValue;
    }

    public bool IsOrigin(EnumState compared, EnumState value)
    {
        return (Interlocked.CompareExchange(ref this.OriginValue, (int) value, (int) compared) == (int)compared);
    }

    [CompilerGenerated]
    internal GetPropertyValueByNameDelegate GetPropertyValueByNameDelegate()
    {
        return this.getPropertyValueByNameDelegate;
    }

    [CompilerGenerated]
    private void SetGetPropertyValueByNameDelegate(GetPropertyValueByNameDelegate getPropertyValueByNameDelegate)
    {
        this.getPropertyValueByNameDelegate = getPropertyValueByNameDelegate;
    }

    [CompilerGenerated]
    internal SetPropertyValueByNameDelegate SetPropertyValueByNameDelegate()
    {
        return this.setPropertyValueByNameDelegate;
    }

    [CompilerGenerated]
    private void SetPropertyValueByNameDelegate(SetPropertyValueByNameDelegate setPropertyValueByNameDelegate)
    {
        this.setPropertyValueByNameDelegate = setPropertyValueByNameDelegate;
    }

    public void InitAll()
    {
        if (Interlocked.CompareExchange(ref this.IsInit, 1, 0) != 1)
        {
            foreach (KeyValuePair<string, MeberOperationHelper> pair in this.dict)
            {
                pair.Value.Init();
            }
        }
    }

    public void SetDelegates(GenerateModelDelegate generateModelDelegate, GetPropertyValueByNameDelegate getPropertyValueByNameDelegate, 
        SetPropertyValueByNameDelegate setPropertyValueByNameDelegate)
    {
        try
        {
        }
        finally
        {
            this.SetModelDelegates(generateModelDelegate);
            this.SetGetPropertyValueByNameDelegate(getPropertyValueByNameDelegate);
            this.SetPropertyValueByNameDelegate(setPropertyValueByNameDelegate);
            this.IsOrigin(EnumState.const_1, EnumState.const_2);
        }
    }

    public Dictionary<string, MeberOperationHelper> GetDict()
    {
        return this.dict;
    }

    public void SetMeberOperationHelperValue(DbColumnAttribute dbColumnAttribute, MemberOperationBase memberOperationBase)
    {
        MeberOperationHelper meberOperationHelper = new MeberOperationHelper(dbColumnAttribute, memberOperationBase) {
            MeberOperationHelperContainer = this
        };
        this.GetDict()[memberOperationBase.GetPropertyName()] = meberOperationHelper;
    }
}

