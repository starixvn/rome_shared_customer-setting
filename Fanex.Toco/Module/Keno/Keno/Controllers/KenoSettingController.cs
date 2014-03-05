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
                RoleId = 4,
                FirstName = "Paul",
                LastName = "Nguyen",
                Phone = "---",
                MobilePhone = "---",
                Fax = "---",
                Username = "Super001"
            };

            customerSetting.BetSettingCopyAll = customerSetting.PositionTakingCopyAll = new CopyAll { IsCopiedAll = false, BettypeId = -1 };

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
                        MaxPerMatch = 0,
                        IsEnabled = true,
                        IsDownlinesEnabled = true
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
                    IsDownlinesEnabled = false,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 50,
                        MaxPerMatch = 400,
                        IsEnabled = true,
                        IsDownlinesEnabled = false
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
                        MaxPerMatch = 300,
                        IsEnabled = false,
                        IsDownlinesEnabled = false
                    }
                },

                 new BetSetting()
                {
                    BetTypeId = 4,
                    BetTypeName = "Vietnam",
                    MinBet = 10,
                    MaxBet = 0,
                    MaxPerMatch = 0,
                    IsEnabled = false,
                    IsDownlinesEnabled = false,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 50,
                        MaxPerMatch = 400,
                        IsEnabled = true,
                        IsDownlinesEnabled = true
                    }
                },
                new BetSetting()
                {
                    BetTypeId = 4,
                    BetTypeName = "Thai",
                    MinBet = 0,
                    MaxBet = 0,
                    MaxPerMatch = 0,
                    IsEnabled = true,
                    IsDownlinesEnabled = false,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 50,
                        MaxPerMatch = 300,
                        IsEnabled = true,
                        IsDownlinesEnabled = true
                    }
                },
            };

            customerSetting.PositionTakings = new List<SuperPositionTaking>() 
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
                    SComm = 0.0095,
                    SCommMax = 0.01
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
                    SComm = 0.0095,
                    SCommMax = 0.01
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
                    SComm = 0.0095,
                    SCommMax = 0.01
                },
                 new SuperPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 2,
                    BetTypeName = "Vietnam",
                    S4 = 0.85,
                    M4 = 0.6,
                    A4 = 0.5,
                    N4 = 0,
                    L4 = 0.85,
                    SComm = 0.0095,
                    SCommMax = 0.01
                },
                new SuperPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 3,
                    BetTypeName = "Thai",
                    S4 = 0.85,
                    M4 = 0.6,
                    A4 = 0.5,
                    N4 = 0,
                    L4 = 0.85,
                    SComm = 0.0095,
                    SCommMax = 0.01
                }
            };

            return View(Constants.EDIT_SUPER_CSHTML, customerSetting);
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

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of EditMaster.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditMaster")]
        public ActionResult EditMaster(int custId)
        {
            var customerSetting = new MasterSetting();

            customerSetting.CustomerInformation = new CustomerInformation()
            {
                CustId = custId,
                RoleId = 3,
                FirstName = "Paul",
                LastName = "Nguyen",
                Phone = "---",
                MobilePhone = "---",
                Fax = "---",
                Username = "Master001"
            };

            customerSetting.BetSettingCopyAll = customerSetting.PositionTakingCopyAll = new CopyAll { IsCopiedAll = false, BettypeId = -1 };

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
                        MaxBet = 100,
                        MaxPerMatch = 1000,
                        IsEnabled = true,
                        IsDownlinesEnabled = true
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
                    IsDownlinesEnabled = false,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 50,
                        MaxPerMatch = 400,
                        IsEnabled = true,
                        IsDownlinesEnabled = false
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
                        MaxPerMatch = 300,
                        IsEnabled = false,
                        IsDownlinesEnabled = false
                    }
                },

                 new BetSetting()
                {
                    BetTypeId = 4,
                    BetTypeName = "Vietnam",
                    MinBet = 10,
                    MaxBet = 0,
                    MaxPerMatch = 0,
                    IsEnabled = false,
                    IsDownlinesEnabled = false,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 50,
                        MaxPerMatch = 400,
                        IsEnabled = true,
                        IsDownlinesEnabled = true
                    }
                },
                new BetSetting()
                {
                    BetTypeId = 4,
                    BetTypeName = "Thai",
                    MinBet = 0,
                    MaxBet = 0,
                    MaxPerMatch = 0,
                    IsEnabled = true,
                    IsDownlinesEnabled = false,
                    Upline = new BetSetting() 
                    {
                        MinBet = 10,
                        MaxBet = 50,
                        MaxPerMatch = 300,
                        IsEnabled = true,
                        IsDownlinesEnabled = true
                    }
                },
            };

            customerSetting.PositionTakings = new List<MasterPositionTaking>() 
            {
                new MasterPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 1,
                    BetTypeName = "Bejing",
                    S4 = 0.8,
                    S3 = 0.2,
                    US3 = 0.6,
                    IS3 = false,
                    M3 = 0.5,
                    N3 = 0.1,
                    SComm = 0.0095,
                    MComm = 0.003
                },
                new MasterPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 2,
                    BetTypeName = "Canada",
                    S4 = 0.8,
                    S3 = 0.2,
                    US3 = 0.6,
                    IS3 = false,
                    M3 = 0.5,
                    N3 = 0.1,
                    SComm = 0.0095,
                    MComm = 0.003
                },
                new MasterPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 3,
                    BetTypeName = "Australia",
                    S4 = 0.8,
                    S3 = 0.2,
                    US3 = 0.6,
                    IS3 = false,
                    M3 = 0.5,
                    N3 = 0.1,
                    SComm = 0.0095,
                    MComm = 0.003
                },
                 new MasterPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 2,
                    BetTypeName = "Vietnam",
                     S4 = 0.8,
                    S3 = 0.2,
                    US3 = 0.6,
                    IS3 = false,
                    M3 = 0.5,
                    N3 = 0.1,
                    SComm = 0.0095,
                    MComm = 0.003
                },
                new MasterPositionTaking()
                {
                    SportTypeId = 202,
                    BetTypeId = 3,
                    BetTypeName = "Thai",
                    S4 = 0.8,
                    S3 = 0.2,
                    US3 = 0.6,
                    IS3 = false,
                    M3 = 0.5,
                    N3 = 0.1,
                    SComm = 0.0095,
                    MComm = 0.003
                }
            };

            return View(Constants.EDIT_MASTER_CSHTML, customerSetting);
   
        }

        [HttpPost]
        public JsonResult EditMaster(MasterSetting masterSetting)
        {
            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of EditAgent.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditAgent")]
        public ActionResult EditAgent(int custId)
        {
            return View(Constants.EditAgentCshtml);
        }

        [HttpPost]
        public JsonResult EditAgent(MasterSetting agentSetting)
        {
            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of SuperEditMember.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "SuperEditMember")]
        public ActionResult SuperEditMember(int custId)
        {
            return View(Constants.SuperEditMemberCshtml);
        }

        [HttpPost]
        public JsonResult SuperEditMember(MemberSetting memberSetting)
        {
            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of MasterEditMember.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "MasterEditMember")]
        public ActionResult MasterEditMember(int custId)
        {
            return View(Constants.MasterEditMemberCshtml);
        }

        [HttpPost]
        public JsonResult MasterEditMember(MemberSetting memberSetting)
        {
            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }

        /// <summary>
        /// Change name controller to clear.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>The ActionResult of MasterEditMember.</returns>
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "AgentEditMember")]
        public ActionResult AgentEditMember(int custId)
        {
            return View(Constants.AgentEditMemberCshtml);
        }

        [HttpPost]
        public JsonResult AgentEditMember(MemberSetting memberSetting)
        {
            return Json(new
            {
                ErrCode = 1,
                ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
            });
        }
    }
}
