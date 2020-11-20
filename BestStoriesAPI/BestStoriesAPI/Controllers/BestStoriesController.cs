using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Acl.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace BestStoriesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BestStoriesController : ControllerBase
    {
        private readonly IBestStoriesService _bestStoriesService;

        public BestStoriesController(IBestStoriesService bestStoriesService) => _bestStoriesService = bestStoriesService;

        [HttpGet]
        [Route("GetStories")]
        [SwaggerResponse(statusCode: 200, type: typeof(List<StoriesDescriptionResponse>))]
        [SwaggerResponse(statusCode: 401, description: "Unauthorized")]
        [SwaggerResponse(statusCode: 500, description: "Internal Server Error")]
        public Task<IActionResult> DoGetBest() => DoGet();

        private async Task<IActionResult> DoGet()
        {
            var response = await _bestStoriesService.GetBestStories();
                        
            return Ok(response);
        }
    }
}
