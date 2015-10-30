namespace Fanex.CustomerSetting.Module.ColossusBets.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using Fanex.BetList.Core.Entities;
    using Fanex.Common;
    using Fanex.CustomerSetting.Module.ColossusBets.Configurations;
    using Fanex.CustomerSetting.Module.ColossusBets.Constants;
    using Fanex.CustomerSetting.Module.ColossusBets.Criteria;
    using Fanex.CustomerSetting.Module.ColossusBets.Models;
    using Fanex.CustomerSetting.Module.ColossusBets.Repositories;
    using Fanex.CustomerSetting.Module.ColossusBets.Resources;
    using Fanex.Module.Logs.Services;
    using Newtonsoft.Json;

    public class ColossusBetsService : IColossusBetsService
    {
        private readonly IColossusBetsRepository _colossusBetsRepository;

        public ColossusBetsService()
            : this(null)
        {
        }

        public ColossusBetsService(IColossusBetsRepository colossusBetsRepository)
        {
            _colossusBetsRepository = colossusBetsRepository ?? new ColossusBetsRepository();
        }

        public IEnumerable<ITicket> BetDetails(GetColossusBetsBetDetailsCriteria criteria)
        {
            var detailsTickets = _colossusBetsRepository.BetDetails(criteria).ToList();
            return detailsTickets;
        }

        public ViewMemberChoiceResultModel MemberChoice(GetColossusBetsBetDetailsCriteria criteria)
        {
            var token = GetToken(criteria);
            var siteId = GetSiteId_ColossusBets(criteria);
            var iframe = string.Format(
                ColossusBetsSection.Instance.Configurations["ColossusBetsAPI_MemberChoice"].Value,
                criteria.EventId,
                token != null ? token.Data.Authorization : string.Empty,
                siteId);
            var result = new ViewMemberChoiceResultModel
            {
                Iframe = iframe
            };

            result.ActionInfo = token != null ?
               new ResultDetail { ResultCode = token.ErrorCode, ResultMessage = token.ErrorMsg } :
               new ResultDetail { ResultCode = 1, ResultMessage = Labels.ColossusBetsAPI_FailedToConnect };

            return result;
        }

        private ViewTokenResultModel GetToken(GetColossusBetsBetDetailsCriteria criteria)
        {
            try
            {
                var getTokenAPI = string.Format(
                    ColossusBetsSection.Instance.Configurations["ColossusBetsAPI_GetToken"].Value,
                    criteria.AccId,
                    criteria.CustId);
                WebClient client = new WebClient();
                var result = JsonConvert.DeserializeObject<ViewTokenResultModel>(client.DownloadString(new Uri(getTokenAPI)));
                return result;
            }
            catch (Exception exception)
            {
                exception.Write();
            }

            return null;
        }

        private int GetSiteId_ColossusBets(GetColossusBetsBetDetailsCriteria criteria)
        {
            var siteIds = ColossusBetsSection.Instance.Configurations["SiteId"].Value.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
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
