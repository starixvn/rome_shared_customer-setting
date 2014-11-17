// --------------------------------------------------------------------------
// <copyright file="CriteriaGeneratorServiceBase.cs" company="Nexcel Solutions Vietnam">
// Copyright (c) Nexcel Solutions Vietnam. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------
// <history>
//   <change who="Paul.Nguyen" date="2014/02/20">Created.</change>
// </history>

namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Services
{
    using Fanex.CustomerSetting.Module.Keno.KenoSetting.Entities;

    /// <summary>
    /// Criteria Generator Service.
    /// </summary>
    public abstract class CriteriaGeneratorServiceBase
    {
        private const int _UserAdminId = 8;
        private readonly IKenoSession _kenoSession;
        private readonly CustomerInformation _customerInformation;

        protected CriteriaGeneratorServiceBase(IKenoSession kenoSession, CustomerInformation customerInformation)
        {
            _kenoSession = kenoSession ?? new KenoSession();

            if (IsLocalTest(customerInformation.UserId))
            {
                customerInformation.UserId = _UserAdminId;
            }

            _customerInformation = customerInformation;
        }

        public IKenoSession KenoSession
        {
            get { return _kenoSession; }
        }

        public CustomerInformation CustomerInformation
        {
            get { return _customerInformation; }
        }

        internal bool IsLocalTest(int userId)
        {
            return userId <= 0 && KenoSession.IsLocalTest;
        }
    }
}