import React from 'react';
import {Component} from "react/cjs/react.production.min";
import {Redirect} from "react-router-dom";

class Item extends Component {
    constructor(props) {
        super(props);
        this.link = "/" + this.props.goods.type;
        this.clicked = false;
    }


    handleClick = () => {
        console.log("Hello");
        this.props.updateData(this.props.goods.name);
        this.clicked = true;
    }

    render() {
        if (this.clicked) {
            return <Redirect to={this.link}/>
        }

        return (
            <div>
                <button onClick={this.handleClick}>{this.props.goods.name}</button>
            </div>
        );
    }
}

export default Item;