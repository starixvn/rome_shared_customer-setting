// --------------------------------------------------------------------------
// <copyright file="KenoSettingControllerTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;
    using Fanex.CustomerSetting.Module.Keno.Controllers;
    using Fanex.CustomerSetting.Module.Keno.Services;
    using Fanex.CustomerSetting.Module.Keno.ViewModels;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class KenoSettingControllerTests
    {
        [Test]
        public void EditSuper_CertainParams_AlwaysReturnActionResult()
        {
            // Arrange
            var mocks = new MockRepository();
            var kenoSettingService = mocks.Stub<IKenoSettingService>();
            var kenoSettingController = new KenoSettingController(kenoSettingService);

            // Act
            var result = kenoSettingController.EditSuper(1);

            // Assert
            Assert.IsInstanceOf<ActionResult>(result);
        }

        [Test]
        public void EditSuper_CertainParams_ViewContainsSuperSettingModel()
        {
            // Arrange
            var mocks = new MockRepository();
            var kenoSettingService = mocks.Stub<IKenoSettingService>();
            var kenoSettingController = new KenoSettingController(kenoSettingService);

            // Act
            var result = kenoSettingController.EditSuper(1);
            var model = ((ViewResult)result).Model;

            // Assert
            Assert.IsInstanceOf<SuperSetting>(model);
        }
    }
}
