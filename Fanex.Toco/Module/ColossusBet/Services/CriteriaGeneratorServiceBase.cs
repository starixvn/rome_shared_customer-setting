namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Services
{
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;

    /// <summary>
    /// Criteria Generator Service.
    /// </summary>
    public abstract class CriteriaGeneratorServiceBase
    {
        private const int _UserAdminId = 8;
        private readonly IColossusBetSession _colossusBet;
        private readonly CustomerInformation _customerInformation;

        protected CriteriaGeneratorServiceBase(IColossusBetSession colossusBet, CustomerInformation customerInformation)
        {
            _colossusBet = colossusBet ?? new ColossusBetSession();

            if (IsLocalTest(customerInformation.UserId))
            {
                customerInformation.UserId = _UserAdminId;
            }

            _customerInformation = customerInformation;
        }

        public IColossusBetSession KenoSession
        {
            get { return _colossusBet; }
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