namespace ThirdPartyProducts.StatusSetting.Model
{
    public class GetProductStatusRequest
    {
        public int CustId { get; set; }

        public int ProductId { get; set; }

        public int RoleId { get; set; }

        public bool IsDeposit { get; set; }
    }
}
