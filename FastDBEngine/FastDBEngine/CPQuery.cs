namespace FastDBEngine
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Reflection;
    using System.Text;

    public sealed class CPQuery
    {
        private bool AutoDiscoverParameters;
        private Func<string, string> funcParameterQueryDelegate = new Func<string, string>(CPQuery.AddPrefix);
        private Func<string, string> funcAddPrefix = new Func<string, string>(CPQuery.AddPrefix);
        private int int_0;
        private List<KeyValuePair<string, QueryParameter>> ParametersDict = new List<KeyValuePair<string, QueryParameter>>(10);
        private SPStep spstep = SPStep.NotSet;
        private StringBuilder stringBuilder = new StringBuilder(0x400);

        public CPQuery(string sqlText, bool autoDiscoverParameters)
        {
            this.AutoDiscoverParameters = autoDiscoverParameters;
            this.AddSqlText(sqlText);
        }

        public CPQuery AppendFormat(string format, params object[] parameters)
        {
            int num;
            if (string.IsNullOrEmpty(format))
            {
                throw new ArgumentNullException("format");
            }
            if ((parameters == null) || (parameters.Length == 0))
            {
                this.stringBuilder.Append(format);
                return this;
            }
            object[] args = new object[parameters.Length];
            for (num = 0; num < parameters.Length; num++)
            {
                args[num] = this.funcParameterQueryDelegate("p" + num.ToString());
            }
            this.stringBuilder.Append(string.Format(format, args));
            for (num = 0; num < parameters.Length; num++)
            {
                string str = this.funcAddPrefix("p" + num.ToString());
                this.AddSqlTextWithParameter(str, new QueryParameter(parameters[num]));
            }
            return this;
        }

        public CPQuery AppendFrom(string parameterizedSQL, object argsObject)
        {
            if (string.IsNullOrEmpty(parameterizedSQL))
            {
                throw new ArgumentNullException("parameterizedSQL");
            }
            this.stringBuilder.Append(parameterizedSQL);
            if (argsObject != null)
            {
                foreach (PropertyInfo info in argsObject.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    QueryParameter parameter = new QueryParameter(info.GetPropertyValue(argsObject));
                    this.AddSqlTextWithParameter(this.funcAddPrefix(info.Name), parameter);
                }
            }
            return this;
        }

        public void BindToCommand(DbCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            command.CommandText = this.stringBuilder.ToString();
            command.Parameters.Clear();
            foreach (KeyValuePair<string, QueryParameter> pair in this.ParametersDict)
            {
                DbParameter parameter = command.CreateParameter();
                parameter.ParameterName = pair.Key;
                parameter.Value = pair.Value.Value;
                command.Parameters.Add(parameter);
            }
        }

        public static CPQuery Format(string format, params object[] parameters)
        {
            return New().AppendFormat(format, parameters);
        }

        public static CPQuery From(string parameterizedSQL, object argsObject)
        {
            return New().AppendFrom(parameterizedSQL, argsObject);
        }

        private void AddSqlText(string sqlText)
        {
            if (!string.IsNullOrEmpty(sqlText))
            {
                if (this.AutoDiscoverParameters)
                {
                    if (this.spstep == SPStep.NotSet)
                    {
                        if (sqlText[sqlText.Length - 1] == '\'')
                        {
                            this.stringBuilder.Append(sqlText.Substring(0, sqlText.Length - 1));
                            this.spstep = SPStep.EndWith;
                        }
                        else
                        {
                            object obj2 = this.TryGetValueFromString(sqlText);
                            if (obj2 == null)
                            {
                                this.stringBuilder.Append(sqlText);
                            }
                            else
                            {
                                this.AddParameter(obj2.AsQueryParameter());
                            }
                        }
                    }
                    else if (this.spstep == SPStep.EndWith)
                    {
                        this.AddParameter(sqlText.AsQueryParameter());
                    }
                    else
                    {
                        if (sqlText[0] != '\'')
                        {
                            throw new ArgumentException("正在等待以单引号开始的字符串，但参数不符合预期格式。");
                        }
                        this.stringBuilder.Append(sqlText.Substring(1));
                        this.spstep = SPStep.NotSet;
                    }
                }
                else
                {
                    this.stringBuilder.Append(sqlText);
                }
            }
        }

        private void AddParameter(QueryParameter p)
        {
            if (this.AutoDiscoverParameters && (this.spstep == SPStep.Skip))
            {
                throw new InvalidOperationException("正在等待以单引号开始的字符串，此时不允许再拼接其它参数。");
            }
            string str = "p" + this.int_0++.ToString();
            if (this.funcParameterQueryDelegate == this.funcAddPrefix)
            {
                string str2 = this.funcParameterQueryDelegate(str);
                this.stringBuilder.Append(str2);
                this.ParametersDict.Add(new KeyValuePair<string, QueryParameter>(str2, p));
            }
            else
            {
                this.stringBuilder.Append(this.funcParameterQueryDelegate(str));
                this.ParametersDict.Add(new KeyValuePair<string, QueryParameter>(this.funcAddPrefix(str), p));
            }
            if (this.AutoDiscoverParameters && (this.spstep == SPStep.EndWith))
            {
                this.spstep = SPStep.Skip;
            }
        }

        internal void AddSqlTextWithParameter(string s, QueryParameter p)
        {
            if (string.IsNullOrEmpty(s))
            {
                int num = this.int_0++;
                s = this.funcAddPrefix("p" + num.ToString());
            }
            this.ParametersDict.Add(new KeyValuePair<string, QueryParameter>(s, p));
        }

        private object TryGetValueFromString(string concatValue)
        {
            int result = 0;
            if (int.TryParse(concatValue, out result))
            {
                return result;
            }
            DateTime minValue = DateTime.MinValue;
            if (DateTime.TryParse(concatValue, out minValue))
            {
                return minValue;
            }
            decimal num2 = 0M;
            if (decimal.TryParse(concatValue, out num2))
            {
                return num2;
            }
            return null;
        }

        public static CPQuery New()
        {
            return new CPQuery(null, false);
        }

        public static CPQuery New(bool autoDiscoverParameters)
        {
            return new CPQuery(null, autoDiscoverParameters);
        }

        public static CPQuery operator +(CPQuery query, QueryParameter p)
        {
            query.AddParameter(p);
            return query;
        }

        public static CPQuery operator +(CPQuery query, string s)
        {
            query.AddSqlText(s);
            return query;
        }

        public CPQuery SetParameterPrefixDelegate(Func<string, string> parameterQueryDelegate, Func<string, string> paraNameDelegate)
        {
            if (parameterQueryDelegate != null)
            {
                this.funcParameterQueryDelegate = parameterQueryDelegate;
            }
            if (paraNameDelegate != null)
            {
                this.funcAddPrefix = paraNameDelegate;
            }
            return this;
        }

        private static string AddPrefix(string parameterField)
        {
            return (":" + parameterField);
        }

        public override string ToString()
        {
            return this.stringBuilder.ToString();
        }

        private enum SPStep    // 字符串参数的处理进度
        {
            NotSet,        // 没开始或者已完成一次字符串参数的拼接。
            EndWith,    // 拼接时遇到一个单引号结束
            Skip        // 已跳过一次拼接
        }
    }
}

