// -----------------------------------------------------------------------
// <copyright file="ICloneAccountDAO.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.CloneSetting.Repositories.Interface
{
    using Fanex.CustomerSetting.Module.CloneSetting.Criteria;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICloneAccountDAO
    {
        Result Clone(CloneAccountCriteria model);
    }
}