import { Comparison } from '../utils/comparison';

export interface FilterOptions {
  readonly propertyName: string;
  readonly comparison: Comparison;
  value: string;
}
