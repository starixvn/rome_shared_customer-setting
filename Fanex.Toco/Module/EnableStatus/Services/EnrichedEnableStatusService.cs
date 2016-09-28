using System;
using System.Collections.Generic;
using Fanex.CustomerSetting.EnableStatus.AllInOne.Entities;
using Fanex.CustomerSetting.EnableStatus.AllInOne.Mappings;
using Fanex.CustomerSetting.EnableStatus.AllInOne.Models;
using Fanex.CustomerSetting.Module.Core.Models;

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

            return enableStatusModel;
        }

        private void EnrichAllbetProductStatus(EnableStatusModel enableStatusModel)
        {
            enableStatusModel.Notes = new List<string>()
            {
                Resources.EnableStatusResources.AllbetNormalGamesNote,
                Resources.EnableStatusResources.AllbetVIPBaccaratNote
            };

            foreach (EnableStatusEntity enableStatusEntity in enableStatusModel.Entities)
            {
                if (enableStatusEntity.BetTypeId.ToString() == "2401")
                {
                    enableStatusEntity.EnableText = Resources.EnableStatusResources.AllbetNormalGames;
                }
            }
        }

        public IList<Result> UpdateProductStatus(EnableStatusModel model, SpElementConfiguration storedProcedure)
        {
            return _enableStatusService.UpdateProductStatus(model, storedProcedure);
        }
    }
}
