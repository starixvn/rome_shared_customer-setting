using System;
using System.Collections;
using System.Data;
using Fanex.Products.Enums;
using Fanex.Products.Extensions;

namespace Fanex.Products.Services
{
    public class Product_23 : BaseProduct, IProduct
    {
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

        public override DCSIcon GetDCSIcon(DataRow drow)
        {
            if (ShowOnWeb && Synced == true)
            {
                string lowerName = Name.ToLowerInvariant();

                string syncedColumnName = "synced" + lowerName;
                object synced = drow.GetValue(syncedColumnName);

                if (synced == null || !Convert.ToBoolean(synced))
                {
                    return DCSIcon.Synchronized;
                }

                string disabledColumnName = "disable" + lowerName;
                object disabled = drow.GetValue(disabledColumnName);

                if (disabled != null && !Convert.ToBoolean(disabled))
                {
                    return DCSIcon.EnabledEdit;
                }
            }

            return DCSIcon.DisabledEdit;
        }

        public override bool ShowDCSLinkForDownline(DataRow drow, int loggedInUserRole)
        {
            if (ShowOnWeb && Synced == true)
            {
                int roleId = Convert.ToInt32(drow["roleid"]);

                // Direct downline or Member.
                return (roleId + 1 == loggedInUserRole) || roleId == 1;
            }

            return false;
        }

        public override bool ShowEnableStatusLinkForDownline(DataRow drow, int loggedInUserRole)
        {
            if (ShowOnWeb && Synced == true)
            {
                object synced = drow.GetValue("IsSync");

                return synced != null && Convert.ToBoolean(synced);
            }

            return false;
        }
    }
}
