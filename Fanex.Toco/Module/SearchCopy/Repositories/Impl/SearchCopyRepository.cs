// --------------------------------------------------------------------------
// <copyright file="SearchCopyRepository.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.SearchCopy.Repositories
{
    using System;
    using System.Collections;
    using System.Collections.Generic;    
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;
    using Fanex.CustomerSetting.Module.SearchCopy.Repository.Impl;
    using Fanex.Data;
    using Fanex.NAP.Framework.Data;
    using Fanex.Common;

    /// <summary>
    /// Class SearchCopyRepository.
    /// </summary>
    public class SearchCopyRepository : BaseRepository, ISearchCopyRepository
    {
        /// <summary>
        /// Searching a existing super user.
        /// </summary>
        /// <param name="criteria">Criteria SearchExistingUserCriteria.</param>
        /// <returns>Return customer id if success else return -1.</returns>
        public int SearchExistingUser(SearchExistingUserCriteria criteria)
        {
            return Convert.ToInt32(ReturnSingleValueByColumnName(criteria, "custid"));
        }

        /// <summary>
        /// Get site and currency of user.
        /// </summary>
        /// <param name="criteria">Criteria GetSiteByUserIdCriteria.</param>
        /// <returns>Return site and currency as format "site,credit,currency".</returns>
        public string GetSiteCreditAndCurrencyByUserId(GetSiteByUserIdCriteria criteria)
        {
            string site = string.Empty;
            string credit = string.Empty;
            int currencyId = 0;
            string currencyName = string.Empty;

            // prepare SP parameters
            Hashtable param = criteria.GetSpParams();
            param = (new ResultCriteria()).AddSpParams(param);

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var dataReader = objDb.ExecuteReader(param);
                while (dataReader.Read())
                {
                    site = dataReader["site"].ToString();
                    credit = dataReader["credit"].ToString();
                    currencyId = Converter.ToInt(dataReader["currencyID"]);
                    currencyName = dataReader["currencyName"].ToString();
                }                
            }

            return site + "," + credit + "," + currencyId + "," + currencyName;
        }

        /// <summary>
        /// Get status of sports belong super user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Object DisableSports.</returns>
        public HideSports GetSportsStatusByUserName(string username)
        {
            HideSports statusSports = new HideSports();
            var param = new Hashtable { { "custid", 0 }, { "username", username } };

            using (var objDb = new ObjectDb("CustomerSelStatus"))
            {
                var dataReader = objDb.ExecuteReader(param);
                while (dataReader.Read())
                {
                    statusSports.HideSportsbook = false; ////alway show
                    statusSports.HideSportsbookMinPT = false; ////alway show
                    statusSports.HideRacing = false;// Convert.ToBoolean(dataReader["disablehorseracing"]);
                    statusSports.HideRacingMinPT = false;// Convert.ToBoolean(dataReader["disablehorseracing"]);
                    statusSports.HideCasino = false;// Convert.ToBoolean(dataReader["disablecasino"]);
                    statusSports.HideVirtualSports = false;// Convert.ToBoolean(dataReader["disablevirtualsports"]);
                    statusSports.HideBingo = false;// Convert.ToBoolean(dataReader["disablebingo"]);
                    statusSports.HideLiveCasino = Convert.ToBoolean(dataReader["synclivecasino"]);                                                              
                } 
            }

            return statusSports;
        }

        /// <summary>
        /// Get sites of the user.
        /// </summary>
        /// <param name="criteria">Criteria GetSitesExistingSuperCriteria.</param>
        /// <returns>The list of sites.</returns>
        public IEnumerable<Site> GetSitesExistingSuper(GetSitesCriteria criteria)
        {
            List<Site> sites = new List<Site>();

            // prepare SP parameters
            Hashtable param = criteria.GetSpParams();
            param = (new ResultCriteria()).AddSpParams(param);

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var dataReader = objDb.ExecuteReader(param);
                while (dataReader.Read())
                {
                    if (Convert.ToBoolean(dataReader["islicensee"]) == false)
                    {
                        Site site = new Site { SiteId = Convert.ToInt32(dataReader["siteid"]), SiteName = dataReader["site"].ToString() };
                        sites.Add(site);
                    }
                } 
            }

            return sites;
        }

        /// <summary>
        /// Copy existing user.
        /// </summary>
        /// <param name="criteria">The new user.</param>
        /// <returns>Object CommandResult.</returns>
        public CommandResult CopyExistingUser(CustomerInsMemberCriteria criteria)
        {            
            // prepare SP parameters
            Hashtable param = criteria.GetSpParams();

            param = (new ResultCriteria()).AddSpParams(param);

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(param);

                return GetCommandResult(param);
            }         
        }

        public CommandResult UpdateSyncLiveCasino(UpdateSyncLiveCasinoCriteria criteria)
        {
            // prepare SP parameters
            Hashtable param = criteria.GetSpParams();

            param = (new ResultCriteria()).AddSpParams(param);

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(param);

                return GetCommandResult(param);
            }         
        }

        public CommandResult UpdateSync(ICriteria criteria)
        {
            // prepare SP parameters
            Hashtable param = criteria.GetSpParams();

            param = (new ResultCriteria()).AddSpParams(param);

            using (var db = new ObjectDb(criteria.GetSettingKey()))
            {
                db.ExecuteNonQuery(param);

                return GetCommandResult(param);
            }
        }
    }
}
