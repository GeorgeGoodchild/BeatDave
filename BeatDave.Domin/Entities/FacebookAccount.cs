
namespace BeatDave.Domain
{
    public class FacebookAccount : ISocialNetworkAccount
    {
        public string SocialNetworkName
        {
            get { return "Facebook"; }
        }

        public bool Share(Entry entry)
        {
            throw new System.NotImplementedException();
        }
    }
}