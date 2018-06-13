namespace Fanex.CustomerSetting.Module.Casino.ViewModels
{
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
            this.CustId = custId;
            this.CustName = custName;
            this.UplineId = uplineId;
            this.IP = ip;
            this.ModifiedBy = modifiedBy;
            this.ModifiedId = modifiedId;
            this.FromSystem = fromSystem;
            this.SystemId = systemId;
            this.SettingModel = settingModel;
            this.CustomerModel = customerModel;
        }

        public int CustId { get; }

        public string CustName { get; }

        public int UplineId { get; }

        public string IP { get; }

        public int ModifiedId { get; }

        public string ModifiedBy { get; }

        public int FromSystem { get; }

        public byte SystemId { get; }

        public CasinoSettingModel SettingModel { get; }

        public GeneralCasinoCustomer CustomerModel { get; }
    }
}