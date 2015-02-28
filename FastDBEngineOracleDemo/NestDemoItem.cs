using FastDBEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastDBEngineOracleDemo
{
    public sealed class NestDemoItem
    {
        [DbColumn(SubItemPrefix = "BaseInfo.")]		// 注意：这里的SubItemPrefix="BaseInfo." 与 sql 语句中的输出列名有关。
        public CustBaseInfo BaseInfo;

        [DbColumn(SubItemPrefix = "*")] // "*" 等效于： "AddrInfo."
        public CustAddInfo AddrInfo;

        [DbColumn(SubItemPrefix = "*")] // "*" 等效于： "OrdersInfo."
        public CustOrdersInfo OrdersInfo;
    }



    // 这些嵌套的成员也可以不使用DbColumn来说明，例如：
    public sealed class NestDemoItem2
    {
        public CustBaseInfo BaseInfo;

        public CustAddInfo AddrInfo;

        public CustOrdersInfo OrdersInfo;


        // 那么就要使用以下语句来加载数据。
        public static readonly string LoadSqlText = @"
	select top (@TopN) * from (
		select 
			c.CustomerID, 
			c.CustomerName,  
			c.ContactName, 
			c.Tel,  
			c.Address, 
			c.PostalCode,  
			(select count(*) from Orders where CustomerID = c.CustomerID) as [Count],
			(select sum(SumMoney) from Orders where CustomerID = c.CustomerID) as [SumMoney]
		from Customers as c
		) as t
	where [Count] > 0
	order by [SumMoney] desc;";

    }




    public sealed class CustBaseInfo
    {
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string ContactName { get; set; }
        public string Tel { get; set; }
    }

    public sealed class CustAddInfo
    {
        public string Address { get; set; }
        public string PostalCode { get; set; }
    }

    public sealed class CustOrdersInfo
    {
        public int Count;
        public decimal SumMoney;
    }


    // 再来个复杂嵌套类型的示例，FastDBEngineProfilerLib 支持无限级嵌套，只要能找到对应的数据字段就能加载。

    public sealed class NestDemoItem3 : NestDemoItem3b
    {
        public CustBaseInfo BaseInfo;
    }

    public class NestDemoItem3b : NestDemoItem3c
    {
        public CustAddInfo AddrInfo;
    }

    public class NestDemoItem3c : NestDemoItem3d
    {
        public CustOrdersInfo OrdersInfo;
    }

    public class NestDemoItem3d
    {
        // 故意放二个【不能加载】的成员，因为它们没有没有对应的数据表字段

        public string AAAAAA;

        public int BBBBB;
    }
}
