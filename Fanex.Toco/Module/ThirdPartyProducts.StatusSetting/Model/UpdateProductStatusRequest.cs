using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThirdPartyProducts.StatusSetting.Model
{
    public class UpdateProductStatusRequest
    {
        public int CustId { get; set; }

        public int SubAccId { get; set; }

        public int UplineId { get; set; }

        public int ProductId { get; set; }

        public int UserId { get; set; }

        public int SystemId { get; set; }

        public string Ip { get; set; }

        public List<ProductStatus> ProductStatus { get; set; }

        public bool IsDeposit { get; set; }
    }
}
