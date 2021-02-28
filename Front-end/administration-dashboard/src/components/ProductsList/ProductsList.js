import React from 'react';
import Product from "../Product/Product";
import "./ProductsList.module.css"

const ProductsList = (props) => {
    const items = props.goods.map((item) =>
        <Product updateData = {props.updateData} goods = {item} />
    );

    return (
        <ul>{items}</ul>
        )
}

export default ProductsList;