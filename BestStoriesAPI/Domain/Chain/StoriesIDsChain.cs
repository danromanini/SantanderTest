using Domain.Acl.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Chain
{
    public class StoriesIDsChain : IChain<BestStoriesChainParameters, List<StoriesDescriptionResponse>>
    {
        public IChain<BestStoriesChainParameters, List<StoriesDescriptionResponse>> Next { get; set; }

        private readonly IStoriesAcl _storiesAcl; 

        public StoriesIDsChain(IStoriesAcl storiesAcl)
        {
            _storiesAcl = storiesAcl;
        }

        public async Task<List<StoriesDescriptionResponse>> Execute(BestStoriesChainParameters chainParam)
        {
            chainParam.IDStoriesResponse = await _storiesAcl.GetStoriesIDs();

            if (chainParam.IDStoriesResponse == null)
            {
                return chainParam.StoriesDescriptionResponseList;
            }

            return await Next.Execute(chainParam);
        }
    }
}
