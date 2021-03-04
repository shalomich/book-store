import React from 'react';
import {Component} from "react/cjs/react.production.min";
import {Redirect, withRouter} from "react-router-dom";
import style from "./Product.module.css"

class Product extends Component {
    constructor(props) {
        super(props);
    }


    handleClick = () => {
        this.props.updateData(this.props.goods.name);
        this.props.history.push("/admin/" + this.props.goods.type)
    }


    render() {
        return (
            <div className={style.product_block}>
                <button className={style.product} onClick={this.handleClick}>{this.props.goods.name}</button>
            </div>
        );
    }
}

export default withRouter(Product);