using NMCP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Models
{
    public class SalesDataModel : ISalesData
    {
        public string SaleId { get; set;}
        public int DistributorId { get; set;}
        public string SaleDate { get; set;}
        public int ProductId { get; set;}
        public int UnitsSold { get; set;}
        public decimal UnitsTotalPrice { get; set;}
    }
}
