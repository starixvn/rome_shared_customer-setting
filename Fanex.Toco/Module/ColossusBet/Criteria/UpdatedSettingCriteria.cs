namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Criteria
{
    using System;
    using System.Configuration;
    using Entities;
    using NAP.Framework.Data;

    public class UpdatedSettingCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public bool IsUpdateCommission { get; set; }

        public bool IsUpdateCashInShare { get; set; }

        public bool IsUpdateDisable { get; set; }

        public decimal V204Commission_18001 { get; set; }

        public decimal V204CashInShare_18002 { get; set; }

        public object EnableDownlines { get; set; }

        public bool Disable { get; set; }

        public string IP { get; set; }

        public int UserID { get; set; }

        public string Language { get; set; }

        public int SystemId
        {
            get
            {
                int systemId;
                string systemIdInConfig = ConfigurationManager.AppSettings["SystemId"];

                if (string.IsNullOrWhiteSpace(systemIdInConfig) || !int.TryParse(systemIdInConfig, out systemId))
                {
                    throw new ArgumentException("SystemId is null, empty or not a integer number.", "SystemId");
                }

                return systemId;
            }
        }

        public override string GetSettingKey()
        {
            return Constants.GetUpdatedColossusBetCriteriaKeyName;
        }

        public override bool IsValid()
        {
            return this.CustId > 0;
        }
    }
}