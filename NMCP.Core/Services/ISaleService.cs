using NMCP.Core.Models;
using System.Collections.Generic;

namespace NMCP.Core.Services
{
    public interface ISaleService
    {
        void RegisterPaidSale(int forId, string saleId);
        bool SaleExists(int forId, string saleId);
        int RegisterSale(ISalesData saleData);
        List<string> GetSalesByIdAndDate(int distributorId, string from, string to);
        decimal GetTotalOfSales(List<string> Ids);
        List<ISalesData> GetSalesForId(int Id);
    }
}
