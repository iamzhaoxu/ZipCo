using System.ComponentModel.DataAnnotations;

namespace ZipCo.Users.WebApi.Requests
{
    public class SignUpAccountRequest
    {
        /// <summary>
        /// When sign up an account for the member, a member id is required
        /// </summary>
        [Required]
        public long MemberId { get; set; }
    }
}
