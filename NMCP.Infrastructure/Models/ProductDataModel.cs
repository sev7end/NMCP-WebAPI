using NMCP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Models
{
    public class ProductDataModel : IProductData
    {
        public int Id { get; set;}
        public string ProductName { get; set;}
        public decimal UnitPrice { get; set;}
    }
}
