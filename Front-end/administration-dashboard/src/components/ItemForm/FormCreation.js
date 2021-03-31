import React from "react";

export const createForm = (formData, config) => {
    const information = Object.entries(formData);

    return  information.map((field) => {
        if (field[1] === "textarea") {
            return (
                <>
                    <div>
                        <span>{field[0]}: </span>
                    </div>
                    <div style={{marginBottom: '10px'}}>
                        <textarea style={{width: "200px"}} name={field[0]}/>
                    </div>
                </>
            )
        } else if (field[1].indexOf("select") !== -1) {
            if (field[1].indexOf("multiple") !== -1) {
                for (let key in config) {
                    if (key === field[0]) {
                        return createMultipleSelect(config[key], field[0])
                    }
                }
            } else {
                for (let key in config) {
                    if (key === field[0]) {
                        return createSelect(config[key], field[0])
                    }
                }
            }
        } else {
            for (let key in config) {
                if (key === field[0]) {
                    return createInput(config[key], field)
                }
            }
            return createInput(null, field)
        }
    });
}

const createSelect = (optionsData, name) => {
    const options = optionsData.map((field) => {
        return <option value={field.value}>{field.key}</option>
    });
    return (
        <>
            <div>
                <span>{name}: </span>
            </div>
            <div style={{marginBottom: '10px'}}>
                <select defaultValue="Выберите..." name = {name} style={{width: "200px"}}>
                    <option disabled >Выберите...</option>
                    {options}
                </select>
            </div>
        </>
    )
}

const createMultipleSelect = (optionsData, name) => {
    const options = optionsData.map((field) => {
        return <option value={field.value}>{field.key}</option>
    });
    return (
        <>
            <div>
                <span>{name}: </span>
            </div>
            <div style={{marginBottom: '10px'}}>
                <select name = {name} multiple style={{width: "200px"}}>
                    {options}
                </select>
            </div>
        </>
    )
}

const createInput = (value, field) => {
    if (value === null) {
        if (field[1] === 'file') {
            return (
                <>
                    <div>
                        <span>{field[0]}: </span>
                    </div>
                    <div style={{marginBottom: '10px'}}>
                        <input style={{width: "300px"}} name = {field[0]} type={field[1]} multiple accept="image/jpeg,image/png"/>
                    </div>
                </>
            )
        }
        else {
            return (
                <>
                    <div>
                        <span>{field[0]}: </span>
                    </div>
                    <div style={{marginBottom: '10px'}}>
                        <input style={{width: "200px"}} name = {field[0]} type={field[1]}/>
                    </div>
                </>
            )
        }
    }
    else {
        if (field[1] === 'text') {
            return (
                <>
                    <div>
                        <span>{field[0]}: </span>
                    </div>
                    <div style={{marginBottom: '10px'}}>
                        <input style={{width: "200px"}} name = {field[0]} type={field[1]} placeholder={value}/>
                    </div>
                </>
            )
        }
        else {
            return (
                <>
                    <div>
                        <span>{field[0]}: </span>
                    </div>
                    <div style={{marginBottom: '10px'}}>
                        <input style={{width: "200px"}} name = {field[0]} type={field[1]} max={value.max} min={value.min}/>
                    </div>
                </>
            )
        }
    }


}


