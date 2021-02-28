import React from 'react';
import MainPage from "./MainPage";
import {BrowserRouter, Route, Redirect} from "react-router-dom";
import {Component} from "react/cjs/react.production.min";
import EntityPage from "./EntityPage";

class PagesManager extends Component{
    constructor(props) {
        super(props);
        this.state = {product: String()}
    }

    updateData = (value) => {
        this.setState({ product: value })
    }

    render() {
        return (
            <BrowserRouter>
                <div>
                    <Redirect from="/" to="/admin" />
                    <Route exact path="/admin" render={() => <MainPage updateData = {this.updateData} goods = {this.props.goods}/>}/>
                    <Route strict path="/admin/" render={() => <EntityPage name={this.state.product} />}/>
                </div>
            </BrowserRouter>
        );
    }
}

export default PagesManager;