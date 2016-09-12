using System;
using System.Collections;
using Fanex.Products.Utilities;

namespace Fanex.Products.Services
{
    public class BaseProduct : IProduct
    {
        protected const string HideProduct = "_HIDEPRODUCT_";
        protected const string DisabledProduct = "_DISABLEDPRODUCT_";

        private bool _active;

        public BaseProduct(bool webConfigActive)
        {
            _active = webConfigActive;
        }

        #region Properties

        public int Id { get; set; }

        public string Name { get; set; }

        public string RemotingRefName { get; set; }

        public int SportId { get; set; }

        public string ReportSPParam { get; set; }

        public string ViewPTLink { get; set; }

        public string ViewCommLink { get; set; }

        public string SettingPTLink { get; set; }

        #endregion

        #region Virtual and Override Methods

        public virtual bool Active
        {
            get
            {
                return _active && !GetHide();
            }
        }

        public virtual bool Disabled
        {
            get
            {
                return GetDisabled();
            }
        }

        public virtual bool? Synced
        {
            get
            {
                return new bool?();
            }
        }

        public virtual bool ShowReport
        {
            get
            {
                return _active && !GetHide();
            }
        }

        public virtual bool ShowDCS
        {
            get
            {
                return _active && !GetHide();
            }
        }

        public virtual void SetProductStatusToSession(Hashtable outputParams)
        {
            string lowerName = Name.ToLowerInvariant();
            string storedProcedureName = "hide" + lowerName;

            if (outputParams.ContainsKey(storedProcedureName))
            {
                SetHide(Convert.ToBoolean(outputParams[storedProcedureName]));
            }

            storedProcedureName = "disable" + lowerName;

            if (outputParams.ContainsKey(storedProcedureName))
            {
                SetDisabled(Convert.ToBoolean(outputParams[storedProcedureName]));
            }
        }

        #endregion

        #region Session Methods

        protected bool GetHide()
        {
            return SessionUtility.GetSession(HideProduct + Id, true);
        }

        protected void SetHide(bool value)
        {
            SessionUtility.SetSession(HideProduct + Id, value);
        }

        protected bool GetDisabled()
        {
            return SessionUtility.GetSession(DisabledProduct + Id, true);
        }

        protected void SetDisabled(bool value)
        {
            SessionUtility.SetSession(DisabledProduct + Id, value);
        }

        #endregion
    }
}