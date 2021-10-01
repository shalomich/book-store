import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { BookMapper } from '../mappers/book.mapper';
import { Book } from '../models/book';

import { BookDto } from '../DTOs/book-dto';

import { EntityRestService } from './entity-rest.service';
import {ProductCrudService} from "./product-crud.service";

@Injectable({
  providedIn: 'root',
})
export class BookCrudService extends ProductCrudService<BookDto, Book>{
  protected readonly type: string = "book";

  public constructor(entityService: EntityRestService) {
    super(new BookMapper(),entityService);
  }
}
