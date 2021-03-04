import React from 'react';
import ItemsList from "./ItemsList/ItemsList";
import {render} from "@testing-library/react";
import {Redirect, withRouter} from "react-router-dom";

const EntityPage = (props) => {
    let clicked = false;

    const handleClick = (e) => {
        e.preventDefault();
        props.history.push("/admin")
    }

    return (
        <div>
            <button onClick={handleClick}>На главную</button>
            <h1 style={{marginLeft: "65px", marginBottom: "50px"}}>{props.name}</h1>
            <ItemsList />
        </div>
    );
}

export default withRouter(EntityPage);