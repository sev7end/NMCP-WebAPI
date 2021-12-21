using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Abstractions.Models
{
    public interface IProductData
    {
        int Id { get; set; }
        string ProductName { get; set; }
        decimal UnitPrice { get; set; }
    }
}
