using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FastDBEngine;

namespace FastDBEngineGenerator
{
    internal class GerneratorEFHelper
    {
        private StringBuilder sb;
        private string classname;
        public GerneratorEFHelper(string tablename)
        {
            sb = new StringBuilder(1024);
            this.classname = Util.GetClassName(tablename);
        }


        public string GetEFCommand()
        {
            Push(string.Format("public class {0}DAL",classname));
            Left();
            GetAddEFCommand();
            GetUpdateEFCommand();
            GetDeleteEFCommand();
            Right();
            return sb.ToString();
        }




        private int start = 10;
        private int increase = 5;

        private void GetAddEFCommand()
        {
            Push(string.Format("public bool Add{0}({0} info)", classname));
            Left();
            Push("using (DBContext dbContext = new DBContext())");
            Left();
            Push(string.Format("dbContext.Set<{0}>().Add(info);", classname));
            Push("return dbContext.SaveChanges() > 0;");
            Right();
            Right();
            End();
        }

        private void GetUpdateEFCommand()
        {
            Push(string.Format("public bool Update{0}({0} info)", classname));
            Left();
            Push("using (DBContext dbContext = new DBContext())");
            Left();
            Push(string.Format("dbContext.Set<{0}>().Attach(info);", classname));
            Push(string.Format("dbContext.Entry<{0}>(info).State = System.Data.Entity.EntityState.Modified;", classname));
            Push("return dbContext.SaveChanges() > 0;");
            Right();
            Right();
            End();
        }

        private void GetDeleteEFCommand()
        {
            Push(string.Format("public bool Delete{0}(long pkid)", classname));
            Left();
            Push("using (DBContext dbContext = new DBContext())");
            Left();
            Push(string.Format("{0} info = new ", classname));
            sb.Append(string.Format(" {0}() ", classname));
            sb.Append(" { Pkid = pkid };");
            Push(string.Format("dbContext.Set<{0}>().Attach(info);", classname));
            Push(string.Format("dbContext.Entry<{0}>(info).State = System.Data.Entity.EntityState.Deleted;", classname));
            Push("return dbContext.SaveChanges() > 0;");
            Right();
            Right();
            End();
        }

        private void Push(string text)
        {
            sb.Append(Environment.NewLine + text.Pad(start));
        }

        private void Left()
        {
            sb.Append(Environment.NewLine + "{".Pad(start));
            start += increase;
        }

        private void Right(int number = 1)
        {
            for (int i = 0; i < number; i++)
            {
                start -= increase;
                sb.Append(Environment.NewLine + "}".Pad(start));
            }

        }

        private void End()
        {
            sb.Append(Environment.NewLine);
        }

    }
}
