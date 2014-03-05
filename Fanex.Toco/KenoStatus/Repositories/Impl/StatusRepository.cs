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
    using Fanex.Data;
    using Fanex.CustomerSetting.Module.KenoStatus.Repositories.Interface;
    using Fanex.CustomerSetting.Module.KenoStatus.ViewModels;
    using Fanex.CustomerSetting.Module.KenoStatus.Criteria;

    /// <summary>
    /// Progressive repository.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
    public class ProgressiveRepository : IStatusRepository
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
					if ( ds.Tables.Count>0)
					{
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            kenoEnableStatus.BetTypeStatusList.Add(new BetTypeStatus(){
                                BetTypeID=  Converter.ToInt(row["BettypeID"]),
                                IsDisabled = row["Disable"] == DBNull.Value ? true : Converter.ToBool(row["Disable"]),
                                IsDisabledUpline = row["DisabledUpline"] == DBNull.Value ? true : Converter.ToBool(row["DisabledUpline"]),
                                IsDisabledDownlines = row["DisabledDownlines"] == DBNull.Value ? true : Converter.ToBool(row["DisabledDownlines"])
                            });
                        }
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
	}
}