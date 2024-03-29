﻿using NMCP.Core.Enums;
using NMCP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Models
{
    public class DistributorPersonalDataModel : IDistributorPersonalData
    {
        public int Id { get; set;}
        public DocumentType documentType { get; set;}
        public string DocumentNumber { get; set;}
        public string DocumentSerial { get; set;}
        public string IssueDate { get; set;}
        public string ExpirtyDate { get; set;}
        public string PrivateNumber { get; set;}
        public string IssuingAgency { get; set;}
    }
}
