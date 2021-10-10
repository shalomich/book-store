
import { Injectable } from '@angular/core';

import { ProductConfig } from '../interfaces/product-config';
import { BookConfig } from '../utils/book-config';
import { EntityType } from '../interfaces/entity-type';


@Injectable({
  providedIn: 'root',
})
export class ProductTypeConfigurationService {
  private readonly productConfigs: Array<ProductConfig> = [new BookConfig()];

  private getProductConfig(type: string) {
    return this.productConfigs.find(config => config.entityType.value == type);
  }

  public getProductTypes(): EntityType[] {
    return this.productConfigs.map(config => config.entityType);
  }

  public getProductName(type: string): string | undefined {
    return this.getProductConfig(type)?.entityType.name;
  }

  public getProductRelatedEntityTypes(type: string): EntityType[] {
    return this.getProductConfig((type))?.relatedEntityConfigs.map(config => config.entityType) ?? [];
  }

  public getRelatedEntityName(productType: string, relatedEntityType: string): string | undefined {
    return this.getProductRelatedEntityTypes(productType).find(entity => entity.value === relatedEntityType)?.name;
  }

  public isProduct(entityType: string): boolean {
    return this.getProductTypes().some(productType => productType.value == entityType);
  }

  public isRelatedEntity(entityType: string): boolean {
    let isRelatedEntityStatus = false;

    for (const productConfig of this.productConfigs) {
      if (productConfig.relatedEntityConfigs.some(config => config.entityType.value == entityType)) {
        isRelatedEntityStatus = true;
        break;
      }
    }

    return isRelatedEntityStatus;
  }

  public isEntity(entityType: string): boolean {
    return this.isProduct(entityType) || this.isRelatedEntity(entityType);
  }
}

