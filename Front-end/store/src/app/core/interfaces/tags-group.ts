import { Tag } from './tag';

export interface TagsGroup {
  readonly id: number;

  readonly name: string;

  readonly colorHex: string;

  readonly tags: Tag[];
}
