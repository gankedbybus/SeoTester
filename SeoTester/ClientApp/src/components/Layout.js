import React from 'react';
import NavMenu from './NavMenu/index';

const Layout = ({ children }) => {
    return (
        <div>
            <NavMenu />
            {children}
        </div>
    );
};

export default Layout;
