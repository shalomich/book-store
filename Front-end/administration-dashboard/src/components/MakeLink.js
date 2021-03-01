import React from 'react';
import {Link} from "react-router-dom";

function MakeLink(Component){
    return function BlockLink({name, ...props}) {
        return <Link to={`admin/${name}`}>
            <Component name={name} {...this.props}/>
        </Link>
    }
    
}

export default MakeLink;