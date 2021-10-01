
import {EntityRestService} from "./entity-rest.service";
import {Book} from "../models/book";
import {Observable} from "rxjs";
import {BookDto} from "../DTOs/book-dto";
import {map} from "rxjs/operators";
import {ProductDto} from "../DTOs/product-dto";
import {Product} from "../models/product";
import { Mapper } from '../mappers/mapper/mapper';
import { Injectable } from '@angular/core';
import { ProductTypeConfigurationService } from './product-type-configuration.service';
import { ProductConfig } from '../interfaces/product-config';

export abstract class ProductCrudService<TProductDto extends ProductDto, TProduct extends Product> {

  protected get type() : string {
    return this.config.entityType.value;
  }

  protected constructor (
    private readonly mapper: Mapper<TProductDto, TProduct>,
    private readonly config: ProductConfig,
    protected readonly entityService: EntityRestService
  ) {

  }

  public add(product: TProduct): Observable<void> {
    const productDto = this.mapper.toDto(product);
    productDto.id = 0;

    return this.entityService.add(this.type, productDto);
  }

  public edit(product: TProduct): Observable<void> {
    const productDto = this.mapper.toDto(product);

    return this.entityService.edit(this.type, productDto.id, product);
  }

  public delete(id: number): Observable<void> {
    return this.entityService.delete(this.type, id);
  }

  public getById(id: number): Observable<TProduct> {
    return this.entityService.getById(this.type, id)
      .pipe(
        map(entityDto => this.mapper.fromDto(entityDto as TProductDto)),
      );
  }
}
