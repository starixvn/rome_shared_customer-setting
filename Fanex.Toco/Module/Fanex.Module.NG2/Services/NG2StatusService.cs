using System.Configuration;
using System.Web;
using Fanex.CustomerSetting.Module.Core;
using Fanex.CustomerSetting.Module.Core.Criteria;
using Fanex.CustomerSetting.Module.Core.Models;
using Fanex.CustomerSetting.Module.Core.Services.Impl;
using Fanex.CustomerSetting.Module.Core.Web;
using Fanex.Module.NG2.Entities;
using Fanex.Module.NG2.Repository;
using ONEbook.NG2.API.Interface;

namespace Fanex.Module.NG2.Services
{
    public class NG2StatusService : ING2StatusService
    {
        private ING2StatusRepository _repository = new NG2StatusRepository();

        public Result AutoInsertAccount(int custId, string username, RoleId roleId, int uplineId, bool uplineDisabled, SiteId siteId)
        {
            var allowed = false;
            var result = new Result();

            if (siteId == SiteId.Alpha)
            {
                allowed = true;
            }
            else
            {
                var service = new ServiceBase();
                var hasSpecialPermission = service.CheckSpecialSitePermission(new CheckSpecialSitePermissionCriteria
                {
                    CustID = custId,
                    CustName = username,
                    UserID = AppSession.CurrentSession.UserId
                });

                var db = Fanex.Module.NG2.Repository.BaseRepository.CheckAllowedCurrencyAll(custId);
                var allowedNG2 = Utils.ToBoolean(db.Outputs["allowedng2"]);
                allowed = allowedNG2 && hasSpecialPermission.IsSuccess && Utils.ToBoolean(ConfigurationManager.AppSettings["IsShowNG2"]);
            }
            
            if (allowed)
            {
                // Call API to sync to 3rd.
                IThirdPartyServices serviceAPI = new ONEbook.NG2.API.Services();
                var request = new ONEbook.NG2.API.DTO.Request();

                request.Auth.SiteId = ((int)siteId).ToString();
                request.Call.CustIds.Add(custId);
                request.Call.RoleId = (int)roleId;
                request.Call.UplineId = roleId == RoleId.Super  ? custId : uplineId;
                request.Call.UserId = AppSession.CurrentSession.UserId;

                var response = serviceAPI.SyncCustomers(request);

                if (response.Result.ResultDetail[0].IsSuccess)
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