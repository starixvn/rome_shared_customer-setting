// --------------------------------------------------------------------------
// <copyright file="KenoSettingRepositoryTests.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.IntegrationTests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using NUnit.Framework;
    using Rhino.Mocks;
    using Fanex.CustomerSetting.Module.Keno.Criteria;
    using Fanex.CustomerSetting.Module.Keno.Repositories;
    using Fanex.Data.Configuration;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [TestFixture]
    public class KenoSettingRepositoryTests
    {
        private KenoSettingRepository _kenoSettingRepository;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            DbSettingProviderManager.Current.Start();
        }

        [SetUp]
        public void SetUp()
        {
            _kenoSettingRepository = new KenoSettingRepository();
        }

        [TearDown]
        public void TearDown()
        {
            _kenoSettingRepository = null;
        }

        [Test]
        public void GetSuperSetting_ShouldRunNormally()
        {
            var criteria = new GetSuperSettingCriteria() { CustId = 1 };
            var result = _kenoSettingRepository.GetSuperSetting(criteria);
        }

        [Test]
        public void GetSettingInCaseOfAgentEditMember_ShouldRunNormally()
        {
            var criteria = new AgentGetMemberSettingCriteria() { CustId = 137459563 };
            var result = _kenoSettingRepository.GetSettingInCaseOfAgentEditMember(criteria);
        }
    }
}
