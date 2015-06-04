// --------------------------------------------------------------------------
// <copyright file="EnableStatusRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Helpers;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.Data;
    using Fanex.Logging;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnableStatusRepository : IEnableStatusRepository
    {
        public EnableStatusModel GetProductStatus(SpElementConfiguration storedProcedure, RequestContext context)
        {
            var model = new EnableStatusModel();
            model.RequestContext = context;
            model.Entities = new List<EnableStatusEntity>();
            
            if (storedProcedure.Key == string.Empty)
            {
                model.Entities.Add(new EnableStatusEntity
                {
                    BetTypeId = 0,
                    Disabled = !context.Enable,
                    DisabledDownlines = !context.EnableDownlines,
                    DisabledUplines = !context.EnableUplines,
                    DisabledText = Helper.GetResource("enable"),
                    DisabledDownlinesText = Helper.GetResource("enableDownline")
                });

                return model;
            }

            using (var objectDb = new ObjectDb(storedProcedure.Key))
            {
                var param = new Hashtable();

                foreach (ParamElementConfiguration item in storedProcedure.ParamCollection)
                {
                    param.Add(item.Name.ToLower(), Helper.GetParamValue(item.MapProperty, context, model, item.Reverse));
                }

                try
                {
                    objectDb.AssignParamValues(param);

                    using (var reader = objectDb.ExecuteReader())
                    {                        
                        while (reader != null && reader.Read())
                        {
                            var entity = new EnableStatusEntity();

                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                foreach (ResultElementConfiguration paramResult in storedProcedure.ResultCollection)
                                {
                                    if (reader.GetName(i).Equals(paramResult.Name, StringComparison.OrdinalIgnoreCase))
                                    {
                                        entity = Helper.MapResultValue(paramResult.MapProperty, reader[i], entity);
                                        break;
                                    }
                                }                                
                            }

                            model.Entities.Add(entity);
                        }
                    }                    

                    return model;
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);

                    throw ex;
                }                
            }
        }

        public Result UpdateProductStatus(SpElementConfiguration storedProcedure, RequestContext context, EnableStatusEntity entity)
        {
            var result = new Result();

            using (var objectDb = new ObjectDb(storedProcedure.Key))
            {
                var param = new Hashtable();

                foreach (ParamElementConfiguration item in storedProcedure.ParamCollection)
                {
                    param.Add(item.Name.ToLower(), Helper.GetParamValue(item.MapProperty, entity, context, item.Reverse));
                }

                try
                {
                    objectDb.AssignParamValues(param);
                    objectDb.ExecuteNonQuery();                    

                    var outputParams = objectDb.GetOuputParams();
                    foreach (var outputParam in outputParams.Keys)
                    {
                        result = Helper.MapResultValue(outputParam.ToString(), outputParams[outputParam], result);
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    Logger.Log.Error(ex.Message, ex);

                    throw ex;
                }
            }
        }
    }
}
