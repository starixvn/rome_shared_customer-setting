namespace Fanex.CustomerSetting.Module.NG2.Core.Models
{
    using Fanex.CustomerSetting.Module.NG2.Core.Services.Impl;

    public class MatchInfo
    {
        public int MatchId { get; set; }

        public string MatchCode { get; set; }

        public string EventDate { get; set; }

        public int HomeId { get; set; }

        public int AwayId { get; set; }

        public string HomeName
        {
            get
            {
                return CacheRef.GetTeamName(HomeId) ?? string.Empty;
            }
        }

        public string AwayName
        {
            get
            {
                return CacheRef.GetTeamName(AwayId) ?? string.Empty;
            }
        }

    }
}
