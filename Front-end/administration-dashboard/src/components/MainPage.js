import React from 'react';
import ItemsList from "./ItemsList";

const MainPage = (props) => {
    return (
        <div>
            <h1>Товары</h1>
            <ItemsList goods = {props.goods}/>
        </div>
    );
}

export default MainPage;