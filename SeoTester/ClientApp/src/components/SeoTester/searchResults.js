import React, { Component } from 'react';

export class SearchResults extends Component {
  render() {
    const { keyWords, url, ranks } = this.props;
    return ranks === '0' ? (
      <div>
        Url: <span style={{ fontWeight: 'bold' }}>{url}</span> did not appear in
        the first 100 search results when searched with Key Word/s{' '}
        <span style={{ fontWeight: 'bold' }}>{keyWords}</span>
      </div>
    ) : (
      <div>
        Url: <span style={{ fontWeight: 'bold' }}>{url}</span> came up at rank/s{' '}
        <span style={{ fontWeight: 'bold' }}>{ranks}</span> when searched with
        Key Word/s <span style={{ fontWeight: 'bold' }}>{keyWords}</span>
      </div>
    );
  }
}

export default SearchResults;
