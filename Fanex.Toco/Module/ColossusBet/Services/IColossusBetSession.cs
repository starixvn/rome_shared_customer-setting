namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Services
{
    /// <summary>
    /// Bound the app session from core.
    /// </summary>
    public interface IColossusBetSession
    {
        int UserId { get; }

        int SubAccId { get; }

        string Ip { get; }

        string Language { get; }

        bool IsLocalTest { get; }
    }
}