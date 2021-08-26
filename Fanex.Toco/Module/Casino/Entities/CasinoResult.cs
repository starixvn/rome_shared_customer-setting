// --------------------------------------------------------------------------
// <copyright file="CasinoCustomer.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Andy.Nguyen" date="2013.4.11">Created</change>
// </history>

using Fanex.CustomerSetting.Module.Core.Models;

namespace Fanex.CustomerSetting.Module.Casino.Entities
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CasinoResult
    {
        public CasinoResult()
        {
            ResultError = new Result();
            CustId = 0;
        }

        public CasinoResult(int custId, Result resultError)
        {
            CustId = custId;
            ResultError = resultError;          
        }

        public Result ResultError { get; set; }       

        public int CustId { get; set; }
    }
}