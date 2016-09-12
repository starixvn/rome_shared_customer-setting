using System;
using System.Collections;
using Fanex.Products.Utilities;

namespace Fanex.Products.Services
{
    public class Product_23 : BaseProduct, IProduct
    {
        protected const string SyncedProduct = "_SYNCEDPRODUCT_";

        public Product_23(bool webConfigActive)
            : base(webConfigActive)
        {
        }

        public override bool? Synced
        {
            get
            {
                return GetSynced();
            }
        }

        public override void SetProductStatusToSession(Hashtable outputParams)
        {
            base.SetProductStatusToSession(outputParams);

            string lowerName = Name.ToLowerInvariant();
            string storedProcedureName = "synced" + lowerName;

            if (outputParams.ContainsKey(storedProcedureName))
            {
                SetSynced(Convert.ToBoolean(outputParams[storedProcedureName]));
            }
        }

        protected bool? GetSynced()
        {
            return SessionUtility.GetSession(SyncedProduct + Id, new bool?());
        }

        protected void SetSynced(bool? value)
        {
            SessionUtility.SetSession(SyncedProduct + Id, value);
        }
    }
}
