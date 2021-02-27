import React, { useState } from 'react';
import { getSearchRanks } from '../../api/searchApi';
import SearchResults from '../../components/SearchResults';
import './seoTester.css';
import { Form, Button } from 'react-bootstrap';
import Row from 'react-bootstrap/Row';
import Container from 'react-bootstrap/Container';
import Col from 'react-bootstrap/Col';

const SeoTesterPage = () => {
    const [errors, setErrors] = useState({
        keyWords: '',
        url: ''
    });

    const [keyWords, setKeyWords] = useState('online title search');
    const [lastKeyWords, setLastKeyWords] = useState('');
    const [lastUrl, setLastUrl] = useState('');
    const [loading, setLoading] = useState(false);
    const [ranks, setRanks] = useState('');
    const [url, setUrl] = useState('https://www.infotrack.com.au');
    const searchEngines = ['Bing', 'Google'];
    const [searchEngine, setSearchEngine] = useState('Google');

    const handleInputChange = (event) => {
        var target = event.target;
        var name = target.name;
        var value = target.value;
        switch (name) {
            case 'keyWords':
                setErrors({
                    ...errors,
                    keyWords: (errors.keyWords = !value || value.trim() === '' ? 'Key words is a required field!' : '')
                });
                setKeyWords(value);
                break;
            case 'searchEngine':
                setSearchEngine(value);
                break;
            case 'url':
                setErrors({
                    ...errors,
                    url:
                        !value || value.trim() === ''
                            ? 'Url is a required field!'
                            : !value.match('h+[a-zA-Z0-9--?=/]*')
                            ? 'Invalid Url format e.g https://www.infotrack.com.au'
                            : ''
                });
                setUrl(value);
                break;
            default:
                break;
        }
    };

    const getRanks = async () => {
        if (!formIsValidated()) {
            return;
        }

        setLoading(true);
        try {
            const response = await getSearchRanks(searchEngine, keyWords, url);
            var ranks = await response.json();
            setLastKeyWords(keyWords);
            setLastUrl(url);
            setRanks(ranks);
            setLoading(false);
        } catch (error) {
            // TODO: error handling
            console.log('error: ', error);
        }
    };

    const formIsValidated = () => {
        return !errors.keyWords && !errors.url;
    };

    let contents =
        !ranks && !loading ? (
            <></>
        ) : loading ? (
            <p>
                <em>Loading...</em>
            </p>
        ) : (
            <SearchResults keyWords={lastKeyWords} url={lastUrl} ranks={ranks}></SearchResults>
        );

    return (
        <Container>
            <Row>
                <Col>
                    <Form>
                        <Form.Group controlId='searchEngine'>
                            <Form.Label>Search Engine</Form.Label>
                            <Form.Control as='select' defaultValue={searchEngine} custom name='searchEngine' onChange={handleInputChange}>
                                {searchEngines.map((item, i) => (
                                    <option key={i}>{item}</option>
                                ))}
                            </Form.Control>
                        </Form.Group>
                        <Form.Group controlId='keyWords'>
                            <Form.Label>Key Words</Form.Label>
                            <Form.Control name={'keyWords'} value={keyWords} onChange={handleInputChange} />
                            {errors.keyWords.length > 0 && <span className='error'>{errors.keyWords}</span>}
                        </Form.Group>
                        <Form.Group controlId='url'>
                            <Form.Label>Url</Form.Label>
                            <Form.Control name={'url'} value={url} onChange={handleInputChange} />
                            {errors.url.length > 0 && <span className='error'>{errors.url}</span>}
                        </Form.Group>
                        <Button type='button' className={formIsValidated() ? '' : 'disabled'} onClick={getRanks}>
                            Get Search Ranks
                        </Button>
                    </Form>
                </Col>
            </Row>
            {contents}
        </Container>
    );
};

export default SeoTesterPage;
