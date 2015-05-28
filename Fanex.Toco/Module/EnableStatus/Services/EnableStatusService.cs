// --------------------------------------------------------------------------
// <copyright file="EnableStatusService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Services
{
    using System;
    using System.Collections.Generic;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Repositories;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnableStatusService : IEnableStatusService
    {
        private IEnableStatusRepository data = new EnableStatusRepository();

        public EnableStatusModel GetProductStatus(SpElementConfiguration storedProcedure)
        {
            try
            {
                var model = data.GetProductStatus(storedProcedure);

                return model;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
        
        public IList<Result> UpdateProductStatus(EnableStatusModel model, SpElementConfiguration storedProcedure)
        {
            try
            {
                var results = new List<Result>();

                foreach (var item in model.Entities)
                {
                    var result = data.UpdateProductStatus(storedProcedure);
                    results.Add(result);
                }

                return results;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
