import React from 'react';
import MainPage from "./MainPage";
import {BrowserRouter, Route, Redirect} from "react-router-dom";
import {Component} from "react/cjs/react.production.min";
import EntityPage from "./EntityPage";
import FormPage from "./FormPage/FormPage"
import AddingForm from "./Form/AddingForm/AddingForm"
import UpdatingForm from "./Form/UpdatingForm/UpdatingForm"


class PagesManager extends Component{
    constructor(props) {
        super(props);
        this.state = {
            product: String(),
            type: String(),
            action: String()
        }
    }

    updateData = (product, type, action) => {
        this.setState({ product: product, type: type, action: action })
    }

    render() {
        return (
            <BrowserRouter>
                <div>
                    <Route exact path="/admin" render={() => <MainPage updateData = {this.updateData} goods = {this.props.goods}/>}/>
                    <Route exact path="/admin/:String" render={() => <EntityPage updateData = {this.updateData} type={this.state.type} name={this.state.product} />}/>
                    <Route exact path="/admin/:entityName/form" render={({match}) => <FormPage Form={AddingForm} match={match}/>}/>
                    <Route exact path="/admin/:entityName/form/:id" render={({match}) => <FormPage Form={UpdatingForm} match={match}/>}/>
                </div>
            </BrowserRouter>
        );
    }
}

export default PagesManager;