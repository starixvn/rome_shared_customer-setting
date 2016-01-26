namespace Fanex.Module.RNGCasino.Configurations
{
    using System.Configuration;

    public class OWRNGCasinoSection : ConfigurationSection
    {
        public static OWRNGCasinoSection Instance
        {
            get
            {
                var section = ConfigurationManager.GetSection("OWRNGCasinoSection") as OWRNGCasinoSection;
                return section;
            }
        }

        [ConfigurationProperty("Configurations", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(OWRNGCasinoCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public OWRNGCasinoCollection Configurations
        {
            get
            {
                return (OWRNGCasinoCollection)base["Configurations"];
            }
        }
    }
}
