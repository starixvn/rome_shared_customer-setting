// --------------------------------------------------------------------------
// <copyright file="KenoSettingController.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.Helper;
    using Fanex.CustomerSetting.Module.Keno.Services;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;

    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class KenoSettingController : KenoSettingBaseController
    {
        private static IKenoSettingService _kenoSettingService;

        public KenoSettingController()
            : this(null)
        {
        }

        public KenoSettingController(IKenoSettingService kenoSettingService)
        {
            _kenoSettingService = kenoSettingService ?? new KenoSettingService();
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of EditSuper.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditSuper")]
        public ActionResult EditSuper(int custId)
        {
            var customerSetting = new SuperSetting();

            customerSetting.CustomerInformation = new CustomerInformation()
            {
                CustId = custId,
                FirstName = "Paul",
                LastName = "Nguyen",
                Phone = "---",
                MobilePhone = "---",
                Fax = "---",
                Username = "Super001"
            };

            customerSetting.BetSettings = new List<BetSetting>() 
            {
                new BetSetting()
                {
                    BetTypeId = 1,
                    BetTypeName = "Beijing",
                    MinBet = 0,
                    MaxBet = 0,
                    MaxPerMatch = 0,
                    IsEnabled = true,
                    IsDownlinesEnabled = true,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 0,
                        MaxPerMatch = 0
                    }
                },
                new BetSetting()
                {
                    BetTypeId = 2,
                    BetTypeName = "Canada",
                    MinBet = 10,
                    MaxBet = 0,
                    MaxPerMatch = 0,
                    IsEnabled = false,
                    IsDownlinesEnabled = true,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 50,
                        MaxPerMatch = 400
                    }
                },
                new BetSetting()
                {
                    BetTypeId = 3,
                    BetTypeName = "Australia",
                    MinBet = 0,
                    MaxBet = 0,
                    MaxPerMatch = 0,
                    IsEnabled = true,
                    IsDownlinesEnabled = false,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 50,
                        MaxPerMatch = 300
                    }
                },
            };

            customerSetting.SuperPositionTakings = new List<SuperPositionTaking>() 
            {
                new SuperPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 1,
                    BetTypeName = "Bejing",
                    S4 = 0.85,
                    M4 = 0.6,
                    A4 = 0.5,
                    N4 = 0,
                    L4 = 0.85,
                    SCo = 0.0095,
                    SCoMax = 0.01
                },
                new SuperPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 2,
                    BetTypeName = "Canada",
                    S4 = 0.85,
                    M4 = 0.6,
                    A4 = 0.5,
                    N4 = 0,
                    L4 = 0.85,
                    SCo = 0.0095,
                    SCoMax = 0.01
                },
                new SuperPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 3,
                    BetTypeName = "Australia",
                    S4 = 0.85,
                    M4 = 0.6,
                    A4 = 0.5,
                    N4 = 0,
                    L4 = 0.85,
                    SCo = 0.0095,
                    SCoMax = 0.01
                }
            };

            return View(Constants.EditSuperCshtml, customerSetting);
        }

        [HttpPost]
        public JsonResult EditSuper(SuperSetting superSetting)
        {
            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }
    }
}
