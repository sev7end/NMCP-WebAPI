﻿using NMCP.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Models
{
    public class ReferalDataModel : IReferalData
    {
        public int Id { get; set;}
        public int ReferalId { get; set;}
        public int ReferedUsers { get; set;}
        public int ReferallLevel { get; set;}
    }
}
