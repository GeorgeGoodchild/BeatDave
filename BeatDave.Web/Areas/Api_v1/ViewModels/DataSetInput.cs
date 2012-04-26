using System.Collections.Generic;

namespace BeatDave.Web.Areas.Api_v1.ViewModels
{
    public class DataSetInput
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Units Units { get; set; }
        public List<DataPoint> DataPoints { get; set; }
        public List<string> Tags { get; set; }
        public List<ISocialNetworkAccount> AutoShareOn { get; set; }
        public string OwnerId { get; set; }
        public Visibility Visibility { get; set; }
    }
}