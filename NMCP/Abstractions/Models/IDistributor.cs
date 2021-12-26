using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Abstractions.Models
{
    public interface IDistributor
    {
        DistributorAuthModel distributorAuth { get; set; }
        DistributorDataModel distributorData { get; set; }
        DistributorPersonalDataModel distributorPersonalData { get; set; }
        ReferalDataModel referalData { get; set; }
        DistributorWalletModel distributorWallet { get; set; }
    }
}
