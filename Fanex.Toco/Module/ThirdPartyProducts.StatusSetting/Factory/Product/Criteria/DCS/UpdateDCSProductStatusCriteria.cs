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

        public string Ip { get; set; }

        public int SystemId { get; set; }

        public bool IsDeposit { get; set; }

        public override string GetSettingKey()
        {
            return IsDeposit ? "Acc_Dep_CustSetting_DisableStatUpd" : "Acc_CustSetting_DisableNewProductStatusUpd";
        }

        public override bool IsValid()
        {
            return true;
        }

        public void MapSpParams(List<ProductStatus> productStatus)
        {
            string productStatusFormat = "<ProductStatus SportType=\"{0}\" BetType=\"{1}\" IsDisabled=\"{2}\" IsDisabledDownline=\"{3}\" />";
            var writer = new StringBuilder();
            writer.Append("<ROOT>");
            foreach (var item in productStatus)
            {
                var isDisabled = item.IsDisabled;

                writer.Append(
                    string.Format(
                        productStatusFormat, 
                        item.SportType, 
                        item.BetType, 
                        Convert.ToInt32(isDisabled), 
                        Convert.ToInt32(!isDisabled && item.IsDisabledDownline)));
            }

            writer.Append("</ROOT>");
            IsDisabled = writer.ToString();
        }
    }
}
