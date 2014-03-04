// -----------------------------------------------------------------------
// <copyright file="KenoSettingBaseRepositoryTests.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Module.Keno.Tests.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Keno.Repositories;
    using Fanex.NAP.Framework.Data;
    using Fanex.NAP.Framework.Exceptions;
    using NUnit.Framework;
    using Rhino.Mocks;

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
        public void UpdateSetting_CriteriaIsNotValid_ThrowsException()
        {
            // Arrange
            var mocks = new MockRepository();
            var criteria = mocks.DynamicMock<ICriteria>();

            using (mocks.Record())
            {
                criteria.IsValid();
                LastCall.Return(false);
            }

            var kenoSettingBaseRepository = new KenoSettingBaseRepository();

            // Act
            kenoSettingBaseRepository.UpdateSetting(criteria);
        }

        [Test]
        public void UpdateSetting_CreateMockCriteria_ReturnTrue()
        {
            // Arrange
            var mocks = new MockRepository();
            var criteria = mocks.DynamicMock<ICriteria>();

            using (mocks.Record())
            {
                criteria.IsValid();
                LastCall.Return(true);
            }

            // Act
            var isValid = criteria.IsValid();

            // Assert
            Assert.AreEqual(true, isValid);
        }
    }
}
