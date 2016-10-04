using Fanex.Common;
using Fanex.HttpModule.NG2.CheckBelonging.Configurations.AuthenticationSection;
using Fanex.HttpModule.NG2.CheckBelonging.Repositories;
using Sunplus.Agent.Model.BL;
using Sunplus.Agent.Model.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace Fanex.HttpModule.NG2.CheckBelonging.Services
{
    public class NG2SecurityService
    {
        private string _customerClosedStatusSessionKey = "Fanex.Razorbill.Security.Services.Authentication.CheckingClosedStatus";
        private string _subAccountPermissionSessionKey = "Fanex.Razorbill.Security.Services.Authentication.CheckingCustomerPermission";
        private IUserSession _currentUserSession;
        private NG2SecurityRepository _ng2SecurityRepository;
        private IUserSession _userSession;
        private static object _blockStatus = new object();

        public NG2SecurityService()
        {
            _currentUserSession = UserSessionService.GetUserSessionInstance(HttpContext.Current.Session.SessionID);
            _ng2SecurityRepository = new NG2SecurityRepository();
        }

        public string CustomerCurrentStatus
        {
            get
            {
                return (string)HttpContext.Current.Session[string.Format("{0}_{1}", HttpContext.Current.Session.SessionID, _customerClosedStatusSessionKey)];
            }

            set
            {
                HttpContext.Current.Session[string.Format("{0}_{1}", HttpContext.Current.Session.SessionID, _customerClosedStatusSessionKey)] = value;
            }
        }

        public bool IsBelonging(int custId, long transId, bool isDataWarehouse, string currentUrlAccess)
        {
            var isBelonging = true;
            var authenticationSection = AuthenticationSection.Instance;

            // get all urls that allow he/she access by his/her level
            var levels = from q in authenticationSection.GroupLevel.Cast<GroupLevelElement>() where q.RoleId <= _currentUserSession.RoleId select q;
            if (levels.Any())
            {
                foreach (GroupLevelElement level in levels)
                {
                    // get setting of current url against config file
                    if (level != null && level.GroupLevel != null && level.GroupLevel.Count > 0)
                    {
                        var pageAccess = from q in level.GroupLevel.Cast<PageAccessElement>()
                                         where q.Url.Equals(currentUrlAccess, StringComparison.OrdinalIgnoreCase)
                                         select q;

                        if (pageAccess.Any())
                        {
                            foreach (PageAccessElement pageElement in pageAccess)
                            {
                                // Check belonging when:
                                // - downline id not equal 0
                                // - and current requesting url need to check belonging
                                // - and current requesting url not in special list: allow customers to update by themselves
                                if (custId != 0 && pageElement.IsCheckBelonging)
                                {
                                    if (!pageElement.IsAllowToDoByThemselves && UserSession.Current.CustId == custId)
                                    {
                                        return false;
                                    }

                                    if (pageElement.IsAllowToDoByThemselves && UserSession.Current.CustId == custId)
                                    {
                                        return true;
                                    }

                                    var netSessionId = HttpContext.Current.Session.SessionID;
                                    _userSession = UserSessionService.GetUserSessionInstance(netSessionId);

                                    if (pageElement.IsViewedByGroupHeadline
                                        && _ng2SecurityRepository.Age_Rpt_CustomerConsolidatedGroup_CheckBelonging(custId, _userSession.CustId, _userSession.SubAccId, transId, _userSession.IsInternal))
                                    {
                                        return true;
                                    }

                                    return isDataWarehouse
                                        ? _ng2SecurityRepository.Age_CustomerSelCheckBelonging_OLD(custId, _userSession.CustId, transId)
                                        : _ng2SecurityRepository.Age_CustomerSelCheckBelonging(custId, _userSession.CustId, _userSession.SubAccId, transId, _userSession.IsInternal);
                                }
                            }

                            break;
                        }
                    }
                }
            }

            return isBelonging;
        }

        public bool HasSubAccReadPermission(int custId)
        {
            return _ng2SecurityRepository.Age_CustomerSelCheckBelonging(custId, _currentUserSession.CustId, _currentUserSession.SubAccId, 0, _currentUserSession.IsInternal);
        }

        public bool HasSubAccUpdatePermission(int custId)
        {
            bool subAccountPermission = !_currentUserSession.IsSubAcc || _currentUserSession.SubPageAccess.UpdateAccount;
            return !_currentUserSession.IsInternal && subAccountPermission && HasSubAccReadPermission(custId);
        }

        public bool IsLoginAccClosed()
        {
            lock (_blockStatus)
            {
                if (_currentUserSession == null ||
                    _currentUserSession.CustId == null ||
                    _currentUserSession.SubAccId == null)
                {
                    return true;
                }

                if (!string.IsNullOrEmpty(CustomerCurrentStatus))
                {
                    var sessionInfo = CustomerCurrentStatus.Split('_');
                    if (sessionInfo.Length > 1 &&
                        Converter.ToDateTime(sessionInfo[1]) >= DateTime.Now)
                    {
                        return false;
                    }
                }

                var isActive = _ng2SecurityRepository.CheckingCustomerStatus(_currentUserSession.CustId, _currentUserSession.SubAccId);
                if (!isActive)
                {
                    return true;
                }

                CustomerCurrentStatus =
                    string.Format("{0}_{1}", isActive, DateTime.Now.AddSeconds(Converter.ToInt(ConfigurationManager.AppSettings["CustomerStatusExpireTime"])));

                return false;
            }
        }
    }
}
