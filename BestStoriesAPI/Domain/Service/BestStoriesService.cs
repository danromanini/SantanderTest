using Domain.Acl.Entities;
using Domain.Chain;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Patterns.ChainOfResponsability;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Service
{
    public class BestStoriesService : IBestStoriesService
    {
        private readonly IStoriesAcl _storiesAcl;

        public BestStoriesService(IStoriesAcl storiesAcl)
        {
            _storiesAcl = storiesAcl;
        }


        public async Task<List<StoriesDescriptionResponse>> GetBestStories()
        {
            var chain = new ChainBuilder<BestStoriesChainParameters, List<StoriesDescriptionResponse>>()
                        .Then(new StoriesIDsChain(_storiesAcl))
                        .Then(new StoriesDescriptionChain(_storiesAcl))
                        .Build();

            return await chain.Execute(new BestStoriesChainParameters {StoriesDescriptionResponseList = new List<StoriesDescriptionResponse>() });
        }
    }
}
