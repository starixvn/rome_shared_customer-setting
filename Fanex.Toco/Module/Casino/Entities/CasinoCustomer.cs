// --------------------------------------------------------------------------
// <copyright file="CasinoCustomer.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.4.11">Created</change>
// </history>

namespace Fanex.CustomerSetting.Module.Casino.Entities
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CasinoCustomer
    {
        public CasinoCustomer()
        {
            GeneralInfo = new GeneralCasinoCustomer();
        }

        public GeneralCasinoCustomer GeneralInfo { get; set; }

        public string UserPwd { get; set; }

        public int SIBCCustID { get; set; }

        public int MIBCCustID { get; set; }

        public int AIBCCustID { get; set; }
    }
}