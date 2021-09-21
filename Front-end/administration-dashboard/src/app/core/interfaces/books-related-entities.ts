import { RelatedEntity } from '../models/related-entity';

export interface BooksRelatedEntities {
  genres: RelatedEntity[];
  ageLimits: RelatedEntity[];
  authors: RelatedEntity[];
  bookTypes: RelatedEntity[];
  coverArts: RelatedEntity[];
  publishers: RelatedEntity[];
}
