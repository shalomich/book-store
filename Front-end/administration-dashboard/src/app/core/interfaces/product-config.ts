import { RelatedEntityConfig } from './related-entity-config';
import { EntityType } from './entity-type';

export interface ProductConfig {
  readonly entityType : EntityType,
  readonly relatedEntityConfigs: Array<RelatedEntityConfig>
}
