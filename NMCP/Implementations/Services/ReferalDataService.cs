using NMCP.Abstractions.Models;
using NMCP.Implementations.Database;
using NMCP.Implementations.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NMCP.Implementations.Services
{
    public class ReferalDataService
    {
        private ReferalDataDbService referalDatabase;
        public ReferalDataService() 
        {
            referalDatabase = new ReferalDataDbService();
        }

        private bool IsReferalValid(int referalId) {
            return (referalDatabase.GetReferalById(referalId) != null) ? true : false;
        }
        public bool IsRefererEligible(int referalId)
        {
            return (referalDatabase.GetReferalById(referalId).ReferedUsers > 3 && referalDatabase.GetReferalById(referalId).ReferallLevel > 5) ? true : false;
        }
        private int GetRefererLevel(int referalId)
        {
            return referalDatabase.GetReferalById(referalId).ReferallLevel;
        }
        public void CreateReferal(IReferalData data)
        {
            if (IsReferalValid(data.ReferalId) && IsRefererEligible(data.ReferalId) && data.ReferalId != null)
            {
                IReferalData newReferal = new ReferalDataModel()
                {
                    Id = data.Id,
                    ReferalId = data.ReferalId,
                    ReferedUsers = 0,
                    ReferallLevel = GetRefererLevel(data.ReferalId) + 1
                };
                referalDatabase.AddItemToCollection(newReferal);
                IReferalData refererData = referalDatabase.GetReferalById(data.ReferalId);
                refererData.ReferedUsers++;
                referalDatabase.UpdateItemInCollection(refererData);
            }
            else
            {
                IReferalData newReferal = new ReferalDataModel()
                {
                    Id = data.Id,
                    ReferalId = 0,
                    ReferedUsers = 0,
                    ReferallLevel = 1
                };
                referalDatabase.AddItemToCollection(newReferal);
            }
        }
        public IReferalData GetReferalById(int Id)
        {
            return referalDatabase.GetReferalById(Id);
        }
    }
}
