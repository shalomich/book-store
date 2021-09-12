import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

import productTypeConfiguration from '../core/utils/product-type-configuration';

import { ProductService } from '../core/services/product.service';
import { ProductPreview } from '../core/models/product-preview';
import { EntityType } from '../core/interfaces/entity-type';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css'],
})
export class ProductPageComponent implements OnInit {

  public readonly productName: string | null;

  public readonly relatedEntities: EntityType[];

  public readonly productType: string;

  public readonly entityList$: Observable<ProductPreview[]>;

  public constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly productService: ProductService,
  ) {
    this.productType = this.activatedRoute.snapshot.params.product;
    this.productName = productTypeConfiguration.getProductName(this.productType);
    this.relatedEntities = productTypeConfiguration.getProductRelatedEntities(this.productType);

    this.entityList$ = this.productService.getProductPage(this.productType);
  }

  public ngOnInit(): void {
  }

}
