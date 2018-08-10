using System;
using System.Collections.Generic;
using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;
using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
using Fanex.CustomerSetting.Module.Core.Models;
using Fanex.CustomerSetting.Module.Core.Services.Impl;
using Fanex.CustomerSetting.Module.Core.Web;

namespace Fanex.CustomerSetting.EnableStatus.AllInOne.Services
{
    public class EnrichedEnableStatusService : IEnableStatusService
    {
        private readonly IEnableStatusService _enableStatusService;

        public EnrichedEnableStatusService(IEnableStatusService enableStatusService)
        {
            _enableStatusService = enableStatusService;
        }

        public EnableStatusModel GetProductStatus(SpElementConfiguration storedProcedure, RequestContext context)
        {
            EnableStatusModel enableStatusModel = _enableStatusService.GetProductStatus(storedProcedure, context);

            if (context.Product == 21)
            {
                EnrichAllbetProductStatus(enableStatusModel);
            }

            if (context.Product == 24)
            {
                EnrichFWProductStatus(enableStatusModel);
            }

            return enableStatusModel;
        }

        private void EnrichAllbetProductStatus(EnableStatusModel enableStatusModel)
        {
            foreach (EnableStatusEntity enableStatusEntity in enableStatusModel.Entities)
            {
                if (enableStatusEntity.BetTypeId.ToString() == "2401")
                {
                    enableStatusEntity.EnableText = Resources.EnableStatusResources.AllbetNormalGames;
                }
            }
        }

        private void EnrichFWProductStatus(EnableStatusModel enableStatusModel)
        {
            foreach (EnableStatusEntity enableStatusEntity in enableStatusModel.Entities)
            {
                enableStatusEntity.EnableText = CacheRef.GetRefResourceByRefIdAndResourceId("Product", "24", string.Empty);
            }
        }

        public IList<Result> UpdateProductStatus(EnableStatusModel model, SpElementConfiguration storedProcedure)
        {
            return _enableStatusService.UpdateProductStatus(model, storedProcedure);
        }
    }
}
