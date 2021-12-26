using NMCP.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Models
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
