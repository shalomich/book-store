import React from 'react';
import ItemsList from "./ProductsList/ItemsList";

const MainPage = (props) => {
    return (
        <div>
            <h1 style={{marginLeft: "65px"}}>Товары</h1>
            <ItemsList updateData = {props.updateData} goods = {props.goods}/>
        </div>
    );
}

export default MainPage;