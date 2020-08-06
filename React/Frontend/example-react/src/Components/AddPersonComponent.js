import React, { Component } from 'react';
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
        await axios.post("/api/Example/Person",this.state).then(res => console.log(res));
        return;
    }

    render() {
        return (
            <div className="container">
            <form onSubmit={e=>this.handleInputForm(e)}>
                    <label htmlFor="firstname" className="label"><b>First name:</b></label>
                    <input type="text" id="firstname" value={this.state.firstName} onChange={e => this.setState({ firstName: e.target.value})}/>
                    <label htmlFor="lastname" className="label"><b>Last name: </b></label>
                    <input type="text" id="lastname" value={this.state.lastName} onChange={e => this.setState({ lastName: e.target.value})} />
                <label htmlFor="height" className="label"><b>Height: </b></label>
                    <input type="text" id="height" value={this.state.height} onChange={e => this.setState({ height: e.target.value})}/>
                <label htmlFor="weight" className="label"><b>Weight: </b></label>
                <input type="text" id="weight" value={this.state.weight} onChange={e => this.setState({ weight: e.target.value})} />
      
                <button type="submit" className="button">Add</button>   
            </form>
            </div>
        );
    }
}

export default AddPersonComponent;