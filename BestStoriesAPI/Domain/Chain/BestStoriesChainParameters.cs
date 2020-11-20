using Domain.Acl.Entities;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.Chain
{
    public class BestStoriesChainParameters
    {
        public IDStoriesResponse IDStoriesResponse { get; set; }
        
        public List<StoriesDescriptionResponse> StoriesDescriptionResponseList { get; set; }
    }
}
