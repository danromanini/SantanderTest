using Domain.Acl.Entities;
using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IBestStoriesService
    {
        Task<List<StoriesDescriptionResponse>> GetBestStories();
    }
}
