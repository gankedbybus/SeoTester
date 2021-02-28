using SeoTester.Application.Common.Constants;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services.Google;
using System;

namespace SeoTester.Web.Services
{
    public class SearchServiceFactory : ISearchServiceFactory
    {
        private readonly IScraperService _scraperService;
        private readonly ISearchRankService _searchRankService;
        public SearchServiceFactory(IScraperService scraperService, ISearchRankService searchRankService)
        {
            _scraperService = scraperService;
            _searchRankService = searchRankService;
        }

        public ISearchService CreateService(SearchEngine type)
        {
            switch (type)
            {
                case SearchEngine.Bing:
                    return new BingSearchService(_scraperService, _searchRankService);
                case SearchEngine.Google:
                    return new GoogleSearchService(_scraperService, _searchRankService);
                default:
                    throw new Exception();
            }
        }
    }
}
