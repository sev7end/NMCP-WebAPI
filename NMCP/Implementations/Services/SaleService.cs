using NMCP.Abstractions.Models;
using NMCP.Implementations.Database;
using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Services
{
    public class SaleService
    {
        private readonly ProductService productService;
        private readonly SalesDataDbService dbService;
        private readonly DistributorDataDbService distributorService;
        private readonly PaidSaleDbService paidSaleService;
        public SaleService()
        {
            dbService = new SalesDataDbService();
            distributorService = new DistributorDataDbService();
            productService = new ProductService();
            paidSaleService = new PaidSaleDbService();
        }

        /* Status codes: 
         * 0 : success
         * 1 : product not found
         * 2 : distributor not found
         */
        public void RegisterPaidSale(int forId, string saleId)
        {
            IPaidSales paidSale = new PaidSalesModel()
            {
                ForId = forId,
                SaleId = saleId
            };
            paidSaleService.AddItemToCollection(paidSale);
        }
        public bool SaleExists(int forId, string saleId)
        {
            return paidSaleService.CheckIfSaleExists(forId, saleId);
        }

        /* Status codes: 
         * 0 : success
         * 1 : product not found
         * 2 : distributor not found
         */
        public int RegisterSale(ISalesData saleData)
        {
            var item = productService.GetProductById(saleData.ProductId);
            if (item != null )
            {
                if (distributorService.GetDistributorDataById(saleData.DistributorId) != null)
                {
                    saleData.SaleId = Guid.NewGuid().ToString();
                    dbService.AddItemToCollection(saleData);
                    saleData.UnitsTotalPrice = saleData.UnitsSold * item.UnitPrice;
                    return 0;
                }
                else return 2;
            }
            else return 1;
        }
        public List<string> GetSalesByIdAndDate(int distributorId, string from, string to)
        {
            var sales = dbService.GetSaleBetweenDatesAndId(distributorId, from, to);
            List<string> saleIds = new List<string>();
            sales.ForEach(o => saleIds.Add(o.SaleId));
            return saleIds.Count != 0 ? saleIds : null;
        }

        public decimal GetTotalOfSales(List<string> Ids)
        {
            List<ISalesData> sales = new List<ISalesData>();
            foreach (var id in Ids)
            {
                sales.Add(dbService.GetSaleById(id));
            }
            decimal total = 0;
            sales.ForEach(o => {
                total = total + o.UnitsTotalPrice;
            });
            return total;
        }
        public List<ISalesData> GetSalesForId(int Id)
        {
            return dbService.GetSalesByUserId(Id);
        }
    }
}
