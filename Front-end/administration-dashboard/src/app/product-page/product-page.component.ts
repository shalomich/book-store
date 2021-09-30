import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Observable } from 'rxjs';

import productTypeConfiguration from '../core/utils/product-type-configuration';

import { ProductPreview } from '../core/models/product-preview';
import { EntityType } from '../core/interfaces/entity-type';
import { EntityPreviewService } from '../core/services/entity-preview.service';

@Component({
  selector: 'app-product-page',
  templateUrl: './product-page.component.html',
  styleUrls: ['./product-page.component.css'],
})
export class ProductPageComponent implements OnInit {

  public readonly productName: string | undefined;

  public readonly relatedEntities: EntityType[];

  public readonly productType: string;

  public readonly productList$: Observable<ProductPreview[]>;

  /** URL validation status. */
  public isValid = true;

  public constructor(
    private readonly activatedRoute: ActivatedRoute,
    private readonly previewService: EntityPreviewService,
  ) {
    this.productType = this.activatedRoute.snapshot.params.product;
    this.productName = productTypeConfiguration.getProductName(this.productType);
    this.relatedEntities = productTypeConfiguration.getProductRelatedEntities(this.productType);

    this.productList$ = this.previewService.getProductPreviews(this.productType);
  }

  public ngOnInit(): void {
    if (!this.productName) {
      this.isValid = false;
    }
  }

}
