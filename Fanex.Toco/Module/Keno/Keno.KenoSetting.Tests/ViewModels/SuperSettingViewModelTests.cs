// -----------------------------------------------------------------------
// <copyright file="SuperSettingViewModelTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/04">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Tests.ViewModels
{
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.ViewModels;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class SuperSettingViewModelTests
    {
        [Test]
        public void CopyToOnePositionTaking_ShouldMappingCorectly()
        {
            // Arrange
            var superSettingViewModel = new SuperSettingViewModel();
            var source = new SuperPositionTaking()
            {
                SportTypeId = 202,
                BetTypeId = 1,
                BetTypeName = "Bejing",
                S4 = 0.85,
                M4 = 0.6,
                A4 = 0.5,
                N4 = 0.2,
                L4 = 0.85,
                SComm = 0.0095,
                SCommMax = 0.01,
                IsUpdateN4 = true,
                IsUpdateS4 = false,
                IsUpdateSCo = true
            };
            var destination = new SuperPositionTaking();

            // Act
            superSettingViewModel.CopyToOnePositionTaking(source, destination);

            // Assert
            Assert.AreEqual(0.85, destination.S4);
            Assert.AreEqual(0.2, destination.N4);
            Assert.AreEqual(0.0095, destination.SComm);
            Assert.AreEqual(true, destination.IsUpdateN4);
            Assert.AreEqual(true, destination.IsUpdateS4);
            Assert.AreEqual(true, destination.IsUpdateSCo);
        }
    }
}
