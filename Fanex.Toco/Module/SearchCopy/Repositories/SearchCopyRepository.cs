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
    using System.Collections;
    using System.Data;
    using Fanex.Data;

    /// <summary>
    /// Class SearchCopyRepository.
    /// </summary>
    public class SearchCopyRepository : ISearchCopyRepository
    {
        /// <summary>
        /// Get sports book settings.
        /// </summary>
        /// <param name="custId">The customer Id.</param>
        /// <returns>The data table.</returns>
        public DataTable GetSportsbookSettings(int custId)        
        {
            var param = new Hashtable { { "custid", custId } };

            using (var objDb = new ObjectDb("GetSportsbookSettings"))
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
