// --------------------------------------------------------------------------
// <copyright file="ISearchCopyRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.SearchCopy.Repositories
{
    using System.Collections.Generic;    
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;
    using Fanex.CustomerSetting.Module.SearchCopy.Repository.Interface;
    using Fanex.NAP.Framework.Data;

    public interface ISearchCopyRepository : IBaseRepository
    {        
        int SearchExistingUser(SearchExistingUserCriteria criteria);

        HideSports GetSportsStatusByUserName(string username);

        IEnumerable<Site> GetSitesExistingSuper(GetSitesCriteria criteria);

        string GetSiteCreditAndCurrencyByUserId(GetSiteByUserIdCriteria criteria);

        CommandResult CopyExistingUser(ref CustomerInsMemberCriteria criteria);

        CommandResult UpdateSyncLiveCasino(UpdateSyncLiveCasinoCriteria criteria);

        CommandResult UpdateSync(ICriteria criteria);
    }
}