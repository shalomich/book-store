import { EntityType } from './entity-type';
import { RelatedEntityType } from './related-entity-type';

export interface ProductType extends EntityType{

  relatedEntities: RelatedEntityType[];
}
