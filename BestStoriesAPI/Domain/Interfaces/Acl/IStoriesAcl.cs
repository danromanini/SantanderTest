using Domain.Acl.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IStoriesAcl
    {
        Task<IDStoriesResponse> GetStoriesIDs();

        Task<StoriesDescriptionResponse> GetStoriesDescription(long storiesID);

    }
}
