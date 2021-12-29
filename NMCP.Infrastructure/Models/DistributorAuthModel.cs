using NMCP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Models
{
    public class DistributorAuthModel : IDistributorAuth
    {
        public int Id { get; set;}
        public string Email { get; set;}
        public string UserName { get; set;}
        public string SHA512Pwd { get; set;}
        public string TempPwd { get; set;}
    }
}
