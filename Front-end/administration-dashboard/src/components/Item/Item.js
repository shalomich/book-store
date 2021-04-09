import React from 'react';
import {Component} from "react/cjs/react.production.min";
import {Redirect, withRouter} from "react-router-dom";
import style from "./Item.module.css"
import axios from "axios";
import {resolveToLocation} from "react-router-dom/modules/utils/locationUtils";

class Item extends Component {
    constructor(props) {
        super(props);
        this.link = "/form";
        this.clicked = false;
        const information = Object.entries(this.props.item);
        information.splice(2);
        this.imageSource = '';
        this.props.item.images.forEach((image) => {
            if (image.name === this.props.item.titleImageName) {
                this.imageSource = `data:${image.format};${image.encoding},${image.data}`;
            }
        })
        this.info = information.map((field, index) =>
            <div key = {index} className={style.item}>
                <span>{field[0]}: </span>
                <span>{field[1]}</span>
            </div>
        );
    }


    handleClickDelete = (e) => {
        e.preventDefault();
        axios.delete("https://localhost:44327/storage/publication/" + this.props.item.id)
            .then(res => {
                this.props.history.go(0)
            });
    };

    handleClickEdit = (e) => {
        e.preventDefault();
        sessionStorage.setItem('editItemId', this.props.item.id)
        this.props.updateData(this.props.name, this.props.type, 'edit')
        this.props.history.push("/admin/" + this.props.type + "/form")
    };


    render() {
        return (
            <div className={style.item_block}>
                <img className={style.image} src={this.imageSource}/>
                {this.info}
                <div className={style.button_block}>
                    <hr className={style.line}/>
                    <button className={style.edit_button} onClick={this.handleClickEdit}>Изменить</button>
                    <button className={style.delete_button} onClick={this.handleClickDelete}>Удалить</button>
                </div>
            </div>
        );
    }
}

export default withRouter(Item);