// ************************************************************************************
// <copyright file="CloneAccount.cs" company="">
//     Copyright (c) CloneAccount.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="Andy.Nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.Module.CloneSetting.Entities
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CloneAccount
    {
        public string TargetUsername { get; set; }

        public string DestinationUsername { get; set; }

        public int ErrorCode { get; set; }

        public string ErrorMessage { get; set; }

        public bool IsSuccess { get; set; }
    }
}