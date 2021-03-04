import React from 'react';
import Item from "../Item/Item";
import style from "./ItemsList.module.css"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPlus } from '@fortawesome/free-solid-svg-icons'

const ItemsList = (props) => {
    const books = [{name: "a", author: "b"}, {name: "b", author: "b"}, {name: "c", author: "b"}, {name: "d", author: "b"}, {name: "e", author: "b"}, {name: "a", author: "b"}];
    const items = books.map((item) =>
        <Item updateData = {props.updateData} item = {item} />
    );

    return (
        <div className={style.list_block}>
            <button className={style.button}>
                <FontAwesomeIcon icon={faPlus} size="6x"/>
            </button>
            <ul className={style.list}>
                {items}
            </ul>
        </div>
    )
}

export default ItemsList;