// --------------------------------------------------------------------------
// <copyright file="SuperController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>
namespace Fanex.CustomerSetting.Module.SearchCopy.Controllers
{
    using System;
    using System.Configuration;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.CustomerSetting.Module.SearchCopy.Common;
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;
    using Fanex.CustomerSetting.Module.SearchCopy.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.Logging;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class SuperController : BaseCloneController
    {        
        /// <summary>
        /// Render search existing super form.
        /// </summary>        
        /// <returns>The ActionResult.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "SearchAndCopy")]
        public ActionResult Index()
        {            
            string accIdSessionName = ConfigurationManager.AppSettings["AccIdSessionName"].ToString();
            ViewBag.CurrentUserId = Fanex.Common.Converter.ToInt(System.Web.HttpContext.Current.Session[accIdSessionName]);
            ViewBag.CurrentURL = ConfigurationManager.AppSettings["CurrentURL"].ToString();
          
            return View("~/Areas/CustomerSetting/Views/SearchCopy/Super/Index.cshtml");           
        }

        /// <summary>
        /// Searching a existing super user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Return JSONRESULT.</returns>
        public JsonResult SearchExistingSuper(string username)
        {
            var criteria = new SearchExistingUserCriteria
            {
                Uplineid = 0,
                Username = username,
                Custid = -1,
                Issubacc = false
            };

            int userId = SearchExistingUser(criteria);
            
            CustomerSettingView customerSettingView = new CustomerSettingView
            {
                UserId = userId,
                Username = username,
                StatusSports = new HideSports() ////userId > 0 ? GetSportsStatusByUserName(username) : new HideSports()
            };

            return Json(customerSettingView, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Copy existing super.
        /// </summary>
        /// <param name="criteria">The criteria for copying.</param>
        /// <returns>The JSONRESULT .</returns>        
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "SearchAndCopy")]
        public JsonResult CopyExistingSuper(CustomerInsMemberCriteria criteria)
        {            
            criteria.Userpwd = IBCUtils.Encryption.CfsEnCode(criteria.Userpwd);
            criteria.FirstName = criteria.FirstName ?? string.Empty;
            criteria.LastName = criteria.LastName ?? string.Empty;
            criteria.Phone = criteria.Phone ?? string.Empty;
            criteria.MobilePhone = criteria.MobilePhone ?? string.Empty;
            criteria.Fax = criteria.Fax ?? string.Empty;

            CommandResult result = new CommandResult { Code = 0, Message = string.Empty };
            try
            {
                result = SearchCopyServiceInstance.CopyExistingUser(ref criteria);

                if (result.IsSuccessful)
                {
                    result.Message = string.Join(null, new string[] { "<div class=\"alertboxscc\"><div>", Constants.CopyMessage.Success, "</div></div>" });

                    CommandResultBase resultSyncData = UpdateSyncData(criteria);
                    if (resultSyncData.IsSuccessful == false)
                    {
                        result.Message += resultSyncData.Message;
                    }
                }
                else
                {
                    if (result.Message == string.Empty)
                    {
                        result.Message = string.Join(null, new string[] { "<div class=\"alertboxerr\"><div>", Constants.CopyMessage.Failed, "</div></div>" });
                    }
                    else
                    {
                        result.Message = string.Join(null, new string[] { "<div class=\"alertboxerr\"><div>", result.Message, "</div></div>" });
                    }
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                result.Code = -1;
                result.Message = string.Join(null, new string[] { "<div class=\"alertboxerr\"><div>", Constants.CopyMessage.Failed, "</div></div>" });

                return Json(result, JsonRequestBehavior.AllowGet);
            }          
                      
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Update Sync

        /// <summary>
        /// Update sync Data after copy customer.
        /// </summary>
        /// <param name="criteria">Information of Customer.</param>   
        /// <returns>True if success else return False.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "SearchAndCopy")]
        private CommandResult UpdateSyncData(CustomerInsMemberCriteria criteria)
        {
            CommandResult resultSyncData = new CommandResult { Code = 0, Message = string.Empty };
            try
            {
                resultSyncData = UpdateSyncCasinoAfterCopySuper(criteria);
                if (resultSyncData.IsSuccessful)
                {
                    resultSyncData = UpdateSyncBingoAfterCopySuper(criteria);
                    if (resultSyncData.IsSuccessful)
                    {
                        resultSyncData = UpdateSyncLiveCasinoAfterCopySuper(criteria);
                    }
                }

                if (resultSyncData.IsSuccessful == false)
                {
                    resultSyncData.Message = string.Join(null, new string[] { "<div class='alertboxerr'><div>", resultSyncData.Message, "</div></div>" });
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                resultSyncData.Code = -1;
                resultSyncData.Message = string.Join(null, new string[] { "<div class=\"alertboxerr\"><div>", ex.Message, "</div></div>" });
                return resultSyncData;
            }          

            return resultSyncData;
        }

        /// <summary>
        /// Update syncCasino Data after copy customer.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>        
        /// <returns>True if success else return False.</returns>        
        private CommandResult UpdateSyncCasinoAfterCopySuper(CustomerInsMemberCriteria customer)
        {
            CommandResult result = new CommandResult { Code = 0, Message = string.Empty };
            if (customer.IsSyncCasino == false)
            {
                return result;
            }

            try
            {
                bool success = SyncCasino(customer);
                if (success == false)
                {
                    result.Code = -1;
                    result.Message = Constants.FailedSyncMessage.Casino;
                    return result;
                }

                ////Sync data to IBC 
                result = UpdateSyncDataToIBC(customer.CustId, new UpdateSyncCasinoForIBCCriteria());                
                if (result.IsSuccessful == false)
                {
                    result.Code = -1;
                    result.Message = Constants.FailedSyncMessage.Casino;
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                result.Code = -1;
                result.Message = Constants.FailedSyncMessage.Casino;
                return result;
            }

            return result;
        }

        /// <summary>
        /// Synchronous Casino.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>   
        /// <returns>True if success else return False.</returns>
        private bool SyncCasino(CustomerInsMemberCriteria customer)
        {
            ////Sync data to Casino 
            CasinoLibrary.SBAgent.Customer casinoCustomer = new CasinoLibrary.SBAgent.Customer();
            bool success = casinoCustomer.AutoInsertSuperFromExisting(
                                                                                    "01",
                                                                                    customer.CustId,
                                                                                    customer.ExistingCustId,
                                                                                    customer.UserName,
                                                                                    string.Empty,
                                                                                    customer.FirstName,
                                                                                    customer.LastName,
                                                                                    customer.Userpwd,
                                                                                    customer.Phone,
                                                                                    customer.MobilePhone,
                                                                                    customer.Fax,
                                                                                    customer.CurrencyId,
                                                                                    DateTime.Now,
                                                                                    GetClientIP(),
                                                                                    AppSession.AccId,
                                                                                    AppSession.AccUserName,
                                                                                    3);
            return success;
        }

        /// <summary>
        /// Update syncBingo Data after copy customer.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>        
        /// <returns>True if success else return False.</returns>        
        private CommandResult UpdateSyncBingoAfterCopySuper(CustomerInsMemberCriteria customer)
        {
            CommandResult result = new CommandResult { Code = 0, Message = string.Empty };
            if (customer.IsSyncBingo == false)
            {
                return result;
            }

            try
            {
                bool success = SyncBingo(customer);
                if (success == false)
                {
                    result.Code = -1;
                    result.Message = Constants.FailedSyncMessage.Bingo;
                    return result;
                }

                ////Sync data to IBC 
                result = UpdateSyncDataToIBC(customer.CustId, new UpdateSyncBingoForIBCCriteria());
                if (result.IsSuccessful == false)
                {
                    result.Code = -1;
                    result.Message = Constants.FailedSyncMessage.Bingo;
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                result.Code = -1;
                result.Message = Constants.FailedSyncMessage.Bingo;
                return result;
            }

            return result;
        }

        /// <summary>
        /// Synchronous Bingo.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>   
        /// <returns>True if success else return False.</returns>
        private bool SyncBingo(CustomerInsMemberCriteria customer)
        {
            ////Sync data to Bingo                                                            
            LiveCasinoLibrary.SBAgent.Customer bingoCustomer = new LiveCasinoLibrary.SBAgent.Customer();
            bool success = bingoCustomer.AutoInsertSuperFromExisting(
                                                                                    "01",
                                                                                    customer.CustId,
                                                                                    customer.ExistingCustId,
                                                                                    customer.UserName,
                                                                                    DateTime.Now,
                                                                                    GetClientIP(),
                                                                                    AppSession.AccId,
                                                                                    AppSession.AccUserName,
                                                                                    3);
            return success;
        }

        /// <summary>
        /// Update syncLiveCasino Data after copy customer.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>        
        /// <returns>True if success else return False.</returns>        
        private CommandResult UpdateSyncLiveCasinoAfterCopySuper(CustomerInsMemberCriteria customer)
        {
            CommandResult result = new CommandResult { Code = 0, Message = string.Empty };
            if (customer.IsSyncLiveCasino == false)
            {
                return result;
            }

            try
            {
                result = SyncLiveCasino(customer);
                if (result.IsSuccessful == false)
                {
                    result.Code = -1;
                    result.Message = Constants.FailedSyncMessage.LiveCasino;
                    return result;
                }

                ////Sync data to IBC 
                result = UpdateSyncDataToIBC(customer.CustId, new UpdateSyncLiveCasinoForIBCCriteria());
                if (result.IsSuccessful == false)
                {
                    result.Code = -1;
                    result.Message = Constants.FailedSyncMessage.LiveCasino;
                    return result;
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                result.Code = -1;
                result.Message = Constants.FailedSyncMessage.LiveCasino;
                return result;
            }

            return result;
        }

        /// <summary>
        /// Synchronous Live Casino.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>
        /// <returns>True if success else return False.</returns>
        private CommandResult SyncLiveCasino(CustomerInsMemberCriteria customer)
        {
            ////Sync data to Live Casino                                        
            UpdateSyncLiveCasinoCriteria syncLiveCasino = new UpdateSyncLiveCasinoCriteria();
            syncLiveCasino.CasinoNo = "01";
            syncLiveCasino.CustID = customer.CustId;
            syncLiveCasino.OldCustID = customer.ExistingCustId;            
            syncLiveCasino.SignupIP = GetClientIP();
            syncLiveCasino.SignupBy = AppSession.AccId;
            syncLiveCasino.SignupFrom = 3;
            syncLiveCasino.ModifyUserName = AppSession.AccUserName;
            syncLiveCasino.ToUserName = customer.UserName;

            return SearchCopyServiceInstance.UpdateSync(syncLiveCasino);            
        }

        /// <summary>
        /// Update syncLiveCasino Data to IBC.
        /// </summary>
        /// <param name="custid">The customer id.</param>
        /// <param name="sysnIBC">The criteria.</param>
        /// <returns>True if success else return False.</returns>
        private CommandResult UpdateSyncDataToIBC(int custid, UpdateSyncBaseForIBCCriteria sysnIBC)
        {
            CommandResult result = new CommandResult { Code = 0, Message = string.Empty };
            sysnIBC.CustId = custid;            
            sysnIBC.UserId = AppSession.AccId;
            sysnIBC.LoginIP = GetClientIP();

            result = SearchCopyServiceInstance.UpdateSync(sysnIBC);         

            return result;
        }
        #endregion
    }
}
