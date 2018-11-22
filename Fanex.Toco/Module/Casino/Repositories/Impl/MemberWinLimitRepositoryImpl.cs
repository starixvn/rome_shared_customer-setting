namespace Fanex.CustomerSetting.Module.Casino.Repositories.Impl
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Core;
    using Core.Models;
    using Data;
    using Data.Repository;
    using Entities;
    using Interface;
    using Logging.Extensions.Web;
    using Resources;

    public class MemberWinLimitRepositoryImpl : MemberWinLimitRepository
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
                        if (reader != null && reader.Read())
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
                        }
                    }

                    return winLimit;
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                    throw;
                }
            }
        }

        public CurrentMemberWinLoss GetCurrentWinLimit(int custId)
        {
            var param = new Hashtable
            {
                { "custid", custId },
                { "sporttype", 208 },
                { "bettype", 2101 }
            };

            using (var db = new ObjectDb("Acc_CustomerWinLimitSel"))
            {
                db.AssignParamValues(param);

                using (var reader = db.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        decimal winPercent = reader.ToDecimal("WinPercent");
                        decimal maxWinCredit = reader.ToDecimal("MaxWinCredit");
                        decimal currentAmount = reader.ToDecimal("CurrentAmount");
                        decimal lossPercent = reader.ToDecimal("LossPercent");
                        decimal? maxLossAmount = reader["MaxLossAmount"] == DBNull.Value ? new decimal?() : reader.ToDecimal("MaxLossAmount");

                        return new CurrentMemberWinLoss(winPercent, currentAmount, maxWinCredit, lossPercent, maxLossAmount);
                    }
                }
            }

            return new CurrentMemberWinLoss();
        }

        public Result ExecuteNonQuery(ICriteria criteria)
        {
            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());

                return new Result(Utils.ToInt32(db.GetParameterValue("errcode")), Utils.ToString(db.GetParameterValue("errmsg")));
            }
        }

        public Result UpdateMemberWinLimit(
            int custId,
            int userId,
            int subAccId,
            string ip,
            string language,
            MemberWinLimit memberWinLimit)
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
                param.Add("ip", ip);
                param.Add("userid", userId);
                param.Add("language", language);
                param.Add("errcode", 0);
                param.Add("errmsg", string.Empty);

                db.AssignParamValues(param);

                try
                {
                    db.ExecuteNonQuery();

                    return new Result(Utils.ToInt32(db.GetParameterValue("errcode")), Utils.ToString(db.GetParameterValue("errmsg")));
                }
                catch (Exception ex)
                {
                    ExceptionHandler.Handle(ex);
                    throw;
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