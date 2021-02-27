using SeoTester.Application.Common.Constants;

namespace SeoTester.Application.Common.Interfaces
{
    public interface ISearchServiceFactory
    {
        public ISearchService CreateService(SearchEngine type);
    }
}
