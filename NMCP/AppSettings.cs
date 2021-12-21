using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP
{
    public static class AppSettings
    {
        public static readonly string MSAccessDBPath = ConfigurationManager.AppSettings["MSAccessDBPath"].ToString();
    }
}
