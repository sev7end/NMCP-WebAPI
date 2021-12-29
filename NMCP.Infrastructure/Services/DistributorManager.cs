using NMCP.Core.Models;
using NMCP.Implementations.Database;
using NMCP.Infrastructure.Interfaces;
using NMCP.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Infrastructure.Services
{
    public class DistributorManager
    {
        private readonly AuthDbService authService;
        private readonly DistributorDataDbService distDataService;
        private readonly PersonalDataDbService personalDataService;
        private readonly ReferalDataService referalDataService;
        private readonly SaleService saleService;
        private readonly WalletDbService walletService;
        private readonly TransactionDbService transactionDbService;
        public DistributorManager() {
            authService = new AuthDbService();
            distDataService = new DistributorDataDbService();
            personalDataService = new PersonalDataDbService();
            referalDataService = new ReferalDataService();
            walletService = new WalletDbService();
            transactionDbService = new TransactionDbService();
            saleService = new SaleService();
        }

        public void RegisterNewDistributor(IDistributor distributor)
        {
            string UniqueIdHolder = Guid.NewGuid().ToString();
            distributor.distributorData.UniqueId = UniqueIdHolder;
            distDataService.AddItemToCollection(distributor.distributorData);
            int distributorId = distDataService.GetDistributorDataByUniqueId(UniqueIdHolder).Id;
            distributor.distributorAuth.Id = distributorId;
            distributor.distributorAuth.SHA512Pwd = EncryptionModule.SHA512(distributor.distributorAuth.SHA512Pwd);
            distributor.distributorPersonalData.Id = distributorId;
            distributor.referalData.Id = distributorId;
            distributor.distributorWallet = new DistributorWalletModel() {
                DistributorId = distributorId,
                WalletMoney = 0,
                transactions = new List<ITransaction>()
            };
            walletService.AddItemToCollection(distributor.distributorWallet);
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
                var walletData = walletService.GetDistributorWallet(Id);
                IDistributor distributor = new DistributorModel()
                {
                    distributorAuth = authData as DistributorAuthModel,
                    distributorPersonalData = personalData as DistributorPersonalDataModel,
                    distributorData = distData as DistributorDataModel,
                    referalData = referalData as ReferalDataModel,
                    distributorWallet = walletData as DistributorWalletModel
                };
                return distributor;
            }
            return null;
        }
        private List<string> filterPerformedSales(List<string> saleIds, int Id)
        {
            List<string> FilteredList = new List<string>();
            if (saleIds != null)
            {
                foreach (var item in saleIds)
                {
                    if (!saleService.SaleExists(Id, item))
                    {
                        FilteredList.Add(item);
                    }
                }
                return FilteredList;
            }
            return null;
        }
        public decimal GeneratePayoutForId(int Id, string from, string to)
        {
            var CurrUserSaleIds = filterPerformedSales(saleService.GetSalesByIdAndDate(Id, from, to), Id);
            
            decimal CurrUserEarnings = 0;
            if (CurrUserSaleIds != null) {
                CurrUserEarnings = saleService.GetTotalOfSales(CurrUserSaleIds);
                CurrUserEarnings = (CurrUserEarnings / 100) * 10;
                CurrUserSaleIds.ForEach(o => saleService.RegisterPaidSale(Id, o));

            }
            List<int> UserLevelTwoAffiliates = referalDataService.GetReferedUsersByIdAndLevel(Id, 2);
            List<int> UserLevelThreeAffiliates = referalDataService.GetReferedUsersByIdAndLevel(Id, 3);
            List<string> LevelTwoSales = new List<string>();
            List<string> LevelThreeSales = new List<string>();
            decimal LevelTwoEarnings = 0;
            decimal LevelThreeEarnings = 0;
            if (UserLevelTwoAffiliates!= null)
            {
                foreach(var item in UserLevelTwoAffiliates)
                {
                    var temp = filterPerformedSales(saleService.GetSalesByIdAndDate(item, from, to), Id);
                    if(temp!= null)
                        LevelTwoSales.AddRange(temp);
                }
                if (LevelTwoSales.Count != 0)
                {
                    LevelTwoEarnings = (saleService.GetTotalOfSales(LevelTwoSales) / 100) * 5;
                    LevelTwoSales.ForEach(o => saleService.RegisterPaidSale(Id, o));
                }
                if (UserLevelThreeAffiliates != null)
                {
                    foreach (var itemtwo in UserLevelThreeAffiliates)
                    {
                        var temp = filterPerformedSales(saleService.GetSalesByIdAndDate(itemtwo, from, to), Id);
                        if(temp!= null)
                            LevelThreeSales.AddRange(temp);
                    }
                    if (LevelTwoSales.Count != 0)
                    {
                        LevelThreeEarnings = saleService.GetTotalOfSales(LevelThreeSales) / 100;
                        LevelThreeSales.ForEach(o => saleService.RegisterPaidSale(Id, o));
                    }
                }
            }
            return (decimal)CurrUserEarnings + (decimal)LevelTwoEarnings + (decimal)LevelThreeEarnings;
        }
        public void AddToWallet(int ToId, decimal Amount)
        {
            var IdWallet = walletService.GetDistributorWallet(ToId);
            IdWallet.WalletMoney = IdWallet.WalletMoney + Amount;
            walletService.UpdateItemInCollection(IdWallet);
            transactionDbService.AddItemToCollection(new TransactionModel()
            {
                TowardsId = ToId,
                Amount = Amount,
                TransactionDate = DateTime.Now.ToString("dd/MM/yyyy")
            });
        }
    }
}
