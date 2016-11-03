using System.Collections.Generic;

namespace Fanex.Module.NG2.Entities
{
    public class SyncCustomerResult : Result
    {
        public SyncCustomerResult()
        {
            NotSyncedInMbcCustomers = new List<Customer>();
            NotSyncedIn3rdCustomers = new List<Customer>();
        }

        public IEnumerable<Customer> NotSyncedInMbcCustomers
        {
            get; set;
        }

        public IEnumerable<Customer> NotSyncedIn3rdCustomers
        {
            get; set;
        }

        public string LogIds
        {
            get; set;
        }
    }
}