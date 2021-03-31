import React from 'react';
import MainPage from "./MainPage";
import {BrowserRouter, Route, Redirect} from "react-router-dom";
import {Component} from "react/cjs/react.production.min";
import EntityPage from "./EntityPage";
import ItemForm from "./ItemForm/ItemForm";

class PagesManager extends Component{
    constructor(props) {
        super(props);
        this.state = {
            product: String(),
            type: String(),
            action: String()
        }
    }

    updateData = (product, type) => {
        this.setState({ product: product, type: type })
    }

    render() {
        return (
            <BrowserRouter>
                <div>
                    <Route exact path="/admin" render={() => <MainPage updateData = {this.updateData} goods = {this.props.goods}/>}/>
                    <Route exact path="/admin/:String" render={() => <EntityPage updateData = {this.updateData} type={this.state.type} name={this.state.product} />}/>
                    <Route path="/admin/:String/form" render={() => <ItemForm type={this.state.type} name={this.state.product}/>}/>
                </div>
            </BrowserRouter>
        );
    }
}

export default PagesManager;