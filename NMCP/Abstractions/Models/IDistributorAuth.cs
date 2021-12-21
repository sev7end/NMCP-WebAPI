using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Abstractions.Models
{
    public interface IDistributorAuth
    {
        int Id { get; set; }
        string Email { get; set; }
        string UserName { get; set; }
        string SHA512Pwd { get; set; }
        string TempPwd { get; set; }
    }
}
