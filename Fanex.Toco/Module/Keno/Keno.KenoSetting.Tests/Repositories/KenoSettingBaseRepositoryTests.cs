// -----------------------------------------------------------------------
// <copyright file="KenoSettingBaseRepositoryTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/04">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Repositories;
    using Fanex.NAP.Framework.Data;
    using Fanex.NAP.Framework.Exceptions;
    using NUnit.Framework;
    using Rhino.Mocks;
    using Fanex.CustomerSetting.Module.Core.Models;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class KenoSettingBaseRepositoryTests
    {
        [Test]
        [ExpectedException(typeof(NapDataAccessException))]
        public void UpdateSetting_CriteriaIsNull_ThrowsException()
        {
            // Arrange
            var kenoSettingBaseRepository = new KenoSettingBaseRepository();

            // Act
            kenoSettingBaseRepository.UpdateSetting(null);
        }

        [Test]
        [ExpectedException(typeof(NapDataAccessException))]
        public void UpdateSetting_CriteriaIsNotValid_ThrowsExceptionWithUsingRecord()
        {
            // Arrange
            var mocks = new MockRepository();
            var mockCriteria = mocks.DynamicMock<ICriteria>();

            using (mocks.Record())
            {
                mockCriteria.IsValid();
                LastCall.Return(false);
            }

            var kenoSettingBaseRepository = new KenoSettingBaseRepository();

            // Act
            kenoSettingBaseRepository.UpdateSetting(mockCriteria);
        }

        [Test]
        [ExpectedException(typeof(NapDataAccessException))]
        public void UpdateSetting_CriteriaIsNotValid_ThrowsExceptionWithoutUsingRecord()
        {
            // Arrange
            var mockCriteria = MockRepository.GenerateMock<ICriteria>();
            mockCriteria.Stub(c => c.IsValid()).Return(false);
            var kenoSettingBaseRepository = new KenoSettingBaseRepository();

            // Act
            kenoSettingBaseRepository.UpdateSetting(mockCriteria);
        }

        [Test]
        public void UpdateSetting_CreateMockCriteria_ReturnTrue()
        {
            // Arrange
            var mockCriteria = MockRepository.GenerateMock<ICriteria>();
            mockCriteria.Stub(c => c.IsValid()).Return(true);

            // Act
            var isValid = mockCriteria.IsValid();

            // Assert
            Assert.AreEqual(true, isValid);
        }

        [Test]
        public void UpdateSetting_CriteriaIsValid_ShouldReturnResultObject()
        {
            // Arrange
            var mockCriteria = MockRepository.GenerateMock<ICriteria>();
            mockCriteria.Stub(c => c.IsValid()).Return(true);
            var kenoSettingBaseRepository = new TestableKenoSettingBaseRepository();

            // Act
            var result = kenoSettingBaseRepository.UpdateSetting(mockCriteria);

            // Assert
            Assert.IsInstanceOf<IList<Result>>(result);
            Assert.AreEqual(0, result[0].ErrorCode);
            Assert.AreEqual(string.Empty, result[0].ErrorMessage);
        }
    }
}
