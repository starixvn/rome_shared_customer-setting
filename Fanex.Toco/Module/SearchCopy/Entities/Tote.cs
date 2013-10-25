// ************************************************************************************
// <copyright file="Tote.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

using System.Collections.Generic;

namespace Fanex.CustomerSetting.Module.SearchCopy.Entities
{
    /// <summary>
    /// Class Tote.
    /// </summary>
    public class Tote
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>The tote name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Tote"/> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible { get; set; }

        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is top.
        /// </summary>
        /// <value><c>true</c> if this instance is top; otherwise, <c>false</c>.</value>
        public bool IsTop { get; set; }

        /// <summary>
        /// Gets or sets the maximum bet.
        /// </summary>
        /// <value>The maximum bet.</value>
        public decimal MaxBet { get; set; }

        /// <summary>
        /// Gets or sets the minimum bet.
        /// </summary>
        /// <value>The minimum bet.</value>
        public decimal MinBet { get; set; }

        /// <summary>
        /// Gets or sets the maximum per match.
        /// </summary>
        /// <value>The maximum per match.</value>
        public decimal MaxPerMatch { get; set; }

        /// <summary>
        /// Gets or sets the maximum bet maximum.
        /// </summary>
        /// <value>The maximum bet maximum.</value>
        public decimal MaxBetMax { get; set; }

        /// <summary>
        /// Gets or sets the minimum bet minimum.
        /// </summary>
        /// <value>The minimum bet minimum.</value>
        public decimal MinBetMin { get; set; }

        /// <summary>
        /// Gets or sets the maximum per match maximum.
        /// </summary>
        /// <value>The maximum per match maximum.</value>
        public decimal MaxPerMatchMax { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [update disable].
        /// </summary>
        /// <value><c>true</c> if [update disable]; otherwise, <c>false</c>.</value>
        public bool UpdateDisable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Tote"/> is enable.
        /// </summary>
        /// <value><c>true</c> if enable; otherwise, <c>false</c>.</value>
        public bool Enable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable DOWNLINE].
        /// </summary>
        /// <value><c>true</c> if [enable DOWNLINE]; otherwise, <c>false</c>.</value>
        public bool EnableDownline { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable automatic MPT].
        /// </summary>
        /// <value><c>true</c> if [enable automatic MPT]; otherwise, <c>false</c>.</value>
        public bool EnableAutoMPT { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [enable UPLINE].
        /// </summary>
        /// <value><c>true</c> if [enable UPLINE]; otherwise, <c>false</c>.</value>
        public bool EnableUpline { get; set; }

        /// <summary>
        /// Gets or sets the commission.
        /// </summary>
        /// <value>The commission.</value>
        public decimal Comm { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [update commission].
        /// </summary>
        /// <value><c>true</c> if [update commission]; otherwise, <c>false</c>.</value>
        public bool UpdateComm { get; set; }

        /// <summary>
        /// Gets or sets the master commission.
        /// </summary>
        /// <value>The master commission.</value>
        public decimal MasterComm { get; set; }

        /// <summary>
        /// Gets or sets the agent commission.
        /// </summary>
        /// <value>The agent commission.</value>
        public decimal AgentComm { get; set; }

        /// <summary>
        /// Gets or sets the member commission.
        /// </summary>
        /// <value>The member commission.</value>
        public decimal MemberComm { get; set; }

        /// <summary>
        /// Gets or sets the maximum commission.
        /// </summary>
        /// <value>The maximum commission.</value>
        public decimal MaxComm { get; set; }

        /// <summary>
        /// Gets or sets the minimum commission.
        /// </summary>
        /// <value>The minimum commission.</value>
        public decimal MinComm { get; set; }

        /// <summary>
        /// Gets or sets the house position taking.
        /// </summary>
        /// <value>The house position taking.</value>
        public decimal HousePt { get; set; }

        /// <summary>
        /// Gets or sets the maximum house position taking.
        /// </summary>
        /// <value>The maximum house position taking.</value>
        public decimal MaxHousePt { get; set; }

        /// <summary>
        /// Gets or sets the minimum house position taking.
        /// </summary>
        /// <value>The minimum house position taking.</value>
        public decimal MinHousePt { get; set; }

        /// <summary>
        /// Gets or sets the bet types.
        /// </summary>
        /// <value>The bet types.</value>
        public List<Bettype> Bettypes { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is apply preset position taking.
        /// </summary>
        /// <value><c>true</c> if this instance is apply preset position taking; otherwise, <c>false</c>.</value>
        public bool IsApplyPresetPT { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [update minimum maximum].
        /// </summary>
        /// <value><c>true</c> if [update minimum maximum]; otherwise, <c>false</c>.</value>
        public bool UpdateMinMax { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [update position taking].
        /// </summary>
        /// <value><c>true</c> if [update position taking]; otherwise, <c>false</c>.</value>
        public bool UpdatePT { get; set; }
    }
}