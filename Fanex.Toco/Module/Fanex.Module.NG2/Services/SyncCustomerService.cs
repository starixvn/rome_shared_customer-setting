using Fanex.Caching;
using Fanex.Caching.Memory;
using Fanex.Logging.Extensions.Web;
using Fanex.Module.NG2.Entities;
using Fanex.Module.NG2.Repository;
using ONEbook.NG2.API.DTO.CheckIsSyncCustomer;
using ONEbook.NG2.API.Interface;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace Fanex.Module.NG2.Services
{
    public class SyncCustomerService : ISyncCustomerService
    {
        private readonly ISyncCustomerRepository _repo = new SyncCustomerRepository();

        /// <summary>
        /// For mail station call first to check sync status of NG2.
        /// </summary>
        /// <returns>SyncCustomerResult object. 
        /// If SyncCustomerResult.IsSuccess = false then export excel 2 customer list to send mail to IT 
        /// and call UpdateNotifiedEmail(SyncCustomerResult.LogIds) to update notified in DB.</returns>
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public SyncCustomerResult CheckSyncCustomers()
        {
            var result = new SyncCustomerResult();
            var cache = new CacheService(new MemoryCache(new MemoryCacheOptions()));
            var preResult = cache.Get<SyncCustomerResult>("SyncCustomerResult_" + DateTime.Now.ToString("yyyyMMdd")) ?? new SyncCustomerResult();
            var oldCustIds = preResult.NotSyncedIn3rdCustomers.Select(l => l.CustId)
                .Concat(preResult.NotSyncedInMbcCustomers.Select(l => l.CustId));

            var logs = _repo.Logs(ConfigurationManager.AppSettings["IsTest"] == "1");
            if (logs.Any() || oldCustIds.Any())
            {
                var custIds = logs.Select(l => l.CustId).Concat(oldCustIds);
                var ids = new StringBuilder();
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
                    ids.Append(custIds.ElementAt(i) + ",");
                    request.Call.AccountArray.Add(new AccountInfo { ObCustId = custIds.ElementAt(i) });

                    if (i == custIds.Count() - 1 || index == 500)
                    {
                        customers = _repo.Customers(ids.ToString().Substring(0, ids.ToString().Length - 1));
                        try
                        {
                            response = service.CheckIsSyncCustomers(request);
                        }
                        catch (Exception ex)
                        {
                            Logging.Logger.Log.Error(ex.Message, ex);
                        }

                        if (response.Result.AccountArray.Count > 0)
                        {
                            // This case is success calling API and got response from 3rd.
                            foreach (var customer in response.Result.AccountArray)
                            {
                                // Mark IsSyncIn3rd = actual status at 3rd.
                                customers.FirstOrDefault(c => c.CustId == customer.ObCustId).IsSyncIn3rd = customer.IsCreated == 1;
                            }
                        }
                        else
                        {
                            // This case is failed coz of failed calling API due to network or unexpected error. Then don't mark them as notified flag in DB.
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
                        ids = new StringBuilder();
                        index = 0;
                        request = new Request();
                        request.Auth.SiteId = 2000;
                    }
                }

                // Get all log id that successfully checked at 3rd.
                var logIds = logs.Where(log => !failedCustIds.Contains(log.CustId)).Select(l => l.LogId);
                var idsLog = string.Join(",", logIds);

                result.ErrorCode = result.NotSyncedInMbcCustomers.Any() || result.NotSyncedIn3rdCustomers.Any() ? 1 : 0;
                result.LogIds = idsLog;

                // Cache result
                cache.Set("SyncCustomerResult_" + DateTime.Now.ToString("yyyyMMdd"), result, new CacheItemOptions().SetAbsoluteExpiration(DateTime.Now.AddHours(26)));
            }
            else
            {
                result.ErrorCode = 0;
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
            var idsLog = new StringBuilder();
            var result = false;
            try
            {
                for (int i = 0; i < ids.Length; i++)
                {
                    ++index;
                    idsLog.Append(ids.ElementAt(i) + ",");

                    if (i == ids.Length - 1 || index == 200)
                    {
                        result = _repo.UpdateIsNotified(idsLog.ToString().Substring(0, idsLog.ToString().Length - 1));

                        index = 0;
                        idsLog = new StringBuilder();
                    }
                }
            }
            catch (Exception ex)
            {
                ExceptionHandler.Handle(ex);
            }            

            return result;
        }
    }
}