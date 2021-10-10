import { Album } from '../interfaces/album';
import { EntityDto } from './entity-dto';

export interface ProductDto extends EntityDto {
  cost: number;
  quantity: number;
  description: string;
  album: Album;
}
