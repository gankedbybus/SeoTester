using FluentAssertions;
using Moq;
using NUnit.Framework;
using SeoTester.Application.Common.Constants;
using SeoTester.Application.Common.Exceptions;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services.Search;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeoTester.Tests.UnitTests
{
    [TestFixture]
    public class SearchServiceTests
    {
        private ISearchService _searchService;
        private Mock<IScraperService> _scraperService;
        private Mock<ISearchRankService> _searchRankService;
        private int _maxResults;

        [SetUp]
        public void SetUp()
        {
            _scraperService = new Mock<IScraperService>();
            _searchRankService = new Mock<ISearchRankService>();
            _searchService = new SearchService(_scraperService.Object, _searchRankService.Object, SearchEngine.Google, string.Empty);
            _maxResults = 50;
        }

        [Test]
        public async Task GetRanksShouldReturnExpectedRank()
        {
            // arrange
            var keywords = "online title search";
            var url = "https://www.infotrack.com.au";
            var expectedRank = "1";
            var expectedRankList = new List<int> { 1 };
            var mockHtml = "test";
            _scraperService.Setup(x => x.GetHtml(It.IsAny<string>())).ReturnsAsync(mockHtml);
            _searchRankService.Setup(x => x.GetSearchRanks(It.IsAny<string>(),
                                                           It.IsAny<string>(),
                                                           It.IsAny<string>(),
                                                           It.IsAny<int>(),
                                                           ref It.Ref<int>.IsAny,
                                                           ref It.Ref<int>.IsAny))

                              .Returns((string html, string toFind, string searchResultRegex, int maxResults, int fetchedCount, int resultCount) =>
                              {
                                  fetchedCount = _maxResults;
                                  resultCount = _maxResults;
                                  return expectedRankList;
                              });

            // act
            var result = await _searchService.GetRanks(keywords, url, _maxResults);

            // assert
            result.Should().Be(expectedRank);
        }

        [Test]
        public async Task GetRanksShouldReturn0WhenNoHtmlIsFetched()
        {
            // arrange
            var keywords = "online title search";
            var url = "https://www.infotrack.com.au";
            var expectedRankList = new List<int> { 1 };
            _scraperService.Setup(x => x.GetHtml(It.IsAny<string>())).ReturnsAsync(string.Empty);

            // act
            var result = await _searchService.GetRanks(keywords, url, _maxResults);

            // assert
            result.Should().Be("0");
        }

        [TestCase("", "https://www.google.com/")]
        [TestCase("google", "")]
        [TestCase("", "")]
        public void GetRanksShouldThrowMissingInputException(string keywords, string url)
        {
            // arrange

            // act
            Func<Task> act = async () => { await _searchService.GetRanks(keywords, url, _maxResults); };

            // assert
            act.Should().Throw<MissingInputException>();
        }

        [Test]
        public void GetRanksShouldNotThrowMissingInputExceptionWhenKeywordsAndUrlAreProvided()
        {
            // arrange
            var keywords = "online title search";
            var url = "https://www.infotrack.com.au";

            // act
            Func<Task> act = async () => { await _searchService.GetRanks(keywords, url, _maxResults); };

            // assert
            act.Should().NotThrow<MissingInputException>();
        }

        [TestCase("htt://www.google.com/")]
        [TestCase("www.google.com")]
        public void GetRanksShouldThrowInvalidUrlFormatException(string url)
        {
            // arrange
            string keywords = "google";

            // act
            Func<Task> act = async () => { await _searchService.GetRanks(keywords, url, _maxResults); };

            // assert
            act.Should().Throw<InvalidUrlFormatException>();
        }

        [TestCase("http://www.google.com/")]
        [TestCase("https://www.google.com")]
        public void GetRanksShouldNotThrowInvalidUrlFormatExceptionWhenUrlFormatIsCorrect(string url)
        {
            // arrange
            string keywords = "google";

            // act
            Func<Task> act = async () => { await _searchService.GetRanks(keywords, url, _maxResults); };

            // assert
            act.Should().NotThrow<InvalidUrlFormatException>();
        }
    }
}

