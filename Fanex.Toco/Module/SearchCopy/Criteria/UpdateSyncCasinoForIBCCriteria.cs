// -----------------------------------------------------------------------
// <copyright file="UpdateSyncCasinoForIBCCriteria.cs" company="Nexcel">
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
    /// Class UpdateSyncCasinoForIBCCriteria.
    /// </summary>
    [Serializable]
    public class UpdateSyncCasinoForIBCCriteria : UpdateSyncBaseForIBCCriteria
    {
        public UpdateSyncCasinoForIBCCriteria()
        {
            SyncCS = true;
        }

        public bool SyncCS { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_UpdCSStatus";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}