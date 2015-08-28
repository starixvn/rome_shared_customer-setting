namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Entities
{
    /// <summary>
    /// The BetSetting entity.
    /// </summary>
    public class SettingContext
    {
        public int CustId { get; set; }

        public int RoleId { get; set; }

        public int Srecommend { get; set; }

        public int Mrecommend { get; set; }

        public int Recommend { get; set; }

        public int ActorId { get; set; }

        public int SubAccId { get; set; }

        public string Language { get; set; }

        public string IP { get; set; }
    }
}