using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Performance.DTO
{
    [Table("TMS.DIC_WAREHOUSES")]
    public class Warehouse
    {
        [Key]
        [Column("PKID")]
        public int Pkid { get; set; }
        [Column("WAREHOUSECODE")]
        public string Warehousecode { get; set; }
        [Column("WAREHOUSENAME")]
        public string Warehousename { get; set; }
        [Column("WAREHOUSEADDRESS")]
        public string Warehouseaddress { get; set; }
        [Column("WAREHOUSETYPE")]
        public string Warehousetype { get; set; }
        [Column("WAREHOUSEWEIGHT")]
        public float? Warehouseweight { get; set; }
        [Column("WAREHOUSEKIND")]
        public string Warehousekind { get; set; }
        [Column("WAREHOUSEWORKTIME")]
        public string Warehouseworktime { get; set; }
        [Column("WAREHOUSEOVERTIME")]
        public string Warehouseovertime { get; set; }
        [Column("WAREHOUSECHANGETIME")]
        public string Warehousechangetime { get; set; }
        [Column("WAREHOUSEOVERTIMEPRICE")]
        public float? Warehouseovertimeprice { get; set; }
        [Column("WAREHOUSECONTACTUSER")]
        public string Warehousecontactuser { get; set; }
        [Column("WAREHOUSETELEPHONE")]
        public string Warehousetelephone { get; set; }
        [Column("WAREHOUSEFAX")]
        public string Warehousefax { get; set; }
        [Column("WAREHOUSEJCCOST")]
        public float? Warehousejccost { get; set; }
        [Column("WAREHOUSEJBCOST")]
        public float? Warehousejbcost { get; set; }
        [Column("REMARK")]
        public string Remark { get; set; }
        [Column("STATUS")]
        public int? Status { get; set; }
        [Column("WAREHOUSECOLLECTPRICE")]
        public float? Warehousecollectprice { get; set; }
        [Column("WAREHOUSEAREA")]
        public string Warehousearea { get; set; }
        [Column("WAREHOUSEPROVINCE")]
        public string Warehouseprovince { get; set; }
        [Column("WAREHOUSECITY")]
        public string Warehousecity { get; set; }
        [Column("SHIPMENTADDRESSTYPE")]
        public int? Shipmentaddresstype { get; set; }
        [Column("ADDED_BY")]
        public string Addedby { get; set; }
        [Column("ADDED_TIME")]
        public DateTime? Addedtime { get; set; }
        [Column("LAST_MODIFIED_BY")]
        public string Lastmodifiedby { get; set; }
        [Column("LAST_MODIFIED_IP")]
        public string Lastmodifiedip { get; set; }
        [Column("LAST_MODIFIED_TIME")]
        public DateTime? Lastmodifiedtime { get; set; }
        [Column("ADDED_NAME")]
        public string Addedname { get; set; }
        [Column("LAST_MODIFIED_NAME")]
        public string Lastmodifiedname { get; set; }
        [Column("VALID")]
        public int? Valid { get; set; }
    }


}
