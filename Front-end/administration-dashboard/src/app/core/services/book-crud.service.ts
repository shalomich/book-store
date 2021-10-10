import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { BookMapper } from '../mappers/book.mapper';
import { Book } from '../models/book';

import { BookDto } from '../DTOs/book-dto';

import { BookConfig } from '../utils/book-config';

import { EntityRestService } from './entity-rest.service';
import { ProductCrudService } from './product-crud.service';
import { ProductTypeConfigurationService } from './product-type-configuration.service';

@Injectable({
  providedIn: 'root',
})
export class BookCrudService extends ProductCrudService<BookDto, Book> {
  protected getType(): string {
    return 'book';
  }

  public constructor(mapper: BookMapper, config: BookConfig, entityService: EntityRestService) {
    super(mapper, config, entityService);
  }
}
