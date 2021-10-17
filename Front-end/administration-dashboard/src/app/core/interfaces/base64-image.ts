/** Base64Image object. */
export interface Base64Image {

  /** Base64Image's name. */
  readonly name: string;

  /** Base64Image's format. */
  readonly format: string;

  /** Base64Image base64 data string. */
  readonly data: string;

  readonly height: number;

  readonly width: number;
}
