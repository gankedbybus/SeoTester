using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Config;
using System;
using System.Threading.Tasks;

namespace SeoTester.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchController : ControllerBase
    {
        private readonly ILogger<SearchController> _logger;
        private readonly ISearchServiceFactory _searchServiceFactory;
        private readonly SeoSettings _config;

        public SearchController(ILogger<SearchController> logger,
                                ISearchServiceFactory searchServiceFactory,
                                IOptions<SeoSettings> config)
        {
            _logger = logger;
            _searchServiceFactory = searchServiceFactory;
            _config = config.Value;
        }

        [HttpGet("ranks")]
        public async Task<string> GetRanks([FromHeader] Application.Common.Constants.SearchEngine searchEngine, string keyWords, string url)
        {
            try
            {
                return await _searchServiceFactory.CreateService(searchEngine).GetRanks(keyWords, url, _config.MaxResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }
    }
}
