import logo from './logo.svg';
import './App.css';
import EntityPage from "./components/EntityPage/EntityPage";
import {BrowserRouter} from "react-router-dom"
import {Route, Switch} from "react-router";

function App() {
  return (
      <BrowserRouter>
          <Switch>
            <Route exact path='/store/:entityName/:id' component={EntityPage}/>
          </Switch>
      </BrowserRouter>
  );
}

export default App;
