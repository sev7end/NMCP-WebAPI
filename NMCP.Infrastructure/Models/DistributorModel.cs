using NMCP.Core.Models;
using NMCP.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Models
{
    public class DistributorModel : IDistributor
    {
        public DistributorAuthModel distributorAuth { get; set; }
        public DistributorDataModel distributorData { get; set; }
        public DistributorPersonalDataModel distributorPersonalData { get; set; }
        public ReferalDataModel referalData { get; set; }
        public DistributorWalletModel distributorWallet { get; set; }
    }
}
