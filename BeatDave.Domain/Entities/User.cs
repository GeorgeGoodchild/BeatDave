
namespace BeatDave.Domain
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class User
    {
        // Properties
        public string Id { get; set; }
        public string Username { get { return this.Id; } }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AspNetId { get; set; }
        private List<ISocialNetworkAccount> SocialNetworkAccounts { get; set; }
        private List<Friend> Friends { get; set; }


        // C'tor
        public User()
        { }


        // Public Members
        public IEnumerable<Friend> GetFriends()
        {
            var friends = this.Friends ?? new List<Friend>();

            return new ReadOnlyCollection<Friend>(friends);
        }    
    }
}