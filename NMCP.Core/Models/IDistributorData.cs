using NMCP.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Core.Models
{
    public interface IDistributorData
    {
        int Id { get; set; }
        string LastName { get; set; }
        string FirstName { get; set; }
        string DateOfBirth { get; set; }
        GenderType Gender { get; set; }
        string PictureUrl { get; set; }
        bool IsUserVerified { get; set; }
        ContactType contactType { get; set; }
        string ContactInfo { get; set; }
        AddressType addressType { get; set; }
        string UserAddress { get; set; }
        string UniqueId { get; set; }
    }
}
