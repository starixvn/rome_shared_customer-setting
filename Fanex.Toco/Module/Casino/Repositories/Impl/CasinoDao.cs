// --------------------------------------------------------------------------
// <copyright file="CasinoDao.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.4.11">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.Casino.Repositories.Impl
{
    using System;
    using System.Collections;
    using System.Data;
    using CasinoLibrary.SBAgent;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.Data.DNAide;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CasinoDao
    {
        public CasinoCustomer GetCustInfoIBC(int ibcCustId, int uplineId)
        {
            Hashtable param = new Hashtable();
            param.Add("custid", ibcCustId);

            var uplineParam = new Hashtable();
            uplineParam.Add("custid", uplineId);

            DbResult dr = DbHelper.ExecuteDbResult("Age_CustomerSelUserInfo", param, 300);
            var upline = DbHelper.ExecuteDbResult("Age_CustomerSelUserInfo", uplineParam, 300);

            DataRow row = null;
            CasinoCustomer customerDetail = new CasinoCustomer();
            customerDetail.UplineDisabledCasino = (bool)upline.ResultSet.Tables[0].Rows[0]["disablecasino"];

            if (dr.ResultSet.Tables[0].Rows.Count > 0)
            {
                row = dr.ResultSet.Tables[0].Rows[0];
                customerDetail.UserName = Convert.ToString(row["username"]);
                customerDetail.UserName2 = Convert.ToString(row["username2"]);
                customerDetail.FirstName = Convert.ToString(row["firstname"]);
                customerDetail.LastName = Convert.ToString(row["lastname"]);
                customerDetail.Phone = Convert.ToString(row["phone"]);
                customerDetail.MobilePhone = Convert.ToString(row["mobilephone"]);
                customerDetail.Fax = Convert.ToString(row["fax"]);
                customerDetail.Email = Convert.ToString(row["email"]);
                customerDetail.RoleId = Convert.ToInt32(row["roleid"]);
                customerDetail.Currency = Convert.ToInt32(row["currency"]);
                customerDetail.Closed = Convert.ToBoolean(row["closed"]);
                customerDetail.Suspend = Convert.ToBoolean(row["suspend"]);
                customerDetail.SIBCCustID = Convert.ToInt32(row["superid"]);
                customerDetail.MIBCCustID = Convert.ToInt32(row["masterid"]);
                customerDetail.AIBCCustID = Convert.ToInt32(row["agentid"]);

                customerDetail.GivenCredit = Convert.ToInt32(row["credit"]);

                customerDetail.CasinoSync = Convert.ToBoolean(row["synccasino"]);
                customerDetail.CasinoDisabled = Convert.ToBoolean(row["disablecasino"]);

                customerDetail.LiveCasinoSync = Convert.ToBoolean(row["synclivecasino"]);
                customerDetail.LiveCasinoDisabled = Convert.ToBoolean(row["disablelivecasino"]);

                customerDetail.IBCCustID = Convert.ToInt32(row["custid"]);
                customerDetail.UserPwd = Convert.ToString(row["userpwd"]);
                customerDetail.UpperCustID = uplineId;
                customerDetail.NoCSData = true;
                customerDetail.IsEnablePT = true;

                // Get max transfer information
                CasinoLibrary.SBAgent.Customer customer = new CasinoLibrary.SBAgent.Customer();                
                DataTable dt = customer.GetDefaultMaxTransfer(customerDetail.UpperCustID, customerDetail.Currency, false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    customerDetail.MaxTransferCreditLimit = Convert.ToDouble(dt.Rows[0]["MaxLimit"]);
                    customerDetail.MinTransferCreditLimit = Convert.ToDouble(dt.Rows[0]["MinLimit"]);
                    customerDetail.DefaultTransferCredit = Convert.ToDouble(dt.Rows[0]["DefMaxTransferCredit"]);
                }

                customerDetail.NoIBCData = false;
            }
                        
            return customerDetail;
        }

        public bool GetCasinoStatus(int custid)
        {
            Hashtable param = new Hashtable();
            param.Add("custid", custid);

            DbResult dr = DbHelper.ExecuteDbResult("Age_GetCSStatus", param, 300);
            return Convert.ToBoolean(dr.Outputs["status"]);
        }

        public bool UpdateCSStatus(int custid, bool syncCS, bool disabledCS, int userId, string ip, int subAccId, string lang, string username, string subUsername)
        {
            Hashtable param = new Hashtable();
            param.Add("custid", custid);
            param.Add("disabledcs", disabledCS);
            param.Add("synccs", syncCS);
            param.Add("enabledownline", false);

            param.Add("userid", userId);
            param.Add("subaccid", subAccId);
            param.Add("loginip", ip);
            param.Add("language", lang);

            DbResult dr = DbHelper.ExecuteDbResult("Acc_UpdCSStatus", param, 300);

            bool status = Convert.ToBoolean(dr.Outputs["status"]);

            if (!status)
            {
                return false;
            }

            Customer cust = new Customer();
            return cust.UpdateCustEnableCasino(custid, !disabledCS, true, ip, custid, subAccId == 0 ? username : subUsername, 2);
        }

        public bool DoUpdateCSStatusWithoutSync(int custid, bool syncCS, bool disabledCS, bool isUpdDownline, int userId, string ip, int subAccId, string lang, out string errMsg)
        {
            Hashtable param = new Hashtable();
            param.Add("custid", custid);
            param.Add("disabledcs", disabledCS);
            param.Add("synccs", syncCS);
            param.Add("enabledownline", isUpdDownline);

            param.Add("userid", userId);
            param.Add("subaccid", subAccId);
            param.Add("loginip", ip);
            param.Add("language", lang);

            DbResult dr = DbHelper.ExecuteDbResult("Acc_UpdCSStatus", param, 300);

            errMsg = dr.Outputs["errmsg"].ToString();
            return Convert.ToBoolean(dr.Outputs["status"]);
        }
    }
}
