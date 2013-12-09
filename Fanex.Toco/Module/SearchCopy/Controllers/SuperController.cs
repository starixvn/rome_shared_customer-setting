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
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;
    using Fanex.CustomerSetting.Module.SearchCopy.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.Logging;
    using Fanex.NAP.Framework.Data;
    using Fanex.CustomerSetting.Module.SearchCopy.Common;

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

            CommandResult result = _searchCopyService.CopyExistingUser(criteria);
            try
            {
                if (result.IsSuccessful)
                {
                    result = UpdateSyncCasinoAfterCopySuper(criteria);
                    if (result.IsSuccessful)
                    {
                        result = UpdateSyncBingoAfterCopySuper(criteria);
                        if (result.IsSuccessful)
                        {
                            result = UpdateSyncLiveCasinoAfterCopySuper(criteria);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogService.Error(ex.Message);
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Update Sync

        /// <summary>
        /// Update syncCasino Data after copy customer.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>        
        /// <returns>True if success else return False.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "SearchAndCopy")]
        public CommandResult UpdateSyncCasinoAfterCopySuper(CustomerInsMemberCriteria customer)
        {            
            if (customer.StatusSetting.SyncCasino == true)
            {
                ////Sync data to Casino 
                CasinoLibrary.SBAgent.Customer casinoCustomer = new CasinoLibrary.SBAgent.Customer();
                bool success = casinoCustomer.AutoInsertSuperFromExisting(
                                                                                        "01",
                                                                                        customer.CustId,
                                                                                        customer.ExistingCustId,
                                                                                        customer.UserName,
                                                                                        customer.UserName,
                                                                                        customer.FirstName,
                                                                                        customer.LastName,
                                                                                        customer.Userpwd,
                                                                                        customer.Phone,
                                                                                        customer.MobilePhone,
                                                                                        customer.Fax,
                                                                                        customer.CurrencyId,
                                                                                        new DateTime(),
                                                                                        GetClientIP(),
                                                                                        AppSession.AccId,
                                                                                        AppSession.AccUserName,
                                                                                        3);
                if (success == false)
                {
                    return new CommandResultBase(-1, Constants.FailedCopyMessage.Casino) as CommandResult;
                }

                ////Sync data to IBC 
                UpdateSyncCasinoForIBCCriteria syncCasinoIBC = new UpdateSyncCasinoForIBCCriteria();
                syncCasinoIBC.CustId = customer.CustId;
                syncCasinoIBC.SyncCS = true;
                syncCasinoIBC.UserId = AppSession.AccId;
                syncCasinoIBC.LoginIP = GetClientIP();

                var result = _searchCopyService.UpdateSync((ICriteria)syncCasinoIBC);
                if (result.IsSuccessful == false)
                {
                    return new CommandResultBase(-1, Constants.FailedCopyMessage.Casino) as CommandResult;
                }             
            }

            return new CommandResultBase(0, string.Empty) as CommandResult;
        }

        /// <summary>
        /// Update syncBingo Data after copy customer.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>        
        /// <returns>True if success else return False.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "SearchAndCopy")]
        public CommandResult UpdateSyncBingoAfterCopySuper(CustomerInsMemberCriteria customer)
        {            
            if (customer.StatusSetting.SyncBingo == true)
            {
                ////Sync data to Bingo                                                            
                LiveCasinoLibrary.SBAgent.Customer bingoCustomer = new LiveCasinoLibrary.SBAgent.Customer();
                bool success = bingoCustomer.AutoInsertSuperFromExisting(
                                                                                        "01",
                                                                                        customer.CustId,
                                                                                        customer.ExistingCustId,
                                                                                        customer.UserName,
                                                                                        new DateTime(),
                                                                                        GetClientIP(),
                                                                                        AppSession.AccId,
                                                                                        AppSession.AccUserName,
                                                                                        3);
                if (success == false)
                {
                    return new CommandResultBase(-1, Constants.FailedCopyMessage.Bingo) as CommandResult;
                }

                ////Sync data to IBC 
                UpdateSyncBingoForIBCCriteria syncBingoIBC = new UpdateSyncBingoForIBCCriteria();
                syncBingoIBC.CustId = customer.CustId;
                syncBingoIBC.SyncBingo = true;
                syncBingoIBC.UserId = AppSession.AccId;
                syncBingoIBC.IP = GetClientIP();

                var result = _searchCopyService.UpdateSync((ICriteria)syncBingoIBC);
                if (result.IsSuccessful == false)
                {
                    return new CommandResultBase(-1, Constants.FailedCopyMessage.Bingo) as CommandResult;
                }
            }

            return new CommandResultBase(0, string.Empty) as CommandResult;
        }

        /// <summary>
        /// Update syncLiveCasino Data after copy customer.
        /// </summary>
        /// <param name="customer">Information of Customer.</param>        
        /// <returns>True if success else return False.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Host, "SearchAndCopy")]
        public CommandResult UpdateSyncLiveCasinoAfterCopySuper(CustomerInsMemberCriteria customer)
        {            
            if (customer.StatusSetting.SyncCasino == true)
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

                var result = _searchCopyService.UpdateSyncLiveCasino(syncLiveCasino);
                if (result.IsSuccessful == false)
                {
                    return new CommandResultBase(-1, Constants.FailedCopyMessage.LiveCasino) as CommandResult;
                }

                ////Sync data to IBC 
                UpdateSyncLiveCasinoForIBCCriteria syncLiveCasinoIBC = new UpdateSyncLiveCasinoForIBCCriteria();
                syncLiveCasinoIBC.CustId = customer.CustId;                
                syncLiveCasinoIBC.SyncLiveCS = true;
                syncLiveCasinoIBC.UserId = AppSession.AccId;
                syncLiveCasinoIBC.IP = GetClientIP();

                result = _searchCopyService.UpdateSync((ICriteria)syncLiveCasinoIBC);
                if (result.IsSuccessful == false)
                {
                    return new CommandResultBase(-1, Constants.FailedCopyMessage.LiveCasino) as CommandResult;
                }
            }

            return new CommandResultBase(0, string.Empty) as CommandResult;
        }
        #endregion
    }
}
