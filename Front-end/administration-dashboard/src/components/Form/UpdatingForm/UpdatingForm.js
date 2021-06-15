import axios from 'axios'
import React from 'react'
import EntityForm from '../EntityForm/EntityForm'

const UpdatingForm = ({template, constants, uri}) => {

    const [data,setData] = React.useState([])
    
    React.useEffect(()=>{
        axios.get(uri)
        .then((response) => {
            setData(response.data)
        })
    })

    const Update = (formData) => {
        axios.put(uri, formData, { headers: {'Content-Type': 'application/json' }})
            .then(res => {
                console.log(res.status)
        })
    }
    return <EntityForm template={template} constants={constants} sendHandler={Update} data={data}/>
}

export default UpdatingForm