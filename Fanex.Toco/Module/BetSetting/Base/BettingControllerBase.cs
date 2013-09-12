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
    using System;

    /// <summary>
    /// Base class for getting customer info
    /// </summary>
    public class BettingControllerBase : BaseController
    {
        private Fanex.PTEngine.Services.IBettingLimitService sportsbookService = new Fanex.PTEngine.Services.BettingLimitService();
        private Fanex.PTRacing.Services.ICustomerService racingService = new Fanex.PTRacing.Services.CustomerService();
        private Fanex.PTVirtualSports.Services.ICustomerService virtualSportsService = new Fanex.PTVirtualSports.Services.CustomerService();

        public Customer CustomerInfo(int id, int userId)
        {
            ICustomerService customerService = new CustomerService();

            var customer = customerService.UserInfo(id);
            customer.GroupId = customerService.UserGroupId(userId);

            return customer;
        }

        public Fanex.PTEngine.Services.IBettingLimitService SportsbookService
        {
            get
            {
                return this.sportsbookService ?? new Fanex.PTEngine.Services.BettingLimitService();
            }
        }

        public Fanex.PTRacing.Services.ICustomerService RacingService
        {
            get
            {
                return this.racingService ?? new Fanex.PTRacing.Services.CustomerService();
            }
        }

        public Fanex.PTVirtualSports.Services.ICustomerService VirtualSportsService
        {
            get
            {
                return this.virtualSportsService ?? new Fanex.PTVirtualSports.Services.CustomerService();
            }
        }

        public Fanex.CustomerSetting.Module.Casino.Services.Impl.CasinoManager CasinoService
        {
            get
            {
                return Fanex.CustomerSetting.Module.Casino.Services.Impl.CasinoManager.Instance;
            }
        }
    }
}