using NMCP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Models
{
    public class PaidSalesModel : IPaidSales
    {
        public int ForId { get; set; }
        public string SaleId { get; set; }
    }
}
