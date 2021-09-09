/** Entity preview class. */
export class EntityPreview {
  /** Entity item's id. */
  public readonly id: number;

  /** Entity item's name. */
  public readonly name: string;

  public constructor(entity: EntityPreview) {
    this.id = entity.id;
    this.name = entity.name;
  }
}
