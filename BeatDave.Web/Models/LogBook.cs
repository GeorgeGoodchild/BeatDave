
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
        private List<Entry> Entries { get; set; }

        public string OwnerId { get; set; }
        public List<ISocialNetworkAccount> AutoShareOn { get; set; }
        public Visibility Visibility { get; set; }


        // C'tor
        public LogBook()
        { }


        // Public Members
        public IEnumerable<Entry> GetEntries()
        {
            if (this.Entries == null)
                this.Entries = new List<Entry>();

            return new ReadOnlyCollection<Entry>(this.Entries);
        }

        public void AddEntry(Entry e)
        {            
            if (this.Entries == null) 
                this.Entries = new List<Entry>();

            e.LogBook = this;
            e.Id = this.Entries.Count == 0 ? 1 : this.Entries.Max(x => x.Id) + 1;

            this.Entries.Add(e);
        }

        public void RemoveEntry(Entry e)
        {
            if (this.Entries == null)
                return;

            this.Entries.Remove(e);
        }
    }
}