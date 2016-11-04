using System.Collections.Generic;
using Fanex.Module.NG2.Entities;

namespace Fanex.Module.NG2.Repository
{
    public interface ISyncCustomerRepository
    {
        IList<LogInfo> Logs(bool isTest = true);

        IList<Customer> Customers(string custIds);

        bool UpdateIsNotified(string logIds);
    }
}