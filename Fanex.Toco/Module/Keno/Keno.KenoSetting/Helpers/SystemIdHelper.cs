namespace Fanex.CustomerSetting.Module.Keno.KenoSetting.Helpers
{
    using System;
    using System.Configuration;

    internal static class SystemIdHelper
    {
        internal static byte GetFromConfigurationFile()
        {
            byte systemId;
            string systemIdInConfig = ConfigurationManager.AppSettings["SystemId"];

            if (string.IsNullOrWhiteSpace(systemIdInConfig) || !byte.TryParse(systemIdInConfig, out systemId))
            {
                throw new ArgumentException("SystemId is null, empty or not a integer number.", "SystemId");
            }

            return systemId;
        }
    }
}