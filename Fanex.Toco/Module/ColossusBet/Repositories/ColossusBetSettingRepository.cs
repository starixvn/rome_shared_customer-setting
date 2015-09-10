namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using Fanex.Common.Extensions;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Helpers;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.NAP.Framework.Exceptions;

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
        /// Gets the super setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The SuperSetting object.</returns>
        public ColossusBetModel GetSetting(ICriteria criteria, SettingContext settingContext)
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
                    settingModel = ReadCustomerInformationData(reader, settingContext);
                    settingModel.ComissionPartial = reader.NextResult() ? ReadCommisionPartial(reader, settingContext) : null;
                    settingModel.CashInSharePartial = reader.NextResult() ? ReadCashInSharePartial(reader, settingContext) : null;
                }
            }

            return settingModel;
        }

        /// <summary>
        /// Updates the setting.
        /// </summary>
        /// <param name="criteria">The criteria.</param>
        /// <returns>The Result object.</returns>
        public IList<Result> UpdateSetting(ICriteria criteria)
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
        /// Reads the agent information data.
        /// </summary>
        /// <param name="dataReader">The data reader.</param>
        /// <param name="role">The role of target user.</param>
        /// <param name="actionType">Type of the action.</param>
        /// <returns>
        /// The CustomerInformation object.
        /// </returns>
        internal static ColossusBetModel ReadCustomerInformationData(IDataReader dataReader, SettingContext settingContext)
        {
            ColossusBetModel model = new ColossusBetModel();
            while (dataReader.Read())
            {
                model.CustomerInformationPartial = new CustomerInformation()
                {
                    CustId = settingContext.CustId,
                    Username = dataReader.To<string>("username"),
                    FirstName = dataReader.To<string>("firstname"),
                    LastName = dataReader.To<string>("lastname"),
                    NickName = dataReader.To<string>("nickname"),
                    Phone = dataReader.To<string>("phone"),
                    MobilePhone = dataReader.To<string>("mobilephone"),
                    Fax = dataReader.To<string>("fax"),
                    RoleId = settingContext.RoleId,
                    UserId = settingContext.UserId
                };

                model.StatusPartial = new Status()
                {
                    IsEnabled = !dataReader.To<bool>("DisableColossusBet"),
                    IsDownlinesEnabled = !dataReader.To<bool>("DisableDownlines"),
                    IsHasDownlines = dataReader.To<bool>("Downlines"),
                    Upline = new Status()
                    {
                        IsEnabled = !dataReader.To<bool>("DisableUpline"),
                    }
                };
            }

            return model;
        }

        internal static CashInShare ReadCashInSharePartial(IDataReader reader, SettingContext settingContext)
        {
            while (reader.Read())
            {
                return new CashInShare()
                {
                    CashInShareValue = reader.To<decimal>("v204discount_18002") * 100,
                    MaxCashInShareValue = settingContext.RoleId.Equals(1) ? 0 : reader.To<decimal>("v204discount_18002_max") * 100
                };
            }

            return new CashInShare();
        }

        internal static Comission ReadCommisionPartial(IDataReader reader, SettingContext settingContext)
        {
            while (reader.Read())
            {
                return new Comission()
                {
                    CommisionValue = reader.To<decimal>("v204discount_18001") * 100,
                    MaxCommisionValue = settingContext.RoleId.Equals(1) ? 0 : reader.To<decimal>("v204discount_18001_max") * 100
                };
            }

            return new Comission();
        }
    }
}