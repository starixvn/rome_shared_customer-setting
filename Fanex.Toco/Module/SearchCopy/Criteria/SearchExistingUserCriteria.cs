// -----------------------------------------------------------------------
// <copyright file="SearchExistingUserCriteria.cs" company="Nexcel">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
// <history>
//   <change who="Thai.Tran" date="2013.11.05">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.SearchCopy.Criteria
{
    using System;
    using Fanex.NAP.Framework.Data;

    [Serializable]
    public class SearchExistingUserCriteria : CriteriaBase
    {
        public int uplineid { get; set; }

        public string username { get; set; }

        public int custid { get; set; }

        public bool issubacc { get; set; }
         
        public override string GetSettingKey()
        {
            return "Age_CustomerSelCheckBelonging_ByUsername";
        }

        public override bool IsValid() { return true; }
    }
}