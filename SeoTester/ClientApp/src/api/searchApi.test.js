import fetchMock from 'jest-fetch-mock';
import api from './searchApi';
describe('searchApi', () => {
    describe('getSearchRanks', () => {
        fetchMock.enableMocks();
        beforeEach(() => {
            fetch.resetMocks();
        });

        it('should return data', async () => {
            const expectedData = { name: 'test' };
            const dataString = JSON.stringify(expectedData);
            fetch.mockResponseOnce(dataString);
            const searchEngine = 'firefox';
            const keyWords = 'user';
            const url = 'www.test.com';
            const result = await api.getSearchRanks(searchEngine, keyWords, url);
            const jsonResult = await result.json();
            expect(jsonResult).toStrictEqual(expectedData);
            expect(fetch).toHaveBeenCalledTimes(1);
        });
    });
});
