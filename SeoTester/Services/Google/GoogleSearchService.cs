using SeoTester.Application.Common.Constants;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services.Search;
using System.Net.Http;

namespace SeoTester.Web.Services.Google
{
    public class GoogleSearchService : SearchService, IGoogleSearchService
    {
        public GoogleSearchService(HttpClient client) : base(client, SearchEngine.Google, SeoConstants.GoogleRegex)
        {
        }
    }
}
