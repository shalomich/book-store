import React from 'react';
import Item from "../Item/Item";
import style from "./ItemsList.module.css"
import { withRouter } from "react-router-dom"
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faPlus } from '@fortawesome/free-solid-svg-icons'

const ItemsList = (props) => {
    let cutItem = {
        id: null,
        name: null,
        titleImageName: null,
        images: null
    }
    console.log(props.data)
    const items = props.data.map((item) =>
        <Item key = {item.id} {...props} item = {cutItem = {id: item.id, name: item.name,
            titleImageName: item.titleImageName, images: item.images}}/>
    );

    const handleClick = (e) => {
        e.preventDefault();
        props.updateData(props.name, props.type);
        props.history.push("/admin/" + props.type + "/form")
    }

    return (
        <div className={style.list_block}>
            <button onClick={handleClick} className={style.button}>
                <FontAwesomeIcon icon={faPlus} size="6x"/>
            </button>
            <ul className={style.list}>
                {items}
            </ul>
        </div>
    )
}

export default withRouter(ItemsList);