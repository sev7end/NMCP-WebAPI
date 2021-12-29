using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NMCP.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminstratorController : ControllerBase
    {
        DistributorManager distributorManager = new DistributorManager();
        [HttpPost]
        [Route("Payout")] //Date format : dd/mm/yyyy
        public string DoPayoutForUser(int Id, string FromDate, string ToDate)
        {
            decimal Amount = distributorManager.GeneratePayoutForId(Id, FromDate, ToDate);
            distributorManager.AddToWallet(Id, Amount);
            return $"{Amount}$ was added to wallet belonging to distributor by ID: {Id}";
        }
    }
}
