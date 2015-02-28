namespace FastDBEngineProfilerLib
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Text;

    [Serializable]
    public sealed class ExecuteInfo
    {
        public string AppName;
        public string CommandText;
        public System.Data.CommandType CommandType;
        public string ConnectionString;
        public DateTime? EndTime;
        public string ExceptionMessage;
        public string InfoKey;
        public FastDBEngineProfilerLib.InfoType InfoType;
        public bool IsWithTranscation;
        public List<ParamValuePair> ParameterValues;
        public DateTime StartTime;
        public string XmlCommandName;

        public string GetParameterValuesShowText()
        {
            if ((this.ParameterValues == null) || (this.ParameterValues.Count == 0))
            {
                return string.Empty;
            }
            StringBuilder builder = new StringBuilder();
            if (this.ParameterValues.Count >= 1)
            {
                builder.AppendFormat("{0}={1}", this.ParameterValues[0].Name, this.ParameterValues[0].Value);
            }
            if (this.ParameterValues.Count >= 2)
            {
                builder.Append(" ; ");
                builder.AppendFormat("{0}={1}", this.ParameterValues[1].Name, this.ParameterValues[1].Value);
            }
            if (this.ParameterValues.Count >= 3)
            {
                builder.Append(" ; ");
                builder.AppendFormat("{0}={1}", this.ParameterValues[2].Name, this.ParameterValues[2].Value);
            }
            if (this.ParameterValues.Count >= 4)
            {
                builder.Append(" ...");
            }
            return builder.ToString();
        }
    }
}

