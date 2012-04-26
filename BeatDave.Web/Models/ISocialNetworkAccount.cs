
namespace BeatDave.Web.Models
{
    public interface ISocialNetworkAccount
    {
        string SocialNetworkName { get; }
        bool Share(DataPoint dataPoint);
    }
}