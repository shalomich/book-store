export interface Image {

  /** Image's name. */
  readonly name: string;

  /** Image's format. */
  readonly format: string;

  readonly fileUrl: string;

  readonly height: number;

  readonly width: number;
}
