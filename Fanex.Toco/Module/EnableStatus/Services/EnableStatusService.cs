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

                model.HasCheckAll = product.HasCheckAll;
                model.Height = product.Height;
                model.ShowInFrame = product.ShowInFrame;
                model.Title = Helper.GetResource(product.Title);
                model.Warning = Helper.GetResource(product.Warning);
                model.Width = product.Width;
                model.Username = model.Entities.FirstOrDefault().Username == null ? context.Username == null ? string.Empty : context.Username : 
                    model.Entities.FirstOrDefault().Username.ToString();

                foreach (var item in model.Entities)
                {
                    if (product.HideBetTypes.Contains(item.BetTypeId.ToString()))
                    {
                        item.Hide = true;
                    }

                    if ((int)item.BetTypeId != 0)
                    {
                        item.DisabledText = item.BetTypeName = RefData.GetBetTypeName(item.BetTypeId.ToString(), context.Language, string.Empty);
                    }
                    else
                    {
                        item.DisabledText = Helper.GetResource("enable");
                    }

                    item.DisabledDownlinesText = Helper.GetResource("enableDownline");
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

                foreach (var item in model.Entities)
                {
                    var result = data.UpdateProductStatus(storedProcedure, model.RequestContext, item);
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
