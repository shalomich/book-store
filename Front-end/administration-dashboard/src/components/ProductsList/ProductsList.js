import React from 'react';
import Product from "../Product/Product";
import "./ProductsList.module.css"

const ItemsList = (props) => {
    const items = props.goods.map((item) =>
        <Product updateData = {props.updateData} goods = {item} />
    );

    return (
        <ul className="products-block">{items}</ul>
        )
}

export default ItemsList;