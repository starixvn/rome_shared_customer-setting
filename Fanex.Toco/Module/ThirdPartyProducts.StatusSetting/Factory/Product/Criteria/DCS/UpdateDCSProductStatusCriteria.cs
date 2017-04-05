using Fanex.NAP.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria.DCS
{
    internal class UpdateDCSProductStatusCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public string IsDisabled { get; set; }

        public int SubAccId { get; set; }

        public int UserId { get; set; }

        public string IP { get; set; }

        public int SystemId { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustSetting_DisableNewProductStatusUpd";
        }

        public override bool IsValid()
        {
            return true;
        }

        public void MapSPParams(List<ProductStatus> productStatus)
        {
            string productStatusFormat = "<ProductStatus SportType=\"{0}\" BetType=\"{1}\" IsDisabled=\"{2}\" IsDisabledDownline=\"{3}\" />";
            var writer = new StringBuilder();
            writer.Append("<ROOT>");
            foreach (var item in productStatus)
            {
                writer.Append(string.Format(productStatusFormat, item.SportType, item.BetType, Convert.ToInt32(item.IsDisabled), Convert.ToInt32(item.IsDisabledDownline)));
            }

            writer.Append("</ROOT>");
            this.IsDisabled = writer.ToString();
        }
    }
}
