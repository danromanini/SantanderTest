using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Acl.Entities
{
    public class StoriesKids
    {
        [JsonProperty(PropertyName = "kids")]
        public List<int> kids { get; set; }
    }
}
