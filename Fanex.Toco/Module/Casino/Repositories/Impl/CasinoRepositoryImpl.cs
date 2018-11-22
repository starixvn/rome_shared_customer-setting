namespace Fanex.CustomerSetting.Module.Casino.Repositories.Impl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using Core;
    using Core.Models;
    using Criteria;
    using Data;
    using Data.Repository;
    using Entities;
    using Interface;

    public class CasinoRepositoryImpl : CasinoRepository
    {
        public CasinoCustomer GetCustInfoIBC(int ibcCustId, int uplineId)
        {
            DataSet dr;
            var uplineDisableCasino = false;

            Hashtable param = new Hashtable();
            param.Add("custid", ibcCustId);

            using (IObjectDb db = new ObjectDb("Age_CustomerSelUserInfo"))
            {
                db.AssignParamValues(param);
                dr = db.ExecuteDataSet();
            }

            if (uplineId != 0)
            {
                var uplineParam = new Hashtable();
                uplineParam.Add("custid", uplineId);

                using (IObjectDb db = new ObjectDb("Age_CustomerSelUserInfo"))
                {
                    db.AssignParamValues(uplineParam);
                    var upline = db.ExecuteDataSet();
                    uplineDisableCasino = (bool)upline.Tables[0].Rows[0]["disablecasino"];
                }
            }

            DataRow row = null;
            CasinoCustomer customerDetail = new CasinoCustomer();
            customerDetail.GeneralInfo.CasinoNo = "01";
            customerDetail.GeneralInfo.UplineDisabledCasino = uplineDisableCasino;

            if (dr.Tables[0].Rows.Count > 0)
            {
                row = dr.Tables[0].Rows[0];
                customerDetail.GeneralInfo.UserName = Utils.ToString(row["username"]);
                customerDetail.GeneralInfo.UserName2 = Utils.ToString(row["username2"]);
                customerDetail.GeneralInfo.FirstName = Utils.ToString(row["firstname"]);
                customerDetail.GeneralInfo.LastName = Utils.ToString(row["lastname"]);
                customerDetail.GeneralInfo.Phone = Utils.ToString(row["phone"]);
                customerDetail.GeneralInfo.MobilePhone = Utils.ToString(row["mobilephone"]);
                customerDetail.GeneralInfo.Fax = Utils.ToString(row["fax"]);
                customerDetail.GeneralInfo.Email = Utils.ToString(row["email"]);
                customerDetail.GeneralInfo.RoleId = Utils.ToInt32(row["roleid"]);
                customerDetail.GeneralInfo.Currency = Utils.ToInt32(row["currency"]);
                customerDetail.GeneralInfo.Closed = Utils.ToBoolean(row["closed"]);
                customerDetail.GeneralInfo.Suspend = Utils.ToBoolean(row["suspend"]);
                customerDetail.SIBCCustID = Utils.ToInt32(row["superid"]);
                customerDetail.MIBCCustID = Utils.ToInt32(row["masterid"]);
                customerDetail.AIBCCustID = Utils.ToInt32(row["agentid"]);
                customerDetail.GeneralInfo.GivenCredit = Utils.ToDecimal(row["credit"]).Format();
                customerDetail.GeneralInfo.CasinoSync = Utils.ToBoolean(row["synccasino"]);
                customerDetail.GeneralInfo.CasinoDisabled = Utils.ToBoolean(row["disablecasino"]);
                // TO DO hard code

                customerDetail.GeneralInfo.GoldDeluxeDisabled = Utils.ToBoolean(row["disablegolddeluxe"]);
                customerDetail.GeneralInfo.IBCCustID = Utils.ToInt32(row["custid"]);
                customerDetail.UserPwd = Utils.ToString(row["userpwd"]);
                customerDetail.GeneralInfo.UpperCustID = uplineId;
                customerDetail.GeneralInfo.NoCSData = true;
                customerDetail.GeneralInfo.IsEnablePT = true;

                // Get max transfer information

                if (uplineId == 0)
                {
                    var allCustomerInfoParam = new Hashtable();
                    allCustomerInfoParam.Add("username", customerDetail.GeneralInfo.UserName);

                    using (IObjectDb db = new ObjectDb("SelAllInfo"))
                    {
                        db.AssignParamValues(allCustomerInfoParam);
                        var allCustomerInfoDr = db.ExecuteDataSet().Tables[0].Rows[0];

                        customerDetail.GeneralInfo.IsDeposit = Utils.ToBoolean(allCustomerInfoDr["isdeposit"]);
                        customerDetail.GeneralInfo.SiteName = customerDetail.GeneralInfo.IsDeposit
                            ? Utils.ToString(allCustomerInfoDr["site"])
                            : "ibcbet";
                    }
                }

                customerDetail.GeneralInfo.NoIBCData = false;
            }

            return customerDetail;
        }

        public bool GetCasinoStatus(int custid)
        {
            Hashtable param = new Hashtable();
            param.Add("custid", custid);

            using (IObjectDb db = new ObjectDb("Age_GetCSStatus"))
            {
                db.AssignParamValues(param);

                db.ExecuteDataSet();
                var outputs = db.GetOuputParams();

                return Utils.ToBoolean(outputs["status"]);
            }
        }

        public IList<int> GetEnabledJackpotProducts(GetEnabledJackpotProductsCriteria criteria)
        {
            var productIds = new List<int>();

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    if (reader != null)
                    {
                        while (reader.Read())
                        {
                            if (!Convert.IsDBNull(reader["productid"]))
                            {
                                productIds.Add(Convert.ToInt32(reader["productid"]));
                            }
                        }
                    }
                }
            }

            return productIds;
        }

        public Result ExecuteNonQuery(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());

                return new Result(Utils.ToInt32(db.GetParameterValue("errcode")), Utils.ToString(db.GetParameterValue("errmsg")));
            }
        }
    }
}
