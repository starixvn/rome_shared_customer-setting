namespace Fanex.CustomerSetting.Module.ColossusBetSetting.Entities
{
    /// <summary>
    /// The CustomerInformation entity.
    /// </summary>
    public class CustomerInformation
    {
        public int CustId { get; set; }

        public int UplineId { get; set; }

        public int RoleId { get; set; }

        public int ActionType { get; set; }

        public string Username { get; set; }

        public string NickName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public string Fax { get; set; }

        public string Language { get; set; }

        public string Ip { get; set; }

        public int UserId { get; set; }

        public int SubAccId { get; set; }

        public bool IsMultiple { get; set; }
    }
}