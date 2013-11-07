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
    using Fanex.CustomerSetting.Module.SearchCopy.Command;
    using Fanex.CustomerSetting.Module.SearchCopy.Criteria;
    using Fanex.CustomerSetting.Module.SearchCopy.Entities;
    using Fanex.CustomerSetting.Module.SearchCopy.Repository.Impl;
    using Fanex.Data;
    using System.Web.Mvc;
    using System.Collections.Generic;

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
        /// <returns>Return site and currency as format "sitename,currencyname".</returns>
        public string GetSiteAndCurrencyByUserId(GetSiteByUserIdCriteria criteria)
        {
            string site = string.Empty;
            string currency = string.Empty;

            // prepare SP parameters
            Hashtable param = criteria.GetSpParams();
            param = (new ResultCriteria()).AddSpParams(param);

            using (var objDb = new ObjectDb(criteria.GetSettingKey()))
            {
                var dataReader = objDb.ExecuteReader(param);
                while (dataReader.Read())
                {
                    site = dataReader["site"].ToString();
                    currency = dataReader["currencyid"].ToString();
                }                
            }

            return site + "," + currency;
        }

        /// <summary>
        /// Get status of sports belong super user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Object DisableSports.</returns>
        public DisableSports GetSportsStatusByUserName(string username)
        {
            DisableSports statusSports = new DisableSports();
            var param = new Hashtable { { "custid", 0 }, { "username", username } };

            using (var objDb = new ObjectDb("Acc_CustomerSelStatus"))
            {
                var dataReader = objDb.ExecuteReader(param);
                while (dataReader.Read())
                {
                    statusSports.DisableSportsbook = false; ////alway show
                    statusSports.DisableSportsbookMinPT = false; ////alway show
                    statusSports.DisableRacing = Convert.ToBoolean(dataReader["disablehorseracing"]);
                    statusSports.DisableRacingMinPT = Convert.ToBoolean(dataReader["disablehorseracing"]);
                    statusSports.DisableCasino = Convert.ToBoolean(dataReader["disablecasino"]);
                    statusSports.DisableVirtualSports = Convert.ToBoolean(dataReader["disablevirtualsports"]);
                    statusSports.DisableBingo = Convert.ToBoolean(dataReader["disablebingo"]);
                    statusSports.DisableLiveCasino = Convert.ToBoolean(dataReader["disablelivecasino"]);                                                              
                } 
            }

            return statusSports;
        }

        /// <summary>
        /// Get sites of the user
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
                        Site site = new Site { siteid = Convert.ToInt32(dataReader["siteid"]), site = dataReader["site"].ToString() };
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
    }
}
