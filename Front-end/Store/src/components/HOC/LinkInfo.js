const LinkInfo = (InfoItemComponent, links) => {
    const LinkItem = (props) => {

        const link = links.get(props.propertyName);
        if (!link) {
            return (
                <a href={link}>
                    <InfoItemComponent {...props} />
                </a>
            )
        } else return <InfoItemComponent {...props} />
    }
    return LinkItem
}

export default LinkInfo;