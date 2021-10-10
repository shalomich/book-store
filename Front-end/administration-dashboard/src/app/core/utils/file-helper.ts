import { Image } from '../interfaces/image';

export function fileToImage(file: File): Image {
  const reader = new FileReader();
  reader.readAsDataURL(file);
}
