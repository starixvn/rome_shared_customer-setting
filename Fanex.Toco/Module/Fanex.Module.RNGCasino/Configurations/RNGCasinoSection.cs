namespace Fanex.Module.RNGCasino.Configurations
{
    using System.Configuration;

    public class RNGCasinoSection : ConfigurationSection
    {
        public static RNGCasinoSection Instance
        {
            get
            {
                var section = ConfigurationManager.GetSection("RNGCasinoSection") as RNGCasinoSection;
                return section;
            }
        }

        [ConfigurationProperty("Configurations", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(RNGCasinoCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public RNGCasinoCollection Configurations
        {
            get
            {
                return (RNGCasinoCollection)base["Configurations"];
            }
        }
    }
}
