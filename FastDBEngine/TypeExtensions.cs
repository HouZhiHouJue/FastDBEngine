using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

internal static class TypeExtensions
{
    public static bool IsValid(this Type type)
    {
        if ((((type == TypeInfo.TypeString) || !type.IsClass) || (type == TypeInfo.TypeBytes)) || (type == TypeInfo.TypeObject))
        {
            return false;
        }
        return true;
    }

    public static void ValidateType(this Type type)
    {
        if (!type.IsValid())
        {
            throw new ArgumentException(string.Format("类型 {0} 不是有效的数据实体类型。", type.ToString()));
        }
    }

    public static bool IsNullable(this Type type)
    {
        return (type.IsGenericType && (type.GetGenericTypeDefinition() == TypeInfo.TypeNullable));
    }

    public static Type GetFirstGenericArgumentsOrDefault(this Type type)
    {
        if (type.IsGenericType && (type.GetGenericTypeDefinition() == TypeInfo.TypeNullable))
        {
            return type.GetGenericArguments()[0];
        }
        return type;
    }

    public static Type GetFirstGenericArguments(this Type type)
    {
        if (type.IsGenericType && (type.GetGenericTypeDefinition() == TypeInfo.TypeNullable))
        {
            return type.GetGenericArguments()[0];
        }
        return null;
    }

    public static bool IsArray(this Type type)
    {
        if ((type == TypeInfo.TypeString) || (type == TypeInfo.TypeBytes))
        {
            return false;
        }
        return (type.IsArray || TypeInfo.TypeIEnumerable.IsAssignableFrom(type));
    }

    public static bool HasPublicConstructor(this Type type_0)
    {
        return (type_0.GetConstructor(Type.EmptyTypes) != null);
    }

    public static string GetGenericArgumentsString(this Type Ttype)
    {
        if (!(Ttype.IsGenericType && !Ttype.ContainsGenericParameters))
        {
            return Ttype.FullName.Replace('+', '.');
        }
        string fullName = Ttype.GetGenericTypeDefinition().FullName;
        int index = fullName.IndexOf('`');
        StringBuilder builder = new StringBuilder();
        builder.Append(fullName.Substring(0, index)).Append("<");
        foreach (Type type in Ttype.GetGenericArguments())
        {
            builder.Append(type.GetGenericArgumentsString()).Append(",");
        }
        builder.Remove(builder.Length - 1, 1);
        builder.Append(">");
        return builder.ToString();
    }

    public static bool IsTypeContainer(this Type type)
    {
        return type.Name.StartsWith("TypeContainer");
    }

    public static bool NeedExtraParameters(this PropertyInfo propertyInfo)
    {
        if (propertyInfo == null)
        {
            return false;
        }
        if (propertyInfo.CanRead)
        {
            return (propertyInfo.GetGetMethod(true).GetParameters().Length > 0);
        }
        return (propertyInfo.CanWrite && (propertyInfo.GetSetMethod(true).GetParameters().Length > 1));
    }
}

