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
    using System.Collections.Generic;    
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;
    using Fanex.CustomerSetting.Module.SearchCopy.Repositories;
    using Fanex.NAP.Framework.Data;

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

        public HideSports GetSportsStatusByUserName(string username)
        {
            return _searchCopyRepository.GetSportsStatusByUserName(username);
        }

        public IEnumerable<Site> GetSitesExistingSuper(GetSitesCriteria criteria)
        {
            return _searchCopyRepository.GetSitesExistingSuper(criteria);
        }

        public string GetSiteCreditAndCurrencyByUserId(GetSiteByUserIdCriteria criteria)
        {
            return _searchCopyRepository.GetSiteCreditAndCurrencyByUserId(criteria);
        }

        public CommandResult CopyExistingUser(ref CustomerInsMemberCriteria criteria)
        {
            return _searchCopyRepository.CopyExistingUser(ref criteria);
        }

        public CommandResult UpdateSyncLiveCasino(UpdateSyncLiveCasinoCriteria criteria)
        {
            return _searchCopyRepository.UpdateSyncLiveCasino(criteria);
        }

        public CommandResult UpdateSync(ICriteria criteria)
        {
            return _searchCopyRepository.UpdateSync(criteria);
        }
    }
}
