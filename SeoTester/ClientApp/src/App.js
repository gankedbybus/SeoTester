import React from 'react';
import { Route } from 'react-router';
import Layout from './components/Layout';
import SeoTesterPage from './pages/SeoTester/index';

import './custom.css';

const App = () => {
    return (
        <Layout>
            <Route exact path='/' component={SeoTesterPage} />
        </Layout>
    );
};

export default App;
