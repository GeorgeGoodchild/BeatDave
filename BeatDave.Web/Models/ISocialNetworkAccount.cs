
namespace BeatDave.Web.Models
{
    public interface ISocialNetworkAccount
    {
        string SocialNetworkName { get; }
        bool Share(Record record);
    }
}