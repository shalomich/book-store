import React from 'react';
import style from "./ProductType.module.css"
import MakeLink from "../MakeLink"

const ProductType = ({name, type}) => {
    return (
        <div className={style.product_block}>
            {name}
        </div>
    );
}

export default MakeLink(ProductType);