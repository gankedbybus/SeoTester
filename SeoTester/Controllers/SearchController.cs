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
        private readonly SeoSettings _config;
        private readonly ISearchServiceFactory _searchServiceFactory;

        public SearchController(ILogger<SearchController> logger,
                                IOptions<SeoSettings> config,
                                ISearchServiceFactory searchServiceFactory)
        {
            _logger = logger;
            _config = config.Value;
            _searchServiceFactory = searchServiceFactory;
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
