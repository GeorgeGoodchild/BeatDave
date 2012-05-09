
namespace BeatDave.Web.Models
{
    public class FacebookAccount : ISocialNetworkAccount
    {
        public string SocialNetworkName
        {
            get { return "Facebook"; }
        }

        public bool Share(Record record)
        {
            throw new System.NotImplementedException();
        }
    }
}