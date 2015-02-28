namespace FastDBEngine
{
    using System;

    public sealed class QueryParameter
    {
        private object value;

        public QueryParameter(object obj)
        {
            this.value = obj;
        }

        public static explicit operator QueryParameter(string str)
        {
            return new QueryParameter(str);
        }

        public static implicit operator QueryParameter(bool b)
        {
            return new QueryParameter(b);
        }

        public static implicit operator QueryParameter(byte b)
        {
            return new QueryParameter(b);
        }

        public static implicit operator QueryParameter(char c)
        {
            return new QueryParameter(c);
        }

        public static implicit operator QueryParameter(DateTime date)
        {
            return new QueryParameter(date);
        }

        public static implicit operator QueryParameter(decimal d)
        {
            return new QueryParameter(d);
        }

        public static implicit operator QueryParameter(double d)
        {
            return new QueryParameter(d);
        }

        public static implicit operator QueryParameter(Guid g)
        {
            return new QueryParameter(g);
        }

        public static implicit operator QueryParameter(short s)
        {
            return new QueryParameter(s);
        }

        public static implicit operator QueryParameter(int value)
        {
            return new QueryParameter(value);
        }

        public static implicit operator QueryParameter(long l)
        {
            return new QueryParameter(l);
        }

        [CLSCompliant(false)]
        public static implicit operator QueryParameter(sbyte s)
        {
            return new QueryParameter(s);
        }

        public static implicit operator QueryParameter(float f)
        {
            return new QueryParameter(f);
        }

        [CLSCompliant(false)]
        public static implicit operator QueryParameter(ushort u)
        {
            return new QueryParameter(u);
        }

        [CLSCompliant(false)]
        public static implicit operator QueryParameter(uint u)
        {
            return new QueryParameter(u);
        }

        [CLSCompliant(false)]
        public static implicit operator QueryParameter(ulong u)
        {
            return new QueryParameter(u);
        }

        public static implicit operator QueryParameter(byte[] b)
        {
            return new QueryParameter(b);
        }

        public object Value
        {
            get
            {
                return this.value;
            }
        }
    }
}

