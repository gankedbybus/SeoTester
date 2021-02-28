import React from 'react';
import { render, waitFor, screen } from '@testing-library/react';
import NavMenu from './index';

describe('NavMenu', () => {
    it('displays the title', async () => {
        render(<NavMenu />);
        await waitFor(() => {
            expect(screen.getByText('SeoTester')).toBeTruthy();
        });
    });
});
