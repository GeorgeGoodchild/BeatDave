﻿
namespace BeatDave.Web.Models
{
    using System.Collections.Generic;

    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string EmailVerificationCode { get; set; }
        public bool EmailVerified { get; set; }
        public List<ISocialNetworkAccount> SocialNetworkAccounts { get; set; }
        public List<Friend> Friends { get; set; }
    }
}