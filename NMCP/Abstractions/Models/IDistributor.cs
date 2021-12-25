using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Abstractions.Models
{
    public interface IDistributor
    {
        IDistributorAuth distributorAuth { get; set; }
        IDistributorData distributorData { get; set; }
        IDistributorPersonalData distributorPersonalData { get; set; }
        IReferalData referalData { get; set; }
        IDistributorWallet distributorWallet { get; set; }
    }
}
