namespace Fanex.Module.RNGCasino.Repositories
{
    using BetList.Core.Entities;
    using Common;
    using Criteria;
    using Data;
    using System;
    using System.Collections.Generic;

    [Obsolete]
    public class RNGCasinoRepository : IRNGCasinoRepository
    {
        public IEnumerable<Ticket> BetDetails(GetRNGCasinoBetDetailsCriteria criteria)
        {
            var result = new List<Ticket>();

            using (ObjectDb objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    while (reader.Read())
                    {
                        result.Add(new Ticket
                        {
                            TransId = Converter.To<long>(reader["transid"]),
                            SportTypeId = Converter.ToInt(reader["SportType"]),
                            BetTypeId = Converter.ToInt(reader["BetType"]),
                            TransDate = Converter.ToDateTime(reader["transdate"]),
                            WinlostDate = Converter.ToDateTime(reader["winlostdate"]),

                            Status = Converter.ToString2(reader["status"]),
                            StatusId = Converter.ToInt(reader["statusid"]),

                            Odds = Converter.ToDecimal(reader["odds"]),
                            Currency = Converter.ToString2(reader["currency"]),
                            Stake = Converter.ToDecimal(reader["stake"]),
                            ActualStake = Converter.ToDecimal(reader["actual_stake"]),
                            Winlost = Converter.ToDecimal(reader["WinLost"]),
                            IP = Converter.ToString2(reader["ip"]),
                            TransDesc = Converter.ToString2(reader["transdesc"]),

                            PlayerComm = Converter.ToDecimal(reader["commission"]),
                            AgentWinlost = Converter.ToDecimal(reader["awinlost"]),
                            AgentPositionTaking = Converter.ToDecimal(reader["apositiontaking"]),
                            Comm = Converter.ToDecimal(reader["comm"]),
                            AgentDiscount = Converter.ToDecimal(reader["discount"]),

                            MasterWinlost = Converter.ToDecimal(reader["mwinlost"]),
                            MasterPositionTaking = Converter.ToDecimal(reader["mpositiontaking"]),
                            AgentComm = Converter.ToDecimal(reader["acomm"]),
                            MasterDiscount = Converter.ToDecimal(reader["adiscount"]),

                            SuperWinlost = Converter.ToDecimal(reader["swinlost"]),
                            SuperPositionTaking = Converter.ToDecimal(reader["spositiontaking"]),
                            SuperComm = Converter.ToDecimal(reader["scomm"]),
                            SuperDiscount = Converter.ToDecimal(reader["sdiscount"]),
                            BetCheck = Converter.ToString2(reader["BetCheck"]),

                            MatchCode = Converter.ToString2(reader["matchcode"])
                        });
                    }
                }
            }

            return result;
        }
    }
}
