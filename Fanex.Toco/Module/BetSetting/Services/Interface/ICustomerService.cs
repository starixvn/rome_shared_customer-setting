// -----------------------------------------------------------------------
// <copyright file="ICustomerService.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.BetSetting.Services.Interface
{
    using Fanex.CustomerSetting.Module.BetSetting.ViewModels;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICustomerService
    {
        Customer UserInfo(int custId);

        int UserGroupId(int custId);
    }
}
