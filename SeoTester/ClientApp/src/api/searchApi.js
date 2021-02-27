export const get = async (endpoint, extraHeaders) => {
    return await fetch(endpoint, {
        headers: {
            ...extraHeaders,
            Accept: 'application/json',
            'Content-Type': 'application/json'
        }
    });
};

export const getSearchRanks = async (searchEngine, keyWords, url) => {
    const searchEngineHeader = { searchEngine: searchEngine };
    return await get(`search/ranks?keyWords=${keyWords}&url=${url}`, searchEngineHeader);
};
