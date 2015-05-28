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
        public EnableStatusModel GetProductStatus(SpElementConfiguration storedProcedure)
        {
            var model = new EnableStatusModel();
            model.Entities = new List<EnableStatusEntity>();
            
            if (storedProcedure.Key == string.Empty)
            {
                model.Entities.Add(new EnableStatusEntity
                {
                    Upline = new EnableStatusRecord
                    {
                        Text = Helper.GetResource("enable")
                    },
                    Downline = new EnableStatusRecord
                    {
                        Text = Helper.GetResource("enableDownline")
                    }
                });

                return model;
            }

            using (var objectDb = new ObjectDb(storedProcedure.Key))
            {
                var param = new Hashtable();

                foreach (ParamElementConfiguration item in storedProcedure.ParamCollection)
                {
                    param.Add(item.Name.ToLower(), item.Value);
                }

                try
                {
                    objectDb.AssignParamValues(param);

                    using (var reader = objectDb.ExecuteReader())
                    {
                        while (reader != null && reader.Read())
                        {
                            model.Entities.Add(new EnableStatusEntity
                            {
                                Upline = new EnableStatusRecord
                                {                                    
                                },
                                Downline = new EnableStatusRecord
                                {
                                }
                            });
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

        public Result UpdateProductStatus(SpElementConfiguration storedProcedure)
        {
            var result = new Result();

            using (var objectDb = new ObjectDb(storedProcedure.Key))
            {
                var param = new Hashtable();

                foreach (ParamElementConfiguration item in storedProcedure.ParamCollection)
                {
                    param.Add(item.Name.ToLower(), item.Value);
                }

                try
                {
                    objectDb.AssignParamValues(param);

                    using (var reader = objectDb.ExecuteReader())
                    {
                        while (reader != null && reader.Read())
                        {
                            result.ErrorCode = (int)reader["errcode"];
                            result.ErrorMessage = (string)reader["errmsg"];
                        }
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
