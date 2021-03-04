import React from 'react';
import {Component} from "react/cjs/react.production.min";
import style from "./Item.module.css"

class Item extends Component {
    constructor(props) {
        super(props);
        this.link = "/form";
        this.clicked = false;
        const information = Object.entries(this.props.item);
        this.info = information.map((field) =>
            <div className={style.item}>
                <span>{field[0]}: </span>
                <span>{field[1]}</span>
            </div>
        );
    }



    handleClick = () => {

        this.clicked = true;
    };


    render() {
        // if (this.clicked) {
        //     return <Redirect to={this.link}/>
        // }

        return (
            <div className={style.item_block}>
                {this.info}
                <hr className={style.line}/>
                <button className={style.edit_button} onClick={this.handleClick}>Изменить</button>
                <button className={style.delete_button} onClick={this.handleClick}>Удалить</button>
            </div>
        );
    }
}

export default Item;