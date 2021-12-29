using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Core.Models
{
    public interface IReferalData
    {
        int Id { get; set; }
        int ReferalId { get; set; }
        int ReferedUsers { get; set; }
        int ReferallLevel { get; set; }
    }
}
