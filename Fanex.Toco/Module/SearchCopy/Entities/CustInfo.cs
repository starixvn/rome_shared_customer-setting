namespace Fanex.CustomerSetting.Module.SearchCopy.Entities
{
    public class CustInfo
    {
        public enum Roles
        {
            Super = 4,
            Master = 3,
            Agent = 2,
            Member = 1,
            SuperOnMember = -1,
            MasterOnMember = -2,
            SuperMin = -3,
            MasterMinMax = -4,
            AgentMinMax = -5
        }
        public int CustId { get; set; }
        public int CustLevel { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public string Site { get; set; }
        public string Fax { get; set; }
        public decimal Credit { get; set; }
        public bool IsInternal { get; set; }
        public int ExistingCustId { get; set; }
        public int Creator { get; set; }        
    }
}