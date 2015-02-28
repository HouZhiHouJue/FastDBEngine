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
            TempStrContainer class2 = new TempStrContainer {
                comparedValue = value
            };
            return Array.FindIndex<string>(array, new Predicate<string>(class2.CompareStr));
        }

        public static int FindIndex(this List<string> list, string value)
        {
            TempStrContainer tempStrContainer = new TempStrContainer
            {
                comparedValue = value
            };
            return list.FindIndex(new Predicate<string>(tempStrContainer.CompareStr));
        }

        public static T GetAttribute<T>(this MemberInfo memberInfo_0) where T: Attribute
        {
            T[] customAttributes = memberInfo_0.GetCustomAttributes(typeof(T), false) as T[];
            if (customAttributes.Length == 1)
            {
                return customAttributes[0];
            }
            return default(T);
        }

        public static T[] GetAttributes<T>(this MemberInfo memberInfo_0) where T: Attribute
        {
            return (memberInfo_0.GetCustomAttributes(typeof(T), false) as T[]);
        }

        [CompilerGenerated]
        private sealed class TempStrContainer
        {
            public string comparedValue;

            public bool CompareStr(string orginValue)
            {
                return (string.Compare(orginValue, this.comparedValue, StringComparison.OrdinalIgnoreCase) == 0);
            }
        }
    }
}

