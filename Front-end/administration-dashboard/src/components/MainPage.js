import React from 'react';
import ProductsList from "./ProductsList/ProductsList";

const MainPage = (props) => {
    return (
        <div>
            <h1 style={{marginLeft: "65px"}}>Товары</h1>
            <ProductsList updateData = {props.updateData} goods = {props.goods}/>
        </div>
    );
}

export default MainPage;