import React from 'react';

export const AddUser = (prop) =>{
    return (
        <React.Fragment>
        <div className="container">
        <form onSubmit={e=>prop.functionToAdd(e)}>
                <label htmlFor="firstname" className="label"><b>First name:</b></label>
                <input type="text" id="firstname" value={prop.getState("firstName")} onChange={e => prop.changeState("firstName", e.target.value)}/>
                <label htmlFor="lastname" className="label"><b>Last name: </b></label>
                <input type="text" id="lastname" value={prop.getState("lastName")} onChange={e => prop.changeState( "lastName", e.target.value)} />
            <label htmlFor="height" className="label"><b>Height: </b></label>
                <input type="text" id="height" value={prop.getState("height")} onChange={e => prop.changeState("height", e.target.value)}/>
            <label htmlFor="weight" className="label"><b>Weight: </b></label>
            <input type="text" id="weight" value={prop.getState("weight")} onChange={e => prop.changeState("weight", e.target.value)} />
  
            <button type="submit" className="button">Add</button>   
        </form>
        </div>
        </React.Fragment>
    );
}

export const Users = (prop) => {
    var persons = [];
    for(const [index, value] of prop.children.entries()){
        persons.push(
        <tr key={index}>
            <td>{value.firstName}</td>
            <td>{value.lastName}</td>
            <td>{value.height}</td>
            <td>{value.weight}</td>
            <td><button className="btndelete" onClick={async()=>await prop.functionToDelete(value.personId,index)}
            ><i className="fa fa-trash" /></button></td>
            <td className="thinp"><input type="text" className="inputname" id={"newname"+index}>
                </input><button className="btnedit" onClick={async()=> await prop.functionToUpdate(value.personId,index,document.getElementById("newname"+index).value)}><i className="fa fa-pencil" /></button></td>
        </tr> 
        );
    }
    return (
        <React.Fragment>
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
                {persons}
            </tbody>
            </table>
        </React.Fragment>

    );
}

