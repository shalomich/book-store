import React from 'react';
import style from './Image.module.css';


export const Image = ({imageObj}) => {
    const imageSource = `data:${imageObj?.format};${imageObj?.encoding},${imageObj?.data}`;

    return (
        <>
            <img src={imageSource} className={style.image}/>
        </>
        )
}