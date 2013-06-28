using System.Web.Mvc;
using Fanex.CustomerSetting.Module.Core.Security;
using Fanex.ExternalAccount.Service.Impl.Customer.Casino;
using Fanex.ExternalAccount.DTO.Customer.Casino;
using System.Collections.Generic;
using Fanex.CustomerSetting.Module.Core.Web.Controllers;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Fanex.CustomerSetting.Module.Casino.Controllers
{
    public class CasinoController : BaseController
    {
        [AuthenticateAction(AuthenticateAction.AuthenticationType.Read, "index")]
        public ActionResult Index()
        {
            return View();
        }

        public static int GameCount(int id, int uplineId, int roleId, int uplineRoleId)
        {            
            var custInfo = CasinoManager.Instance.GetCustInfoIBC(id, uplineId);
            var gameKinds = new List<GameKindDetail>();

            if (!custInfo.CasinoSync)
            {
                if (uplineRoleId == BaseController.Super && roleId == BaseController.Member)
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds_EditMember(uplineId);
                }
                else if (uplineRoleId == BaseController.Master && roleId == BaseController.Member)
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds_MasterEditMember(uplineId);
                }
                else
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds(uplineId);
                }
            }
            else
            {
                if (uplineRoleId == BaseController.Super && roleId == BaseController.Member)
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds_EditMember(id);
                }
                else if (uplineRoleId == BaseController.Master && roleId == BaseController.Member)
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds_MasterEditMember(id);
                }
                else
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds(id);
                }
            }

            return gameKinds.Count;          
        }

        public ActionResult EditCasinoPost(int custId, int uplineId, int roleId, int uplineRoleId)
        {
            var custInfo = CasinoManager.Instance.GetCustInfoIBC(custId, uplineId);
            var casinoSetting = new CasinoCustomer();
            var gameKinds = new List<GameKindDetail>();
            var games = new List<List<GameTypeDetail>>();
            var stakes = new List<List<Stakes>>();
            var casinoSettingUpline = new CasinoCustomer();

            // Gets upline settings
            casinoSettingUpline.IBCCustID = uplineId;
            CasinoManager.Instance.GetCustSettingCS(ref casinoSettingUpline);

            if (!custInfo.CasinoSync)
            {
                casinoSetting = CasinoManager.Instance.GetCustSettingCS(uplineId);

                if (uplineRoleId == BaseController.Super && roleId == BaseController.Member)
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds_EditMember(uplineId);
                }
                else if (uplineRoleId == BaseController.Master && roleId == BaseController.Member)
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds_MasterEditMember(uplineId);
                }
                else
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds(uplineId);
                }
            }
            else
            {
                casinoSetting = CasinoManager.Instance.GetCustSettingCS(custId);

                if (uplineRoleId == BaseController.Super && roleId == BaseController.Member)
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds_EditMember(custId);
                }
                else if (uplineRoleId == BaseController.Master && roleId == BaseController.Member)
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds_MasterEditMember(custId);
                }
                else
                {
                    gameKinds = CasinoManager.Instance.GetGameKinds(custId);
                }
            }

            // Sets PTByGameType property from upline to identify whether PT is apply for each game type or not
            casinoSetting.PTByGameType = casinoSetting.PTByGameTypeUpline = casinoSettingUpline.PTByGameType;

            if (gameKinds != null && gameKinds.Count > 0 && custInfo.CasinoSync)
            {
                for (int i = 0; i < gameKinds.Count; i++)
                {
                    if (uplineRoleId == BaseController.Super && roleId == BaseController.Member)
                    {
                        games.Add(CasinoManager.Instance.GetGameTypes_EditMember(custId, gameKinds[i].CodeID));
                    }
                    else if (uplineRoleId == BaseController.Master && roleId == BaseController.Member)
                    {
                        games.Add(CasinoManager.Instance.GetGameTypes_MasterEditMember(custId, gameKinds[i].CodeID));
                    }
                    else
                    {
                        games.Add(CasinoManager.Instance.GetGameType(custId, gameKinds[i].CodeID));
                    }
                }
            }
            else if (gameKinds != null && gameKinds.Count > 0 && !custInfo.CasinoSync)
            {
                for (int i = 0; i < gameKinds.Count; i++)
                {
                    if (uplineRoleId == BaseController.Super && roleId == BaseController.Member)
                    {
                        games.Add(CasinoManager.Instance.GetGameTypes_EditMember(uplineId, gameKinds[i].CodeID));
                    }
                    else if (uplineRoleId == BaseController.Master && roleId == BaseController.Member)
                    {
                        games.Add(CasinoManager.Instance.GetGameTypes_MasterEditMember(uplineId, gameKinds[i].CodeID));
                    }
                    else
                    {
                        games.Add(CasinoManager.Instance.GetGameType(uplineId, gameKinds[i].CodeID));
                    }
                }
            }

            if (games != null && games.Count > 0 && custInfo.CasinoSync)
            {
                foreach (var gameTypes in games)
                {
                    var stakeList = new List<Stakes>();

                    foreach (var gameType in gameTypes)
                    {
                        stakeList.Add(new Stakes
                        {
                            GameTypeID = gameType.GameTypeID,
                            GameTypeName = gameType.GameTypeName,
                            StakeLevels = CasinoManager.Instance.GetCustStakeLevels(custId, gameType.GameTypeID),
                            Pt = gameType
                        });
                    }

                    stakes.Add(stakeList);
                }
            }
            else if (games != null && games.Count > 0 && !custInfo.CasinoSync)
            {
                foreach (var gameTypes in games)
                {
                    var stakeList = new List<Stakes>();

                    foreach (var gameType in gameTypes)
                    {
                        stakeList.Add(new Stakes
                        {
                            GameTypeID = gameType.GameTypeID,
                            GameTypeName = gameType.GameTypeName,
                            StakeLevels = CasinoManager.Instance.GetCustStakeLevels(uplineId, gameType.GameTypeID),
                            Pt = gameType
                        });
                    }

                    stakes.Add(stakeList);
                }
            }

            return this.JsonBase(
                new CasinoObject
                {
                    CustomerInfo = custInfo,
                    CasinoInfo = casinoSetting,
                    CasinoGames = stakes,
                    Pts = gameKinds
                },
                new Newtonsoft.Json.JsonSerializerSettings
                {
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                });
        }

        public ActionResult UpdateCasinoPost(int custId, int roleId, int uplineId, int uplineRoleId, string customer)
        {
            var ret = CasinoManager.Instance.Update(custId, roleId, uplineId, uplineRoleId, CurrentContext.IP, CurrentSession.UserId, CurrentSession.Username, BaseController.FromSystem, JsonConvert.DeserializeObject<CasinoObject>(customer));

            return this.JsonBase(new { ErrorCode = ret.IsSuccess ? 0 : 1, ErrorMessage = ret.ErrorMessage });
        }
    }
}
