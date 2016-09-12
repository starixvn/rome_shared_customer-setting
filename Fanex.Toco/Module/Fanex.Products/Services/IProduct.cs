using System.Collections;

namespace Fanex.Products.Services
{
    public interface IProduct
    {
        bool Active { get; }

        bool Disabled { get; }

        bool? Synced { get; }

        bool ShowReport { get; }

        bool ShowDCS { get; }

        void SetProductStatusToSession(Hashtable outputParams);
    }
}
