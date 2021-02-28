using FluentAssertions;
using Moq;
using NUnit.Framework;
using SeoTester.Application.Common.Constants;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services;
using SeoTester.Web.Services.Google;

namespace SeoTester.Tests.UnitTests
{
    [TestFixture]
    public class SearchServiceFactoryTests
    {
        private ISearchServiceFactory _searchServiceFactory;
        private Mock<IScraperService> _scraperService;
        private Mock<ISearchRankService> _searchRankService;

        [SetUp]
        public void SetUp()
        {
            _scraperService = new Mock<IScraperService>();
            _searchRankService = new Mock<ISearchRankService>();
            _searchServiceFactory = new SearchServiceFactory(_scraperService.Object, _searchRankService.Object);
        }


        [TestCase()]
        public void CreateServiceShouldCreateInstanceOfBingSearchEngineWhenTypeIsBing()
        {
            // arrange
            var type = SearchEngine.Bing;

            // act
            var result = _searchServiceFactory.CreateService(type);

            // assert
            result.Should().BeOfType<BingSearchService>();
        }

        [Test]
        public void CreateServiceShouldCreateInstanceOfGoogleSearchEngineWhenTypeIsGoogle()
        {
            // arrange
            var type = SearchEngine.Google;

            // act
            var result = _searchServiceFactory.CreateService(type);

            // assert
            result.Should().BeOfType<GoogleSearchService>();
        }
    }
}
