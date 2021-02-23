import React from 'react';
import Item from "./Item";

const ItemsList = (props) => {
    const items = props.goods.map((item) =>
        <Item goods = {item} />
    );

    return (
        <div>{items}</div>
        )
}

export default ItemsList;