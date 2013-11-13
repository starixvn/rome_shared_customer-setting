// ************************************************************************************
// <copyright file="ExchangeRate.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="kan.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.Racebook.ViewModels
{
    using System;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    public class ExchangeRate
    {
        public int Currency { get; set; }

        public string CurrencyName { get; set; }

        public double BasePerUnit { get; set; }

        public double UnitsPerBase { get; set; }
    }
}
