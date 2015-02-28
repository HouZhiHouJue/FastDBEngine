namespace FastDBEngine
{
    using System;
    using System.Runtime.CompilerServices;

    public class PagingInfo
    {
        public int CalcPageCount()
        {
            if ((this.PageSize == 0) || (this.TotalRecords == 0))
            {
                return 0;
            }
            return (int) Math.Ceiling((double) (((double) this.TotalRecords) / ((double) this.PageSize)));
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }
    }
}

