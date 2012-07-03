
namespace BeatDave.Domain
{
    public class TwitterAccount : ISocialNetworkAccount
    {
        public string SocialNetworkName
        {
            get { return "Twitter"; }
        }

        public bool Share(LogBookEntry entry)
        {
            throw new System.NotImplementedException();
        }        
    }
}