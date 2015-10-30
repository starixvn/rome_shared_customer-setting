namespace Fanex.CustomerSetting.Module.ColossusBets.Configurations
{
    using System.Configuration;

    public class ColossusBetsSection : ConfigurationSection
    {
        public static ColossusBetsSection Instance
        {
            get
            {
                var section = ConfigurationManager.GetSection("ColossusBetsSection") as ColossusBetsSection;
                return section;
            }
        }

        [ConfigurationProperty("Configurations", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ColossusBetsCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public ColossusBetsCollection Configurations
        {
            get
            {
                return (ColossusBetsCollection)base["Configurations"];
            }
        }
    }
}
