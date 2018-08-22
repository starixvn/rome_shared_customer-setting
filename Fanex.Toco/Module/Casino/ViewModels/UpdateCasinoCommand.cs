namespace Fanex.CustomerSetting.Module.Casino.ViewModels
{
    using Core.Web;
    using Entities;

    public class UpdateCasinoCommand
    {
        public UpdateCasinoCommand(
            int custId,
            string custName,
            int uplineId,
            string ip,
            int modifiedId,
            string modifiedBy,
            int fromSystem,
            byte systemId,
            CasinoSettingModel settingModel,
            GeneralCasinoCustomer customerModel)
        {
            CustId = custId;
            CustName = custName;
            UplineId = uplineId;
            IP = ip;
            ModifiedBy = modifiedBy;
            ModifiedId = modifiedId;
            FromSystem = fromSystem;
            SystemId = systemId;
            SettingModel = settingModel;
            CustomerModel = customerModel;
            SubAccId = AppSession.SubAccountId;
            SubAccName = AppSession.SubAccountName;
        }

        public int CustId { get; }

        public string CustName { get; }

        public int UplineId { get; }

        public int SubAccId { get; }

        public string SubAccName { get; set; }

        public string IP { get; }

        public int ModifiedId { get; }

        public string ModifiedBy { get; }

        public int FromSystem { get; }

        public byte SystemId { get; }

        public CasinoSettingModel SettingModel { get; }

        public GeneralCasinoCustomer CustomerModel { get; }
    }
}