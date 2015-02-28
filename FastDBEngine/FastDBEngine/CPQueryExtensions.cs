namespace FastDBEngine
{
    using System;
    using System.Runtime.CompilerServices;

    public static class CPQueryExtensions
    {
        public static CPQuery AsCPQuery(this string sqlText)
        {
            return new CPQuery(sqlText, false);
        }

        public static CPQuery AsCPQuery(this string sqlText, bool autoDiscoverParameters)
        {
            return new CPQuery(sqlText, autoDiscoverParameters);
        }

        public static QueryParameter AsQueryParameter(this object obj)
        {
            return new QueryParameter(obj);
        }
    }
}

