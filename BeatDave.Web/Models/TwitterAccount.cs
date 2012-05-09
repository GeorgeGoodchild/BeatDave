
namespace BeatDave.Web.Models
{
    public class TwitterAccount : ISocialNetworkAccount
    {
        public string SocialNetworkName
        {
            get { return "Twitter"; }
        }

        public bool Share(Record record)
        {
            throw new System.NotImplementedException();
        }        
    }
}