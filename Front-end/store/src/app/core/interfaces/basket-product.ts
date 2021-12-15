import { Base64Image } from './base64-image';

export interface BasketProduct {
  id: number;
  name: string;
  cost: number;
  quantity: number;
  titleImage: Base64Image;
}
