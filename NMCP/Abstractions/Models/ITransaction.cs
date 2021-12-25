using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Abstractions.Models
{
    public interface ITransaction
    {
        int TowardsId { get; set; }
        decimal Amount { get; set; }
        string TransactionDate { get; set; }
    }
}
