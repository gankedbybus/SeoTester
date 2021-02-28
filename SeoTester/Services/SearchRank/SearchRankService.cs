using SeoTester.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace SeoTester.Web.Services.SearchRank
{
    public class SearchRankService : ISearchRankService
    {
        public List<int> GetSearchRanks(string html, string toFind, string regex, int maxResults, ref int fetchedCount, ref int resultCount)
        {
            var rankList = new List<int>();
            var searchResults = Regex.Matches(html, regex.ToString());
            fetchedCount = searchResults.Count;
            for (int i = 0; i < fetchedCount; i++)
            {
                var match = searchResults[i].Value;
                if (match.Contains(toFind))
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

            resultCount = resultCount += fetchedCount;
            return rankList;
        }
    }
}
