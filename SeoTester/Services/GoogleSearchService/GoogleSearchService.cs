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
using System.Web;

namespace SeoTester.Web.Services.GoogleSearchService
{
    public class GoogleSearchService : IGoogleSearchService
    {
        private readonly HttpClient _client;
        public GoogleSearchService(HttpClient client)
        {
            _client = client;
        }

        public async Task<string> GetRanks(string keyWords, string url, int maxResults)
        {
            ValidateSearchParams(keyWords, url);
            var uri = new Uri(url);
            var ranks = await GetGoogleSearchRanks(keyWords, uri, maxResults);
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

            if (!Regex.Match(url, new SeoConstants().WebsiteRegex).Success)
            {
                throw new InvalidUrlFormatException(url);
            }
        }

        private async Task<List<int>> GetGoogleSearchRanks(string searchTerm, Uri uri, int maxResults)
        {
            var searchUrl = new StringBuilder("https://www.google.com/search?");
            searchUrl.AppendFormat("num={0}&q={1}&btnG=Search", maxResults, HttpUtility.UrlEncode(searchTerm));
            var response = await _client.GetStreamAsync(searchUrl.ToString());
            using var reader = new StreamReader(response, Encoding.ASCII);
            var html = reader.ReadToEnd();
            return FindSearchRanks(html, uri);
        }

        private List<int> FindSearchRanks(string html, Uri uri)
        {
            var rankList = new List<int>();
            var searchResultRegex = new StringBuilder("(<div class=\"kCrYT\"><a href=\"/url\\?q=)");
            searchResultRegex.AppendFormat("({0})", new SeoConstants().WebsiteRegex);
            var matches = Regex.Matches(html, searchResultRegex.ToString());
            for (int i = 0; i < matches.Count; i++)
            {
                var match = matches[i].Groups[2].Value;
                if (match.Contains(uri.Host))
                {
                    rankList.Add(i + 1);
                }
            }

            return rankList;
        }
    }
}
