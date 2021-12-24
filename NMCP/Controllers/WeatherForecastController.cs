﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NMCP.Implementations.Database;
using NMCP.Implementations.Models;
using NMCP.Implementations.Services;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [Route("hello")]
        public string ConnStatus()
        {
            DistributorManager manager = new DistributorManager();
            DistributorModel distributor = new DistributorModel();
            distributor.distributorAuth = new DistributorAuthModel() { Email = "test@test.test", SHA512Pwd = "test", UserName = "test" };
            distributor.distributorData = new DistributorDataModel()
            {
                FirstName = "toko",
                LastName = "test",
                DateOfBirth = "22/01/2000",
                Gender = Abstractions.DataTypes.GenderType.Male,
                addressType = Abstractions.DataTypes.AddressType.ActualAddress,
                UserAddress = "mars",
                ContactInfo = "599930411",
                contactType = Abstractions.DataTypes.ContactType.Phone,
                IsUserVerified = false,
                PictureUrl = "https://avatarfiles.alphacoders.com/217/thumb-217296.jpg"
            };
            distributor.distributorPersonalData = new DistributorPersonalDataModel()
            {
                documentType = Abstractions.DataTypes.DocumentType.Idcard,
                DocumentNumber = "ASDASDASD",
                IssuingAgency = "MIA",
                ExpirtyDate = "31/12/2026",
                PrivateNumber = "011010101",
                IssueDate = "22/3/2019",
                DocumentSerial = "SmTHI"
            };
            distributor.referalData = new ReferalDataModel();
            
            
            
            
            manager.RegisterNewDistributor(distributor);
            return "done";
        }
    }
}
