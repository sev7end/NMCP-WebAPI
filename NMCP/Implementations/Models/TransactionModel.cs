using NMCP.Abstractions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Models
{
    public class TransactionModel : ITransaction
    {
        public int TowardsId { get; set; }
        public decimal Amount { get; set; }
        public string TransactionDate { get; set; }
    }
}
