// -----------------------------------------------------------------------
// <copyright file="ICustomerDao.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.BetSetting.Repositories.Interface
{
    using Fanex.CustomerSetting.Module.BetSetting.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICustomerDao
    {
        Customer UserInfo(int custId);

        int UserGroupId(int custId);
    }
}
