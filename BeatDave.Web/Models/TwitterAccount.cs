
namespace BeatDave.Web.Models
{
    public class TwitterAccount : ISocialNetworkAccount
    {
        public string SocialNetworkName
        {
            get { return "Twitter"; }
        }

        public bool Share(Entry entry)
        {
            throw new System.NotImplementedException();
        }        
    }
}