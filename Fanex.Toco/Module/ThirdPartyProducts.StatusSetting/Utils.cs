using RemotingRef;
using System;
using System.Threading;

namespace ThirdPartyProducts.StatusSetting
{
    internal static class Utils
    {
        internal static bool ToBoolean(object value)
        {
            var val = false;

            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
            {
                return false;
            }

            if (value.ToString() == "1")
            {
                return true;
            }

            bool.TryParse(value.ToString(), out val);
            
            return val;
        }
        
        internal static int ToInt32(object value)
        {
            var val = 0;

            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
            {
                return 0;
            }

            if (value is decimal || value is double)
            {
                val = Convert.ToInt32(value);
            }
            else
            {
                int.TryParse(value.ToString(), out val);
            }

            return val;
        }
        
        internal static string GetLanguageKeyForSql()
        {
            var langCode = Thread.CurrentThread.CurrentCulture.Name;
            switch (langCode)
            {
                case "ja-JP": return "J";
                case "zh-TW": return "CT";
                case "zh-CN": return "CS";
                case "th-TH": return "TH";
                default: return "E";
            }
        }

        internal static string GetLanguageKeyForRemotingRef()
        {
            var langCode = Thread.CurrentThread.CurrentCulture.Name;
            switch (langCode)
            {
                case "zh-TW": return "ch"; // Traditional
                case "zh-CN": return "cs"; // Simplified
                case "ja-JP": return "jp";
                case "ko-KR": return "ko";
                case "vi-VN": return "vn";
                case "th-TH": return "th"; // Thai
                default: return "en";
            }
        }

        internal static string GetProductName(string productId, string language)
        {
            return RefData.GetResourceDataByRefIdAndResourceId("Product", productId, language, string.Empty);
        }

        internal static string GetSportName(string sportType, string language)
        {
            var sportName = Resources.SportName.ResourceManager.GetString("Sport_" + sportType);
            if (string.IsNullOrEmpty(sportName))
            {
                sportName = RefData.GetSportName(sportType, language, string.Empty);
            }

            return sportName;
        }

        internal static string GetBetTypeName(string betType, string language)
        {
            var betTypeName = Resources.BetTypeName.ResourceManager.GetString("BetType_" + betType);
            if (string.IsNullOrEmpty(betTypeName))
            {
                betTypeName = RefData.GetBetTypeName(betType, language, string.Empty);
            }

            return betTypeName;
        }
    }
}
