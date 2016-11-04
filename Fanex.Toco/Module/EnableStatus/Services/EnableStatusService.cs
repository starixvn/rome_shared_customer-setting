// --------------------------------------------------------------------------
// <copyright file="EnableStatusService.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2015.5.27">Created</change>
// </history>

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Xml.Linq;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Helpers;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Repositories;
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Models;
    using Module.Core.Services.Impl;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnableStatusService : IEnableStatusService
    {
        private IEnableStatusRepository _data = new EnableStatusRepository();

        [SuppressMessage("StyleCop.CSharp.StylecopPlus.Rules", "SP2101:MethodMustNotContainMoreLinesThan", Justification = "Reviewed. Suppression is OK here.")]
        public EnableStatusModel GetProductStatus(SpElementConfiguration storedProcedure, RequestContext context)
        {
            try
            {
                var model = _data.GetProductStatus(storedProcedure, context);
                var product = Helper.Products.Where(p => p.Id.Equals(context.ProductId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                model.RequestContext.HasDownline = model.Entities.FirstOrDefault().HasDownline ? true : model.RequestContext.HasDownline;
                context.NewMode = product.NewMode;
                model.IframeId = Helper.Products.IframeId;
                model.HasCheckAll = product.HasCheckAll;
                model.ReloadAfterUpdate = product.ReloadAfterUpdate;
                model.ClosePopupFunction = product.ClosePopupFunction;
                model.Username = model.Entities.FirstOrDefault().Username == null ?
                    (context.Username == null ? string.Empty : context.Username) : model.Entities.FirstOrDefault().Username.ToString();
                model.RequestContext.Username = model.Username;

                foreach (var item in model.Entities)
                {
                    if (item.BetTypeId != null && product.HideBetTypes.Contains(item.BetTypeId.ToString()))
                    {
                        item.Hide = true;
                    }

                    if (item.BetTypeId != null && !product.HideBetTypes.Contains(item.BetTypeId.ToString())
                        && context.Level == EnableStatusEnum.Level.Super)
                    {
                        item.Hide = false;
                    }

                    if (context.NewMode)
                    {
                        if (model.Entities.Count > 1)
                        {
                            item.EnableText = item.BetTypeName = CacheRef.GetBettype(item.BetTypeId.ToString());
                            item.SportTypeName = CacheRef.GetSportName(item.SportTypeId.ToString());
                        }
                        else
                        {
                            if (product.GetFromRemotingRef)
                            {
                                item.EnableText = item.SportTypeName = item.BetTypeName = CacheRef.GetRefResourceByRefIdAndResourceId("Product", context.Product.ToString(), context.Language, string.Empty);
                            }
                            else
                            {
                                item.EnableText = item.SportTypeName = item.BetTypeName = CacheRef.GetSportName(item.SportTypeId.ToString());
                            }
                        }                        
                    }
                    else if (item.BetTypeId != null && Utils.ToInt32(item.BetTypeId) > 0 && (item.SportTypeId == null || Utils.ToInt32(item.SportTypeId) == 0))
                    {
                        item.EnableText = item.BetTypeName = CacheRef.GetBettype(item.BetTypeId.ToString());
                    }
                    else if (item.BetTypeId != null && item.SportTypeId != null && Utils.ToInt32(item.BetTypeId) > 0 && Utils.ToInt32(item.SportTypeId) > 0)
                    {
                        item.EnableText = item.BetTypeName = Helper.GetResource("b" + item.BetTypeId.ToString());
                        item.SportTypeName = Helper.GetResource("s" + item.SportTypeId.ToString());
                    }
                    else if (item.ProductId != 0)
                    {
                        item.BetTypeId = item.ProductId;
                        item.EnableText = item.BetTypeName = Helper.GetResource("p" + item.ProductId.ToString());
                    }
                    else
                    {
                        if (object.ReferenceEquals(null, item.BetTypeId))
                        {
                            item.BetTypeId = -1;
                        }

                        item.EnableText = Helper.GetResource("enable");
                    }

                    item.EnableDownlinesText = Helper.GetResource("enableDownline");
                }

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
                var result = new Result();
                if (model.RequestContext.NewMode)
                {
                    result = _data.UpdateProductStatus(storedProcedure, model.RequestContext, model.Entities);
                    if (result.ErrorCode != 0 || !string.IsNullOrWhiteSpace(result.ErrorMessage))
                    {
                        var xmlMessage = XElement.Parse(result.ErrorMessage);
                        var message = string.Empty;
                        foreach (var attr in xmlMessage.Attributes())
                        {
                            if (attr.Name.ToString().Equals("Msg", StringComparison.OrdinalIgnoreCase))
                            {
                                message += string.Join("\r\n", attr.Value);
                            }
                        }

                        result = new Result(999, message);
                        results.Add(result);
                    }
                }
                else
                {
                    foreach (var item in model.Entities)
                    {
                        if (!item.Hide && item.IsDirty)
                        {
                            result = _data.UpdateProductStatus(storedProcedure, model.RequestContext, item);
                            if (result.ErrorCode != 0 || !string.IsNullOrWhiteSpace(result.ErrorMessage))
                            {
                                result = new Result(999, result.ErrorMessage);
                                results.Add(result);
                            }
                        }
                    }
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
