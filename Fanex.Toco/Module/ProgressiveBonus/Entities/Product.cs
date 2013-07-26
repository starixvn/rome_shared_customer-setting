using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fanex.CustomerSetting.Module.ProgressiveModule.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Enable { get; set; }

        public bool EnableDownlines { get; set; }

        public bool EnableButton { get; set; }

        public bool EnableDownlinesButton { get; set; }
    }
    
    public class Progressive
    {
        public Progressive()
        {
            UserName = string.Empty;
            AdminUserName = string.Empty;

            Products = new List<Product>();
        }

        public int CustId { get; set; }

        public string UserName { get; set; }

        public int RoleId { get; set; }

        public int AdminId { get; set; }

        public string AdminUserName { get; set; }

        public List<Product> Products { get; set; }
    }

}