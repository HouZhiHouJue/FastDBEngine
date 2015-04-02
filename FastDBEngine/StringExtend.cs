using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastDBEngine
{
   public static class StringExtend
    {
       public static string FilterStr(this string originStr)
       {
           return originStr.Replace(" ", "").Replace("[", "").Replace("]", "").Replace("'", "").Replace("\"", "").Replace("_", "");
       }

       public static string Pad(this string str, int number)
       {
           return str.PadLeft(str.Length + number, ' ');
       }

    }
}
