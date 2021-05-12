import {PublicationCard} from '../EntityCard/EntityCard'
import ToApiEntity from "../HOC/ToApiEntity";

const Cards = new Map([['publication',PublicationCard]])

const EntityPage = ({match}) => {
    const {entityName, id} = match.params;
    const uri = `https://localhost:44327/storage/${entityName}/${id}`

    let Card = Cards.get(entityName);
    Card = ToApiEntity(Card,uri)

    return (
        <>
            <a href="/store">Назад</a>
            <Card/>
        </>
    )
}

export default EntityPage;