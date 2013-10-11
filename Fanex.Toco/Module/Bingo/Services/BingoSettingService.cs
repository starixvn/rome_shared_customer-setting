// ************************************************************************************
// <copyright file="BingoSettingService.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="kan.nguyen">Create</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.Module.Bingo.Services
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web;
    using Bingo.Configuration;
    using Bingo.Repositories;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Services.Impl;
    using Fanex.CustomerSetting.Module.Core.Extensions;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Microsoft.Practices.Unity;
    using Sunplus.Bingo.Core;
    using Sunplus.Bingo.Core.Common;
    using Sunplus.Bingo.Data;
    using Sunplus.Bingo.Entities;
    using Sunplus.Bingo.Services;

    /// <summary>
    /// BingoSettingService class.
    /// </summary>
    public class BingoSettingService : IBingoSettingService
    {
        private readonly IBingoDAO _bingoDAO;

        public BingoSettingService()
            : this(null)
        {
        }

        public BingoSettingService(IBingoDAO bingoDAO)
        {
            this._bingoDAO = bingoDAO ?? new BingoDAO();
        }

        #region IBingoSettingService Members

        /// <summary>
        /// Gets the customer setting.
        /// </summary>
        /// <param name="custID">The customer ID.</param>
        /// <param name="updatedByRoleID">The updated by role ID.</param>
        /// <returns>The customer object.</returns>
        public Customer GetCustomerSetting(int custID, int updatedByRoleID)
        {
            Customer customer = new Customer();
            try
            {
                var customerService = BingoContainer.Current.Resolve<ICustomerService>();

                IResult result = new Sunplus.Bingo.Entities.Result();
                customer = customerService.GetCustomerSetting(custID, updatedByRoleID, ref result) as Customer;
                if (customer != null)
                {
                    customer.Result = result as Sunplus.Bingo.Entities.Result;
                }
            }
            catch (Exception ex)
            {
                customer.Result = new Sunplus.Bingo.Entities.Result(1, ex.Message);
            }

            return customer;
        }

        /// <summary>
        /// Updates the customer setting.
        /// </summary>
        /// <param name="customer">The customer.</param>
        /// <param name="updatedByRoleID">The updated by role ID.</param>
        /// <param name="fromSystem">From system.</param>
        /// <returns>The result object.</returns>
        public IResult UpdateCustomerSetting(Customer customer, int updatedByRoleID, int fromSystem)
        {
            IResult result = null;
            try
            {
                var customerService = BingoContainer.Current.Resolve<ICustomerService>();
                string fromIP = GetClientIP();

                var loginUser = AppSession.CurrentUser;
                result = customerService.UpdateCustomerSetting(customer, updatedByRoleID, fromIP, loginUser.Id, loginUser.Username, fromSystem);
                if (result != null && result.ErrorCode == 0 && !customer.SyncBingo)
                {
                    bool disableBingo = false;
                    CasinoCustomer custInfo = null, uplineInfo = null;
                    custInfo = CasinoManager.Instance.GetCustInfoIBC(customer.ID, customer.UplineID);
                    disableBingo = custInfo.BingoDisabled;
                    
                    var role = (Enumerators.Role)customer.RoleId;
                    if (role == Enumerators.Role.Member)
                    {
                        uplineInfo = CasinoManager.Instance.GetCustInfoIBC(custInfo.AIBCCustID, custInfo.UpperCustID);
                        disableBingo = uplineInfo.BingoDisabled;
                    }
                    else if (role == Enumerators.Role.Agent)
                    {
                        uplineInfo = CasinoManager.Instance.GetCustInfoIBC(custInfo.MIBCCustID, custInfo.UpperCustID);
                        disableBingo = uplineInfo.BingoDisabled;
                    }
                    else if (role == Enumerators.Role.Master)
                    {
                        uplineInfo = CasinoManager.Instance.GetCustInfoIBC(custInfo.SIBCCustID, custInfo.UpperCustID);
                        disableBingo = uplineInfo.BingoDisabled;
                    }

                    this._bingoDAO.CustomerUpdateDisabledBingoGame(loginUser.Id, customer.ID, disableBingo, fromIP, disableBingo, true);
                }
            }
            catch (Exception ex)
            {
                result = new Sunplus.Bingo.Entities.Result(1, ex.Message);
            }

            return result;
        }

        /// <summary>
        /// Resets the max transfer.
        /// </summary>
        /// <param name="custId">The customer ID.</param>
        /// <param name="fromSystem">From system.</param>
        /// <returns>The result object.</returns>
        public IResult ResetMaxTransfer(int custId, int fromSystem, int modifiedUserId, string modifiedUsername)
        {
            string fromIP = GetClientIP();
            var customerService = BingoContainer.Current.Resolve<ICustomerService>();
            IResult result = customerService.ResetMaxTransfer(custId, fromIP, modifiedUserId, modifiedUsername, fromSystem);

            return result;
        }

        /// <summary>
        /// Autoes the insert bingo account.
        /// </summary>
        /// <param name="customerInfo">The customer info.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="userName">Name of the user.</param>
        /// <param name="ip">The IP address.</param>
        /// <param name="fromSystem">From system.</param>
        public void AutoInsertBingoAccount(CasinoCustomer customerInfo, int userId, string userName, string ip, int fromSystem)
        {
            try
            {
                bool enabledBingo = AppSetting.IsAllowMoveToBingo;
                Task.Factory.StartNew(() =>
                {
                    if (enabledBingo)
                    {
                        var bingoLibray = new LiveCasinoLibrary.SBAgent.Customer();
                        bool isSuccess = true;
                        isSuccess = bingoLibray.AutoInsertAccount(
                            customerInfo.CasinoNo,
                            customerInfo.IBCCustID,
                            customerInfo.UserName,
                            customerInfo.SIBCCustID,
                            customerInfo.MIBCCustID,
                            customerInfo.AIBCCustID,
                            customerInfo.RoleId.ToString(),
                            customerInfo.ExchangeID,
                            DateTime.Now,
                            ip,
                            userId,
                            userName,
                            fromSystem);

                        Thread.Sleep(2 * 1000);
                        if (isSuccess)
                        {
                            IResult iResult = null;
                            var bingoDao = BingoContainer.Current.Resolve<ICustomerDAO>();
                            bingoDao.UpdateSyncBingo(customerInfo.IBCCustID, true, ip, userId, ref iResult);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                LogExtension.Log(ex);
            }
        }

        #endregion IBingoSettingService Members

        /// <summary>
        /// Gets the client IP.
        /// </summary>
        /// <returns>Client IP address.</returns>
        private string GetClientIP()
        {
            string ip = string.Empty;
            try
            {
                if (HttpContext.Current.Request.IsLocal)
                {
                    ip = "127.0.0.1";
                }
                else
                {
                    ip = Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]);
                }
            }
            catch (Exception)
            {
            }

            return ip;
        }
    }
}