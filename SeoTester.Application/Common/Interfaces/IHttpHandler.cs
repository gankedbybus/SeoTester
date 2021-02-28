using System.IO;
using System.Threading.Tasks;

namespace SeoTester.Application.Common.Interfaces
{
    public interface IHttpHandler
    {
        Task<Stream> GetStreamAsync(string requestUri);
    }
}
