import React, {useEffect, useState} from 'react';
import ItemsList from "./ItemsList/ItemsList";
import {render} from "@testing-library/react";
import {Redirect, withRouter} from "react-router-dom";
import axios from "axios";
import {getData} from "../API/api";

const EntityPage = (props) => {
    let clicked = false;
    const [data, setData] = useState();

    useEffect(() => {
        console.log(props.type)
        axios.get("https://localhost:44327/storage/publication")
            .then(res => {
                if(res.status===200){
                    setData(res.data)
                }
            });
    }, [setData]);

    const handleClick = (e) => {
        e.preventDefault();
        props.history.push("/admin")
    }

    if (!data) {
        return (<div>
            <p>Данные отстутствуют</p>
        </div>)
    }

    return (
        <div>
            <button onClick={handleClick}>На главную</button>
            <h1 style={{marginLeft: "65px", marginBottom: "50px"}}>{props.name}</h1>
            <ItemsList data = {data} {...props} />
        </div>
    );
}

export default withRouter(EntityPage);