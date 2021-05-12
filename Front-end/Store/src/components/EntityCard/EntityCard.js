import React, { Fragment } from 'react';

import {BaseInfoItem} from '../Info/Info';
import MakeCard from '../HOC/MakeCard'
import ChangeInfo,{ChangeAction} from '../HOC/ChangeInfo'

import style from './EntityCard.module.css';

const entityExcludedProperties = ["id","titleImageName","images"]

const productExcludedProperties = entityExcludedProperties.concat(["addingDate"])

const publisherExcludedProperties = entityExcludedProperties.concat(["publications"])

const authorExcludedProperties = entityExcludedProperties.concat(["publications"])

const publicationExcludedProperties = productExcludedProperties.concat(["authorId", "publisherId", "author", "publisher"])

const imageBlockClasses = `${style.image_block} col-6`;
const infoBlockClasses = `${style.info_block} col-6`;

const CardBlock = ({entity, propertyNames}) => {
    const Card = MakeCard(
        ChangeInfo(BaseInfoItem,propertyNames,ChangeAction.Exclude),
        imageBlockClasses,
        infoBlockClasses
    );

    return (
        <div className='row'>
            <Card entity={entity} />
        </div>
    )
}

export const PublicationCard = ({entity}) => <CardBlock entity={entity} propertyNames={publicationExcludedProperties}/>