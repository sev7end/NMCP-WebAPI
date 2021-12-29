using NMCP.Abstractions.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Core.Models
{
    public interface IDistributorPersonalData
    {
        int Id { get; set; }
        DocumentType documentType { get; set; }
        string DocumentNumber { get; set; }
        string DocumentSerial { get; set; }
        string IssueDate { get; set; }
        string ExpirtyDate { get; set; }
        string PrivateNumber { get; set; }
        string IssuingAgency { get; set; }
    }
}
