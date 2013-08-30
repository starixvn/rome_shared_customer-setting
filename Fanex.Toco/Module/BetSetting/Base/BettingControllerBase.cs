// --------------------------------------------------------------------------
// <copyright file="BettingControllerBase.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.8.28">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.BetSetting.Base
{
    using Fanex.CustomerSetting.Module.BetSetting.Services.Impl;
    using Fanex.CustomerSetting.Module.BetSetting.Services.Interface;
    using Fanex.CustomerSetting.Module.BetSetting.ViewModels;
    using Fanex.CustomerSetting.Module.Core.Web.Controllers;

    /// <summary>
    /// Base class for getting customer info
    /// </summary>
    public class BettingControllerBase : BaseController
    {
        public Customer CustomerInfo(int id, int userId)
        {
            ICustomerService customerService = new CustomerService();

            var customer = customerService.UserInfo(id);
            customer.GroupId = customerService.UserGroupId(userId);

            return customer;
        }
    }
}
