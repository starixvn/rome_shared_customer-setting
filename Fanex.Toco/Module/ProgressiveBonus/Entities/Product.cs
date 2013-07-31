// -----------------------------------------------------------------------
// <copyright file="Product.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
// <summary>
//  Product
// </summary>

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Entities
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// Product progressive
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
    public class Product
    {
        /// <summary>
        /// Gets or sets the Id of the product. 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product. 
        /// </summary>
        public string Name { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether enable or not for a customer
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether enable or not for a downline customer 
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public bool EnableDownlines { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether enable button or not
        /// </summary>
        public bool EnableButton { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether enable button downline or not
        /// </summary>
         [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
        public bool EnableDownlinesButton { get; set; }
    }
    
    /// <summary>
    /// Progressive module
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed.")]
    public class Progressive
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Progressive"/> class.
        /// </summary>
        public Progressive()
        {
            this.UserName = string.Empty;
            this.AdminUserName = string.Empty;

            this.Products = new List<Product>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether customer id
        /// </summary>
        public int CustId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customer username
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether customer role id
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether admin id
        /// </summary>
        public int AdminId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether admin role id
        /// </summary>
        [SuppressMessage("StyleCop.CSharp.SpacingRules", "SA1027:TabsMustNotBeUsed", Justification = "Reviewed.")]
		public int AdminRoleId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether admin username
        /// </summary>
        public string AdminUserName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether product list
        /// </summary>
        public List<Product> Products { get; set; }
    }
}