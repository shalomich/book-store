import { Component, OnDestroy, OnInit } from '@angular/core';

import { Observable, Subscription } from 'rxjs';

import { ProfileProviderService } from '../core/services/profile-provider.service';
import { BasketService } from '../core/services/basket.service';
import { UserProfile } from '../core/models/user-profile';
import { BasketProduct } from '../core/models/basket-product';

@Component({
  selector: 'app-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css'],
})
export class OrderPageComponent implements OnInit, OnDestroy {

  public userProfile$: Observable<UserProfile> = new Observable<UserProfile>();

  public personalData: UserProfile = new UserProfile();

  public basketProducts: BasketProduct[] = [];

  public totalCost = 0;

  private readonly subs: Subscription = new Subscription();

  public constructor(
    private readonly profileProviderService: ProfileProviderService,
    private readonly basketService: BasketService,
  ) {
    this.userProfile$ = this.profileProviderService.userProfile;
  }

  public ngOnInit(): void {
    this.subs.add(this.basketService.basketProducts.subscribe(products => {
      this.basketProducts = products;
      this.totalCost = products.reduce((sum, a) => sum + (a.cost * a.quantity), 0);
    }));

    if (!this.basketService.basketProductsValue.length) {
      this.basketService.getBasket();
    }
  }

  public ngOnDestroy() {
    this.subs.unsubscribe();
  }

  public updatePersonalData(personalData: UserProfile) {
    this.personalData = personalData;
  }
}
