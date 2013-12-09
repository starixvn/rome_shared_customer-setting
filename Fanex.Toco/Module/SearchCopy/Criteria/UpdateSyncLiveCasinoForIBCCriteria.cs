// -----------------------------------------------------------------------
// <copyright file="UpdateSyncLiveCasinoForIBCCriteria.cs" company="Nexcel">
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
    /// Class UpdateSyncLiveCasinoForIBCCriteria.
    /// </summary>
    [Serializable]
    public class UpdateSyncLiveCasinoForIBCCriteria : CriteriaBase
    {
        public int CustId { get; set; }
        
        public bool SyncLiveCS { get; set; }

        public int UserId { get; set; }

        public string IP { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdSyncLiveCS";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}