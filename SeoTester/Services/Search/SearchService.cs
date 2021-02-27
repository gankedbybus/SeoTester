using SeoTester.Application.Common.Constants;
using SeoTester.Application.Common.Exceptions;
using SeoTester.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SeoTester.Web.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly HttpClient _client;
        private readonly string _searchResultRegex = string.Empty;
        private readonly SearchEngine _searchEngine;
        public SearchService(HttpClient client, SearchEngine searchEngine, string searchResultRegex)
        {
            _client = client;
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

            if (string.IsNullOrWhiteSpace(url))
            {
                throw new MissingInputException("url");
            }

            if (!Regex.Match(url, SeoConstants.WebsiteRegex).Success)
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
            while (!maxResultsReached)
            {
                // searchTerm would normally be appended here if using a real search engine and not a static webpage
                var searchUrl = new StringBuilder("https://infotrack-tests.infotrack.com.au/" + $"{_searchEngine}/Page{ (pageCount < 10 ? $"0{pageCount}" : pageCount.ToString())}.html");
                var response = await _client.GetStreamAsync(searchUrl.ToString());
                using var reader = new StreamReader(response, Encoding.ASCII);
                var html = reader.ReadToEnd();
                var fetchedCount = FindSearchRanks(rankList, html, uri, maxResults, ref resultCount);
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

        private int FindSearchRanks(List<int> rankList, string html, Uri uri, int maxResults, ref int resultCount)
        {
            var searchResultRegex = new StringBuilder(_searchResultRegex);
            searchResultRegex.AppendFormat("({0})", SeoConstants.WebsiteRegex);
            var searchResults = Regex.Matches(html, searchResultRegex.ToString());
            for (int i = 0; i < searchResults.Count; i++)
            {
                var match = searchResults[i].Groups[2].Value;
                if (match.Contains(uri.Host))
                {
                    // prevent fetching results higher than max e.g 51 if maxResults is 50
                    var rank = resultCount + i + 1;
                    if (rank <= maxResults)
                    {
                        rankList.Add(rank);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            resultCount = resultCount += searchResults.Count;
            return searchResults.Count;
        }
    }
}
