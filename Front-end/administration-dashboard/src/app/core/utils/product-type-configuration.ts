import ProductTypeConfig from '../../../products-config.json';
import { ProductType } from '../interfaces/product-type';
import { EntityType } from '../interfaces/entity-type';

class ProductTypeConfiguration {
  private readonly productConfig: ProductType[] = ProductTypeConfig;

  public getProducts(): ProductType[] {
    return this.productConfig;
  }

  public getProductName(type: string): string {
    const productName = this.productConfig.find(productType => productType.value === type)?.name;
    return productName ? productName : '';
  }

  public getProductRelatedEntities(type: string): EntityType[] {
    const relatedEntities = this.productConfig.find(productType => productType.value === type)?.relatedEntities;
    return relatedEntities ? relatedEntities : [];
  }

  public getRelatedEntityName(productType: string, relatedEntityType: string): string {
    const relatedEntity = this.getProductRelatedEntities(productType).find(entity => entity.value === relatedEntityType);
    return relatedEntity ? relatedEntity.name : '';
  }
}

export default new ProductTypeConfiguration();
