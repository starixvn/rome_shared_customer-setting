// ************************************************************************************
// <copyright file="BaseRacingController.cs" company="Nexcel Solutions Vietnam">
//     Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// ************************************************************************************
// <history>
//     <change who="steven.nguyen">Create/Run StyleCop.</change>
// </history>
// ************************************************************************************

using System;

namespace Fanex.CustomerSetting.Module.Racebook.Controllers
{
    using Core.Web.Controllers;
    using Entities;

    /// <summary>
    /// Base racing controller.
    /// </summary>
    public class BaseRacingController : BaseController
    {
        public BaseRacingController()
        {
            string custId = Fanex.Common.Converter.ToString2((Request["custid"]));
            ViewBag.PageRole = GetPageRole(custId);
        }

        protected PageRole.Roles CurrentRole { get; set; }

        /// <summary>
        /// Gets the page role.
        /// </summary>
        /// <param name="custId">The customer identifier.</param>
        /// <returns>Return System.String.</returns>
        private string GetPageRole(string custId)
        {
            var pageRole = new PageRole { CurrRole = Convert.ToInt16(CurrentRole) };
            pageRole.PageCrypt = IBCCrypt.IBCCrypt.GenMD5Key(custId + pageRole.CurrRole.ToString()) + IBCCrypt.IBCCrypt.GenMD5Key("^") + custId + pageRole.CurrRole.ToString();
            //jsonData.Message = GetRacingMessage();

            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            var jsonData = serializer.Serialize(pageRole);
            return jsonData;
        }
    }
}
