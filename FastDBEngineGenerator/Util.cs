using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FastDBEngineGenerator
{
   internal class Util
    {
       internal static string GetConnUserName(string conn)
       {
           Regex reg = new Regex(@"(?<=User ID=)\w+(?=;)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
           return reg.Match(conn).Value.ToUpper();
       }
    }
}
