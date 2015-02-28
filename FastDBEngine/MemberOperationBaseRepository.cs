using System;
using System.Reflection;

internal static class MemberOperationBaseRepository
{
    public static MemberOperationBase GetMemberOperationBase(MemberInfo memberInfo)
    {
        if (memberInfo.MemberType == MemberTypes.Field)
        {
            return new FieldInfoOperation((FieldInfo) memberInfo);
        }
        if (memberInfo.MemberType != MemberTypes.Property)
        {
            throw new NotSupportedException();
        }
        return new PropertyOperation((PropertyInfo) memberInfo);
    }
}

