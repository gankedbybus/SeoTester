using System.IO;

namespace SeoTester.Application.Common.Interfaces
{
    public interface IFileManagerService
    {
        string ReadStream(Stream stream);
    }
}
