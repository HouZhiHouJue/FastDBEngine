namespace FastDBEngine
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public static class MyExtensions
    {
        public static int FindIndex(this string[] array, string value)
        {
            return Array.FindIndex<string>(array, t => { return (string.Compare(t, value, StringComparison.OrdinalIgnoreCase) == 0); });
        }

        public static int FindIndex(this List<string> list, string value)
        {
            return list.FindIndex(t => { return (string.Compare(t, value, StringComparison.OrdinalIgnoreCase) == 0); });
        }

        public static T GetAttribute<T>(this MemberInfo memberInfo_0) where T : Attribute
        {
            T[] customAttributes = memberInfo_0.GetCustomAttributes(typeof(T), false) as T[];
            if (customAttributes.Length == 1)
            {
                return customAttributes[0];
            }
            return default(T);
        }

        public static T[] GetAttributes<T>(this MemberInfo memberInfo_0) where T : Attribute
        {
            return (memberInfo_0.GetCustomAttributes(typeof(T), false) as T[]);
        }

    }
}

