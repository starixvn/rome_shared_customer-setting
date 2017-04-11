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
            this.ProductName = Utils.GetProductName(this.ProductId.ToString(), language);
            this.SportName = Utils.GetSportName(this.SportType.ToString(), language);
            this.BetTypeName = Utils.GetBetTypeName(this.BetType.ToString(), language);
        }
    }
}
