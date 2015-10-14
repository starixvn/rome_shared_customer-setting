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
    using CasinoLibrary.SBAgent;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Helpers;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Fanex.CustomerSetting.Module.Core.Web;
    using Fanex.Data;
    using Fanex.Data.DNAide;
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
            
            if (string.IsNullOrWhiteSpace(storedProcedure.Key))
            {
                model.Entities.Add(new EnableStatusEntity
                {
                    BetTypeId = 0,
                    Disabled = !context.Enable,
                    DisabledDownlines = !context.EnableDownlines,
                    DisabledUplines = !context.EnableUplines,
                    Synced = context.Synced
                });

                return model;
            }

            model.ReadOnly = this.IsCrossingLevel(context);

            using (var objectDb = new ObjectDb(storedProcedure.Key))
            {
                var param = new Hashtable();

                foreach (ParamElementConfiguration item in storedProcedure.ParamCollection)
                {
                    param.Add(item.Name.ToLower(), Helper.GetParamValue(item.MapProperty, context, model, item.Reverse, item.Default));
                }

                try
                {
                    objectDb.AssignParamValues(param);
                    var entity = new EnableStatusEntity();
                    using (var reader = objectDb.ExecuteReader())
                    {
                        var hasRecord = false;
                            
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                hasRecord = true;
                                entity = new EnableStatusEntity();

                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    foreach (ResultElementConfiguration paramResult in storedProcedure.ResultCollection)
                                    {
                                        if (reader.GetName(i).Equals(paramResult.Name, StringComparison.OrdinalIgnoreCase))
                                        {
                                            entity = Helper.MapResultValue(paramResult.MapProperty, reader[i], entity, paramResult.Reverse, paramResult.Default);
                                            break;
                                        }
                                    }
                                }

                                model.Entities.Add(entity);
                            }

                            reader.Close();
                        }

                        var outputValues = objectDb.GetOuputParams();

                        if (!hasRecord)
                        {
                            foreach (var key in outputValues.Keys)
                            {
                                foreach (ResultElementConfiguration paramResult in storedProcedure.ResultCollection)
                                {
                                    if (key.ToString().Equals(paramResult.Name, StringComparison.OrdinalIgnoreCase))
                                    {
                                        entity = Helper.MapResultValue(paramResult.MapProperty, outputValues[key], entity, paramResult.Reverse, paramResult.Default);
                                        break;
                                    }
                                }                                
                            }

                            entity.BetTypeId = -1;
                            model.Entities.Add(entity);
                        }

                        if (outputValues != null)
                        {
                            model.RequestContext = Helper.MapResultValue("HasDownline", outputValues["hasdownline"] == null ? true : outputValues["hasdownline"], model.RequestContext);
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
            if (this.IsCrossingLevel(context))
            {
                return new Result(1, "You are not authorized on this function");
            }

            var result = new Result();
            
            using (var objectDb = new ObjectDb(storedProcedure.Key))
            {
                var param = new Hashtable();

                foreach (ParamElementConfiguration item in storedProcedure.ParamCollection)
                {
                    param.Add(item.Name.ToLower(), Helper.GetParamValue(item.MapProperty, entity, context, item.Reverse, item.Default));
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

                    if (result.IsSuccess && context.IsCasino)
                    {
                        var customer = new Customer();
                        try
                        {
                            customer.UpdateCustEnableCasino(context.CustId, !(bool)entity.Disabled, !(bool)entity.DisabledDownlines, context.Ip, context.UserId, AppSession.CurrentSession.UserName, AppSession.CurrentSession.FromSystem);
                        }
                        catch
                        {                            
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

        public Result UpdateProductStatus(SpElementConfiguration storedProcedure, RequestContext context, IList<EnableStatusEntity> entities)
        {
            if (this.IsCrossingLevel(context))
            {
                return new Result(1, "You are not authorized on this function");
            }

            var result = new Result();

            using (var objectDb = new ObjectDb(storedProcedure.Key))
            {
                var param = new Hashtable();

                foreach (ParamElementConfiguration item in storedProcedure.ParamCollection)
                {
                    if (item.Name.Equals("IsDisabled", StringComparison.OrdinalIgnoreCase))
                    {
                        var xmlFormat = string.Empty;
                        var products = string.Empty;
                        var root = "<ROOT>{0}</ROOT>";
                        var product = "<ProductStatus " +
                                        "SportType=\"{0}\" " +
                                        "BetType=\"{1}\" " +
                                        "IsDisabled=\"{2}\" " +
                                        "IsDisabledDownline=\"{3}\" />";

                        foreach (var entity in entities)
                        {
                            products += string.Format(product, entity.SportTypeId, entity.BetTypeId, (bool)entity.Disabled ? 1 : 0, (bool)entity.DisabledDownlines ? 1 : 0);
                        }

                        xmlFormat = string.Format(root, products);
                        param.Add(item.Name.ToLower(), xmlFormat);
                    }
                    else
                    {
                        param.Add(item.Name.ToLower(), Helper.GetParamValue(item.MapProperty, entities[0], context, item.Reverse, item.Default));
                    }                    
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

        private bool IsCrossingLevel(RequestContext context)
        {
            if (context.UplineId == 0)
            {
                return false;
            }

            var param = new Hashtable();
            param.Add("custid", context.CustId);

            DbResult currentCustomer = DbHelper.ExecuteDbResult("Age_CustomerSelUserInfo", param);

            if (currentCustomer == null ||
                currentCustomer.ResultSet == null ||
                currentCustomer.ResultSet.Tables == null ||
                currentCustomer.ResultSet.Tables.Count == 0 ||
                currentCustomer.ResultSet.Tables[0] == null ||
                currentCustomer.ResultSet.Tables[0].Rows == null ||
                currentCustomer.ResultSet.Tables[0].Rows.Count == 0)
            {
                return true;
            }

            var row = currentCustomer.ResultSet.Tables[0].Rows[0];
            var roleId = Utils.ToInt32(row["roleid"]);
            var parentRoleId =
                context.UplineId == Utils.ToInt32(row["superid"]) ? 4 : 
                context.UplineId == Utils.ToInt32(row["masterid"]) ? 3 : 
                context.UplineId == Utils.ToInt32(row["agentid"]) ? 2 : 5;

            return parentRoleId - roleId != 1;
        }
    }
}
