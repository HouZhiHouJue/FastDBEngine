namespace FastDBEngine
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;

    public static class StringExtensions
    {
        public static object ConvertString(this string str, Type conversionType)
        {
            if (conversionType == null)
            {
                throw new ArgumentNullException("conversionType");
            }
            if (conversionType == TypeInfo.TypeString)
            {
                return str;
            }
            if (!string.IsNullOrEmpty(str))
            {
                if (conversionType.IsEnum)
                {
                    return int.Parse(str);
                }
                if (conversionType.IsGenericType)
                {
                    if (conversionType.GetGenericTypeDefinition() != TypeInfo.TypeNullable)
                    {
                        throw new InvalidCastException();
                    }
                    conversionType = conversionType.GetGenericArguments()[0];
                }
                if (conversionType == TypeInfo.TypeInt)
                {
                    return int.Parse(str);
                }
                if (conversionType == TypeInfo.TypeLong)
                {
                    return long.Parse(str);
                }
                if (conversionType == TypeInfo.TypeShort)
                {
                    return short.Parse(str);
                }
                if (conversionType == TypeInfo.TypeDatetime)
                {
                    return DateTime.Parse(str);
                }
                if (conversionType == TypeInfo.TypeBool)
                {
                    return (((string.Compare(str, "false", StringComparison.OrdinalIgnoreCase) == 0) || (str == "0")) ? ((object) 0) : ((object) (str.Length > 0)));
                }
                if (conversionType == TypeInfo.TypeDouble)
                {
                    return double.Parse(str);
                }
                if (conversionType == TypeInfo.TypeDecimal)
                {
                    return decimal.Parse(str);
                }
                if (conversionType == TypeInfo.TypeFloat)
                {
                    return float.Parse(str);
                }
                if (conversionType == TypeInfo.TypeGuid)
                {
                    return new Guid(str);
                }
                if (conversionType == TypeInfo.TypeUlong)
                {
                    return ulong.Parse(str);
                }
                if (conversionType == TypeInfo.TypeUint)
                {
                    return uint.Parse(str);
                }
                if (conversionType == TypeInfo.TypeUshort)
                {
                    return ushort.Parse(str);
                }
                if (conversionType == TypeInfo.TypeChar)
                {
                    return str[0];
                }
                if (conversionType == TypeInfo.TypeByte)
                {
                    return byte.Parse(str);
                }
                if (conversionType != TypeInfo.TypeSbyte)
                {
                    throw new InvalidCastException();
                }
                return sbyte.Parse(str);
            }
            if (conversionType == TypeInfo.TypeDatetime)
            {
                return DateTime.MinValue;
            }
            if (conversionType == TypeInfo.TypeGuid)
            {
                return Guid.Empty;
            }
            if (conversionType.IsValueType && !conversionType.IsGenericType)
            {
                try
                {
                    return Convert.ChangeType(0, conversionType);
                }
                catch
                {
                    throw new InvalidCastException();
                }
            }
            return null;
        }

        public static string GetMd5String(this string input, Encoding encoding)
        {
            if (input == null)
            {
                input = string.Empty;
            }
            return BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(encoding.GetBytes(input))).Replace("-", "");
        }

        public static string GetSha1String(this string input, Encoding encoding)
        {
            if (input == null)
            {
                input = string.Empty;
            }
            return BitConverter.ToString(new SHA1CryptoServiceProvider().ComputeHash(encoding.GetBytes(input))).Replace("-", "");
        }

        public static byte[] HexToBin(string string_0)
        {
            if (string_0 == null)
            {
                throw new ArgumentNullException("hex");
            }
            if ((string_0.Length % 2) != 0)
            {
                throw new InvalidOperationException("十六进制的字节数组的长度不正确。");
            }
            byte[] buffer = new byte[string_0.Length / 2];
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = byte.Parse(string_0.Substring(i * 2, 2), NumberStyles.HexNumber);
            }
            return buffer;
        }

        public static int IgnoreCaseCompare(this string str, string test)
        {
            return string.Compare(str, test, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IgnoreCaseEndsWith(this string str, string test)
        {
            return str.EndsWith(test, StringComparison.OrdinalIgnoreCase);
        }

        public static int IgnoreCaseIndexOf(this string str, string test)
        {
            return str.IndexOf(test, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IgnoreCaseStartsWith(this string str, string test)
        {
            return str.StartsWith(test, StringComparison.OrdinalIgnoreCase);
        }

        public static bool IsGuid(this string str)
        {
            if ((str == null) || (str.Length != 0x24))
            {
                return false;
            }
            try
            {
                Guid guid = new Guid(str);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<KeyValuePair<string, string>> SplitString(string line, char separator1, char separator2)
        {
            if (string.IsNullOrEmpty(line))
            {
                return new List<KeyValuePair<string, string>>();
            }
            string[] strArray2 = line.Split(new char[] { separator1 }, StringSplitOptions.RemoveEmptyEntries);
            List<KeyValuePair<string, string>> list2 = new List<KeyValuePair<string, string>>(strArray2.Length);
            char[] separator = new char[] { separator2 };
            foreach (string str in strArray2)
            {
                string[] strArray = str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                if (strArray.Length != 2)
                {
                    throw new ArgumentException("要拆分的字符串的格式无效。");
                }
                list2.Add(new KeyValuePair<string, string>(strArray[0], strArray[1]));
            }
            return list2;
        }

        public static List<int> StringToIntList(string str, char flag)
        {
            if (string.IsNullOrEmpty(str))
            {
                return new List<int>();
            }
            string[] strArray = str.Split(new char[] { flag }, StringSplitOptions.RemoveEmptyEntries);
            List<int> list2 = new List<int>(strArray.Length);
            foreach (string value in strArray)
            {
                int num2;
                if (int.TryParse(value, out num2))
                {
                    list2.Add(num2);
                }
            }
            return list2;
        }

        public static T TryChangeType<T>(this string str)
        {
            if (str == null)
            {
                return default(T);
            }
            try
            {
                return (T) Convert.ChangeType(str, typeof(T));
            }
            catch
            {
                return default(T);
            }
        }

        public static DateTime TryToDateTime(this string str)
        {
            return str.TryToDateTime(DateTime.MinValue);
        }

        public static DateTime TryToDateTime(this string str, DateTime defaultVal)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defaultVal;
            }
            DateTime result = defaultVal;
            DateTime.TryParse(str, out result);
            return result;
        }

        public static decimal TryToDecimal(this string str)
        {
            return str.TryToDecimal(0M);
        }

        public static decimal TryToDecimal(this string str, decimal defaultVal)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defaultVal;
            }
            decimal result = defaultVal;
            decimal.TryParse(str, NumberStyles.Currency, null, out result);
            return result;
        }

        public static int TryToInt(this string str)
        {
            return str.TryToInt(0);
        }

        public static int TryToInt(this string str, int defaultVal)
        {
            if (string.IsNullOrEmpty(str))
            {
                return defaultVal;
            }
            int result = defaultVal;
            int.TryParse(str, out result);
            return result;
        }
    }
}

