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

    /// <summary>
    /// Class GetSitesCriteria.
    /// </summary>
    [Serializable]
    public class GetSitesCriteria : CriteriaBase
    {           
        public override string GetSettingKey()
        {
            return "CustomerSetting_SiteSel";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}