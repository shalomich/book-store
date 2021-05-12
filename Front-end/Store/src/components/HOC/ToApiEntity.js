import React from 'react';
import axios from 'axios'
import {useState} from "react";

const ToApiEntity = (EntityComponent, uri) => {
    const ApiEntity = () => {
        const [entity, setEntity] = useState();

        React.useEffect(() => {
            axios.get(uri)
                .then(res => {
                    setEntity(res.data)
                });
        }, [setEntity]);

        if (entity === undefined)
            return <span>Loading...</span>;
        else
            return <EntityComponent entity={entity}/>

    }
    return ApiEntity;
}

export default ToApiEntity