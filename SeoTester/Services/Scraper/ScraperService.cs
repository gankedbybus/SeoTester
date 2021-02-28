using SeoTester.Application.Common.Interfaces;
using System.Threading.Tasks;

namespace SeoTester.Web.Services.Scraper
{
    public class ScraperService : IScraperService
    {
        private readonly IHttpHandler _client;
        private readonly IFileManagerService _fileManagerService;

        public ScraperService(IHttpHandler client, IFileManagerService fileManagerService)
        {
            _client = client;
            _fileManagerService = fileManagerService;
        }

        public async Task<string> GetHtml(string url)
        {
            var response = await _client.GetStreamAsync(url);
            return _fileManagerService.ReadStream(response);
        }
    }
}
