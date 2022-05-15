import { Image } from '../interfaces/image';

export interface BasketProductDto {
  id: number;
  name: string;
  cost: number;
  quantity: number;
  titleImage: Image;
  productId: number;
}
