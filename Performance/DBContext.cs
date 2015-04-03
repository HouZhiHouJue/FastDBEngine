using Performance.DTO;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Performance
{
    class DBContext : DbContext
    {
        public DBContext()
            : base("conn")
        { }
        public DbSet<Warehouse> WarehouseTable { get; set; }
    }
}
