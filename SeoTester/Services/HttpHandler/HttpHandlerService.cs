using SeoTester.Application.Common.Interfaces;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SeoTester.Web.Services.HttpHandler
{
    public class HttpHandlerService : IHttpHandler
    {
        private readonly HttpClient _client;
        public HttpHandlerService(HttpClient client)
        {
            _client = client;
        }

        public async Task<Stream> GetStreamAsync(string requestUri)
        {
            return await _client.GetStreamAsync(requestUri);
        }
    }
}
