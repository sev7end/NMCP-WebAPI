using Microsoft.AspNetCore.Mvc;
using NMCP.Implementations.Models;
using NMCP.Implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NMCP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorController : ControllerBase
    {
        private DistributorManager manager = new DistributorManager();
        // GET: api/<DistributorController>
        [HttpGet("{id}")]
        public DistributorModel GetDistributor(int id)
        {
            manager.GetDistributorById(id);
            return manager.GetDistributorById(id) as DistributorModel;
        }

        //// GET api/<DistributorController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<DistributorController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
            
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
        }

        // PUT api/<DistributorController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DistributorController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
