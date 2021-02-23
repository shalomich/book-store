import React from 'react';
import {Component} from "react/cjs/react.production.min";
import {NavLink} from "react-router-dom";

class Item extends Component {
    constructor(props) {
        super(props);
        this.state = {date: new Date()};
        this.link = "/" + this.props.goods.type;
    }

    render() {

        return (
            <div>
                <button><NavLink to={this.link}>{this.props.goods.name}</NavLink></button>
            </div>
        );
    }
}

export default Item;