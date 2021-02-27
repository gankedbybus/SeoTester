using SeoTester.Application.Common.Constants;
using SeoTester.Application.Common.Interfaces;
using SeoTester.Web.Services.Google;
using System;
using System.Net.Http;

namespace SeoTester.Web.Services
{
    public class SearchServiceFactory : ISearchServiceFactory
    {
        private readonly HttpClient _client;
        public SearchServiceFactory(HttpClient client)
        {
            _client = client;
        }

        public ISearchService CreateService(SearchEngine type)
        {
            switch (type)
            {
                case SearchEngine.Google:
                    return new GoogleSearchService(_client);
                case SearchEngine.Bing:
                    return new BingSearchService(_client);
                default:
                    throw new Exception();
            }
        }
    }
}
