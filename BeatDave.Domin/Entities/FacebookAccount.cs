
namespace BeatDave.Domain
{
    public class FacebookAccount : ISocialNetworkAccount
    {
        public string SocialNetworkName
        {
            get { return "Facebook"; }
        }

        public bool Share(LogBookEntry entry)
        {
            throw new System.NotImplementedException();
        }
    }
}