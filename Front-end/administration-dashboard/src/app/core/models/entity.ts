export class Entity {
  public readonly id: number;

  public readonly name: string;

  public constructor(entity: Entity) {
    this.id = entity.id;
    this.name = entity.name;
  }
}
