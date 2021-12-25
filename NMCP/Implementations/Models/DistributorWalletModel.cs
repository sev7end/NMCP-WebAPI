﻿using NMCP.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Models
{
    public class DistributorWalletModel : IDistributorWallet
    {
        public int DistributorId { get; set; }
        public decimal WalletMoney { get; set; }
        public List<ITransaction> transactions { get; set; }
    }
}
