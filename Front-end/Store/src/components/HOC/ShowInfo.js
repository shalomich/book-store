import {Fragment} from 'react'

const ShowInfo = (InfoItemComponent) =>{
    const ItemList = ({object}) =>{
        return (
            <Fragment>
                {
                    Object.entries(object).map(([propertyName, value]) => {
                        return <InfoItemComponent key={propertyName} propertyName={propertyName} value={value}/>
                    })
                }
            </Fragment>
        )
    }

    return ItemList;
}

export default ShowInfo