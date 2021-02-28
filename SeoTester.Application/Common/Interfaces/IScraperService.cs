using System.Threading.Tasks;

namespace SeoTester.Application.Common.Interfaces
{
    public interface IScraperService
    {
        Task<string> GetHtml(string url);
    }
}
