using NMCP.Core.Enums;
using NMCP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Models
{
    public class DistributorDataModel : IDistributorData
    {
        public int Id { get; set;}
        public string LastName { get; set;}
        public string FirstName { get; set;}
        public string DateOfBirth { get; set;}
        public GenderType Gender { get; set;}
        public string PictureUrl { get; set;}
        public bool IsUserVerified { get; set;}
        public ContactType contactType { get; set;}
        public string ContactInfo { get; set;}
        public AddressType addressType { get; set;}
        public string UserAddress { get; set;}
        public string UniqueId { get; set; }
        }
}
