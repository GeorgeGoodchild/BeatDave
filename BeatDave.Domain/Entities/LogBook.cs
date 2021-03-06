
namespace BeatDave.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Linq;
    using BeatDave.Domain.Infrastructure;
    using Raven.Imports.Newtonsoft.Json;

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
            var entries = this.Entries ?? new List<Entry>();

            return new ReadOnlyCollection<Entry>(entries);
        }

        public void AddEntry(Entry e)
        {            
            if (this.Entries == null) 
                this.Entries = new List<Entry>();

            e.LogBook = this;
            e.Id = this.Entries.Count == 0 ? 1 : this.Entries.Max(x => x.Id) + 1;

            this.Entries.Add(e);

            DomainEvents.Raise(new EntryLoggedEvent(this, e));
        }

        public void RemoveEntry(Entry e)
        {
            if (this.Entries == null)
                return;

            this.Entries.Remove(e);
        }


        public bool IsOwnedBy(string username)
        {
            return string.Equals(this.OwnerId, username, StringComparison.OrdinalIgnoreCase);
        }

        public bool IsVisibleTo(string username, Func<string, IEnumerable<Friend>> getOwnerFriends)
        {
            if (string.Equals(this.OwnerId, username, StringComparison.OrdinalIgnoreCase) == true)
                return true;

            if (this.Visibility == Domain.Visibility.Public || this.Visibility == Domain.Visibility.PublicAnonymous)
                return true;

            var confirmedFriends = getOwnerFriends(this.OwnerId).Where(x => x.Confirmed)
                                                                .Select(x => x.FriendUsername);

            if (this.Visibility == Domain.Visibility.FriendsOnly && confirmedFriends.Contains(username) == true)
                return true;

            return false;
        }
    }
}