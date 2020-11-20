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
    public class StoriesIDsChainTest
    {
        private IChain<BestStoriesChainParameters, List<StoriesDescriptionResponse>> _chain;
        private Mock<IStoriesAcl> _storiesAcl;
        private Fixture _fixture;
        private IDStoriesResponse _idStoriesResponse;

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _storiesAcl = new Mock<IStoriesAcl>();

            
            _idStoriesResponse = _fixture.Create<IDStoriesResponse>();
            
            List<StoriesDescriptionResponse> storiesDescriptionResponse = _fixture.Create<List<StoriesDescriptionResponse>>();

            _chain = new StoriesIDsChain(_storiesAcl.Object)
            {
                Next = new DafaultChain<BestStoriesChainParameters, List<StoriesDescriptionResponse>>(storiesDescriptionResponse)
            };
        }

        [Test]
        public async Task ShouldExecuteSuccessfully()
        {
            _storiesAcl.Setup(s => s.GetStoriesIDs()).ReturnsAsync(_idStoriesResponse);
            var param = GetParam();

            var result = await _chain.Execute(GetParam());

            result.Should().NotBeNull();
            _storiesAcl.Verify(v => v.GetStoriesIDs(), Times.Once);            
        }

        [Test]
        public async Task ShouldFailIfIdsIsNull()
        {
            _storiesAcl.Setup(s => s.GetStoriesIDs()).ReturnsAsync(() => null);
            var result = await _chain.Execute(new BestStoriesChainParameters());
            result.Should().BeNull();
        }

        private BestStoriesChainParameters GetParam()
            =>
                new BestStoriesChainParameters
                {
                    IDStoriesResponse = _idStoriesResponse
                };
               
    }
}
