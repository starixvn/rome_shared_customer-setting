using Fanex.NAP.Framework.Data;
using System.Collections.Generic;
using ThirdPartyProducts.StatusSetting.Model;

namespace ThirdPartyProducts.StatusSetting.Factory.Product.Criteria
{
    internal class UpdateRacingStatusCriteria : CriteriaBase
    {
        public int CustId { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public string Language { get; set; }

        public string Ip { get; set; }

        public int SystemId { get; set; }

        public bool UpdateDisable151_31 { get; set; }

        public bool UpdateDisable151_41 { get; set; }

        public bool UpdateDisable151_51 { get; set; }

        public bool UpdateDisable152_41 { get; set; }

        public bool UpdateDisable152_51 { get; set; }

        public bool UpdateDisable153_51 { get; set; }

        public bool Disable151_31 { get; set; }

        public bool Disable151_41 { get; set; }

        public bool Disable151_51 { get; set; }

        public bool Disable152_41 { get; set; }

        public bool Disable152_51 { get; set; }

        public bool Disable153_51 { get; set; }

        public bool DisableDownlines151_31 { get; set; }

        public bool DisableDownlines151_41 { get; set; }

        public bool DisableDownlines151_51 { get; set; }

        public bool DisableDownlines152_41 { get; set; }

        public bool DisableDownlines152_51 { get; set; }

        public bool DisableDownlines153_51 { get; set; }

        public override string GetSettingKey()
        {
            return "Acc_CustomerUpdRBStatus";
        }

        public override bool IsValid()
        {
            return true;
        }

        public void MapSpParams(List<ProductStatus> productStatuses)
        {
            foreach (var productStatus in productStatuses)
            {
                MapProductStatusToSpParam(productStatus);
            }
        }

        public void MapProductStatusToSpParam(ProductStatus productStatus)
        {
            if (productStatus.SportType == 151)
            {
                if (productStatus.BetType == 31)
                {
                    Disable151_31 = productStatus.IsDisabled;
                    DisableDownlines151_31 = productStatus.IsDisabledDownline;
                    UpdateDisable151_31 = true;
                }
                else if (productStatus.BetType == 41)
                {
                    Disable151_41 = productStatus.IsDisabled;
                    DisableDownlines151_41 = productStatus.IsDisabledDownline;
                    UpdateDisable151_41 = true;
                }
                else
                {
                    Disable151_51 = productStatus.IsDisabled;
                    DisableDownlines151_51 = productStatus.IsDisabledDownline;
                    UpdateDisable151_51 = true;
                }
            }
            else if (productStatus.SportType == 152)
            {
                if (productStatus.BetType == 41)
                {
                    Disable152_41 = productStatus.IsDisabled;
                    DisableDownlines152_41 = productStatus.IsDisabledDownline;
                    UpdateDisable152_41 = true;
                }
                else
                {
                    Disable152_51 = productStatus.IsDisabled;
                    DisableDownlines152_51 = productStatus.IsDisabledDownline;
                    UpdateDisable152_51 = true;
                }
            }
            else
            {
                if (productStatus.BetType == 51)
                {
                    Disable153_51 = productStatus.IsDisabled;
                    DisableDownlines153_51 = productStatus.IsDisabledDownline;
                    UpdateDisable153_51 = true;
                }
            }
        }
    }
}
