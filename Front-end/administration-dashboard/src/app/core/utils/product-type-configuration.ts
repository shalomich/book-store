import ProductTypeConfig from '../../../products-config.json';
import { ProductType } from '../interfaces/product-type';
import { RelatedEntityType } from '../interfaces/related-entity-type';

class ProductTypeConfiguration {
  private readonly productConfig: ProductType[] = ProductTypeConfig;

  public getProducts(): ProductType[] {
    return this.productConfig;
  }

  public getProductName(type: string): string | undefined {
    return this.productConfig.find(productType => productType.value === type)?.name;
  }

  public getProductRelatedEntities(type: string): RelatedEntityType[] {
    const relatedEntities = this.productConfig.find(productType => productType.value === type)?.relatedEntities;
    return relatedEntities ? relatedEntities : [];
  }

  public getRelatedEntityName(productType: string, relatedEntityType: string): string | undefined {
    return this.getProductRelatedEntities(productType).find(entity => entity.value === relatedEntityType)?.name;
  }
}

export default new ProductTypeConfiguration();
