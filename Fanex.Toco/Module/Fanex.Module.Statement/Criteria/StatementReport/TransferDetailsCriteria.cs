// --------------------------------------------------------------------------
// <copyright file="TransferDetailsCriteria.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Louis.Nguyen" date="2012.12.07">Created</change>
// </history>

namespace Fanex.Module.Statement.Criteria.StatementReport
{
    using System.Collections;
    using Fanex.Module.Statement.Repositories;
    using Fanex.NAP.Framework.Data;

    /// <summary>
    /// Transfer Details Criteria.
    /// </summary>
    public class TransferDetailsCriteria : CriteriaBase
    {
        public string TransferDate { get; set; }

        public int CustId { get; set; }

        [SpParam(Ignored = true)]
        public int RoleId { get; set; }

        public override string GetSettingKey()
        {
            return "Statement_Report_TransferDetails";
        }

        public override bool IsValid()
        {
            return true;
        }

        public override Hashtable GetSpParams()
        {
            Hashtable storeProcedureParams = new Hashtable();
            storeProcedureParams.Add("custid", CustId);
            storeProcedureParams.Add("fromdate", TransferDate);
            storeProcedureParams.Add("todate", TransferDate);
            storeProcedureParams.Add("language", "EN");
            storeProcedureParams.Add("balanceupday", DummyDataRepository.BalanceUpDay);
            storeProcedureParams.Add("movebetday", DummyDataRepository.MoveBetsDay);
            return storeProcedureParams;
        }
    }
}
