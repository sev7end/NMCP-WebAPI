using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Core.Models
{
    public interface IDistributorWallet
    {
        int DistributorId { get; set; }
        decimal WalletMoney { get; set; }
        List<ITransaction> transactions { get; set; }
    }
}
