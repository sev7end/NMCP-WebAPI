using Microsoft.AspNetCore.Mvc;
using NMCP.Core.Models;
using NMCP.Infrastructure.Models;
using NMCP.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private SaleService saleService = new SaleService();
        [HttpPost]
        [Route("Register")] //Date format : dd/mm/yyyy
        public int RegisterSale(SalesDataModel data)
        {
           /* Status codes: 
             * 0 : success
             * 1 : product not found
             * 2 : distributor not found
             */
            return saleService.RegisterSale(data);
        }
        [HttpGet]
        [Route("{Id}")]
        public List<ISalesData> GetSalesForId(int Id)
        {
            return saleService.GetSalesForId(Id);
        }
    }
}
