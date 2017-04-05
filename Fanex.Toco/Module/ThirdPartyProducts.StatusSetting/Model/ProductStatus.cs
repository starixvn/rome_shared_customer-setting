using RemotingRef;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThirdPartyProducts.StatusSetting.Model
{
    public class ProductStatus
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int SportType { get; set; }

        public string SportName { get; set; }

        public int BetType { get; set; }

        public string BetTypeName { get; set; }

        public bool IsSync { get; set; }

        public bool IsHidden { get; set; }

        public bool IsHiddenUpline { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsDisabledDownline { get; set; }

        public bool IsSyncedDownline { get; set; }

        public bool IsDisabledUpline { get; set; }

        public bool IsSyncedUpline { get; set; }

        public bool HasDownline { get; set; }

        internal void GetNameFromRemotingRef()
        {
            var language = Utils.GetLanguageKeyForRemotingRef();
            this.GetProductName(language);
            this.GetSportName(language);
            this.GetBetTypeName(language);
        }

        private void GetProductName(string language)
        {
            this.ProductName = RefData.GetResourceDataByRefIdAndResourceId("Product", this.ProductId.ToString(), language, string.Empty);
        }

        private void GetSportName(string language)
        {
            this.SportName = Resources.SportName.ResourceManager.GetString("Sport_" + this.SportType);
            if (string.IsNullOrEmpty(this.SportName))
            {
                this.SportName = RefData.GetSportName(this.SportType.ToString(), language, string.Empty);
            }
        }

        private void GetBetTypeName(string language)
        {
            this.BetTypeName = Resources.BetTypeName.ResourceManager.GetString("BetType_" + this.BetType);
            if (string.IsNullOrEmpty(this.BetTypeName))
            {
                this.BetTypeName = RefData.GetBetTypeName(this.BetType.ToString(), language, string.Empty);
            }
        }
    }
}
