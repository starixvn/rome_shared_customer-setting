using Fanex.CustomerSetting.Module.Core.Models;
using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
using Fanex.Data;
using System;

namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    public class PermissionService
    {
        public Result CheckSpecialSitePermission(CheckSpecialSitePermissionCriteria criteria)
        {
            Result result = new Result();
            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                db.ExecuteNonQuery();
                var outputs = db.GetOuputParams();
                result.ErrorCode = Convert.IsDBNull(outputs["errcode"]) ? 0 : Convert.ToInt32(outputs["errcode"]);
                result.ErrorMessage = Convert.IsDBNull(outputs["errmsg"]) ? string.Empty : outputs["errmsg"].ToString();
            }

            return result;
        }
    }
}