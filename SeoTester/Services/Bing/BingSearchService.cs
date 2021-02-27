using SeoTester.Application.Common.Constants;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services.Search;
using System.Net.Http;

namespace SeoTester.Web.Services.Google
{
    public class BingSearchService : SearchService, IBingSearchService
    {
        public BingSearchService(HttpClient client) : base(client, SearchEngine.Bing, SeoConstants.BingRegex)
        {
        }
    }
}
