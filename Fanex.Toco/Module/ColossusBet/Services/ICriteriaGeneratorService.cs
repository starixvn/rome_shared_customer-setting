namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Services
{
    using Fanex.CustomerSetting.Module.ColossusBetSetting.Entities;
    using Fanex.NAP.Framework.Data;

    public interface ICriteriaGeneratorService
    {
        IColossusBetSession ColossusBetSession { get; }
    }
}