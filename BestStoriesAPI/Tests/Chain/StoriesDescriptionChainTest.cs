using AutoFixture;
using Domain.Acl.Entities;
using Domain.Chain;
using Domain.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.Chain
{
    public class StoriesDescriptionChainTest
    {
        private IChain<BestStoriesChainParameters, List<StoriesDescriptionResponse>> _chain;
        private Mock<IStoriesAcl> _storiesAcl;
        private Fixture _fixture;
        private long _idStories;
        private StoriesDescriptionResponse _storiesDescription;
        private List<StoriesDescriptionResponse> _storiesDescriptionList;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _storiesAcl = new Mock<IStoriesAcl>();


            _idStories = _fixture.Create<long>();
            _storiesDescription = _fixture.Create<StoriesDescriptionResponse>();

            
            _storiesDescriptionList = _fixture.Create<List<StoriesDescriptionResponse>>();

            _chain = new StoriesDescriptionChain(_storiesAcl.Object)
            {
                Next = new DafaultChain<BestStoriesChainParameters, List<StoriesDescriptionResponse>>(_storiesDescriptionList)
            };
        }

        [Test]
        public async Task ShouldExecuteSuccessfully()
        {
            _storiesAcl.Setup(s => s.GetStoriesDescription(_idStories)).ReturnsAsync(_storiesDescription);
            
            var result = await _chain.Execute(GetParam());

            result.Should().NotBeNull();
            _storiesAcl.Verify(v => v.GetStoriesDescription(_idStories), Times.Once);
        }

        [Test]
        public async Task ShouldFailIfIReturnIsNull()
        {
            _storiesAcl.Setup(s => s.GetStoriesDescription(_idStories)).ReturnsAsync(() => null);

            var entity = new BestStoriesChainParameters
            {
                IDStoriesResponse = null,
                StoriesDescriptionResponseList = new List<StoriesDescriptionResponse>()
            };

            var result = await _chain.Execute(entity);

            result.Should().BeNull();
        }

        private BestStoriesChainParameters GetParam()
            =>
                new BestStoriesChainParameters
                {
                    IDStoriesResponse = new IDStoriesResponse {IDStoriesList = new List<long> { _idStories } },
                    StoriesDescriptionResponseList = new List<StoriesDescriptionResponse>()
                };
               
    }
}
