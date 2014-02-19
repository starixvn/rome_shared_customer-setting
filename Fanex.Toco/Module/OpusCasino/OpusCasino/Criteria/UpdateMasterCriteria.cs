// ************************************************************************************
// <copyright file="UpdateMasterCriteria.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

namespace Fanex.CustomerSetting.OpusCasino.Criteria
{
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// UpdateMasterCriteria Class.
    /// </summary>
    public class UpdateMasterCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int Srecommend { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public bool V163UpdateCustomerStatus_1401 { get; set; }

        public bool Disable { get; set; }

        public bool DisableDownLines { get; set; }

        public bool V163UpdatePositionTaking_1401 { get; set; }

        public double V163spositiontaking_1401 { get; set; }

        public double V163mpositiontaking_1401 { get; set; }

        public bool V163UpdateAutoPositiontaking_1401 { get; set; }

        public bool Isauto_v163spositiontaking_1401 { get; set; }

        public double Auto_v163spositiontaking_1401 { get; set; }

        public bool V163UpdateMinPositiontaking_1401 { get; set; }

        public double V163mpositiontaking_1401_min { get; set; }

        public bool Updateallmembers { get; set; }

        public double Newspositiontaking { get; set; }

        public bool V163UpdateCommission_1401 { get; set; }

        public double V163Discount_1401 { get; set; }

        public bool V163UpdateMaxTransfer_1401 { get; set; }

        public double MaxTransferCredit { get; set; }

        public bool IsAutoReset { get; set; }

        public string Ip { get; set; }

        public string Language { get; set; }

        ////public int ErrCode { get; set; }

        ////public string ErrMsg { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdMasterOpusCS_All";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}