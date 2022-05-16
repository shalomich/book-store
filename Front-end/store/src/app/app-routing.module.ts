import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { BookCardComponent } from './product-card-page/book-card/book-card.component';
import { BookSearchPageComponent } from './book-search-page/book-search-page.component';
import { MainPageComponent } from './main-page/main-page.component';
import { SelectionPageComponent } from './selection-page/selection-page.component';
import { BasketPageComponent } from './basket-page/basket-page.component';
import { AuthGuard } from './core/guards/auth.guard';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { OrderPageComponent } from './order-page/order-page.component';
import { BattlePageComponent } from './battle-page/battle-page.component';
import { OrdersListPageComponent } from './orders-list-page/orders-list-page.component';

const routes: Routes = [
  { path: '', redirectTo: 'book-store', pathMatch: 'full' },
  { path: 'book-store', component: MainPageComponent },
  { path: 'book-store?:openTelegramDialog', component: MainPageComponent },
  { path: 'book-store/profile', component: ProfilePageComponent, canActivate: [AuthGuard] },
  { path: 'book-store/basket', component: BasketPageComponent, canActivate: [AuthGuard] },
  { path: 'book-store/order', component: OrderPageComponent, canActivate: [AuthGuard] },
  { path: 'book-store/orders', component: OrdersListPageComponent, canActivate: [AuthGuard] },
  { path: 'book-store/catalog/book', component: BookSearchPageComponent },
  { path: 'book-store/catalog/book/:id', component: BookCardComponent },
  { path: 'book-store/catalog/selection/:selectionName', component: SelectionPageComponent },
  { path: 'book-store/battle', component: BattlePageComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule { }
