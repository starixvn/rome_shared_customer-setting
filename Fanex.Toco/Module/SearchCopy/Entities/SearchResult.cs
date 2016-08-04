using System.Collections;

namespace Fanex.CustomerSetting.Module.SearchCopy.Entities
{
    public class SearchResult
    {
        public int CustId { get; set; }

        public int ParentId { get; set; }

        public string BreadCrumbs { get; set; }
    }
}