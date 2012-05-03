
namespace BeatDave.Web.Models
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
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
        private List<DataPoint> DataPoints { get; set; }
        
        public string OwnerId { get; set; }
        public List<ISocialNetworkAccount> AutoShareOn { get; set; }        
        public Visibility Visibility { get; set; }

        // C'tor
        public DataSet()
        { }


        // Public Members
        public void AddDataPoint(DataPoint dp)
        {            
            if (this.DataPoints == null) 
                this.DataPoints = new List<DataPoint>();

            dp.DataSet = this;
            dp.Id = this.DataPoints.Count == 0 ? 1 : this.DataPoints.Max(x => x.Id) + 1;

            this.DataPoints.Add(dp);
        }

        public IEnumerable<DataPoint> GetDataPoints()
        {
            if (this.DataPoints == null)
                this.DataPoints = new List<DataPoint>();

            return new ReadOnlyCollection<DataPoint>(this.DataPoints);
        }
    }
}