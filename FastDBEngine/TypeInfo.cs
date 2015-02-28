using FastDBEngine;
using System;
using System.Collections;

internal static class TypeInfo
{
    public static readonly Type TypeString = typeof(string);
    public static readonly Type TypeInt = typeof(int);
    public static readonly Type TypeUlong = typeof(ulong);
    public static readonly Type TypeUint = typeof(uint);
    public static readonly Type TypeUshort = typeof(ushort);
    public static readonly Type TypeChar = typeof(char);
    public static readonly Type TypeByte = typeof(byte);
    public static readonly Type TypeSbyte = typeof(sbyte);
    public static readonly Type TypeBytes = typeof(byte[]);
    public static readonly Type TypeObject = typeof(object);
    public static readonly Type TypeVoid = typeof(void);
    public static readonly Type TypeDbColumnAttribute = typeof(DbColumnAttribute);
    public static readonly Type TypeLong = typeof(long);
    public static readonly Type TypeNullable = typeof(Nullable<>);
    public static readonly Type TypeIEnumerable = typeof(IEnumerable);
    public static readonly Type TypeShort = typeof(short);
    public static readonly Type TypeDatetime = typeof(DateTime);
    public static readonly Type TypeBool = typeof(bool);
    public static readonly Type TypeDouble = typeof(double);
    public static readonly Type TypeDecimal = typeof(decimal);
    public static readonly Type TypeFloat = typeof(float);
    public static readonly Type TypeGuid = typeof(Guid);
}

