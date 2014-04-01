// -----------------------------------------------------------------------
// <copyright file="StatusRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  Enable Status.
// </summary>
namespace Fanex.CustomerSetting.Module.KenoStatus.Repositories.Impl
{
    using System;
    using System.Collections;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using Fanex.Common;
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.CustomerSetting.Module.KenoStatus.Criteria;
    using Fanex.CustomerSetting.Module.KenoStatus.Repositories.Interface;
    using Fanex.CustomerSetting.Module.KenoStatus.ViewModels;
    using Fanex.Data;

    /// <summary>
    /// Progressive repository.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
    public class StatusRepository : IStatusRepository
    {
        /// <summary>
        /// Get Enable Status.
        /// </summary>
        /// <param name="criteria">Parameters of stored procedure.</param>
        /// <returns>Keno Status object.</returns>
        [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1305:FieldNamesMustNotUseHungarianNotation", Justification = "Reviewed.")]
        public KenoEnableStatus GetStatus(SelectStatusCriteria criteria)
        {
            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                var kenoEnableStatus = new KenoEnableStatus();
                DataSet ds = db.ExecuteDataSet(criteria.GetSpParams());
                if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables.Count > 0)
                    {
                        var dt = ds.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {
                            kenoEnableStatus.BetTypeStatusList.Add(new BetTypeStatus()
                            {
                                BetTypeID = Converter.ToInt(row["IBCBetTypeID"]),
                                BetTypeName = RemotingRef.RefData.GetBetTypeName(Utils.ToString(row["IBCBetTypeID"]), TWLang(AppSession.Language)),
                                IsDisabled = row["IsDisabled"] == DBNull.Value ? true : Utils.ToBoolean(row["IsDisabled"]),
                                IsDisabledUpline = row["DisableUplines"] == DBNull.Value ? true : Utils.ToBoolean(row["DisableUplines"]),
                                IsDisabledDownlines = row["DisableDownlines"] == DBNull.Value ? true : Utils.ToBoolean(row["DisableDownlines"])
                            });
                        }

                        kenoEnableStatus.UserName = dt.Rows.Count > 0 ? Convert.ToString(dt.Rows[0]["UserName"]) : string.Empty;
                        kenoEnableStatus.HasDownline = Utils.ToBoolean(db.GetOuputParams()["hasdownline"]);

                        return kenoEnableStatus;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Update Progressive Bonus.
        /// </summary>
        /// <param name="criteria">Parameter of stored procedure.</param>
        /// <returns>Result code of DB.</returns>
        public Hashtable UpdateStatus(UpdateStatusCriteria criteria)
        {
            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(criteria.GetSpParams());
                return db.GetOuputParams();
            }
        }

        /// <summary>
        /// Convert the language key.
        /// </summary>
        /// <param name="lang">The language.</param>
        /// <returns>Short key.</returns>
        private static string TWLang(string lang)
        {
            switch (lang)
            {
                case "en-US": return "en";
                case "zh-TW": return "ch"; // Traditional
                case "zh-CN": return "cs"; // Simplified
                case "ja-JP": return "jp";
                case "th-TH": return "th"; // Thai
                default: return "en";
            }
        }
    }
}