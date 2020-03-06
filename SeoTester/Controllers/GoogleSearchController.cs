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
    public class GoogleSearchController : ControllerBase
    {
        private readonly IGoogleSearchService _googleSearchService;
        private readonly ILogger<GoogleSearchController> _logger;
        private readonly SeoSettings _config;

        public GoogleSearchController(IGoogleSearchService googleSearchService,
                                      ILogger<GoogleSearchController> logger,
                                      IOptions<SeoSettings> config)
        {
            _googleSearchService = googleSearchService;
            _logger = logger;
            _config = config.Value;
        }

        [HttpGet("ranks")]
        public async Task<string> GetRanks(string keyWords, string url)
        {
            try
            {
                return await _googleSearchService.GetRanks(keyWords, url, _config.MaxResults);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex.StackTrace);
                throw ex;
            }
        }
    }
}
