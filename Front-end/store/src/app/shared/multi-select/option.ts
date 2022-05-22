/**
 * Options are used in multi - select.
 */
export interface  Option {
  /**
   * Name of the option by which filtering.
   */
  name: string;
  /**
   * the value in the multi-select can be any because it can be implemented by compare with.
   */
  value: any;
}
