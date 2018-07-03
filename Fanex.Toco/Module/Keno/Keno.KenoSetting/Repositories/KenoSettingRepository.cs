// --------------------------------------------------------------------------
// <copyright file="KenoSettingRepository.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Repositories
{
    using Fanex.Common.Extensions;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Configurations;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Helpers;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.NAP.Framework.Exceptions;
    using System.Collections.Generic;
    using System.Data;

    /// <summary>
    /// KenoSettingRepository Class.
    /// </summary>
    public class KenoSettingRepository : KenoSettingBaseRepository, IKenoSettingRepository, IKenoSettingBaseRepository
    {
        /// <summary>
        /// Gets the super setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The SuperSetting object.</returns>
        public SuperSettingViewModel GetSuperSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var superSetting = new SuperSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    superSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Super, ActionType.EditSuper);
                    superSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    superSetting.PositionTakings = reader.NextResult() ? ReadSuperPositionTakingData(reader) : null;
                }
            }

            return superSetting;
        }

        /// <summary>
        /// Gets the master setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MasterSetting object.</returns>
        public MasterSettingViewModel GetMasterSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var masterSetting = new MasterSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    masterSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Master, ActionType.EditMaster);
                    masterSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    masterSetting.PositionTakings = reader.NextResult() ? ReadMasterPositionTakingData(reader) : null;
                }
            }

            return masterSetting;
        }

        /// <summary>
        /// Gets the agent setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The AgentSetting object.</returns>
        public AgentSettingViewModel GetAgentSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var agentSetting = new AgentSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    agentSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Agent, ActionType.EditAgent);
                    agentSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    agentSetting.PositionTakings = reader.NextResult() ? ReadAgentPositionTakingData(reader) : null;
                }
            }

            return agentSetting;
        }

        /// <summary>
        /// Gets the member setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MemberSetting object.</returns>
        public AgentEditMemberSettingViewModel GetSettingInCaseOfAgentEditMember(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var memberSetting = new AgentEditMemberSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    memberSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Member, ActionType.AgentEditMember);
                    memberSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    memberSetting.PositionTakings = reader.NextResult() ? ReadMemberPositionTakingInCaseOfAgentEditMember(reader) : null;
                }
            }

            return memberSetting;
        }

        /// <summary>
        /// Gets the member setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MemberSetting object.</returns>
        public SuperEditMemberSettingViewModel GetSettingInCaseOfSuperEditMember(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var memberSetting = new SuperEditMemberSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    memberSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Member, ActionType.SuperEditMember);
                    memberSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    memberSetting.PositionTakings = reader.NextResult() ? ReadMemberPositionTakingInCaseOfSuperEditMember(reader) : null;
                }
            }

            return memberSetting;
        }

        /// <summary>
        /// Gets the member setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MemberSetting object.</returns>
        public MasterEditMemberSettingViewModel GetSettingInCaseOfMasterEditMember(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var memberSetting = new MasterEditMemberSettingViewModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    memberSetting.CustomerInformation = ReadCustomerInformationData(reader, Roles.Member, ActionType.MasterEditMember);
                    memberSetting.BetSettings = reader.NextResult() ? ReadBetLimitData(reader) : null;
                    memberSetting.PositionTakings = reader.NextResult() ? ReadMemberPositionTakingInCaseOfMasterEditMember(reader) : null;
                }
            }

            return memberSetting;
        }

        public IList<Result> UpdateBetSetting(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        public IList<Result> UpdatePositionTaking(ICriteria criteria)
        {
            return this.UpdateSetting(criteria);
        }

        /// <summary>
        /// Reads the agent information data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="role">The role of target user.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <returns>
        /// The CustomerInformation object.
        /// </returns>
        internal static CustomerInformation ReadCustomerInformationData(IDataReader dataReader, Roles role, ActionType actionType)
        {
            CustomerInformation customerInformation = null;
            while (dataReader.Read())
            {
                customerInformation = new CustomerInformation();
                customerInformation.CustId = dataReader.HasColumn("custid") ? dataReader.ToInt("custid") : 0;
                customerInformation.Username = dataReader.HasColumn("username") ? dataReader.ToString("username") : string.Empty;
                customerInformation.FirstName = dataReader.HasColumn("firstname") ? dataReader.ToString("firstname") : string.Empty;
                customerInformation.LastName = dataReader.HasColumn("lastname") ? dataReader.ToString("lastname") : string.Empty;
                customerInformation.Phone = dataReader.HasColumn("phone") ? dataReader.ToString("phone") : string.Empty;
                customerInformation.MobilePhone = dataReader.HasColumn("mobilephone") ? dataReader.ToString("mobilephone") : string.Empty;
                customerInformation.Fax = dataReader.HasColumn("fax") ? dataReader.ToString("fax") : string.Empty;
                customerInformation.IsDisableAutoMPT = dataReader.HasColumn("DisableAutoMPT") ? dataReader.ToBool("DisableAutoMPT") : false;
                customerInformation.RoleId = (int)role;
                customerInformation.ActionType = (int)actionType;
            }

            return customerInformation;
        }

        /// <summary>
        /// Reads the bet limit data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of BetSetting.</returns>
        internal static IList<BetSetting> ReadBetLimitData(IDataReader dataReader)
        {
            var betSettings = new List<BetSetting>();
            while (dataReader.Read())
            {
                var bettingSetting = new BetSetting
                {
                    BetTypeId = dataReader.HasColumn("IBCBetTypeID") ? dataReader.ToInt("IBCBetTypeID") : 0,
                    MinBet = dataReader.HasColumn("MinBet") ? dataReader.ToDouble("MinBet") : 0,
                    MaxBet = dataReader.HasColumn("MaxBet") ? dataReader.ToDouble("MaxBet") : 0,
                    MaxPerMatch = dataReader.HasColumn("MaxPerGameType") ? dataReader.ToDouble("MaxPerGameType") : 0,
                    IsEnabled = dataReader.HasColumn("IsDisabled") ? !dataReader.ToBool("IsDisabled") : false,
                    Upline = new BetSetting()
                    {
                        MinBet = dataReader.HasColumn("MinBet_Min") ? dataReader.ToDouble("MinBet_Min") : 0,
                        MaxBet = dataReader.HasColumn("MaxBet_Max") ? dataReader.ToDouble("MaxBet_Max") : 0,
                        MaxPerMatch = dataReader.HasColumn("MaxPerMatch_Max") ? dataReader.ToDouble("MaxPerMatch_Max") : 0,
                        IsEnabled = dataReader.HasColumn("DisableUplinesKeno") ? !dataReader.ToBool("DisableUplinesKeno") : true
                    }
                };

                if (!KenoSection.Instance.ExcludeBetTypeList.Contains(bettingSetting.BetTypeId))
                {
                    betSettings.Add(bettingSetting);
                }
            }

            return betSettings;
        }

        /// <summary>
        /// Reads the super position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of SuperPositionTaking.</returns>
        internal static IList<SuperPositionTaking> ReadSuperPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<SuperPositionTaking>();
            while (dataReader.Read())
            {
                var bettingSetting = new SuperPositionTaking
                {
                    BetTypeId = dataReader.ToInt("BetType"),
                    S4 = dataReader.ToDouble("s4"),
                    N4 = dataReader.ToDouble("n4"),
                    L4 = dataReader.ToDouble("l4"),
                    SComm = dataReader.ToDouble("sdiscount"),
                    SCommMax = dataReader.ToDouble("maxsdiscount")
                };

                if (!KenoSection.Instance.ExcludeSettingBetTypeList.Contains(bettingSetting.BetTypeId) 
                    && !KenoSection.Instance.ExcludeBetTypeList.Contains(bettingSetting.BetTypeId))
                {
                    positionTakings.Add(bettingSetting);
                }
            }

            return positionTakings;
        }

        /// <summary>
        /// Reads the master position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of MasterPositionTaking.</returns>
        internal static IList<MasterPositionTaking> ReadMasterPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<MasterPositionTaking>();
            while (dataReader.Read())
            {
                var bettingSetting = new MasterPositionTaking
                {
                    BetTypeId = dataReader.ToInt("BetType"),
                    S4 = dataReader.ToDouble("s4"),
                    S3 = dataReader.ToDouble("s3"),
                    M3 = dataReader.ToDouble("m3"),
                    N3 = dataReader.ToDouble("n3"),
                    IS3 = dataReader.ToBool("is3"),
                    US3 = dataReader.ToDouble("us3"),
                    N4 = dataReader.ToDouble("n4"),
                    MComm = dataReader.ToDouble("adiscount"),
                    SComm = dataReader.ToDouble("discount_max")
                };

                if (!KenoSection.Instance.ExcludeSettingBetTypeList.Contains(bettingSetting.BetTypeId) 
                    && !KenoSection.Instance.ExcludeBetTypeList.Contains(bettingSetting.BetTypeId))
                {
                    positionTakings.Add(bettingSetting);
                }
            }

            return positionTakings;
        }

        /// <summary>
        /// Reads the agent position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of MasterPositionTaking.</returns>
        internal static IList<AgentPositionTaking> ReadAgentPositionTakingData(IDataReader dataReader)
        {
            var positionTakings = new List<AgentPositionTaking>();
            while (dataReader.Read())
            {
                var bettingSetting = new AgentPositionTaking
                {
                    BetTypeId = dataReader.ToInt("BetType"),
                    IM2 = dataReader.ToBool("i2"),
                    M2 = dataReader.ToDouble("m2"),
                    UM2 = dataReader.ToDouble("u2"),
                    M3 = dataReader.ToDouble("m3"),
                    A2 = dataReader.ToDouble("a2"),
                    N2 = dataReader.ToDouble("n2"),
                    N3 = dataReader.ToDouble("n3"),
                    AComm = dataReader.ToDouble("discount"),
                    MComm = dataReader.ToDouble("discount_max")
                };

                if (!KenoSection.Instance.ExcludeSettingBetTypeList.Contains(bettingSetting.BetTypeId)
                    && !KenoSection.Instance.ExcludeBetTypeList.Contains(bettingSetting.BetTypeId))
                {
                    positionTakings.Add(bettingSetting);
                }
            }

            return positionTakings;
        }

        /// <summary>
        /// Reads the member position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of MasterPositionTaking.</returns>
        internal static IList<AgentEditMemberPositionTaking> ReadMemberPositionTakingInCaseOfAgentEditMember(IDataReader dataReader)
        {
            var positionTakings = new List<AgentEditMemberPositionTaking>();
            while (dataReader.Read())
            {
                var bettingSetting = new AgentEditMemberPositionTaking
                {
                    BetTypeId = dataReader.ToInt("BetType"),
                    A1 = dataReader.ToDouble("a1"),
                    N2 = dataReader.ToDouble("a1_min"),
                    A2 = dataReader.ToDouble("a1_max"),
                    Comm = dataReader.ToDouble("playerdiscount"),
                    AComm = dataReader.ToDouble("playerdiscount_max")
                };

                if (!KenoSection.Instance.ExcludeSettingBetTypeList.Contains(bettingSetting.BetTypeId) 
                    && !KenoSection.Instance.ExcludeBetTypeList.Contains(bettingSetting.BetTypeId))
                {
                    positionTakings.Add(bettingSetting);
                }
            }

            return positionTakings;
        }

        /// <summary>
        /// Reads the member position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of MasterPositionTaking.</returns>
        internal static IList<SuperEditMemberPositionTaking> ReadMemberPositionTakingInCaseOfSuperEditMember(IDataReader dataReader)
        {
            var positionTakings = new List<SuperEditMemberPositionTaking>();
            while (dataReader.Read())
            {
                var bettingSetting = new SuperEditMemberPositionTaking
                {
                    BetTypeId = dataReader.ToInt("BetType"),
                    A1 = dataReader.ToDouble("a1"),
                    M1 = dataReader.ToDouble("m1"),
                    S1 = dataReader.ToDouble("s1"),
                    IS1 = dataReader.ToBool("is1"),
                    US1 = dataReader.ToDouble("us1"),
                    IS3 = dataReader.ToBool("is3"),
                    S1US1Min = dataReader.ToDouble("s1_min"),
                    S1Max = dataReader.ToDouble("s1_max"),
                    S4 = dataReader.ToDouble("us1_max")
                };

                if (!KenoSection.Instance.ExcludeSettingBetTypeList.Contains(bettingSetting.BetTypeId) 
                    && !KenoSection.Instance.ExcludeBetTypeList.Contains(bettingSetting.BetTypeId))
                {
                    positionTakings.Add(bettingSetting);
                }
            }

            return positionTakings;
        }

        /// <summary>
        /// Reads the member position taking data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <returns>The list of MasterPositionTaking.</returns>
        internal static IList<MasterEditMemberPositionTaking> ReadMemberPositionTakingInCaseOfMasterEditMember(IDataReader dataReader)
        {
            var positionTakings = new List<MasterEditMemberPositionTaking>();
            while (dataReader.Read())
            {
                var bettingSetting = new MasterEditMemberPositionTaking
                {
                    BetTypeId = dataReader.ToInt("BetType"),
                    A1 = dataReader.ToDouble("a1"),
                    M1 = dataReader.ToDouble("m1"),
                    IM2 = dataReader.ToBool("im2"),
                    IM1 = dataReader.ToBool("im1"),
                    UM1 = dataReader.ToDouble("um1"),
                    M1Max = dataReader.ToDouble("m1_max"),
                    M1UM1Min = dataReader.ToDouble("um1_min"),
                    M3 = dataReader.ToDouble("um1_max"),
                };

                if (!KenoSection.Instance.ExcludeSettingBetTypeList.Contains(bettingSetting.BetTypeId) 
                    && !KenoSection.Instance.ExcludeBetTypeList.Contains(bettingSetting.BetTypeId))
                {
                    positionTakings.Add(bettingSetting);
                }
            }

            return positionTakings;
        }
    }
}
