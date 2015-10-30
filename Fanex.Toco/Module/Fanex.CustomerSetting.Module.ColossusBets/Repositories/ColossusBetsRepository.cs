namespace Fanex.CustomerSetting.Module.ColossusBets.Repositories
{
    using System.Collections.Generic;
    using Fanex.BetList.Core.Entities;
    using Fanex.Common;
    using Fanex.CustomerSetting.Module.ColossusBets.Criteria;
    using Fanex.Data;

    public class ColossusBetsRepository : IColossusBetsRepository
    {
        public IEnumerable<ITicket> BetDetails(GetColossusBetsBetDetailsCriteria criteria)
        {
            var result = new List<ITicket>();

            using (ObjectDb objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(new Ticket
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

            return result;
        }
    }
}
