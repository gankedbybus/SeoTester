using SeoTester.Application.Common.Constants;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services.Search;

namespace SeoTester.Web.Services.Google
{
    public class GoogleSearchService : SearchService, IGoogleSearchService
    {
        public GoogleSearchService(IScraperService scraperService, ISearchRankService searchRankService) :
            base(scraperService, searchRankService, SearchEngine.Google, SeoConstants.GoogleRegex)
        {
        }
    }
}
