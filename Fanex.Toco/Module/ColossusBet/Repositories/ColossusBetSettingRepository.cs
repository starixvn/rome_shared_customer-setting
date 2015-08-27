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
        public ColossusBetModel GetSetting(ICriteria criteria, int custId, int roleId)
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
                    settingModel = ReadCustomerInformationData(reader, custId, roleId);
                    settingModel.ComissionPartial = reader.NextResult() ? ReadCommisionPartial(reader) : null;
                    settingModel.CashInSharePartial = reader.NextResult() ? ReadCashInSharePartial(reader) : null;
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
        internal static ColossusBetModel ReadCustomerInformationData(IDataReader dataReader, int custId, int roleId)
        {
            ColossusBetModel model = new ColossusBetModel();
            while (dataReader.Read())
            {
                model.CustomerInformationPartial = new CustomerInformation()
                {
                    CustId = custId,
                    Username = dataReader.To<string>("username"),
                    FirstName = dataReader.To<string>("firstname"),
                    LastName = dataReader.To<string>("lastname"),
                    NickName = dataReader.To<string>("nickname"),
                    Phone = dataReader.To<string>("phone"),
                    MobilePhone = dataReader.To<string>("mobilephone"),
                    Fax = dataReader.To<string>("fax"),
                    RoleId = roleId
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

        internal static CashInShare ReadCashInSharePartial(IDataReader reader)
        {
            while (reader.Read())
            {
                return new CashInShare()
                {
                    CashInShareValue = reader.To<double>("v204discount_18002"),
                    MaxCashInShareValue = reader.To<double>("v204discount_18002_max")
                };
            }

            return new CashInShare();
        }

        internal static Comission ReadCommisionPartial(IDataReader reader)
        {
            while (reader.Read())
            {
                return new Comission()
                {
                    CommisionValue = reader.To<double>("v204discount_18001"),
                    MaxCommisionValue = reader.To<double>("v204discount_18001_max")
                };           
            }

            return new Comission();
        }
    }
}
