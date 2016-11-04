using System;
using System.Diagnostics.CodeAnalysis;

namespace Fanex.CustomerSetting.Module.NG2.Core.Models
{
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Serializable]
    [SuppressMessage("Microsoft.StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented")]
    public class Bettypes:BaseModel
    {
        public int Type { get; set; }

        public bool LiveIndicator { get; set; }
    }
}
