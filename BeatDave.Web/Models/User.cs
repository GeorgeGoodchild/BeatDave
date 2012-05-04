
namespace BeatDave.Web.Models
{
    using System.Collections.Generic;

    public class User
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AspNetId { get; set; }
        public List<ISocialNetworkAccount> SocialNetworkAccounts { get; set; }
        public List<Friend> Friends { get; set; }
    }
}