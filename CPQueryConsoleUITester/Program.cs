using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FastDBEngine;
using System.Reflection;

namespace CPQueryConsoleUITester
{
    class Program
    {
        static void Main(string[] args)
        {
            Product p = new Product();
            p.ProductID = 1;
            p.ProductName = "2";
            var query = "select ProductID, ProductName from Products where (1=1) ".AsCPQuery(true);

            if (p.ProductID > 0)
                query = query + " and ProductID = " + p.ProductID.ToString();

            if (string.IsNullOrEmpty(p.ProductName) == false)
                query = query + " and ProductName like '" + p.ProductName + "'";

            if (p.CategoryID > 0)
                query = query + " and CategoryID = " + p.CategoryID.ToString();

            if (string.IsNullOrEmpty(p.Unit) == false)
                query = query + " and Unit = '" + p.Unit + "'";

            if (p.UnitPrice > 0)
                query = query + " and UnitPrice >= " + p.UnitPrice.ToString();

            if (p.Quantity > 0)
                query = query + " and Quantity >= " + p.Quantity.ToString();
        }
    }
}
