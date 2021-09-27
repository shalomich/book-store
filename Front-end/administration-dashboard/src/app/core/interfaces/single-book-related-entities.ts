import { RelatedEntity } from '../models/related-entity';

export interface SingleBookRelatedEntities {
  genres: RelatedEntity[];
  ageLimit: RelatedEntity;
  author: RelatedEntity;
  type: RelatedEntity;
  coverArt: RelatedEntity;
  publisher: RelatedEntity;
}
