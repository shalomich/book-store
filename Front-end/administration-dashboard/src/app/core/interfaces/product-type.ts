import { EntityType } from './entity-type';

export interface ProductType extends EntityType{

  relatedEntities: EntityType[];
}
