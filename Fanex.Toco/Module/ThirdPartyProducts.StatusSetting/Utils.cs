using RemotingRef;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;

namespace ThirdPartyProducts.StatusSetting
{
    internal static class Utils
    {
        internal static byte ToByte(object value)
        {
            byte val = 0;

            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
            {
                return val;
            }

            byte.TryParse(value.ToString(), out val);

            return val;
        }

        internal static short ToInt16(object value)
        {
            short val = 0;

            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
            {
                return val;
            }

            short.TryParse(value.ToString(), out val);

            return val;
        }

        internal static decimal ToDecimal(object value)
        {
            var val = 0.0m;

            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
            {
                return 0.0m;
            }

            decimal.TryParse(value.ToString(), out val);

            return val;
        }

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

        internal static string ToString(object value)
        {
            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
            {
                return string.Empty;
            }

            return Convert.ToString(value);
        }

        internal static double ToDouble(object value)
        {
            var val = 0.0d;

            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
            {
                return 0.0d;
            }

            double.TryParse(value.ToString(), out val);

            return val;
        }

        internal static DateTime ToDateTime(object value)
        {
            var val = DateTime.MinValue;

            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
            {
                return DateTime.MinValue;
            }

            DateTime.TryParse(value.ToString(), out val);

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

        internal static long ToInt64(object value)
        {
            var val = 0L;

            if (value == null || value == DBNull.Value || value.ToString() == string.Empty)
            {
                return 0L;
            }

            if (value is decimal || value is double)
            {
                val = Convert.ToInt64(value);
            }
            else
            {
                long.TryParse(value.ToString(), out val);
            }

            return val;
        }

        internal static string GetLanguageKeyForSQL()
        {
            var langCode = Thread.CurrentThread.CurrentCulture.Name;
            switch (langCode)
            {
                case "en-US": return "E";
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
                case "en-US": return "en";
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
                sportName = RefData.GetSportName(sportType.ToString(), language, string.Empty);
            }

            return sportName;
        }

        internal static string GetBetTypeName(string betType, string language)
        {
            var betTypeName = Resources.BetTypeName.ResourceManager.GetString("BetType_" + betType);
            if (string.IsNullOrEmpty(betTypeName))
            {
                betTypeName = RefData.GetBetTypeName(betType.ToString(), language, string.Empty);
            }

            return betTypeName;
        }
    }
}
