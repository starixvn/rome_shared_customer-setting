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
        private ColossusBetModel modelFake = new ColossusBetModel()
        {
            CustomerInformationPartial = new CustomerInformation()
            {
                Username = "master1",
                NickName = "bear",
                FirstName = "Tran",
                LastName = "Vu",
                Phone = "096414444",
                MobilePhone = "1234543463",
                Fax = "45645646512",
                RoleId = 4
            },
            StatusPartial = new Status()
            {
                IsEnabled = false,
                IsHasDownlines = true,
                IsDownlinesEnabled = false,
                IsStatusUpdated = false,
                Upline = new Status
                {
                    IsEnabled = true,
                    IsDownlinesEnabled = true
                }
            },
            ComissionPartial = new Comission()
            {
                CommisionValue = 6.75,
                MaxCommisionValue = 6.75,
                IsCommisionUpdated = false
            },
            CashInSharePartial = new CashInShare()
            {
                CashInShareValue = 14.5,
                MaxCashInShareValue = 22.5,
                IsCashInShareUpdated = false
            }
        };


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
        public ColossusBetModel GetSuperSetting(ICriteria criteria)
        {
            return modelFake;

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
        internal static CustomerInformation ReadCustomerInformationData(IDataReader dataReader, Roles role, ActionType actionType)
        {
            CustomerInformation customerInformation = null;
            while (dataReader.Read())
            {
                customerInformation = new CustomerInformation();
                customerInformation.CustId = dataReader.HasColumn("custid") ? dataReader.ToInt("custid") : 0;
                customerInformation.Username = dataReader.ToString("username");
                customerInformation.FirstName = dataReader.ToString("firstname");
                customerInformation.LastName = dataReader.ToString("lastname");
                customerInformation.LastName = dataReader.ToString("nickname");
                customerInformation.Phone = dataReader.ToString("phone");
                customerInformation.MobilePhone = dataReader.ToString("mobilephone");
                customerInformation.Fax = dataReader.ToString("fax");
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
                status.IsEnabled = !reader.ToBool("DisableColossusBet");
                status.IsDownlinesEnabled =!reader.ToBool("DisableDownlines");
                status.IsHasDownlines = reader.ToBool("Downlines");
                status.Upline = new Status()
                {
                    IsEnabled = !reader.ToBool("DisableUpline"),
                };
            }

            return status;
        }

        internal static CashInShare ReadCashInSharePartial(IDataReader reader)
        {
            var cashInSharePartial = new CashInShare();
            while (reader.Read())
            {
                cashInSharePartial.CashInShareValue = reader.ToDouble("v204sdiscount_18001");
                cashInSharePartial.MaxCashInShareValue = reader.ToDouble("v204sdicount_18001_max");
            }

            return cashInSharePartial;
        }

        internal static Comission ReadCommisionPartial(IDataReader reader)
        {
            var commisionPartial = new Comission();
            while (reader.Read())
            {
                commisionPartial.CommisionValue = reader.ToDouble("v204sdiscount_18002");
                commisionPartial.MaxCommisionValue = reader.ToDouble("v204sdicount_18002_max");
            }

            return commisionPartial;
        }
    }
}
