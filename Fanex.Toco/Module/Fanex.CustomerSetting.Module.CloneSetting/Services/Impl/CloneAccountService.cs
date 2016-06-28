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
    using Fanex.CustomerSetting.Module.CloneSetting.Resources;
    using Fanex.CustomerSetting.Module.CloneSetting.Services.Interface;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CloneAccountService : ICloneAccountService
    {
        private readonly ICloneAccountDAO _dao = new CloneAccountDAO();

        /// <summary>
        /// Clones settings.
        /// </summary>
        /// <param name="model">Cloning information.</param>
        /// <returns>Cloning result.</returns>
        public Result Clone(CloneAccount model)
        {
            var criteria = new CloneAccountCriteria();
            criteria.TargetUsername = string.IsNullOrEmpty(model.TargetUsername) ? string.Empty : model.TargetUsername;
            criteria.DestinationUsername = string.IsNullOrEmpty(model.DestinationUsername) ? string.Empty : model.DestinationUsername;
            var result = _dao.Clone(criteria);
            if (result.ErrorCode == 0)
            {
                result.ErrorMessage = Labels.CloneSetting_Success;
            }

            return result;
        }
    }
}
