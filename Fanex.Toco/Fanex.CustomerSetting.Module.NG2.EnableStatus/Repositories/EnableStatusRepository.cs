namespace Fanex.CustomerSetting.Module.NG2.EnableStatus.Repositories
{
    using CasinoLibrary.SBAgent;
    using Fanex.CustomerSetting.Module.NG2.Core;
    using Fanex.CustomerSetting.Module.NG2.Core.Models;
    using Fanex.CustomerSetting.Module.NG2.Core.Web;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Entities;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Helpers;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Mappings;
    using Fanex.CustomerSetting.Module.NG2.EnableStatus.Models;
    using Fanex.Data;
    using Fanex.Data.DNAide;
    using Fanex.Module.Logs;
    using Fanex.Module.Logs.Services;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;

    public class EnableStatusRepository : IEnableStatusRepository
    {
        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
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

            var currentCustInfo = this.GetCurrentCustomerInfo(context.CustId);
            var isCrossLevel = this.IsCrossingLevel(context, currentCustInfo);
            var product = Helper.Products.Where(p => p.Id.Equals(context.ProductId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            model.ReadOnly = isCrossLevel;
            model.Warning = Helper.GetResource(product.Warning);
            if (product.Id.Equals("voidbridge", StringComparison.CurrentCultureIgnoreCase))
            {
                var isSyncedAllDownlineVoidbridge = this.IsSyncedAllDownlineVoidbridge(currentCustInfo);
                model.ReadOnly = model.ReadOnly || !isSyncedAllDownlineVoidbridge;
                if (isSyncedAllDownlineVoidbridge)
                {
                    model.Warning = null;
                }
            }
            else if (product.Id.Equals("allbet", StringComparison.CurrentCultureIgnoreCase))
            {
                var isSyncedAllDownlineAllbet = this.IsSyncedAllDownlineAllbet(currentCustInfo);
                model.ReadOnly = model.ReadOnly || !isSyncedAllDownlineAllbet;
                if (isSyncedAllDownlineAllbet)
                {
                    model.Warning = null;
                }
            }

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

                        if (!hasRecord && outputValues != null)
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
                    ex.Write();

                    throw ex;
                }                
            }
        }

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public Result UpdateProductStatus(SpElementConfiguration storedProcedure, RequestContext context, EnableStatusEntity entity)
        {
            if (this.IsCrossingLevel(context, this.GetCurrentCustomerInfo(context.CustId)))
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
                    foreach (ResultElementConfiguration resultElement in storedProcedure.ResultCollection)
                    {
                        foreach (var outputParam in outputParams.Keys)
                        {
                            if (outputParam.ToString().Equals(resultElement.Name, StringComparison.OrdinalIgnoreCase))
                            {
                                result = Helper.MapResultValue(resultElement.MapProperty, outputParams[outputParam], result);
                            }
                        }   
                    }                    

                    if (result.IsSuccess && context.IsCasino)
                    {
                        var customer = new Customer();
                        try
                        {
                            customer.UpdateCustEnableCasino(
                                context.CustId, 
                                !(bool)entity.Disabled, 
                                !(bool)entity.DisabledDownlines, 
                                context.Ip, 
                                context.UserId, 
                                AppSession.CurrentSession.UserName, 
                                AppSession.CurrentSession.FromSystem);
                        }
                        catch (Exception casinoEx)
                        {
                            casinoEx.Write(Formater.LogType.Warning);
                        }         
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    ex.Write();

                    throw ex;
                }
            }
        }

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public Result UpdateProductStatus(SpElementConfiguration storedProcedure, RequestContext context, IList<EnableStatusEntity> entities)
        {
            if (this.IsCrossingLevel(context, this.GetCurrentCustomerInfo(context.CustId)))
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
                            if (!entity.Hide && entity.IsDirty)
                            {
                                products += string.Format(
                                    product, 
                                    entity.SportTypeId, 
                                    entity.BetTypeId, 
                                    (bool)entity.Disabled ? 1 : 0, 
                                    (bool)entity.DisabledDownlines && context.Level != EnableStatusEnum.Level.Member ? 1 : 0);
                            }
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
                    foreach (ResultElementConfiguration resultElement in storedProcedure.ResultCollection)
                    {
                        foreach (var outputParam in outputParams.Keys)
                        {
                            if (outputParam.ToString().Equals(resultElement.Name, StringComparison.OrdinalIgnoreCase))
                            {
                                result = Helper.MapResultValue(resultElement.MapProperty, outputParams[outputParam], result);
                            }
                        }
                    }    

                    if (result.IsSuccess && context.IsCasino)
                    {
                        var customer = new Customer();
                        try
                        {
                            customer.UpdateCustEnableCasino(
                                context.CustId, 
                                !(bool)entities[0].Disabled, 
                                !(bool)entities[0].DisabledDownlines, 
                                context.Ip, 
                                context.UserId, 
                                AppSession.CurrentSession.UserName, 
                                AppSession.CurrentSession.FromSystem);
                        }
                        catch (Exception casinoEx)
                        {
                            casinoEx.Write(Formater.LogType.Warning);
                        }
                    }

                    return result;
                }
                catch (Exception ex)
                {
                    ex.Write();

                    throw ex;
                }
            }
        }

        private bool IsCrossingLevel(RequestContext context, DbResult currentCustomer)
        {
            try
            {
                if (context.UplineId == 0)
                {
                    return false;
                }

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
            catch (Exception ex)
            {
                ex.Write();

                return true;
            }
        }

        private DbResult GetCurrentCustomerInfo(int custId)
        {
            var param = new Hashtable();
            param.Add("custid", custId);

            return DbHelper.ExecuteDbResult("Age_CustomerSelUserInfo", param);
        }

        private bool IsSyncedAllDownlineVoidbridge(DbResult currentCustomer)
        {
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

            return Utils.ToBoolean(row["SyncedVoidBridge"]) && Utils.ToBoolean(row["SyncedVoidBridgeDownline"]);
        }

        private bool IsSyncedAllDownlineAllbet(DbResult currentCustomer)
        {
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

            return Utils.ToBoolean(row["SyncedAllbet"]) && Utils.ToBoolean(row["SyncedAllbetDownline"]);
        }
    }
}
