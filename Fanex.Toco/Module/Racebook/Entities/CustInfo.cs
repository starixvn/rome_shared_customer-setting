// ************************************************************************************
// <copyright file="CustInfo.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) CustInfo.cs. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="kan.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************
namespace Fanex.CustomerSetting.Module.Racebook.Entities
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CustInfo
    {
        /// <summary>
        /// Role of customer.
        /// </summary>
        public enum Roles
        {
            /// <summary>
            /// The super.
            /// </summary>
            Super = 4,
            
            /// <summary>
            /// The master.
            /// </summary>
            Master = 3,
            
            /// <summary>
            /// The agent.
            /// </summary>
            Agent = 2,
            
            /// <summary>
            /// The member.
            /// </summary>
            Member = 1,
            
            /// <summary>
            /// The super on member.
            /// </summary>
            SuperOnMember = -1,
            
            /// <summary>
            /// The master on member.
            /// </summary>
            MasterOnMember = -2,
            
            /// <summary>
            /// The super min.
            /// </summary>
            SuperMin = -3,
            
            /// <summary>
            /// The master min max.
            /// </summary>
            MasterMinMax = -4,
            
            /// <summary>
            /// The agent min max.
            /// </summary>
            AgentMinMax = -5
        }

        public int CustId { get; set; }

        public int CustLevel { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public string Fax { get; set; }

        public int Currency { get; set; }
    }
}