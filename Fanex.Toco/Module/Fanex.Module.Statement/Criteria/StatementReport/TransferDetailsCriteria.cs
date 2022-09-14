// --------------------------------------------------------------------------
// <copyright file="TransferDetailsCriteria.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Criteria.StatementReport
{
    using System;
    using Fanex.Common;
    using Fanex.CustomerSetting.Module.Core.Extensions;
    using Fanex.Module.Statement.Services;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Transfer Details Criteria.
    /// </summary>
    public class TransferDetailsCriteria : CriteriaBase
    {
        [SpParam(SqlParamName = "WinLostDate")]
        public string TransferDate { get; set; }

        [SpParam(Ignored = true)]
        public DateTime BalanceUpDate { get; set; }

        public int CustId { get; set; }

        [SpParam(Ignored = true)]
        public string Username { get; set; }

        [SpParam(Ignored = true)]
        public int RoleId { get; set; }

        [SpParam(Ignored = true)]
        public string SiteName { get; set; }

        [SpParam(Ignored = true)]
        public bool IsSiteDB { get; set; }

        [SpParam(Ignored = true)]
        public int SiteDBGroupID { get; set; }

        [SpParam(Ignored = true)]
        public bool IsBeforeBalanceUpDate
            => Converter.ToDateTime(TransferDate) >= BalanceUpDate;

        public override string GetSettingKey()
        {
            if (IsBeforeBalanceUpDate)
            {
                return IsSiteDB
                    ? $"SiteDB{SiteDBGroupID}_Statement_Report_TransferDetails"
                    : "Statement_Report_TransferDetails";
            }

            return $"Statement_Report_TransferDetails{ReportService.WarehouseSPSuffix}";
        }

        public override bool IsValid()
        {
            try
            {
                Converter.ToDateTime(this.TransferDate);
            }
            catch (Exception ex)
            { 
                LogExtension.LogInfo(ex);
                return false;
            }

            return true;
        }
    }
}
