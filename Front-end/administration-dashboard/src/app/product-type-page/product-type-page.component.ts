import { Component, OnInit } from '@angular/core';

import { EntityType } from '../core/interfaces/entity-type';
import { ProductTypeConfigurationService } from '../core/services/product-type-configuration.service';

/** Main page's component. */
@Component({
  selector: 'app-product-type-page',
  templateUrl: './product-type-page.component.html',
  styleUrls: ['./product-type-page.component.css'],
})
export class ProductTypePageComponent implements OnInit {

  public readonly products: EntityType[]

  public constructor(productTypeConfiguration : ProductTypeConfigurationService) {
    this.products = productTypeConfiguration.getProductTypes();
  }

  public ngOnInit(): void {

  }

}
