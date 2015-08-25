namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Repositories
{
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Common.Extensions;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Helpers;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.NAP.Framework.Exceptions;
    using System;

    public class ColossusBetSettingRepository : IColossusBetSettingRepository
    {

        private readonly IMessageHelper _messageHelper;

        public ColossusBetSettingRepository()
            : this(null)
        {
        }

        public ColossusBetSettingRepository(IMessageHelper messageHelper)
        {
            _messageHelper = messageHelper ?? new MessageHelper();
        }

        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Result object.</returns>
        public IList<Result> UpdateSetting(ICriteria criteria)
        {
            if (criteria == null || !criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            return ExecuteUpdateSetting(criteria);
        }

        /// <summary>
        /// Executes the update setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Result object.</returns>
        internal virtual IList<Result> ExecuteUpdateSetting(ICriteria criteria)
        {
            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                objDb.ExecuteNonQuery(criteria.GetSpParams());
                var outputParams = objDb.GetOuputParams();
                _messageHelper.SetErrorCodeAndErrorMessage(Convert.ToString(outputParams["errcode"]), Convert.ToString(outputParams["errmsg"]));
                return _messageHelper.GetErrors();
            }
        }

        /// <summary>
        /// Gets the super setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The SuperSetting object.</returns>
        public ColossusBetModel GetSuperSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var settingModel = new ColossusBetModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    settingModel.CustomerInformationPartial = ReadCustomerInformationData(reader, Roles.Super, ActionType.EditSuper);
                    settingModel.StatusPartial = reader.NextResult() ? ReadStatusPartial(reader) : null;
                    settingModel.ComissionPartial = reader.NextResult() ? ReadCommisionPartial(reader) : null;
                    settingModel.CashInSharePartial = reader.NextResult() ? ReadCashInSharePartial(reader) : null;
                }
            }

            return settingModel;
        }

        /// <summary>
        /// Gets the master setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MasterSetting object.</returns>
        public ColossusBetModel GetMasterSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var settingModel = new ColossusBetModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    settingModel.CustomerInformationPartial = ReadCustomerInformationData(reader, Roles.Super, ActionType.EditSuper);
                    settingModel.StatusPartial = reader.NextResult() ? ReadStatusPartial(reader) : null;
                    settingModel.ComissionPartial = reader.NextResult() ? ReadCommisionPartial(reader) : null;
                    settingModel.CashInSharePartial = reader.NextResult() ? ReadCashInSharePartial(reader) : null;
                }
            }

            return settingModel;
        }

        /// <summary>
        /// Gets the agent setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The AgentSetting object.</returns>
        public ColossusBetModel GetAgentSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var settingModel = new ColossusBetModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    settingModel.CustomerInformationPartial = ReadCustomerInformationData(reader, Roles.Super, ActionType.EditSuper);
                    settingModel.StatusPartial = reader.NextResult() ? ReadStatusPartial(reader) : null;
                    settingModel.ComissionPartial = reader.NextResult() ? ReadCommisionPartial(reader) : null;
                    settingModel.CashInSharePartial = reader.NextResult() ? ReadCashInSharePartial(reader) : null;
                }
            }

            return settingModel;
        }

        /// <summary>
        /// Gets the member setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The MemberSetting object.</returns>
        public ColossusBetModel GetMemberSetting(ICriteria criteria)
        {
            if (!criteria.IsValid())
            {
                throw new NapDataAccessException();
            }

            var settingModel = new ColossusBetModel();

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                using (var reader = objDb.ExecuteReader(criteria.GetSpParams()))
                {
                    settingModel.CustomerInformationPartial = ReadCustomerInformationData(reader, Roles.Super, ActionType.EditSuper);
                    settingModel.StatusPartial = reader.NextResult() ? ReadStatusPartial(reader) : null;
                    settingModel.ComissionPartial = reader.NextResult() ? ReadCommisionPartial(reader) : null;
                    settingModel.CashInSharePartial = reader.NextResult() ? ReadCashInSharePartial(reader) : null;
                }
            }

            return settingModel;
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
                customerInformation.RoleId = (int)role;
                customerInformation.ActionType = (int)actionType;
            }

            return customerInformation;
        }

        internal static Status ReadStatusPartial(IDataReader reader)
        {
            var status = new Status();
            while (reader.Read())
            {
                status.IsEnabled = reader.ToBool("MaxWinLimit");
                status.IsDownlinesEnabled = reader.ToBool("MaxWinLimit");
                status.Upline = new Status()
                {
                    IsEnabled = reader.ToBool("MaxWinLimit"),
                    IsDownlinesEnabled = reader.ToBool("MaxWinLimit")
                };
            }

            return status;
        }

        internal static CashInShare ReadCashInSharePartial(IDataReader reader)
        {
            var cashInSharePartial = new CashInShare();
            while (reader.Read())
            {
                cashInSharePartial.CashInShareValue = reader.ToDouble("MaxWinLimit");
                cashInSharePartial.MaxCashInShareValue = reader.ToDouble("IsAutoReset");
            }

            return cashInSharePartial;
        }

        internal static Comission ReadCommisionPartial(IDataReader reader)
        {
            var commisionPartial = new Comission();
            while (reader.Read())
            {
                commisionPartial.CommisionValue = reader.ToDouble("MaxWinLimit");
                commisionPartial.MaxCommisionValue = reader.ToDouble("IsAutoReset");
            }

            return commisionPartial;
        }
    }
}
