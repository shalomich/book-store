import React from 'react';
import {Component} from "react/cjs/react.production.min";
import {Redirect} from "react-router-dom";
import style from "./Product.module.css"

class Product extends Component {
    constructor(props) {
        super(props);
        this.state = {
            link: "/admin/" + this.props.goods.type,
            clicked: false
        }
    }


    handleClick = () => {
        this.props.updateData(this.props.goods.name);
        this.state.clicked = true;
    }


    render() {
        if (this.state.clicked) {
            return <Redirect to={this.state.link}/>
        }

        return (
            <div className={style.product_block}>
                <button className={style.product} onClick={this.handleClick}>{this.props.goods.name}</button>
            </div>
        );
    }
}

export default Product;