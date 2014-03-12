// --------------------------------------------------------------------------
// <copyright file="KenoSettingBaseRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Repositories
{
    using System;
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Keno.Helpers;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.NAP.Framework.Exceptions;

    /// <summary>
    /// KenoSettingRepository Class.
    /// </summary>
    public class KenoSettingBaseRepository : IKenoSettingBaseRepository
    {
        private readonly IMessageHelper _messageHelper;

        public KenoSettingBaseRepository()
            : this(null)
        {
        }

        public KenoSettingBaseRepository(IMessageHelper messageHelper)
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
    }
}
