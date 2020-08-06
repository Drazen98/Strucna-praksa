import React, { Component } from 'react';
import { NavLink } from 'react-router-dom'

class NavbarComponent extends Component {
    render() {
        return (
            <div className="topnav">
            <NavLink activeClassName="active" to="/Persons">Persons</NavLink>
            <NavLink activeClassName="active" to="/AddPerson">Add person</NavLink>
            </div>
        );
    }
}

export default NavbarComponent;