
namespace BeatDave.Web.Models
{
    using System.Collections.Generic;

    public class Competition
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<string> Tags { get; set; }
        public List<TransforedLogBook> Competitors { get; set; }
        public Units CompetitionUnits { get; set; }
        public Visibility Visibility { get; set; }
    }
}