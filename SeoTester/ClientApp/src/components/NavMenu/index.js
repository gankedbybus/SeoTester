import React from 'react';
import { Container, Navbar } from 'react-bootstrap';
import './navMenu.css';

const NavMenu = () => {
    return (
        <header>
            <Navbar className='navbar-expand-sm navbar-toggleable-sm ng-white border-bottom box-shadow mb-3'>
                <Container>
                    <Navbar.Brand to='/'>SeoTester</Navbar.Brand>
                </Container>
            </Navbar>
        </header>
    );
};

export default NavMenu;
