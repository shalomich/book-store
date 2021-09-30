
import { ProductConfig } from '../interfaces/product-config';
import { BookConfig } from './book-config';
import { EntityType } from '../interfaces/entity-type';

class ProductTypeConfiguration {
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
}

export default new ProductTypeConfiguration();
