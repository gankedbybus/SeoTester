using FluentAssertions;
using Moq;
using NUnit.Framework;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services.Scraper;
using System.IO;
using System.Threading.Tasks;

namespace SeoTester.Tests.UnitTests
{
    [TestFixture]
    public class ScraperServiceTests
    {
        private IScraperService _scraperService;
        private Mock<IHttpHandler> _client;
        private Mock<IFileManagerService> _fileManagerService;

        [SetUp]
        public void SetUp()
        {
            _client = new Mock<IHttpHandler>();
            _fileManagerService = new Mock<IFileManagerService>();
            _scraperService = new ScraperService(_client.Object, _fileManagerService.Object);
        }

        [TestCase()]
        public async Task GetHtmlShouldReturnHtmlWhenClientReturnAResponse()
        {
            // arrange
            var mockStream = new Mock<Stream>();
            _client.Setup(x => x.GetStreamAsync(It.IsAny<string>())).ReturnsAsync(mockStream.Object);
            var expectedResponse = "123";
            _fileManagerService.Setup(x => x.ReadStream(It.IsAny<Stream>())).Returns(expectedResponse);
            var url = "www.google.com";

            // act
            var result = await _scraperService.GetHtml(url);

            // assert
            result.Should().Be(expectedResponse);
        }
    }
}
