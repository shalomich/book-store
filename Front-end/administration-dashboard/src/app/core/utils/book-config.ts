import { Injectable } from '@angular/core';

import { ProductConfig } from '../interfaces/product-config';
import { RelatedEntityConfig } from '../interfaces/related-entity-config';
import { EntityType } from '../interfaces/entity-type';

@Injectable({
  providedIn: 'root',
})
export class BookConfig implements ProductConfig {

  public readonly entityType: EntityType = {
    value: 'book',
    name: 'Книги',
  };

  public readonly authorConfig: RelatedEntityConfig = {
    entityType: {
      value: 'author',
      name: 'Авторы',
    },
  };

  public readonly publisherConfig: RelatedEntityConfig = {
    entityType: {
      value: 'publisher',
      name: 'Издатели',
    },
  };

  public readonly bookTypeConfig: RelatedEntityConfig = {
    entityType: {
      value: 'bookType',
      name: 'Типы книг',
    },
  };

  public readonly genreConfig: RelatedEntityConfig = {
    entityType: {
      value: 'genre',
      name: 'Жанры',
    },
  };

  public readonly ageLimitConfig: RelatedEntityConfig = {
    entityType: {
      value: 'ageLimit',
      name: 'Возрастные ограничения',
    },
  };

  public readonly coverArtConfig: RelatedEntityConfig = {
    entityType: {
      value: 'coverArt',
      name: 'Типы обложек',
    },
  };

  public readonly relatedEntityConfigs = [
    this.authorConfig,
    this.publisherConfig,
    this.bookTypeConfig,
    this.genreConfig,
    this.ageLimitConfig,
    this.coverArtConfig,
  ];


}
