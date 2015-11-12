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
    using System.Linq;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Helpers;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
    using Fanex.CustomerSetting.EnableStatus.AllInOne.Repositories;
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Module.Core.Models;
    using RemotingRef;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class EnableStatusService : IEnableStatusService
    {
        private IEnableStatusRepository data = new EnableStatusRepository();

        public EnableStatusModel GetProductStatus(SpElementConfiguration storedProcedure, RequestContext context)
        {
            try
            {
                var model = data.GetProductStatus(storedProcedure, context);
                var product = Helper.Products.Where(p => p.Id.Equals(context.ProductId, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                context.NewMode = product.NewMode;
                model.IframeId = Helper.Products.IframeId;
                model.HasCheckAll = product.HasCheckAll;
                model.ReloadAfterUpdate = product.ReloadAfterUpdate;
                model.Warning = Helper.GetResource(product.Warning);
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
                        item.EnableText = item.SportTypeName = RefData.GetSportName(item.SportTypeId.ToString(), context.Language, string.Empty);
                    }
                    else if (item.BetTypeId != null && Utils.ToInt32(item.BetTypeId) > 0 && (item.SportTypeId == null || Utils.ToInt32(item.SportTypeId) == 0))
                    {
                        item.EnableText = item.BetTypeName = RefData.GetBetTypeName(item.BetTypeId.ToString(), context.Language, string.Empty);
                    }
                    else if (item.BetTypeId != null && item.SportTypeId != null && Utils.ToInt32(item.BetTypeId) > 0 && Utils.ToInt32(item.SportTypeId) > 0)
                    {
                        item.EnableText = item.BetTypeName = Helper.GetResource("b" + item.BetTypeId.ToString());
                        item.SportTypeName = Helper.GetResource("s" + item.SportTypeId.ToString());
                    }
                    if (item.ProductId != 0)
                    {
                        item.BetTypeId = item.ProductId;
                        item.EnableText = item.BetTypeName = Helper.GetResource("p" + item.ProductId.ToString());
                    }
                    else
                    {
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
                    result = data.UpdateProductStatus(storedProcedure, model.RequestContext, model.Entities);
                }
                else
                {
                    foreach (var item in model.Entities)
                    {
                        if (!item.Hide)
                        {
                            result = data.UpdateProductStatus(storedProcedure, model.RequestContext, item);
                        }
                    }
                }

                results.Add(result);

                return results;
            }
            catch (Exception ex)
            {                
                throw ex;
            }
        }
    }
}
