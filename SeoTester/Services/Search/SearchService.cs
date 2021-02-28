using SeoTester.Application.Common.Constants;
using SeoTester.Application.Common.Exceptions;
using SeoTester.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeoTester.Web.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly IScraperService _scraperService;
        private readonly ISearchRankService _searchRankService;
        private readonly SearchEngine _searchEngine;
        private readonly string _searchResultRegex = string.Empty;
        public SearchService(IScraperService scraperService, ISearchRankService searchRankService, SearchEngine searchEngine, string searchResultRegex)
        {
            _scraperService = scraperService;
            _searchRankService = searchRankService;
            _searchEngine = searchEngine;
            _searchResultRegex = searchResultRegex;
        }

        public virtual async Task<string> GetRanks(string keyWords, string url, int maxResults)
        {
            ValidateSearchParams(keyWords, url);
            var uri = new Uri(url);
            var ranks = await GetSearchRanks(keyWords, uri, maxResults);
            if (ranks.Any())
            {
                return string.Join(", ", ranks);
            }

            return "0";
        }

        private void ValidateSearchParams(string keyWords, string url)
        {
            if (string.IsNullOrWhiteSpace(keyWords))
            {
                throw new MissingInputException("keywWords");
            }
            else if (string.IsNullOrWhiteSpace(url))
            {
                throw new MissingInputException("url");
            }
            else if (!Regex.Match(url, SeoConstants.WebsiteRegex).Success)
            {
                throw new InvalidUrlFormatException(url);
            }
        }

        private async Task<List<int>> GetSearchRanks(string searchTerm, Uri uri, int maxResults)
        {
            var pageCount = 1;
            var resultCount = 0;
            var maxResultsReached = false;
            var rankList = new List<int>();
            // seperated here to make it easier to read string interpolation in VS :)
            var baseUrl = "https://infotrack-tests.infotrack.com.au/" + $"{_searchEngine}/Page";
            while (!maxResultsReached)
            {
                // searchTerm would normally be appended here if using a real search engine and not a static webpage
                var searchUrl = new StringBuilder($"{baseUrl}{ (pageCount < 10 ? $"0{ pageCount }" : pageCount.ToString())}.html");
                var html = await _scraperService.GetHtml(searchUrl.ToString());
                if (string.IsNullOrWhiteSpace(html))
                {
                    return rankList;
                }

                var searchResultRegex = new StringBuilder(_searchResultRegex);
                searchResultRegex.AppendFormat("({0})", SeoConstants.WebsiteRegex);
                int fetchedCount = 0;
                rankList.AddRange(_searchRankService.GetSearchRanks(html, uri.Host, searchResultRegex.ToString(), maxResults, ref fetchedCount, ref resultCount));
                if (fetchedCount == 0 || resultCount > maxResults)
                {
                    maxResultsReached = true;
                }
                else
                {
                    pageCount++;
                }
            }

            return rankList;
        }
    }
}
