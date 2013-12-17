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
                LogExtension.Log(ex);
                customer.Result = new Sunplus.Bingo.Entities.Result(1, "Not bingo's user");
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
        public IResult UpdateCustomerSetting(Customer customer, int updatedByRoleID, int fromSystem, int modifiedUserId, string modifiedUsername)
        {
            try
            {
                if ((Enumerators.Role)customer.RoleId == Enumerators.Role.Super)
                {
                    return new Result(1, "Invalid customer");
                }

                ICustomerService customerService = BingoContainer.Current.Resolve<ICustomerService>();
                string fromIP = AppSession.CurrentContext.Ip4Address;
                IResult result = customerService.UpdateCustomerSetting(customer, updatedByRoleID, fromIP, modifiedUserId, modifiedUsername, fromSystem);
                
                if (result == null || !result.IsSuccessful || customer.SyncBingo)
                {
                    return result;
                }
                
                CasinoCustomer custInfo = null, uplineInfo = null;
                custInfo = CasinoManager.Instance.GetCustInfoIBC(customer.ID, customer.UplineID);
                
                switch ((Enumerators.Role)customer.RoleId)
                {
                    case Enumerators.Role.Member:
                        uplineInfo = CasinoManager.Instance.GetCustInfoIBC(custInfo.AIBCCustID, custInfo.UpperCustID);
                        break;
                    case Enumerators.Role.Agent:
                        uplineInfo = CasinoManager.Instance.GetCustInfoIBC(custInfo.MIBCCustID, custInfo.UpperCustID);
                        break;
                    case Enumerators.Role.Master:
                        uplineInfo = CasinoManager.Instance.GetCustInfoIBC(custInfo.SIBCCustID, custInfo.UpperCustID);
                        break;
                    default:
                        break;
                }

                return this._bingoDAO.UpdateBingoStatus(modifiedUserId, customer.ID, uplineInfo.BingoDisabled, fromIP, uplineInfo.EnableDownline, true);
            }
            catch (Exception ex)
            {
                LogExtension.Log(ex);
                return new Sunplus.Bingo.Entities.Result(1, "Not bingo's user");
            }
        }

        /// <summary>
        /// Resets the max transfer.
        /// </summary>
        /// <param name="custId">The customer ID.</param>
        /// <param name="fromSystem">From system.</param>
        /// <returns>The result object.</returns>
        public IResult ResetMaxTransfer(int custId, int fromSystem, int modifiedUserId, string modifiedUsername)
        {
            string fromIP = AppSession.CurrentContext.Ip4Address;

            IResult result = null;
            try
            {
                var customerService = BingoContainer.Current.Resolve<ICustomerService>();
                result = customerService.ResetMaxTransfer(custId, fromIP, modifiedUserId, modifiedUsername, fromSystem);
            }
            catch (Exception ex)
            {
                LogExtension.Log(ex);
                result = new Sunplus.Bingo.Entities.Result(1, "Not bingo's user");
            }

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
            if (!AppSetting.IsAllowMoveToBingo)
            {
                return;
            }

            try
            {
                Task.Factory.StartNew(() =>
                {
                    var bingoLibray = new LiveCasinoLibrary.SBAgent.Customer();
                    bool isSuccess = bingoLibray.AutoInsertAccount(
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
                });
            }
            catch (Exception ex)
            {
                LogExtension.Log(ex);
            }
        }

        #endregion IBingoSettingService Members
    }
}