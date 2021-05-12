import React, {Fragment} from 'react';

export const BaseInfoItem = ({propertyName,value}) => {
    return (
        <Fragment>
            <b>{propertyName}</b><br/>
            <span>{value?.toString()}</span><br/>
        </Fragment>
    )
}

export const SpareInfoItem = ({propertyName,value}) => {
    return (
        <Fragment>
            <span>{propertyName}: {value?.toString()}</span><br/>
        </Fragment>
    )
}