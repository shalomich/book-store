export  class RelatedEntityPreview {
  public readonly id: number;

  public readonly name: string;

  public constructor(preview: RelatedEntityPreview) {
    this.id = preview.id;
    this.name = preview.name;
  }
}
