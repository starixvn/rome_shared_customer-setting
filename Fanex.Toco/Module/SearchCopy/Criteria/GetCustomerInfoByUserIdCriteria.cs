// -----------------------------------------------------------------------
// <copyright file="GetCustomerInfoByUserIdCriteria.cs" company="Nexcel">
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
    /// Class GetCustomerInfoByUserIdCriteria.
    /// </summary>
    [Serializable]
    public class GetCustomerInfoByUserIdCriteria : CriteriaBase
    {       
        public int Custid { get; set; }
         
        public override string GetSettingKey()
        {
            return "Acc_CustomerSelCEditCustomer";
        }

        public override bool IsValid()
        {
            return true;
        }
    }
}