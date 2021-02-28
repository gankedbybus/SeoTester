using System.Collections.Generic;

namespace SeoTester.Application.Common.Interfaces
{
    public interface ISearchRankService
    {
        List<int> GetSearchRanks(string html, string toFind, string regex, int maxResults, ref int fetchedCount, ref int resultCount);
    }
}
