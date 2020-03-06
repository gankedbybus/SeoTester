import React, { Component } from 'react';
import { getGoogleSearchRanks } from '../../api/googleSearchApi';
import { SearchResults } from './searchResults';
import './seoTester.css';

export class SeoTesterPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      errors: {
        keyWords: '',
        url: ''
      },
      keyWords: '',
      lastKeyWords: '',
      lastUrl: '',
      loading: false,
      ranks: '',
      url: ''
    };
  }

  handleInputChange = event => {
    var target = event.target;
    var name = target.name;
    var value = target.value;
    var errors = this.state.errors;
    switch (name) {
      case 'keyWords':
        errors.keyWords =
          !value || value.trim() === '' ? 'Key words is a required field!' : '';
        break;
      case 'url':
        errors.url =
          !value || value.trim() === ''
            ? 'Url is a required field!'
            : !value.match('h+[a-zA-Z0-9--?=/]*')
            ? 'Invalid Url format e.g https://www.smokeball.com.au'
            : '';
        break;
      default:
        break;
    }

    this.setState({ errors, [name]: value });
  };

  getSearchRanks = async () => {
    const { keyWords, url } = this.state;
    if (!this.formIsValidated()) {
      return;
    }

    this.setState({ loading: true });
    try {
      const response = await getGoogleSearchRanks(keyWords, url);
      var ranks = await response.json();
      this.setState({
        lastKeyWords: keyWords,
        lastUrl: url,
        ranks: ranks,
        loading: false
      });
    } catch (error) {
      console.log('error: ', error);
    }
  };

  formIsValidated = () => {
    const { errors } = this.state;
    return !errors.keyWords && !errors.url;
  };

  render() {
    const {
      errors,
      keyWords,
      lastKeyWords,
      lastUrl,
      loading,
      ranks,
      url
    } = this.state;

    let contents =
      !ranks && !loading ? (
        <></>
      ) : loading ? (
        <p>
          <em>Loading...</em>
        </p>
      ) : (
        <SearchResults
          keyWords={lastKeyWords}
          url={lastUrl}
          ranks={ranks}
        ></SearchResults>
      );

    return (
      <>
        <div>
          <label>
            {'Key Words: '}
            <input
              type='text'
              name={'keyWords'}
              value={keyWords}
              onChange={this.handleInputChange}
            />
          </label>
          {errors.keyWords.length > 0 && (
            <span className='error'>{errors.keyWords}</span>
          )}
        </div>
        <div>
          <label>
            {'Url: '}
            <input
              type='text'
              name={'url'}
              value={url}
              onChange={this.handleInputChange}
            />
          </label>
          {errors.url.length > 0 && <span className='error'>{errors.url}</span>}
        </div>
        <button
          className={this.formIsValidated() ? '' : 'disabled'}
          onClick={this.getSearchRanks}
        >
          Get Search Rank/s
        </button>
        {contents}
      </>
    );
  }
}
