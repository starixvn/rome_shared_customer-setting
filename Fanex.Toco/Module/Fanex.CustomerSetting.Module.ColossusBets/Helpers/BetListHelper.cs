namespace Fanex.CustomerSetting.Module.ColossusBets.Helpers
{
    using System;
    using Fanex.CustomerSetting.Module.ColossusBets.Resources;

    public class BetListHelper
    {
        public static string PercentFormat(decimal input)
        {
            var isInputInterger = (input * 100) % 1 == 0;
            return isInputInterger ?
                Math.Round(input * 100, 0).ToString() + Labels.Symbol_Percent :
                Math.Round(input * 100, 2).ToString() + Labels.Symbol_Percent;
        }
    }
}
