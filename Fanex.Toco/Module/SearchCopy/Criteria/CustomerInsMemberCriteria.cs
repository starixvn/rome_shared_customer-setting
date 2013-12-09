// -----------------------------------------------------------------------
// <copyright file="CustomerInsMemberCriteria.cs" company="Nexcel">
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
using Fanex.PTEngine.BusinessObjects;

    /// <summary>
    /// Class CustomerInsMemberCriteria.
    /// </summary>
    [Serializable]
    public class CustomerInsMemberCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int CustLevel { get; set; }

        public string UserName { get; set; }

        public string Userpwd { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public string Site { get; set; }

        public string Fax { get; set; }

        public decimal Credit { get; set; }

        public bool IsInternal { get; set; }

        public int ExistingCustId { get; set; }

        public int CurrencyId { get; set; }

        public int Creator { get; set; }

        public string Language { get; set; }

        public string IP { get; set; }

        public StatusSetting StatusSetting { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerInsMember_CloneSetting_Com";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}