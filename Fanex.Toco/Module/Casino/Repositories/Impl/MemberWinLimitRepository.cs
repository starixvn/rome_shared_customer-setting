namespace Fanex.CustomerSetting.Module.Casino.Repositories.Impl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using Fanex.CustomerSetting.Module.Casino.Entities;
    using Fanex.CustomerSetting.Module.Casino.Repositories.Interface;
    using Fanex.CustomerSetting.Module.Casino.Resources;
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.Data;
    using Fanex.Module.Logs.Services;

    public class MemberWinLimitRepository : IMemberWinLimitRepository
    {
        public MemberWinLimit GetMemberWinLimit(int custId)
        {
            using (var db = new ObjectDb("Acc_CustomerWinLimitSelEdit"))
            {
                var winLimit = new MemberWinLimit();

                var param = new Hashtable();
                param.Add("custid", custId);
                param.Add("sporttype", 208);
                param.Add("bettype", 2101);

                db.AssignParamValues(param);

                try
                {
                    using (var reader = db.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                winLimit.IsDailyAutoResetUpline = reader.ToBoolean("UplineIsAutoReset");
                                winLimit.IsDailyAutoReset = reader.ToBoolean("IsAutoReset");
                                winLimit.MinimumLoss = reader.ToDecimal("MinLossLimit");

                                winLimit.MaximumLoss = reader.ToDecimal("MaxLossLimit");
                                winLimit.MaximumLossLimit = reader.ToDecimal("UplineMaxLossLimit");
                                winLimit.MaximumWinLimits = new List<KeyValuePair<string, int>>();

                                var maxWin = reader.ToInt32("MaxWinLimit");
                                var maxWinLimit = reader.ToInt32("UplineMaxWinLimit");

                                winLimit = MapMemberWinLimits(winLimit, maxWin, maxWinLimit);

                                break;
                            }
                        }
                    }

                    return winLimit;
                }
                catch (Exception ex)
                {
                    ex.Write();

                    throw ex;
                }                
            }
        }

        public CurrentMemberWinLimit GetCurrentWinLimit(int custId)
        {
            var winLimit = new CurrentMemberWinLimit();

            using (var db = new ObjectDb("Acc_CustomerWinLimitSel"))
            {
                var param = new Hashtable();
                param.Add("custid", custId);
                param.Add("sporttype", 208);
                param.Add("bettype", 2101);

                db.AssignParamValues(param);

                try
                {
                    using (var reader = db.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                winLimit.Balance = reader.ToDecimal("Balance");
                                winLimit.MaxWinCredit = reader.ToDecimal("MaxWinCredit");
                                winLimit.MaxWinLimit = reader.ToInt32("MaxWinLimit");
                                winLimit.WinPercent = reader.ToDecimal("WinPercent");

                                break;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ex.Write();

                    throw ex;
                }
            }

            return winLimit;
        }

        public bool UpdateMemberWinLimit(int custId, int userId, int subAccId, MemberWinLimit memberWinLimit)
        {
            using (var db = new ObjectDb("Acc_CustomerWinlimitUpd"))
            {
                var param = new Hashtable();
                param.Add("custid", custId);
                param.Add("maxwinlimit", memberWinLimit.MaximumWin.Value);
                param.Add("isautoreset", memberWinLimit.IsDailyAutoReset);
                param.Add("sporttype", 208);
                param.Add("bettype", 2101);
                param.Add("maxlosslimit", memberWinLimit.MaximumLoss);
                param.Add("subaccid", subAccId);
                param.Add("ip", AppSession.CurrentContext.Ip4Address);
                param.Add("userid", userId);
                param.Add("language", CultureInfo.CurrentCulture.Name);
                param.Add("errcode", 0);
                param.Add("errmsg", string.Empty);

                db.AssignParamValues(param);

                try
                {
                    db.ExecuteNonQuery();

                    return true;
                }
                catch (Exception ex)
                {
                    ex.Write();

                    throw ex;
                }
            }            
        }

        private MemberWinLimit MapMemberWinLimits(MemberWinLimit winLimit, int maxWinLimit, int uplineMaxWinLimit)
        {
            var maxWinKey = string.Empty;
            winLimit.MaximumWinLimits = new List<KeyValuePair<string, int>>();

            switch (uplineMaxWinLimit)
            {
                case 0:
                    winLimit.MaximumWinLimits.Add(new KeyValuePair<string, int>(NGCasinoLang.lblUnlimited, 0));
                    winLimit.MaximumWinLimits.Add(new KeyValuePair<string, int>("10X", 10));
                    winLimit.MaximumWinLimits.Add(new KeyValuePair<string, int>("5X", 5));
                    winLimit.MaximumWinLimits.Add(new KeyValuePair<string, int>("3X", 3));

                    break;
                case 10:
                    winLimit.MaximumWinLimits.Add(new KeyValuePair<string, int>("10X", 10));
                    winLimit.MaximumWinLimits.Add(new KeyValuePair<string, int>("5X", 5));
                    winLimit.MaximumWinLimits.Add(new KeyValuePair<string, int>("3X", 3));

                    break;

                case 5:
                    winLimit.MaximumWinLimits.Add(new KeyValuePair<string, int>("5X", 5));
                    winLimit.MaximumWinLimits.Add(new KeyValuePair<string, int>("3X", 3));

                    break;
                default:                    
                    winLimit.MaximumWinLimits.Add(new KeyValuePair<string, int>("3X", 3));

                    break;
            }

            maxWinKey = maxWinLimit == 0 ? NGCasinoLang.lblUnlimited : maxWinLimit + "X";

            winLimit.MaximumWin = new KeyValuePair<string, int>(maxWinKey, maxWinLimit);

            return winLimit;
        }
    }
}