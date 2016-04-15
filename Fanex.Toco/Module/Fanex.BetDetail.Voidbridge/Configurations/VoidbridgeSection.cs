namespace Fanex.Voidbridge.BetDetail.Configurations
{
    using System.Configuration;

    public class OWVoidbridgeSection : ConfigurationSection
    {
        public static OWVoidbridgeSection Instance
        {
            get
            {
                var section = ConfigurationManager.GetSection("OWVoidbridgeSection") as OWVoidbridgeSection;
                return section;
            }
        }

        [ConfigurationProperty("Configurations", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(OWVoidbridgeCollection),
            AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public OWVoidbridgeCollection Configurations
        {
            get
            {
                return (OWVoidbridgeCollection)base["Configurations"];
            }
        }
    }
}
