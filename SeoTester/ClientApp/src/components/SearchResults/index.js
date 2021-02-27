import React from 'react';

const SearchResults = ({ keyWords, url, ranks }) => {
    return ranks === '0' ? (
        <div>
            Url: <span style={{ fontWeight: 'bold' }}>{url}</span> did not appear in the first 100 search results when searched with Key
            Word/s <span style={{ fontWeight: 'bold' }}>{keyWords}</span>
        </div>
    ) : (
        <div>
            Url: <span style={{ fontWeight: 'bold' }}>{url}</span> appeared at rank/s <span style={{ fontWeight: 'bold' }}>{ranks}</span>{' '}
            when searched with key word/s <span style={{ fontWeight: 'bold' }}>{keyWords}</span>
        </div>
    );
};

export default SearchResults;
