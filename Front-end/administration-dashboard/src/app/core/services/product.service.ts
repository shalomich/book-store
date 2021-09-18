import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

import { map } from 'rxjs/operators';

import { ProductPreviewMapper } from '../mappers/product-preview.mapper';

import { EntityService } from './entity.service';

@Injectable({
  providedIn: 'root',
})
export class ProductService extends EntityService {

  public constructor(
    http: HttpClient,
    private readonly productPreviewMapper: ProductPreviewMapper,
  ) {
    super(http);
  }
}
