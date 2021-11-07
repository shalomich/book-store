import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';

import { map } from 'rxjs/operators';

import { Observable } from 'rxjs';

import { BookDto } from '../DTOs/book-dto';
import { PRODUCT_URL } from '../utils/values';
import { BookMapper } from '../mappers/book.mapper';
import { ProductPreview } from '../models/product-preview';
import { ProductPreviewDto } from '../DTOs/product-preview-dto';
import { ProductPreviewMapper } from '../mappers/product-preview.mapper';
import { RelatedEntityDto } from '../DTOs/related-entity-dto';
import { RelatedEntityMapper } from '../mappers/related-entity.mapper';
import { RelatedEntity } from '../models/related-entity';

@Injectable({
  providedIn: 'root',
})
export class BookService {

  private readonly type = 'book';

  public constructor(
    private readonly http: HttpClient,
    private readonly bookMapper: BookMapper,
    private readonly productPreviewMapper: ProductPreviewMapper,
    private readonly relatedEntityMapper: RelatedEntityMapper,
  ) { }

  public getById(id: number) {
    const book$ = this.http.get<BookDto>(`${PRODUCT_URL}${this.type}/${id}`);

    return book$.pipe(
      map(book => this.bookMapper.fromDto(book)),
    );
  }

  public get(params?: HttpParams): Observable<ProductPreview[]> {
    console.log(params);
    return this.http.get<ProductPreviewDto[]>(`${PRODUCT_URL}${this.type}`, { params }).pipe(
      map(books => books.map(book => this.productPreviewMapper.fromDto(book))),
    );
  }

  public getQuantity(params?: HttpParams): Observable<number> {
    return this.http.head(`${PRODUCT_URL}${this.type}`, { observe: 'response', params })
      .pipe(map(response => parseInt(<string>response.headers.get('dataCount'))));
  }

  public getRelatedEntity(entityName: string): Observable<RelatedEntity[]> {
    return this.http.get<RelatedEntityDto[]>(`${PRODUCT_URL}${this.type}/${entityName}`)
      .pipe(map(items => items.map(item => this.relatedEntityMapper.fromDto(item))));
  }
}
