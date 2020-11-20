using AutoFixture;
using Domain.Acl;
using Domain.Entities;
using Domain.Interfaces;
using FluentAssertions;
using Flurl.Http.Testing;
using Microsoft.Extensions.Configuration;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Tests.Acls
{
    public class StoriesAclTest
    {        
        private IStoriesAcl _acl;
        private HttpService _httpService;
        private Fixture _fixture;
        protected HttpTest _httpTest;
        protected Mock<IConfiguration> _configuration;

        [SetUp]
        public void Setup()
        {
            _httpTest = new HttpTest();
            _fixture = new Fixture();

            _configuration = new Mock<IConfiguration>();
            
            var _mockConfSection = new Mock<IConfigurationSection>();
            _mockConfSection.SetupGet(m => m[It.Is<string>(s => s == "default")]).Returns("mock value");

            _httpService = new HttpService();

            _configuration.Setup(a => a.GetSection(It.Is<string>(s => s == "base.url.api"))).Returns(_mockConfSection.Object);
            _configuration.Setup(s => s.GetSection(It.Is<string>(t => t == "sufix.url.listid.api"))).Returns(_mockConfSection.Object);
            _configuration.Setup(d => d.GetSection(It.Is<string>(f => f == "sufix.url.desc.api"))).Returns(_mockConfSection.Object);


            _acl = new StoriesAcl(_configuration.Object, _httpService);
        }

        [TearDown]
        public void TearDown() => _httpTest.Dispose();



        [Test]
        public async Task ShouldGetStoriesIDSuccessfully()
        {
            
            var result = await _acl.GetStoriesIDs();

            result.Should().NotBeNull();
            
        }

        [Test]
        public async Task ShouldGetStoriesDescriptionSuccessfully()
        {
            var idStories = _fixture.Create<long>();
            
            var result = await _acl.GetStoriesDescription(idStories);

            result.Should().NotBeNull();

        }
    }
}
