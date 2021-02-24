import React from 'react';
import Item from "../Product/Item";
import "./ProductsList.module.css"

const ItemsList = (props) => {
    const items = props.goods.map((item) =>
        <Item updateData = {props.updateData} goods = {item} />
    );

    return (
        <ul className="products-block">{items}</ul>
        )
}

export default ItemsList;