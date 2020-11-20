using Domain.Acl.Entities;
using Domain.Entities;
using Domain.Interfaces;
using Flurl.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Acl
{
    public class StoriesAcl : IStoriesAcl
    {        
        private readonly IConfiguration _config;
        protected readonly IHttpService _httpService;
        protected readonly string _baseUrl;

        public StoriesAcl(IConfiguration config,
                          IHttpService httpService)
        {
            _config = config;
            _httpService = httpService;
        }


        public async Task<IDStoriesResponse> GetStoriesIDs()
        {
            IDStoriesResponse ResponseIDs = new IDStoriesResponse();
            var suffix = _config.GetValue<string>("sufix.url.listid.api", "v0/beststories.json");
            var urlApiBase = _config.GetValue<string>("base.url.api", "https://hacker-news.firebaseio.com/");

            var response = await _httpService.CallHttp(
                (url) => url
                    .WithHeader("Accept", "application/json")
                    .GetAsync()
                    .ReceiveJson<List<long>>(),
                    urlApiBase, string.Format(suffix));

            if(response.Result != null)
            {
                var first20 = response.Result.Take(20);
                ResponseIDs = new IDStoriesResponse { IDStoriesList = first20.ToList() };
            }
            
            return ResponseIDs;
            
        }

        public async Task<StoriesDescriptionResponse> GetStoriesDescription(long storiesID)
        {
            var suffix = _config.GetValue<string>("sufix.url.desc.api", "v0/item/{0}.json");
            var urlApiBase = _config.GetValue<string>("base.url.api", "https://hacker-news.firebaseio.com/");
            StoriesDescriptionResponse StoriesDescriptionResponse = new StoriesDescriptionResponse();


            var response = await _httpService.CallHttp(
                (url) => url
                    .WithHeader("Accept", "application/json")
                    .GetAsync()
                    .ReceiveJson<Root>(),
                    urlApiBase, string.Format(suffix, storiesID));

            if (response.Result != null)
            {
                StoriesDescriptionResponse = new StoriesDescriptionResponse
                {
                    by = response.Result.by,
                    descendants = response.Result.descendants,
                    id = response.Result.id,
                    kids = response.Result.kids,
                    score = response.Result.score,
                    time = response.Result.time,
                    title = response.Result.title,
                    type = response.Result.type,
                    url = response.Result.url
                };
            }

            return StoriesDescriptionResponse;
        }

    }
}
