using System;
using System.Configuration;
using System.Web;
using Fanex.Module.NG2.Entities;
using Fanex.Module.NG2.Repository;
using ONEbook.NG2.API.DTO.CreateAccount;
using ONEbook.NG2.API.Interface;

namespace Fanex.Module.NG2.Services
{
    public class NG2StatusService : ING2StatusService
    {
        private ING2StatusRepository _repository = new NG2StatusRepository();

        public Entities.Result AutoInsertAccount(
            int custId, 
            string username,
            RoleId roleId, 
            int uplineId, 
            bool uplineDisabled,
            SiteId siteId, 
            int currencyId,
            int userId, 
            string language = "en-US", 
            int subAccId = 0)
        {
            var allowed = false;
            var result = new Entities.Result();

            if (siteId == SiteId.Alpha)
            {
                allowed = true;
            }
            else
            {
                var db = BaseRepository.CheckAllowedCurrencyAll(custId);
                var allowedNG2 = Convert.ToBoolean(db.Outputs["allowedng2"]);
                allowed = allowedNG2 && ConfigurationManager.AppSettings["IsShowNG2"] == "1";
            }
            
            if (allowed)
            {
                // Call API to sync to 3rd.
                IThirdPartyServices serviceAPI = new ONEbook.NG2.API.Services();
                var request = new ONEbook.NG2.API.DTO.CreateAccount.Request();

                request.Auth.SiteId = (int)siteId;
                request.Call.AccountArray.Add(new ONEbook.NG2.API.DTO.CreateAccount.AccountInfo { ObCustId = custId });
                request.Call.Level = (int)roleId;
                request.Call.UplineId = roleId == RoleId.Super  ? custId : uplineId;
                request.UserId = userId;
                request.Language = language;
                request.Call.CurrencyId = currencyId;
                request.Call.IsDisabled = uplineDisabled;

                var response = serviceAPI.SyncCustomers(request);

                if (response.Result.IsSuccess && (response.Result.AccountArray.Count > 0 ? response.Result.AccountArray[0].IsSuccess : true))
                {                    
                    result = SyncStatus(custId, userId, HttpContext.Current.Request.UserHostName, subAccId);

                    if (result.IsSuccess)
                    {
                        result = EnableStatus(custId, userId, uplineDisabled, HttpContext.Current.Request.UserHostAddress, subAccId);
                    }
                }
            }

            return result;
        }

        public Entities.Result EnableStatus(int custId, int userId, bool disabled, string ip, int subAccId = 0)
        {
            return _repository.EnableStatus(custId, userId, disabled, ip, subAccId);
        }

        public Entities.Result RetrySyncCustomers(Request request)
        {
            var config = ONEbook.NG2.API.Configuration.NG2Configuration.Instance;
            var result = new Entities.Result();
            IThirdPartyServices serviceAPI = new ONEbook.NG2.API.Services();

            var response = serviceAPI.RetrySyncCustomers(request);

            result.ErrorCode = response.Result.ErrorCode;
            result.ErrorMessage = response.Result.ErrorDescription;

            if (response.Result.IsSuccess)
            {
                var index = 0;
                foreach (var cust in request.Call.AccountArray)
                {
                    if ((config.Debug == "1" && response.Result.IsSuccess) || (response.Result.IsSuccess && response.Result.AccountArray[index].IsSuccess))
                    {
                        result = _repository.SyncStatus(cust.ObCustId, request.UserId, request.Ip);
                    }

                    ++index;
                }
            }
            
            return result;
        }

        public Entities.Result SyncStatus(int custId, int userId, string ip, int subAccId = 0)
        {
            return _repository.SyncStatus(custId, userId, ip, subAccId);
        }        
    }
}