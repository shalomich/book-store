import ProductTypeConfig from '../../../products-config.json';
import { ProductType } from '../interfaces/product-type';
import { RelatedEntityType } from '../interfaces/related-entity-type';

class ProductTypeConfiguration {
  private readonly productConfig: ProductType[] = ProductTypeConfig;

  public getProducts(): ProductType[] {
    return this.productConfig;
  }

  public getProductName(type: string): string | undefined {
    const productName = this.productConfig.find(productType => productType.value === type)?.name;
    return productName ? productName : undefined;
  }

  public getProductRelatedEntities(type: string): RelatedEntityType[] {
    const relatedEntities = this.productConfig.find(productType => productType.value === type)?.relatedEntities;
    return relatedEntities ? relatedEntities : [];
  }

  public getRelatedEntityName(productType: string, relatedEntityType: string): string | undefined {
    const relatedEntity = this.getProductRelatedEntities(productType).find(entity => entity.value === relatedEntityType);
    return relatedEntity ? relatedEntity.name : undefined;
  }
}

export default new ProductTypeConfiguration();
