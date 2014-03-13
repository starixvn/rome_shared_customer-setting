// --------------------------------------------------------------------------
// <copyright file="CustomerSettingBase.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Entities
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The CustomerSettingBase entity.
    /// </summary>
    public class CustomerSettingBase
    {
        public CustomerInformation CustomerInformation { get; set; }

        public IList<BetSetting> BetSettings { get; set; }

        public CopyAll BetSettingCopyAll { get; set; }

        public CopyAll PositionTakingCopyAll { get; set; }

        public void UpdateSettingInCaseOfCopyAll()
        {
            this.UpdateBetSettingsInCaseOfCopyAll();
            this.UpdatePositionTakingsInCaseOfCopyAll();
        }

        internal static bool IsCopyToItself(int currentBetTypeId, int copiedBetTypeId)
        {
            return currentBetTypeId == copiedBetTypeId;
        }

        internal static void CopyToOneBetSetting(BetSetting copiedBetSetting, BetSetting betSetting)
        {
            betSetting.MinBet = copiedBetSetting.MinBet;
            betSetting.MaxBet = copiedBetSetting.MaxBet;
            betSetting.MaxPerMatch = copiedBetSetting.MinBet;
            betSetting.IsEnabled = copiedBetSetting.IsEnabled;
            betSetting.IsDownlinesEnabled = copiedBetSetting.IsDownlinesEnabled;
            betSetting.IsBetSettingUpdated = copiedBetSetting.IsBetSettingUpdated;
            betSetting.IsStatusUpdated = copiedBetSetting.IsStatusUpdated;
        }

        internal virtual void UpdatePositionTakingsInCaseOfCopyAll()
        {
        }

        /// <summary>
        /// Updates the bet settings in case of copy all.
        /// </summary>
        internal void UpdateBetSettingsInCaseOfCopyAll()
        {
            if (this.CopyAllIsValidForBetSetting())
            {
                var copiedBetSetting = this.GetCopiedBetSetting();

                if (copiedBetSetting == null)
                {
                    return;
                }

                foreach (var betSetting in this.BetSettings)
                {
                    if (IsCopyToItself(betSetting.BetTypeId, this.BetSettingCopyAll.BettypeId))
                    {
                        return;
                    }

                    CopyToOneBetSetting(copiedBetSetting, betSetting);
                }
            }
        }

        internal BetSetting GetCopiedBetSetting()
        {
            return this.BetSettings.Where(b => b.BetTypeId == this.BetSettingCopyAll.BettypeId).FirstOrDefault();
        }

        internal bool CopyAllIsValidForBetSetting()
        {
            return this.BetSettingCopyAll != null && this.BetSettingCopyAll.IsCopiedAll && this.BetSettings != null && this.BetSettings.Count > 0;
        }
    }
}