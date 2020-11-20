using Domain.Acl.Entities;
using Domain.Entities;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Chain
{
    public class StoriesDescriptionChain : IChain<BestStoriesChainParameters, List<StoriesDescriptionResponse>>
    {
        public IChain<BestStoriesChainParameters, List<StoriesDescriptionResponse>> Next { get; set; }

        private readonly IStoriesAcl _storiesAcl;

        public StoriesDescriptionChain(IStoriesAcl storiesAcl)
        {
            _storiesAcl = storiesAcl;
        }

        public async Task<List<StoriesDescriptionResponse>> Execute(BestStoriesChainParameters chainParam)
        {
            if(chainParam.IDStoriesResponse != null)
            {
                var ListIds = chainParam.IDStoriesResponse;

                Parallel.ForEach(ListIds.IDStoriesList, (id) =>
                {
                    var result = _storiesAcl.GetStoriesDescription(id);
                    if (result.Result != null)
                    {
                        chainParam.StoriesDescriptionResponseList.Add(result.Result);
                    }

                });
            }
            
            if(chainParam.StoriesDescriptionResponseList == null || chainParam.IDStoriesResponse == null)
            {
                return null;
            }

            return chainParam.StoriesDescriptionResponseList;

        }
    }
}
