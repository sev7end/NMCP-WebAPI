using NMCP.Abstractions.Models;
using NMCP.Implementations.Database;
using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Services
{
    public class ProductService
    {
        private readonly ProductDataDbService databaseService;

        public ProductService()
        {
            databaseService = new ProductDataDbService();
        }
        public bool RegisterProduct(int id,string name, decimal unitPrice)
        {
            if (databaseService.GetProductById(id) == null)
            {
                databaseService.AddItemToCollection(new ProductDataModel()
                {
                    Id = id,
                    ProductName = name,
                    UnitPrice = unitPrice
                });
                return true;
            }
            return false;
        }
        public IProductData GetProductById(int id)
        {
            return databaseService.GetProductById(id);
        }
    }
}
