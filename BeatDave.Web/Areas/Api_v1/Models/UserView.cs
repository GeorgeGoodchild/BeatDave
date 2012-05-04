using System.Collections.Generic;

namespace BeatDave.Web.Areas.Api_v1.Models
{
    public class UserView
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return (this.FirstName + " " + this.LastName).Trim(); } }
        public string Email { get; set; }
        public List<FriendView> Friends { get; set; }

        //
        // Nested Inner Classes
        //
        public class FriendView
        {
            public int FriendUsername { get; set; }
            public string FriendFullName { get; set; }
        }
    }
}