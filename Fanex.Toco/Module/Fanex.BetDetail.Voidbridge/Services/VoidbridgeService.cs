namespace Fanex.Voidbridge.BetDetail.Services
{
    using System;
    using System.Collections.Generic;
    using Criteria;
    using Extensions;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Helpers;
    using Models;
    using Models.HandHistory;
    using Repositories;
    using Result.Core.Voidbridge;

    public class VoidbridgeService : IVoidbridgeService
    {
        private readonly IVoidbridgeRepository _rngCasinoRepository;

        public VoidbridgeService(IVoidbridgeRepository rngCasinoRepository = null)
        {
            _rngCasinoRepository = rngCasinoRepository ?? new VoidbridgeRepository();
        }

        public IEnumerable<TicketDetail> BetDetails(GetVoidbridgeBetDetailsCriteria criteria)
        {
            var detailsTickets = new List<TicketDetail>();

            var rawData = _rngCasinoRepository.BetDetails(criteria);

            if (criteria.TotalRows > 0)
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
                    ticket.CustId = criteria.CustId;
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

            try
            {
                var voidbridgeApi = new VoidbridgeApiImp();

                result.Iframe = voidbridgeApi.GetBetHistoryUrl(criteria.CustId.ToString(), criteria.BetCheck);
            }
            catch (Exception ex)
            {
                result.ActionInfo = new ResultDetail
                {
                    ResultCode = 1,
                    ResultMessage = ex.Message
                };
            }

            return result;
        }
    }
}
