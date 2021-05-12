import {Image} from '../Image/Image'
import ShowInfo from './ShowInfo'
import {Fragment} from 'react'


const MakeCard = (InfoItem, imageBlockClasses, infoBlockClasses) => {
    const Card = ({entity}) => {

        const images = entity.images;
        const titleImage = images.find(image => image.name === entity.titleImageName);

        const InfoBlock = ShowInfo(InfoItem);

        return (
            <Fragment>
                <div className={imageBlockClasses}>
                    <Image imageObj={titleImage} />
                </div>
                <div className={infoBlockClasses}>
                    <InfoBlock object={entity}/>
                </div>
            </Fragment>
        )
    }

    return Card;
}

export default MakeCard