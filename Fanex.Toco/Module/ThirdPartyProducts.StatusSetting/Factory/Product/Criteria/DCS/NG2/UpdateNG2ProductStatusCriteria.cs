using Fanex.NAP.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria.DCS
{
    internal class UpdateNG2ProductStatusCriteria : UpdateDCSProductStatusCriteria
    {
        public override string GetSettingKey()
        {
            return "Acc_CustSetting_DisableGameStatusUpd";
        }
    }
}
