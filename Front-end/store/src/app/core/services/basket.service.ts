import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { HttpClient } from '@angular/common/http';

import { BASKET_URL } from '../utils/values';

import { BasketProductMapper } from '../mappers/basket-product.mapper';

import { BasketProductDto } from '../DTOs/basket-product-dto';
import { BasketProduct } from '../models/basket-product';

import { AuthorizationDataProvider } from './authorization-data.provider';
import { AuthorizationService } from './authorization.service';

@Injectable({
  providedIn: 'root',
})
export class BasketService {

  private _basketProducts: BehaviorSubject<BasketProduct[]> = new BehaviorSubject<BasketProduct[]>([]);

  constructor(
    private readonly http: HttpClient,
    private readonly authorizationService: AuthorizationService,
    private readonly basketProductMapper: BasketProductMapper,
  ) { }

  public getBasket(): void {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    this.http.get<BasketProductDto[]>(BASKET_URL, { headers })
      .subscribe(data => this._basketProducts.next(data.map(item => this.basketProductMapper.fromDto(item))));
  }

  public get basketProducts(): Observable<BasketProduct[]> {
    return this._basketProducts.asObservable();
  }

  public get basketProductsValue(): BasketProduct[] {
    return this._basketProducts.value;
  }

  public addProduct(productId: number): Observable<{}> {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    return this.http.post(BASKET_URL, { productId }, { headers });
  }

  public removeAll(): void {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    this._basketProducts.next([]);
    this.http.delete(BASKET_URL, { headers }).subscribe();
  }

  public remove(inputProduct: BasketProduct): void {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    const { id } = inputProduct;

    this._basketProducts.next(this._basketProducts.value.filter(product => product.id !== id));

    this.http.delete(`${BASKET_URL}/${id}`, { headers }).subscribe();
  }

  public saveBasket(): void {
    const headers = {
      Authorization: `Bearer ${this.authorizationService.accessToken}`,
    };

    this._basketProducts.value.forEach(product => {
      const body = { id: product.id, quantity: product.quantity };
      this.http.put(BASKET_URL, body, { headers }).subscribe();
    });
  }

  public increase(inputProduct: BasketProduct): void {
    this._basketProducts.next(this._basketProducts.value.map(product => {
      if (inputProduct.id === product.id) {
        return {
          ...product,
          quantity: product.quantity + 1,
        };
      }

      return product;
    }));
  }

  public decrease(inputProduct: BasketProduct): void {
    this._basketProducts.next(this._basketProducts.value.map(product => {
      if (inputProduct.id === product.id) {
        return {
          ...product,
          quantity: product.quantity - 1,
        };
      }

      return product;
    }));
  }

}
