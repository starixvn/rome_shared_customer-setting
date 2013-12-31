// -----------------------------------------------------------------------
// <copyright file="UpdateSyncBingoForIBCCriteria.cs" company="Nexcel">
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
    /// Class UpdateSyncBingoForIBCCriteria.
    /// </summary>
    [Serializable]
    public class UpdateSyncBingoForIBCCriteria : UpdateSyncBaseForIBCCriteria
    {
        public UpdateSyncBingoForIBCCriteria()
        {
            SyncBingo = true;
        }

        public bool SyncBingo { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdSyncBingo";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}