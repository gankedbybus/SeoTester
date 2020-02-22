import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { SeoTesterPage } from './components/SeoTester/seoTesterPage';

import './custom.css';

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Route exact path='/' component={SeoTesterPage} />
      </Layout>
    );
  }
}
