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
                StatusSports = userId > 0 ? GetSportsStatusByUserName(username) : new HideSports()
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
            criteria.FirstName = criteria.FirstName ?? string.Empty;
            criteria.LastName = criteria.LastName ?? string.Empty;
            criteria.Phone = criteria.Phone ?? string.Empty;
            criteria.MobilePhone = criteria.MobilePhone ?? string.Empty;
            criteria.Fax = criteria.Fax ?? string.Empty;

            CommandResult result = new CommandResult();            
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
                    result.Message = string.Join(null, new string[] { "<div class=\"alertboxerr\"><div>", Constants.CopyMessage.Failed, "</div></div>" });
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
                result.Code = -1;
                result.Message = string.Join(null, new string[] { "<div class=\"alertboxerr\"><div>", ex.Message, "</div></div>" });

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
        public CommandResult UpdateSyncData(CustomerInsMemberCriteria criteria)
        {
            CommandResult resultSyncData = new CommandResult();
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
                resultSyncData.Message = string.Join(null, new string[] { "<div class='alertboxerr'><div>", ex.Message, "</div></div>" });
                return resultSyncData;
            }

            return resultSyncData;
        }

        /// <summary>
        /// Update syncCasino Data after copy customer.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>        
        /// <returns>True if success else return False.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "SearchAndCopy")]
        public CommandResult UpdateSyncCasinoAfterCopySuper(CustomerInsMemberCriteria customer)
        {
            CommandResult result = new CommandResult();

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
            if (success == false)
            {
                result.Code = -1;
                result.Message = Constants.FailedSyncMessage.Casino;
                return result;
            }

            ////Sync data to IBC 
            UpdateSyncCasinoForIBCCriteria syncCasinoIBC = new UpdateSyncCasinoForIBCCriteria();
            syncCasinoIBC.CustId = customer.CustId;
            syncCasinoIBC.SyncCS = true;
            syncCasinoIBC.UserId = AppSession.AccId;
            syncCasinoIBC.LoginIP = GetClientIP();

            result = SearchCopyServiceInstance.UpdateSync(syncCasinoIBC);
            if (result.IsSuccessful == false)
            {
                result.Code = -1;
                result.Message = Constants.FailedSyncMessage.Casino;
                return result;                
            }

            return result;
        }

        /// <summary>
        /// Update syncBingo Data after copy customer.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>        
        /// <returns>True if success else return False.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "SearchAndCopy")]
        public CommandResult UpdateSyncBingoAfterCopySuper(CustomerInsMemberCriteria customer)
        {
            CommandResult result = new CommandResult();

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
            if (success == false)
            {
                result.Code = -1;
                result.Message = Constants.FailedSyncMessage.Bingo;
                return result;                            
            }

            ////Sync data to IBC 
            UpdateSyncBingoForIBCCriteria syncBingoIBC = new UpdateSyncBingoForIBCCriteria();
            syncBingoIBC.CustId = customer.CustId;
            syncBingoIBC.SyncBingo = true;
            syncBingoIBC.UserId = AppSession.AccId;
            syncBingoIBC.IP = GetClientIP();

            result = SearchCopyServiceInstance.UpdateSync(syncBingoIBC);
            if (result.IsSuccessful == false)
            {
                result.Code = -1;
                result.Message = Constants.FailedSyncMessage.Bingo;
                return result;            
            }

            return result;
        }

        /// <summary>
        /// Update syncLiveCasino Data after copy customer.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>        
        /// <returns>True if success else return False.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "SearchAndCopy")]
        public CommandResult UpdateSyncLiveCasinoAfterCopySuper(CustomerInsMemberCriteria customer)
        {
            CommandResult result = new CommandResult();

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

            result = SearchCopyServiceInstance.UpdateSync(syncLiveCasino);
            if (result.IsSuccessful == false)
            {
                result.Code = -1;
                result.Message = Constants.FailedSyncMessage.LiveCasino;
                return result;    
            }

            ////Sync data to IBC 
            UpdateSyncLiveCasinoForIBCCriteria syncLiveCasinoIBC = new UpdateSyncLiveCasinoForIBCCriteria();
            syncLiveCasinoIBC.CustId = customer.CustId;
            syncLiveCasinoIBC.SyncLiveCS = true;
            syncLiveCasinoIBC.UserId = AppSession.AccId;
            syncLiveCasinoIBC.IP = GetClientIP();

            result = SearchCopyServiceInstance.UpdateSync(syncLiveCasinoIBC);
            if (result.IsSuccessful == false)
            {
                result.Code = -1;
                result.Message = Constants.FailedSyncMessage.LiveCasino;
                return result;    
            }

            return result;
        }
        #endregion
    }
}
