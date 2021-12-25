using NMCP.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Models
{
    public class DistributorModel : IDistributor
    {
        public IDistributorAuth distributorAuth { get; set; }
        public IDistributorData distributorData { get; set; }
        public IDistributorPersonalData distributorPersonalData { get; set; }
        public IReferalData referalData { get; set; }
        public IDistributorWallet distributorWallet { get; set; }
    }
}
