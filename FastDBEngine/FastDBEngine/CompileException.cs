namespace FastDBEngine
{
    using System;
    using System.CodeDom.Compiler;
    using System.Runtime.CompilerServices;
    using System.Text;

    public sealed class CompileException : Exception
    {
        internal CompileException(string str, CompilerErrorCollection compilerErrorCollection, Type[] typeArray) : base(str)
        {
            this.Errors = compilerErrorCollection;
            this.Types = typeArray;
        }

        public string GetDetailMessages()
        {
            StringBuilder builder = new StringBuilder();
            if (this.Errors.Count > 0)
            {
                builder.AppendLine("编译错误消息：");
                foreach (CompilerError error in this.Errors)
                {
                    builder.AppendLine(error.ErrorText);
                }
            }
            builder.AppendLine().AppendLine("正在编译的类型：");
            foreach (Type type in this.Types)
            {
                builder.AppendLine(type.FullName);
            }
            return builder.ToString();
        }

        public CompilerErrorCollection Errors{get;set;}

        public Type[] Types{get;set;}
        
    }
}

