import React from 'react';
import PersonComponent from './Components/PersonComponent';
import NavbarComponent from './Components/NavbarComponent';
import AddPersonComponent from './Components/AddPersonComponent';
import HomePageComponent from './Components/HomePageComponent';

import {BrowserRouter as Router,Switch,Route} from'react-router-dom';

import './App.css';

function App() {
  return (
    <Router>
      <div className="App">
          <NavbarComponent />
          <Switch>
          <Route path="/" component={HomePageComponent} exact/>
            <Route path="/AddPerson" component={AddPersonComponent } />
            <Route path="/Persons" component={PersonComponent} />
          </Switch>
      </div>
    </Router>
  );
}

export default App;
