import {ChangeDetectionStrategy, Component, OnDestroy, OnInit, ViewChild} from '@angular/core';

import {Observable, Subscription} from 'rxjs';

import {ProfileProviderService} from '../core/services/profile-provider.service';
import {BasketService} from '../core/services/basket.service';
import {UserProfile} from '../core/models/user-profile';
import {BasketProduct} from '../core/models/basket-product';
import {OrderService} from '../core/services/order.service';
import {Router} from '@angular/router';
import {PersonalDataFormComponent} from './personal-data-form/personal-data-form.component';
import {getTotalCost} from '../core/utils/helpers';

@Component({
  selector: 'app-order-page',
  templateUrl: './order-page.component.html',
  styleUrls: ['./order-page.component.css'],
})
export class OrderPageComponent implements OnInit, OnDestroy {

  public userProfile$: Observable<UserProfile> = new Observable<UserProfile>();

  @ViewChild('dataForm')
  public personalData: PersonalDataFormComponent = new PersonalDataFormComponent();

  public basketProducts: BasketProduct[] = [];

  public totalCost = 0;

  private readonly subs: Subscription = new Subscription();

  public constructor(
    private readonly profileProviderService: ProfileProviderService,
    private readonly basketService: BasketService,
    private readonly orderService: OrderService,
    private readonly router: Router,
  ) {
    this.userProfile$ = this.profileProviderService.userProfile;
  }

  public ngOnInit(): void {
    this.subs.add(this.basketService.basketProducts.subscribe(products => {
      this.basketProducts = products;
      this.totalCost = getTotalCost(products);
    }));

    if (!this.basketService.basketProductsValue.length) {
      this.basketService.getBasket();
    }
  }

  public ngOnDestroy() {
    this.subs.unsubscribe();
  }

  public onOrderApply() {
    this.subs.add(this.orderService.applyOrder(this.personalData.personalDataForm.value).subscribe(_ => {
      this.router.navigate(['/']);
    }));
  }
}
