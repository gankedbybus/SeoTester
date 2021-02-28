import React from 'react';
import { render, waitFor, screen } from '@testing-library/react';
import SearchResults from './index';

describe('SearchResults', () => {
    it('has correct props', async () => {
        const props = { keyWords: 'test', url: 'www.123.com', ranks: '5' };
        render(<SearchResults {...props} />);
        await waitFor(() => {
            expect(screen.getByText(props.keyWords)).toBeTruthy();
            expect(screen.getByText(props.url)).toBeTruthy();
            expect(screen.getByText(props.ranks)).toBeTruthy();
        });
    });
});
