/** Image object. */
export interface Image {

  /** Image's name. */
  readonly name: string;

  /** Image's format. */
  readonly format: string;

  /** Image base64 data string. */
  readonly data: string;
}
