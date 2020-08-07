import React, { Component } from 'react';
import {AddUser} from './../FunctionComponents/PersonComponent';
import axios from 'axios';

class AddPersonComponent extends Component {
    constructor(props){
        super(props);
        this.state = {
            firstName : "",
            lastName : "",
            height : "",
            weight : ""
        };
    }
    handleInputForm = async (e) =>{
        e.preventDefault();
        var res = await axios.post("/api/Example/Person",this.state).then(res => console.log(res));
        this.setState({
            firstName : "",
            lastName : "",
            height : "",
            weight : "" 
        });
        return res;
    }
    addState = (param,value) =>{
        this.setState({
            [param] : value
        });
        return;
    }
    getState = (param) =>{
        return this.state[param];
    }
    render() {
        return (
            <AddUser functionToAdd={this.handleInputForm} changeState={this.addState} getState={this.getState}/>
        );
    }
}

export default AddPersonComponent;