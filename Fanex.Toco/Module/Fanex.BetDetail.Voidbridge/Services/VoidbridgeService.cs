namespace Fanex.Voidbridge.BetDetail.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Net;
    using System.Web;
    using Common;
    using Configurations;
    using Constants;
    using Criteria;
    using Extensions;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Fanex.Module.Logs.Services;
    using Helpers;
    using Models;
    using Models.HandHistory;
    using Newtonsoft.Json;
    using Repositories;
    using Resources;

    public class VoidbridgeService : IVoidbridgeService
    {
        private readonly IVoidbridgeRepository _rngCasinoRepository;

        public VoidbridgeService()
            : this(null)
        {
        }

        public VoidbridgeService(IVoidbridgeRepository rngCasinoRepository)
        {
            _rngCasinoRepository = rngCasinoRepository ?? new VoidbridgeRepository();
        }

        public IEnumerable<TicketDetail> BetDetails(GetVoidbridgeBetDetailsCriteria criteria)
        {
            var rawData = _rngCasinoRepository.BetDetails(criteria);
            var detailsTickets = new List<TicketDetail>();
            if (rawData != null)
            {
                ITicketHelper ticketHelper = new TicketHelper();
                BetListHTMLFactory factory = new BetListHTMLFactory();
                BetListHTMLBuilder builder = factory.CreateBuilder();
                BetListHelper betListHelper = new BetListHelper();
                GetCachePropertyById funcGetOddsTypeName = CustomerSetting.Module.Core.Services.Impl.CacheRef.GetOddsTypeName;

                List<ITicketData> outputLiveCasino = new List<ITicketData>();
                foreach (var ticket in rawData)
                {
                    var ticketDetail = new TicketDetail();
                    ticketDetail.Ticket = ticket;
                    ticketHelper.IsShowOddsWithChoice = betListHelper.IsShowOddsWithChoice(ticketDetail.Ticket.BetTypeId);
                    ticketDetail.TransDateHtml = builder.BuildTrans(ticketDetail.Ticket);
                    ticketDetail.ChoiceHtml = builder.BuildChoice(ticketDetail.Ticket, ticketHelper, outputLiveCasino, false);
                    ticketDetail.StakeHtml = BetListExtension.BuildStake(ticketDetail, ref builder);
                    ticketDetail.OddHtml = builder.BuildOdds(ticketDetail.Ticket, outputLiveCasino, funcGetOddsTypeName);
                    ticketDetail.StatusHtml = betListHelper.StatusRefNo(ticketDetail);
                    detailsTickets.Add(ticketDetail);
                }
            }

            return detailsTickets;
        }

        public HandHistoryModel GetHandHistory(GetVoidbridgeResultCriteria criteria)
        {
            var result = new HandHistoryModel();
            var token = GetToken(criteria);
            if (token == null)
            {
                result.ActionInfo = new ResultDetail
                {
                    ResultCode = 1,
                    ResultMessage = BetListResources.VoidbridgeAPI_FailedToConnect
                };
            }
            else if (string.Equals(token.Result.ErrorCode, "0"))
            {
                result.ActionInfo = new ResultDetail();
                var parameters = GetHandHistoryParameters(criteria, token.Result.Token);
                result.Iframe = string.Format(OWVoidbridgeSection.Instance.Configurations["HandHistoryAPI"].Value, parameters);
            }
            else
            {
                result.ActionInfo = new ResultDetail
                {
                    ResultCode = 1,
                    ResultMessage = token.Result.ErrorDescription
                };
            }

            return result;
        }

        private TokenResponse GetToken(GetVoidbridgeResultCriteria criteria)
        {
            try
            {
                var rngCasinoSection = OWVoidbridgeSection.Instance;
                var siteId = GetSiteId(criteria);
                var getTokenAPI = string.Format(rngCasinoSection.Configurations["GetToken"].Value);
                var requestInfo = new Request
                {
                    Timestamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                    Auth = new Auth
                    {
                        Login = "Login",
                        Password = rngCasinoSection.Configurations["PassToGetToken"].Value,
                        SiteId = siteId
                    },
                    Call = new Call
                    {
                        Seq = rngCasinoSection.Configurations["Seq"].Value,
                        VendorId = Converter.ToInt(rngCasinoSection.Configurations["VendorId"].Value)
                    }
                };

                var responseString = CallHttpPost(JsonConvert.SerializeObject(requestInfo), getTokenAPI);
                var result = JsonConvert.DeserializeObject<TokenResponse>(responseString);
                return result;
            }
            catch (Exception exception)
            {
                exception.Write();
            }

            return null;
        }

        private string CallHttpPost(string parameters, string url)
        {
            var responseString = string.Empty;
            using (WebClient webClient = new WebClient())
            {
                webClient.Headers[HttpRequestHeader.ContentType] = "application/json";
                responseString = webClient.UploadString(url, parameters);
            }

            return responseString;
        }

        private string GetHandHistoryParameters(GetVoidbridgeResultCriteria criteria, string token)
        {
            var encryptedKey = OWVoidbridgeSection.Instance.Configurations["EncryptedKey"].Value;
            var param = string.Format(
                OWVoidbridgeSection.Instance.Configurations["ParameterList"].Value,
                token,
                criteria.Refno,
                GetLanguage());
            var result = HttpUtility.UrlEncode(IBCCrypt.IBCCrypt.Encrypt(param, encryptedKey));
            return result;
        }

        private string GetLanguage()
        {
            var lang = CultureInfo.CurrentCulture.Name;
            switch (lang)
            {
                case "en-US":
                    return "en";
                case "zh-TW":
                    // Traditional
                    return "ch";
                case "zh-CN":
                    // Simplified
                    return "cs";
                case "ja-JP":
                    return "jp";
                case "ko-KR":
                    return "ko";
                case "vi-VN":
                    return "vi";
                case "th-TH":
                    // Thai
                    return "th";
                default:
                    return "en";
            }
        }

        private int GetSiteId(GetVoidbridgeResultCriteria criteria)
        {
            var siteIds = OWVoidbridgeSection.Instance.Configurations["SiteId"].Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
            switch (criteria.RoleId)
            {
                case RoleIdConstants.Super:
                    return Converter.ToInt(siteIds[0]);
                case RoleIdConstants.Master:
                    return Converter.ToInt(siteIds[1]);
                case RoleIdConstants.Agent:
                    return Converter.ToInt(siteIds[2]);
                default:
                    return Converter.ToInt(siteIds[0]);
            }
        }
    }
}
