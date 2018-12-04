using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Fanex.BetList.Core.Entities;
using Fanex.CustomerSetting.Module.ColossusBets.Configurations;
using Fanex.CustomerSetting.Module.ColossusBets.Constants;
using Fanex.CustomerSetting.Module.ColossusBets.Criteria;
using Fanex.CustomerSetting.Module.ColossusBets.Models;
using Fanex.CustomerSetting.Module.ColossusBets.Repositories;
using Fanex.CustomerSetting.Module.ColossusBets.Resources;
using Fanex.Logging.Extensions.Web;
using Newtonsoft.Json;
using Fanex.Data.Repository;

namespace Fanex.CustomerSetting.Module.ColossusBets.Services
{
    public class ColossusBetsService : IColossusBetsService
    {
        private readonly IColossusBetsRepository _colossusBetsRepository;

        public ColossusBetsService(IColossusBetsRepository colossusBetsRepository = null)
        {
            _colossusBetsRepository = colossusBetsRepository ?? new ColossusBetsRepository();
        }

        public IEnumerable<ITicket> BetDetails(GetColossusBetsBetDetailsCriteria criteria)
        {
            return _colossusBetsRepository.BetDetails(criteria).ToList();
        }

        public ViewMemberChoiceResultModel MemberChoice(GetColossusBetsBetDetailsCriteria criteria)
        {
            var siteId = GetSiteId(criteria.RoleId);
            var tokenResult = GetTokenResult(criteria.AccId, criteria.CustId);
            var memberChoice = string.Format(
                ColossusBetsSection.MemberChoice,
                criteria.EventId,
                tokenResult != null ? tokenResult.Data.Authorization : string.Empty,
                siteId);

            var siteIdByCustomer = GetSiteIdByCustomerId(criteria.CustId);
            var domain = ColossusBetsSection.GetDomainBySiteId(siteIdByCustomer);

            var result = new ViewMemberChoiceResultModel
            {
                Iframe = domain + memberChoice
            };

            result.ActionInfo = tokenResult != null ?
               new ResultDetail { ResultCode = tokenResult.ErrorCode, ResultMessage = tokenResult.ErrorMsg } :
               new ResultDetail { ResultCode = 1, ResultMessage = Labels.ColossusBetsAPI_FailedToConnect };

            return result;
        }

        private string GetSiteId(int roleId)
        {
            var siteIds = ColossusBetsSection.SiteId.Split(new char[] { ',', ';' }, StringSplitOptions.RemoveEmptyEntries);

            switch (roleId)
            {  
                case RoleIdConstants.Master:
                    return siteIds[1];

                case RoleIdConstants.Agent:
                    return siteIds[2];

                default:
                    return siteIds[0];
            }
        }

        private ViewTokenResultModel GetTokenResult(int userId, int customerId)
        {
            WebClient client = new WebClient();
            try
            {               
                string tokenUrl = string.Format(ColossusBetsSection.TokenUrl, userId, customerId);
                string resultJSON = client.DownloadString(new Uri(tokenUrl));
                return JsonConvert.DeserializeObject<ViewTokenResultModel>(resultJSON);
            }
            catch (Exception exception)
            {
                ExceptionHandler.Handle(exception);
            }
            finally
            {
                if (client != null)
                {
                    client.Dispose();
                }
                    
            }  
            return null;
        }

        private string GetSiteIdByCustomerId(int customerId)
        {
            ICriteria criteria = new GetSiteIdByCustomerIdCriteria(customerId);

            return _colossusBetsRepository.GetSiteIdByCustomerId(criteria);
        }
    }
}
