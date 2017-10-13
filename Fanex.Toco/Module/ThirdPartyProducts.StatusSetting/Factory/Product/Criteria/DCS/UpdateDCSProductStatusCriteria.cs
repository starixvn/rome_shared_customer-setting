using Fanex.NAP.Framework.Data;
using System;
using System.Collections.Generic;
using System.Text;
using ThirdPartyProducts.StatusSetting.Model;
using System.Linq;
using System.Xml.Linq;

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
            => IsDeposit ? "Acc_Dep_CustSetting_DisableStatUpd" : "Acc_CustSetting_DisableNewProductStatusUpd";

        public override bool IsValid() => true;

        public void MapSpParams(List<ProductStatus> products)
        {
            var rootElement = new XElement("ROOT");

            rootElement.Add(products.Select(product => BuildProductStatusElement(product)));
           
            this.IsDisabled = rootElement.ToString();
        }

        public static XElement BuildProductStatusElement(ProductStatus productStatus)
        {
            var productStatusElement = new XElement("ProductStatus");
            productStatusElement.Add(new XAttribute("SportType", productStatus.SportType));
            productStatusElement.Add(new XAttribute("BetType", productStatus.BetType));
            productStatusElement.Add(new XAttribute("IsDisabled", Convert.ToInt32(productStatus.IsDisabled)));

            if (productStatus.HasDownline)
            {
                productStatusElement.Add(new XAttribute("IsDisabledDownline", Convert.ToInt32(productStatus.IsDisabled && productStatus.IsDisabledDownline)));
            }

            return productStatusElement;
        }
    }
}
