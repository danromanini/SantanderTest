using Domain.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Domain.Acl.Entities
{
    public class IDStoriesResponse
    {
        //[JsonProperty(PropertyName = "Data")]
        public List<long> IDStoriesList { get; set; }
    }
}
