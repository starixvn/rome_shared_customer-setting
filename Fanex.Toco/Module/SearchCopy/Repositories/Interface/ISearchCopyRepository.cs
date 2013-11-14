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
    using Fanex.CustomerSetting.Module.SearchCopy.Command;
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;
    using Fanex.CustomerSetting.Module.SearchCopy.Repository.Interface;

    public interface ISearchCopyRepository : IBaseRepository
    {        
        int SearchExistingUser(SearchExistingUserCriteria criteria);

        DisableSports GetSportsStatusByUserName(string username);

        IEnumerable<Site> GetSitesExistingSuper(GetSitesCriteria criteria);

        string GetSiteCreditAndCurrencyByUserId(GetSiteByUserIdCriteria criteria);

        CommandResult CopyExistingUser(CustomerInsMemberCriteria criteria);
    }
}