
namespace BeatDave.Web.Models
{
    public class TwitterAccount : ISocialNetworkAccount
    {
        public string SocialNetworkName
        {
            get { return "Twitter"; }
        }

        public bool Share(DataPoint dataPoint)
        {
            throw new System.NotImplementedException();
        }        
    }
}