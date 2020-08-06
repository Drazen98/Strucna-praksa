import React, { Component } from 'react';
import axios from 'axios';

class PersonComponent extends Component {
    constructor(){
        super();
        this.state = {
            person : []
        };
    }
     getEveryPersonButton = async() =>{
        await axios.get("/api/Example/Person").then(response => {
            this.setState({
                person : response.data
            });
        });
    };
    returnTableRows = ()=>{
        var persons = [];
        for(const [index, value] of this.state.person.entries()){
            persons.push(
            <tr key={index}>
                <td>{value.firstName}</td>
                <td>{value.lastName}</td>
                <td>{value.height}</td>
                <td>{value.weight}</td>
            </tr> 
                );
        }
        return (persons);
    }
    render() {
        return (
            <div>
                <p> Persons in database</p>
                <table className="zui-table">
                    <thead>
                        <tr>
                            <th>First name</th>
                            <th>Last name</th>
                            <th>Height</th>
                            <th>Weight</th>
                        </tr>   
                    </thead>
                    <tbody>
                       {this.returnTableRows()}
                    </tbody>
                </table>
                <button onClick={this.getEveryPersonButton}>Get every person</button>
            </div>
        );
    }
}

export default PersonComponent;