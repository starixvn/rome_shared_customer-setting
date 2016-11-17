using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Fanex.Caching;
using Fanex.Module.NG2.Entities;
using Fanex.Module.NG2.Repository;
using ONEbook.NG2.API.DTO.CheckIsSyncCustomer;
using ONEbook.NG2.API.Interface;

namespace Fanex.Module.NG2.Services
{
    public class SyncCustomerService : ISyncCustomerService
    {
        private ISyncCustomerRepository _repo = new SyncCustomerRepository();

        /// <summary>
        /// For mail station call first to check sync status of NG2.
        /// </summary>
        /// <returns>SyncCustomerResult object. 
        /// If SyncCustomerResult.IsSuccess = false then export excel 2 customer list to send mail to IT 
        /// and call UpdateNotifiedEmail(SyncCustomerResult.LogIds) to update notified in DB.</returns>
        public SyncCustomerResult CheckSyncCustomers()
        {
            var result = new SyncCustomerResult();
            var cache = CacheService.CreateInstance(CacheType.MemoryCache);
            var preResult = cache.Get<SyncCustomerResult>("SyncCustomerResult_" + DateTime.Now.ToString("yyyyMMdd")) ?? new SyncCustomerResult();
            try
            {
                // Gets old list
                result.NotSyncedInMbcCustomers = preResult.NotSyncedInMbcCustomers;
                result.NotSyncedIn3rdCustomers = preResult.NotSyncedIn3rdCustomers;

                var logs = _repo.Logs(ConfigurationManager.AppSettings["IsTest"] == "1");
                if (logs != null && logs.Count > 0)
                {
                    var custIds = logs.Select(l => l.CustId);
                    var ids = string.Empty;
                    var index = 0;
                    var failedCustIds = new List<int>();

                    IThirdPartyServices service = new ONEbook.NG2.API.Services();
                    IEnumerable<Customer> customers = new List<Customer>();
                    Response response = new Response();
                    Request request = new Request();
                    request.Auth.SiteId = 2000;

                    for (int i = 0; i < custIds.Count(); i++)
                    {
                        ++index;
                        ids += custIds.ElementAt(i) + ",";
                        request.Call.AccountArray.Add(new AccountInfo { ObCustId = custIds.ElementAt(i) });

                        if (i == custIds.Count() - 1 || index == 500)
                        {
                            customers = _repo.Customers(ids.Substring(0, ids.Length - 1));
                            response = service.CheckIsSyncCustomers(request);

                            if (response.Result.IsSuccess && response.Result.AccountArray.Count > 0)
                            {
                                foreach (var customer in response.Result.AccountArray)
                                {
                                    customers.Where(c => c.CustId == customer.ObCustId).FirstOrDefault().IsSyncIn3rd = customer.IsCreated == 1;
                                }
                            }
                            else
                            {
                                foreach (var custId in request.Call.AccountArray)
                                {
                                    failedCustIds.Add(custId.ObCustId);
                                }                                
                            }

                            var notSyncedInMbc = customers.Where(c => !c.IsSyncInMBC && c.IsSyncIn3rd);
                            var notSyncedIn3rd = customers.Where(c => c.IsSyncInMBC && !c.IsSyncIn3rd);

                            result.NotSyncedInMbcCustomers = result.NotSyncedInMbcCustomers.Concat(notSyncedInMbc);
                            result.NotSyncedIn3rdCustomers = result.NotSyncedIn3rdCustomers.Concat(notSyncedIn3rd);

                            // Reset indexes to perform next batch of 500 customers
                            ids = string.Empty;
                            index = 0;
                            request = new Request();
                            request.Auth.SiteId = 2000;
                        }
                    }
                    
                    var logIds = logs.Where(l => !failedCustIds.Contains(l.CustId)).Select(l => l.LogId);
                    var idsLog = string.Join(",", logIds);

                    result.ErrorCode = result.NotSyncedInMbcCustomers.Count() > 0 || result.NotSyncedIn3rdCustomers.Count() > 0 ? 1 : 0;
                    result.LogIds = idsLog;

                    // Cache result
                    cache.Set("SyncCustomerResult_" + DateTime.Now.ToString("yyyyMMdd"), result, DateTime.Now.AddHours(26));
                }
            }
            catch (Exception ex)
            {
                Logging.Logger.Log.Error(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        /// Sends mail successfully then call this function to update notified in DB.
        /// </summary>
        /// <param name="logIds">Log ids get from result of CheckSyncCustomers function.</param>
        /// <returns>Boolean object. true is succeeded, otherwise is false.</returns>
        public bool UpdateNotifiedEmail(string logIds)
        {
            var ids = logIds.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            var index = 0;
            var idsLog = string.Empty;
            var result = false;
            try
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    ++index;
                    idsLog += ids.ElementAt(i) + ",";

                    if (i == ids.Length - 1 || index == 200)
                    {
                        result = _repo.UpdateIsNotified(idsLog.Substring(0, idsLog.Length - 1));

                        index = 0;
                        idsLog = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                Logging.Logger.Log.Error(ex.Message, ex);
            }            

            return result;
        }
    }
}