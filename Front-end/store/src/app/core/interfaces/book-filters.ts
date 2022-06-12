import {RelatedEntity} from '../models/related-entity';

export interface BookFilters {

  readonly bookTypes: RelatedEntity[];

  readonly ageLimits: RelatedEntity[];

  readonly coverArts: RelatedEntity[];

  readonly genres: RelatedEntity[];
}
