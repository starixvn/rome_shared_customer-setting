// -----------------------------------------------------------------------
// <copyright file="GroupConfigurations.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Fanex.CustomerSetting.Security.Authenticate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Fanex.CustomerSetting.Module.Core;
    using Fanex.CustomerSetting.Security.Configurations;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class GroupConfigurations
    {
        public static IList<int> AllowEditSportsbookBettingGroups 
        {
            get
            {
                var allowGroups = CustomerSettingSection.Instance.AllowGroups.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

                if (allowGroups != null && allowGroups.Count() > 0)
                {
                    var allows = new List<int>();

                    foreach (var item in allowGroups)
                    {
                        allows.Add(Utils.ToInt32(item));
                    }

                    return allows;
                }

                return null;
            }
        }
    }
}
