export const createForm = (formData, config) => {
    const information = Object.entries(formData);

    return  information.map((field) => {
        if (field[1] === "textarea") {
            return createTextAreaBlock(field)
        } else if (field[1].indexOf("select") !== -1) {
            for (let key in config) {
                if (key === field[0]) {
                    return createSelectBlock(config[key], field)
                }
            }
            return createSelectBlock([], field)
        } else {
            for (let key in config) {
                if (key === field[0]) {
                    return createInputBlock(config[key], field)
                }
            }
            return createInputBlock(null, field)
        }
    });
}

const createSelectBlock = (optionsData, field) => {
    return (
        <>
            <div>
                <span>{field[0]}: </span>
            </div>
            <div style={{marginBottom: '10px'}}>
                {createSelect(optionsData, field)}
            </div>
        </>
    )
}

const createSelect = (optionsData, field) => {
    const options = optionsData.map((field) => {
        return <option value={field.value}>{field.key}</option>
    });
    if (field[1].indexOf("multiple") !== -1) {
        return (
        <select name = {field[0]} multiple style={{width: "200px"}}>
            {options}
        </select>
        )
    }
    else {
        return (
            <select defaultValue="Выберите..." name = {field[0]} style={{width: "200px"}}>
                <option disabled >Выберите...</option>
                {options}
            </select>
        )
    }
}

const createInputBlock = (value, field) => {
    return (
        <>
            <div>
                <span>{field[0]}: </span>
            </div>
            <div style={{marginBottom: '10px'}}>
                {createInput(value, field)}
            </div>
        </>
    )
}

const createInput = (value, field) => {
    if (value === null) {
        if (field[1] === 'file') {
            return (
                <input style={{width: "300px"}} name = {field[0]} type={field[1]} multiple accept="image/jpeg,image/png"/>
                )
        }
        else {
            return (
                <input style={{width: "200px"}} name = {field[0]} type={field[1]}/>
            )
        }
    }
    else {
        if (field[1] === 'text') {
            return (
                <input style={{width: "200px"}} name = {field[0]} type={field[1]} placeholder={value}/>
            )
        }
        else {
            return (
                <input style={{width: "200px"}} name = {field[0]} type={field[1]} max={value.max} min={value.min}/>
            )
        }
    }
}

const createTextAreaBlock = (field) => {
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
}


