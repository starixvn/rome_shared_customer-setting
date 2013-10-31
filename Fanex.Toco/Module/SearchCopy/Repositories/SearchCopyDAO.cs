// --------------------------------------------------------------------------
// <copyright file="SearchCopyDAO.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.10.30">Created</change>
// </history>

using System.Collections.Generic;
using System.Collections;
using System.Data;
using Fanex.CustomerSetting.Module.SearchCopy.Entities;
using Fanex.Data;
using Fanex.Data.DNAide;

namespace Fanex.CustomerSetting.Module.SearchCopy.Repositories
{
    public class SearchCopyDAO : ISearchCopyDAO
    {
        public DataTable GetSportsbookSettings(int custId)        
        {
            var param = new Hashtable { { "custid", custId } };

            using (var objDb = new ObjectDb("GetSportsbookSettings"))
            {
                var dsResult = objDb.ExecuteDataSet(param);
                if (dsResult == null || dsResult.Tables.Count == 0)
                    return null;
                return dsResult.Tables[0];
            }

        }
    }
}
