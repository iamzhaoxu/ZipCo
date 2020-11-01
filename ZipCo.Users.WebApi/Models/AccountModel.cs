namespace ZipCo.Users.WebApi.Models
{
    public class AccountModel
    {
        /// <summary>
        /// Account Id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Account string identifier. Start with prefix ZIP then follow with digital number
        /// </summary>
        public string AccountNumber { get; set; }
        /// <summary>
        /// Account status. Either Active or Closed
        /// </summary>
        public string AccountStatus { get; set; }
        /// <summary>
        /// Available fund
        /// </summary>
        public decimal AvailableBalance { get; set; }
        /// <summary>
        /// The current account balance, excluding pending balance
        /// </summary>
        public decimal AccountBalance { get; set; }
        /// <summary>
        /// The fund which is still pending for settlement
        /// </summary>
        public decimal PendingBalance { get; set; }

    }
}
