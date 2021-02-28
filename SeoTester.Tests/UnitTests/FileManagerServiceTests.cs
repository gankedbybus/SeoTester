using FluentAssertions;
using NUnit.Framework;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services.FileManager;
using System.IO;
using System.Text;

namespace SeoTester.Tests.UnitTests
{
    [TestFixture]
    public class FileManagerServiceTests
    {
        private IFileManagerService _fileManagerService;

        [SetUp]
        public void SetUp()
        {
            _fileManagerService = new FileManagerService();
        }

        [Test]
        public void ReadStreamShouldReturnExpectedString()
        {
            // arrange
            var expectedString = "test";
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(expectedString));

            // act
            var result = _fileManagerService.ReadStream(stream);

            // assert
            result.Should().Be(expectedString);

        }
    }
}
