// <copyright file="PermissionServiceTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/03/07">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Core.Tests.Services
{
    using System.Collections.Generic;
    using Fanex.CustomerSetting.Module.Core.Services;
    using Fanex.NAP.Core.Business;
    using Fanex.NAP.Core.Model;
    using Fanex.NAP.Core.Model.ValueObject;
    using NUnit.Framework;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class PermissionServiceTests
    {
        [Test]
        public void GetRoleAccess_PolicyIsNone_ShouldReturnEmptyRoleAccesses()
        {
            // Arrange
            var permissionService = new PermissionService();
            var functionPolicies = new List<FunctionPolicy>() 
            {
                new FunctionPolicy()
                {
                    Policy = (int)Constant.Policy.None
                }
            };

            // Act
            var roleAccesses = permissionService.GetRoleAccess(functionPolicies);

            // Assert
            Assert.AreEqual(0, roleAccesses.Count);
        }

        [Test]
        public void GetRoleAccess_HavePermissions_ShouldCorrectlyReturnRoleAccesses()
        {
            // Arrange
            var permissionService = new PermissionService();
            var functionPolicies = new List<FunctionPolicy>() 
            {
                new FunctionPolicy()
                {
                    Policy = (int)Constant.Policy.Read,
                    Function = new Function() 
                    {
                        Key = "item1"
                    }
                },
                new FunctionPolicy()
                {
                    Policy = (int)Constant.Policy.Host,
                    Function = new Function() 
                    {
                        Key = "item2"
                    }
                }
            };

            // Act
            var roleAccesses = permissionService.GetRoleAccess(functionPolicies);

            // Assert
            Assert.AreEqual(2, roleAccesses.Count);
            Assert.AreEqual(1, roleAccesses["item1"]);
            Assert.AreEqual(2, roleAccesses["item2"]);
        }

        [Test]
        public void HasReadPermission_VerifyForCaseOfSuccess()
        {
            // Arrange
            var roleAccess = new Dictionary<string, int>();
            roleAccess.Add("item1", (int)Constant.Policy.Read);
            var permissionService = new PermissionService();

            // Act
            var result = permissionService.HasReadPermission(roleAccess, "item1");

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void HasReadPermission_RoleAccessIsNull_ShouldReturnFalse()
        {
            // Arrange
            Dictionary<string, int> roleAccess = null;
            var permissionService = new PermissionService();

            // Act
            var result = permissionService.HasReadPermission(roleAccess, "item1");

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void HasReadPermission_RoleAccessDontHaveExpectedFunction_ShouldReturnFalse()
        {
            // Arrange
            var roleAccess = new Dictionary<string, int>();
            roleAccess.Add("item1", (int)Constant.Policy.Read);
            var permissionService = new PermissionService();

            // Act
            var result = permissionService.HasReadPermission(roleAccess, "item2");

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void HasReadPermission_RoleAccessDontHaveReadPermission_ShouldReturnFalse()
        {
            // Arrange
            var roleAccess = new Dictionary<string, int>();
            roleAccess.Add("item1", (int)Constant.Policy.Host);
            var permissionService = new PermissionService();

            // Act
            var result = permissionService.HasReadPermission(roleAccess, "item1");

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}
