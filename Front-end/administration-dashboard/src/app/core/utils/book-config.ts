import {ProductConfig} from "../interfaces/product-config";
import { RelatedEntityConfig } from '../interfaces/related-entity-config';
import { EntityType } from '../interfaces/entity-type';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class BookConfig implements ProductConfig {

  readonly entityType : EntityType = {
    value : "book",
    name : "Книги"
  }

  readonly AuthorConfig : RelatedEntityConfig = {
    entityType : {
      value : "author",
      name : "Авторы"
    }
  }

  readonly PublisherConfig : RelatedEntityConfig = {
    entityType : {
      value : "publisher",
      name : "Издатели"
    }
  }

  readonly BookTypeConfig : RelatedEntityConfig = {
    entityType : {
      value : "bookType",
      name : "Типы книг"
    }
  }

  readonly GenreConfig : RelatedEntityConfig = {
    entityType : {
      value : "genre",
      name : "Жанры"
    }
  }

  readonly AgeLimitConfig : RelatedEntityConfig = {
    entityType : {
      value : "ageLimit",
      name : "Возрастные ограничения"
    }
  }

  readonly CoverArtConfig : RelatedEntityConfig = {
    entityType : {
      value : "coverArt",
      name : "Типы обложек"
    }
  }

  readonly relatedEntityConfigs = [this.AuthorConfig, this.PublisherConfig, this.BookTypeConfig, this.GenreConfig, this.AgeLimitConfig, this.CoverArtConfig]



}
