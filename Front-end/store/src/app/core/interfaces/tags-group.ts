import { Tag } from './tag';

export interface TagsGroup {
  readonly id: number;

  readonly name: string;

  readonly tags: Tag[];
}
