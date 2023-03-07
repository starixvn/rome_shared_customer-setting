using System;
using System.Collections;
using Fanex.Data;
using Fanex.Logging.Extensions.Web;
using Fanex.Module.NG2.Entities;

namespace Fanex.Module.NG2.Repository
{
    public class NG2StatusRepository : ING2StatusRepository
    {
        public Result EnableStatus(int custId, int userId, bool disabled, string ip, byte systemId, byte settingStatus, int subAccId = 0)
        {
            var result = new Result();
            try
            {
                using (var db = new ObjectDb("Acc_CustSetting_DisableGameStatusUpd"))
                {
                    var param = new Hashtable();
                    param.Add("custid", custId);
                    param.Add("isdisabled", "<ROOT><ProductStatus SportType=\"213\" BetType=\"2501\" IsDisabled=\"" + (disabled ? "1" : "0") + "\" /></ROOT>");
                    param.Add("msgtype", 0);
                    param.Add("userid", userId);
                    param.Add("subaccid", subAccId);
                    param.Add("ip", ip);
                    param.Add("systemid", systemId);
                    param.Add("settingstatus", settingStatus);
                    param.Add("errcode", 0);
                    param.Add("errmsg", string.Empty);

                    db.ExecuteNonQuery(param);
                    result.ErrorCode = Convert.ToInt32(db.GetParameterValue("errcode"));
                    result.ErrorMessage = Convert.ToString(db.GetParameterValue("errmsg"));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                result.ErrorCode = 1;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        public Result SyncStatus(int custId, int userId, string ip, byte settingStatus, int subAccId = 0)
        {
            var result = new Result();
            try
            {
                using (var db = new ObjectDb("Acc_CustSetting_SyncGameStatusUpd"))
                {
                    var param = new Hashtable();
                    param.Add("custid", custId);
                    param.Add("issynced", true);
                    param.Add("productid", 23);
                    param.Add("userid", userId);
                    param.Add("subaccid", subAccId);
                    param.Add("ip", ip);
                    param.Add("settingstatus", settingStatus);
                    param.Add("errcode", 0);
                    param.Add("errmsg", string.Empty);

                    db.ExecuteNonQuery(param);
                    result.ErrorCode = Convert.ToInt32(db.GetParameterValue("errcode"));
                    result.ErrorMessage = Convert.ToString(db.GetParameterValue("errmsg"));
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
                result.ErrorCode = 1;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }
    }
}