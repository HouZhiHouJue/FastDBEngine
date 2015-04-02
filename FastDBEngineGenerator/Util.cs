using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using FastDBEngine;

namespace FastDBEngineGenerator
{
   internal class Util
    {
       internal static string GetConnUserName(string conn)
       {
           Regex reg = new Regex(@"(?<=User ID=)\w+(?=;)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
           return reg.Match(conn).Value.ToUpper();
       }

       internal static string GetClassName(string tablename)
       {
           int index = tablename.IndexOf('_');
           tablename = tablename.Substring(index + 1, tablename.Length - index - 1);
           tablename = tablename.FilterStr();
           return tablename.Substring(0, 1) + tablename.Substring(1, tablename.Length - 1).ToLower();
       }
    }
}
