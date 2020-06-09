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
        private readonly ING2StatusRepository _repository = new NG2StatusRepository();

        public Entities.Result AutoInsertAccount(
            int custId, 
            string username,
            RoleId roleId, 
            int uplineId, 
            bool uplineDisabled,
            SiteId siteId, 
            int currencyId,
            int userId,
            byte systemId,
            string language = "en-US", 
            int subAccId = 0)
        {
            var logInfo = string.Empty;
            var allowed = false;
            var result = new Entities.Result();

            if (siteId == SiteId.Alpha)
            {
                allowed = true;
            }
            else
            {
                var db = BaseRepository.CheckAllowedCurrencyAll(custId);
                allowed = Convert.ToBoolean(db.Outputs["allowedng2"]);
            }
            
            if (allowed)
            {
                try
                {
                    // Call API to sync to 3rd.
                    IThirdPartyServices serviceAPI = new ONEbook.NG2.API.Services();
                    var request = new ONEbook.NG2.API.DTO.CreateAccount.Request();

                    request.Auth.SiteId = (int)siteId;
                    request.Call.AccountArray.Add(new ONEbook.NG2.API.DTO.CreateAccount.AccountInfo { ObCustId = custId });
                    request.Call.Level = (int)roleId;
                    request.Call.UplineId = roleId == RoleId.Super ? custId : uplineId;
                    request.UserId = userId;
                    request.Language = language;
                    request.Call.CurrencyId = currencyId;
                    request.Call.IsDisabled = uplineDisabled;

                    logInfo += "1. Calling CreateAccount - CustId: " + request.Call.AccountArray[0].ObCustId;
                    var response = serviceAPI.SyncCustomers(request);
                    logInfo += "<br />2. Result from 3rd - Success: " + response.Result.IsSuccess;

                    if (response.Result.IsSuccess && (response.Result.AccountArray.Count > 0 ? response.Result.AccountArray[0].IsSuccess : true))
                    {
                        logInfo += "<br />3. Calling update sync at MBC";
                        result = SyncStatus(custId, userId, HttpContext.Current.Request.UserHostName, subAccId);

                        logInfo += "<br />4. Update sync at MBC - Success: " + result.IsSuccess;
                        if (result.IsSuccess)
                        {
                            logInfo += "<br />5. Calling update disable status: " + uplineDisabled;
                            result = EnableStatus(custId, userId, uplineDisabled, HttpContext.Current.Request.UserHostAddress, systemId, subAccId);
                            logInfo += "<br />6. Update disable status - Success: " + result.IsSuccess;
                        }
                    }
                }
                catch (Exception ex)
                {
                    logInfo += "<br />Exception occurs: " + ex;
                }

                if (roleId == RoleId.Super)
                {
                    Fanex.Logging.Logger.Log.Info(logInfo);
                }
            }                  

            return result;
        }

        public Entities.Result EnableStatus(int custId, int userId, bool disabled, string ip, byte systemId, int subAccId = 0)
        {
            return _repository.EnableStatus(custId, userId, disabled, ip, systemId, subAccId);
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
                        if (result.IsSuccess)
                        {
                            result = EnableStatus(cust.ObCustId, request.UserId, request.Call.IsDisabled.Value, request.Ip, GetSystemId(request.Auth.SiteId));
                        }
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

        private byte GetSystemId(int siteId)
            => ((SiteId)siteId) == SiteId.Alpha ? (byte)2 : (byte)1;
    }
}