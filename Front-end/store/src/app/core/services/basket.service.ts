import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, Subject } from 'rxjs';

import { HttpClient } from '@angular/common/http';

import { BasketProduct } from '../interfaces/basket-product';
import { BASKET_URL } from '../utils/values';

import { AuthorizationDataProvider } from './authorization-data.provider';

@Injectable({
  providedIn: 'root',
})
export class BasketService {

  private _basketProducts: BehaviorSubject<BasketProduct[]> = new BehaviorSubject<BasketProduct[]>([]);

  constructor(private readonly http: HttpClient, private readonly authorizationDataProvider: AuthorizationDataProvider) { }

  public getBasket(): void {
    this.http.get<BasketProduct[]>(BASKET_URL).subscribe(data => this._basketProducts.next(data));
  }

  public get basketProducts(): Observable<BasketProduct[]> {
    return this._basketProducts.asObservable();
  }

  public addProduct(productId: number): void {
    const headers = {
      Authorization: `Bearer ${this.authorizationDataProvider.token.value}`,
    };

    this.http.post(BASKET_URL, { productId }, { headers }).subscribe();
  }

  public removeAll(): void {
    const headers = {
      Authorization: `Bearer ${this.authorizationDataProvider.token.value}`,
    };

    this.http.delete(BASKET_URL, { headers }).subscribe();
  }

  public remove(product: BasketProduct): void {
    const headers = {
      Authorization: `Bearer ${this.authorizationDataProvider.token.value}`,
    };

    const { id } = product;

    this.http.delete(`${BASKET_URL}/${id}`, { headers }).subscribe();
  }

  public saveBasket(): void {
    const headers = {
      Authorization: `Bearer ${this.authorizationDataProvider.token.value}`,
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
