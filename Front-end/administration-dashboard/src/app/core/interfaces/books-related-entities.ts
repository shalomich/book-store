import { RelatedEntity } from '../models/related-entity';

export interface BooksRelatedEntities {
  genres: RelatedEntity[];
  ageLimits: RelatedEntity[];
  authors: RelatedEntity[];
  types: RelatedEntity[];
  coverArts: RelatedEntity[];
  publishers: RelatedEntity[];
}
