using System;
using System.Collections;
using System.Collections.Generic;
using Fanex.Data;
using Fanex.Module.NG2.Entities;

namespace Fanex.Module.NG2.Repository
{
    public class SyncCustomerRepository : ISyncCustomerRepository
    {
        public IList<Customer> Customers(string custIds)
        {
            var result = new List<Customer>();
            try
            {
                using (var db = new ObjectDb("Acc_CustSetting_SyncAPICustomerProfileSelect_CustID"))
                {
                    var param = new Hashtable();
                    param.Add("custids", custIds);

                    using (var reader = db.ExecuteReader(param))
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                result.Add(new Customer { CustId = (int)reader["custid"], IsSyncInMBC = (bool)reader["issync"], Username = (string)reader["username"] });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Logger.Log.Error(ex.Message, ex);
            }

            return result;
        }

        public IList<LogInfo> Logs(bool isTest = true)
        {
            var result = new List<LogInfo>();
            try
            {
                using (var db = new ObjectDb("Acc_UserLog_ExternalAPI_SelSyncedCustomer"))
                {
                    var param = new Hashtable();
                    param.Add("istestonly", isTest);

                    using (var reader = db.ExecuteReader(param))
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                result.Add(new LogInfo { CustId = (int)reader["custid"], LogId = (long)reader["logid"] });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Logger.Log.Error(ex.Message, ex);
            }
            
            return result;
        }

        public bool UpdateIsNotified(string logIds)
        {
            try
            {
                using (var db = new ObjectDb("Acc_UserLog_ExternalAPI_UpdisNotified"))
                {
                    var param = new Hashtable();
                    param.Add("logids", logIds);

                    db.ExecuteNonQuery(param);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Logging.Logger.Log.Error(ex.Message, ex);
            }

            return false;
        }
    }
}
