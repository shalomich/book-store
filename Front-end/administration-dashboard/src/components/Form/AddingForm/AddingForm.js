import React from 'react'
import EntityForm from '../EntityForm/EntityForm'
import axios from 'axios'

const AddingForm = ({template, constants, uri}) => {
    
    const Add = (formData) => {
        axios.post(uri, formData, { headers: {'Content-Type': 'application/json' }})
            .then(res => {
                console.log(res.status)
        })
    }
    return <EntityForm template={template} constants={constants} sendHandler={Add}/>
}

export default AddingForm