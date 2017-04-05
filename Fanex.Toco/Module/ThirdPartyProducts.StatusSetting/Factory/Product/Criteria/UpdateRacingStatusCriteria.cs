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

        public string IP { get; set; }

        public int SystemId { get; set; }

        public bool UpdateDisable151_31
        {
            get { return true; }
        }

        public bool UpdateDisable151_41
        {
            get { return true; }
        }

        public bool UpdateDisable151_51
        {
            get { return true; }
        }

        public bool UpdateDisable152_41
        {
            get { return true; }
        }

        public bool UpdateDisable152_51
        {
            get { return true; }
        }

        public bool UpdateDisable153_51
        {
            get { return true; }
        }

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

        public void MapSPParam(List<ProductStatus> productStatus)
        {
            foreach (var item in productStatus)
            {
                if (item.SportType == 151)
                {
                    if (item.BetType == 31)
                    {
                        this.Disable151_31 = item.IsDisabled;
                        this.DisableDownlines151_31 = item.IsDisabledDownline;
                    }
                    else if (item.BetType == 41)
                    {
                        this.Disable151_41 = item.IsDisabled;
                        this.DisableDownlines151_41 = item.IsDisabledDownline;
                    }
                    else if (item.BetType == 51)
                    {
                        this.Disable151_51 = item.IsDisabled;
                        this.DisableDownlines151_51 = item.IsDisabledDownline;
                    }
                }
                else if (item.SportType == 152)
                {
                    if (item.BetType == 41)
                    {
                        this.Disable152_41 = item.IsDisabled;
                        this.DisableDownlines152_41 = item.IsDisabledDownline;
                    }
                    else if (item.BetType == 51)
                    {
                        this.Disable152_51 = item.IsDisabled;
                        this.DisableDownlines152_51 = item.IsDisabledDownline;
                    }
                }
                else if (item.SportType == 153)
                {
                    if (item.BetType == 51)
                    {
                        this.Disable153_51 = item.IsDisabled;
                        this.DisableDownlines153_51 = item.IsDisabledDownline;
                    }
                }
            }
        }
    }
}
