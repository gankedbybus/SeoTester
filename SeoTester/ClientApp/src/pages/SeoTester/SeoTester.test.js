import React from 'react';
import { act, render, waitFor, screen, fireEvent } from '@testing-library/react';
import SeoTesterPage from './index';
import api from '../../api/searchApi';

describe('SeoTesterPage', () => {
    beforeEach(() => {
        jest.clearAllMocks();
    });

    it('should render the submit button', async () => {
        render(<SeoTesterPage />);
        await waitFor(() => {
            expect(screen.getByText('Get Search Ranks')).toBeTruthy();
        });
    });

    it('should change keyWords value when input is changed', async () => {
        render(<SeoTesterPage />);
        const keyWordsInput = screen.getByLabelText('Key Words');
        const expectedValue = 'test';
        act(() => {
            fireEvent.change(keyWordsInput, { target: { value: expectedValue } });
        });

        await waitFor(() => {
            expect(keyWordsInput.value).toBe(expectedValue);
        });
    });

    it('should show loading when submitButton is clicked when fields are empty', async () => {
        const apiSpy = jest.spyOn(api, 'getSearchRanks').mockImplementation(() => {
            return Promise.reject(new Error('fake error'));
        });

        render(<SeoTesterPage />);
        const submitButton = screen.getByText('Get Search Ranks');
        act(() => {
            fireEvent.click(submitButton);
        });

        await waitFor(() => {
            expect(screen.getByText('Loading...')).toBeTruthy();
        });
    });

    it('should show call apiSpy when submitButton is clicked', async () => {
        const apiSpy = jest.spyOn(api, 'getSearchRanks').mockImplementation(() => {
            return Promise.resolve({
                json: () => Promise.resolve('12')
            });
        });

        render(<SeoTesterPage />);
        const submitButton = screen.getByText('Get Search Ranks');
        act(() => {
            fireEvent.click(submitButton);
        });

        await waitFor(() => {
            expect(apiSpy).toHaveBeenCalled();
        });
    });
});
