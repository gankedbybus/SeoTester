import 'bootstrap/dist/css/bootstrap.css';
import React from 'react';
import ReactDOM from 'react-dom';
import { Router, Route, Switch } from 'react-router-dom';
import App from './App';
import registerServiceWorker from './registerServiceWorker';
import { createBrowserHistory } from 'history';

const history = createBrowserHistory();
const rootElement = document.getElementById('root');

ReactDOM.render(
    <Router history={history}>
        <Switch>
            <Route path='/' component={App}></Route>
        </Switch>
    </Router>,
    rootElement
);

registerServiceWorker();
