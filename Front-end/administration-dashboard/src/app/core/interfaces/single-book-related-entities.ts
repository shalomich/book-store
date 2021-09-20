import { RelatedEntity } from '../models/related-entity';

export interface SingleBookRelatedEntities {
  genres: RelatedEntity[];
  ageLimit: RelatedEntity;
  authors: RelatedEntity;
  bookType: RelatedEntity;
  coverArt: RelatedEntity;
  publisher: RelatedEntity;
}
