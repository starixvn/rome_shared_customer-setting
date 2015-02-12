// -----------------------------------------------------------------------
// <copyright file="ICloneAccountService.cs" company="Nexcel Solutions Vietnam">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.CloneSetting.Services.Interface
{
    using Fanex.CustomerSetting.Module.CloneSetting.Entities;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public interface ICloneAccountService
    {
        Result Clone(CloneAccount model);
    }
}