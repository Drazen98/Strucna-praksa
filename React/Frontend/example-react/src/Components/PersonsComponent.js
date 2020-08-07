import React, { Component } from 'react';
import axios from 'axios';
import './../../node_modules/font-awesome/css/font-awesome.min.css';
import {Users} from './../FunctionComponents/PersonComponent';

class PersonsComponent extends Component {
    constructor(props){
        super(props);
        this.state = {
            person : []
        };
        this.handleDelete = this.handleDelete.bind(this);
    }
     getEveryPersonButton = async() =>{
        await axios.get("/api/Example/Person").then(response => {
            this.setState({
                person : response.data
            });
        });
    };
    handleDelete = async(id,index) =>{
        var res = await axios.delete("/api/Example/Person/" + id);
        var tmparaay = this.state.person;
        tmparaay.splice(index,1);
        this.setState({
            person : tmparaay
        });
        return res;
    }
    handleChangeFirstName = async(id,index,newName) =>{
        var res = await axios.put("/api/Example/Person/" + id,{
            "firstName": newName,
            "lastName": "",
            "height": 0,
            "weight": 0,
            "personId": 0
        });
        var tmparray = this.state.person;
        tmparray[index].firstName = newName;
        this.setState({
            person : tmparray
        });
        document.getElementById("newname"+index).value = "";
        return res;
    }

    render() {
        return (
        <Users functionToDelete={this.handleDelete}
               functionToUpdate={this.handleChangeFirstName} >{this.state.person}</Users>
        );
    }
    componentDidMount() {
        this.getEveryPersonButton();
    }
}

export default PersonsComponent;