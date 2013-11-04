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
    using System.Data;
    using Fanex.Data;

    /// <summary>
    /// Class SearchCopyRepository.
    /// </summary>
    public class SearchCopyRepository : ISearchCopyRepository
    {
        /// <summary>
        /// Searching a existing super user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>Return customer id if success else return string.Empty.</returns>
        public int SearchExistingUser(string username)
        {
            var param = new Hashtable { { "uplineid", 0 }, { "username", username }, { "custid", -1 }, { "issubacc", 0 } };

            using (var objDb = new ObjectDb("Age_CustomerSelCheckBelonging_ByUsername"))
            {
                objDb.ExecuteNonQuery(param);
                return Convert.ToInt32(param["custid"]);
            }            
        }

        /// <summary>
        /// Get sports book settings.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The data table.</returns>
        public DataTable GetCustomerInfo(string username) 
        {
            var param = new Hashtable { { "custid", 0 }, { "username", username } };

            using (var objDb = new ObjectDb("Acc_CustomerSelStatus"))
            {
                var datasetResult = objDb.ExecuteDataSet(param);
                if (datasetResult == null || datasetResult.Tables.Count == 0)
                {
                    return null;
                }

                return datasetResult.Tables[0];
            }
        }
    }
}
