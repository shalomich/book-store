export const ChangeAction = {
    Exclude: 'exclude', 
    Stay: 'stay'
}

const ChangeInfo = (InfoItemComponent, propertyNames, action) => {
    const ChangedItem = (props) => {
        if ((propertyNames.includes(props.propertyName) && action === ChangeAction.Stay)
            || (!propertyNames.includes(props.propertyName) && action === ChangeAction.Exclude))
            return <InfoItemComponent {...props}/>

        return null;
    }
    return ChangedItem;
}

export default ChangeInfo