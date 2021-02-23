import React from 'react';
import MainPage from "./MainPage";
import {BrowserRouter} from "react-router-dom";
import {Component} from "react/cjs/react.production.min";

class PagesManager extends Component{
    updateData = (value) => {
        this.setState({ name: value })
    }

    render() {
        return (
            <BrowserRouter>
                <div>
                    <MainPage goods = {this.props.goods}/>
                </div>
            </BrowserRouter>
        );
    }
}

export default PagesManager;