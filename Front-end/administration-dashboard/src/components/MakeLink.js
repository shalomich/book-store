import React from 'react';
import {Link} from "react-router-dom";

function MakeLink(Component){
    return function BlockLink({type, ...props}) {
        return <Link to={`admin/${type}`} style={{textDecoration:'none'}}>
            <Component type={type} {...props}/>
        </Link>
    }
    
}

export default MakeLink;