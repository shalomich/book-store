import React from 'react';
import MainPage from "./MainPage";
import {BrowserRouter} from "react-router-dom";
import {Component} from "react/cjs/react.production.min";

class PagesManager extends Component{
    constructor(props) {
        super(props);
        this.state = {goodsName: String()}
        this.link = "/" + this.props.goods.type;
        this.clicked = false;
    }

    updateData = (value) => {
        this.setState({ goodsName: value })
    }

    render() {
        console.log(this.state.goodsName);
        return (
            <BrowserRouter>
                <div>
                    <MainPage updateData = {this.updateData} goods = {this.props.goods}/>
                </div>
            </BrowserRouter>
        );
    }
}

export default PagesManager;