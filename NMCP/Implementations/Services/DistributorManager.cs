using NMCP.Abstractions.Models;
using NMCP.Implementations.Database;
using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Services
{
    public class DistributorManager
    {
        private readonly AuthDbService authService;
        private readonly DistributorDataDbService distDataService;
        private readonly PersonalDataDbService personalDataService;
        private readonly ReferalDataService referalDataService;
        public DistributorManager() {
            authService = new AuthDbService();
            distDataService = new DistributorDataDbService();
            personalDataService = new PersonalDataDbService();
            referalDataService = new ReferalDataService();
        }

        public void RegisterNewDistributor(IDistributor distributor)
        {
            string UniqueIdHolder = Guid.NewGuid().ToString();
            distributor.distributorData.UniqueId = UniqueIdHolder;
            distDataService.AddItemToCollection(distributor.distributorData);
            int distributorId = distDataService.GetDistributorDataByUniqueId(UniqueIdHolder).Id;
            distributor.distributorAuth.Id = distributorId;
            distributor.distributorPersonalData.Id = distributorId;
            distributor.referalData.Id = distributorId;
            referalDataService.CreateReferal(distributor.referalData);
            personalDataService.AddItemToCollection(distributor.distributorPersonalData);
            authService.AddItemToCollection(distributor.distributorAuth);
        }

        public IDistributor GetDistributorById(int Id)
        {
            if (distDataService.GetDistributorDatas().Exists(item => item.Id == Id)) {
                var distData = distDataService.GetDistributorDataById(Id);
                var personalData = personalDataService.GetDistributorPersonalDataById(Id);
                var authData = authService.GetAuthInfoById(Id);
                var referalData = referalDataService.GetReferalById(Id);
                IDistributor distributor = new DistributorModel()
                {
                    distributorAuth = authData,
                    distributorPersonalData = personalData,
                    distributorData = distData,
                    referalData = referalData
                };
                return distributor;
            }
            return null;
        }
    }
}
