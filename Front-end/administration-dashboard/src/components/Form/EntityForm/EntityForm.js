import React, {Fragment} from 'react' 

const Select = ({attributes, options}) => {
    return (
        <select {...attributes}>
            {!attributes?.multiple && <option disabled >Выберите...</option>}
            {options.map(option => <option key={option.value}>{option.text}</option>)}
        </select>
    )
}

const FormField = ({name, value,type, constants}) => { 
    
    const attributes = {name : name}
    const inputAttributes = {...attributes, type : type, value : value}
    
    switch(type) { 
        case "textarea": 
            return <textarea {...{...attributes,value : value}}></textarea>
        case "select": 
            return <Select attributes={{defaultValue:"Выберите...",...attributes}} options={constants}/>
        case "select multiple" :
            return <Select attributes={{multiple: true,...attributes, }} options= {constants}/>
        case "number":
            return <input {...{...inputAttributes, min: constants?.min , max: constants?.max}}/>
        case "file":
            return <input{...{...inputAttributes, multiple:true, accept:"image/jpeg,image/png"}}/>
        case "text":
            return <input{...{...inputAttributes, placeholder: constants}}/>
        default : 
            throw "Unexpected file type" 
    } 
} 
const EntityForm = ({template, constants, sendHandler,data = {}}) => { 
    
    let formData = {}
    return ( 
            
            <form>
            {
               Object.entries(template).map(([name,type]) => {
                   return <Fragment key={name}>
                        <div>
                            <span>{name}: </span>
                        </div>
                        <div>
                            <FormField value={data[name]} name={name} type={type} constants={constants[name]}/>
                        </div>
                    </Fragment>
                }) 
            }
            <button onClick={() => sendHandler(formData)}>Send</button>
            </form>
    ) 
} 
export default EntityForm