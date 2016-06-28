// --------------------------------------------------------------------------
// <copyright file="CustomerInformationViewModel.cs" company="">
// Copyright (c). All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels
{
    /// <summary>
    /// The CustomerInformationViewModel view model.
    /// </summary>
    public class CustomerInformationViewModel
    {
        public int CustId { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public int IsMultipleEdit { get; set; }
    }
}
