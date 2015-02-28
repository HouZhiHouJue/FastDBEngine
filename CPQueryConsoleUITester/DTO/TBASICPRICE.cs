using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPQueryConsoleUITester.DTO
{
    public class TBASICPRICE
    {
        public int Pkid { get; set; }
        public string Area { get; set; }
        public string Categoryname { get; set; }
        public string Spectificationname { get; set; }
        public string Materialname { get; set; }
        public string Factoryname { get; set; }
        public int? Basicprice { get; set; }
        public int? Trend { get; set; }
        public int? Percentage { get; set; }
        public int? Differprice { get; set; }
        public int? Sortno { get; set; }
        public int? Addedby { get; set; }
        public DateTime? Addedtime { get; set; }
        public int? Lastmodifiedby { get; set; }
        public DateTime? Lastmodifiedtime { get; set; }
        public string Lastmodifiedip { get; set; }
        public string Valid { get; set; }
    }
}
