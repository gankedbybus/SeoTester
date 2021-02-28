using FluentAssertions;
using NUnit.Framework;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services.SearchRank;

namespace SeoTester.Tests.UnitTests
{
    [TestFixture]
    public class SearchRankServiceTests
    {
        private ISearchRankService _searchRankService;

        [SetUp]
        public void SetUp()
        {
            _searchRankService = new SearchRankService();
        }

        [TestCase]
        public void GetSearchRanksShouldReturnExpectedMatches()
        {
            // arrange
            var html = "1 3 2 a b c";
            var toFind = "3";
            var searchRegex = "[0-9]";
            var maxResults = 5;
            var fetchedCount = 0;
            var resultCount = 0;
            var expectedMatches = 1;
            var expectedMatchPosition = 2;

            // act
            var result = _searchRankService.GetSearchRanks(html, toFind, searchRegex, maxResults, ref fetchedCount, ref resultCount);

            // assert
            result.Count.Should().Be(expectedMatches);
            result.Should().Contain(expectedMatchPosition);

        }
    }
}
