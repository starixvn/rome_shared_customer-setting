namespace Fanex.CustomerSetting.Module.ColossusBets.Models
{
    public class ViewTokenResultModel
    {
        public int ErrorCode { get; set; }

        public string ErrorMsg { get; set; }

        public TokenDetailModel Data { get; set; }
    }
}
