namespace FastDBEngineProfilerLib
{
    using System;
    using System.Collections.Generic;

    internal static class EnumNameDict<T> where T: struct
    {
        private static Dictionary<T, string> s_dict;

        static EnumNameDict()
        {
            string[] names = Enum.GetNames(typeof(T));
            T[] values = (T[]) Enum.GetValues(typeof(T));
            if (names.Length == values.Length)
            {
                EnumNameDict<T>.s_dict = new Dictionary<T, string>(names.Length);
                for (int i = 0; i < names.Length; i++)
                {
                    EnumNameDict<T>.s_dict[values[i]] = names[i];
                }
            }
        }

        public static string GetString(T value)
        {
            if (EnumNameDict<T>.s_dict != null)
            {
                string str = null;
                if (EnumNameDict<T>.s_dict.TryGetValue(value, out str))
                {
                    return str;
                }
            }
            return value.ToString();
        }
    }
}

