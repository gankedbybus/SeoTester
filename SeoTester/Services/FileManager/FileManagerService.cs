using SeoTester.Application.Common.Interfaces;
using System.IO;
using System.Text;

namespace SeoTester.Web.Services.FileManager
{
    public class FileManagerService : IFileManagerService
    {
        public string ReadStream(Stream stream)
        {
            using var reader = new StreamReader(stream, Encoding.ASCII);
            return reader.ReadToEnd();
        }
    }
}
