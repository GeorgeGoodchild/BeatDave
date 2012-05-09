
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
    public class LogBook
    {
        // Properties
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<string> Tags { get; set; }

        public Units Units { get; set; }
        private List<Record> Records { get; set; }

        public string OwnerId { get; set; }
        public List<ISocialNetworkAccount> AutoShareOn { get; set; }
        public Visibility Visibility { get; set; }


        // C'tor
        public LogBook()
        { }


        // Public Members
        public IEnumerable<Record> GetRecords()
        {
            if (this.Records == null)
                this.Records = new List<Record>();

            return new ReadOnlyCollection<Record>(this.Records);
        }

        public void AddRecord(Record r)
        {            
            if (this.Records == null) 
                this.Records = new List<Record>();

            r.LogBook = this;
            r.Id = this.Records.Count == 0 ? 1 : this.Records.Max(x => x.Id) + 1;

            this.Records.Add(r);
        }

        public void RemoveRecord(Record r)
        {
            if (this.Records == null)
                return;

            this.Records.Remove(r);
        }
    }
}