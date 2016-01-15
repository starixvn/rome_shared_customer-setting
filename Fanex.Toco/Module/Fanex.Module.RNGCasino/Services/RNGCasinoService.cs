namespace Fanex.Module.RNGCasino.Services
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Net;
    using System.Text;
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

    public class RNGCasinoService : IRNGCasinoService
    {
        private readonly IRNGCasinoRepository _rngCasinoRepository;

        public RNGCasinoService()
            : this(null)
        {
        }

        public RNGCasinoService(IRNGCasinoRepository rngCasinoRepository)
        {
            _rngCasinoRepository = rngCasinoRepository ?? new RNGCasinoRepository();
        }

        public IEnumerable<TicketDetail> BetDetails(GetRNGCasinoBetDetailsCriteria criteria)
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

        public HandHistoryModel GetHandHistory(GetRNGCasinoResultCriteria criteria)
        {
            var result = new HandHistoryModel();
            var token = GetToken(criteria);
            if (token == null)
            {
                result.ActionInfo = new ResultDetail
                {
                    ResultCode = 1,
                    ResultMessage = BetListResources.RNGCasinoAPI_FailedToConnect
                };
            }
            else if (string.IsNullOrEmpty(token.Result.ErrorCode))
            {
                result.ActionInfo = new ResultDetail();
                var parameters = GetHandHistoryParameters(criteria, token.Result.Token);
                result.Iframe = string.Format(OWRNGCasinoSection.Instance.Configurations["HandHistoryAPI"].Value, parameters);
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

        private TokenResponse GetToken(GetRNGCasinoResultCriteria criteria)
        {
            try
            {
                var rngCasinoSection = OWRNGCasinoSection.Instance;
                var siteId = GetSiteId(criteria);
                var getTokenAPI = string.Format(rngCasinoSection.Configurations["GetToken"].Value);
                var requestInfo = new RequestInfo
                {
                    Timestamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff"),
                    AuthInfo = new AuthInfo
                    {
                        Login = "Login",
                        Password = rngCasinoSection.Configurations["PassToGetToken"].Value
                    },
                    CallInfo = new CallInfo()
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
            var data = Encoding.ASCII.GetBytes(parameters);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            return responseString;
        }

        private string GetHandHistoryParameters(GetRNGCasinoResultCriteria criteria, string token)
        {
            var encryptedKey = OWRNGCasinoSection.Instance.Configurations["EncryptedKey"].Value;
            var param = string.Format(
                OWRNGCasinoSection.Instance.Configurations["ParameterList"].Value,
                token,
                criteria.Refno,
                GetLanguage());
            return HttpUtility.UrlEncode(IBCCrypt.IBCCrypt.Encrypt(param, encryptedKey));
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

        private int GetSiteId(GetRNGCasinoResultCriteria criteria)
        {
            var siteIds = OWRNGCasinoSection.Instance.Configurations["SiteId"].Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
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
