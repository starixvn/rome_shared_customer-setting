// --------------------------------------------------------------------------
// <copyright file="SearchCopyService.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.SearchCopy.Services
{
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;
    using Fanex.CustomerSetting.Module.SearchCopy.Repositories;
    using Fanex.CustomerSetting.Module.SearchCopy.Command;
    using System.Web.Mvc;
    using System.Collections.Generic;

    /// <summary>
    /// Class SearchCopyService.
    /// </summary>
    public class SearchCopyService : ISearchCopyService
    {
        private static ISearchCopyRepository _searchCopyRepository;

        public SearchCopyService() : this(null)
        {
        }

        public SearchCopyService(ISearchCopyRepository searchCopyRepository)
        {
            _searchCopyRepository = searchCopyRepository ?? new SearchCopyRepository();
        }        

        public int SearchExistingUser(SearchExistingUserCriteria criteria)
        {
            return _searchCopyRepository.SearchExistingUser(criteria);
        }

        public DisableSports GetSportsStatusByUserName(string username)
        {
            return _searchCopyRepository.GetSportsStatusByUserName(username);
        }

        public IEnumerable<Site> GetSitesExistingSuper(GetSitesCriteria criteria)
        {
            return _searchCopyRepository.GetSitesExistingSuper(criteria);
        }

        public string GetSiteAndCurrencyByUserId(GetSiteByUserIdCriteria criteria)
        {
            return _searchCopyRepository.GetSiteAndCurrencyByUserId(criteria);
        }

        public CommandResult CopyExistingUser(CustomerInsMemberCriteria criteria)
        {
            return _searchCopyRepository.CopyExistingUser(criteria);
        }
    }
}
