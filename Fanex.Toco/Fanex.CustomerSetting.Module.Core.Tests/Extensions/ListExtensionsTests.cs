// --------------------------------------------------------------------------
// <copyright file="ListExtensionsTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Tests.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Core.Extensions;
    using NUnit.Framework;
    using Rhino.Mocks;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class ListExtensionsTests
    {
        [Test]
        public void SortByProperty_InputParamsAreInvalid_DataSourceShouldNotBeChanged()
        {
            // Arrange
            List<FakeObject> nullList = null;
            var emptyList = new List<FakeObject>();
            var validListInCaseInvalidSortColumn = GenerateFakeObjects();
            var validListInCaseInvalidSortDirection = GenerateFakeObjects();

            // Act
            ListExtensions.SortByProperty(nullList, string.Empty, string.Empty);
            ListExtensions.SortByProperty(emptyList, string.Empty, string.Empty);
            ListExtensions.SortByProperty(validListInCaseInvalidSortColumn, ListExtensions.ASC, "CustId1");
            ListExtensions.SortByProperty(validListInCaseInvalidSortDirection, "wrongDirection", "CustId");

            // Assert
            Assert.IsNull(nullList);
            Assert.AreEqual(0, emptyList.Count);
            Assert.AreEqual(2, validListInCaseInvalidSortColumn.Count);

            Assert.AreEqual(1, validListInCaseInvalidSortColumn[0].CustId);
            Assert.AreEqual("Paul", validListInCaseInvalidSortColumn[0].Name);
            Assert.AreEqual("ABC", validListInCaseInvalidSortColumn[1].Name);
            Assert.AreEqual(2, validListInCaseInvalidSortColumn[1].CustId);

            Assert.AreEqual(1, validListInCaseInvalidSortDirection[0].CustId);
            Assert.AreEqual("Paul", validListInCaseInvalidSortDirection[0].Name);
            Assert.AreEqual("ABC", validListInCaseInvalidSortDirection[1].Name);
            Assert.AreEqual(2, validListInCaseInvalidSortDirection[1].CustId);
        }

        [Test]
        public void SortByProperty_ChangeSortDirection_ShouldReturnExactList()
        {
            // Arrange
            var ascList = GenerateFakeObjects();
            var descList = GenerateFakeObjects();

            // Act
            ListExtensions.SortByProperty(ascList, "asc", "CustId");
            ListExtensions.SortByProperty(descList, "desc", "CustId");

            // Assert
            Assert.AreEqual(1, ascList[0].CustId);
            Assert.AreEqual("Paul", ascList[0].Name);
            Assert.AreEqual("ABC", ascList[1].Name);
            Assert.AreEqual(2, ascList[1].CustId);

            Assert.AreEqual(2, descList[0].CustId);
            Assert.AreEqual("ABC", descList[0].Name);
            Assert.AreEqual("Paul", descList[1].Name);
            Assert.AreEqual(1, descList[1].CustId);
        }

        private class FakeObject
        {
            public int CustId { get; set; }

            public string Name { get; set; }
        }

        private List<FakeObject> GenerateFakeObjects()
        {
            return new List<FakeObject>() 
            {
                new FakeObject()
                {
                    CustId = 1,
                    Name = "Paul"
                },
                new FakeObject()
                {
                    CustId = 2,
                    Name = "ABC"
                }
            };
        }
    }
}
