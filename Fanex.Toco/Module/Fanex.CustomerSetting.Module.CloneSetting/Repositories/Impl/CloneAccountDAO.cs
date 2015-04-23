// -----------------------------------------------------------------------
// <copyright file="CloneAccountDAO.cs" company="Nexcel Solutions Vietnam">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.CloneSetting.Services.Interface
{
    using Fanex.CustomerSetting.Module.CloneSetting.Criteria;
    using Fanex.CustomerSetting.Module.CloneSetting.Repositories.Interface;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CloneAccountDAO : ICloneAccountDAO
    {
        /// <summary>
        /// Clones setting.
        /// </summary>
        /// <param name="criteria">Clone setting criteria.</param>
        /// <returns>Result object.</returns>
        public Result Clone(CloneAccountCriteria criteria)
        {
            using (var db = new Fanex.Data.ObjectDb(criteria.GetSettingKey()))
            {
                db.AssignParamValues(criteria.GetSpParams());
                db.ExecuteNonQuery();

                var output = db.GetOuputParams();

                return new Result((int)output["errcode"], (string)output["errmsg"]);
            }
        }
    }
}
