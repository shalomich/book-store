import { Base64Image } from './base64-image';

export interface Album {
  titleImageName: string;
  images: Base64Image[];
}
