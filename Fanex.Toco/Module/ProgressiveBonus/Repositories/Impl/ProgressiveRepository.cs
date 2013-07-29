// -----------------------------------------------------------------------
// <copyright file="ProgressiveRepository.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Impl
{
	using System.Collections;
	using System.Data;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Configurations;
	using Fanex.CustomerSetting.Module.ProgressiveModule.Repositories.Interface;
	using Fanex.Data;
	using Fanex.NAP.Framework.Data;
    using Fanex.CustomerSetting.Module.ProgressiveModule.Entities;
    using System;

	public class ProgressiveRepository : IProgressiveRepository
	{
        public ProgressiveStatus GetStatusOfProgressiveBonus(ICriteria criteria)
		{
			using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
			{
				DataSet ds = db.ExecuteDataSet(criteria.GetSpParams());
				if (ds != null && ds.Tables != null && ds.Tables.Count > 0)
				{
					FromSystem fromSystem = (FromSystem)ProgressiveSection.Instance.System;
					DataTable dtTable = fromSystem == FromSystem.Accounting // Acc
							? ds.Tables[0]
							: ds.Tables[1];
					if (dtTable.Rows.Count > 0)
					{
						DataRow drow = dtTable.Rows[0];
                        return new ProgressiveStatus
						{
							CustId = Convert.ToInt32(drow["custid"]),
							DisableCasinoBonus = Convert.ToBoolean(drow["DisableCasinoBonus"]),
                            DisableDownlinesCasinoBonus = Convert.ToBoolean(drow["DisableDownlinesCasinoBonus"])
						};
					}
				}
			}

			return null;
		}

		public Hashtable UpdateStatusOfProgressiveBonus(ICriteria criteria)
		{
			using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
			{
				db.ExecuteNonQuery(criteria.GetSpParams());
				return db.GetOuputParams();
			}
		}
	}
}