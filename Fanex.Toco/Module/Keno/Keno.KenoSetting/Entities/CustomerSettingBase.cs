// --------------------------------------------------------------------------
// <copyright file="CustomerSettingBase.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Fanex.CustomerSetting.Module.Core.Services.Impl;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels;

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

        public CopyAll StatusCopyAll { get; set; }

        public CopyAll PositionTakingCopyAll { get; set; }

        public void AdjustInformationWhenUpdateSetting()
        {
            this.CopyBetSettingInCaseOfCopyAll();
            this.CopyStatusInCaseOfCopyAll();
            this.CopyPositionTakingInCaseOfCopyAll();
            this.AdjustValuesOfPositionTakings();
            this.UpdateBetSettingWhenMultiple();
            this.UpdatePositionTakingWhenMultiple();
        }

        /// <summary>
        /// Adjust information when load setting.
        /// </summary>
        /// <param name="customerInformationViewModel">Customer Information View Model.</param>
        public void AdjustInformationWhenLoadSetting(CustomerInformationViewModel customerInformationViewModel)
        {
            try
            {
                this.UpdateCustomerInformation(customerInformationViewModel);
                this.UpdateCopyAllInformation();
                this.UpdateBetTypeNameForBetSetting();
                this.UpdateBetTypeNameForPositionTaking();
            }
            catch (Exception ex)
            {
                LoggingHandler.Log(ex);
            }
        }

        public void UpdateBetSettingWhenMultiple()
        {
            if (this.CustomerInformation.IsMultiple)
            {
                foreach (var betSetting in this.BetSettings)
                {
                    betSetting.IsBetSettingUpdated = true;
                    betSetting.IsStatusUpdated = true;
                }
            }
        }

        /// <summary>
        /// Copy setting for multiple.
        /// </summary>
        public void UpdatePositionTakingWhenMultiple()
        {
            if (this.CustomerInformation.IsMultiple)
            {
                foreach (var betSetting in this.BetSettings)
                {
                    betSetting.IsBetSettingUpdated = true;
                    betSetting.IsStatusUpdated = true;
                }
            }

            foreach (var positionTaking in this.PositionTakings)
            {
                this.ChangeSettingWhenMultiple(positionTaking, this.CustomerInformation.IsMultiple);
            }
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
            destinationBetSetting.IsBetSettingUpdated = true;
        }

        /// <summary>
        /// Copies to one status.
        /// </summary>
        /// <param name="sourceBetSetting">The copied status.</param>
        /// <param name="destinationBetSetting">The status.</param>
        internal static void CopyToOneStatus(BetSetting sourceBetSetting, BetSetting destinationBetSetting)
        {
            destinationBetSetting.IsEnabled = sourceBetSetting.IsEnabled;
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
            this.StatusCopyAll = new CopyAll();
        }

        internal void UpdateCustomerInformation(CustomerInformationViewModel customerInformationViewModel)
        {
            this.CustomerInformation.CustId = customerInformationViewModel.CustId;
            this.CustomerInformation.UserId = customerInformationViewModel.UserId;
            this.CustomerInformation.SubAccId = customerInformationViewModel.SubAccId;
        }

        /// <summary>
        /// Updates the position takings in case of copy all.
        /// </summary>
        internal void CopyPositionTakingInCaseOfCopyAll()
        {
            if (!this.CopyAllIsValidForPositionTaking())
            {
                return;
            }

            var copiedPositionTaking = GetCopiedPositionTaking();
            if (copiedPositionTaking.BetTypeId <= 0)
            {
                return;
            }

            foreach (var positionTaking in this.PositionTakings)
            {
                if (IsCopyToItself(positionTaking.BetTypeId, this.PositionTakingCopyAll.BetTypeId))
                {
                    continue;
                }

                CopyToOnePositionTaking(copiedPositionTaking, positionTaking);
            }
        }

        internal void AdjustValuesOfPositionTakings()
        {
            foreach (var positionTaking in this.PositionTakings)
            {
                positionTaking.AdjustValues();
            }
        }

        internal bool CopyAllIsValidForPositionTaking()
        {
            return this.PositionTakingCopyAll != null && this.PositionTakingCopyAll.IsCopiedAll && this.PositionTakings != null && this.PositionTakings.Count > 0;
        }

        /// <summary>
        /// Updates the bet settings in case of copy all.
        /// </summary>
        internal void CopyBetSettingInCaseOfCopyAll()
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

        /// <summary>
        /// Updates the status in case of copy all.
        /// </summary>
        internal void CopyStatusInCaseOfCopyAll()
        {
            if (!this.CopyAllIsValidForStatus())
            {
                return;
            }

            var copiedBetSetting = this.GetCopiedStatus();
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

                CopyToOneStatus(copiedBetSetting, betSetting);
            }
        }

        internal BetSetting GetCopiedBetSetting()
        {
            var betSetting = this.BetSettings.Where(b => b.BetTypeId == this.BetSettingCopyAll.BetTypeId).FirstOrDefault();
            return betSetting == null ? new BetSetting() : betSetting;
        }

        internal BetSetting GetCopiedStatus()
        {
            var betSetting = this.BetSettings.Where(b => b.BetTypeId == this.StatusCopyAll.BetTypeId).FirstOrDefault();
            return betSetting == null ? new BetSetting() : betSetting;
        }

        internal bool CopyAllIsValidForBetSetting()
        {
            return this.BetSettingCopyAll != null && this.BetSettingCopyAll.IsCopiedAll && this.BetSettings != null && this.BetSettings.Count > 0;
        }

        internal bool CopyAllIsValidForStatus()
        {
            return this.StatusCopyAll != null && this.StatusCopyAll.IsCopiedAll && this.BetSettings != null && this.BetSettings.Count > 0;
        }

        internal dynamic GetCopiedPositionTaking()
        {
            var positionTaking = this.PositionTakings.Where(p => p.BetTypeId == this.PositionTakingCopyAll.BetTypeId).FirstOrDefault();
            return positionTaking == null ? new PositionTakingBase() : positionTaking;
        }

        internal virtual void ChangeSettingWhenMultiple(T positionTaking, bool isMultiple)
        {
        }

        internal virtual void CopyToOnePositionTaking(T sourcePositionTaking, T destinationPositionTaking)
        {
        }
    }
}