import React, {Fragment} from 'react' 

const Select = ({attributes, options}) => {
    return (
        <select {...attributes}>
            <option disabled >Выберите...</option>
            {options.map(option => <option key={option.value}>{option.text}</option>)}
        </select>
    )
}

const Image = ({info}) => {
    const source = `data:${info.format};${info.encoding},${info.data}`;
    
    return <Fragment>
        <img src={source} style={{height:'100px'}}/>
    </Fragment>
}

const ImageField = ({attributes, onValueChange, value = []}) => {
    
    const [images, setImages] = React.useState(value)
    
    attributes.onChange = (event) => {
        
        const promises = [...event.target.files].map(file => 
            new Promise((resolve,reject) => {
                const reader = new FileReader()
                reader.onload = () => {
                    const info = reader.result
                    const [encoding,data] = info.slice(info.indexOf(';') + 1).split(',')
                    
                    resolve({
                        name : file.name.split('.')[0],
                        encoding : encoding,
                        format : file.type,
                        data : data
                    })
                } 
                reader.readAsDataURL(file)
            })
        )

        Promise.all(promises).then(images => {
            onValueChange(attributes.name, images)
            setImages(images)
        }) 
    } 
    return (
        <Fragment>
            <input{...{...attributes, multiple:true, accept:"image/jpeg,image/png"}}/>
            <div>
                {images.map((image,index) => <Image key={index} info={image} />)}
            </div>
        </Fragment>
    ) 
}

const FormField = ({name,type, constants, value, onValueChange}) => { 
    
    const changeHandler = (event) => onValueChange(name,event.target.value)
    
    const attributes = {name : name, onChange : changeHandler}
    const inputAttributes = {...attributes, type : type, value : value}
    
    
    switch(type) { 
        case "textarea": 
            return <textarea {...{...attributes,value : value}}></textarea>
        case "select": 
            return <Select options={constants} attributes={{defaultValue:"Выберите...",...attributes}} />
        case "select multiple" :
            return <Select options= {constants} attributes={{...attributes,multiple: true, onChange: (event) => {
                const values = [...event.target.options]
                    .filter(option => option.selected)
                    .map(x => x.value);

                onValueChange(name,values)
            }}} />
        case "number":
            return <input {...{...inputAttributes, min: constants?.min , max: constants?.max}}/>
        case "file":
            return <ImageField attributes={inputAttributes} onValueChange={onValueChange}/>
        case "text":
            return <input {...{...inputAttributes, placeholder: constants}}/>
        default : 
            throw "Unexpected field type" 
    } 
} 
const EntityForm = ({template, constants, sendHandler,data = {}}) => { 
    
    const formData = {}

    const UpdateFormData = (name,value) => {
        console.log(formData)

        if (value)
            formData[name] = value
        else formData[name] = null
        
        console.log(formData)
        
    }

    return ( 
            
            <form>
            {
               Object.entries(template).map(([name,type]) => {
                   return <Fragment key={name}>
                        <div>
                            <span>{name}: </span>
                        </div>
                        <div>
                            <FormField onValueChange={UpdateFormData} value={data[name]} name={name} type={type} constants={constants[name]}/>
                        </div>
                    </Fragment>
                }) 
            }
            <button onClick={() => sendHandler(formData)}>Send</button>
            </form>
    ) 
} 
export default EntityForm