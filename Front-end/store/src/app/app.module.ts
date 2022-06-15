import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { CommonModule } from '@angular/common';

import { HttpClientModule } from '@angular/common/http';

import { NgxPaginationModule } from 'ngx-pagination';

import { MatExpansionModule } from '@angular/material/expansion';

import { MatIconModule } from '@angular/material/icon';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { MatButtonModule } from '@angular/material/button';

import { MatFormFieldModule } from '@angular/material/form-field';

import { MatInputModule } from '@angular/material/input';

import { MatTableModule } from '@angular/material/table';

import { MatPaginatorModule } from '@angular/material/paginator';

import { CdTimerModule } from 'angular-cd-timer';

import { MatTooltipModule } from '@angular/material/tooltip';

import { MatDialogModule } from '@angular/material/dialog';

import { MatChipsModule } from '@angular/material/chips';

import { MatSelectModule } from '@angular/material/select';

import { MatAutocompleteModule } from '@angular/material/autocomplete';

import { MatDividerModule } from '@angular/material/divider';

import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

import { ProductCardPageModule } from './product-card-page/product-card-page.module';
import { CoreModule } from './core/core.module';
import { SharedModule } from './shared/shared.module';
import { BookSearchPageComponent } from './book-search-page/book-search-page.component';
import { SelectionComponent } from './main-page/selection/selection.component';
import { MainPageComponent } from './main-page/main-page.component';
import { SelectionPageComponent } from './selection-page/selection-page.component';
import { CatalogComponent } from './book-search-page/catalog/catalog.component';

import { BasketPageComponent } from './basket-page/basket-page.component';
import { BasketElementComponent } from './basket-page/basket-element/basket-element.component';
import { BasketInfoBlockComponent } from './basket-page/basket-info-block/basket-info-block.component';

import { ProductOptionsStorage } from './core/services/product-options.storage';
import { ProfilePageComponent } from './profile-page/profile-page.component';
import { ProfileProviderService } from './core/services/profile-provider.service';
import { OrderPageComponent } from './order-page/order-page.component';
import { PersonalDataFormComponent } from './order-page/personal-data-form/personal-data-form.component';
import { OrderInfoComponent } from './order-page/order-info/order-info.component';

import { BattlePageComponent } from './battle-page/battle-page.component';


import { BattleInfoDialogComponent } from './battle-page/battle-info-dialog/battle-info-dialog.component';


import { BattleVotingBlockComponent } from './battle-page/battle-voting-block/battle-voting-block.component';
import { OrdersListPageComponent } from './orders-list-page/orders-list-page.component';
import { CustomSelectionComponent } from './main-page/custom-selection/custom-selection.component';
import { CustomSelectionSettingsDialogComponent } from './main-page/custom-selection/custom-selection-settings-dialog/custom-selection-settings-dialog.component';
import {
  AutocompleteWithChipsComponent,
} from './main-page/custom-selection/autocomplete-with-chips/autocomplete-with-chips.component';

import {
  CustomSelectionInfoDialogComponent,
} from './main-page/custom-selection/custom-selection-info-dialog/custom-selection-info-dialog.component';


@NgModule({
  declarations: [
    AppComponent,
    BookSearchPageComponent,
    SelectionComponent,
    MainPageComponent,
    SelectionPageComponent,
    CustomSelectionComponent,
    CatalogComponent,
    BasketPageComponent,
    BasketElementComponent,
    BasketInfoBlockComponent,
    ProfilePageComponent,
    OrderPageComponent,
    PersonalDataFormComponent,
    OrderInfoComponent,
    OrdersListPageComponent,
    BattlePageComponent,
    BattleInfoDialogComponent,
    BattleVotingBlockComponent,
    CustomSelectionSettingsDialogComponent,
    CustomSelectionInfoDialogComponent,
    AutocompleteWithChipsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ProductCardPageModule,
    CommonModule,
    CoreModule,
    SharedModule,
    HttpClientModule,
    NgxPaginationModule,
    MatExpansionModule,
    MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    CdTimerModule,
    MatTooltipModule,
    MatDialogModule,
    MatChipsModule,
    MatSelectModule,
    MatAutocompleteModule,
    MatDividerModule,
    MatProgressSpinnerModule,
  ],
  providers: [ProductOptionsStorage, ProfileProviderService],
  bootstrap: [AppComponent],
})
export class AppModule { }
