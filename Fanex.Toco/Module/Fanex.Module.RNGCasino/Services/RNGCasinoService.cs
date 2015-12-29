namespace Fanex.Module.RNGCasino.Services
{
    using System.Collections.Generic;
    using Criteria;
    using CustomerSetting.Module.Core.Services.Impl;
    using Extensions;
    using Fanex.BetList.Core.Builder;
    using Fanex.BetList.Core.Entities;
    using Fanex.BetList.Core.Factories;
    using Fanex.BetList.Core.TicketHelper;
    using Helpers;
    using Models;

    public class RNGCasinoService : ServiceBase, IRNGCasinoService
    {
        public IEnumerable<TicketDetail> BetDetails(GetRNGCasinoBetDetailsCriteria criteria)
        {
            var rawData = GetListData<Ticket>(criteria);
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
    }
}
