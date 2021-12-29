using Microsoft.AspNetCore.Mvc;
using NMCP.Core.Models;
using NMCP.Infrastructure.Models;
using NMCP.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace NMCP.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorController : ControllerBase
    {
        private DistributorManager manager = new DistributorManager();
        private ProductService productService = new ProductService();
        
        [HttpGet]
        [Route("{id}")]
        public DistributorModel GetDistributor(int id)
        {
            return manager.GetDistributorById(id) as DistributorModel;
        }

        [HttpPost]
        [Route("RegisterDistributor")]
        public void RegisterDistributor(DistributorModel model)
        {
            #region temp
            /*
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
            */
            #endregion
            manager.RegisterNewDistributor(model);
        }


        [HttpPost]
        [Route("Products/Add")]
        public void RegisterProduct(string name, decimal price)
        {
            productService.RegisterProduct(name, price);
        }

        [HttpGet]
        [Route("Products/{id}")]
        public IProductData GetProduct(int id)
        {
            return productService.GetProductById(id);
        }

        [HttpGet]
        [Route("Products")]
        public List<IProductData> GetProducts(int id)
        {
            return productService.GetProducts();
        }
    }
}
