using System.Configuration;
using System.Web;
using Fanex.CustomerSetting.Module.Core;
using Fanex.CustomerSetting.Module.Core.Models;
using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.Module.NG2.Entities;
using Fanex.Module.NG2.Repository;
using ONEbook.NG2.API.Interface;

namespace Fanex.Module.NG2.Services
{
    public class NG2StatusService : ING2StatusService
    {
        private ING2StatusRepository _repository = new NG2StatusRepository();

        public Result AutoInsertAccount(int custId, string username, RoleId roleId, int uplineId, bool uplineDisabled, SiteId siteId, string language = "en-US")
        {
            var allowed = false;
            var result = new Result();

            if (siteId == SiteId.Alpha)
            {
                allowed = true;
            }
            else
            {
                var db = BaseRepository.CheckAllowedCurrencyAll(custId);
                var allowedNG2 = Utils.ToBoolean(db.Outputs["allowedng2"]);
                allowed = allowedNG2 && Utils.ToBoolean(ConfigurationManager.AppSettings["IsShowNG2"]);
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
                request.UserId = AppSession.CurrentSession.UserId;
                request.Language = language;

                var response = serviceAPI.SyncCustomers(request);

                if (response.Result.IsSuccess && response.Result.AccountArray.Count > 0 ? response.Result.AccountArray[0].IsSuccess : true)
                {                    
                    result = SyncStatus(custId, AppSession.CurrentSession.UserId, HttpContext.Current.Request.UserHostName);

                    if (result.IsSuccess)
                    {
                        result = EnableStatus(custId, AppSession.CurrentSession.UserId, uplineDisabled, HttpContext.Current.Request.UserHostAddress);
                    }
                }
            }

            return result;
        }

        public Result EnableStatus(int custId, int userId, bool disabled, string ip)
        {
            return _repository.EnableStatus(custId, userId, disabled, ip);
        }

        public Result SyncStatus(int custId, int userId, string ip)
        {
            return _repository.SyncStatus(custId, userId, ip);
        }
    }
}