namespace Fanex.Module.NG2.Entities
{
    public class Customer
    {
        public int CustId { get; set; }

        public string Username { get; set; }

        public bool IsSyncInMBC { get; set; }

        public bool IsSyncIn3rd { get; set; }
    }
}