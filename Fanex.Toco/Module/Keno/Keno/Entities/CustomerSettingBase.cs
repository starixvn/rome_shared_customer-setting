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
    using Fanex.CustomerSetting.Module.Core.Services.Impl;

    /// <summary>
    /// The CustomerSettingBase entity.
    /// </summary>
    /// <typeparam name="T">The type of PositionTaking.</typeparam>
    public class CustomerSettingBase<T> where T : PositionTakingBase
    {
        public CustomerInformation CustomerInformation { get; set; }

        public IList<BetSetting> BetSettings { get; set; }

        public IList<T> PositionTakings { get; set; }

        public CopyAll BetSettingCopyAll { get; set; }

        public CopyAll PositionTakingCopyAll { get; set; }

        public void UpdateSettingInCaseOfCopyAll()
        {
            this.UpdateBetSettingsInCaseOfCopyAll();
            this.UpdatePositionTakingsInCaseOfCopyAll();
        }

        public void UpdateRemainingInformation(int custId)
        {
            this.CustomerInformation.CustId = custId;
            this.UpdateCopyAllInformation();
            this.UpdateBetTypeNameForBetSetting();
            this.UpdateBetTypeNameForPositionTaking();
        }

        internal static bool IsCopyToItself(int currentBetTypeId, int copiedBetTypeId)
        {
            return currentBetTypeId == copiedBetTypeId;
        }

        /// <summary>
        /// Copies to one bet setting.
        /// </summary>
        /// <param name="sourceBetSetting">The copied bet setting.</param>
        /// <param name="destinationBetSetting">The bet setting.</param>
        internal static void CopyToOneBetSetting(BetSetting sourceBetSetting, BetSetting destinationBetSetting)
        {
            destinationBetSetting.MinBet = sourceBetSetting.MinBet;
            destinationBetSetting.MaxBet = sourceBetSetting.MaxBet;
            destinationBetSetting.MaxPerMatch = sourceBetSetting.MaxPerMatch;
            destinationBetSetting.IsEnabled = sourceBetSetting.IsEnabled;
            destinationBetSetting.IsDownlinesEnabled = sourceBetSetting.IsDownlinesEnabled;
            destinationBetSetting.IsBetSettingUpdated = true;
            destinationBetSetting.IsStatusUpdated = true;
        }

        /// <summary>
        /// Updates the bet type name for position taking.
        /// </summary>
        internal void UpdateBetTypeNameForPositionTaking()
        {
            if (this.PositionTakings == null || this.PositionTakings.Count == 0)
            {
                return;
            }

            foreach (var positionTaking in this.PositionTakings)
            {
                positionTaking.BetTypeName = CacheRef.GetBettype(positionTaking.BetTypeId);
            }
        }

        /// <summary>
        /// Updates the bet type name for bet setting.
        /// </summary>
        internal void UpdateBetTypeNameForBetSetting()
        {
            if (this.BetSettings == null || this.BetSettings.Count == 0)
            {
                return;
            }

            foreach (var betSetting in this.BetSettings)
            {
                betSetting.BetTypeName = CacheRef.GetBettype(betSetting.BetTypeId);
            }
        }

        internal void UpdateCopyAllInformation()
        {
            this.PositionTakingCopyAll = new CopyAll();
            this.BetSettingCopyAll = new CopyAll();
        }

        /// <summary>
        /// Updates the position takings in case of copy all.
        /// </summary>
        internal void UpdatePositionTakingsInCaseOfCopyAll()
        {
            if (!this.CopyAllIsValidForPositionTaking())
            {
                return;
            }

            var copiedSuperPositionTaking = GetCopiedPositionTaking();
            if (copiedSuperPositionTaking.BetTypeId <= 0)
            {
                return;
            }

            foreach (var superPositionTaking in this.PositionTakings)
            {
                if (IsCopyToItself(superPositionTaking.BetTypeId, this.PositionTakingCopyAll.BetTypeId))
                {
                    continue;
                }

                CopyToOnePositionTaking(copiedSuperPositionTaking, superPositionTaking);
            }
        }

        internal virtual void CopyToOnePositionTaking(T sourcePositionTaking, T destinationPositionTaking)
        {
        }

        internal bool CopyAllIsValidForPositionTaking()
        {
            return this.PositionTakingCopyAll != null && this.PositionTakingCopyAll.IsCopiedAll && this.PositionTakings != null && this.PositionTakings.Count > 0;
        }

        /// <summary>
        /// Updates the bet settings in case of copy all.
        /// </summary>
        internal void UpdateBetSettingsInCaseOfCopyAll()
        {
            if (!this.CopyAllIsValidForBetSetting())
            {
                return;
            }

            var copiedBetSetting = this.GetCopiedBetSetting();
            if (copiedBetSetting.BetTypeId <= 0)
            {
                return;
            }

            foreach (var betSetting in this.BetSettings)
            {
                if (IsCopyToItself(betSetting.BetTypeId, this.BetSettingCopyAll.BetTypeId))
                {
                    continue;
                }

                CopyToOneBetSetting(copiedBetSetting, betSetting);
            }
        }

        internal BetSetting GetCopiedBetSetting()
        {
            var betSetting = this.BetSettings.Where(b => b.BetTypeId == this.BetSettingCopyAll.BetTypeId).FirstOrDefault();
            return betSetting == null ? new BetSetting() : betSetting;
        }

        internal bool CopyAllIsValidForBetSetting()
        {
            return this.BetSettingCopyAll != null && this.BetSettingCopyAll.IsCopiedAll && this.BetSettings != null && this.BetSettings.Count > 0;
        }

        internal dynamic GetCopiedPositionTaking()
        {
            var positionTaking = this.PositionTakings.Where(p => p.BetTypeId == this.PositionTakingCopyAll.BetTypeId).FirstOrDefault();
            return positionTaking == null ? new PositionTakingBase() : positionTaking;
        }
    }
}