using Domain.Acl.Entities;
using Domain.Entities;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class BestStoriesDTO
    {
        public List<StoriesDescriptionResponse> storiesDescriptionResponseList { get; set; }
    }
}
