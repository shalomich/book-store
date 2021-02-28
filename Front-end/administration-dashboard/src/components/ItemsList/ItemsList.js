import React from 'react';
import Item from "../Item/Item";
import style from "./ItemsList.module.css"

const ItemsList = (props) => {
    const books = [{name: "a", author: "b"}, {name: "b", author: "b"}, {name: "c", author: "b"}, {name: "d", author: "b"}, {name: "e", author: "b"}, {name: "a", author: "b"}];
    const items = books.map((item) =>
        <Item updateData = {props.updateData} item = {item} />
    );

    return (
        <ul>
            <div className={style.button}>
                <button>
                    <i className="fas fa-plus"></i>
                </button>
            </div>
            {items}
        </ul>
    )
}

export default ItemsList;