using NMCP.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Interfaces
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
