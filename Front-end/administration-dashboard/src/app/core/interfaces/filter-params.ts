import {Comparison} from "../utils/comparison";

export interface FilterParams {
  readonly propertyName: string,
  readonly comparison: Comparison
  value: string,
}
