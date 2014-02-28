// --------------------------------------------------------------------------
// <copyright file="KenoSettingBaseController.cs" company="Nexcel Solutions Vietnam">
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
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using Fanex.CustomerSetting.Security.Authenticate;
    using Fanex.CustomerSetting.Module.Keno.Helper;
    using Fanex.Keno;
    using Fanex.CustomerSetting.Module.Keno.Entities;
    using Fanex.CustomerSetting.Module.Keno.Services;

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
        /// <returns></returns>
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
                    SCO = 0.0095,
                    SCOMax = 0.01
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
                    SCO = 0.0095,
                    SCOMax = 0.01
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
                    SCO = 0.0095,
                    SCOMax = 0.01
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

        ///// <summary>
        ///// Change name controller to clear.
        ///// </summary>
        ///// <returns></returns>
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditMaster")]
        //public ActionResult EditMaster(int custId)
        //{
        //    var customerSetting = new CustomerSettingBase();

        //    customerSetting.FormType = (int)FormType.EditMaster;

        //    customerSetting.CustomerInformation = new CustomerInformation()
        //    {
        //        CustId = custId,
        //        FirstName = "Paul",
        //        LastName = "Nguyen",
        //        Phone = "---",
        //        MobilePhone = "---",
        //        Fax = "---",
        //        Username = "Master001"
        //    };

        //    customerSetting.BetSettings = new List<BetSetting>() 
        //    {
        //        new BetSetting()
        //        {
        //            BetTypeId = 1,
        //            BetTypeName = "Beijing",
        //            MinBet = 0,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = false,
        //            IsDownlinesEnabled = true,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 100
        //            }
        //        },
        //        new BetSetting()
        //        {
        //            BetTypeId = 2,
        //            BetTypeName = "Canada",
        //            MinBet = 10,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = false,
        //            IsDownlinesEnabled = true,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 400
        //            }
        //        },
        //        new BetSetting()
        //        {
        //            BetTypeId = 3,
        //            BetTypeName = "Australia",
        //            MinBet = 0,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = true,
        //            IsDownlinesEnabled = false,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 300
        //            }
        //        },
        //    };

        //    customerSetting.PositiongTakingSections = new List<PositiongTakingSection>() 
        //    {
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 1,
        //                BetTypeName = "Bejing",
        //                Max = 0.85M,
        //                Min = 0.5M,
        //                Preset = 0.85M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.85M,
        //                    Min = 0.5M,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 1,
        //                BetTypeName = "Bejing",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        },
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 2,
        //                BetTypeName = "Canada",
        //                Max = 0.80M,
        //                Min = 0,
        //                Preset = 0.80M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.80M,
        //                    Min = 0,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 2,
        //                BetTypeName = "Canada",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        },
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 3,
        //                BetTypeName = "Australia",
        //                Max = 0.70M,
        //                Min = 0,
        //                Preset = 0.70M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.70M,
        //                    Min = 0,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 3,
        //                BetTypeName = "Australia",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        }
        //    };

        //    return View(Constants.EDIT_MASTER_CSHTML, customerSetting);
        //}

        ///// <summary>
        ///// Change name controller to clear.
        ///// </summary>
        ///// <returns></returns>
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "EditAgent")]
        //public ActionResult EditAgent(int custId)
        //{
        //    var customerSetting = new CustomerSettingBase();

        //    customerSetting.FormType = (int)FormType.EditAgent;

        //    customerSetting.CustomerInformation = new CustomerInformation()
        //    {
        //        CustId = custId,
        //        FirstName = "Paul",
        //        LastName = "Nguyen",
        //        Phone = "---",
        //        MobilePhone = "---",
        //        Fax = "---",
        //        Username = "Agent001"
        //    };

        //    customerSetting.BetSettings = new List<BetSetting>() 
        //    {
        //        new BetSetting()
        //        {
        //            BetTypeId = 1,
        //            BetTypeName = "Beijing",
        //            MinBet = 0,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = false,
        //            IsDownlinesEnabled = true,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 100
        //            }
        //        },
        //        new BetSetting()
        //        {
        //            BetTypeId = 2,
        //            BetTypeName = "Canada",
        //            MinBet = 10,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = false,
        //            IsDownlinesEnabled = true,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 400
        //            }
        //        },
        //        new BetSetting()
        //        {
        //            BetTypeId = 3,
        //            BetTypeName = "Australia",
        //            MinBet = 0,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = true,
        //            IsDownlinesEnabled = false,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 300
        //            }
        //        },
        //    };

        //    customerSetting.PositiongTakingSections = new List<PositiongTakingSection>() 
        //    {
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 1,
        //                BetTypeName = "Bejing",
        //                Max = 0.85M,
        //                Min = 0.5M,
        //                Preset = 0.85M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.85M,
        //                    Min = 0.5M,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 1,
        //                BetTypeName = "Bejing",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        },
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 2,
        //                BetTypeName = "Canada",
        //                Max = 0.80M,
        //                Min = 0,
        //                Preset = 0.80M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.80M,
        //                    Min = 0,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 2,
        //                BetTypeName = "Canada",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        },
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 3,
        //                BetTypeName = "Australia",
        //                Max = 0.70M,
        //                Min = 0,
        //                Preset = 0.70M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.70M,
        //                    Min = 0,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 3,
        //                BetTypeName = "Australia",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        }
        //    };

        //    return View(Constants.EDIT_AGENT_CSHTML, customerSetting);
        //}

        ///// <summary>
        ///// Change name controller to clear.
        ///// </summary>
        ///// <returns></returns>
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "SuperEditMember")]
        //public ActionResult SuperEditMember(int custId)
        //{
        //    var customerSetting = new CustomerSettingBase();

        //    customerSetting.FormType = (int)FormType.SuperEditMember;

        //    customerSetting.CustomerInformation = new CustomerInformation()
        //    {
        //        CustId = custId,
        //        FirstName = "Paul",
        //        LastName = "Nguyen",
        //        Phone = "---",
        //        MobilePhone = "---",
        //        Fax = "---",
        //        Username = "Member001"
        //    };

        //    customerSetting.BetSettings = new List<BetSetting>() 
        //    {
        //        new BetSetting()
        //        {
        //            BetTypeId = 1,
        //            BetTypeName = "Beijing",
        //            MinBet = 0,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = false,
        //            IsDownlinesEnabled = true,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 100
        //            }
        //        },
        //        new BetSetting()
        //        {
        //            BetTypeId = 2,
        //            BetTypeName = "Canada",
        //            MinBet = 10,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = false,
        //            IsDownlinesEnabled = true,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 400
        //            }
        //        },
        //        new BetSetting()
        //        {
        //            BetTypeId = 3,
        //            BetTypeName = "Australia",
        //            MinBet = 0,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = true,
        //            IsDownlinesEnabled = false,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 300
        //            }
        //        },
        //    };

        //    customerSetting.PositiongTakingSections = new List<PositiongTakingSection>() 
        //    {
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 1,
        //                BetTypeName = "Bejing",
        //                Max = 0.85M,
        //                Min = 0.5M,
        //                Preset = 0.85M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.85M,
        //                    Min = 0.5M,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 1,
        //                BetTypeName = "Bejing",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        },
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 2,
        //                BetTypeName = "Canada",
        //                Max = 0.80M,
        //                Min = 0,
        //                Preset = 0.80M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.80M,
        //                    Min = 0,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 2,
        //                BetTypeName = "Canada",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        },
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 3,
        //                BetTypeName = "Australia",
        //                Max = 0.70M,
        //                Min = 0,
        //                Preset = 0.70M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.70M,
        //                    Min = 0,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 3,
        //                BetTypeName = "Australia",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        }
        //    };

        //    return View(Constants.SUPER_EDIT_MEMBER_CSHTML, customerSetting);
        //}

        ///// <summary>
        ///// Change name controller to clear.
        ///// </summary>
        ///// <returns></returns>
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "MasterEditMember")]
        //public ActionResult MasterEditMember(int custId)
        //{
        //    var customerSetting = new CustomerSettingBase();

        //    customerSetting.FormType = (int)FormType.MasterEditMember;

        //    customerSetting.CustomerInformation = new CustomerInformation()
        //    {
        //        CustId = custId,
        //        FirstName = "Paul",
        //        LastName = "Nguyen",
        //        Phone = "---",
        //        MobilePhone = "---",
        //        Fax = "---",
        //        Username = "Member001"
        //    };

        //    customerSetting.BetSettings = new List<BetSetting>() 
        //    {
        //        new BetSetting()
        //        {
        //            BetTypeId = 1,
        //            BetTypeName = "Beijing",
        //            MinBet = 0,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = false,
        //            IsDownlinesEnabled = true,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 100
        //            }
        //        },
        //        new BetSetting()
        //        {
        //            BetTypeId = 2,
        //            BetTypeName = "Canada",
        //            MinBet = 10,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = false,
        //            IsDownlinesEnabled = true,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 400
        //            }
        //        },
        //        new BetSetting()
        //        {
        //            BetTypeId = 3,
        //            BetTypeName = "Australia",
        //            MinBet = 0,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = true,
        //            IsDownlinesEnabled = false,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 300
        //            }
        //        },
        //    };

        //    customerSetting.PositiongTakingSections = new List<PositiongTakingSection>() 
        //    {
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 1,
        //                BetTypeName = "Bejing",
        //                Max = 0.85M,
        //                Min = 0.5M,
        //                Preset = 0.85M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.85M,
        //                    Min = 0.5M,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 1,
        //                BetTypeName = "Bejing",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        },
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 2,
        //                BetTypeName = "Canada",
        //                Max = 0.80M,
        //                Min = 0,
        //                Preset = 0.80M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.80M,
        //                    Min = 0,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 2,
        //                BetTypeName = "Canada",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        },
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 3,
        //                BetTypeName = "Australia",
        //                Max = 0.70M,
        //                Min = 0,
        //                Preset = 0.70M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.70M,
        //                    Min = 0,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 3,
        //                BetTypeName = "Australia",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        }
        //    };

        //    return View(Constants.MASTER_EDIT_MEMBER_CSHTML, customerSetting);
        //}

        ///// <summary>
        ///// Change name controller to clear.
        ///// </summary>
        ///// <returns></returns>
        //[AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "AgentEditMember")]
        //public ActionResult AgentEditMember(int custId)
        //{
        //    var customerSetting = new CustomerSettingBase();

        //    customerSetting.FormType = (int)FormType.AgentEditMember;

        //    customerSetting.CustomerInformation = new CustomerInformation()
        //    {
        //        CustId = custId,
        //        FirstName = "Paul",
        //        LastName = "Nguyen",
        //        Phone = "---",
        //        MobilePhone = "---",
        //        Fax = "---",
        //        Username = "Member001"
        //    };

        //    customerSetting.BetSettings = new List<BetSetting>() 
        //    {
        //        new BetSetting()
        //        {
        //            BetTypeId = 1,
        //            BetTypeName = "Beijing",
        //            MinBet = 0,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = false,
        //            IsDownlinesEnabled = true,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 100
        //            }
        //        },
        //        new BetSetting()
        //        {
        //            BetTypeId = 2,
        //            BetTypeName = "Canada",
        //            MinBet = 10,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = false,
        //            IsDownlinesEnabled = true,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 400
        //            }
        //        },
        //        new BetSetting()
        //        {
        //            BetTypeId = 3,
        //            BetTypeName = "Australia",
        //            MinBet = 0,
        //            MaxBet = 0,
        //            MaxPerMatch = 0,
        //            IsEnabled = true,
        //            IsDownlinesEnabled = false,
        //            Upline = new BetSetting() 
        //            {
        //                MinBet = 10,
        //                MaxBet = 50,
        //                MaxPerMatch = 300
        //            }
        //        },
        //    };

        //    customerSetting.PositiongTakingSections = new List<PositiongTakingSection>() 
        //    {
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 1,
        //                BetTypeName = "Bejing",
        //                Max = 0.85M,
        //                Min = 0.5M,
        //                Preset = 0.85M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.85M,
        //                    Min = 0.5M,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 1,
        //                BetTypeName = "Bejing",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        },
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 2,
        //                BetTypeName = "Canada",
        //                Max = 0.80M,
        //                Min = 0,
        //                Preset = 0.80M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.80M,
        //                    Min = 0,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 2,
        //                BetTypeName = "Canada",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        },
        //        new PositiongTakingSection()
        //        {
        //            PositionTaking = new PositionTakingBase()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 3,
        //                BetTypeName = "Australia",
        //                Max = 0.70M,
        //                Min = 0,
        //                Preset = 0.70M,
        //                Upline = new PositionTakingBase()
        //                {
        //                    Max = 0.70M,
        //                    Min = 0,
        //                    Preset = 0,
        //                }
        //            },
        //            CommissionSetting = new CommissionSetting()
        //            {
        //                SportTypeId = 202,
        //                BetTypeId = 3,
        //                BetTypeName = "Australia",
        //                Value = 0.0095M,
        //                Upline = new CommissionSetting()
        //                {
        //                    SportTypeId = 202,
        //                    BetTypeId = 1,
        //                    Value = 0.01M
        //                }
        //            }
        //        }
        //    };

        //    return View(Constants.AGENT_EDIT_MEMBER_CSHTML, customerSetting);
        //}

        //[HttpPost]
        //public JsonResult UpdateCustomerSetting(CustomerSettingBase customerSetting)
        //{
        //    return Json(new 
        //    {
        //        ErrCode = 1,
        //        ErrMsg = "Reponded From ServerSide: Not Implemented Yet"
        //    });
        //}
    }
}
