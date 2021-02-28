using SeoTester.Application.Common.Constants;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services.Search;

namespace SeoTester.Web.Services.Google
{
    public class BingSearchService : SearchService, IBingSearchService
    {
        public BingSearchService(IScraperService scraperService, ISearchRankService searchRankService) :
            base(scraperService, searchRankService, SearchEngine.Bing, SeoConstants.BingRegex)
        {
        }
    }
}
