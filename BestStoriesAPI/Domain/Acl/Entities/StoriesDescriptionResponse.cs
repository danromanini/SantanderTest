using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domain.Acl.Entities
{
    
    public class StoriesDescriptionResponse
    {
        [JsonProperty(PropertyName = "by")]
        public string by { get; set; }

        [JsonProperty(PropertyName = "descendants")]
        public int descendants { get; set; }

        [JsonProperty(PropertyName = "id")]
        public int id { get; set; }

        [JsonProperty(PropertyName = "kids")]
        public List<int> kids { get; set; }

        [JsonProperty(PropertyName = "score")]
        public int score { get; set; }

        [JsonProperty(PropertyName = "time")]
        public int time { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string title { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string type { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string url { get; set; }
    }
}
