
namespace BeatDave.Web.Models
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Newtonsoft.Json;

    public enum Visibility
    {
        [Description("Private")]            Private = 0,
        [Description("Friends Only")]       FriendsOnly = 1,
        [Description("Public Anonymous")]   PublicAnonymous = 2,
        [Description("Public")]             Public = 3
    }

    [JsonObject(IsReference = true)] 
    public class DataSet
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }
        
        public Units Units { get; set; }
        public List<DataPoint> DataPoints { get; set; }
        
        public string OwnerId { get; set; }
        public List<ISocialNetworkAccount> AutoShareOn { get; set; }        
        public Visibility Visibility { get; set; }
    }
}