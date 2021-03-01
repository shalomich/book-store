import React from 'react';
import ProductType from "../ProductType/ProductType";
import "./ProductsList.module.css"

const ProductsList = ({goods}) => {
    return <ul>
        {
            goods.map((product,index) =>
            <ProductType key={index} name={product.name} type={product.type}/>)
        }
    </ul>
        
}

export default ProductsList;