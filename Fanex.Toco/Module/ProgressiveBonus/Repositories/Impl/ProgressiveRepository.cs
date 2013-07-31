// -----------------------------------------------------------------------
// <copyright file="ProgressiveRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Impl
{
	using System.Collections;
	using System.Data;
	using Fanex.Common;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Configurations;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Criteria;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Interface;
	using Fanex.Data;
	using Fanex.NAP.Framework.Data;
	using System;

	public class ProgressiveRepository : IProgressiveRepository
	{
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