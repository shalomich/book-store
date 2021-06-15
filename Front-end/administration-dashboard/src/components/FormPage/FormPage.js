import axios from 'axios'
import React, { Fragment } from 'react'

const FormPage = ({Form,match}) => {

    const [template,setTemplate] = React.useState()
    const [constants, setConstants] = React.useState()

    const {entityName,id} = match.params

    const entityUri = `https://localhost:44327/storage/${entityName}`
    
    React.useEffect(() =>{
        
        if (!template || !constants) {
            axios.all([
                axios.get(`${entityUri}/config`),
                axios.get(`${entityUri}/form`)
            ])
            .then((responseArray) => {
                setConstants(responseArray[0].data)
                setTemplate(responseArray[1].data)
            })
        }
        
    })
    
    const formUri = id != undefined ? `${entityUri}/id` : entityUri

    if (!template || !constants)
        return "Wait please"
    else return (

        <Fragment>
            <Form template={template} constants={constants} uri={formUri}/>
        </Fragment>
    )
}

export default FormPage