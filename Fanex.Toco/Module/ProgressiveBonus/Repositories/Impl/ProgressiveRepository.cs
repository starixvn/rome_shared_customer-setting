// -----------------------------------------------------------------------
// <copyright file="ProgressiveRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  ProgressiveRepository
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Impl
{
	using System;
	using System.Collections;
	using System.Data;
	using Fanex.Common;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Configurations;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Criteria;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Interface;
	using Fanex.Data;
	using Fanex.NAP.Framework.Data;

	/// <summary>
	/// ProgressiveRepository
	/// </summary>
	public class ProgressiveRepository : IProgressiveRepository
	{
		/// <summary>
		/// Get Progressive Bonus
		/// </summary>
		/// <param name="criteria">Parameters of stored procedure</param>
		/// <returns>Progressive Status object</returns>
        public ProgressiveStatus GetStatusProgressiveBonus(GetProductCriteria criteria)
		{
			using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
			{
				DataSet ds = db.ExecuteDataSet(criteria.GetSpParams());
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					FromSystem fromSystem = (FromSystem)ProgressiveSection.Instance.System;
                    DataTable dtTable = ds.Tables[0];							
					if (dtTable.Rows.Count > 0)
					{
						DataRow drow = dtTable.Rows[0];
                        return new ProgressiveStatus
						{
                            CustId = criteria.CustId,
							Username = Converter.ToString2(drow["Username"]),
							RoleId = Converter.ToInt(drow["RoleId"]),
							DisableCasinoBonus = drow["DisableCasinoBonus"] == DBNull.Value ? true : Converter.ToBool(drow["DisableCasinoBonus"]),
							DisableDownlinesCasinoBonus = drow["DisableDownlinesCasinoBonus"] == DBNull.Value ? true : Converter.ToBool(drow["DisableDownlinesCasinoBonus"]),
							DisableUplinesCasinoBonus = drow["DisableUplinesCasinoBonus"] == DBNull.Value ? true : Converter.ToBool(drow["DisableUplinesCasinoBonus"]),
						};
					}
				}
			}

			return null;
		}

		/// <summary>
		/// Update Progressive Bonus
		/// </summary>
		/// <param name="criteria">Parameter of stored procedure</param>
		/// <returns>Result code of DB</returns>
		public Hashtable UpdateStatusProgressiveBonus(ICriteria criteria)
		{
			using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
			{
				db.ExecuteNonQuery(criteria.GetSpParams());
				return db.GetOuputParams();
			}
		}
	}
}