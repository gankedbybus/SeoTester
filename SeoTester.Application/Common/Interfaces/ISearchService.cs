using System.Threading.Tasks;

namespace SeoTester.Application.Common.Interfaces
{
    public interface ISearchService
    {
        Task<string> GetRanks(string keyWords, string url, int maxResults);
    }
}
