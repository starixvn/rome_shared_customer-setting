namespace Fanex.CustomerSetting.Module.Casino.Entities
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    public class MemberWinLimit
    {
        public List<KeyValuePair<string, int>> MaximumWinLimits { get; set; }

        public decimal CurrentWinLimit { get; set; }

       // [JsonConverter(typeof(JsonConverterExt))]
        public KeyValuePair<string, int> MaximumWin { get; set; }

        public decimal MinimumLoss { get; set; }

        public decimal MaximumLoss { get; set; }

        public decimal MaximumLossLimit { get; set; }

        public bool IsDailyAutoReset { get; set; }

        public bool IsDailyAutoResetUpline { get; set; }
    }
}