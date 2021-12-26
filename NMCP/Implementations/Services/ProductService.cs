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
        public void RegisterProduct(string name, decimal unitPrice)
        {
            databaseService.AddItemToCollection(new ProductDataModel()
            {
               
                ProductName = name,
                UnitPrice = unitPrice
            });       
        }
        public IProductData GetProductById(int id)
        {
            return databaseService.GetProductById(id);
        }
        public List<IProductData> GetProducts()
        {
            return databaseService.GetProducts();
        }
    }
}
