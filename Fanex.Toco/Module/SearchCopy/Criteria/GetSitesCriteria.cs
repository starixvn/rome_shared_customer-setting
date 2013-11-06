// -----------------------------------------------------------------------
// <copyright file="GetSitesCriteria.cs" company="Nexcel">
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
    public class GetSitesCriteria : CriteriaBase
    {           
        public override string GetSettingKey()
        {
            return "Acc_SiteSel";
        }

        public override bool IsValid() { return true; }
    }
}