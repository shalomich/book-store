import { Base64Image } from '../interfaces/base64-image';

export interface BasketProductDto {
  id: number;
  name: string;
  cost: number;
  quantity: number;
  titleImage: Base64Image;
  productId: number;
}
