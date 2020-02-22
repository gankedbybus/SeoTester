using NUnit.Framework;
using SeoTester.Application.Common.Exceptions;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services.GoogleSearchService;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeoTester.Tests.UnitTests
{
    [TestFixture]
    public class GoogleSearchServiceTests
    {
        private IGoogleSearchService _googleSearchService;
        private HttpClient _client;
        private int _maxResults;

        [SetUp]
        public void SetUp()
        {
            _client = new HttpClient();
            _googleSearchService = new GoogleSearchService(_client);
            _maxResults = 100;
        }

        [Test]
        public async Task GetRanksShouldNotReturnNUll()
        {
            var keywords = "google";
            var url = "https://www.google.com/";
            var result = await _googleSearchService.GetRanks(keywords, url, _maxResults);
            Assert.IsNotNull(result);
        }

        [TestCase("", "https://www.google.com/")]
        [TestCase("google", "")]
        [TestCase("", "")]
        public void GetRanksShouldThrowMissingInputException(string keywords, string url)
        {
            Assert.ThrowsAsync<MissingInputException>(async () => await _googleSearchService.GetRanks(keywords, url, _maxResults));
        }

        [TestCase("htt://www.google.com/")]
        [TestCase("www.google.com")]
        public void GetRanksShouldThrowInvalidUrlFormatException(string url)
        {
            string keywords = "google";

            Assert.ThrowsAsync<InvalidUrlFormatException>(async () => await _googleSearchService.GetRanks(keywords, url, _maxResults));
        }

        [TestCase("http://www.google.com/")]
        [TestCase("https://www.google.com")]
        public void GetRanksShouldNotThrowInvalidUrlFormatException(string url)
        {
            string keywords = "google";

            Assert.DoesNotThrowAsync(async () => await _googleSearchService.GetRanks(keywords, url, _maxResults));
        }
    }
}

