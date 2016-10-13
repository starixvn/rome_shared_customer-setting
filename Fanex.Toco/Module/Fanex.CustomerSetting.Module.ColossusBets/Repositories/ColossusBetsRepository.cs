using System.Collections.Generic;
using Fanex.BetList.Core.Entities;
using Fanex.Common;
using Fanex.Data;
using Fanex.NAP.Framework.Data;

namespace Fanex.CustomerSetting.Module.ColossusBets.Repositories
{
    public class ColossusBetsRepository : IColossusBetsRepository
    {
        public IEnumerable<ITicket> BetDetails(ICriteria criteria)
        {
            IList<ITicket> tickets = new List<ITicket>();

            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = db.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        tickets.Add(new Ticket
                        {
                            TransId = Converter.ToInt(reader["transid"]),
                            TransDate = Converter.ToDateTime(reader["transdate"]),
                            BetTypeId = Converter.ToInt(reader["bettype"]),
                            StatusId = Converter.ToInt(reader["statusid"]),
                            Status = Converter.ToString2(reader["status"]),

                            Handicap1 = Converter.ToDecimal(reader["Sold"]),
                            Winlost = Converter.ToDecimal(reader["WinLost"]),

                            SuperComm = Converter.ToDecimal(reader["scomm"]),
                            SuperDiscount = Converter.ToDecimal(reader["sdiscount"]),
                            AgentComm = Converter.ToDecimal(reader["acomm"]),
                            MasterDiscount = Converter.ToDecimal(reader["adiscount"]),
                            Comm = Converter.ToDecimal(reader["comm"]),
                            AgentDiscount = Converter.ToDecimal(reader["discount"]),
                            OddsSpread = Converter.ToDecimal(reader["OddsSpread"])
                        });
                    }
                }
            }

            return tickets;
        }

        public string GetSiteIdByCustomerId(ICriteria criteria)
        {
            string siteId = string.Empty;

            using (ObjectDb db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteScalar(criteria.GetSpParams());
                var outputParams = db.GetOuputParams();

                if (outputParams != null && outputParams.ContainsKey("siteid"))
                {
                    siteId = outputParams["siteid"].ToString();
                }
            }

            return siteId;
        }
    }
}
