using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Abstractions.Models
{
    public interface ISalesData
    {
        string SaleId { get; set; }
        int DistributorId { get; set; }
        string SaleDate { get; set; }
        int ProductId { get; set; }
        int UnitsSold { get; set; }
        decimal UnitsTotalPrice { get; set; }
    }
}
