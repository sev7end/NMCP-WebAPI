﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Core.Models
{
    public interface IPaidSales
    {
        int ForId { get; set; }
        string SaleId { get; set; }
    }
}
