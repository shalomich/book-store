export const setFormData = (formData, config, data) => {
    const information = Object.entries(formData);

    return  information.map((field) => {
        if (field[1] === "textarea") {
            for (let key in data) {
                if (key === field[0]) {
                    return createTextAreaBlock(field, data[key])
                }
            }
            return createTextAreaBlock(field)
        }
        else if (field[1].indexOf("select") !== -1) {
            for (let keyD in data) {
                if (keyD === field[0]) {
                    for (let key in config) {
                        if (key === field[0]) {
                            return createSelectBlock(config[key], field, data[keyD])
                        }
                    }
                    return createSelectBlock([], field, data[keyD])
                }
            }
        } else {
            for (let keyD in data) {
                if (keyD === field[0]) {
                    for (let key in config) {
                        if (key === field[0]) {
                            return createInputBlock(config[key], field, data[keyD])
                        }
                    }
                    return createInputBlock(null, field, data[keyD])
                }
            }
        }
    });
}

const createSelectBlock = (optionsData, field, value) => {
    return (
        <>
            <div>
                <span>{field[0]}: </span>
            </div>
            <div style={{marginBottom: '10px'}}>
                {createSelect(optionsData, field, value)}
            </div>
        </>
    )
}

const createSelect = (optionsData, field, value) => {
    const options = optionsData.map((field) => {
        return <option value={field.value}>{field.key}</option>
    });
    if (field[1].indexOf("multiple") !== -1) {
        return (
            <select defaultValue={value} name = {field[0]} multiple style={{width: "200px"}}>
                {options}
            </select>
        )
    }
    else {
        return (
            <select defaultValue={value} name = {field[0]} style={{width: "200px"}}>
                <option disabled >Выберите...</option>
                {options}
            </select>
        )
    }
}

const createInputBlock = (value, field, defaultValue) => {
    return (
        <>
            <div>
                <span>{field[0]}: </span>
            </div>
            <div style={{marginBottom: '10px'}}>
                {createInput(value, field, defaultValue)}
            </div>
        </>
    )
}

const createInput = (value, field, defaultValue) => {
    if (value === null) {
        if (field[1] === 'file') {
            return (
                <input style={{width: "300px"}} name = {field[0]} type={field[1]} multiple accept="image/jpeg, image/png"/>
            )
        }
        else {
            return (
                <input defaultValue={defaultValue} style={{width: "200px"}} name = {field[0]} type={field[1]}/>
            )
        }
    }
    else {
        if (field[1] === 'text') {
            return (
                <input defaultValue={defaultValue} style={{width: "200px"}} name = {field[0]} type={field[1]} placeholder={value}/>
            )
        }
        else {
            return (
                <input defaultValue={defaultValue} style={{width: "200px"}} name = {field[0]} type={field[1]} max={value.max} min={value.min}/>
            )
        }
    }
}

const createTextAreaBlock = (field, defaultValue) => {
    return (
        <>
            <div>
                <span>{field[0]}: </span>
            </div>
            <div style={{marginBottom: '10px'}}>
                <textarea defaultValue={defaultValue} style={{width: "200px"}} name={field[0]}/>
            </div>
        </>
    )
}


