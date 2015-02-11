// -----------------------------------------------------------------------
// <copyright file="CloneAccountService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.CloneSetting.Services.Impl
{
    using Fanex.CustomerSetting.Module.CloneSetting.Criteria;
    using Fanex.CustomerSetting.Module.CloneSetting.Entities;
    using Fanex.CustomerSetting.Module.CloneSetting.Repositories.Interface;
    using Fanex.CustomerSetting.Module.CloneSetting.Services.Interface;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CloneAccountService : ICloneAccountService
    {
        private readonly ICloneAccountDAO _dao = new CloneAccountDAO();

        public Result Clone(CloneAccount model)
        {
            var criteria = new CloneAccountCriteria();
            criteria.TargetUsername = model.TargetUsername;
            criteria.DestinationUsername = model.DestinationUsername;

            return _dao.Clone(criteria);
        }
    }
}
